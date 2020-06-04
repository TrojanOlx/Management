using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Support.V7.App;
using Android.Widget;
using Toolbar = Android.Support.V7.Widget.Toolbar;
using Com.Bumptech.Glide;
using Android.Views;
using Java.Lang;
using Management.Android.Adapter;
using AndroidResource = Android.Resource;
using Android.Util;
using Management.Android.Models;
using Android.Graphics;
using Android.Support.V7.View.Menu;

namespace Management.Android.Fragments
{
    [Activity(Label = "FruitActivity")]
    public class FruitActivity : AppCompatActivity
    {
        public const string FRUIT_NAME = "fruit_name";

        public const string FRUIT_IMAGE_ID = "fruit_image_id";

        private List<EditText> editTexts = new List<EditText>();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);


            SetContentView(Resource.Layout.activity_fruit);


            var mylinearLayout = FindViewById<LinearLayout>(Resource.Id.my_linearLayout);

            //var linearLayout = GetItemLinearLayout();
            //linearLayout.SetBackgroundColor(Color.Blue);


            FieldServer fieldServer = new FieldServer();
            var list = fieldServer.GetFields();

            foreach (var item in list)
            {
                var linearLayout = GetItemLinearLayout();
                linearLayout.SetBackgroundResource(Resource.Drawable.border);

                var textView = GetItemTextView(3);
                var editView = GetItemEditText(1);

                //textView.SetBackgroundResource(Resource.Drawable.border);
                //editView.SetBackgroundResource(Resource.Drawable.border);

                textView.Text = item.Name;
                editView.Text = item.Name + "_" + item.Key;
                editView.Enabled = false;


                linearLayout.AddView(textView);
                linearLayout.AddView(editView);

                editTexts.Add(editView);
                mylinearLayout.AddView(linearLayout);
            }




            var fruitName = Intent.GetStringExtra(FRUIT_NAME);
            var fruitImageId = Intent.GetIntExtra(FRUIT_IMAGE_ID, 0);

            Toolbar toolbar = FindViewById<Toolbar>(Resource.Id.tool_bar);
            CollapsingToolbarLayout collapsingToolbar = FindViewById<CollapsingToolbarLayout>(Resource.Id.collapsing_toolbar);
            //ImageView fruitImageView = FindViewById<ImageView>(Resource.Id.fruit_image_view);
            TextView fruitContentText = FindViewById<TextView>(Resource.Id.fruit_content_text);

            //toolbar.Title = fruitName;
            SetSupportActionBar(toolbar);


            // 添加返回键
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);

            //collapsingToolbar.SetCollapsedTitleTextAppearance(Resource.Style.CollapsedAppBar);
            //collapsingToolbar.SetExpandedTitleTextAppearance(Resource.Style.CollapsedAppBar);
            collapsingToolbar.SetTitle(fruitName);

            //Glide.With(this).Load(fruitImageId).Into(fruitImageView);
            //Glide.With(this).AsBitmap().Load("").Into(fruitImageView);
            fruitContentText.Text = fruitName;//Add500(fruitName);


            var imageViewer = FindViewById<ViewPager>(Resource.Id.image_pager);
            imageViewer.Adapter = new ImageSliderAdapter(this, new List<string>()
            {
                "https://timgsa.baidu.com/timg?image&quality=80&size=b9999_10000&sec=1590552625990&di=8088736bfaac32f8808861d97edf6d70&imgtype=0&src=http%3A%2F%2Fimg.pconline.com.cn%2Fimages%2Fupload%2Fupc%2Ftx%2Fwallpaper%2F1208%2F15%2Fc0%2F12924355_1344999165562.jpg",
                "https://timgsa.baidu.com/timg?image&quality=80&size=b9999_10000&sec=1590552625990&di=2af594f5495d6f2fc3eb19e023ae0914&imgtype=0&src=http%3A%2F%2Fpic.baike.soso.com%2Fp%2F20131221%2F20131221152446-229545202.jpg",
                "https://timgsa.baidu.com/timg?image&quality=80&size=b9999_10000&sec=1590552625989&di=07248ec9129a47b044881e5df9524d1f&imgtype=0&src=http%3A%2F%2Fattach.bbs.miui.com%2Fforum%2F201308%2F24%2F162824zuojehbb2u4kbula.jpg"
            });




            var enableEdits = FindViewById<FloatingActionButton>(Resource.Id.enable_edit);
            enableEdits.Click += delegate
            {
                editTexts.ForEach(f =>
                {
                    f.Enabled = !f.Enabled;
                });
            };
        }


        /// <summary>
        /// 设置状态栏
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.toolbar, menu);
            return true;
        }




        /// <summary>
        /// 获取基本的布局
        /// </summary>
        /// <returns></returns>
        private LinearLayout GetItemLinearLayout()
        {
            // 布局
            LinearLayout linearLayout = new LinearLayout(this);
            LinearLayout.LayoutParams lp = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, LinearLayout.LayoutParams.WrapContent);
            lp.TopMargin = 5;
            linearLayout.LayoutParameters = lp;
            linearLayout.Orientation = Orientation.Horizontal;
            return linearLayout;
        }

        /// <summary>
        /// 获取标题布局
        /// </summary>
        /// <param name="weight"></param>
        /// <returns></returns>
        private TextView GetItemTextView(int weight)
        {
            TextView textView = new TextView(this);
            var lp = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.WrapContent, weight);
            lp.Gravity = GravityFlags.Center;
            textView.LayoutParameters = lp;
            textView.Gravity = GravityFlags.Center;
            return textView;
        }
        /// <summary>
        /// 获取编辑框
        /// </summary>
        /// <param name="weight"></param>
        /// <returns></returns>
        private EditText GetItemEditText(int weight)
        {
            EditText editText = new EditText(this);
            var lp = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.WrapContent, weight);
            editText.LayoutParameters = lp;
            return editText;
        }


        private bool IsEdit = true;

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case AndroidResource.Id.Home:
                    Finish();
                    break;
                case Resource.Id.Material_Edit_Save:
                    if (IsEdit)
                    {
                        item.SetIcon(Resource.Drawable.baocun);
                        IsEdit = !IsEdit;
                        editTexts.ForEach(f =>
                        {
                            f.Enabled = !f.Enabled;
                        });
                        
                    }
                    else
                    {
                        item.SetIcon(Resource.Drawable.bianji);
                        IsEdit = !IsEdit;
                        editTexts.ForEach(f =>
                        {
                            f.Enabled = !f.Enabled;
                        });
                    }
                    Toast.MakeText(this, "Edit", ToastLength.Short).Show();
                    break;
                case Resource.Id.Material_Delete:
                    Toast.MakeText(this, "Delete", ToastLength.Short).Show();
                    break;
                default:
                    break;
            }

            return true;
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