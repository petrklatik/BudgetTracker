using BudgetTrackerAPI.Data;
using BudgetTrackerAPI.Dtos;
using BudgetTrackerAPI.Models;
using BudgetTrackerAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace BudgetTrackerAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly JwtService _jwtService;

        public AuthenticationController(IUserRepository userRepository, JwtService jwtService)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
        }

        [HttpPost("register")]
        public IActionResult Register(UserDto dto)
        {
            var user = new User()
            {
                Username = dto.Username,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
            };

            return Created("success", _userRepository.Create(user));
        }

        [HttpPost("login")]
        public IActionResult Login(UserDto dto)
        {
            var user = _userRepository.GetByUsername(dto.Username);

            if (user == null) return BadRequest(new { message = "Invalid credentials" });
            if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash)) return BadRequest(new { message = "Invalid credentials" });

            var jwt = _jwtService.Generate(user.Id);

            Response.Cookies.Append("jwt", jwt, new CookieOptions
            {
                HttpOnly = true
            });

            return Ok(new { message = "success" });
        }

        [HttpGet("user")]
        public IActionResult User()
        {
            try
            {
                var jwt = Request.Cookies["jwt"];
                if (jwt == null) return BadRequest(new { message = "Cookies error" });

                var token = _jwtService.Verify(jwt);
                int userId = int.Parse(token.Issuer);
                var user = _userRepository.GetById(userId);

                return Ok(user);
            }
            catch
            {
                return Unauthorized();
            }
        }

        [HttpDelete("delete")]
        public IActionResult Delete()
        {
            try
            {
                var jwt = Request.Cookies["jwt"];
                if (jwt == null) return BadRequest(new { message = "Cookies error" });

                var token = _jwtService.Verify(jwt);
                int userId = int.Parse(token.Issuer);

                var deletedUser = _userRepository.Delete(userId);
                if (deletedUser == null) return NotFound(new { message = "User not found" });

                Response.Cookies.Delete("jwt");
                return Ok(new { message = "success" });
            }
            catch
            {
                return Unauthorized();
            }
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("jwt");
            return Ok(new { message = "success" });
        }
    }
}
