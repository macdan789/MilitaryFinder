using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using MilitaryFinder.API.Domain;
using MilitaryFinder.API.Services.Abstract;
using MilitaryFinder.API.Settings;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MilitaryFinder.API.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<IdentityUser> _repository;
        private readonly JwtSettings _settings;

        public IdentityService(UserManager<IdentityUser> repository, JwtSettings settings)
        {
            _repository = repository;
            _settings = settings;
        }

        public async Task<AuthentificationResult> LoginAsync(string emailAddress, string password)
        {
            var user = await _repository.FindByEmailAsync(emailAddress);

            if (user is null)
            {
                return new AuthentificationResult
                {
                    Token = string.Empty,
                    Success = false,
                    ErrorMessages = new[] { "Such user does not exist. Please register first." }
                };
            }

            var passwordIsValid = await _repository.CheckPasswordAsync(user, password);

            if(!passwordIsValid)
            {
                return new AuthentificationResult
                {
                    Token = string.Empty,
                    Success = false,
                    ErrorMessages = new[] { "Password is invalid." }
                };
            }

            return GenerateAuthtantificationResultForUser(user);
        }

        public async Task<AuthentificationResult> RegisterAsync(string emailAddress, string password)
        {
            var existingUser = await _repository.FindByEmailAsync(emailAddress);

            if(existingUser is not null)
            {
                return new AuthentificationResult
                {
                    Token = string.Empty,
                    Success = false,
                    ErrorMessages = new[] { "User already exists." }
                };
            }

            var createdUser = new IdentityUser
            {
                Email = emailAddress,
                UserName = emailAddress
            };

            //Hash the password for user
            var result = await _repository.CreateAsync(createdUser, password);

            if(!result.Succeeded)
            {
                return new AuthentificationResult
                {
                    Token = string.Empty,
                    Success = false,
                    ErrorMessages = result.Errors.Select(x => x.Description)
                };
            }

            return GenerateAuthtantificationResultForUser(createdUser);
        }

        private AuthentificationResult GenerateAuthtantificationResultForUser(IdentityUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_settings.Secret);
            //Describe what the user token should have
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim("id", user.Id)
                }),
                //Token will expire in 2 hours after creation
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new AuthentificationResult
            {
                Token = tokenHandler.WriteToken(token),
                Success = true,
                ErrorMessages = Enumerable.Empty<string>()
            };
        }
    }
}
