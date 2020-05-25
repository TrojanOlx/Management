using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Widget;
using Toolbar = Android.Support.V7.Widget.Toolbar;
using ActionBar = Android.Support.V7.App.ActionBar;
using Com.Bumptech.Glide;
using Android.Views;

namespace Management.Android.Fragments
{
    [Activity(Label = "FruitActivity")]
    public class FruitActivity : AppCompatActivity
    {
        public const string FRUIT_NAME = "fruit_name";

        public const string FRUIT_IMAGE_ID = "fruit_image_id";



        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_fruit);
            var fruitName = Intent.GetStringExtra(FRUIT_NAME);
            var fruitImageId = Intent.GetIntExtra(FRUIT_IMAGE_ID, 0);

            Toolbar toolbar = FindViewById<Toolbar>(Resource.Id.tool_bar);
            CollapsingToolbarLayout collapsingToolbar = FindViewById<CollapsingToolbarLayout>(Resource.Id.collapsing_toolbar);
            ImageView fruitImageView = FindViewById<ImageView>(Resource.Id.fruit_image_view);
            TextView fruitContentText = FindViewById<TextView>(Resource.Id.fruit_content_text);
            
            SetSupportActionBar(toolbar);

            ActionBar actionBar = SupportActionBar;
            if (actionBar!=null)
            {
                actionBar.SetDisplayHomeAsUpEnabled(true);
            }
            collapsingToolbar.SetTitle(fruitName);

            Glide.With(this).Load(fruitImageId).Into(fruitImageView);
            fruitContentText.Text = fruitName;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.home:
                    Finish();
                    return true;
                default:
                    break;
            }


            return base.OnOptionsItemSelected(item);
        }
    }
}