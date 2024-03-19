using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Finshark_API.Helpers
{
    public static class ClaimsExtension
    {
        public static string GetUserId(this ClaimsPrincipal user)
        {
            var email = user.FindFirst(ClaimTypes.NameIdentifier).Value;
            return email;
        }
    }
}
