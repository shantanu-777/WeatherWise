// Services/SupabaseService.cs
using Microsoft.AspNetCore.Components.Authorization;
using Supabase;
using Supabase.Gotrue;
using System.Security.Claims;
using WeatherWise.Shared.Models;
using static Supabase.Gotrue.Constants;

public class SupabaseService
{
    private readonly Supabase.Client _client;
    private readonly AuthenticationStateProvider _customAuthStateProvider;

    public SupabaseService(Supabase.Client client, AuthenticationStateProvider customAuthStateProvider)
    {
        _client = client;
        _customAuthStateProvider = customAuthStateProvider;
    }

    public async Task<Session?> SignUp(string email, string password, string firstName)
    {
        try
        {
            var session = await _client.Auth.SignUp(email, password, new Supabase.Gotrue.SignUpOptions
            {
                Data = new Dictionary<string, object>
                {
                    { "first_name", firstName }
                }
            });

            if (_customAuthStateProvider is CustomAuthStateProvider customProvider)
            {
                await customProvider.StateChangedAsync();
            }

            return session;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error during sign up: {ex.Message}");
            throw;
        }
    }

    public async Task<Session?> SignIn(string email, string password)
    {
        try
        {
            var session = await _client.Auth.SignIn(email, password);

            if (_customAuthStateProvider is CustomAuthStateProvider customProvider)
            {
                await customProvider.StateChangedAsync();
            }

            return session;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error during sign in: {ex.Message}");
            throw;
        }
    }

    public async Task SignOut()
    {
        await _client.Auth.SignOut();

        if (_customAuthStateProvider is CustomAuthStateProvider customProvider)
        {
            await customProvider.StateChangedAsync();
        }
    }

    //public async Task SignInWithProvider(Provider provider)
    //{
    //    try
    //    {
    //        await _client.Auth.SignIn(provider);

    //        // Notify authentication state changed
    //        if (_customAuthStateProvider is CustomAuthStateProvider customProvider)
    //        {
    //            await customProvider.StateChangedAsync();
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        Console.WriteLine($"Error during provider sign in: {ex.Message}");
    //        throw;
    //    }
    //}

    public async Task<User?> GetUser()
    {
        return _client.Auth.CurrentUser;
    }
}