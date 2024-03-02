using Awushi.Domain.Contracts;
using Awushi.Domain.Models;
using Awushi.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Awushi.Infrastructure.Repositories
{
    public class ProductRepository :GenericRepository<Product>,IProductRepository
    {
        public ProductRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            
        }

        public async Task<IEnumerable<Product>> GetAllProductAsync()
        {
          return await _dbContext.products.Include(x=>x.Category).Include(x=>x.Brand).AsNoTracking().ToListAsync();
        }

        public async Task<Product> GetDetailsAsync(int id)
        {
            return await _dbContext.products.Include(x => x.Category).Include(x => x.Brand).AsNoTracking().FirstOrDefaultAsync(x=>x.Id == id);
        }

        public async Task UpdateAsync(Product product)
        {
            _dbContext.Update(product);
            await _dbContext.SaveChangesAsync();
        }
    }
}
