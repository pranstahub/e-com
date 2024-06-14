using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Client.DTO.Request
{
    public class CartDTORequest
    {
        public int customerId { get; set; }
        public double? total { get; set; }
        public int? quantity { get; set; }
    }
}
