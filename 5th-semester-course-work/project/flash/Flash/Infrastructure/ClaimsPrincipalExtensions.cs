using System.Security.Claims;

namespace Flash.Infrastructure
{
    public static class ClaimsPrincipalExtensions
    {
        public static int GetClaimIntValue(this ClaimsPrincipal claimsPrincipal, string? claimType)
        {
            if (claimType is null)
                throw new InvalidOperationException();

            int claimValue = int.Parse(claimsPrincipal.Claims.Single(c => c.Type == claimType).Value);
            return claimValue;
        }
    }
}
