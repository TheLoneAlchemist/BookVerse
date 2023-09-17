using BookVerse.DataAccess.Helper;
using BookVerse.DataAccess.Repository;
using BookVerse.DataAccess.Repository.IRepository;
using BookVerse.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookVerse.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize(Roles = "User")]
    public class WishListController : Controller
    {
        private readonly IHelper helper;

        private readonly IProductRepository ProductRepository;

        private readonly IWishListRepository WishListRepository;

        public WishListController(IHelper helper, IProductRepository productRepository, IWishListRepository wishListRepository)
        {
            this.helper = helper;
            ProductRepository = productRepository;
            WishListRepository = wishListRepository;
        }

        public IActionResult Index()
        {
            var userId = helper.GetUserId();
            List<WishList> fw = WishListRepository.GetAll("Product").Where(u => u.UserId == userId).ToList();
            return View(fw);
        }

        [HttpPost]
        public async Task<IActionResult> AddtoWish(int productId)
        {
            try
            {
                var userId = helper.GetUserId();
                if (userId is null)
                {
                    return Redirect("/Identity/Account/Login");
                }
                var product = ProductRepository.Get(x => x.Id == productId);
                
                if (product != null)
                {
                    var checkexist = WishListRepository.Get(u => u.UserId == userId && u.ProductId == product.Id);
                    if (checkexist is not null)
                    {
                        TempData["error"] = "Item Already in WishList";
                        return new JsonResult(TempData);
                    }
                    
                    WishList item = new WishList() { ProductId = product.Id, Status = true,UserId=userId };
                    
                                        
                    WishListRepository.Add(item);
                    WishListRepository.Save();
                    TempData["success"] = "Item Added To WishList";
                }
                else
                {
                    TempData["error"] = "Something went wrong!";
                }

            }
            catch (Exception e)
            {

                TempData["error"] = e.Message;
            }

            return new JsonResult(TempData);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? wid)
        {

            var userId = helper.GetUserId();
            if (userId is null)
            {
                return Redirect("/Identity/Account/Login");
            }

            if (wid is null && wid==0)
            {
                return NotFound();
            }
            WishList checkitem = WishListRepository.Get(x => x.UserId == userId && x.Id==wid);

            if (checkitem is not null)
            {
                WishListRepository.Remove(checkitem);
                WishListRepository.Save();
                TempData["success"] = "Item Removed from WishList!";
            }
            else
            {
                TempData["error"] = "Item Not found!";
            }
            return RedirectToAction("Index");

        }

    }
}
