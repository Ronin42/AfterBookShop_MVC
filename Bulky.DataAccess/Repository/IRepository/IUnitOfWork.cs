using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        ICategoryRepository CategoryRepo { get; }
        IProductRepository productRepo { get; }

        ICompanyRepository CompanyRepo { get; }
        void Save();
    }
}
