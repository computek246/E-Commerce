using System.Security.Claims;
using System.Threading.Tasks;
using eCommerce.Common.Constant;
using eCommerce.Security.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace eCommerce.Security.Implementations
{
    public class AdditionalUserClaimsPrincipalFactory
        : UserClaimsPrincipalFactory<User, Role>
    {
        private readonly UserManager<User> _userManager;

        public AdditionalUserClaimsPrincipalFactory(
            UserManager<User> userManager,
            RoleManager<Role> roleManager,
            IOptions<IdentityOptions> optionsAccessor)
            : base(userManager, roleManager, optionsAccessor)
        {
            _userManager = userManager;
        }

        public override async Task<ClaimsPrincipal> CreateAsync(User user)
        {
            var principal = await base.CreateAsync(user);
            if (principal.Identity is not ClaimsIdentity identity) return principal;

            identity.AddClaim(new Claim(AppValues.AppClaims.Id, user.Id.ToString()));
            identity.AddClaim(new Claim(AppValues.AppClaims.FullName, user.FullName));

            return principal;
        }
    }
}