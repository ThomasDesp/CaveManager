using System.Security.Claims;

namespace CaveManager.Entities
{
    public static class Connection
    {
        public static bool Isconnected(this HttpContext httpContext, Owner owner)
        {
            var identity = httpContext.User?.Identity as ClaimsIdentity;
            var idCurrentUser = identity?.FindFirst(ClaimTypes.NameIdentifier);
            if (idCurrentUser == null)
                return true;
            else
                return false;
        }
    }
}
