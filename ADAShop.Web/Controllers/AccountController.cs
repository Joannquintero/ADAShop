using ADAShop.Shared.DTOs;
using ADAShop.Shared.Entities;
using ADAShop.Web.Models;
using ADAShop.Web.Services.Account;
using ADAShop.Web.ViewModels.Account;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.Json;

namespace ADAShop.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View(new LoginMV());
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View(new RegisterVM());
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginMV model)
        {
            if (ModelState.IsValid)
            {
                LoginDTO loginDTO = new LoginDTO
                {
                    UserName = model.UserName,
                    Password = model.Password
                };

                var loginResponse = await _accountService.LoginAsync(loginDTO);
                if (loginResponse != null)
                {
                    User user = await _accountService.GetAsync(model.UserName);
                    if (user != null)
                    {
                        ClaimsIdentity identity = new ClaimsIdentity(new List<Claim>
                            {
                                new Claim("UserName", $"{model.UserName}", ClaimValueTypes.String),
                                new Claim("UserId", $"{user.Id}", ClaimValueTypes.Integer64),
                                new Claim("Token", $"{loginResponse.Token}", ClaimValueTypes.String)
                            }, "UserIdentity");

                        HttpContext.User = new ClaimsPrincipal(identity);
                        ClaimsIdentityModel sessionDataModel = new ClaimsIdentityModel
                        {
                            UserId = user.Id,
                            UserName = user.UserName,
                            FullName = $"{user.Name} {user.LastName}",
                            Token = loginResponse.Token
                        };

                        var serializedRecords = JsonSerializer.Serialize(sessionDataModel);
                        HttpContext.Session.SetString("ClaimsIdentityModel", serializedRecords);
                        return RedirectToAction("Index", "Home");
                    }
                }
            }

            ModelState.AddModelError("", "Nombre de usuario o Contraseña no válido");
            return View("Login", model);
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("ClaimsIdentityModel");
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM model)
        {
            if (ModelState.IsValid)
            {
                UserDTO User = new UserDTO
                {
                    UserName = model.UserName,
                    Document = model.Document,
                    Name = model.Name,
                    LastName = model.LastName,
                    Address = model.Address,
                    PhoneNumber = model.PhoneNumber,
                    Password = model.Password,
                    PasswordConfirm = model.ConfirmPassword,
                    Email = model.UserName
                };

                var response = await _accountService.CreateAsync(User);
                if (response.Id > 0)
                {
                    var user = await _accountService.GetAsync(model.UserName);
                    await _accountService.AddToRoleUserAsync(user);

                    LoginDTO loginDTO = new LoginDTO
                    {
                        UserName = model.UserName,
                        Password = model.Password
                    };

                    var loginResponse = await _accountService.LoginAsync(loginDTO);
                    if (loginResponse != null && user != null)
                    {
                        ClaimsIdentityModel sessionDataModel = new ClaimsIdentityModel
                        {
                            UserId = user.Id,
                            UserName = user.UserName,
                            FullName = $"{user.Name} {user.LastName}",
                            Token = loginResponse!.Token
                        };

                        var serializedRecords = JsonSerializer.Serialize(sessionDataModel);
                        HttpContext.Session.SetString("ClaimsIdentityModel", serializedRecords);
                        return RedirectToAction("Index", "Home");
                    }
                }

                return View("Register", model);
            }
            return View("Register", model);
        }
    }
}