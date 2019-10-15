using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication21.Dtos
{
    public class UserForLoginDto
    {
        [Required(ErrorMessage = "Nombre de Usuario Requerido")]
        public string Username { get; set; }
        [Required(ErrorMessage = "La contraseña es Requerida")]
        public string Password { get; set; }
    }
}
