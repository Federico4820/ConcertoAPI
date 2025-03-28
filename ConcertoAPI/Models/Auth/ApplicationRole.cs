using Microsoft.AspNetCore.Identity;

namespace ConcertoAPI.Models.Auth;

public class ApplicationRole : IdentityRole
{
    public ICollection<ApplicationUserRole> UserRoles { get; set; }
}