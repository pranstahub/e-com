using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Api.Entity.Entity
{
    [Table("Rating")]
    public class Rating
    {
        [Key]
        public int id { get; set; }
        public int productId { get; set; }
        public int customerId { get; set; }
        public int rating { get; set; }

        public Customer customer { get; set; }
        public Product product { get; set; }
    }
}
