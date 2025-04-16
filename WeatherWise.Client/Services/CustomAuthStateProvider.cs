// Services/CustomAuthStateProvider.cs
using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using Supabase.Gotrue;
using System.Threading.Tasks;

public class CustomAuthStateProvider : AuthenticationStateProvider
{
    private readonly Supabase.Client _client;

    public CustomAuthStateProvider(Supabase.Client client)
    {
        _client = client;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var user = _client?.Auth.CurrentUser;

        if (user != null)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.UserMetadata?.GetValueOrDefault("first_name")?.ToString() ?? string.Empty)
            };

            var identity = new ClaimsIdentity(claims, "supabase");
            return new AuthenticationState(new ClaimsPrincipal(identity));
        }

        return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
    }

    public async Task StateChangedAsync()
    {
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }
}