using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Management.Android.Models;

namespace Management.Android.Fragments
{
    public class PhotoAlbumAdapter : RecyclerView.Adapter
    {
        public event EventHandler<int> ItemClick;

        PhotoAlbum mPhotoAlbum;
        RecyclerView.LayoutManager mLayoutManager;

        // 普通布局
        private const int TYPE_ITEM = 1;
        // 脚布局
        private const int TYPE_FOOTER = 2;


        public override int ItemCount => mPhotoAlbum.NumPhotos;



        public PhotoAlbumAdapter(PhotoAlbum mPhotoAlbum)
        {
            this.mPhotoAlbum = mPhotoAlbum;
        }

        Context context;

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            if (viewType == TYPE_ITEM)
            {
                context = parent.Context;
                View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.PhotoCardView, parent, false);
                PhotoViewHolder photoViewHolder = new PhotoViewHolder(itemView, OnClick);
                return photoViewHolder;
            }
            else if (viewType == TYPE_FOOTER)
            {
                View view = LayoutInflater.From(parent.Context)
                    .Inflate(Resource.Layout.layout_refresh_footer, parent, false);
                return new FootViewHolder(view);
            }

            return null;


        }



        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            if (holder is PhotoViewHolder)
            {
                PhotoViewHolder photoViewHolder = holder as PhotoViewHolder;
                photoViewHolder.Image.SetImageResource(mPhotoAlbum[position].PhotoID);
                photoViewHolder.Caption.Text = mPhotoAlbum[position].Caption;
            }
        }

        public override int GetItemViewType(int position)
        {
            if (position + 1 == mPhotoAlbum.NumPhotos)
            {
                return TYPE_FOOTER;
            }
            else
            {
                return TYPE_ITEM;
            }
        }


        private void OnClick(int position)
        {
            if (ItemClick != null)
                ItemClick(this, position);
        }
    }

    public class PhotoViewHolder : RecyclerView.ViewHolder
    {
        public ImageView Image { get; private set; }
        public TextView Caption { get; private set; }

        // Get references to the views defined in the CardView layout.
        public PhotoViewHolder(View itemView, Action<int> listener)
            : base(itemView)
        {
            // Locate and cache view references:
            Image = itemView.FindViewById<ImageView>(Resource.Id.imageView);
            Caption = itemView.FindViewById<TextView>(Resource.Id.textView);

            // Detect user clicks on the item view and report which item
            // was clicked (by layout position) to the listener:
            itemView.Click += (sender, e) => listener(base.LayoutPosition);
        }
    }


    public class FootViewHolder : RecyclerView.ViewHolder
    {
        public FootViewHolder(View itemView) : base(itemView)
        {
        }
    }


    public class RecyclerViewOnScrollListtener : RecyclerView.OnScrollListener
    {

        public delegate void LoadMoreEventHandler();
        private LoadMoreEventHandler LoadMoreEvent;
        private GridLayoutManager gridLayoutManager;
        private readonly Handler handler;

        public RecyclerViewOnScrollListtener(LoadMoreEventHandler loadMoreEvent, GridLayoutManager gridLayoutManager, Handler handler)
        {
            LoadMoreEvent = loadMoreEvent;
            this.gridLayoutManager = gridLayoutManager;
            this.handler = handler;
        }

        public override void OnScrolled(RecyclerView recyclerView, int dx, int dy)
        {
            base.OnScrolled(recyclerView, dx, dy);

            var visibleItemCount = recyclerView.ChildCount;
            var totalItemCount = recyclerView.GetAdapter().ItemCount;
            var pastVisiblesItems = gridLayoutManager.FindFirstVisibleItemPosition();

            if ((visibleItemCount + pastVisiblesItems) >= totalItemCount)
            {
                Handler handler = new Handler();
                handler.Post(StartAction);
            }

        }





        private void StartAction()
        {
            LoadMoreEvent();
        }
    }



}