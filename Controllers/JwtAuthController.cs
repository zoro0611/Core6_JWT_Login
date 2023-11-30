using Core6_JWT_Login.Models;
using Core6_JWT_Login.Models.DBModels;
using Core6_JWT_Login.Repos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Core6_JWT_Login.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JwtAuthController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IRepository<User> _userRepos;

        public JwtAuthController(IConfiguration config, IRepository<User> userRepos)
        {
            _config = config;
            _userRepos = userRepos;
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login([FromBody] LoginViewModel loginVM)
        {
            //_logger.LogWarning(2001, DateTime.Now.ToLongTimeString() + " JwtAuth控制器POST方法被呼叫");

            IActionResult response = Unauthorized();

            var user = AuthenticateUser(loginVM);

            if (user is not null)
            {
                var token = GenerateJsonWebToken(user);
                //將token存入Db
                user.JwtToken = token;
                _userRepos.Update(user);
                response = Ok(token);
            }

            return response;
        }




        private User? AuthenticateUser(LoginViewModel loginVM)
        {
            
            User? user = _userRepos.GetAll().SingleOrDefault(x => x.UserId == loginVM.Username && x.Password == loginVM.Password);

            return user;
        }

        //生成JWT Token
        private string GenerateJsonWebToken(User userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim("UserId", userInfo.UserId), // You can replace "UserId" with an appropriate claim type
                new Claim("Username", userInfo.Username), // Add other user-related claims as needed
            };

            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);

            string jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

            return jwtToken;
        }
    }
}
