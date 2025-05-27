using Microsoft.AspNetCore.Identity;

namespace DAL.Entities;

public class User : IdentityUser
{
    public string FullName { get; set; }
}