
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using BookShop.Models;
using BookShop.DataAccess.Repository.IRepository;

namespace BookShopWeb.Areas.Customer.Controllers
{
	[Area("Customer")]
	public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
		private readonly IUnitOfWork unitOfWork;

		public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
			this.unitOfWork = unitOfWork;
		}

        public IActionResult Index()
        {
            IEnumerable<Product> products = unitOfWork.Products.GetAll(includeProperties:"Category");
            return View(products);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
