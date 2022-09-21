using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs
{
    public class SignUpViewModel
    {
        [Required(ErrorMessage = "Required")]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Required")]
        [Display(Name = "Email")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Required")]
        [Display(Name = "Email")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Required")]
        [Display(Name = "Email")]
        [Compare("Password")]
        [DataType(DataType.Password)]
        public string RePassword { get; set; }

    }
}
