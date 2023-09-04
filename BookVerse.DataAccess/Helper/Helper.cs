using BookVerse.DataAccess.Data;
using BookVerse.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookVerse.DataAccess.Helper
{
    public class Helper:IHelper
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _context;
        public Helper(IHttpContextAccessor httpContextAccessor, UserManager<IdentityUser> userManager, ApplicationDbContext context)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _context = context;
        }

        public async Task<Cart> GetCart(string userId)
        {

            var cart = await _context.Carts.Where(i => i.UserId == userId)
                                           .Include(f => f.BasketItems)
                                           .ThenInclude(p => p.Product)
                                           .FirstOrDefaultAsync();

            return cart;
        }

        public string GetUserId()
        {
            var claimsprincipal = _httpContextAccessor.HttpContext.User;
            var userId = _userManager.GetUserId(claimsprincipal);
            return userId;
        }
    }
}
