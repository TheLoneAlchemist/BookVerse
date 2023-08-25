using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookVerse.Models;
using BookVerse.Models.ViewModels;

namespace BookVerse.DataAccess.Repository.IRepository
{
    public interface ICartRepository:IRepository<Cart>
    {
        void UpdateCart(Cart cartitem);
        void SaveCart();
        public Task<int> AddItemToCart( int productid, int quantity);
        public Task<Cart?> GetUserCart(string userId);
        public string GetUserId();


    }
}
