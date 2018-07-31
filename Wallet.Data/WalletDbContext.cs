using Bit.Core.Implementations;
using Bit.Core.Models;
using Bit.Data.EntityFrameworkCore.Contracts;
using Bit.Data.EntityFrameworkCore.Implementations;
using Microsoft.EntityFrameworkCore;
using System;
using Wallet.Model.User;

namespace Wallet.Data
{
    public class WalletDbContext : EfCoreDbContextBase
    {
        public WalletDbContext()
            : base(new DbContextOptionsBuilder().UseSqlServer(DefaultAppEnvironmentsProvider.Current.GetActiveAppEnvironment().GetConfig<string>("AppConnectionString")).Options)
        {

        }

        public WalletDbContext(AppEnvironment appEnvironment, IDbContextObjectsProvider dbContextCreationOptionsProvider)
              : base(appEnvironment.GetConfig<string>("AppConnectionString"), dbContextCreationOptionsProvider)
        {

        }

        public virtual DbSet<Customer> Customers { get; set; }

        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<Operator> Operators { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(user =>
            {
                user.HasIndex(u => u.MobileNumber).IsUnique(true);
                user.HasIndex(u => u.EmailAddress).HasFilter($"{nameof(User.EmailAddress)} IS NOT NULL").IsUnique(true);
                user.Property(u => u.Id).HasDefaultValueSql("NEWSEQUENTIALID()").ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<Operator>(@operator =>
            {
                @operator.Property(u => u.Id).HasDefaultValueSql("NEWSEQUENTIALID()").ValueGeneratedOnAdd();
                @operator.HasIndex(o => o.UserName).IsUnique(true);
            });

            modelBuilder.Entity<Customer>(customer =>
            {
                customer.Property(u => u.Id).HasDefaultValueSql("NEWSEQUENTIALID()").ValueGeneratedOnAdd();
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
