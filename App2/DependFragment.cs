using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace App2
{
    public class DependFragment : Fragment
    {
        private string answer;

        public static DependFragment newInstance(string answer)
        {
            DependFragment fragment = new DependFragment();
            fragment.answer = answer;
            return fragment;
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

            var view = inflater.Inflate(Resource.Layout.depend_fragment, container, false);
            TextView content = view.FindViewById<TextView>(Resource.Id.tv_content);
            content.SetText(answer, TextView.BufferType.Normal);
            return view;
        }
    }
}