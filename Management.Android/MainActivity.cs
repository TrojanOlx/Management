﻿using System;
using System.Net;
using System.Text.RegularExpressions;
using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Java.Lang;
using Management.Android.Fragments;
using Management.Android.Models;
using Management.Android.UI;
using static Android.Views.View;
using Toolbar = Android.Support.V7.Widget.Toolbar;
using Uri = Android.Net.Uri;

namespace Management.Android
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity, NavigationView.IOnNavigationItemSelectedListener
    {

        private HomeFragment _homeFragment;
        private MineFragment _mineFragment;
        private ListPageFragment _listPageFragment;
        private RecyclerViewFragment _recyclerViewFragment;


        private PhotoAlbum mPhotoAlbum;
        private PhotoAlbumAdapter mAdapter;
        private SwipeRefreshLayout swipeRefreshLayout;
        private RoundImageView imagebutton;

        private void Init()
        {
            _homeFragment = new HomeFragment();
            _mineFragment = new MineFragment();
            _listPageFragment = new ListPageFragment();
            _recyclerViewFragment = new RecyclerViewFragment();
        }


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);

            

            SetContentView(Resource.Layout.activity_main);

            //var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            //SetSupportActionBar(toolbar);

            FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            fab.Click += FabOnClick;

            //DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            //ActionBarDrawerToggle toggle = new ActionBarDrawerToggle(this, drawer, toolbar, Resource.String.navigation_drawer_open, Resource.String.navigation_drawer_close);
            //drawer.AddDrawerListener(toggle);
            //toggle.SyncState();

            NavigationView navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            navigationView.SetNavigationItemSelectedListener(this);


            #region RecyclerView
            var recyclerView = FindViewById<RecyclerView>(Resource.Id.recyclerView_main);

            var manager = new GridLayoutManager(this, 2);

            recyclerView.SetLayoutManager(manager);

            mPhotoAlbum = new PhotoAlbum();

            mAdapter = new PhotoAlbumAdapter(mPhotoAlbum);

            mAdapter.ItemClick += OnItemClick;

            recyclerView.SetAdapter(mAdapter);


            recyclerView.AddOnScrollListener(new RecyclerViewOnScrollListtener(AddList,manager));

            #endregion


            swipeRefreshLayout = FindViewById<SwipeRefreshLayout>(Resource.Id.switch_refresh);
            swipeRefreshLayout.SetColorSchemeColors(Resource.Color.colorPrimary);
            swipeRefreshLayout.Refresh += SwipeRefreshLayout_Refresh;

            var imagebutton = FindViewById<RoundImageView>(Resource.Id.iv_userphoto_mycenter_myprofile);
            imagebutton.Click += Imagebutton_Click;

            var imageBitmap = GetImageBitmapFromUrl("https://avatar.csdnimg.cn/A/4/F/3_kgcourage.jpg");
            imagebutton.SetImageBitmap(imageBitmap);

            Init();
        }


        private void AddList()
        {
            //Thread.Sleep(2000);
            mPhotoAlbum.AddData();
            mAdapter.NotifyDataSetChanged();
            swipeRefreshLayout.Refreshing = false;
            //mAdapter.NotifyItemRemoved(mAdapter.ItemCount);

        }

        private void Imagebutton_Click(object sender, EventArgs e)
        {
            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            drawer.OpenDrawer(GravityCompat.Start);

            Toast.MakeText(this, "clik imageButton", ToastLength.Long).Show();
        }


        private Bitmap GetImageBitmapFromUrl(string url)
        {
            Bitmap imageBitmap = null;

            using (var webClient = new WebClient())
            {
                var imageBytes = webClient.DownloadData(url);
                if (imageBytes != null && imageBytes.Length > 0)
                {
                    imageBitmap = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
                }
            }

            return imageBitmap;
        }

        private void SwipeRefreshLayout_Refresh(object sender, EventArgs e)
        {

            mAdapter.NotifyItemRemoved(mAdapter.ItemCount);
            int idx = mPhotoAlbum.RandomSwap();
            //mAdapter.NotifyItemChanged(0);
            //mAdapter.NotifyItemChanged(idx);
            mAdapter.NotifyDataSetChanged();
            swipeRefreshLayout.Refreshing = false;
        }

        void OnItemClick(object sender, int position)
        {
            // Display a toast that briefly shows the enumeration of the selected photo:
            int photoNum = position + 1;
            Toast.MakeText(this, "This is photo number " + photoNum, ToastLength.Short).Show();
        }


        public override void OnBackPressed()
        {
            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            if (drawer.IsDrawerOpen(GravityCompat.Start))
            {
                drawer.CloseDrawer(GravityCompat.Start);
            }
            else
            {
                base.OnBackPressed();
            }
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.action_settings)
            {
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        private void FabOnClick(object sender, EventArgs eventArgs)
        {
            View view = (View)sender;
            Snackbar.Make(view, "Replace with your own action", Snackbar.LengthLong)
                .SetAction("Action", (IOnClickListener)null).Show();
        }



        public bool OnNavigationItemSelected(IMenuItem item)
        {
            FragmentTransaction fTransaction = FragmentManager.BeginTransaction();
            
            int id = item.ItemId;
            if (id == Resource.Id.nav_camera)
            {
                this.Title = "首页";
                // Handle the camera action
                //fTransaction.Replace(Resource.Id.main_frame_layout, _homeFragment).Commit();
            }
            else if (id == Resource.Id.nav_gallery)
            {
                this.Title = "我的";
                //fTransaction.Replace(Resource.Id.main_frame_layout, _mineFragment).Commit();
            }
            else if (id == Resource.Id.nav_slideshow)
            {
                this.Title = "列表";
                //fTransaction.Replace(Resource.Id.main_frame_layout, _listPageFragment).Commit();
            }
            else if (id == Resource.Id.nav_manage)
            {
                this.Title = "CardView";
                //fTransaction.Replace(Resource.Id.main_frame_layout, _recyclerViewFragment).Commit();
            }
            else if (id == Resource.Id.nav_share)
            {

            }
            else if (id == Resource.Id.nav_send)
            {

            }

            //_fTransaction.Commit();
            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            drawer.CloseDrawer(GravityCompat.Start);
            return true;
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}

