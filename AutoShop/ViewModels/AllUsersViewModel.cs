using Microsoft.AspNetCore.Http;
using AutoShop.Models;
namespace AutoShop.ViewModels

{
    public class AllUsersViewModel
    {
        public ICollection<User>? Users { get; set; }  

        public ICollection<PhotoUser>? Photos { get; set;}

        public ICollection<UserInformation>? UserInformations { get; set; }  

    }
}
