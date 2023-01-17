using BasicMVC.Models;
using Microsoft.EntityFrameworkCore;
using registration.Business.Interfaces;
using registration.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace registration.Data.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(registrationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Product>> GetProductsBySuppiler(Guid supplierID)
        {
            return await Find(p => p.SupplierId == supplierID);
        }

        public async Task<IEnumerable<Product>> GetProductsSuppilers()
        {
            return await Db.Products.AsNoTracking().Include(s => s.Supplier).
                OrderBy(p => p.Name).ToListAsync();
               
        }

       

        public async Task<Product> GetProductSupplier(Guid id)
        {
            return await Db.Products.AsNoTracking().Include(s => s.Supplier)
               .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
