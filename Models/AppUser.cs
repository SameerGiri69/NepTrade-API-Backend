using Microsoft.AspNetCore.Identity;

namespace Finshark_API.Models
{
    public class AppUser : IdentityUser
    {
        public List<Portfolio> Portfolios { get; set; }
    }
}
