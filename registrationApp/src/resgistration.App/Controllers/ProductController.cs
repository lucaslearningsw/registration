using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BasicMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using registration.Business.Interfaces;
using resgistration.App.Data;
using resgistration.App.Extensions;
using resgistration.App.ViewModels;

namespace resgistration.App.Controllers
{
    [Authorize]
    public class ProductController : BaseController
    {

        private readonly IProductService _productService;
        private readonly IProductRepository _productRepository;
        private readonly ISupplierRepository _supplierRepository;
        private readonly IMapper _mapper;

        public ProductController(IProductRepository productRepository,
                                 ISupplierRepository supplierRepository,
                                 IMapper mapper,
                                 IProductService productService,
                                 INotificator notificator) : base(notificator)
        {
            _productRepository = productRepository;
            _supplierRepository = supplierRepository;
            _mapper = mapper;
            _productService = productService;
        }


        [AllowAnonymous]
        [Route("lista-produto")]
        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<ProductViewModel>>(await _productRepository.GetProductsSuppilers()));
        }


        [AllowAnonymous]
        [Route("detalhes-produtos/{id:guid}")]
        public async Task<IActionResult> Details(Guid id)
        {
            var productViewModel = await GetProduct(id);
            if (productViewModel == null)
            {
                return NotFound();
            }

            return View(productViewModel);
        }

        [ClaimsAuthorize("Product","Add")]
        [Route("novo-produto")]
        public async Task <IActionResult> Create()
        {
            var productViewModel = await FillSuppliers(new ProductViewModel());
            return View(productViewModel);  
        }

        [ClaimsAuthorize("Product", "Add")]
        [Route("novo-produto")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductViewModel productViewModel)
        {
            productViewModel = await FillSuppliers(productViewModel);
            if (!ModelState.IsValid) return View(productViewModel);

            var imgId = Guid.NewGuid() + "_";
            if(!await UploadFile(productViewModel.ImagemUpload, imgId))
            {
                return View(productViewModel);
            }

            productViewModel.Image = imgId + productViewModel.ImagemUpload.FileName;

            await _productService.Create(_mapper.Map<Product>(productViewModel));

            if (!OperationValid()) return View(productViewModel);

            return RedirectToAction("Index");
        }

        [ClaimsAuthorize("Product", "Edit")]
        [Route("editar-produto/{id:guid}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var productViewModel = await GetProduct(id);
            
            if(productViewModel == null)
            {
                return NotFound();
            }

            return View(productViewModel);
        }


        [ClaimsAuthorize("Product", "Edit")]
        [Route("editar-produto/{id:guid}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id,  ProductViewModel productViewModel)
        {
            if (id != productViewModel.Id)  return NotFound();
 

            var productUpdated = await GetProduct(id);
            productViewModel.Supplier = productUpdated.Supplier;
            productViewModel.Image = productUpdated.Image;
            if (!ModelState.IsValid) return View(productViewModel);

            if(productViewModel.ImagemUpload != null)
            {
                var imgId = Guid.NewGuid() + "_";
                if (!await UploadFile(productViewModel.ImagemUpload, imgId))
                {
                    return View(productViewModel);
                }

                productUpdated.Image = imgId + productViewModel.ImagemUpload.FileName;
            }

            productUpdated.Name = productViewModel.Name;
            productUpdated.Description = productViewModel.Description;
            productUpdated.Price = productViewModel.Price;
            productUpdated.Active = productViewModel.Active;

            await _productService.Update(_mapper.Map<Product>(productUpdated));

            if (!OperationValid()) return View(productViewModel);

            return RedirectToAction("index");
        }


        [ClaimsAuthorize("Product", "Delete")]
        [Route("excluir-produto/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var product = await GetProduct(id);

            if (product == null) return NotFound();

            return View(product);

        }

        [ClaimsAuthorize("Product", "Delete")]
        [Route("excluir-produto/{id:guid}")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var product = await GetProduct(id);

            if (product == null) return NotFound();

            await _productService.Delete(id);

           if(!OperationValid()) return View(product);

            TempData["Sucesso"] = "Produto excluido com sucesso";

            return RedirectToAction("index");
        }


        private async Task<bool> UploadFile(IFormFile imagemUpload, string imgId)
        {
            if (imagemUpload.Length <= 0) return false;

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/imagens", imgId + imagemUpload.FileName);

            if (System.IO.File.Exists(path))
            {
                ModelState.AddModelError(string.Empty, "Já existe um arquivo com este nome!");
                return false;
            }

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await imagemUpload.CopyToAsync(stream);
            }

            return true;
        }

        private async Task<ProductViewModel> GetProduct(Guid id)
        {
            var product = _mapper.Map<ProductViewModel>(await _productRepository.GetProductSupplier(id));
            product.Suppliers = _mapper.Map<IEnumerable<SupplierViewModel>>(await _supplierRepository.GetAllAsync());

            return product;
        }

        private async Task<ProductViewModel> FillSuppliers(ProductViewModel product)
        {
             product.Suppliers = _mapper.Map<IEnumerable<SupplierViewModel>>(await _supplierRepository.GetAllAsync());

            return product;
        }



    }
}
