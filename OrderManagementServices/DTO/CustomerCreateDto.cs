using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementServices.DTO
{
    public class CustomerCreateDto
    {
        [Required]
        public string CustomerName { get; set; }
        [Required]
        [RegularExpression(@"^[\w-]+(\.[\w-]+)*@([\w-]+\.)+[a-zA-Z]{2,7}$", ErrorMessage = "Email format is invalid.")]
        public string Email { get; set; }
    }
}
