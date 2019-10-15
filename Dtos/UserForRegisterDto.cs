using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication21.Dtos
{
    public class UserForRegisterDto
    {
        public UserForRegisterDto()
        {
            Created = DateTime.UtcNow;
            LastActive = DateTime.UtcNow;
        }

        [Required (ErrorMessage ="Nombre de Usuario Requerido")]
        public string Username { get; set; }

        [Required(ErrorMessage = "La contraseña es Requerida")]
        [StringLength(8, MinimumLength = 4, ErrorMessage = "You must specify password between 4 and 8 characters")]
        public string Password { get; set; }

        //[Required]
        //public string Gender { get; set; }

        //[Required]
        //public string KnownAs { get; set; }

        //[Required]
        //public DateTime DateOfBirth { get; set; }

        //[Required]
        //public string City { get; set; }

        //[Required]
        //public string Country { get; set; }

        public DateTime Created { get; set; }

        public DateTime LastActive { get; set; }
    }
}
