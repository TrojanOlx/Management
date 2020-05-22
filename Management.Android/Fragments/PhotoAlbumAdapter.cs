using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.Widget;
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

            // TODO: 
            if (viewType == VIEW_ITEM)
            {
                context = parent.Context;
                View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.PhotoCardView, parent, false);

                PhotoViewHolder photoViewHolder = new PhotoViewHolder(itemView, OnClick);
                return photoViewHolder;
            }
            else if (viewType == (int)VIEW_FOOTER)
            {
                View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.item_recyclerView_foot, parent, false);
                PhotoViewHolder photoViewHolder = new PhotoViewHolder(itemView, OnClick);
                return photoViewHolder;
            }
            return null;

        }



        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            PhotoViewHolder photoViewHolder = holder as PhotoViewHolder;
            photoViewHolder.Image.SetImageResource(mPhotoAlbum[position].PhotoID);
            photoViewHolder.Caption.Text = mPhotoAlbum[position].Caption;
        }


        private const int VIEW_ITEM = 0;
        private const int VIEW_FOOTER = 1;

        public override int GetItemViewType(int position)
        {

            if (position + 1 == ItemCount)
                return VIEW_FOOTER;
            return VIEW_ITEM;
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

    public class RecyclerViewOnScrollListtener : RecyclerView.OnScrollListener
    {
        private readonly SwipeRefreshLayout swipeRefreshLayout;
        private readonly Handler handle;
        private readonly GridLayoutManager gridLayoutManager;
        private readonly PhotoAlbumAdapter photoAlbumAdapter;

        private readonly InsertData insertDataEvent;//加載更多的事件
        private bool isLoadingMore;

        public delegate void InsertData();//添加更多數據的委托


        public RecyclerViewOnScrollListtener(SwipeRefreshLayout swipeRefreshLayout,
            Handler handle,
            GridLayoutManager gridLayoutManager,//布局管理器
            PhotoAlbumAdapter photoAlbumAdapter, // 适配器
            InsertData InsertDataEvent,
            bool IsLoadingMore)
        {
            this.swipeRefreshLayout = swipeRefreshLayout;
            this.handle = handle;
            this.gridLayoutManager = gridLayoutManager;
            this.photoAlbumAdapter = photoAlbumAdapter;
            insertDataEvent = InsertDataEvent;
            isLoadingMore = IsLoadingMore;
        }


        public override void OnScrollStateChanged(RecyclerView recyclerView, int newState)
        {
            base.OnScrollStateChanged(recyclerView, newState);
            System.Diagnostics.Debug.Write("test", "newState" + newState);
        }


        public override void OnScrolled(RecyclerView recyclerView, int dx, int dy)
        {
            base.OnScrolled(recyclerView, dx, dy);
            System.Diagnostics.Debug.Write("正在滑動");
            int lastVisibleItemPosition = gridLayoutManager.FindLastVisibleItemPosition();

            if (lastVisibleItemPosition + 1 == photoAlbumAdapter.ItemCount)
            {
                System.Diagnostics.Debug.Write("test", "loadding已經完成");
                bool isRefreshing = swipeRefreshLayout.Refreshing;
                if (isRefreshing)
                {
                    photoAlbumAdapter.NotifyItemRemoved(photoAlbumAdapter.ItemCount);
                    return;
                }
                if (!isLoadingMore)
                {
                    isLoadingMore = true;

                    handle.PostDelayed(() =>
                    {
                        insertDataEvent();
                        System.Diagnostics.Debug.Write("test", "加載more已經完成");
                        isLoadingMore = false;
                    }, 3000);
                }
            }

        }


    }



}