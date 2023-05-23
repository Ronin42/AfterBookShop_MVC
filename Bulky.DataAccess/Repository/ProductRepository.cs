using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository
{
    public class ProductRepository : Repository<Product> , IProductRepository
    {
        private ApplicationDBContext _db;
        public ProductRepository(ApplicationDBContext db): base(db)
        {
            _db = db;
        }

        

        public void update(Product obj)
        {
            //_db.Products.Update(obj);
            var objFromDB = _db.Products.FirstOrDefault(u => u.Id == obj.Id);
            if (objFromDB != null)
            {
                objFromDB.Title = obj.Title;
                objFromDB.Description = obj.Description;
                objFromDB.CategoryId = obj.CategoryId;
                objFromDB.ISBN= obj.ISBN;
                objFromDB.Price = obj.Price;
                objFromDB.Price50 = obj.Price50;
                objFromDB.Price100 = obj.Price100;
                objFromDB.ListPrice = obj.ListPrice;
                objFromDB.Author = obj.Author;
                if(obj.ImageUrl != null)
                {
                    objFromDB.ImageUrl = obj.ImageUrl;
                }
            }
        }
    }
}

