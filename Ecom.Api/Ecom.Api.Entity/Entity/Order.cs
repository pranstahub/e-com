using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Api.Entity.Entity
{
    [Table("Order")]
    public class Order
    {
        [Key]
        public int id { get; set; }
        public int customerId { get; set; }
        public double? total { get; set; }
        public int? quantity { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/mm/yyyy}")]
        public DateTime? orderDate { get; set; }

        public Customer? customer { get; set; }
    }
}
