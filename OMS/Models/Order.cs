using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OMS.Models
{
    public class Order
    {
        [Key]
        public string OmsId { get; set; }
        [Required]
        public List<OrderProduct> Products { get; set; } = new List<OrderProduct>();
        public OrderDetails OrderDetails { get; set; }
    }
}
