using Microsoft.AspNetCore.Identity;
using System.IO;

namespace AutoShop.Models
{
    public class User:IdentityUser
    {
        public int Year { get; set; }
        public ICollection<PhotoUser>? PhotoUsers { get; set; }
    }
}
