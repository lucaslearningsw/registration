using BasicMVC.Models;
using registration.Business.Interfaces;
using registration.Business.Validations;

namespace registration.Business.Services
{
    public class ProductService : BaseService, IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository,
                             INotificator notificator) : base(notificator)
        {
            _productRepository = productRepository;
        }

        public async Task Create(Product product)
        {
            if (!ExecuteValidation(new ProductValidation(), product)) return;

            await _productRepository.CreateAsync(product);
        }

        public async Task Update(Product product)
        {
            if (!ExecuteValidation(new ProductValidation(), product)) return;

            await _productRepository.UpdateAsync(product);

        }

        public async Task Delete(Guid id)
        {
            await _productRepository.DeleteAsync(id);
        }

        public void Dispose()
        {
          _productRepository?.Dispose();
              
        }
    }

}
