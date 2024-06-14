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
    public class CustomerDTORequest
    {
        [Required]
        public string name { get; set; }

        [Required]
        [MaxLength(10, ErrorMessage = "Number exceeds 10 Digits!")]
        public string phone { get; set; }
        public string? email { get; set; }

        [NotMapped]
        public IFormFile imageFile { get; set; }
        public string? image { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/mm/yyyy}")]
        public DateTime customerSince { get; set; }

        public int? addressId { get; set; }
        public int? paymentId { get; set; }
    }
}
