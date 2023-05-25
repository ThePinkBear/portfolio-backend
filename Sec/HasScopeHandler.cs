using Microsoft.AspNetCore.Authorization;

namespace portfolio_backend;
public class HasScopeHandler : AuthorizationHandler<HasScopeRequirement>
{
  protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, HasScopeRequirement requirement)
  {
    if (context.User == null)
    {
        return Task.CompletedTask;
    }

    var scopeClaim = context.User.FindFirst(c => c.Type == "scope" && c.Issuer == requirement.Issuer);
    if (scopeClaim == null)
    {
        return Task.CompletedTask;
    }

    var scopes = scopeClaim.Value.Split(' ');

    if (scopes.Any(s => s == requirement.Scope))
    {
        context.Succeed(requirement);
    }

    return Task.CompletedTask;
  }
}