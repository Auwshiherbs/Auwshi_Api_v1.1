using Awushi.Domain.Models;
using Awushi.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Awushi.Infrastructure.Common
{
    public class SeedData
    {

        public static async Task SeedRoles(IServiceProvider serviceProvider)
        {
            using var scope  = serviceProvider.CreateScope();
            var roleManagaer = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            var roles = new List<IdentityRole>
            {
                new IdentityRole {Name="ADMIN",NormalizedName="ADMIN"},
                new IdentityRole {Name="CUSTOMER",NormalizedName="CUSTOMER"}
            };
            foreach (var role in roles) {
                if (!await roleManagaer.RoleExistsAsync(role.Name))
                {
                    await roleManagaer.CreateAsync(role);
                }
            }
        }
        public static async Task SeedDataAsync(ApplicationDbContext _dbContext)
        {
            if( !_dbContext.Brands.Any() )
            {
                await _dbContext.AddRangeAsync(
                    new Brand
                    {
                        Name = "Auwshi",
                        EstablishedYear= 2023
                    },
                    new Brand
                    {
                        Name = "Ayush",
                        EstablishedYear = 2023
                    },
                    new Brand
                    {
                        Name = "Uki",
                        EstablishedYear = 2023
                    }

                    );
                await _dbContext.SaveChangesAsync();
            }

            if (!_dbContext.Categories.Any())
            {
                await _dbContext.AddRangeAsync(
                    new Category
                    {
                        Name = "Auwshi Hair Oil"
                    },
                    new Category
                    {
                        Name = "Auwshi Hair Powder"
                    },
                    new Category
                    {
                        Name = "Auwshi Tooth Powder"
                    }
                    );
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
