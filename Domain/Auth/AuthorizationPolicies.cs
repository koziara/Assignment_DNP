using System.Security.Claims;
using Microsoft.Extensions.DependencyInjection;

namespace Domain.Auth;

public static class AuthorizationPolicies
{
    public static void AddPolicies(IServiceCollection services)
    {
        services.AddAuthorizationCore(options =>
        {
            options.AddPolicy("MustBeAbove18", a =>
                a.RequireAuthenticatedUser().RequireAssertion(context =>
                {
                    Claim? levelClaim = context.User.FindFirst(claim => claim.Type.Equals("Age"));
                    if (levelClaim == null) return false;
                    return int.Parse(levelClaim.Value) >= 18;
                }));
        });
    }
}