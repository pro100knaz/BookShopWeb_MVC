using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BookShopRazorWeb.Data;
using BookShopRazorWeb.Models;

namespace BookShopRazorWeb.Pages.Categories
{
    [BindProperties]
    public class CreateModel : PageModel
    {
        private readonly BookShopRazorWeb.Data.ApplicationDbContext _context;
        public Category Category { get; set; }

        public CreateModel(BookShopRazorWeb.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync()
        {
            _context.Categories.Add(Category);
            await _context.SaveChangesAsync();
            TempData["success"] = "Category created successfully";
            return RedirectToPage("Category");
        }
    }
}
