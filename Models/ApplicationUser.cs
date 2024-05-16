using Microsoft.AspNetCore.Identity;

namespace SafeGuardPro.Models;

public class ApplicationUser : IdentityUser
{
    public decimal Cpf{ get; set;}
}
