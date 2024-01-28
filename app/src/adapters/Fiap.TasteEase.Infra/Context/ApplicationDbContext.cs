using Fiap.TasteEase.Domain.Aggregates.FoodAggregate.ValueObjects;
using Fiap.TasteEase.Domain.Aggregates.OrderAggregate.ValueObjects;
using Fiap.TasteEase.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Fiap.TasteEase.Infra.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<OrderModel> Orders { get; set; } = null!;
        public DbSet<ClientModel> Clients { get; set; } = null!;
        public DbSet<FoodModel> Foods { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

            modelBuilder
                .Entity<OrderModel>()
                .Property(e => e.Status)
                .HasConversion(new EnumToStringConverter<OrderStatus>());
            
            modelBuilder
                .Entity<FoodModel>()
                .Property(e => e.Type)
                .HasConversion(new EnumToStringConverter<FoodType>());

            modelBuilder
                .Entity<FoodModel>()
                .Property(e => e.Price)
                .HasPrecision(18, 2);
            
            modelBuilder
                .Entity<OrderPaymentModel>()
                .Property(e => e.Amount)
                .HasPrecision(18, 2);

            modelBuilder
                .Entity<OrderPaymentModel>()
                .HasIndex(e => e.Reference)
                .IsUnique();
            
            base.OnModelCreating(modelBuilder);
        }
        
        protected sealed override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<DateTime>()
                .HaveConversion(typeof(DateTimeToDateTimeUtc));
        }
    }

    public class DateTimeToDateTimeUtc : ValueConverter<DateTime, DateTime>
    {
        public DateTimeToDateTimeUtc() : 
            base(c => DateTime.SpecifyKind(c, DateTimeKind.Unspecified), c => c) { }
    }
}