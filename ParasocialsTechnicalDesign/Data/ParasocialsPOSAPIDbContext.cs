using Microsoft.EntityFrameworkCore;
using ParasocialsPOSAPI.Models;

namespace ParasocialsPOSAPI.Data
{
    public class ParasocialsPOSAPIDbContext : DbContext
    {
        public ParasocialsPOSAPIDbContext(DbContextOptions options) : base(options)
        {
        }

        //public DbSet<Discount> Discounts { get; set; }
        //public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        //public DbSet<Reservation> Reservations { get; set; }
        //public DbSet<Tax> Taxes { get; set; }
        //public DbSet<Customer> Customers { get; set; }
        //public DbSet<Employee> Employees { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Shift> Shifts { get; set; }
        public DbSet<Tip> Tips { get; set; }
        public DbSet<Company> Companies { get; set; }
        //public DbSet<RefundTicket> RefundTickets { get; set; }
    }
}
