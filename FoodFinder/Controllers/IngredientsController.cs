using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using FoodFinder.Models;
using System.Collections.Generic;
using System.Linq;

namespace FoodFinder.Controllers
{
  public class IngredientsController : Controller
  {
    private readonly FoodFinderContext _db;

    public IngredientsController(FoodFinderContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      return View(_db.Ingredients.ToList());
    }

    public ActionResult Create()
    {
      ViewBag.RecipeId = new SelectList(_db.Recipes, "RecipeId", "Name");
      return View();
    }

    [HttpPost]
    public ActionResult Create(Ingredient ingredient, int RecipeId)
    {
      _db.Ingredients.Add(ingredient);
      _db.SaveChanges();
      if (RecipeId != 0)
      {
        _db.IngredientRecipe.Add(new IngredientRecipe() { RecipeId = RecipeId, IngredientId = ingredient.IngredientId });
        _db.SaveChanges();
      }
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      var thisIngredient = _db.Ingredients
          .Include(ingredient => ingredient.JoinEntities)
          .ThenInclude(join => join.Ingredient)
          .FirstOrDefault(ingredient => ingredient.IngredientId == id);
      return View(thisIngredient);
    }

    public ActionResult Edit(int id)
    {
      var thisIngredient = _db.Ingredients.FirstOrDefault(ingredient => ingredient.IngredientId == id);
      ViewBag.RecipeId = new SelectList(_db.Recipes, "RecipeId", "Name");
      return View(thisIngredient);
    }

    [HttpPost]
    public ActionResult Edit(Ingredient ingredient, int RecipeId)
    {
      if (RecipeId != 0)
      {
        _db.IngredientRecipe.Add(new IngredientRecipe() { RecipeId = RecipeId, IngredientId = ingredient.IngredientId });
      }
      _db.Entry(ingredient).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult AddRecipe(int id)
    {
      var thisIngredient = _db.Ingredients.FirstOrDefault(ingredient => ingredient.IngredientId == id);
      ViewBag.RecipeId = new SelectList(_db.Recipes, "RecipeId", "Name");
      return View(thisIngredient);
    }

    [HttpPost]
    public ActionResult AddRecipe(Ingredient ingredient, int RecipeId)
    {
      if (RecipeId != 0)
      {
        _db.IngredientRecipe.Add(new IngredientRecipe() { RecipeId = RecipeId, IngredientId = ingredient.IngredientId });
        _db.SaveChanges();
      }
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      var thisIngredient = _db.Ingredients.FirstOrDefault(ingredient => ingredient.IngredientId == id);
      return View(thisIngredient);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisIngredient = _db.Ingredients.FirstOrDefault(ingredient => ingredient.IngredientId == id);
      _db.Ingredients.Remove(thisIngredient);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [HttpPost]
    public ActionResult DeleteRecipe(int joinId)
    {
      var joinEntry = _db.IngredientRecipe.FirstOrDefault(entry => entry.IngredientRecipeId == joinId);
      _db.IngredientRecipe.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}
