using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using IdentityModel.Client;
using Java.Lang;
using Xamarin.Essentials;

namespace Management.Android.Controllers
{
    public class AuthorizationTask
    {
        private readonly HttpClient _httpClient;
        private DiscoveryDocumentResponse document;


        private readonly string ClientId;
        private readonly string ClientSecret;
        public AuthorizationTask(string clientId, string clientSecret)
        {
            ClientId = clientId;
            ClientSecret = clientSecret;
            _httpClient = new HttpClient();
            SetDocument();
        }

        public void SetDocument()
        {
            document = _httpClient.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest
            {
                Address = "http://192.168.2.104:5000",
                Policy = {
                    RequireHttps = false
                }
            }).GetAwaiter().GetResult();
        }

        public async Task<string> Login(string userName, string passWord)
        {


            var client = new HttpClient();

            var tokenResponse = await client.RequestPasswordTokenAsync(new PasswordTokenRequest
            {
                Address = document.TokenEndpoint,
                ClientId = ClientId,
                ClientSecret = ClientSecret,
                UserName = userName,
                Password = passWord,
                Scope = "TrojanApi offline_access openid profile",
            });

            if (tokenResponse.IsError)
            {
                return tokenResponse.Error;
            }

            Preferences.Set(AuthorizationConsts.AccessToken, tokenResponse.AccessToken);
            Preferences.Set(AuthorizationConsts.RefreshToken, tokenResponse.RefreshToken);
            Preferences.Set(AuthorizationConsts.Raw, tokenResponse.Raw);

            return "登录成功";
        }

        public async Task RefreshToken()
        {
            var refreshToken = Preferences.Get(AuthorizationConsts.RefreshToken, null);

            var tokenResponse = await _httpClient.RequestRefreshTokenAsync(new RefreshTokenRequest
            {
                Address = document.TokenEndpoint,
                ClientId = ClientId,
                ClientSecret = ClientSecret,
                RefreshToken = refreshToken
            });

            if (tokenResponse.IsError)
            {

            }

            Preferences.Set(AuthorizationConsts.AccessToken, tokenResponse.AccessToken);
            Preferences.Set(AuthorizationConsts.RefreshToken, tokenResponse.RefreshToken);
            Preferences.Set(AuthorizationConsts.Raw, tokenResponse.Raw);

        }

    }
}