using System.ComponentModel.DataAnnotations;

namespace MusicPlatformApi.Models
{
    public class CredentialModel
    {
        public string Jwt { get; set; }
        public DateTime Expiration { get; set; }
        public string UserId { get; set; }
        public bool IsAdmin { get; set; } = false;

        public CredentialModel(string jwt, DateTime expiration, string userId)
        {
            UserId = userId;
            Jwt = jwt;
            Expiration = expiration;
        }
    }
}   
