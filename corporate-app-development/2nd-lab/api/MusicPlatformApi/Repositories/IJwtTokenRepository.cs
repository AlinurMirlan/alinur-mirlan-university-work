using MusicPlatformApi.Data.Entities;
using MusicPlatformApi.Models;

namespace MusicPlatformApi.Repositories
{
    public interface IJwtTokenRepository
    {
        public CredentialModel CreateJwt(User user);
    }
}
