using Android.App;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Widget;
using Toolbar = Android.Support.V7.Widget.Toolbar;
using Com.Bumptech.Glide;
using Android.Views;
using Java.Lang;
using AndroidResource = Android.Resource;

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

            // 添加返回键
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            
            collapsingToolbar.SetTitle(fruitName);

            Glide.With(this).Load(fruitImageId).Into(fruitImageView);
            //Glide.With(this).AsBitmap().Load("").Into(fruitImageView);
            fruitContentText.Text = Add500(fruitName);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case AndroidResource.Id.Home:
                    Finish();
                    return true;
                default:
                    break;
            }

            return base.OnOptionsItemSelected(item);
        }


        private string Add500(string str)
        {
            StringBuffer sb = new StringBuffer();
            for (int i = 0; i < 500; i++)
            {
                sb.Append($"{str}_");
            }

            return sb.ToString();
        }
    }
}