using CookBook.Library.Entities;
using CookBook.Library.Repositories.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace CookBook.Controllers
{
    public class KitchenController : Controller
    {
        private readonly IDishRepository _dishRepo;

        public KitchenController(IDishRepository dishRepo)
        {
            _dishRepo = dishRepo;
        }

        public IActionResult Index()
        {
            return View(_dishRepo.GetDishes());
        }

        public IActionResult Edit(int dishId)
        {
            Dish? dish = _dishRepo.GetDish(dishId);
            if (dish != null)
                return View(dish);

            return RedirectToAction("Index");
        }
    }
}
