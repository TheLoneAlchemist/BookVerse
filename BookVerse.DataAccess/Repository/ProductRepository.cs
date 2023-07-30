using BookVerse.DataAccess.Data;
using BookVerse.DataAccess.Repository.IRepository;
using BookVerse.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookVerse.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db):base(db) 
        {
            _db = db;
        }
        public void Save()
        {
            _db.SaveChanges();
        }

        public void UpdateProduct(Product product)
        {

			_db.Entry(product).State = EntityState.Detached;
			_db.Products.Update(product);


/*
			var chtracker = _db.ChangeTracker.Entries<Product>();
			Product trackedprduct = _db.Products.FirstOrDefault(p => p.Id == product.Id);
			if (product != null)
			{
				trackedprduct.Title = product.Title;
				trackedprduct.Description = product.Description;
				trackedprduct.ListPrice = product.ListPrice;
				trackedprduct.ISBN = product.ISBN;
				trackedprduct.Author = product.Author;
				trackedprduct.CategoryId = product.CategoryId;
				if (product.Image != null)
				{
					trackedprduct.Image = product.Image;

				}

				_db.Entry<Product>(product).State = EntityState.Modified;

				//   _db.Attach<Product>(product);
			}
*/



		}
    }
}
