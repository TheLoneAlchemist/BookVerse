using BookVerse.DataAccess.Data;
using BookVerse.DataAccess.Repository.IRepository;
using BookVerse.Models;
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

        public CartRepository(ApplicationDbContext db):base(db)
        {
            _dbContext = db;
        }

        public void SaveCart()
        {
            _dbContext.SaveChanges();
        }

        public void UpdateCart(Cart cartitem)
        {
            _dbContext.Update(cartitem);
        }
    }
}
