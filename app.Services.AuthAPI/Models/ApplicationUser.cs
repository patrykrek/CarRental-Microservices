
using Microsoft.AspNetCore.Identity;


namespace app.Services.AuthAPI.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        
       
    }
}
