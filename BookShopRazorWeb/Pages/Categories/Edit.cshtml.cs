using BookShopRazorWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BookShopRazorWeb.Pages.Categories
{
	[BindProperties]
	public class EditModel : PageModel
	{
		private readonly BookShopRazorWeb.Data.ApplicationDbContext _context;
		public Category Category { get; set; }

		public EditModel(BookShopRazorWeb.Data.ApplicationDbContext context)
		{
			_context = context;
		}

		public void OnGet(int? id)
		{
			if (id is not null && id != 0)
			{
				Category = _context.Categories.Find(id);
			}
		}

		public IActionResult OnPost()
		{
			if (ModelState.IsValid)
			{
				_context.Categories.Update(Category);
				_context.SaveChanges();
				TempData["success"] = "Category edited successfully";
				return RedirectToPage("Category");
			}

			return Page();
		}
	}
}
