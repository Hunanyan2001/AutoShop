using AutoShop.Models;
using AutoShop.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using AutoShop.ViewModels;
using AutoShop.Models;
using System.Security.Claims;
using System.IO;
using Microsoft.CodeAnalysis;
using System.Security.Principal;

namespace AutoShop.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ApplicationContext _context;
        private readonly IWebHostEnvironment _appEnvironment;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, IWebHostEnvironment appEnvironment,
            ApplicationContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _appEnvironment = appEnvironment;
            _context = context;
        }
        [HttpGet]
        [HttpPost]

        public IActionResult PageUser()
        {
            var currentUserId = _userManager.GetUserId(User);
            AllUsersViewModel model = new AllUsersViewModel()
            {
                Users = _context.Users.ToList(),
                Photos = _context.Photos.Where(p => p.UserId == currentUserId).ToList(),
                UserInformations = _context.UserInformations.ToList(),
            };

            return View(model);
        }
        

        public IActionResult ChangeInformation()
        {
            var current = _userManager.GetUserId(User);
            var friend = _context.UserInformations.Find(current);
            if (friend == null)
            {
                return NotFound();
            }
            _context.UserInformations.Remove(friend);
            _context.SaveChanges();
            return RedirectToAction("PageUser");
        }

        [HttpPost]
        public async Task<IActionResult>? UserInf(UserInformation informatiom)
        {
            var currentUserId = _userManager.GetUserId(User);
            UserInformation user = new UserInformation();
            user.Id = currentUserId;
            user.FirstName = informatiom.FirstName;
            user.LastName = informatiom.LastName;
            user.UserName = informatiom.UserName;
            user.Email = informatiom.Email;
            user.Address = informatiom.Address;
            user.Address2 = informatiom.Address2;
            user.Country = informatiom.Country;
            user.State = informatiom.State;
            user.Zip = informatiom.Zip;
            user.CreditCardNumber = informatiom.CreditCardNumber;
            user.CVV = informatiom.CVV;
            user.Experiation = informatiom.Experiation;
            user.PaymentMethod = informatiom.PaymentMethod;
            user.PhoneNumber = informatiom.PhoneNumber;
            user.NameOnCard = informatiom.NameOnCard;
            _context.UserInformations.Add(user);
            _context.SaveChanges();
            return RedirectToAction("PageUser");
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> PhotoUser(IFormFile uploadedFile)
        {
            if (uploadedFile != null)
            {
                // путь к папке Files
                string path = "/Files/" + uploadedFile.FileName;
                // сохраняем файл в папку Files в каталоге wwwroot
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream);
                }
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                PhotoUser file = new PhotoUser { Name = uploadedFile.FileName, Path = path, UserId = userId };
                _context.Photos.Add(file);
                _context.SaveChanges();
            }
            return RedirectToAction("PageUser");
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]

        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = new User { Email = model.Email, UserName = model.Email, Year = model.Year };
                // добавляем пользователя
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    // установка куки
                    await _signInManager.SignInAsync(user, false);
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    // проверяем, принадлежит ли URL приложению
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("PageUser", "Account");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Неправильный логин и (или) пароль");
                }
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Logout()
        {
            // удаляем аутентификационные куки
            
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
