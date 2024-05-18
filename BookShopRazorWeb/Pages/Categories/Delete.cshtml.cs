using BookShopRazorWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BookShopRazorWeb.Pages.Categories
{
	[BindProperties]
	public class DeleteModel : PageModel
	{
		private readonly BookShopRazorWeb.Data.ApplicationDbContext _context;
		public Category Category { get; set; }

		public DeleteModel(BookShopRazorWeb.Data.ApplicationDbContext context)
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

		public async Task<IActionResult> OnPostAsync()
		{
			var obj = _context.Categories.Find(Category.Id);
			if (obj == null)
			{
				return NotFound();
			}

			_context.Categories.Remove(obj);
			await _context.SaveChangesAsync();
			TempData["success"] = "Category deleted successfully";
			return RedirectToPage("Category");
		}
	}
}
