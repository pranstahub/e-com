using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Api.Entity.Entity
{
    [Table("CartItem")]
    public class CartItem
    {
        [Key]
        public int id { get; set; }
        public int cartId { get; set; }
        public int productId { get; set; }
        public int quantity { get; set; }
        public double price { get; set; }
        //public string image { get; set; }
        public double? itemTotal { get; set; }


        public Cart? cart { get; set; }
        public Product? product { get; set; }

    }
}
