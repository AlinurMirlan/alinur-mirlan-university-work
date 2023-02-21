using CookBook.Library.Repositories.Abstractions;
using CookBook.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CookBook.Controllers
{
    public class StoreController : Controller
    {
        private readonly IDishRepository _dishRepo;

        public StoreController(IDishRepository dishRepo)
        {
            _dishRepo = dishRepo;
        }

        public IActionResult Index()
        {
            return View(_dishRepo.GetDishes());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}