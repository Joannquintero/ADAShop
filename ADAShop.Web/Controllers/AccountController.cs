using ADAShop.Shared.DTOs;
using ADAShop.Shared.Emuns;
using ADAShop.Web.ViewModels.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ADAShop.Web.Controllers
{
    public class AccountController : Controller
    {
        public AccountController()
        {
        }

        [HttpGet]
        public IActionResult Register(bool IsAdmin = false)
        {
            ViewBag.IsAdmin = IsAdmin;
            return View("register");
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM model, bool isAdmin)
        {
            UserDTO User = new UserDTO
            {
                UserName = model.UserName,
                Identification = model.Identification,
                Name = model.Name,
                LastName = model.LastName,
                Address = model.Address,
                PhoneNumber = model.PhoneNumber,
                PasswordHash = model.Password,
                PasswordConfirm = model.ConfirmPassword,
                UserType = !isAdmin ? UserType.User : UserType.Admin
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