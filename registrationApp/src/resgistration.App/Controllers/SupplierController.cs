using AutoMapper;
using BasicMVC.Models;
using Microsoft.AspNetCore.Mvc;
using registration.Business.Interfaces;
using resgistration.App.ViewModels;

namespace resgistration.App.Controllers
{
    public class SupplierController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ISupplierRepository _supplierRepo;

        public SupplierController(ISupplierRepository supplierRepo,
                                  IMapper mapper)
        {
            _supplierRepo = supplierRepo;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<SupplierViewModel>>(await _supplierRepo.GetAllAsync()));
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var supplierViewModel = await GetSupplierAddress(id);
 
            if (supplierViewModel == null)
            {
                return NotFound();
            }

            return View(supplierViewModel);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SupplierViewModel supplierViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(supplierViewModel);
            }
            var supplier = _mapper.Map<Supplier>(supplierViewModel);

            await _supplierRepo.CreateAsync(supplier);
            return RedirectToAction("Index");

        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var supplierViewModel = await GetProductsSupplierAddress(id);

            if (supplierViewModel == null)
            {
                return NotFound();
            }

            return View(supplierViewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id,SupplierViewModel supplierViewModel)
        {

            if (id != supplierViewModel.id) return NotFound();

            if (!ModelState.IsValid) return View(supplierViewModel);

            var supplier = _mapper.Map<Supplier>(supplierViewModel);
            await _supplierRepo.UpdateAsync(supplier);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(Guid id)
        {

            var supplierViewModel = await GetProductsSupplierAddress(id);

            if (supplierViewModel == null)
            {
                return NotFound();
            }

            return View(supplierViewModel);
        }

  
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
           var supplierViewModel = await GetProductsSupplierAddress(id);
            if (supplierViewModel == null) return NotFound();

            await _supplierRepo.DeleteAsync(id);

            return RedirectToAction("Index");
        }


        private async Task<SupplierViewModel> GetSupplierAddress(Guid id)
        {
            return _mapper.Map<SupplierViewModel>(await _supplierRepo.GetSupplierAddress(id));
        }

        private async Task<SupplierViewModel> GetProductsSupplierAddress(Guid id)
        {
            return _mapper.Map<SupplierViewModel>(await _supplierRepo.GetProductsSupplierAddress(id));
        }
    }
}
