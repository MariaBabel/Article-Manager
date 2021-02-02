using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using article_manager.api.Repositories;
using backendlib.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace article_manager.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ICRUDRepository<User> _userRepository;
        private readonly IConfiguration _configuration;
        private readonly SignInManager<IdentityUser> _signInManager;

        public UserController(ICRUDRepository<User> userRepository, IConfiguration configuration,
                           SignInManager<IdentityUser> signInManager)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _signInManager = signInManager;
        }

        [HttpGet]
        public async Task<ActionResult> GetUsersAsync()
        {
            return Ok(await _userRepository.GetList());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetUserAsync(int id)
        {
            return Ok(await _userRepository.Get(id));
        }

        // [HttpPost("Login")]
        // public async Task<ActionResult> Login([FromBody] User user)
        // {
        //     string token = "abcd";
        //     return Ok(token);
        // }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] User user)
        {
            // var result = await _signInManager.PasswordSignInAsync(user.Name, user.Password, false, false);

            // if (!result.Succeeded) return BadRequest("Username and password are invalid.");

            user.Id = 1;

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Id.ToString(), "auth")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSecurityKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiry = DateTime.Now.AddDays(Convert.ToInt32(_configuration["JwtExpiryInDays"]));

            var token = new JwtSecurityToken(
                _configuration["JwtIssuer"],
                _configuration["JwtAudience"],
                claims,
                expires: expiry,
                signingCredentials: creds
            );

            return Ok(new JwtSecurityTokenHandler().WriteToken(token));
        }
    }
}