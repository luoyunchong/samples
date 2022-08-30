using Microsoft.AspNetCore.Identity;

namespace aspnetcore_identity_freesql.Models
{
    public class AppUser : IdentityUser<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
