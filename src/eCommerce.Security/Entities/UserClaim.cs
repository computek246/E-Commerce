using Microsoft.AspNetCore.Identity;

namespace eCommerce.Security.Entities
{
    public class UserClaim : IdentityUserClaim<int>
    {
        public User User { get; set; }
    }
}