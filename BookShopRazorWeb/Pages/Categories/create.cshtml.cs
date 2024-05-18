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
    public class CreateModel : PageModel
    {
        private readonly BookShopRazorWeb.Data.ApplicationDbContext _context;
        [BindProperty]
        public Category Category { get; set; } = default!;

        public CreateModel(BookShopRazorWeb.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Categories.Add(Category);
            await _context.SaveChangesAsync();

            return RedirectToPage("Category");
        }
    }
}
