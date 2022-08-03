using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using FoodFinder.Models;
using System.Collections.Generic;
using System.Linq;

namespace FoodFinder.Controllers
{
  public class MainController : Controller
  {
    public ActionResult Index()
    {
      return View();
    }
  }
}