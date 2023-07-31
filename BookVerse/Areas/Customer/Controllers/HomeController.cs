using BookVerse.DataAccess.Repository.IRepository;
using BookVerse.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BookVerse.Areas.Admin.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductRepository _productcontext;
        public HomeController(ILogger<HomeController> logger, IProductRepository productRepository)
        {
            _logger = logger;
            _productcontext = productRepository;
        }



        public IActionResult Index()
        {
            var Products = _productcontext.GetAll(includeProperty: "Category");
            return View(Products);
        }

        public IActionResult ProductDetails(int id)
        {

            var Product = _productcontext.Get(g => g.Id==id,includeProperty:"Category");
            return View(Product);
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