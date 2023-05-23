using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Bulky.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Xml.XPath;

namespace BulkyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }


        public IActionResult Index()
        {
            List<Product> objProductList = _unitOfWork.productRepo.GetAll(includeProperties:"Category").ToList();
            IEnumerable<SelectListItem> CategoryList = _unitOfWork.CategoryRepo
                .GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.CategoryId.ToString()
                });
            return View(objProductList);
        }

        public IActionResult Upsert(int? id) //update+insert
        {

            ProductVM productVM = new()
            {
                CategoryList = _unitOfWork.CategoryRepo
                .GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.CategoryId.ToString()
                }),
                Product = new Product()
            };
            if (id == null || id == 0)
            {
                //create
                return View(productVM);
            }
            else
            {
                //update
                productVM.Product = _unitOfWork.productRepo.Get(u => u.Id == id);
                return View(productVM);
            }

        }
        [HttpPost]
        public IActionResult Upsert(ProductVM productVM, IFormFile? file)
        //IFormFile? file มาจากหน้า Create ตรง form ตรง enctype="multipart/form-data"
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string filename = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string productPath = Path.Combine(wwwRootPath, @"images\product");

                    if (!string.IsNullOrEmpty(productVM.Product.ImageUrl))
                    {
                        //delete old image
                        var oldImagePath = Path.Combine(wwwRootPath, productVM.Product.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }
                    
                    using (var fileStream = new FileStream(Path.Combine(productPath, filename ), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    productVM.Product.ImageUrl = @"\images\product\" + filename;
                }
                if (productVM.Product.Id == 0)
                {
                    _unitOfWork.productRepo.Add(productVM.Product);
                }
                else
                {
                    _unitOfWork.productRepo.update(productVM.Product);
                }

                //_unitOfWork.productRepo.Add(productVM.Product);
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            else
            {
                productVM.CategoryList = _unitOfWork.CategoryRepo
                .GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.CategoryId.ToString()
                });
                return View(productVM);
            }


        }


        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Product? ProductFromDB = _unitOfWork.productRepo.Get(u => u.Id == id);

            if (ProductFromDB == null)
            {
                return NotFound();
            }
            return View(ProductFromDB);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            Product? obj = _unitOfWork.productRepo.Get(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }

            _unitOfWork.productRepo.Remove(obj);
            _unitOfWork.Save();

            return RedirectToAction("Index");

        }

        #region API CALLS
        [HttpGet]
         public IActionResult GetAll() { 
            List<Product> objProductList = _unitOfWork.productRepo.GetAll(includeProperties:"Category").ToList();
            return Json(new { data = objProductList });
        }
        #endregion

    }
}
