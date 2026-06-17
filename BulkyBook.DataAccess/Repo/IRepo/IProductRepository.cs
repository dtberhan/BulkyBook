using BulkyBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repo.IRepo
{
    public interface IProductRepository: IRepository<Product>
    {
        public void Update(Product product);
    }
}
