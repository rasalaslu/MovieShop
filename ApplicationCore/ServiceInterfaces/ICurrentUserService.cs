using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.ServiceInterfaces
{
    public interface ICurrentUserService
    {
        int? UserId { get; }
        bool IsAuthenticated { get; }
        string UserName { get; }
        string FullName { get; }
        string Email { get; }
        string RemoteIpAddress { get; }
        IEnumerable<Claim> GetClaimsIdentity();
        IEnumerable<string> Roles { get; }
        string ProfilePictureUrl { get; set; }
        bool IsAdmin { get; }
        bool IsSuperAdmin { get; }
    }
}
