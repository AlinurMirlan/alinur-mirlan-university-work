using System.Security.Claims;

namespace BudgetTracker.Infrastructure
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetUserId(this ClaimsPrincipal user)
        {
            var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? throw new InvalidOperationException("User hasn't logged in.");
            return userId;
        }
    }
}
