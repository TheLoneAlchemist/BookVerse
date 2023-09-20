using BookVerse.DataAccess.Data;
using BookVerse.Models.ViewModels;
using BookVerse.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;

namespace BookVerse.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize(Roles = "User")]
    public class CheckoutController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CheckoutController(ApplicationDbContext context, UserManager<IdentityUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<IActionResult> Index()
        {
            var userId = GetUserId();
            if (string.IsNullOrEmpty(userId))
            {
                return Redirect("/Identity/Account/Login");
            }

            var cart = await GetUserCart(userId);
            if (cart.ItemCount == 0)
            {
                TempData["error"] = "Cart is Empty! First Add Some Item to chekout";
                return Redirect("/Customer/Home");
            }


            CheckoutVM checkoutVM = new CheckoutVM();

            checkoutVM.Cart = cart;
            checkoutVM.Checkout = new Checkout(checkoutVM.Cart.GrandTotalPrice);

            return View(checkoutVM);
        }

        [HttpPost]
        public async Task<IActionResult> Index(CheckoutVM checkoutVM)
        {

            var userId = GetUserId();
            checkoutVM.Checkout.UserId = userId;
            checkoutVM.Address.UserId = userId;
            if (GetUserId() != null)
            {
                ModelState.ClearValidationState("Checkout.UserId");
                ModelState.MarkFieldValid("Checkout.UserId");
                ModelState.ClearValidationState("Address.UserId");
                ModelState.MarkFieldValid("Address.UserId");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    ////storing address on session
                    //var AddressSessioned = JsonConvert.SerializeObject(checkoutVM.Address);
                    //_httpContextAccessor.HttpContext.Session.SetString("AddressSession", AddressSessioned);


                    await _context.Addresses.AddAsync(checkoutVM.Address);
                    await _context.SaveChangesAsync();
                    var address = await _context.Addresses.Where(p => p.UserId == userId).FirstOrDefaultAsync();
                    var cart = await GetUserCart(userId);
                    if (address != null && cart != null)
                    {
                        checkoutVM.Checkout.AddressId = address.Id;
                        checkoutVM.Checkout.CartId = cart.Id;


                        await _context.Checkouts.AddAsync(checkoutVM.Checkout);
                        await _context.SaveChangesAsync();
                        var checkout = await _context.Checkouts.Where(c => c.CartId == cart.Id).FirstOrDefaultAsync();
                        return RedirectToAction("Index", "Order", new { checkoutId = checkout.Id });
                    }
                }
                catch (Exception e)
                {
                    TempData["error"] = "Somethingwent wrong";
                    //Store error in the database
                }
            }
            return View();

        }






        [NonAction]
        private string GetUserId()
        {
            var claimsprincipal = _httpContextAccessor.HttpContext.User;
            var userId = _userManager.GetUserId(claimsprincipal);
            return userId;

        }

        //
        //Summary:
        //      Returns the Cart type
        //Return:
        //      Cart if found, or null

        [NonAction]
        private async Task<Cart?> GetUserCart(string userId)
        {

            var cart = await _context.Carts.Where(i => i.UserId == userId).Include(f => f.BasketItems).ThenInclude(p => p.Product).FirstOrDefaultAsync();

            return cart;
        }

    }
}
