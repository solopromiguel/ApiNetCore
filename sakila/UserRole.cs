using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication21.sakila
{
    public class UserRole: IdentityUserRole<int>
    {
        public Users User { get; set; }
        public Role Roles { get; set; }

    }
}
