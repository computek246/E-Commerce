using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace eCommerce.Security.Entities
{
    public class Role : IdentityRole<int>
    {
        public Role()
        {
            UserRoles = new HashSet<UserRole>();
            RoleClaims = new HashSet<RoleClaim>();
        }

        public ICollection<UserRole> UserRoles { get; set; }
        public ICollection<RoleClaim> RoleClaims { get; set; }
    }
}