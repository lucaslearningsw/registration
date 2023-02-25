using BasicMVC.Models;
using registration.Business.Interfaces;

namespace registration.Business.Services
{
    public class SupplierService : BaseService, ISupplierService
    {
        public Task Create(Supplier supplier)
        {
            throw new NotImplementedException();
        }

        public Task Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task Update(Supplier supplier)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAddress(Address address)
        {
            throw new NotImplementedException();
        }
    }
}