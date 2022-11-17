using PSP_Komanda32_API.Models;
using PSP_Komanda32_API.Services.Interfaces;
using System.Diagnostics;
using System.Reflection;

namespace PSP_Komanda32_API.Services
{
    public class Randomizer : IRandomizer
    {
        public T GenerateRandomData<T>(int? id = null) where T : class, new()
        {
            var data = typeof(T).GetProperties();
            var newClass = new T();
            var rnd = new Random();

            foreach (var el in data)
            {
                if (el.Name == "id")
                {
                    el.SetValue(newClass, id ?? rnd.Next(1000));
                    continue;
                }


                if (el.PropertyType == typeof(int))
                {
                    el.SetValue(newClass, rnd.Next(1000));
                }
                else if (el.PropertyType == typeof(double))
                {
                    el.SetValue(newClass, rnd.NextDouble()); //TODO: kokiu cia reziu reikia?
                }
                else if (el.PropertyType == typeof(DateTime))
                {
                    el.SetValue(newClass, RandomDateTime(rnd));
                }
                else if (el.PropertyType == typeof(Transport))
                {
                    el.SetValue(newClass, RandomEnum<Transport>(rnd));
                }
                else if (el.PropertyType == typeof(OrderStatus))
                {
                    el.SetValue(newClass, RandomEnum<OrderStatus>(rnd));
                }
                else if (el.PropertyType == typeof(TimeOnly))
                {
                    el.SetValue(newClass, new TimeOnly(rnd.Next(24), rnd.Next(60)));
                }
                else if (el.PropertyType == typeof(Payment))
                {
                    el.SetValue(newClass, RandomEnum<Payment>(rnd));
                }
                else if (el.PropertyType == typeof(ReservationStatus))
                {
                    el.SetValue(newClass, RandomEnum<Reservation>(rnd));
                }
                else if (el.PropertyType == typeof(bool))
                {
                    el.SetValue(newClass, rnd.NextDouble()); //generates bool upon conversion
                }
                else if (el.PropertyType == typeof(string))
                {
                    el.SetValue(newClass, GenerateString(el.Name, rnd));
                }
            }

            return newClass;
        }

        private DateTime RandomDateTime(Random rnd)
        {
            DateTime start = new DateTime(1995, 1, 1);
            int range = (DateTime.Today - start).Days;
            return start.AddDays(rnd.Next(range));
        }

        private object RandomEnum<T>(Random rnd)
        {
            var values = Enum.GetValues(typeof(T));
            var index = rnd.Next(values.Length);

            return (T)values.GetValue(index);
        }

        private string GenerateString(string name, Random rnd)
        {
            string? str;

            if (name.ToLower().Contains("email"))
            {
                str = "myEmail" + rnd.Next(100) + "@gmail.com";
            }
            else if (name.ToLower().Contains("phonenr"))
            {
                str = "86";
                for (int i = 0; i < 7; i++)
                {
                    str += rnd.Next(9);
                }
            }
            else
            {
                str = name + rnd.Next(200);
            }

            return str;
        }
    }
}
