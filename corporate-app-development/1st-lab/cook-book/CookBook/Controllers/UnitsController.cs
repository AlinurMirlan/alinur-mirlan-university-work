using CookBook.Library.Entities;
using CookBook.Library.Repositories.Abstractions;
using CookBook.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace CookBook.Controllers
{
    public class UnitsController : Controller
    {
        private readonly IUnitRepository _unitRepo;

        public UnitsController(IUnitRepository unitRepo)
        {
            _unitRepo = unitRepo;
        }

        public IActionResult Index()
        {
            UnitsViewModel model = new() { Units = _unitRepo.GetAll() };
            return View(model);
        }

        [HttpPost]
        public IActionResult Delete(int unitId)
        {
            try
            {
                _unitRepo.DeleteUnit(unitId);
            }
            catch (SqlException)
            {
                UnitsViewModel model = new() { Units = _unitRepo.GetAll(), DeletedUnitIsInUse = true, DeletedUnitName = _unitRepo.GetUnit(unitId) };
                return View(nameof(Index), model);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Add()
        {
            UnitsViewModel model = new();
            return View(model);
        }

        [HttpPost]
        public IActionResult Add(Unit unit)
        {
            UnitsViewModel model = new();
            try
            {
                _unitRepo.AddUnit(unit.Name);
            }
            catch (ArgumentException exception)
            {
                model.ErrorMessage = exception.Message;
                return View(nameof(Add), model);
            }

            return RedirectToAction(nameof(Add));
        }

        [HttpGet]
        public IActionResult Edit(int unitId)
        {
            string unitName = _unitRepo.GetUnit(unitId)!;
            UnitsViewModel model = new() { Unit = new Unit() { Name = unitName, Id = unitId } };
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(Unit unit)
        {
            UnitsViewModel model = new() { Unit = unit };
            try
            {
                _unitRepo.EditUnitName(unit.Id, unit.Name);
            }
            catch (ArgumentException exception)
            {
                model.ErrorMessage = exception.Message;
                return View(nameof(Edit), model);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
