using Microsoft.AspNetCore.Identity;

namespace IdentityDemo2.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }

        //IndentityUserId

    }
}
