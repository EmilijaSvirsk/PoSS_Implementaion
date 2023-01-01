using PSP_Komanda32_API.Models;

namespace PSP_Komanda32_API.Services.Database
{
    public class DbInitializer
    {
        public static void Initialize(PoSSContext context)
        {
            if (context.Addresses.Any())
            {
                return;   // DB has been seeded
            }

            var addresses = new Address[]
            {
                new Address{Country = "Lithuania", City = "Vilnius", Street = "Gedimino", HouseNr = 1, FlatNr = 1, CustomerId = 1},
                new Address{Country = "Lithuania", City = "Vilnius", Street = "Gedimino", HouseNr = 1, FlatNr = 2, CustomerId = 2},
                new Address{Country = "Lithuania", City = "Vilnius", Street = "Gedimino", HouseNr = 1, FlatNr = 3, CustomerId = 3},
                new Address{Country = "Lithuania", City = "Vilnius", Street = "Gedimino", HouseNr = 1, FlatNr = 4, CustomerId = 4}
            };

            context.Addresses.AddRange(addresses);
            context.SaveChanges();

            var employees = new Employee[]
            {
                new Employee{Name="Rokas", Surname="Lekecinskas", Email="rokas.lekecinskas@gmail.com", Username="Rokas123", Password="password", CreatedBy=1},
                new Employee{Name="Rokas", Surname="Lekecinskas", Email="rokas.lekecinskas@gmail.com", Username="Rokas123", Password="password", CreatedBy=1}
            };

            context.Employees.AddRange(employees);
            context.SaveChanges();

            var shifts = new Shift[]
            {
                new Shift{EmployeeId=1, Start=DateTime.Today.AddHours(-4), End=DateTime.Today.AddHours(4), Location="VU"},
                new Shift{EmployeeId=2, Start=DateTime.Today.AddHours(-4), End=DateTime.Today.AddHours(4), Location="VU"},
            };

            context.Shifts.AddRange(shifts);
            context.SaveChanges();

            var reservations = new Reservation[]
            {
                new Reservation{Date=DateTime.Today, Duration=30, CustomerCount=1, Status=ReservationStatus.Awaiting},
                new Reservation{Date=DateTime.Today.AddHours(1), Duration=30, CustomerCount=1, Status=ReservationStatus.Awaiting},
            };

            context.Reservations.AddRange(reservations);
            context.SaveChanges();
        }
    }
}
