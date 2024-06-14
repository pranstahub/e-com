using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Client.DTO.Request
{
    public class OrderItemDTORequest
    {
        public int orderId { get; set; }
        public int productId { get; set; }
        public int quantity { get; set; }
        public double price { get; set; }
        public double? itemTotal { get; set; }
        public ProductDTORequest? product { get; set; }
    }
}
