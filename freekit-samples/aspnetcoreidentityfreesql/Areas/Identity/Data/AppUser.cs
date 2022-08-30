using FreeSql.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace aspnetcoreidentityfreesql.Areas.Identity.Data;

// Add profile data for application users by adding properties to the ApplicationUser class
public class AppUser : IdentityUser<Guid>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
}