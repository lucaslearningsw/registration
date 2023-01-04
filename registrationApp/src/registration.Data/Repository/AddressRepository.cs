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
    public class AddressRepository : Repository<Address>, IAddressRepository
    {
        public AddressRepository(registrationDbContext dbContext) : base(dbContext)
        {

        }
        public async Task<Address> GetAddressBySupplier(Guid supplierId)
        {
            return await Db.Addresses.AsNoTracking().
                FirstOrDefaultAsync(s => s.SupplierId == supplierId);
        }
    }
}
