using Microsoft.AspNetCore.Http;

namespace AutoShop.ViewModels
{
    public class PhotoViewModel
    {
        public string? Name { get; set; }
        public IFormFile? ProfilImage { get; set; }
    }
}
