using ADAShop.Api.Helpers;
using ADAShop.Shared.DTOs;
using ADAShop.Shared.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ADAShop.Api.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class AccountsController : ControllerBase
    {
        private readonly IUserHelper _userHelper;
        private readonly IConfiguration _configuration;

        public AccountsController(IUserHelper userHelper, IConfiguration configuration)
        {
            _userHelper = userHelper;
            _configuration = configuration;
        }

        [HttpGet(nameof(Get))]
        public async Task<ActionResult> Get(string userName)
        {
            return Ok(await _userHelper.GetUserAsync(userName));
        }

        [HttpPost(nameof(IsUserInRole))]
        public async Task<ActionResult> IsUserInRole(UserRoleDTO userRoleDTO)
        {
            var user = await _userHelper.GetUserAsync(userRoleDTO.user!.UserName!);
            bool isUserInRole = await _userHelper.IsUserInRoleAsync(user, userRoleDTO.roleName!);
            return Ok(isUserInRole);
        }

        [HttpPost(nameof(CreateUser))]
        public async Task<ActionResult> CreateUser([FromBody] UserDTO model)
        {
            User user = model;
            var result = await _userHelper.AddUserAsync(user, model.Password);
            if (result.Succeeded)
            {
                return Ok(user);
            }

            return BadRequest(result.Errors.FirstOrDefault());
        }

        [HttpPost(nameof(Login))]
        public async Task<ActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            var result = await _userHelper.LoginAsync(loginDTO);
            if (result.Succeeded)
            {
                var user = await _userHelper.GetUserAsync(loginDTO.UserName);
                return Ok(await BuildToken(user));
            }

            if (result.IsLockedOut)
            {
                return BadRequest("Ha superado el máximo número de intentos, su cuenta está bloqueada, intente de nuevo en 5 minutos.");
            }

            if (result.IsNotAllowed)
            {
                return BadRequest("El usuario no ha sido habilitado, debes de seguir las instrucciones del correo enviado para poder habilitar el usuario.");
            }

            return BadRequest("Email o contraseña incorrectos.");
        }

        [HttpPost(nameof(AddUserToRole))]
        public async Task<ActionResult> AddUserToRole(UserRoleDTO userRoleDTO)
        {
            await _userHelper.AddUserToRoleAsync(userRoleDTO.user!, userRoleDTO.roleName!);
            return Ok();
        }

        private async Task<TokenDTO> BuildToken(User user)
        {
            var role = await _userHelper.GetRolesUserAsync(user);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email!),
                new Claim(ClaimTypes.Role, role.FirstOrDefault()!),
                new Claim("Name", user.Name),
                new Claim("LastName", user.LastName),
                new Claim("Address", user.Address!),
                new Claim("FullName", user.FullName)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["jwtKey"]!));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.UtcNow.AddDays(30);
            var token = new JwtSecurityToken(
                issuer: null,
                audience: null,
                claims: claims,
                expires: expiration,
                signingCredentials: credentials);

            return new TokenDTO
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration
            };
        }
    }
}