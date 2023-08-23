using BookVerse.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace BookVerse.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class CartController : Controller
    {

        private readonly ICartRepository _cartRepository;

        public CartController(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }


        public IActionResult Index()
        {
            return View();
        }
    }
}
