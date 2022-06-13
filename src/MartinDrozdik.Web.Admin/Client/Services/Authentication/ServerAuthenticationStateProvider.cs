using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using MartinDrozdik.Data.Models.Authentication;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;

namespace MartinDrozdik.Web.Admin.Client.Services.Authentication
{
    //Sauce: https://github.com/dotnet/aspnetcore/blob/master/src/Components/test/testassets/BasicTestApp/AuthTest/ServerAuthenticationStateProvider.cs

    public class ServerAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly HttpClient httpClient;

        public ServerAuthenticationStateProvider(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            //Base address check
            if (httpClient?.BaseAddress is null)
                throw new Exception("Could not retrieve the base address");

            //Get auth data about me
            var uri = new Uri(httpClient.BaseAddress, "api/user/me");
            var dataResult = await httpClient.GetAsync(uri.AbsoluteUri);

            //Ensure Ok response
            dataResult.EnsureSuccessStatusCode();

            //Get the auth state object
            var dataString = await dataResult.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<AuthenticationStateData>(dataString);
            if (data is null)
                throw new Exception("Could not convert the recieved authentication data");

            //Create and return the authentication state
            ClaimsIdentity identity;
            if (data.IsAuthenticated)
            {
                var claims = new[] { new Claim(ClaimTypes.Name, data.UserName) }
                    .Concat(data.ExposedClaims.Select(c => new Claim(c.Type, c.Value)));
                identity = new ClaimsIdentity(claims, "Server authentication");
            }
            else
            {
                identity = new ClaimsIdentity();
            }

            return new AuthenticationState(new ClaimsPrincipal(identity));
        }
    }
}
