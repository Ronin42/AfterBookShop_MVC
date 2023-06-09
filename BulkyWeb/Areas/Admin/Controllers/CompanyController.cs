using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Bulky.Models.ViewModels;
using Bulky.Utillity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using System;
using System.Data;
using System.Xml.XPath;

namespace BulkyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        
        public CompanyController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
           
        }


        public IActionResult Index()
        {
            List<Company> objCompanyList = _unitOfWork.CompanyRepo.GetAll().ToList();
            IEnumerable<SelectListItem> CategoryList = _unitOfWork.CategoryRepo
                .GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.CategoryId.ToString()
                });
            return View(objCompanyList);
        }

        public IActionResult Upsert(int? id) //update+insert
        {

           
               
            if (id == null || id == 0)
            {
                //create
                return View(new Company());
            }
            else
            {
                //update
                Company CompanyObj = _unitOfWork.CompanyRepo.Get(u => u.Id == id);
                return View(CompanyObj);
            }

        }
        [HttpPost]
        public IActionResult Upsert(Company CompanyObj)
        //IFormFile? file มาจากหน้า Create ตรง form ตรง enctype="multipart/form-data"
        {
            if (ModelState.IsValid)
            {
                
               
                if (CompanyObj.Id == 0)
                {
                    _unitOfWork.CompanyRepo.Add(CompanyObj);
                }
                else
                {
                    _unitOfWork.CompanyRepo.update(CompanyObj);
                }

                
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            else
            {
                
                return View(CompanyObj);
            }


        }


        #region API CALLS
        [HttpGet]
         public IActionResult GetAll() {
            List<Company> objCompanyList = _unitOfWork.CompanyRepo.GetAll().ToList();
            return Json(new { data = objCompanyList });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var CompanyToBeDeleted = _unitOfWork.CompanyRepo.Get(u => u.Id == id);
            if (CompanyToBeDeleted == null)
            {
                return Json(new { success = false, message = "ไม่สามารถลบบริษัทนี้ได้" });
            }

            

            _unitOfWork.CompanyRepo.Remove(CompanyToBeDeleted);
            _unitOfWork.Save();
           
            return Json(new { success = true, message = "ลบบริษัทออกจากระบบสำเร็จ" });
        }
        #endregion

    }
}
