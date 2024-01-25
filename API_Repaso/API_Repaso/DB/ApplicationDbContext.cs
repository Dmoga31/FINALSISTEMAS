
using API_Repaso.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace API_Repaso.DB
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
    }
}
