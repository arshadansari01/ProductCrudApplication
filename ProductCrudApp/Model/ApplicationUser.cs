using Microsoft.AspNetCore.Identity;

namespace ProductCrudApp.Model
{
    public class ApplicationUser:IdentityUser
    {
        public int Age { get; set; }
        public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }
    }

    public class ApplicationRole : IdentityRole
    {
        public string? RoleName { get; set; }
    }
    public class ApplicationUserRole : IdentityUserRole<string>
    {
        public virtual ApplicationUser User { get; set; }
        public virtual ApplicationRole Role { get; set; }
    }
}
