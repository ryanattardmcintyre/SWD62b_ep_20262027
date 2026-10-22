using Common.Models;
using DataAccess.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class CategoriesRepository
    {
        private LibraryDbContext _context;
        public CategoriesRepository(LibraryDbContext context) 
        { 
        _context = context; 
        }

        public IQueryable<Category> GetCategories() {
            return _context.Categories.OrderBy(x => x.Name);
        
        }
    }
}
