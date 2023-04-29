using ClassWork.Domain.Entites;
using Microsoft.EntityFrameworkCore;

namespace ClassWork.Data.DbContexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public virtual DbSet<TEntity> Users { get; set; }
        public virtual DbSet<UserImage> UserImages { get; set; }

    }
}
