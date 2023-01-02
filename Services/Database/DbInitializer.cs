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
                new Employee{Name="Tomas", Surname="Tomauskas", Email="tomas.tomauskas@gmail.com", Username="Tomas123", Password="password", CreatedBy=1},
                new Employee{Name="Jonas", Surname="Jonaitis", Email="jonas.jonaitis@gmail.com", Username="Jonas123", Password="password", CreatedBy=1},
                new Employee{Name="Petras", Surname="Petraitis", Email="petras.petraitis@gmail.com", Username="Petras123", Password="password", CreatedBy=1},
                new Employee{Name="Antanas", Surname="Antanaitis", Email="antanas.antanaitis@gmail.com", Username="Antanas123", Password="password", CreatedBy=1},
                new Employee{Name="Vardenis", Surname="Pavardenis", Email="vardenis.pavardenis@gmail.com", Username="Vardenis123", Password="password", CreatedBy=1} 
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
            
            var businessManagers = new BusinessManager[]
            {
                new BusinessManager{id=1, Name="Business", Surname="Manager", Email="businessmanager@gmail.com", Username="businessmanager", Password="businessmanager123", BusinessId=1,CreatedBy=1}
            };

            context.BusinessManagers.AddRange(businessManagers);
            context.SaveChanges();

            var taxes = new Tax[]
            {
                new Tax{id=1, Percentage=22.3M, Name="Normal Tax", Description="Tax for normal purchases."},
                new Tax{id=2, Percentage=50.0M, Name="Special Tax", Description="Tax for special purchases."}
            };

            context.Tax.AddRange(taxes);
            context.SaveChanges();

            var discounts = new Discount[]
            {
                new Discount{id=1, Credit=10.0M, CreatedBy=1, LoaltyCost=20},
                new Discount{id=2, Credit=80.9M, CreatedBy=1, LoaltyCost=99}
            };

            context.Discount.AddRange(discounts);
            context.SaveChanges();
        }
    }
}
