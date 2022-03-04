using Microsoft.AspNetCore.Identity;

namespace eCommerce.Security.Entities
{
    public class UserToken : IdentityUserToken<int>
    {
        public User User { get; set; }
    }
}