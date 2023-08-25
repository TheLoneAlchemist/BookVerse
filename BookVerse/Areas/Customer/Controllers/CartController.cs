using BookVerse.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using BookVerse.Models.ViewModels;


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

        [HttpPost]
        public async Task<IActionResult> AddToCart( int quantity, int productid)
        {

             _cartRepository.AddItemToCart( quantity, productid);


            return new JsonResult("Done");
        }

        #endregion
    }
}
