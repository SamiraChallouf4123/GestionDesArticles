using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GestionDesArticles.Models;
using GestionDesArticles.Models.Repositories;
using GestionDesArticles.ViewModels;

namespace GestionDesArticles.Controllers
{
    [Authorize(Roles = "Admin,Manager")]
    public class CategoryController : Controller
    {
        readonly ICategoryRepository CategRepository;

        public CategoryController(ICategoryRepository categRepository)
        {
            CategRepository = categRepository;
        }

        // GET: CategoryController
        [AllowAnonymous]
        public ActionResult Index()
        {
            var categories = CategRepository.GetAll();
            ViewData["Categories"] = categories;

            return View(categories);
        }

        // GET: CategoryController/Details/5
        public ActionResult Details(int id)
        {
            var categories = CategRepository.GetAll();
            ViewData["Categories"] = categories;

            var category = CategRepository.GetById(id);
            if (category == null) return NotFound();

            return View(category);
        }

        // GET: CategoryController/Create
        public ActionResult Create()
        {
            var categories = CategRepository.GetAll();
            ViewData["Categories"] = categories;

            return View();
        }

        // POST: CategoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                var category = new Category
                {
                    CategoryName = collection["CategoryName"]
                };

                CategRepository.Add(category);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                var categories = CategRepository.GetAll();
                ViewData["Categories"] = categories;
                return View();
            }
        }

        // GET: CategoryController/Edit/5
        public ActionResult Edit(int id)
        {
            var categories = CategRepository.GetAll();
            ViewData["Categories"] = categories;

            var category = CategRepository.GetById(id);
            if (category == null) return NotFound();

            return View(category);
        }

        // POST: CategoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                var category = CategRepository.GetById(id);
                if (category == null) return NotFound();

                category.CategoryName = collection["CategoryName"];
                CategRepository.Update(category);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                var categories = CategRepository.GetAll();
                ViewData["Categories"] = categories;
                return View();
            }
        }

        // GET: CategoryController/Delete/5
        public ActionResult Delete(int id)
        {
            var categories = CategRepository.GetAll();
            ViewData["Categories"] = categories;

            var category = CategRepository.GetById(id);
            if (category == null) return NotFound();

            return View(category);
        }

        // POST: CategoryController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                CategRepository.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                var categories = CategRepository.GetAll();
                ViewData["Categories"] = categories;
                return View();
            }
        }


        [AllowAnonymous]
        public IActionResult GetCategoriesMenu()
        {
            var categories = CategRepository.GetAll();


            return PartialView("_CategoriesMenu", categories);
        }

    }
}
