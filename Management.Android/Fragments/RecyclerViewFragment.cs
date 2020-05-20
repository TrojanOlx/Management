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
    public class RecyclerViewFragment : Fragment
    {

        PhotoAlbumAdapter mAdapter;
        RecyclerView recyclerView;

        RecyclerView.LayoutManager mLayoutManager;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {

            var view = inflater.Inflate(Resource.Layout.fragment_RecyclerView, container, false);


            


            recyclerView = view.FindViewById<RecyclerView>(Resource.Id.recyclerView);

            GridLayoutManager manager = new GridLayoutManager(this.Context,2);

            //mLayoutManager = new LinearLayoutManager(this.Context);


            recyclerView.SetLayoutManager(manager);

            var mPhotoAlbum = new PhotoAlbum();

            mAdapter = new PhotoAlbumAdapter(mPhotoAlbum);

            mAdapter.ItemClick += OnItemClick;

            recyclerView.SetAdapter(mAdapter);

            mAdapter.NotifyDataSetChanged();

            return view;
        }

        void OnItemClick(object sender, int position)
        {
            // Display a toast that briefly shows the enumeration of the selected photo:
            int photoNum = position + 1;
            Toast.MakeText(Context, "This is photo number " + photoNum, ToastLength.Short).Show();
        }
    }
}