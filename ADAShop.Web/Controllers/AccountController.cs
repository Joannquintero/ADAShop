using ADAShop.Shared.DTOs;
using ADAShop.Shared.Emuns;
using ADAShop.Shared.Entities;
using ADAShop.Web.Models;
using ADAShop.Web.Services.Account;
using ADAShop.Web.ViewModels.Account;
using Microsoft.AspNetCore.Identity;
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
        public IActionResult Register(bool IsAdmin = false)
        {
            ViewBag.IsAdmin = IsAdmin;
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
            return View("login", model);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("ClaimsIdentityModel");
            return RedirectToAction("login");
        }

        [HttpPost]
        public async Task<IActionResult> Login_(RegisterVM model, bool isAdmin)
        {
            UserDTO User = new UserDTO
            {
                UserName = model.UserName,
                Identification = model.Document,
                Name = model.Name,
                LastName = model.LastName,
                Address = model.Address,
                PhoneNumber = model.PhoneNumber,
                PasswordHash = model.Password,
                PasswordConfirm = model.ConfirmPassword,
                UserType = !isAdmin ? UserTypeEnum.User : UserTypeEnum.Admin
            };

            if (ModelState.IsValid)
            {
                IdentityResult result = new IdentityResult();
                try
                {
                    //result = await userManager.CreateAsync(applicationUser, applicationUser.PasswordHash);
                }
                catch (Exception ex)
                {
                    if (ex.InnerException!.Message.StartsWith("Cannot insert duplicate key"))  // if modelstate is valid >> no thing can make excption except duplicate email >> this line for more verification
                        ModelState.AddModelError(string.Empty, "Already existing email");  // saeed : may cause bugs
                    else { ModelState.AddModelError(string.Empty, ex.InnerException.Message); }
                }

                if (result.Succeeded)
                {
                    switch (isAdmin)
                    {
                        //case true:
                        //    await userManager.AddToRoleAsync(applicationUser, "Admin");
                        //    return RedirectToAction("admins", "dashbourd");
                        //    break;

                        //default:
                        //    await userManager.AddToRoleAsync(applicationUser, "Admin");
                        //    if (User.IsInRole("Admin"))
                        //        return RedirectToAction("users", "dashbourd");
                        //    break;
                    }
                    return RedirectToAction("SendForceEmailConfirmationMail", "Mail", new { toEmail = model.UserName });
                }

                foreach (IdentityError err in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, err.Description);
                }
            }

            ViewBag.IsAdmin = isAdmin;
            return View("register");
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM model, bool isAdmin)
        {
            UserDTO User = new UserDTO
            {
                UserName = model.UserName,
                Identification = model.Document,
                Name = model.Name,
                LastName = model.LastName,
                Address = model.Address,
                PhoneNumber = model.PhoneNumber,
                PasswordHash = model.Password,
                PasswordConfirm = model.ConfirmPassword,
                UserType = !isAdmin ? UserTypeEnum.User : UserTypeEnum.Admin
            };

            if (ModelState.IsValid)
            {
                IdentityResult result = new IdentityResult();
                try
                {
                    //result = await userManager.CreateAsync(applicationUser, applicationUser.PasswordHash);
                }
                catch (Exception ex)
                {
                    if (ex.InnerException!.Message.StartsWith("Cannot insert duplicate key"))  // if modelstate is valid >> no thing can make excption except duplicate email >> this line for more verification
                        ModelState.AddModelError(string.Empty, "Already existing email");  // saeed : may cause bugs
                    else { ModelState.AddModelError(string.Empty, ex.InnerException.Message); }
                }

                if (result.Succeeded)
                {
                    switch (isAdmin)
                    {
                        //case true:
                        //    await userManager.AddToRoleAsync(applicationUser, "Admin");
                        //    return RedirectToAction("admins", "dashbourd");
                        //    break;

                        //default:
                        //    await userManager.AddToRoleAsync(applicationUser, "Admin");
                        //    if (User.IsInRole("Admin"))
                        //        return RedirectToAction("users", "dashbourd");
                        //    break;
                    }
                    return RedirectToAction("SendForceEmailConfirmationMail", "Mail", new { toEmail = model.UserName });
                }

                foreach (IdentityError err in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, err.Description);
                }
            }

            ViewBag.IsAdmin = isAdmin;
            return View("register");
        }
    }
}