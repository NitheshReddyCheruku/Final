using Microsoft.AspNetCore.Identity;

namespace GrowthPath.AuthAPI.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
    }
}
