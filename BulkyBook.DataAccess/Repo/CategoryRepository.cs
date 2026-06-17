using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repo.IRepo;
using BulkyBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repo
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext _db;
        public CategoryRepository(ApplicationDbContext db):base(db)
        {
            _db = db;

        }

        
        public void Update(Category obj)
        {
            _db.Categories.Update(obj);
        }
    }
}
