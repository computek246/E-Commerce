using Microsoft.AspNetCore.Identity;

namespace eCommerce.Security.Entities
{
    public class RoleClaim : IdentityRoleClaim<int>
    {
        public Role Role { get; set; }
    }
}