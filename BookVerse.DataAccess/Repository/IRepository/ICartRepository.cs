using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookVerse.Models;
namespace BookVerse.DataAccess.Repository.IRepository
{
    public interface ICartRepository:IRepository<Cart>
    {
        void UpdateCart(Cart cartitem);
        void SaveCart();
        public  Task AddItemsToCart(int productid, int quantity);
    }
}
