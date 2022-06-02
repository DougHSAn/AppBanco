using AppBanco.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppBanco.Data
{
    public class AccountContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<AccTran> AccTran { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=IMPOSTACAO\SQLEXPRESS;Database=SQLEXPRESS;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccTran>(entity =>
            {
                entity.HasKey(e => new { e.NumAccount, e.IDTransaction });
            });
            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasKey(e => new { e.NumAccount });
            });
            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.HasKey(e => new { e.IDTransaction });
            });
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => new { e.ID });
            });
        }
    }
}
