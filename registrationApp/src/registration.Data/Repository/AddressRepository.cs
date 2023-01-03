using BasicMVC.Models;
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
        public Task<Address> GetAddressBySupplier(Guid supplierId)
        {
            throw new NotImplementedException();
        }
    }
}
