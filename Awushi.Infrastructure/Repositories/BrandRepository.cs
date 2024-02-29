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
    internal class BrandRepository : GenericRepository<Brand>,IBrandRepository
    {
        public BrandRepository(ApplicationDbContext dbContext) :base(dbContext)
        {
            
        }
        public async Task UpdateAsync(Brand brand)
        {
            _dbContext.Update(brand);
            await  _dbContext.SaveChangesAsync();
        }
    }
}
