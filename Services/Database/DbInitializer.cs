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
        }
    }
}
