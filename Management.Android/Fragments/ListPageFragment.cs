using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;

namespace Management.Android.Fragments
{
    public class ListPageFragment : Fragment
    {

        private readonly string _textContext;

        public ListPageFragment(string textContext = default)
        {
            _textContext = textContext;
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

            var view = inflater.Inflate(Resource.Layout.fragment_list_page, container, false);

            if (!string.IsNullOrWhiteSpace(_textContext))
            {
                var text = (TextView)view.FindViewById(Resource.Id.text);
                text.Text = _textContext;
            }

            return view;
        }
    }
}