using System.ComponentModel.DataAnnotations;
using System;

namespace OMS.Models
{
    public class OrderDetails
    {
        [Key]
        public string FactoryId { get; set; }
        public string FactoryName { get; set; }
        public string FactoryAddress { get; set; }
        [Required]
        public string FactoryCountry { get; set; }
        [Required]
        public string ProductionLineId { get; set; }
        [Required]
        public string ProductCode { get; set; }
        [Required]
        public string ProductDescription { get; set; }
        public string PoNumber { get; set; }
        public DateTime? ExpectedStartDate { get; set; }
    }
}
