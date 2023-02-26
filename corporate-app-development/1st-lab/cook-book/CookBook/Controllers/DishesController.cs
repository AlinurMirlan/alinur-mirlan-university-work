using CookBook.Library.Entities;
using CookBook.Library.Repositories.Abstractions;
using CookBook.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace CookBook.Controllers
{
    public class DishesController : Controller
    {
        private readonly IDishRepository _dishRepo;

        public DishesController(IDishRepository dishRepo)
        {
            _dishRepo = dishRepo;
        }

        public IActionResult Index()
        {
            return View(_dishRepo.GetDishes());
        }

        [HttpPost]
        public IActionResult Index(int dishId)
        {
            _dishRepo.DeleteDish(dishId);
            return View(_dishRepo.GetDishes());
        }

        public IActionResult Edit(int dishId)
        {
            Dish? dish = _dishRepo.GetDish(dishId);
            if (dish is null)
                return RedirectToAction("Index");

            if (dish.Ingredients.Count == 0)
                dish.Ingredients.Add(new DishIngredient());
            DishErrorsViewModel model = new() { Dish = dish };
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(DishErrorsViewModel dishNErrors)
        {
            Dish newDish = dishNErrors.Dish;
            Dish? oldDish = _dishRepo.GetDish(newDish.Id);
            if (oldDish is null)
                return RedirectToAction("Index");

            try
            {
                _dishRepo.EditDish(oldDish, newDish);
            }
            catch (AggregateException exceptions)
            {
                foreach (Exception exception in exceptions.InnerExceptions)
                {
                    if (exception is SqlException sqlException)
                    {
                        foreach (SqlError error in sqlException.Errors)
                            dishNErrors.ErrorMessages.Add(error.Message);
                    }
                    else if (exception is ArgumentException argumentException)
                        dishNErrors.ErrorMessages.Add(argumentException.Message);
                }

                return View(nameof(Edit), dishNErrors);
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Add()
        {
            Dish dish = new();
            dish.Ingredients.Add(new DishIngredient());
            DishErrorsViewModel model = new() { Dish = dish };
            return View(model);
        }

        [HttpPost]
        public IActionResult Add(DishErrorsViewModel dishNErrors)
        {
            Dish dish = dishNErrors.Dish;
            try
            {
                _dishRepo.AddDish(dish, dish.Ingredients.ToArray());
            }
            catch (AggregateException exceptions)
            {
                foreach (Exception exception in exceptions.InnerExceptions)
                {
                    if (exception is SqlException sqlException)
                    {
                        foreach (SqlError error in sqlException.Errors)
                            dishNErrors.ErrorMessages.Add(error.Message);
                    }
                    else if (exception is ArgumentException argumentException)
                        dishNErrors.ErrorMessages.Add(argumentException.Message);
                }

                return View(nameof(Add), dishNErrors);
            }

            return RedirectToAction(nameof(Add));
        }

        [HttpPost]
        public IActionResult AddIngredient(DishErrorsViewModel dishNErrors)
        {
            dishNErrors.Dish.Ingredients.Add(new DishIngredient());
            return View(nameof(Add), dishNErrors);
        }

        [HttpPost]
        public IActionResult AddIngredientOnEdit(DishErrorsViewModel dishNErrors)
        {
            dishNErrors.Dish.Ingredients.Add(new DishIngredient());
            return View(nameof(Edit), dishNErrors);
        }
    }
}
