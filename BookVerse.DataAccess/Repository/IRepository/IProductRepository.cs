using BookVerse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookVerse.DataAccess.Repository.IRepository
{
    public interface IProductRepository: IRepository<Product>
    {
        void UpdateProduct(Product product);
        void Save();
    }
}
