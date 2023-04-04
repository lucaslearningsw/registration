using BasicMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace registration.Business.Interfaces
{
    public interface ISupplierService : IDisposable
    {
        Task Create(Supplier supplier);
        Task Update(Supplier supplier);
        Task Delete(Guid id);

        Task UpdateAddress(Address address);
    }
}
