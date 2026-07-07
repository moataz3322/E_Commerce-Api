using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Dtos.Identity
{
   public class LoginDto
    {
        [Required,EmailAddress]
        public string Email { get; set; } = default!;
        [Required]
        public string Passowrd { get; set; } = default!;
    }
}
