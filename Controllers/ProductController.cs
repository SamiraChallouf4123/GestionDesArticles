using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using GestionDesArticles.Models;
using GestionDesArticles.Models.Repositories;
using GestionDesArticles.ViewModels;

namespace GestionDesArticles.Controllers
{
    [Authorize(Roles = "Admin,Manager")]
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly AppDbContext _context;
        public ProductController(IProductRepository productRepository,
                                 ICategoryRepository categoryRepository,
                                 IWebHostEnvironment webHostEnvironment, AppDbContext context)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _webHostEnvironment = webHostEnvironment;
            _context = context;
        }

        // ✅ GET: Index — liste paginée + filtrée par catégorie
        [AllowAnonymous]
        public IActionResult Index(int? categoryId, int page = 1)
        {
            const int pageSize = 6;
            var categories = _categoryRepository.GetAll();
            ViewData["Categories"] = categories;

            var productsQuery = _productRepository.GetAllProducts();

            if (categoryId.HasValue)
            {
                productsQuery = productsQuery.Where(p => p.CategoryId == categoryId);
            }

            var totalProducts = productsQuery.Count();
            var products = productsQuery.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            ViewBag.TotalPages = (int)Math.Ceiling((double)totalProducts / pageSize);
            ViewBag.CurrentPage = page;
            ViewBag.CategoryId = categoryId;

            return View(products);
        }

        // ✅ GET: Details
        [AllowAnonymous]
        public IActionResult Details(int id)
        {
            var product = _productRepository.GetById(id);
            if (product == null)
                return NotFound();

            ViewData["Categories"] = _categoryRepository.GetAll();
            return View(product);
        }

        // ✅ GET: Create
        public IActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(_categoryRepository.GetAll(), "CategoryId", "CategoryName");
            return View();
        }

        // ✅ POST: Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                string fileName = null;
                if (model.ImagePath != null)
                {
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                    fileName = Guid.NewGuid().ToString() + "_" + model.ImagePath.FileName;
                    string filePath = Path.Combine(uploadsFolder, fileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        model.ImagePath.CopyTo(fileStream);
                    }
                }

                var product = new Product
                {
                    Name = model.Name,
                    Price = model.Price,
                    QteStock = model.QteStock,
                    Type = model.Type,
                    CategoryId = model.CategoryId.Value,
                    Image = fileName
                };

                _productRepository.Add(product);
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(_context.Categories, "CategoryId", "CategoryName", model.CategoryId);
            return View(model);
        }
        // ✅ GET: Edit
        public IActionResult Edit(int id)
        {
            var product = _productRepository.GetById(id);
            if (product == null)
                return NotFound();

            var model = new EditViewModel
            {
                ProductId = product.ProductId,
                Name = product.Name,
                Price = (float)product.Price,
                QteStock = product.QteStock,
                CategoryId = product.CategoryId,
                Type = product.Type,            // ✅ AJOUT ICI
                ExistingImagePath = product.Image
            };

            ViewBag.CategoryId = new SelectList(_categoryRepository.GetAll(), "CategoryId", "CategoryName", product.CategoryId);
            return View(model);
        }

        // ✅ POST: Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.CategoryId = new SelectList(_categoryRepository.GetAll(), "CategoryId", "CategoryName", model.CategoryId);
                return View(model);
            }

            var product = _productRepository.GetById(model.ProductId);
            if (product == null)
                return NotFound();

            product.Name = model.Name;
            product.Price = model.Price;
            product.QteStock = model.QteStock;
            product.CategoryId = model.CategoryId.Value;
            product.Type = model.Type;     // ✅ AJOUT ICI

            if (model.ImagePath != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ImagePath.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.ImagePath.CopyTo(fileStream);
                }

                product.Image = uniqueFileName;
            }

            _productRepository.Update(product);
            return RedirectToAction(nameof(Index));
        }

        // ✅ GET: Delete
        public IActionResult Delete(int id)
        {
            var product = _productRepository.GetById(id);
            if (product == null)
                return NotFound();

            return View(product);
        }

        // ✅ POST: Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _productRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
