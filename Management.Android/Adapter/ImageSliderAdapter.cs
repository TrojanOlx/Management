using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.View;
using Android.Views;
using Android.Widget;
using Com.Bumptech.Glide;
using Java.Lang;
using Java.Net;
using Management.Android.Fragments;
using Object = Java.Lang.Object;

namespace Management.Android.Adapter
{
    /// <summary>
    /// 轮播图适配器
    /// </summary>
    public class ImageSliderAdapter : PagerAdapter
    {

        private readonly Context _context;
        private readonly List<string> _imageList;

        public ImageSliderAdapter(Context context, List<string> imageList)
        {
            _context = context;
            _imageList = imageList;
        }

        public override bool IsViewFromObject(View view, Object @object)
        {
            return view == (LinearLayout)@object;
        }

        public override Object InstantiateItem(ViewGroup container, int position)
        {

            position = position % Count;

            View view = container;
            var inflater = _context.GetSystemService(Context.LayoutInflaterService) as LayoutInflater;
            view = inflater.Inflate(Resource.Layout.image_slider_item, null);
            var child = view.FindViewById<ImageView>(Resource.Id.image_slider_item);
            // 点击事件
            child.Click += (sender, args) =>
            {
                Toast.MakeText(_context, $"{_imageList[position]}", ToastLength.Short).Show();
            };


            Glide.With(_context).AsBitmap().Load(_imageList[position]).Into(child);


            //Bitmap image = null;
            //Task.Run(() =>
            //{
            //    URL url = new URL(_imageList[position]);
            //    image = BitmapFactory.DecodeStream(url.OpenConnection().InputStream);
            //}).ContinueWith(t =>
            //{
            //    (_context as FruitActivity)?.RunOnUiThread(() =>
            //    {
            //        child.SetImageBitmap(image);
            //    });
            //});

            container.AddView(view);
            return view;
        }

        //public override ICharSequence GetPageTitleFormatted(int position)
        //{
        //    return new Java.Lang.String("Problem " + (position + 1));
        //}

        public override void DestroyItem(ViewGroup container, int position, Java.Lang.Object @object)
        {
            container.RemoveView((View)@object);
        }

        /// <summary>
        /// 返回总数量
        /// </summary>
        public override int Count => _imageList.Count;
    }
}