using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Api.Entity.Entity
{
    [Table("Customer")]
    public class Customer
    {
        public Customer()
        {
            address = new HashSet<Address>();
            payment = new HashSet<Payment>();
        }

        [Key]
        public int id { get; set; }
        public string name { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string? image { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/mm/yyyy}")]
        public DateTime customerSince { get; set; }

        public int? addressId { get; set; }
        public int? paymentId { get; set; }

        public ICollection<Address>? address { get; set; }
        public ICollection<Payment>? payment { get; set; }
    }
}
