using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Test.Application.Abstract.Services._Log;
using Test.Application.Dto;
using Test.Domain.Entites;

namespace Test.Application.Features.Query._User
{
    public class UserForAuthenticationQueryHandler : IRequestHandler<UserForAuthenticationQueryRequest, UserForAuthenticationQueryRespons>
    {
        private readonly ILoggerService logger;
        private readonly IMapper mapper;
        private readonly UserManager<User> userManager;
        private readonly IConfiguration configuration;

        private User? user;
        public UserForAuthenticationQueryHandler(ILoggerService logger, IMapper mapper, UserManager<User> userManager, IConfiguration configuration)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.userManager = userManager;
            this.configuration = configuration;
        }

        public async Task<UserForAuthenticationQueryRespons> Handle(UserForAuthenticationQueryRequest request, CancellationToken cancellationToken)
        {
            user = await userManager.FindByNameAsync(request.UserName);
            var result = (user != null && await userManager.CheckPasswordAsync(user, request.Password)); //boyle bır kullanıcı varmı var ve sıfre de dogru ıse true dön

            if (!result)
            {
                logger.LogWarning($"{request.UserName}:böyle bir kullanıcı yok veya şifreniz yanlış");
                return new UserForAuthenticationQueryRespons()
                {
                    Success = false,
                    Token = null
                };
            }
            //Token üretme işlemi burada olucak 

            var token = await CreateToken(true);

            return new UserForAuthenticationQueryRespons()
            {
                Success = true,
                Token = token
            };
        }
        public async Task<TokenDto> CreateToken(bool populateExp)
        {
            var signinCredentials = GetSiginCredentatials(); //kımlık bılgılerı aldık 
            var claims = await GetClaims();//rolelr hak ıdda ları aldık 
            var tokenOptions = GenerateTokenOpyions(signinCredentials, claims); //token olusturma optionslarını generate ettık 


            var refreshToken = GenerateRefreshToken();
            user.RefreshToken = refreshToken;

            if (populateExp)//
            {
                user.RefreshTokenExpiryTie = DateTime.Now.AddDays(7);
            }
            await userManager.UpdateAsync(user);

            var accessToken = new JwtSecurityTokenHandler().WriteToken(tokenOptions); //token olusturldu
            return new TokenDto()
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }

        private JwtSecurityToken GenerateTokenOpyions(SigningCredentials signinCredentials, List<Claim> claims)
        {
            var jwtSettings = configuration.GetSection("JwtSettings");

            var tokenOptions = new JwtSecurityToken(
                issuer: jwtSettings["validIssure"],
                audience: jwtSettings["validAudience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["expaires"])),
                signingCredentials: signinCredentials);
            return tokenOptions;
        }

        private async Task<List<Claim>> GetClaims()
        {
            //user nameyı ekledik 
            var claims = new List<Claim>() {
            new Claim(ClaimTypes.Name,user.UserName)
            };
            //rolleri aldık
            var roles = await userManager.GetRolesAsync(user);
            //rolleri claims e ekledik
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            return claims;
        }

        private SigningCredentials GetSiginCredentatials()
        {
            var jwtSettings = configuration.GetSection("JwtSettings");
            var key = Encoding.UTF8.GetBytes(jwtSettings["secretKey"]);
            var secret = new SymmetricSecurityKey(key);
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }
        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var jwtSettings = configuration.GetSection("JwtSettings");
            var secretKey = jwtSettings["secretKey"];

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true, //uretıcıyı dogrula
                ValidateAudience = true, //gecerlı bır alıcımı onu dogrula
                ValidateLifetime = true,//gecerlılıgını usre dogrula
                ValidateIssuerSigningKey = true,//anahtarı dogrula
                ValidIssuer = jwtSettings["validIssure"],
                ValidAudience = jwtSettings["validAudience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;

            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);

            var jwtSecurityToken = securityToken as JwtSecurityToken;

            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token");
            }

            return principal;
        }
    }

}
