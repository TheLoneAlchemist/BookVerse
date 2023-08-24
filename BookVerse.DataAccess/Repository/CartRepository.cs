using BookVerse.DataAccess.Data;
using BookVerse.DataAccess.Repository.IRepository;
using BookVerse.Models;
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
        public async Task AddItemsToCart( int productid, int quantity)
        {
            var userId = GetUserId();

            if (String.IsNullOrEmpty(userId))
            {
                throw new Exception("User is null");
            }

            var cart = await GetUserCart(userId);
            if (cart is null)
            {
                await _dbContext.Carts.AddAsync(new Cart() { UserId = userId });
                await _dbContext.SaveChangesAsync();
            }

            var product = await _dbContext.Products.Where(m => m.Id == productid).FirstOrDefaultAsync();
            var item = await _dbContext.BasketItems.AddAsync(new BasketItem() { Product = product, AddedOn = DateTime.Now,Status=true, Quantity=quantity});
            await _dbContext.SaveChangesAsync();

            cart = await GetUserCart(userId);
            
            
        }


        #region Utility

        private  string GetUserId()
        {
            var claimsprincipal = _httpContextAccessor.HttpContext.User;
            var userId = _userManager.GetUserId(claimsprincipal);
            return userId;

        }

        private async Task<Cart?> GetUserCart(string userId)
        {
            var cart = await _dbContext.Carts.Where(x => x.UserId == userId).FirstOrDefaultAsync();
            return cart;
        }


        #endregion
    }
}
