using BookShopRazorWeb.Data;
using BookShopRazorWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookShopRazorWeb.Pages.Categoey
{
    public class CategoryModel : PageModel
    {
	    private readonly ApplicationDbContext _DbContext;
        public List<Category> Categories { get; set; }
	    public CategoryModel(ApplicationDbContext dbContext)
	    {
		    _DbContext = dbContext;
	    }

        public void OnGet()
        {
            Categories = _DbContext.Categories.ToList();
        }


    }
}
