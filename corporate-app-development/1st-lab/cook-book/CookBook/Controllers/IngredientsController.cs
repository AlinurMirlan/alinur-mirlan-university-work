using CookBook.Library.Entities;
using CookBook.Library.Repositories;
using CookBook.Library.Repositories.Abstractions;
using CookBook.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data.SqlClient;
using System.Globalization;

namespace CookBook.Controllers
{
    public class IngredientsController : Controller
    {
        private readonly IIngredientRepository _ingredientRepo;
        private readonly IUnitRepository _unitRepo;

        public IngredientsController(IIngredientRepository ingredientRepo, IUnitRepository unitRepo)
        {
            this._ingredientRepo = ingredientRepo;
            this._unitRepo = unitRepo;
        }

        public IActionResult Index()
        {
            return View(_ingredientRepo.GetIngredients());
        }

        [HttpGet]
        public IActionResult Add()
        {
            IngredientUnitsViewModel model = new()
            {
                Ingredient = new Ingredient(),
                Units = new List<SelectListItem>()
            };
            foreach (string unit in _unitRepo.GetAllUnits())
                model.Units.Add(new SelectListItem(unit, unit));

            return View(model);
        }

        [HttpPost]
        public IActionResult Add(IngredientUnitsViewModel ingredientViewModel)
        {
            try
            {
                _ingredientRepo.AddIngredient(ingredientViewModel.Ingredient);
            }
            catch (AggregateException exceptions)
            {
                foreach (Exception exception in exceptions.InnerExceptions)
                {
                    if (exception is SqlException sqlException)
                    {
                        foreach (SqlError error in sqlException.Errors)
                            ingredientViewModel.ErrorMessages.Add(error.Message);
                    }
                    else if (exception is ArgumentException argumentException)
                        ingredientViewModel.ErrorMessages.Add(argumentException.Message);
                }

                return View(nameof(Add), ingredientViewModel);
            }

            return RedirectToAction(nameof(Add));
        }

        [HttpGet]
        public IActionResult Edit(int ingredientId)
        {
            Ingredient? ingredient = _ingredientRepo.GetIngredient(ingredientId);
            if (ingredient is null)
                return RedirectToAction(nameof(Index));

            IngredientUnitsViewModel model = new()
            {
                Ingredient = ingredient,
                Units = new List<SelectListItem>()
            };
            foreach (string unit in _unitRepo.GetAllUnits())
                model.Units.Add(new SelectListItem(unit, unit));

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(IngredientUnitsViewModel ingredientViewModel)
        {
            Ingredient newIngredient = ingredientViewModel.Ingredient;
            Ingredient oldIngredient = _ingredientRepo.GetIngredient(newIngredient.Id)!;
            try
            {
                _ingredientRepo.EditIngredient(oldIngredient, newIngredient);
            }
            catch (AggregateException exceptions)
            {
                foreach (Exception exception in exceptions.InnerExceptions)
                {
                    if (exception is SqlException sqlException)
                    {
                        foreach (SqlError error in sqlException.Errors)
                            ingredientViewModel.ErrorMessages.Add(error.Message);
                    }
                    else if (exception is ArgumentException argumentException)
                        ingredientViewModel.ErrorMessages.Add(argumentException.Message);
                }

                return View(nameof(Edit), ingredientViewModel);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Delete(int ingredientId)
        {
            _ingredientRepo.DeleteIngredient(ingredientId);

            return RedirectToAction(nameof(Index));
        }
    }
}
