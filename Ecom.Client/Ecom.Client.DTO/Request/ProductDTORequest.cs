using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Client.DTO.Request
{
    public  class ProductDTORequest
    {
        public int id { get; set; }
        [Required]
        [Display(Name ="Name")]
        public string name { get; set; }
        [Required]
        [Display(Name = "Description")]
        public string description { get; set; }
        [Required]
        [Display(Name = "Unit Price (₹)")]
        public float price { get; set; }
        [Required]
        [Display(Name = "Quantity")]

        public int quantity { get; set; }

        public float? rating { get; set; }
        [Required]
        [Display(Name = "Category")]
        public int categoryId { get; set; }

        [NotMapped]
        public IFormFile imageFile { get; set; }
        public string? image { get; set; }
    }
}
