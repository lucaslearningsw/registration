using BasicMVC.Models;
using registration.Business.Interfaces;
using registration.Business.Validations;

namespace registration.Business.Services
{
    public class SupplierService : BaseService, ISupplierService
    {
        public async Task Create(Supplier supplier)
        {
            if (!ExecuteValidation(new SupplierValidation(), supplier)) return;


            return;

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