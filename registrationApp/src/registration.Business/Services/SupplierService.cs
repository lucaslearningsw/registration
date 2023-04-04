using BasicMVC.Models;
using registration.Business.Interfaces;
using registration.Business.Validations;

namespace registration.Business.Services
{
    public class SupplierService : BaseService, ISupplierService
    {
        private readonly ISupplierRepository _supplierRepository;
        private readonly IAddressRepository _addressRepository;

        public SupplierService(IAddressRepository addressRepository, 
                              ISupplierRepository supplierRepository,
                              INotificator notificator):base (notificator)
        {
            _addressRepository = addressRepository;
            _supplierRepository = supplierRepository;
        }

        public async Task Create(Supplier supplier)
        {
            if (!ExecuteValidation(new SupplierValidation(), supplier) 
                || !ExecuteValidation(new AddressValidation(), supplier.Address)) return;

            if(_supplierRepository.Find(s => s.Document == supplier.Document).Result.Any())
            {
                Notification("Já existe um fornecedor com este documento informado.");
                return;
            }

            await _supplierRepository.CreateAsync(supplier);
        }

        public async Task Update(Supplier supplier)
        {
            if (!ExecuteValidation(new SupplierValidation(), supplier)) return;

            if(_supplierRepository.Find(s => s.Document == supplier.Document && s.Id != supplier.Id).Result.Any()) 
            {
                Notification("Já existe um fornecedor com este documento informado.");
                return;
            }

            await _supplierRepository.UpdateAsync(supplier);
        }

        public async Task UpdateAddress(Address address)
        {
            if (!ExecuteValidation(new AddressValidation(), address)) return;

            await _addressRepository.UpdateAsync(address);
        }
        public async Task Delete(Guid id)
        {
            if(_supplierRepository.GetProductsSupplierAddress(id).Result.Products.Any()) 
            {
                Notification("O fornecedor possui produtos cadastrados!.");
                return;
            }

            await _supplierRepository.DeleteAsync(id);
        }

        public void Dispose()
        {
            _supplierRepository?.Dispose();
            _addressRepository?.Dispose();
        }
    }
}