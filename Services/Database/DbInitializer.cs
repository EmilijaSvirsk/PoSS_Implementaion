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
        }
    }
}
