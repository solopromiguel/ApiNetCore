using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication21.Dtos
{
    public class UserDto
    {

    }
    public class UserForDetailedDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public string KnownAs { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastActive { get; set; }
        public string Introduction { get; set; }
        public string LookingFor { get; set; }
        public string Interests { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string PhotoUrl { get; set; }
        public ICollection<PhotosForDetailedDto> Photos { get; set; }
    }
    public class UserForListDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public string KnownAs { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastActive { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string PhotoUrl { get; set; }
        public string Url { get; set; }
    }
    public class UserForLoginDto
    {
        [Required(ErrorMessage = "Nombre de Usuario Requerido")]
        public string Username { get; set; }
        [Required(ErrorMessage = "La contraseña es Requerida")]
        public string Password { get; set; }
    }
    public class UserForRegisterDto
    {
        public UserForRegisterDto()
        {
            Created = DateTime.UtcNow;
            LastActive = DateTime.UtcNow;
        }

        [Required(ErrorMessage = "Nombre de Usuario Requerido")]
        public string Username { get; set; }

        [Required(ErrorMessage = "La contraseña es Requerida")]
        [StringLength(8, MinimumLength = 4, ErrorMessage = "You must specify password between 4 and 8 characters")]
        public string Password { get; set; }

        public DateTime Created { get; set; }

        public DateTime LastActive { get; set; }
    }
}
