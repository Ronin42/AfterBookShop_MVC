﻿using Bulky.DataAccess.Data;
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

        public ICompanyRepository CompanyRepo { get; private set; }

        public IShoppingCartRepository ShoppingCartRepo { get; private set; }

        public IApplicationUserRepository ApplicationUserRepo { get; private set; }

        public IOrderDetailRepository OrderDetailRepo { get; private set; }

        public IOrderHeaderRepository OrderHeaderRepo { get; private set; }

        //get; private set;: การกำหนดการเข้าถึงค่าของคุณสมบัติ productRepo โดยมีการกำหนด
        //get ให้สามารถอ่านค่าได้
        //และ private set ที่บังคับให้สามารถกำหนดค่าได้เฉพาะภายในคลาสเท่านั้น นั่นหมายความว่าค่าของ productRepo สามารถเข้าถึงได้จากภายนอก
        //แต่การกำหนดค่าต้องทำภายในคลาสเท่านั้น

        public UnitOfWork(ApplicationDBContext db)
        { 
            _db = db; 
            CategoryRepo = new CategoryRepository(_db);
            productRepo= new ProductRepository(_db);
            CompanyRepo = new CompanyRepository(_db);
            ShoppingCartRepo = new ShoppingCartRepository(_db);
            ApplicationUserRepo = new ApplicationUserRepository(_db);
            OrderDetailRepo= new OrderDetailRepository(_db);
            OrderHeaderRepo= new OrderHeaderRepository(_db);
        }
      

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
