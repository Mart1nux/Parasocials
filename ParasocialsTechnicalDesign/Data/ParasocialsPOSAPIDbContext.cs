using Microsoft.EntityFrameworkCore;
using ParasocialsPOSAPI.Models;

namespace ParasocialsPOSAPI.Data
{
    public class ParasocialsPOSAPIDbContext : DbContext
    {

        public ParasocialsPOSAPIDbContext() { }

        public ParasocialsPOSAPIDbContext(DbContextOptions options) : base(options)
        {
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data source=sql3.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            modelBuilder.Entity<Company>().HasKey(e => e.SupplierId);
            modelBuilder.Entity<Tip>().HasKey(e => e.TipId);
            modelBuilder.Entity<Customer>().HasKey(e => e.Id);
            modelBuilder.Entity<Discount>().HasKey(e => e.DiscountId);
            modelBuilder.Entity<Employee>().HasKey(e => e.Id);
            modelBuilder.Entity<Group>().HasKey(e => e.GroupId);
            modelBuilder.Entity<Inventory>().HasKey(e => e.InventoryId);
            modelBuilder.Entity<Loyalty>().HasKey(e => e.LoyaltyId);
            modelBuilder.Entity<Order>().HasKey(e => e.OrderId);
            modelBuilder.Entity<Position>().HasKey(e => e.PositionId);
            modelBuilder.Entity<Premise>().HasKey(e => e.PremiseId);
            modelBuilder.Entity<RefundTicket>().HasKey(e => e.RefundTicketId);
            modelBuilder.Entity<Reservation>().HasKey(e => e.ReservationId);
            modelBuilder.Entity<Shift>().HasKey(e => e.ShiftId);
            modelBuilder.Entity<Tax>().HasKey(e => e.TaxId);

            modelBuilder.Entity<Company>().Property(e => e.SupplierId).ValueGeneratedOnAdd();
            modelBuilder.Entity<Tip>().Property(e => e.TipId).ValueGeneratedOnAdd();
            modelBuilder.Entity<Customer>().Property(e => e.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Discount>().Property(e => e.DiscountId).ValueGeneratedOnAdd();
            modelBuilder.Entity<Employee>().Property(e => e.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Group>().Property(e => e.GroupId).ValueGeneratedOnAdd();
            modelBuilder.Entity<Inventory>().Property(e => e.InventoryId).ValueGeneratedOnAdd();
            modelBuilder.Entity<Loyalty>().Property(e => e.LoyaltyId).ValueGeneratedOnAdd();
            modelBuilder.Entity<Order>().Property(e => e.OrderId).ValueGeneratedOnAdd();
            modelBuilder.Entity<Position>().Property(e => e.PositionId).ValueGeneratedOnAdd();
            modelBuilder.Entity<Premise>().Property(e => e.PremiseId).ValueGeneratedOnAdd();
            modelBuilder.Entity<RefundTicket>().Property(e => e.RefundTicketId).ValueGeneratedOnAdd();
            modelBuilder.Entity<Reservation>().Property(e => e.ReservationId).ValueGeneratedOnAdd();
            modelBuilder.Entity<Shift>().Property(e => e.ShiftId).ValueGeneratedOnAdd();
            modelBuilder.Entity<Tax>().Property(e => e.TaxId).ValueGeneratedOnAdd();

            modelBuilder.Entity<Discount>().HasOne(e => e.Group).WithOne(x => x.Discount);
            modelBuilder.Entity<Employee>().HasOne(e => e.Position).WithMany(x => x.Employees);
            modelBuilder.Entity<Group>().HasMany(e => e.Products).WithMany(x => x.Groups);
            modelBuilder.Entity<Inventory>().HasOne(e => e.product).WithOne(x => x.Inventory);
            modelBuilder.Entity<Loyalty>().HasOne(e => e.Customer).WithOne(x => x.Loyalty);
            modelBuilder.Entity<Order>().HasMany(e => e.Products).WithMany(x => x.Orders);
            modelBuilder.Entity<Reservation>().HasOne(e => e.Premise).WithOne(x => x.Reservation);
            modelBuilder.Entity<Shift>().HasMany(e => e.Employees).WithMany(x => x.Shifts);
            modelBuilder.Entity<Tax>().HasOne(e => e.Group).WithOne(x => x.Tax);
            modelBuilder.Entity<Tip>().HasOne(e => e.Receiver).WithMany(x => x.Tips);


        }

        public virtual DbSet<Discount> Discounts { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Reservation> Reservations { get; set; }
        public virtual DbSet<Tax> Taxes { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Position> Positions { get; set; }
        public virtual DbSet<Shift> Shifts { get; set; }
        public virtual DbSet<Tip> Tips { get; set; }
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<RefundTicket> RefundTickets { get; set; }
    }
}
