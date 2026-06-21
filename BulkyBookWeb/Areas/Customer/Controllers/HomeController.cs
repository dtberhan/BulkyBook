using System.Diagnostics;
using BulkyBook.DataAccess.Repo;
using BulkyBook.DataAccess.Repo.IRepo;
using BulkyBook.Models;
using BulkyBook.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

            public async Task<IActionResult> Index()
        {
            IEnumerable<Product> productList = await _unitOfWork.Product
                                   .GetAll(includeProperties: "Category");

            return View(productList);
        }

        public async Task<IActionResult> Details(int? productId)
        {
            Product product = await _unitOfWork.Product
                                    .Get(u => u.Id == productId, includeProperties: "Category");

            return View(product);
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
