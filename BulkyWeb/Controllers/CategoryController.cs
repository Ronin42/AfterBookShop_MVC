using BulkyWeb.Data;
using BulkyWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BulkyWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDBContext _db;
        public CategoryController(ApplicationDBContext db)
        {
            _db = db;
        }


        public IActionResult Index()
        {
            List<Category> objcategoryList = _db.Categories.ToList();
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
                _db.Categories.Add(obj);
                _db.SaveChanges();
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
            Category? CategoryFromDB = _db.Categories.Find(id);

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
                Console.WriteLine("id = 0");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Update(obj);
                _db.SaveChanges();
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
            Category? CategoryFromDB = _db.Categories.Find(id);

            if (CategoryFromDB == null)
            {
                return NotFound();
            }
            return View(CategoryFromDB);
        }

        [HttpPost,ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            Category? obj = _db.Categories.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            _db.Categories.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");

        }

    }
}
