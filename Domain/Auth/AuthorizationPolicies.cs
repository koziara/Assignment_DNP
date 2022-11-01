using System.Security.Claims;
using Microsoft.Extensions.DependencyInjection;

namespace Domain.Auth;

public class AuthorizationPolicies
{
    public static void AddPolicies(IServiceCollection services)
    {
        services.AddAuthorizationCore(options =>
        {
            options.AddPolicy("MustBeAbove18", a =>
                a.RequireAuthenticatedUser().RequireAssertion(context =>
                {
                    Claim? ageClaim = context.User.FindFirst(claim => claim.Type.Equals("Age"));
                    if (ageClaim == null) return false;
                    return int.Parse(ageClaim.Value) >= 18;
                }));
        });
    }
}