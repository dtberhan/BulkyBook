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
            context.Products.Update(product);
            
        }
    }
}
