using Microsoft.AspNetCore.Mvc;

namespace FoodFinder.Controllers
{
    public class HomeController : Controller
    {

      [HttpGet("/")]
      public ActionResult Index()
      {
        return View();
      }

    }
}