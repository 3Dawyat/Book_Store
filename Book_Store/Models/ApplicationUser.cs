using Microsoft.AspNetCore.Identity;
namespace Book_Store.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Pass { get; set; }
    }
}
