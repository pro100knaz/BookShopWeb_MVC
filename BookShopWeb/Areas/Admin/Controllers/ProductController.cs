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

		private readonly IWebHostEnvironment _webHostEnvironment;
		public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
		{
			this.unitOfWork = unitOfWork;
			productRepository = unitOfWork.Products;
			categoryRepository = unitOfWork.Category;
			_webHostEnvironment = webHostEnvironment;
		}

		public IActionResult Index()
		{
			var objCategoryList = productRepository.GetAll(includeProperties: "Category").ToList();

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
				string wwwRootPath = _webHostEnvironment.WebRootPath;

				if (file is { })
				{
					string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
					string productPath = Path.Combine(wwwRootPath, @"images\product");

					if (!string.IsNullOrEmpty(productVM.Product.ImageUrl))
					{
						//delete the old image
						var oldImagePath =
							Path.Combine(wwwRootPath, productVM.Product.ImageUrl.TrimStart('\\'));

						if (System.IO.File.Exists(oldImagePath))
						{
							System.IO.File.Delete(oldImagePath);
						}
					}

					using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
					{
						file.CopyTo(fileStream);
					}
					productVM.Product.ImageUrl = @"\images\product\" + fileName;
				}
				if (productVM.Product.Id == 0)
				{
					productRepository.Add(productVM.Product);
				}
				else
				{
					productRepository.Update(productVM.Product);
				}

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

		//public IActionResult Delete(int? id)
		//{
		//	if (id == null || id == 0)
		//	{
		//		return NotFound();
		//	}
		//	var product = productRepository.Get(c => c.Id == id);
		//	return View(product);
		//}

		//[HttpPost, ActionName("Delete")]
		//public IActionResult DeletePOST(int id)
		//{
		//	var obj = productRepository.Get(c => c.Id == id);

		//	productRepository.Delete(obj);
		//	unitOfWork.Save();
		//	TempData["success"] = "Product deleted successfully";
		//	return RedirectToAction("Index", "Product");
		//}


		#region API Calls

		[HttpGet]
		public IActionResult GetAll()
		{
			var objCategoryList = productRepository.GetAll(includeProperties: "Category").ToList();

			return Json(new { data = objCategoryList });

		}

		
		[HttpDelete]
		public IActionResult Delete(int? id)
		{
			var productToDelete = productRepository.Get(u => u.Id == id);
			if (productToDelete == null)
			{
				return Json(new { succes = false, message = "Error while deleting" });
			}
			var oldImagePath =
						Path.Combine(_webHostEnvironment.WebRootPath, productToDelete.ImageUrl.TrimStart('\\'));


			if (System.IO.File.Exists(oldImagePath))
			{
				System.IO.File.Delete(oldImagePath);
			}
			unitOfWork.Products.Delete(productToDelete);
			unitOfWork.Save();

			return Json(new { succes = true, message = "Delete Successful" });

		}

		#endregion
	}
}
