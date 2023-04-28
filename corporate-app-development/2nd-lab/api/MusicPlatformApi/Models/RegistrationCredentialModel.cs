using System.ComponentModel.DataAnnotations;

namespace MusicPlatformApi.Models
{
    public class RegistrationCredentialModel
    {
        [EmailAddress]
        public required string Email { get; set; }

        [MinLength(8)]
        public required string Password { get; set; }

        [MinLength(1)]
        public required string Name { get; set; }

        [Range(1, 120)]
        public int? Age { get; set; }

        [RegularExpression("^(m|f|u)$")]
        public required string Sex { get; set; }
    }
}
