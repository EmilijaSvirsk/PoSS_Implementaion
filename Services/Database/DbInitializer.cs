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

            var businessPlaces = new BusinessPlace[]
            {
                new BusinessPlace{id = 1, Title="Tarkim", Address="Vilnius", Email="tarkim@gmail.com", PhoneNr="123456789", CreatedBy=1},
                new BusinessPlace{id = 2, Title="Antra", Address="Vilnius", Email="info@antra.lt", PhoneNr="123456789", CreatedBy=2}
            };

            context.BusinessPlaces.AddRange(businessPlaces);
            context.SaveChanges();

            var businessAdministrators = new BusinessAdministrator[]
            {
                new BusinessAdministrator{id = 1, Name = "BusinessAdmin1", Surname = "Surname1", Email = "something@something.lt", Password = "password", BusinessId = 1},
                new BusinessAdministrator{id = 2, Name = "BusinessAdmin2", Surname = "Surname2", Email = "businessAdmin2@antra.com", Password = "password", BusinessId = 2},
            };

            context.BusinessAdministrators.AddRange(businessAdministrators);
            context.SaveChanges();

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


            var productServices = new ProductService[]
            {
                new ProductService{id = 1, Name = "Product1", Description = "Description1", CostInCents = 100, BusinessId = 1},
                new ProductService{id = 2, Name = "Product2", Description = "Description2", CostInCents = 200, BusinessId = 1},
            };

            context.ProductServices.AddRange(productServices);
            context.SaveChanges();

            var customers = new Customer[]
            {
                new Customer{id = 1, Name = "Customer1", Surname = "Surname1", Email = "abc@email.com", Password = "password", LoyaltyPoints = 100 },
                new Customer{id = 2, Name = "Customer2", Surname = "Surname2", Email = "abd@email.com", Password = "password", LoyaltyPoints = 200 },
            };

            context.Customers.AddRange(customers);
            context.SaveChanges();


            var orders = new Orders[]
            {
                new Orders{id = 1, CustomerId = 1, EmployeeId = 1, Date = DateTime.Now, Payment = Payment.AtSite, IsPaid = true, Comment = "Comment", IsAccepted = true, DeclineReason = "DeclineReason", DeliveryAddressId = 1, OrderProducts = new List<OrderProducts>() { new OrderProducts() { ProductServiceId = 1, CostInCents = 100 } } },
                new Orders{id = 2, CustomerId = 2, EmployeeId = 2, Date = DateTime.Now, Payment = Payment.AtSite, IsPaid = true, Comment = "Comment", IsAccepted = true, DeclineReason = "DeclineReason", DeliveryAddressId = 2, OrderProducts = new List<OrderProducts>() { new OrderProducts() { ProductServiceId = 2, CostInCents = 200 } } },
            };

            context.Orders.AddRange(orders);
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
                new Reservation{id = 1, Date=DateTime.Today, Duration=30, CustomerCount=1, Status=ReservationStatus.Awaiting},
                new Reservation{id = 2, Date=DateTime.Today.AddHours(1), Duration=30, CustomerCount=1, Status=ReservationStatus.Awaiting},
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
