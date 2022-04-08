using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace FitAppServer.Utils;

public class ClaimsAccessor : IClaimsAccessor
{
    private readonly IHttpContextAccessor _contextAccessor;

    public ClaimsAccessor(IHttpContextAccessor accessor)
    {
        _contextAccessor = accessor;
    }

    public string UserId => _contextAccessor.HttpContext?.User.FindFirstValue("user_id") ?? "";
}