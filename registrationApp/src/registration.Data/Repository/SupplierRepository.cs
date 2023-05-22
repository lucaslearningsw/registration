using BasicMVC.Models;
using Microsoft.EntityFrameworkCore;
using registration.Business.Interfaces;
using registration.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace registration.Data.Repository
{
    public class SupplierRepository : Repository<Supplier>, ISupplierRepository
    {
        public SupplierRepository(registrationDbContext dbContext) : base(dbContext){ }

    
        public async Task<Supplier> GetProductsSupplierAddress(Guid id)
        {

            return await Db.Suppliers.AsNoTracking()
                .Include(s => s.Address)
                .Include(s => s.Products)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<Supplier> GetSupplierAddress(Guid id)
        {
            return await Db.Suppliers.AsNoTracking()
                .Include(s => s.Address)
                .FirstOrDefaultAsync(s => s.Id == id);
             
        }

       
    } 
 }


