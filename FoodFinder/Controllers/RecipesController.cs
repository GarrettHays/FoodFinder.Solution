using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using FoodFinder.Models;
using System.Collections.Generic;
using System.Linq;

namespace FoodFinder.Controllers
{
  public class RecipesController : Controller
  {
    private readonly FoodFinderContext _db;

    public RecipesController(FoodFinderContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      return View(_db.Recipes.ToList());
    }

    public ActionResult Create()
    {
      ViewBag.RestaurantId = new SelectList(_db.Restaurants, "RestaurantId", "Name");
      return View();
    }

    [HttpPost]
    public ActionResult Create(Recipe recipe, int RestaurantId)
    {
      _db.Recipes.Add(recipe);
      _db.SaveChanges();
      if (RestaurantId != 0)
      {
        _db.RecipeRestaurant.Add(new RecipeRestaurant() { RestaurantId = RestaurantId, RecipeId = recipe.RecipeId });
        _db.SaveChanges();
      }
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      var thisRecipe = _db.Recipes
          .Include(recipe => recipe.JoinEntities)
          .ThenInclude(join => join.Recipe)
          .FirstOrDefault(recipe => recipe.RecipeId == id);
      return View(thisRecipe);
    }

    public ActionResult Edit(int id)
    {
      var thisRecipe = _db.Recipes.FirstOrDefault(recipe => recipe.RecipeId == id);
      ViewBag.RestaurantId = new SelectList(_db.Restaurants, "RestaurantId", "Name");
      return View(thisRecipe);
    }

    [HttpPost]
    public ActionResult Edit(Recipe recipe, int RestaurantId)
    {
      if (RestaurantId != 0)
      {
        _db.RecipeRestaurant.Add(new RecipeRestaurant() { RestaurantId = RestaurantId, RecipeId = recipe.RecipeId });
      }
      _db.Entry(recipe).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
       // change description on 74 to name maybe?
    public ActionResult AddRestaurant(int id)
    {
      var thisRecipe = _db.Recipes.FirstOrDefault(Recipe => Recipe.RecipeId == id);
      ViewBag.RestaurantId = new SelectList(_db.Restaurants, "RestaurantId", "Description");
      return View(thisRecipe);
    }

    [HttpPost]
    public ActionResult AddRestaurant(Recipe recipe, int RestaurantId)
    {
      if (RestaurantId != 0)
      {
        _db.RecipeRestaurant.Add(new RecipeRestaurant() { RestaurantId = RestaurantId, RecipeId = recipe.RecipeId });
        _db.SaveChanges();
      }
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      var thisRecipe = _db.Recipes.FirstOrDefault(Recipe => Recipe.RecipeId == id);
      return View(thisRecipe);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisRecipe = _db.Recipes.FirstOrDefault(Recipe => Recipe.RecipeId == id);
      _db.Recipes.Remove(thisRecipe);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [HttpPost]
    public ActionResult DeleteRestaurant(int joinId)
    {
      var joinEntry = _db.RecipeRestaurant.FirstOrDefault(entry => entry.RecipeRestaurantId == joinId);
      _db.RecipeRestaurant.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [HttpPost]
    public ActionResult DeleteIngredient(int joinId)
    {
      var joinEntry = _db.IngredientRecipe.FirstOrDefault(entry => entry.IngredientRecipeId == joinId);
      _db.IngredientRecipe.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}
