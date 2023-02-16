using AntroStop.DAL.Entities;
using AntroStop.Domain.Base.AuthModels;
using AntroStop.Interfaces.Base.Repositories;
using AntroStop.Interfaces.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AntroStop.WebAPI.Controllers.Base
{
    [Route("api/[controller]")]
    [ApiController]
    public class MappedAuthController: ControllerBase
    {
        
        private readonly IConfiguration configuration;
        private readonly IConfigurationSection jwtSettings;
        private readonly IUsersRepository<User> repository;

        public MappedAuthController(IUsersRepository<User> users, IConfiguration configuration)
        {
            this.repository = users;
            this.configuration = configuration;
            this.jwtSettings = this.configuration.GetSection("JwtSettings");
        }

        [HttpPost("Registration")]
        public async Task<IActionResult> RegisterUser([FromBody] UserForRegistrationDto userForRegistration)
        {
            List<string> errors = new List<string>();

            if (userForRegistration == null || !ModelState.IsValid)
            {
                errors.Add("Не удалось создать пользователя!");
                return BadRequest(new RegistrationResponseDto { Errors = errors, IsSuccessfulRegistration = false });
            }
                

            var user = new User
            {
                ID = userForRegistration.Id,
                Name = userForRegistration.Name,
                Password = userForRegistration.Password,
                RoleID = 3,
                Status = true
            };

            var result = await repository.Add(user);
            if (result == null)
            {
                errors.Add("Пользователь с таким E-mail уже существует");
                return BadRequest(new RegistrationResponseDto { Errors = errors, IsSuccessfulRegistration = false });
            }
                
            

            return StatusCode(201);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserForAuthenticationDto userForAuthentication)
        {
            var user = await repository.GetByData(userForAuthentication.Id, userForAuthentication.Password);

            if (user == null)
                return BadRequest(new AuthResponseDto { ErrorMessage = "Неверный E-mail или пароль", IsAuthSuccessful=false });
            

            if (!user.Status)
                return BadRequest(new AuthResponseDto { ErrorMessage = "Ваш профиль заблокирован", IsAuthSuccessful = false });

            var signingCredentials = GetSigningCredentials();
            var claims = GetClaims(user);
            var tokenOptions = GenerateTokenOptions(signingCredentials, claims);
            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return Ok(new AuthResponseDto { IsAuthSuccessful = true, Token = token, ID = user.ID, Name = user.Name });
        }

        private SigningCredentials GetSigningCredentials()
        {
            var key = Encoding.UTF8.GetBytes(jwtSettings.GetSection("securityKey").Value);
            var secret = new SymmetricSecurityKey(key);

            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        private List<Claim> GetClaims(User user)
        {
            var claims = new List<Claim>
            {
                new Claim("userID", user.ID),
                new Claim("UserName", user.Name),
                new Claim("CreatedAt",user.CreatedAt.ToString()),
                new Claim("UserRoleName", user.Role.Name),
                new Claim(ClaimTypes.Role, user.Role.Name)
            };

            return claims;
        }

        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            
            var tokenOptions = new JwtSecurityToken(
                issuer: jwtSettings.GetSection("validIssuer").Value,
                audience: jwtSettings.GetSection("validAudience").Value,
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings.GetSection("expiryInMinutes").Value)),
                signingCredentials: signingCredentials);

            return tokenOptions;
        }
    }
}
