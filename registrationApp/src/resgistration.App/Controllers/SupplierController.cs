using AutoMapper;
using BasicMVC.Models;
using Microsoft.AspNetCore.Mvc;
using registration.Business.Interfaces;
using resgistration.App.ViewModels;

namespace resgistration.App.Controllers
{
    public class SupplierController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly ISupplierRepository _supplierRepo;
        private readonly ISupplierService _supplierService;

        public SupplierController(ISupplierRepository supplierRepo,
                                  IMapper mapper,
                                  ISupplierService supplierService,
                                  INotificator notificator) : base(notificator)
        {
            _supplierRepo = supplierRepo;
            _mapper = mapper;
            _supplierService = supplierService;
        }

        [Route("lista-de-fornecedores")]
        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<SupplierViewModel>>(await _supplierRepo.GetAllAsync()));
        }

        [Route("dados-do-fornecedor/{id:guid}")]
        public async Task<IActionResult> Details(Guid id)
        {
            var supplierViewModel = await GetSupplierAddress(id);
 
            if (supplierViewModel == null)
            {
                return NotFound();
            }

            return View(supplierViewModel);
        }

        [Route("novo-fornecedor")]
        public IActionResult Create()
        {
            return View();
        }

        [Route("novo-fornecedor")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SupplierViewModel supplierViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(supplierViewModel);
            }
            var supplier = _mapper.Map<Supplier>(supplierViewModel);

            await _supplierService.Create(supplier);

            if (!OperationValid()) return View(supplierViewModel);

            return RedirectToAction("Index");

        }

        [Route("editar-fornecedor/{id:guid}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var supplierViewModel = await GetProductsSupplierAddress(id);

            if (supplierViewModel == null)
            {
                return NotFound();
            }

            return View(supplierViewModel);
        }

        public async Task<IActionResult> GetAddress(Guid id)
        {
            var supplier = await GetSupplierAddress(id);

            if (supplier == null)
            {
                return NotFound();
            }

            return PartialView("_AddressDetails", supplier);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id,SupplierViewModel supplierViewModel)
        {

            if (id != supplierViewModel.Id) return NotFound();

            if (!ModelState.IsValid) return View(supplierViewModel);

            var supplier = _mapper.Map<Supplier>(supplierViewModel);
            await _supplierService.Update(supplier);

            if (!OperationValid()) return View(await GetProductsSupplierAddress(id));

            return RedirectToAction("Index");
        }

        [Route("excluir-fornecedor/{id:guid}")]

        public async Task<IActionResult> Delete(Guid id)
        {

            var supplierViewModel = await GetProductsSupplierAddress(id);

            if (supplierViewModel == null)
            {
                return NotFound();
            }

            return View(supplierViewModel);
        }

    
        [Route("excluir-fornecedor/{id:guid}")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
           var supplierViewModel = await GetProductsSupplierAddress(id);
            if (supplierViewModel == null) return NotFound();

            await _supplierService.Delete(id);

            if (!OperationValid()) return View(supplierViewModel);

            TempData["Sucesso"] = "Fornecedor excluido com sucesso";

            return RedirectToAction("Index");
        }



        [Route("atualizar-endereco-fornecedor/{id:guid}")]

        public async Task<IActionResult> UpdateAddress(Guid id)
        {
            var supplier = await GetProductsSupplierAddress(id);

            if (supplier == null) { return NotFound(); }

            return PartialView("_UpdateAddress", new SupplierViewModel { Address = supplier.Address });
        }

        [Route("atualizar-endereco-fornecedor/{id:guid}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateAddress(SupplierViewModel supplierViewModel)
        {
            ModelState.Remove("Name");
            ModelState.Remove("Document");

            if (!ModelState.IsValid) return PartialView("_UpdateAddress", supplierViewModel);


            await _supplierService.UpdateAddress(_mapper.Map<Address>(supplierViewModel.Address));

            if (!OperationValid()) return PartialView("_UpdateAddress", supplierViewModel);

            var url = Url.Action("GetAddress", "Supplier", new { id = supplierViewModel.Address.SupplierId });

            return Json(new { success = true, url });
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
