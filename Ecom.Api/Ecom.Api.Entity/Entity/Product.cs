using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Microsoft.AspNetCore.Http;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Api.Entity.Entity
{
    [Table("Product")]
    public class Product
    {
        public Product()
        {
            category = new HashSet<Category>();
        }

        [Key]
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public double price { get; set; }
        public int quantity { get; set; }
        public double? rating { get; set; }
        public int categoryId { get; set; }

        public string image { get; set; }

        public IEnumerable<Category> category { get; set; }

    }
}
