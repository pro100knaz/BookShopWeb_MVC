using BookShopWeb.Data;
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
	}
}
