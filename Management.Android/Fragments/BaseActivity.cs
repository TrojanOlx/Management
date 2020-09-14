using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Autofac;
using Management.Android.Controllers;
using Xamarin.Essentials;

namespace Management.Android.Fragments
{
    public abstract class BaseActivity : AppCompatActivity
    {
        public const int REQUEST_LOGIN = 10;
        public const int RESULT_LOGIN_FAILED = 20;

        private AuthorizationTask _authorizationTask;
        public BaseActivity()
        {
            _authorizationTask = App.Container.Resolve<AuthorizationTask>();
        }

        public bool EnableLogin()
        {
            var accessToken = Preferences.Get(AuthorizationConsts.AccessToken, null);
            if (accessToken == null)
            {
                return true;
            }

            var expiresIn = Preferences.Get(AuthorizationConsts.ExpiresIn, DateTime.Now);
            if (expiresIn < DateTime.Now.AddMinutes(5))
            {
                var refreshToken = Preferences.Get(AuthorizationConsts.RefreshToken, null);
                if (refreshToken != null)
                {
                    new Thread(async T =>
                    {
                        var tokenResponse = await _authorizationTask.RefreshToken(refreshToken);
                        RunOnUiThread(() =>
                        {
                            if (!tokenResponse.IsError)
                            {
                                Preferences.Set(AuthorizationConsts.AccessToken, tokenResponse.AccessToken);
                                Preferences.Set(AuthorizationConsts.RefreshToken, tokenResponse.RefreshToken);
                                Preferences.Set(AuthorizationConsts.Raw, tokenResponse.Raw);
                                Preferences.Set(AuthorizationConsts.ExpiresIn, DateTime.Now.AddSeconds(tokenResponse.ExpiresIn));
                            }
                            else
                            {
                                Preferences.Remove(AuthorizationConsts.AccessToken);
                                Preferences.Remove(AuthorizationConsts.RefreshToken);
                                Preferences.Remove(AuthorizationConsts.Raw);
                                Preferences.Remove(AuthorizationConsts.ExpiresIn);
                            }
                        });
                    }).Start();
                }
                else
                {
                    return true;
                }
            }
            return false;
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // 如果需要登录，先去登录页面，在初始化数据
            if (EnableLogin())
            {
                StartActivityForResult(typeof(LoginActivity), REQUEST_LOGIN);
            }
            else
            {
                InitData();
            }
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (requestCode == REQUEST_LOGIN)
            {
                if (resultCode == Result.Ok)
                {
                    InitData();
                }
                else
                {
                    Finish();
                }
            }

        }

        public abstract void InitData();

    }
}