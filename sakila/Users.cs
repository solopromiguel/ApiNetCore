using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace WebApplication21.sakila
{
    public partial class Users:IdentityUser<int>
    {
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string KnownAs { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastActive { get; set; }
        public string Introduction { get; set; }
        public string LookingFor { get; set; }
        public string Interests { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        public ICollection<UserRole> UserRoles { get; set; }
        public ICollection<Photo> Photos { get; set; }

    }
}
