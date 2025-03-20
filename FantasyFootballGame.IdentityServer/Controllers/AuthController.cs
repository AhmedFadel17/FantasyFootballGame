using FantasyFootballGame.IdentityServer.Data;
using FantasyFootballGame.IdentityServer.DTOs;
using FantasyFootballGame.IdentityServer.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CatalogServiceApi.IdentityServer.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        public AuthController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Create([FromBody] RegisterDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!Enum.IsDefined(typeof(UserRole), model.Role))
            {
                return BadRequest(new { Message = "Invalid role provided" });
            }

            var user = new ApplicationUser
            {
                UserName = model.Username,
                Email = model.Email,
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                string roleName = model.Role.ToString();
                await _userManager.AddToRoleAsync(user, roleName);
                await _userManager.AddClaimAsync(user, new Claim("role", roleName));
                return Ok(new { Message = "User registered successfully" });
            }

            return BadRequest(result.Errors);
        }


    }
}
