using System;

using Android.Views;
using Android.Widget;
using Android.Support.V7.Widget;

namespace Management.Android.Fragments
{
    public class FruitAdapter : RecyclerView.Adapter
    {
        public event EventHandler<FruitAdapterClickEventArgs> ItemClick;
        public event EventHandler<FruitAdapterClickEventArgs> ItemLongClick;
        string[] items;



        private CardView cardView;
        private ImageView imageView;
        private TextView textView;

        public FruitAdapter(string[] data)
        {
            items = data;
        }

        // Create new views (invoked by the layout manager)
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {

            //Setup your layout here
            View itemView = null;
            //var id = Resource.Layout.fruit_item;
            //itemView = LayoutInflater.From(parent.Context)
            //                        .Inflate(id, parent, false);

            


            var vh = new FruitAdapterViewHolder(itemView, OnClick, OnLongClick);
            return vh;
        }

        // Replace the contents of a view (invoked by the layout manager)
        public override void OnBindViewHolder(RecyclerView.ViewHolder viewHolder, int position)
        {
            var item = items[position];

            // Replace the contents of the view with that element
            var holder = viewHolder as FruitAdapterViewHolder;
            //holder.TextView.Text = items[position];
        }

        public override int ItemCount => items.Length;

        void OnClick(FruitAdapterClickEventArgs args) => ItemClick?.Invoke(this, args);
        void OnLongClick(FruitAdapterClickEventArgs args) => ItemLongClick?.Invoke(this, args);

    }

    public class FruitAdapterViewHolder : RecyclerView.ViewHolder
    {
        //public TextView TextView { get; set; }


        public FruitAdapterViewHolder(View itemView, Action<FruitAdapterClickEventArgs> clickListener,
                            Action<FruitAdapterClickEventArgs> longClickListener) : base(itemView)
        {
            //TextView = v;
            itemView.Click += (sender, e) => clickListener(new FruitAdapterClickEventArgs { View = itemView, Position = AdapterPosition });
            itemView.LongClick += (sender, e) => longClickListener(new FruitAdapterClickEventArgs { View = itemView, Position = AdapterPosition });
        }
    }

    public class FruitAdapterClickEventArgs : EventArgs
    {
        public View View { get; set; }
        public int Position { get; set; }
    }
}