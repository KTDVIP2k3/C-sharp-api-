using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zSkinCareBookingRepositories.Base;
using zSkinCareBookingRepositories.Models;

namespace zSkinCareBookingRepositories
{
    public class BlogCategoriesRepository : GenericRepository<BlogCategory>
    {
        public BlogCategoriesRepository() { }

        public async Task<List<BlogCategory>> GetAll()
        {
            return await _context.BlogCategories.Include(b => b.Posts).ToListAsync();
        }
    }
}
