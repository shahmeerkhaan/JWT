using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;


using Resume_Analyzer_Backend.Data.DTO;
using Resume_Analyzer_Backend.Services;
using Resume_Analyzer_Backend.Repository;
using Resume_Analyzer_Backend.Models;

namespace MyDemo.Api.Controllers
{
    [ApiController]
    [Route("api/Authenticate")]
    public class AuthController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<User> _repository;
        private readonly JwtService _jwtService;
        private readonly IConfiguration _configuration;

        public AuthController(IUnitOfWork unitOfWork, JwtService jwtService, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _repository = new UserManagement(_unitOfWork);
            _jwtService = jwtService;
            _configuration = configuration;
        }

        private LoginToken GenerateJwtToken(User user)
        {
            var secToken = _jwtService.GetToken(user);
            var jwt = new JwtSecurityTokenHandler().WriteToken(secToken);
            var expiresSec = Convert.ToInt32(_configuration["JwtSettings:ExpiryMinutes"]);

            return new LoginToken
            {
                access_token = jwt,
                user_id = user.Id.ToString(),
                expires_in = expiresSec,
                token_type = "Bearer"
            };
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDTO loginRequest)
        {
            try
            {
                var users = await _repository.GetItemWithConditionAsync(
                    u => u.Email == loginRequest.UserName_Email && u.Password == loginRequest.Password);

                var user = users.SingleOrDefault();
                if (user == null)
                {
                    return Unauthorized(new { message = "Invalid credentials." });
                }

                var tokenResponse = GenerateJwtToken(user);
                return Ok(tokenResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [Authorize]
        [HttpGet("Get")]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var users = await _repository.GetAllAsync();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [Authorize]
        [HttpPost("Create")]
        public async Task<IActionResult> CreateUser(User user)
        {
            try
            {
                var res = await _repository.CreateAsync(user);
                return Ok(new {
                    added_Record = res,
                    updated_Table = await _repository.GetAllAsync()
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
    }
}
