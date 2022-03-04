using System;
using System.Security.Claims;
using System.Security.Principal;

namespace eCommerce.Helper.ExtensionMethod
{
    public static class PrincipalExtension
    {
        public static TResult GetFromClaims<TResult>(this IPrincipal principal, string claimsName)
        {
            var value = ((ClaimsPrincipal) principal).FindFirst(x => x.Type == claimsName);
            if (value == null) return default;

            try
            {
                return (TResult) Convert.ChangeType(value.Value, typeof(TResult));
            }
            catch (Exception)
            {
                return default;
            }
        }
    }
}