using System.ComponentModel.DataAnnotations;

using System.ComponentModel.DataAnnotations.Schema;
namespace PSP_Komanda32_API.Models
{
    public class OrderProducts
    {
        public int OrdersId { get; set; }
        public virtual Orders Orders { get; set; }
        public int ProductServiceId { get; set; }
        public virtual ProductService ProductService { get; set; }
        public int CostInCents { get; set; }
    }
}