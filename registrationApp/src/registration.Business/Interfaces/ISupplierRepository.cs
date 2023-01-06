using BasicMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace registration.Business.Interfaces
{
    public interface ISupplierRepository : IRepository<Supplier>
    {
        Task<Supplier> GetSupplierAddress(Guid id);
        Task<Supplier> GetProductsSupplierAddress(Guid id);
    }
}
