using BookVerse.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using BookVerse.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using BookVerse.DataAccess.Data;
using BookVerse.DataAccess.Helper;
using BookVerse.Models;
using Microsoft.EntityFrameworkCore;

namespace BookVerse.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize(Roles = "User")]
    public class CartController : Controller
    {


        private readonly ApplicationDbContext _context;
        
        private readonly ICartRepository _cartcontext;
        private readonly IHelper _helper;
        public CartController(
            ApplicationDbContext context,
            ICartRepository cartcontext,
            IHelper helper
            )
        {
            _context = context;
            
            _cartcontext = cartcontext;
            _helper = helper;
        }

        public async Task<IActionResult> Index()
        {
            var userId = _helper.GetUserId();

            if (string.IsNullOrEmpty(userId))
            {
                return Redirect("/Identity/Account/Login");
            }

            var cart = await _helper.GetCart(userId);
            if (cart is null)
            {
                return NotFound();
            }
            CartVM cartVM = new CartVM();
            cartVM.Cart = cart;
            cartVM.BasketItems = _context.BasketItems.Where(x => x.Cart.Id == cart.Id).Include("Product").ToList();

            return View(cartVM);
        }



        



        //Add Item to Cart
        [HttpPost]
        public async Task<IActionResult> AddToCart(int productid, int quantity, bool directbuy=false)
        {

            var userid = _helper.GetUserId();
            //if user is not login
            if (userid is null)
            {
                //return new JsonResult("User is not found");
                return Redirect("/Identity/Account/Login");

            }

            var cart = await _helper.GetCart(userid);

            var product = _context.Products.Where(p => p.Id == productid).FirstOrDefault();
            //if cart is not created for that user

            if (cart is null)
            {
                cart = new Cart();
                cart.ItemCount = 1;
                cart.UserId = userid;
                var item = new BasketItem() { Cart = cart, Product = product, ProductId = productid, Quantity = quantity, NetPrice = product.Price * quantity };

                cart.BasketItems = new List<BasketItem>
                {
                    item
                };
                _context.Carts.Add(cart);
                _context.SaveChanges();
                TempData["success"] = $"{item.Product.Title} Added To Cart ";

            }
            else
            {
                // if cart is exist for the user
                Cart cart2 = new Cart();
                cart2.BasketItems = new List<BasketItem>();

                var exititem = _context.BasketItems.Where(p => p.ProductId == product.Id).FirstOrDefault();
                BasketItem newitem;
                try
                {
                    if (exititem != null)
                    {

                        newitem = new BasketItem() { Cart = cart, ProductId = productid, Product = product, Id = exititem.Id, Quantity = exititem.Quantity + quantity, NetPrice = exititem.NetPrice + product.Price * quantity };
                        _context.Entry(exititem).State = EntityState.Detached;
                        _context.BasketItems.Update(newitem);
                        cart2.GrandTotalPrice += newitem.Product.Price * quantity;

                    }
                    else
                    {
                        newitem = new BasketItem() { Cart = cart, Product = product, ProductId = productid, Quantity = quantity, NetPrice = product.Price * quantity };
                        _context.BasketItems.Add(newitem);
                        cart2.BasketItems.Add(newitem);
                        cart2.GrandTotalPrice += newitem.NetPrice;
                        cart2.ItemCount = 1;
                    }

                }

                catch (Exception e)
                {

                    TempData["error"] = e.Message;
                    return new JsonResult(TempData);
                }
                cart.BasketItems.AddRange(cart2.BasketItems);
                cart.ItemCount += cart2.ItemCount;
                cart.GrandTotalPrice += cart2.GrandTotalPrice;
                _context.Carts.Update(cart);
                _context.SaveChanges();

                if (directbuy == false)
                {
                TempData["success"] = $"Item Added To Cart ";

                }
                if (directbuy == true)
                {
                    
                    return Json(new { url = "/Customer/Cart" });
                }
            }

            return new JsonResult(TempData);

        }

        public async Task<IActionResult> Remove(int? itemid)
        {

            var userid = _helper.GetUserId();

            if (userid is null)
            {
                return new JsonResult("User is not found");
            }
            var cart = await _helper.GetCart(userid);
            if (cart is null)
            {
                return NotFound();
            }
            if (itemid != null && itemid != 0)
            {
                var item = await _context.BasketItems.Where(b => b.Id == itemid).FirstOrDefaultAsync();
                cart = await _context.Carts.Where(x => x.UserId == userid).FirstOrDefaultAsync();

                cart.ItemCount -= 1;
                cart.GrandTotalPrice -= item.NetPrice;
                _context.Carts.Update(cart);
                _context.BasketItems.Remove(item);
                await _context.SaveChangesAsync();
                TempData["success"] = "Item removed successfully";
            }

            return RedirectToAction("Index");
        }
    }



}
