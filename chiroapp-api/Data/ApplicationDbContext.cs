using chiroapp_api.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace chiroapp_api.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Post>()
                .HasMany(p => p.Comments)
                .WithOne()
                .IsRequired()
                .HasForeignKey("PostId");
            builder.Entity<Post>().Property(p => p.Title).IsRequired();
            builder.Entity<Post>().Property(p => p.Content).IsRequired();
            builder.Entity<Post>().Property(p => p.Group).IsRequired();
            builder.Entity<Comment>().Property(p => p.Content).IsRequired();
        }

        public DbSet<Post> Posts { get; set; }
    }
}
