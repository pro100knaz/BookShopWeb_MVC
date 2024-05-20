using BookShop.DataAccess.Repository.IRepository;
using BookShop.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookShopWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _UnitOfWork;
        private readonly ICategoryRepository _CategoriesRepo;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _UnitOfWork = unitOfWork;
            _CategoriesRepo = unitOfWork.Category;
        }


        public IActionResult Index()
        {
            var objCategoryList = _CategoriesRepo.GetAll().ToList();
            return View(objCategoryList);
        }

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name");
            }
            if (obj.Name != null && obj.Name.ToLower() == "test")
            {
                ModelState.AddModelError("", "Test is an invalid value");
            }
            if (ModelState.IsValid)
            {
                _CategoriesRepo.Add(obj);
                _UnitOfWork.Save();
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index", "Category");
            }
            return View();
        }


        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var category = _CategoriesRepo.Get(c => c.Id == id);
            //var category1 = _Context.Categories.FirstOrDefault(u => u.Id == id) ?? throw new ArgumentOutOfRangeException(nameof(id), "Категория с таким ид не существует");
            //var category2 = _Context.Categories.Where(u => u.Id == id).FirstOrDefault() ?? throw new ArgumentOutOfRangeException(nameof(id), "Категория с таким ид не существует");
            return View(category);
        }

        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            if (ModelState.IsValid)
            {
                _CategoriesRepo.Update(obj);
                _UnitOfWork.Save();
                TempData["success"] = "Category edited successfully";
                return RedirectToAction("Index", "Category");
            }
            return View();
        }


        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var category = _CategoriesRepo.Get(c => c.Id == id);
            //var category1 = _Context.Categories.FirstOrDefault(u => u.Id == id) ?? throw new ArgumentOutOfRangeException(nameof(id), "Категория с таким ид не существует");
            //var category2 = _Context.Categories.Where(u => u.Id == id).FirstOrDefault() ?? throw new ArgumentOutOfRangeException(nameof(id), "Категория с таким ид не существует");
            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int id)
        {
            var obj = _CategoriesRepo.Get(c => c.Id == id);

            _CategoriesRepo.Delete(obj);
            _UnitOfWork.Save();
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index", "Category");
        }
    }
}
