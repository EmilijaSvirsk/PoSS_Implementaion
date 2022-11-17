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

            foreach (var el in data)
            {
                if (el.Name == "id")
                {
                    el.SetValue(newClass, id ?? GenerateId());
                    continue;
                }


                if (el.PropertyType == typeof(int))
                {
                    el.SetValue(newClass, 10);
                }
                else if (el.PropertyType == typeof(double))
                {
                    el.SetValue(newClass, 10.0);
                }
                else if (el.PropertyType == typeof(DateTime))
                {
                    el.SetValue(newClass, new DateTime(100000000));
                }
                else if (el.PropertyType == typeof(Transport))
                {
                    el.SetValue(newClass, Transport.Car);
                }
                else if (el.PropertyType == typeof(OrderStatus))
                {
                    el.SetValue(newClass, OrderStatus.InProcess);
                }
                else if (el.PropertyType == typeof(TimeOnly))
                {
                    el.SetValue(newClass, new TimeOnly(10, 30));
                }
                else if (el.PropertyType == typeof(Payment))
                {
                    el.SetValue(newClass, Payment.Online);
                }
                else if (el.PropertyType == typeof(ReservationStatus))
                {
                    el.SetValue(newClass, ReservationStatus.Cancelled);
                }
                else if (el.PropertyType == typeof(bool))
                {
                    el.SetValue(newClass, true);
                }
                else if (el.PropertyType == typeof(string))
                {
                    el.SetValue(newClass, "test");
                }
            }

            return newClass;
        }

        private int GenerateId()
        {
            return 1;
        }
    }
}
