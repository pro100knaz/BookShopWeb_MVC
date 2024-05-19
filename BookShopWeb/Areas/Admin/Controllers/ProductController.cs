using BookShop.DataAccess.Repository;
using BookShop.DataAccess.Repository.IRepository;
using BookShop.Models;
using BookShop.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookShopWeb.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class ProductController : Controller
	{
		private readonly IUnitOfWork unitOfWork;
		private readonly IProductRepository productRepository;
		private readonly ICategoryRepository categoryRepository;

		public ProductController(IUnitOfWork unitOfWork)
		{
			this.unitOfWork = unitOfWork;
			productRepository = unitOfWork.Products;
			categoryRepository = unitOfWork.Category;

		}

		public IActionResult Index()
		{
			var objCategoryList = productRepository.GetAll().ToList();
			return View(objCategoryList);
		}

		public IActionResult Upsert(int? id)
		{
			IEnumerable<SelectListItem> CategoryList = categoryRepository.GetAll().Select(u => new SelectListItem
			{
				Text = u.Name,
				Value = u.Id.ToString()
			});
			//ViewBag.CategoryList = CategoryList;
			//ViewData["CategoryList"] = CategoryList;
			ProductVM productVM = new()
			{
				CategoryList = CategoryList,
				Product = new Product()
			};

			if (id == null || id == 0)
			{
				//create
				return View(productVM);

			}
			else
			{
				//update
				productVM.Product = productRepository.Get(u => u.Id == id);
				return View(productVM);
			}
		}

		[HttpPost]
		public IActionResult Upsert(ProductVM productVM, IFormFile? file)
		{
			if (ModelState.IsValid)
			{
				productRepository.Add(productVM.Product);
				unitOfWork.Save();
				TempData["success"] = "Product created successfully";
				return RedirectToAction("Index", "Product");
			}
			else
			{
				//Иначе может столкнуться с проблемой отсутствия категорий (или же аттрибут ValidateNever) 
				productVM.CategoryList = categoryRepository.GetAll().Select(u => new SelectListItem
				{
					Text = u.Name,
					Value = u.Id.ToString()
				});
				return View(productVM);
			}

		}

		public IActionResult Delete(int? id)
		{
			if (id == null || id == 0)
			{
				return NotFound();
			}
			var product = productRepository.Get(c => c.Id == id);
			return View(product);
		}

		[HttpPost, ActionName("Delete")]
		public IActionResult DeletePOST(int id)
		{
			var obj = productRepository.Get(c => c.Id == id);

			productRepository.Delete(obj);
			unitOfWork.Save();
			TempData["success"] = "Product deleted successfully";
			return RedirectToAction("Index", "Product");
		}
	}
}
