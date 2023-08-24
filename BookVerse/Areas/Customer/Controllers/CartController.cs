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




        #region APIs Region

        public async Task<IActionResult> AddToCart(int productid, int quantity)
        {
                await  _cartRepository.AddItemsToCart(productid, quantity);
            var cartitems = _cartRepository.GetAll().ToList();
            return new JsonResult(cartitems);
        }

        #endregion
    }
}
