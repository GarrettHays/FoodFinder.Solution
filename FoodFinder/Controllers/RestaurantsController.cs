using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using FoodFinder.Models;
using System.Collections.Generic;
using System.Linq;

namespace FoodFinder.Controllers
{
  public class RestaurantsController : Controller
  {
    private readonly FoodFinderContext _db;

    public RestaurantsController(FoodFinderContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      List<Restaurant> model = _db.Restaurants.ToList();
      return View(model);
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Create(Restaurant Restaurant)
    {
      _db.Restaurants.Add(Restaurant);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      var thisRecipe = _db.Restaurants
          .Include(Restaurant => Restaurant.JoinEntities)
          .ThenInclude(join => join.Restaurant)
          .FirstOrDefault(Restaurant => Restaurant.RestaurantId == id);
      return View(thisRecipe);
    }
    public ActionResult Edit(int id)
    {
      var thisRecipe = _db.Restaurants.FirstOrDefault(Restaurant => Restaurant.RestaurantId == id);
      return View(thisRecipe);
    }

    [HttpPost]
    public ActionResult Edit(Restaurant Restaurant)
    {
      _db.Entry(Restaurant).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      var thisRecipe = _db.Restaurants.FirstOrDefault(Restaurant => Restaurant.RestaurantId == id);
      return View(thisRecipe);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisRecipe = _db.Restaurants.FirstOrDefault(Restaurant => Restaurant.RestaurantId == id);
      _db.Restaurants.Remove(thisRecipe);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}