using BasicMVC.Models;
using registration.Business.Interfaces;

namespace registration.Business.Services
{
    public class ProductService : BaseService, IProductService
    {
        public Task Create(Product product)
        {
            throw new NotImplementedException();
        }

        public Task Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task Update(Product product)
        {
            throw new NotImplementedException();
        }
    }

}
