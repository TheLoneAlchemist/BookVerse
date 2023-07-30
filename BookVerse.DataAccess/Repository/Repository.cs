using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BookVerse.DataAccess.Data;
using BookVerse.DataAccess.Repository.IRepository;
using BookVerse.Models;
using Microsoft.EntityFrameworkCore;

//Implimentation of common functionality interface
namespace BookVerse.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbSet;
        public Repository(ApplicationDbContext db)
        {
            _db = db;
            this.dbSet = _db.Set<T>();
            _db.Products.Include(x => x.Category);

        }

        public void Add(T entity)
        {
            _db.Add(entity);

        }

        public T Get(Expression<Func<T, bool>> filter,string? includeProperty= null)
        {
            IQueryable<T> query = dbSet;
            if (!string.IsNullOrEmpty(includeProperty))
            {
                foreach (var prop in includeProperty.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(prop);
                }
            }
            query = query.Where(filter).AsNoTracking();
            return query.FirstOrDefault();
        }

        public IEnumerable<T> GetAll(string? includeProperty = null)
        {
            IQueryable<T> query = dbSet;
            if(!string.IsNullOrEmpty(includeProperty))
            {
                foreach(var prop in includeProperty.Split(new char[] {','},StringSplitOptions.RemoveEmptyEntries ))
                {
                    query = query.Include(prop);
                }
            }
            return query.ToList();
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            dbSet.RemoveRange(entities);
        }
    }
}
