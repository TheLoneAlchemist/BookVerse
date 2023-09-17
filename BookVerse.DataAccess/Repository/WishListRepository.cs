using BookVerse.DataAccess.Data;
using BookVerse.DataAccess.Helper;
using BookVerse.DataAccess.Repository.IRepository;
using BookVerse.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookVerse.DataAccess.Repository
{
    public class WishListRepository : Repository<WishList>,IWishListRepository
    {
        private  ApplicationDbContext db;
        public WishListRepository(ApplicationDbContext db) : base(db)
        {
            this.db = db;

        }

        public void Save()
        {
            db.SaveChanges();
        }

       


    }
}
