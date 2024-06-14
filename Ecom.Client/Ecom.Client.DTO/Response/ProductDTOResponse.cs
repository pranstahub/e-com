using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Client.DTO.Response
{
    public class ProductDTOResponse
    {
        [Display(Name = "ID")]
        public int id { get; set; }
        [Display(Name = "Product")]

        public string name { get; set; }
        [Display(Name = "Description")]

        public string description { get; set; }
        [Display(Name = "Unit Price (₹)")]

        public float price { get; set; }
        [Display(Name = "Quantity")]

        public int quantity { get; set; }
        [Display(Name = "Rating")]

        public float? rating { get; set; }
        public int categoryId { get; set; }

        [Display(Name = "Image")]

        public string? image { get; set; }

        //public IEnumerable<CategoryDTOResponse> category { get; set; }


    }
}
