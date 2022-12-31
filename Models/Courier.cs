using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace PSP_Komanda32_API.Models
{
    public class Courier
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? Username { get; set; }
        public string Password { get; set; } = string.Empty;
        public int CreatedBy { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
        public Transport Transportation { get; set; }

        public static string CheckIfValid(Courier courier)
        {
            if (courier.Name == null || courier.Surname == null || courier.Email == null || courier.Username == null || courier.Password == null || courier.CreatedBy == 0 || courier.PhoneNumber == null)
            {
                return "values of Courier cannot be null";
            }

            if (!Regex.IsMatch(courier.Name, @"^[a-zA-Z]{3,20}$"))
            {
                System.Diagnostics.Debug.WriteLine(courier.Name + " regex failed");
                return "invalid Courier Id";
            }

            if (!Regex.IsMatch(courier.Surname, @"^[a-zA-Z]{3,25}$"))
            {
                System.Diagnostics.Debug.WriteLine(courier.Surname + " regex failed");
                return "invalid Courier Surname";
            }

            if (!Regex.IsMatch(courier.Email, @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$"))
            {
                System.Diagnostics.Debug.WriteLine(courier.Email + " regex failed");
                return "invalid Courier Email";
            }

            if (!Regex.IsMatch(courier.Username, @"^(?=[a-zA-Z0-9._]{8,20}$)(?!.*[_.]{2})[^_.].*[^_.]$"))
            {
                System.Diagnostics.Debug.WriteLine(courier.Username + " regex failed");
                return "invalid Courier Username";
            }

            if (!Regex.IsMatch(courier.Password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,20}$"))
            {
                System.Diagnostics.Debug.WriteLine(courier.Password + " regex failed");
                return "invalid Courier Password";
            }

            if (!Regex.IsMatch(courier.CreatedBy.ToString(), @"^[0-9]{1,8}$"))
            {
                System.Diagnostics.Debug.WriteLine(courier.CreatedBy + " regex failed");
                return "invalid Courier CreatedBy";
            }

            if (!Regex.IsMatch(courier.PhoneNumber, @"^[0-9]{11}$"))
            {
                System.Diagnostics.Debug.WriteLine(courier.PhoneNumber + " regex failed");
                return "invalid Courier PhoneNurmber";
            }

            if (!Regex.IsMatch(courier.Transportation.ToString(), @"\b(Scooter|Car|Bicycle)\b"))
            {
                System.Diagnostics.Debug.WriteLine(courier.Transportation + " regex failed");
                return "invalid Courier Transportation";
            }

            return "Ok";
        }
    }
}
