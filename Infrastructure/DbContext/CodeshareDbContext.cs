using DomainLayer.Entity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DbContext
{
    public class CodeshareDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public CodeshareDbContext(DbContextOptions<CodeshareDbContext> options)
             : base(options)
        {
        }

        public DbSet<CodeshareEntity> Code { get; set; }
    }
}
