using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AppTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace AppTest.Resources
{
    public class CustomAdapter : BaseAdapter
    {
        private Activity activity;
        private List<User> users;

        public CustomAdapter(Activity activity, List<User> users)
        {
            this.activity = activity;
            this.users = users;
        }
        public override int Count
        {
            get
            {
                return users.Count;
            }
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return null;
        }

        public override long GetItemId(int position)
        {
            return users[position].UserID;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView ?? activity.LayoutInflater.Inflate(Resource.Layout.activity_customitemlist, parent, false);

            var imageUser = view.FindViewById<ImageView>(Resource.Id.imgUser);
            var txtId = view.FindViewById<TextView>(Resource.Id.txtId);
            var txtFullName = view.FindViewById<TextView>(Resource.Id.txtFullName);

            txtId.Text = users[position].UserID.ToString();
            txtFullName.Text = (users[position].FisrtName + " " + users[position].LastName);

            var imageBitmap = GetImageBitmapFromUrl(users[position].PictureLg.ToString());
            imageUser.SetImageBitmap(imageBitmap);

            return view;
        }

        private Bitmap GetImageBitmapFromUrl(string url)
        {
            Bitmap imageBitmap = null;

            using (var webClient = new WebClient())
            {
                var imageBytes = webClient.DownloadData(url);
                if (imageBytes != null && imageBytes.Length > 0)
                {
                    imageBitmap = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
                }
            }

            return imageBitmap;
        }
    }
}