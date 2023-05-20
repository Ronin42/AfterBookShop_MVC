using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDBContext _db;
        public ICategoryRepository CategoryRepo { get; private set; }

        public IProductRepository productRepo { get; private set; }

        public UnitOfWork(ApplicationDBContext db)
        { 
            _db = db; 
            CategoryRepo = new CategoryRepository(_db);
            productRepo= new ProductRepository(_db);
        }
      

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
