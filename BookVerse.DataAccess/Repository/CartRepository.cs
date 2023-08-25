using BookVerse.DataAccess.Data;
using BookVerse.DataAccess.Repository.IRepository;
using BookVerse.Models;
using BookVerse.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookVerse.DataAccess.Repository
{
    public class CartRepository : Repository<Cart>, ICartRepository
    {
        private ApplicationDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<IdentityUser> _userManager;

        public CartRepository(ApplicationDbContext db, IHttpContextAccessor httpContextAccessor, UserManager<IdentityUser> userManager):base(db)
        {
            _dbContext = db;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        public void SaveCart()
        {
            _dbContext.SaveChanges();
        }

        public void UpdateCart(Cart cartitem)
        {
            _dbContext.Update(cartitem);
        }


        //Item will be added here...
        public async Task<int> AddItemToCart( int productid, int quantity)
        {
            var userId = GetUserId();

            if (String.IsNullOrEmpty(userId))
            {
                throw new Exception("User is null");
            }

           

            Cart? cart = await GetUserCart(userId);

            if (cart is null)
            {
               await _dbContext.Carts.AddAsync(new Cart() { UserId = userId });
               await _dbContext.SaveChangesAsync();
            }

            cart = await GetUserCart(userId);

            if (productid != null)
            {
                var product = await _dbContext.Products.Where(p => p.Id == productid).FirstOrDefaultAsync();
               _dbContext.BasketItems.Add(new BasketItem() { Product = product, AddedOn = DateTime.Now, Quantity = quantity, Cart = cart, NetPrice = product.ListPrice * quantity, Status = true });
               await _dbContext.SaveChangesAsync();


                var item = _dbContext.BasketItems.Where(x => x.Cart == cart).FirstOrDefault();



                var newcart = new Cart()
                {
                    Id = cart.Id,
                    CartPrice = cart.CartPrice + item.NetPrice,
                    ItemCount = cart.ItemCount + 1,
                    UserId = userId
                };

                

                _dbContext.Carts.Update(newcart);
            }



            return cart.ItemCount;




        }


        #region Utility

        public  string GetUserId()
        {
            var claimsprincipal = _httpContextAccessor.HttpContext.User;
            var userId = _userManager.GetUserId(claimsprincipal);
            return userId;

        }

        public async Task<Cart?> GetUserCart(string userId)
        {
            var cart = await _dbContext.Carts.Where(x => x.UserId == userId).Include("Items").FirstOrDefaultAsync();
            return cart;
        }




        #endregion
    }
}
