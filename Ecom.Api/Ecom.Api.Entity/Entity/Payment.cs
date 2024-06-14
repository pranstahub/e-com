using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Api.Entity.Entity
{
    [Table("Payment")]
    public class Payment
    {
        [Key]
        public int id { get; set; }
        public string paymentType { get; set; }
        public string accountNumber { get; set; }
        public DateTime expiry { get; set; }
    }
}
