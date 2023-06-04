using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Bulky.Utillity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BulkyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public IActionResult Index()
        {
            List<Category> objcategoryList = _unitOfWork.CategoryRepo.GetAll().ToList();
            return View(objcategoryList);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "Display Order Cannot exactly Match the name");
            }
            if (obj.Name != null && obj.Name.ToLower() == "badname")
            {
                ModelState.AddModelError("", "you cannot use this name its too bad!!");
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.CategoryRepo.Add(obj);
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? CategoryFromDB = _unitOfWork.CategoryRepo.Get(u => u.CategoryId == id);

            if (CategoryFromDB == null)
            {
                return NotFound();
            }
            return View(CategoryFromDB);
        }
        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            if (obj.CategoryId == 0)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.CategoryRepo.update(obj);
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View(obj);

        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? CategoryFromDB = _unitOfWork.CategoryRepo.Get(u => u.CategoryId == id);

            if (CategoryFromDB == null)
            {
                return NotFound();
            }
            return View(CategoryFromDB);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            Category? obj = _unitOfWork.CategoryRepo.Get(u => u.CategoryId == id);
            if (obj == null)
            {
                return NotFound();
            }

            _unitOfWork.CategoryRepo.Remove(obj);
            _unitOfWork.Save();
            //TempData["success"] = "ทำการลบหมวดหมู่แล้ว";
            return RedirectToAction("Index");

        }

    }
}
