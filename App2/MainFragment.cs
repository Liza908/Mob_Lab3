using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace App2
{
    public class MainFragment : Fragment, View.IOnClickListener
    {
        const string FileName = "storageFile.csv";
        EditText etQuestion;
        RadioGroup radioGroupColor;
        RadioGroup radioGroupPrice;
        public static MainFragment newInstance()
        {
            return new MainFragment(); ;
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

            var view = inflater.Inflate(Resource.Layout.main_fragment, container, false);

            etQuestion = view.FindViewById<EditText>(Resource.Id.et_question);
            radioGroupColor = view.FindViewById<RadioGroup>(Resource.Id.rg_color);
            radioGroupPrice = view.FindViewById<RadioGroup>(Resource.Id.rg_price);



            EditText editText = view.FindViewById<EditText>(Resource.Id.et_question);
            Button btnContinue = view.FindViewById<Button>(Resource.Id.btn_continue);
            btnContinue.SetOnClickListener(this);
            Button btnView = view.FindViewById<Button>(Resource.Id.btn_view);
            btnView.SetOnClickListener(this);
            return view;
        }
        public void OnClick(View v)
        {
            //string folder = Android.OS.Environment.ExternalStorageDirectory.AbsolutePath;
            var dataPath = System.Environment.SpecialFolder.ApplicationData;
            string path = Path.Combine(System.Environment.GetFolderPath(dataPath).Split("files")[0] + @"files", FileName);
            //File.Create(path + "newFile.png");
            //File.Delete(path);
            if (!File.Exists(path)) File.Create(path);
            switch (v.Id)
            {
                case Resource.Id.btn_view:
                    StringBuilder stringBuilder = new StringBuilder();

                    List<string> storage = File.ReadAllLines(path).ToList();
                    foreach (var item in storage)
                    {
                        string[] temp = item.Split(';');
                        stringBuilder.Append($"{temp[0]} ({temp[1]})\n");
                    }

                    DependFragment showFragment = DependFragment.newInstance(stringBuilder.ToString());
                    FragmentTransaction transaction = this.FragmentManager.BeginTransaction();
                    transaction.Replace(Resource.Id.container, showFragment);
                    etQuestion.Text = "";
                    transaction.AddToBackStack(null);
                    transaction.Commit();
                    break;

                case Resource.Id.btn_continue:

                    int selectedIdColor = radioGroupColor.CheckedRadioButtonId;
                    int selectedIdPrice = radioGroupPrice.CheckedRadioButtonId;
                    RadioButton radioButtonColor = radioGroupColor.FindViewById<RadioButton>(selectedIdColor);
                    RadioButton radioButtonPrice = radioGroupPrice.FindViewById<RadioButton>(selectedIdPrice);
                    string text = radioButtonColor.Text + " flower, which costs " + radioButtonPrice.Text;
                    string question = etQuestion.Text.ToString();
                    File.AppendAllText(path, $"{question};{text};\n");
                    etQuestion.Text = "";
                    //etQuestion.ClearFocus();
                    Toast.MakeText(v.Context, "Message saved successfully!", ToastLength.Long).Show();
                    break;

            }
        }
    }
}