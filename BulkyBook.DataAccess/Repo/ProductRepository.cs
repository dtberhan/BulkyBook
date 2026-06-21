using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repo.IRepo;
using BulkyBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repo
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext context;
        public ProductRepository(ApplicationDbContext _context): base(_context)
        {
            this.context = _context;

        }

        public void Update(Product product)
        {
            var objFromDb = context.Products.FirstOrDefault(u => u.Id == product.Id);
            if(objFromDb != null)
            {
                objFromDb.Title = product.Title;
                objFromDb.Description = product.Description;
                objFromDb.ISBN = product.ISBN;
                objFromDb.Author = product.Author;
                objFromDb.ListPrice = product.ListPrice;
                objFromDb.Price = product.Price;
                objFromDb.Price50 = product.Price50;
                objFromDb.Price100 = product.Price100;
                objFromDb.CategoryId = product.CategoryId;
                if (product.ImageUrl != null)
                {
                    objFromDb.ImageUrl = product.ImageUrl;
                }
            }
            
            
        }
    }
}
