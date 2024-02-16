using Microsoft.AspNetCore.Identity;

namespace IdentityDemo2.Models
{
    public enum ACTIVATION_STATUS { 
        ACTIVE, DEACTIVE
    } 
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }

        public ACTIVATION_STATUS sTATUS { get; set; } = ACTIVATION_STATUS.DEACTIVE;

        //IndentityUserId

    }
}
