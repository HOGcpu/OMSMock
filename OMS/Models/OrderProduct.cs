using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OMS.Models
{
    public class OrderProduct
    {
        [Key]
        public string GTIN { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public string SerialNumberType { get; set; }
        [Required]
        public List<string> SerialNumbers { get; set; } = new List<string>();
        [Required] 
        public int TemplateId { get; set; }
    }
}
