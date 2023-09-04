using BookVerse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookVerse.DataAccess.Helper
{
    public interface IHelper
    {
        string GetUserId();

        Task<Cart> GetCart(string userId);
    
    }
}
