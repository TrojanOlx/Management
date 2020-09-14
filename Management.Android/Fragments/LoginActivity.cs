using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Autofac;
using Management.Android.Controllers;
using Xamarin.Essentials;

namespace Management.Android.Fragments
{
    [Activity(Label = "LoginActivity")]
    public class LoginActivity : Activity
    {

        private AuthorizationTask _authorizationTask;
        public LoginActivity()
        {
            _authorizationTask = App.Container.Resolve<AuthorizationTask>();
        }

        private Button Login_Button;
        private EditText UserName_EditText;
        private EditText PassWord_EditText;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_login);
            // Create your application here
            Init();

            Login_Button.Click += Login_Button_Click;
        }

        private void Login_Button_Click(object sender, EventArgs e)
        {
            var userName = UserName_EditText.Text.Trim();
            var passWord = PassWord_EditText.Text.Trim();

            new Thread(async T =>
            {
                var tokenResponse = await _authorizationTask.Login(userName, passWord);
                RunOnUiThread(() =>
                {
                    if (!tokenResponse.IsError)
                    {
                        Toast.MakeText(this, "登录成功", ToastLength.Long).Show();
                        Preferences.Set(AuthorizationConsts.AccessToken, tokenResponse.AccessToken);
                        Preferences.Set(AuthorizationConsts.RefreshToken, tokenResponse.RefreshToken);
                        Preferences.Set(AuthorizationConsts.Raw, tokenResponse.Raw);
                        Preferences.Set(AuthorizationConsts.ExpiresIn, DateTime.Now.AddSeconds(tokenResponse.ExpiresIn));
                        SetResult(Result.Ok);
                        Finish();
                    }
                    else
                    {
                        Toast.MakeText(this, tokenResponse.Error, ToastLength.Long).Show();
                    }
                });
            }).Start();
        }

        private void Init()
        {
            Login_Button = FindViewById<Button>(Resource.Id.login_btn);
            UserName_EditText = FindViewById<EditText>(Resource.Id.login_input_name);
            PassWord_EditText = FindViewById<EditText>(Resource.Id.login_input_password);
        }



        public void StartForResult(Activity activity, int requestCode)
        {
            Intent intent = new Intent(activity, typeof(LoginActivity));
            activity.StartActivityForResult(intent, requestCode);
        }
    }
}