using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using registration.Business.Interfaces;
using resgistration.App.Data;
using resgistration.App.ViewModels;

namespace resgistration.App.Controllers
{
    public class ProductController : Controller
    {

        private readonly IProductRepository _productRepository;
        private readonly ISupplierRepository _supplierRepository;
        private readonly IMapper _mapper;

        public ProductController(IProductRepository productRepository, 
                                 ISupplierRepository supplierRepository,
                                 IMapper mapper)
        {
            _productRepository = productRepository;
            _supplierRepository = supplierRepository;
            _mapper = mapper;
        }


        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<ProductViewModel>>(await _productRepository.GetProductsSuppilers()));
        }

        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.ProductViewModel == null)
            {
                return NotFound();
            }

            var productViewModel = await _context.ProductViewModel
                .Include(p => p.Supplier)
                .FirstOrDefaultAsync(m => m.id == id);
            if (productViewModel == null)
            {
                return NotFound();
            }

            return View(productViewModel);
        }

        public IActionResult Create()
        {
            ViewData["SupplierId"] = new SelectList(_context.Set<SupplierViewModel>(), "id", "Document");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,SupplierId,Name,Description,Image,Price,Active")] ProductViewModel productViewModel)
        {
            if (ModelState.IsValid)
            {
                productViewModel.id = Guid.NewGuid();
                _context.Add(productViewModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SupplierId"] = new SelectList(_context.Set<SupplierViewModel>(), "id", "Document", productViewModel.SupplierId);
            return View(productViewModel);
        }

        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.ProductViewModel == null)
            {
                return NotFound();
            }

            var productViewModel = await _context.ProductViewModel.FindAsync(id);
            if (productViewModel == null)
            {
                return NotFound();
            }
            ViewData["SupplierId"] = new SelectList(_context.Set<SupplierViewModel>(), "id", "Document", productViewModel.SupplierId);
            return View(productViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("id,SupplierId,Name,Description,Image,Price,Active")] ProductViewModel productViewModel)
        {
            if (id != productViewModel.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productViewModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductViewModelExists(productViewModel.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["SupplierId"] = new SelectList(_context.Set<SupplierViewModel>(), "id", "Document", productViewModel.SupplierId);
            return View(productViewModel);
        }

        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.ProductViewModel == null)
            {
                return NotFound();
            }

            var productViewModel = await _context.ProductViewModel
                .Include(p => p.Supplier)
                .FirstOrDefaultAsync(m => m.id == id);
            if (productViewModel == null)
            {
                return NotFound();
            }

            return View(productViewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.ProductViewModel == null)
            {
                return Problem("Entity set 'ApplicationDbContext.ProductViewModel'  is null.");
            }
            var productViewModel = await _context.ProductViewModel.FindAsync(id);
            if (productViewModel != null)
            {
                _context.ProductViewModel.Remove(productViewModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private async Task<ProductViewModel> GetProduto(Guid id)
        {
            var produto = _mapper.Map<ProductViewModel>(_productRepository.GetProductSupplier(id));
            produto.Suppliers = _mapper.Map<IEnumerable<SupplierViewModel>>(await _supplierRepository.GetAllAsync());

            return produto;
        }

        
    }
}
