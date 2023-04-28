using Microsoft.IdentityModel.Tokens;
using MusicPlatformApi.Data.Entities;
using MusicPlatformApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MusicPlatformApi.Repositories
{
    public class JwtTokenRepository : IJwtTokenRepository
    {
        private readonly IConfiguration _config;

        public JwtTokenRepository(IConfiguration config)
        {
            _config = config;
        }

        public CredentialModel CreateJwt(User user)
        {
            // a Claim is just a piece of information about the consumer in the form of a key-value pair.
            Claim[] claims =
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email ?? string.Empty),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Gender, user.Sex),
            };

            // We are making a symmetrically signed JWT, which means we have a key that is used to both encrypt and decrypt
            // the token. 
            string key = _config["Security:Tokens:Key"] ?? throw new InvalidOperationException("JWT security key is not set.");
            SecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

            // SigningCredentials represent the signing process provided the key and the encryption/decryption
            // algorithm to be used with it.
            SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256);

            // Directly instantiating the JWT itself (we could use JwtSecurityTokenHandler's methods)
            DateTime expiration = DateTime.Now.AddHours(5);
            JwtSecurityToken jwtSecurityToken = new(
                issuer: _config["Security:Tokens:Issuer"],
                audience: _config["Security:Tokens:Audience"],
                claims: claims,
                expires: expiration,
                signingCredentials: signingCredentials
            );
            
            // Serializing the JWT. Thus getting the actual thing {header.payload.S1gn@tur3}
            string jwt = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            return new CredentialModel(jwt, expiration, user.Id);
        }
    }
}
