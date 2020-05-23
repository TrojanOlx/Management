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


        public override int ItemCount => mPhotoAlbum.NumPhotos;



        public PhotoAlbumAdapter(PhotoAlbum mPhotoAlbum)
        {
            this.mPhotoAlbum = mPhotoAlbum;
        }

        Context context;

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            context = parent.Context;
            View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.PhotoCardView, parent, false);

            PhotoViewHolder photoViewHolder = new PhotoViewHolder(itemView, OnClick);
            return photoViewHolder;
        }



        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            PhotoViewHolder photoViewHolder = holder as PhotoViewHolder;
            photoViewHolder.Image.SetImageResource(mPhotoAlbum[position].PhotoID);
            photoViewHolder.Caption.Text = mPhotoAlbum[position].Caption;
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

    public class RecyclerViewOnScrollListtener : RecyclerView.OnScrollListener {

        public delegate void LoadMoreEventHandler();
        private LoadMoreEventHandler LoadMoreEvent;
        private GridLayoutManager gridLayoutManager;

        public RecyclerViewOnScrollListtener(LoadMoreEventHandler loadMoreEvent, GridLayoutManager gridLayoutManager)
        {
            LoadMoreEvent = loadMoreEvent;
            this.gridLayoutManager = gridLayoutManager;
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