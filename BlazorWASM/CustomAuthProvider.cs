using System.Security.Claims;
using HttpsClients.ClientInterfaces;
using Microsoft.AspNetCore.Components.Authorization;

namespace BlazorWASM;

public class CustomAuthProvider : AuthenticationStateProvider
{
    private readonly IAuthService authService;

    public CustomAuthProvider(IAuthService authService)
    {
        this.authService = authService;
        authService.OnAuthStateChanged += AuthStateCanged;
    }

    private void AuthStateCanged(ClaimsPrincipal principal)
    {
        NotifyAuthenticationStateChanged(
            Task.FromResult(
                new AuthenticationState(principal)));
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        ClaimsPrincipal principal = await authService.GetAuthAsync();
        return new AuthenticationState(principal);
    }
    
    
}