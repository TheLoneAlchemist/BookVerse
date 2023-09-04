using BookVerse.DataAccess.Data;
using BookVerse.DataAccess.Helper;
using BookVerse.Models.ViewModels;
using BookVerse.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookVerse.Uitility.Constants;
using Microsoft.AspNetCore.Authorization;

namespace BookVerse.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize(Roles = "User")]
    public class OrderController : Controller
    {
        public readonly ApplicationDbContext _context;
        private readonly IHelper _helper;
        public OrderController(ApplicationDbContext context, IHelper helper)
        {
            _context = context;
            _helper = helper;
        }

        public async Task<IActionResult> Index(int? checkoutId)
        {
            if (checkoutId != null)
            {
                List<string> paymentMode = new List<string>()
                {
                    PaymentMode.Cashondelivery,
                    PaymentMode.Debitcard,
                    PaymentMode.Creditcard,
                    PaymentMode.UPI
                };


                var checkout = await _context.Checkouts.Where(c => c.Id == checkoutId).FirstOrDefaultAsync();
                if (checkout != null)
                {
                    OrderVM orderVM = new OrderVM()
                    {
                        TotalPrice = checkout.TotalPrice,
                        PaymentOption = paymentMode,
                        CheckoutId = checkout.Id
                    };
                    return View(orderVM);

                }

            }
            TempData["error"] = "Something Abnormal happened!";
            return RedirectPermanent("/Products/Index");
        }
        [HttpPost]
        public async Task<IActionResult> PaymentOptions(string PaymentOption, int checkoutId)
        {
            if (ModelState.IsValid)
            {
                if (PaymentOption == PaymentMode.Debitcard)
                {
                    ViewData["checkoutID"] = checkoutId;
                    return View("DebitCardPayment");
                }
                else if (PaymentOption == PaymentMode.Creditcard)
                {

                    ViewData["checkoutID"] = checkoutId;
                    return View("CreditCardPayment");
                }
                else if (PaymentOption == PaymentMode.UPI)
                {
                    ViewData["checkoutID"] = checkoutId;
                    return View("UPI");

                }
                else if (PaymentOption == PaymentMode.Cashondelivery)
                {
                    return RedirectToAction("PaymentConfirm", new { checkoutId, PaymentOption });
                }
            }
            TempData["error"] = "Something went wrong";
            return Redirect("/Cart");
        }
        [HttpPost]
        public async Task<IActionResult> PaymentConfirm(int? checkoutId, string PaymentOption)
        {

            //Checking the paymentoption is valid or not

            var checkout = await _context.Checkouts.Where(c => c.Id == checkoutId).FirstOrDefaultAsync();
            if (checkout != null)
            {
                bool paymentstatus;
                if (PaymentOption == PaymentMode.Creditcard || PaymentOption == PaymentMode.Debitcard || PaymentOption == PaymentMode.UPI)
                {
                    paymentstatus = true;
                }
                else if (PaymentOption == PaymentMode.Cashondelivery)
                {
                    paymentstatus = false;
                }
                else
                {
                    TempData["error"] = "Something wrong in Payment Option";
                    return Redirect("/Cart");
                }

                // creating an order
                Order order = new Order()
                {
                    CheckoutId = checkout.Id,
                    PayementMode = PaymentOption,
                    PaymentStatus = paymentstatus,
                    OrderStatus = true
                };
                _context.Orders.Add(order);



                //adding ordereditems

                List<OrderedItem> orderedItems = new List<OrderedItem>();
                var items = _context.Checkouts.Where(c => c.Id == checkoutId)
                                              .Include(i => i.Cart)
                                              .ThenInclude(p => p.BasketItems)
                                              .FirstOrDefault();

                foreach (var item in items.Cart.BasketItems)
                {
                    orderedItems.Add(new OrderedItem() { NetPrice = item.NetPrice, ProductId = item.ProductId, Quantity = item.Quantity, Product = item.Product, Order = order });
                }
                _context.OrderedItems.AddRange(orderedItems);
                _context.SaveChanges();


                //clearing the cart
                var cart = checkout.Cart;
                var basketitems = cart.BasketItems;
                cart.CheckOutStatus = true;
                cart.GrandTotalPrice = 0;
                cart.ItemCount = 0;
                _context.Carts.Update(cart);


                //crearing the basketitems
                basketitems.Clear();
                _context.BasketItems.UpdateRange(basketitems);
                _context.SaveChanges();

                return View("ConfirmOrder");
            }
            TempData["error"] = "Something went wrong!";
            return Redirect("/Cart");
        }

        public async Task<IActionResult> MyOrders()
        {

            List<MyOrderVM> myOrderlist = new List<MyOrderVM>();
            var userId = _helper.GetUserId();
            var orders = _context.Orders.Where(o => o.Checkout.UserId == userId && o.OrderStatus == true).Include(a => a.OrderedItems).ToList();
            var ordereditems = _context.OrderedItems.Where(o => o.Order.Checkout.UserId == userId).Include(i => i.Order).Include(p => p.Product).ToList();
            foreach (var item in ordereditems)
            {
                myOrderlist.Add(new MyOrderVM() { OrderId = item.Id, Name = item.Product.Title, NetPrice = item.NetPrice, Quantity = item.Quantity, DeliveryStatus = item.Order.DeliveryStatus, Price = item.Product.Price });
            }
            return View(myOrderlist);
        }
    }
}
