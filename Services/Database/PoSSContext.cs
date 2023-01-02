using Microsoft.EntityFrameworkCore;
using System;
using PSP_Komanda32_API.Models;

namespace PSP_Komanda32_API.Services.Database
{
    public class PoSSContext : DbContext
    {
        public DbSet<Address> Addresses { get; set; }
        public DbSet<BannedCustomers> BannedCustomers { get; set; }
        public DbSet<BusinessAdministrator> BusinessAdministrators { get; set; }
        public DbSet<BusinessManager> BusinessManagers { get; set; }
        public DbSet<BusinessPlace> BusinessPlaces { get; set; }
        public DbSet<Courier> Couriers { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Discount> Discount { get; set; }
        public DbSet<Discounts> Discounts { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<NonWork> NonWorks { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<ProductService> ProductServices { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Shift> Shifts { get; set; }
        public DbSet<SystemAdministrator> SystemAdministrators { get; set; }
        public DbSet<Tax> Tax { get; set; }
        public DbSet<Taxes> Taxes { get; set; }

        public PoSSContext(DbContextOptions<PoSSContext> options) : base(options)
        {
        }
    }
}
