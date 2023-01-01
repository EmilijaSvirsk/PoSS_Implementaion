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

            var businessPlaces = new BusinessPlace[]
            {
                new BusinessPlace{id = 1, Title="Tarkim", Address="Vilnius", Email="tarkim@gmail.com", PhoneNr="123456789", CreatedBy=1},
                new BusinessPlace{id = 2, Title="Antra", Address="Vilnius", Email="info@antra.lt", PhoneNr="123456789", CreatedBy=2}
            };

            context.BusinessPlaces.AddRange(businessPlaces);
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

            var businessAdministrators = new BusinessAdministrator[]
            {
                new BusinessAdministrator{id = 1, Name = "BusinessAdmin1", Surname = "Surname1", Email = "something@something.lt", Password = "password", BusinessId = 1},
                new BusinessAdministrator{id = 2, Name = "BusinessAdmin2", Surname = "Surname2", Email = "businessAdmin2@antra.com", Password = "password", BusinessId = 2},
            };

            context.BusinessAdministrators.AddRange(businessAdministrators);
            context.SaveChanges();

            var orders = new Orders[]
            {
                new Orders{id = 1, CustomerId = 1, EmployeeId = 1, Date = DateTime.Now, Payment = Payment.AtSite, IsPaid = true, Comment = "Comment", IsAccepted = true, DeclineReason = "DeclineReason", DeliveryAddressId = 1, OrderProducts = new List<OrderProducts>() { new OrderProducts() { ProductServiceId = 1, CostInCents = 100 } } },
                new Orders{id = 2, CustomerId = 2, EmployeeId = 2, Date = DateTime.Now, Payment = Payment.AtSite, IsPaid = true, Comment = "Comment", IsAccepted = true, DeclineReason = "DeclineReason", DeliveryAddressId = 2, OrderProducts = new List<OrderProducts>() { new OrderProducts() { ProductServiceId = 2, CostInCents = 200 } } },
            };

            context.Orders.AddRange(orders);
            context.SaveChanges();
        }
    }
}
