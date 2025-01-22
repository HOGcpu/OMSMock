using System.ComponentModel.DataAnnotations;
using System;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace OMS.Models
{
    [Owned]
    public class OrderDetails
    {
        [Required]
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
        public string ExpectedStartDate { get; set; }
        public string OmsId { get; set; }
    }
}
