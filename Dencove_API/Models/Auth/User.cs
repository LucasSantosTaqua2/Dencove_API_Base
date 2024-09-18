using Microsoft.AspNetCore.Identity;

namespace Dencove_API.Models.Auth
{
    public class User : IdentityUser
    {
        public string CPF { get; set; }
    }
}
