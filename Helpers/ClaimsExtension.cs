using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Finshark_API.Helpers
{
    public static class ClaimsExtension
    {
        public static string GetUserId(this ClaimsPrincipal user)
        {
            return user.Claims.SingleOrDefault(x => x.Type.Equals("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname")).Value; 
        }
    }
}
