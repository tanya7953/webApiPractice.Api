﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using webApiPractice.Contracts;

namespace webApiPractice.Data
{
    public class DataContext : IdentityDbContext<AppUser>
    {
        public DataContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
            
        }
        public DbSet<Stock> Stocks {  get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Portfolio> Portfolio { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Portfolio>(x => x.HasKey(p => new { p.AppUserId, p.StockId }));
            builder.Entity<Portfolio>()
                .HasOne(u => u.AppUser)
                .WithMany(u => u.Portfolios)
                .HasForeignKey(p => p.AppUserId);
            builder.Entity<Portfolio>()
               .HasOne(u => u.Stock)
               .WithMany(u => u.Portfolios)
               .HasForeignKey(p => p.StockId);

            List<IdentityRole> role = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name="Admin",
                    NormalizedName="ADMIN"
                },new IdentityRole
                {
                    Name="User",
                    NormalizedName="USER"
                }
            };
            builder.Entity<IdentityRole>().HasData(role);
           
        }
    }
}
