using Finshark_API.Models;

namespace Finshark_API.Interfaces
{
    public interface ITokenInterface
    {
        string GenerateToken(AppUser user);
    }
}
