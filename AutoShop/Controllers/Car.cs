using AutoShop.Models;
using AutoShop.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AutoShop.Controllers
{
    public class Car : Controller
    {
        private readonly ApplicationContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IWebHostEnvironment _appEnvironment;

        public Car(ApplicationContext context, UserManager<User> userManager, IWebHostEnvironment appEnvironment)
        {
            this._context = context;
            this._userManager = userManager;
            _appEnvironment = appEnvironment;
        }
        public IActionResult Statment()
        {

            return View();
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddStatment(CarCategory car, IFormFile uploadFile)
        {
            CarCategory Car = new CarCategory();
            Car.TypeCar = car.TypeCar;
            Car.CarModel = car.CarModel;
            Car.Year = car.Year;
            Car.Transmision = car.Transmision;
            Car.Wheel = car.Wheel;
            Car.Price = car.Price;
            Car.Description = car.Description;
            if (uploadFile != null)
            {
                string path = "/Files/" + uploadFile.FileName;
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await uploadFile.CopyToAsync(fileStream);
                }
                Car.CarPhotos = path;
            }
            Car.User = _context.Users.Where(u => u.Id == _userManager.GetUserId(User)).ToList().Last();
            _context.CarCategories.Add(Car);
            _context.SaveChanges();
            return RedirectToAction("Statment");
        }
        public async Task<IActionResult> DeleteStatmentAsync(int id)
        {
            var user = await _userManager.GetUserAsync(this.User);
            CarCategory removeStatment = new CarCategory();
            removeStatment.Id = id;
            _context.CarCategories.Remove(removeStatment);
            _context.SaveChanges();
            return RedirectToAction("MyStatment");
        }

        public IActionResult MyStatment()
        {
            CariInformationViewModel model = new CariInformationViewModel()
            {
                CarCategories = _context.CarCategories.Where(p => p.User.Id == _userManager.GetUserId(User)).ToList(),
            };
            return View(model);
        }
        [HttpPost]
        [Authorize]
        public IActionResult SearchCar(CarInfo carInformation)
        {
            CariInformationViewModel model = new CariInformationViewModel()
            {
                CarCategories = _context.CarCategories.Where(p =>
                p.Price >= carInformation.StartingPrice && p.Price <= carInformation.EndingPrice &&
                p.Year >= carInformation.StartingYear && p.Year <= carInformation.EndingYear
                && p.CarModel == carInformation.CarModel && p.TypeCar == carInformation.TypeCar
                && p.Transmision == carInformation.Transmision && p.Wheel == carInformation.Wheel).ToList()
            };
            return View(model);
        }
        public IActionResult ShopCar()
        {
            CariInformationViewModel model = new CariInformationViewModel()
            {
                CarCategories = _context.CarCategories.ToList()
            };
            return View(model);
        }
    }
}

