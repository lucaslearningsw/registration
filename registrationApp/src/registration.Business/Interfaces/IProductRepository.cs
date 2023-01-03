using BasicMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace registration.Business.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IEnumerable<Product>> GetProductsBySuppiler(Guid supplierID);

        Task<IEnumerable<Product>> GetProductsBySuppilers();
        Task<Product> GetProductSupplier(Guid productID);
    }
}
