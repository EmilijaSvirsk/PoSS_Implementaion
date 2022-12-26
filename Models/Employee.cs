using System.Text.RegularExpressions;

namespace PSP_Komanda32_API.Models
{
    public class Employee
    {
        public int id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? Username { get; set; }
        public string Password { get; set; } = string.Empty;
        public int CreatedBy { get; set; }

        public static int CheckIfValid(Employee employee)
        {
            if (employee.id == 0 || employee.Name == null || employee.Surname == null || employee.Email == null || employee.Username == null || employee.Password == null || employee.CreatedBy == 0)
            {
                return 1;
            }

            if (!Regex.IsMatch(employee.id.ToString(), @"^[0-9]{1,8}$"))
            {
                System.Diagnostics.Debug.WriteLine(employee.Name + " regex failed");
                return 2;
            }

            if (!Regex.IsMatch(employee.Name, @"^[a-zA-Z]{3,20}$"))
            {
                System.Diagnostics.Debug.WriteLine(employee.Name + " regex failed");
                return 3;
            }

            if (!Regex.IsMatch(employee.Surname, @"^[a-zA-Z]{3,25}$"))
            {
                System.Diagnostics.Debug.WriteLine(employee.Surname + " regex failed");
                return 4;
            }

            if (!Regex.IsMatch(employee.Email, @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$"))
            {
                System.Diagnostics.Debug.WriteLine(employee.Email + " regex failed");
                return 5;
            }

            if (!Regex.IsMatch(employee.Username, @"^(?=[a-zA-Z0-9._]{8,20}$)(?!.*[_.]{2})[^_.].*[^_.]$"))
            {
                System.Diagnostics.Debug.WriteLine(employee.Username + " regex failed");
                return 6;
            }

            if (!Regex.IsMatch(employee.Password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,20}$"))
            {
                System.Diagnostics.Debug.WriteLine(employee.Password + " regex failed");
                return 7;
            }

            if (!Regex.IsMatch(employee.CreatedBy.ToString(), @"^[0-9]{1,8}$"))
            {
                System.Diagnostics.Debug.WriteLine(employee.CreatedBy + " regex failed");
                return 8;
            }

            return 0;
        }
    }
}
