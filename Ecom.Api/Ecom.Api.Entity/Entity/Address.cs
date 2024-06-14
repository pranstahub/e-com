using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Api.Entity.Entity
{
    [Table("Address")]
    public class Address
    {
        [Key]
        public int id { get; set; }
        public string addressLine { get; set; }
        public string postalCode { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string  country { get; set; }
    }
}
