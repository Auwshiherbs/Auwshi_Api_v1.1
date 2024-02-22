using Awushi.Domain.Contracts;
using Awushi.Domain.Models;
using Awushi.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Awushi.Infrastructure.Repositories
{
    public class CategoryRepostitory : GenericRepository<Category>, ICategoryRepostitory
    {
        public CategoryRepostitory(ApplicationDbContext dbContext) : base(dbContext)
        {
            
        }
        public async Task UpdateAsync(Category category)
        {
             _dbContext.Update(category);
            await _dbContext.SaveChangesAsync();
        }
    }
}
