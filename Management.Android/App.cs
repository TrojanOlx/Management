using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Autofac;
using Management.Android.Controllers;
using Xamarin.Essentials;

namespace Management.Android
{
    [Application(UsesCleartextTraffic = true)]
    public class App : Application
    {
        
        public static IContainer Container { get; set; }

        public App(IntPtr handle, JniHandleOwnership transfer) : base(handle, transfer)
        {
               
        }

        public override void OnCreate()
        {
            Initialize();

            base.OnCreate();
        }

        private static void Initialize()
        {
            var builder = new ContainerBuilder();
            builder.RegisterAssemblyModules(typeof(App).Assembly);
            builder.Register(r => new AuthorizationTask("tt.client", "secret"));
            App.Container = builder.Build();

        }
    }
}