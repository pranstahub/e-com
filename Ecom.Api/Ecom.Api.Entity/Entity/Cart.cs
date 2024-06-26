﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Api.Entity.Entity
{
    [Table("Cart")]
    public class Cart
    {
        [Key]
        public int id { get; set; }
        public int customerId { get; set; }
        public double? total { get; set; }
        public int? quantity { get; set; }

        public Customer customer { get; set; }
    }
}
