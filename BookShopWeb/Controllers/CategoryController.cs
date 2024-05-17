using BookShopWeb.Data;
using BookShopWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookShopWeb.Controllers
{
	public class CategoryController : Controller
	{



		private readonly ApplicationDbContext _Context;

		public CategoryController(ApplicationDbContext context)
		{
			_Context = context;
		}


		public IActionResult Index()
		{
			var objCategoryList = _Context.Categories.ToList();
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
				_Context.Categories.Add(obj);
				_Context.SaveChanges();
				TempData["success"] = "Category created successfully";
				return RedirectToAction("Index", "Category");
			}
			return View();
		}


		public IActionResult Edit(int id)
		{
			if (id == null || id == 0)
			{
				return NotFound();
			}

			var category = _Context.Categories.Find(id) ?? throw new ArgumentOutOfRangeException(nameof(id), "Категория с таким ид не существует");
			//var category1 = _Context.Categories.FirstOrDefault(u => u.Id == id) ?? throw new ArgumentOutOfRangeException(nameof(id), "Категория с таким ид не существует");
			//var category2 = _Context.Categories.Where(u => u.Id == id).FirstOrDefault() ?? throw new ArgumentOutOfRangeException(nameof(id), "Категория с таким ид не существует");
			return View(category);
		}

		[HttpPost]
		public IActionResult Edit(Category obj)
		{
			if (ModelState.IsValid)
			{
				_Context.Categories.Update(obj);
				_Context.SaveChanges();
				TempData["success"] = "Category edited successfully";
				return RedirectToAction("Index", "Category");
			}
			return View();
		}


		public IActionResult Delete(int id)
		{
			if (id == null || id == 0)
			{
				return NotFound();
			}

			var category = _Context.Categories.Find(id) ?? throw new ArgumentOutOfRangeException(nameof(id), "Категория с таким ид не существует");
			//var category1 = _Context.Categories.FirstOrDefault(u => u.Id == id) ?? throw new ArgumentOutOfRangeException(nameof(id), "Категория с таким ид не существует");
			//var category2 = _Context.Categories.Where(u => u.Id == id).FirstOrDefault() ?? throw new ArgumentOutOfRangeException(nameof(id), "Категория с таким ид не существует");
			return View(category);
		}

		[HttpPost, ActionName("Delete")]
		public IActionResult DeletePOST(int id)
		{
			var obj = _Context.Categories.Find(id) ?? throw new ArgumentOutOfRangeException(nameof(id), "Категория с таким ид не существует");

			_Context.Categories.Remove(obj);
			_Context.SaveChanges();
			TempData["success"] = "Category deleted successfully";
			return RedirectToAction("Index", "Category");
		}
	}
}
