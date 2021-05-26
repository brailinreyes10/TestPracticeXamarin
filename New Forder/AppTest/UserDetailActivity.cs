using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using AppTest.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace AppTest
{
    [Activity(Label = "Details of user")]
    public class UserDetailActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.activity_userdetail);

            var user = JsonConvert.DeserializeObject<User>(Intent.GetStringExtra("userSelected"));

            var imgProfile = FindViewById<ImageView>(Resource.Id.imgUserPerfil);
            var txtfullName = FindViewById<TextView>(Resource.Id.txtNameUser);
            var txtAge = FindViewById<TextView>(Resource.Id.txtAge);
            var txtDateofBirth = FindViewById<TextView>(Resource.Id.txtDateofBith);
            var txtGender = FindViewById<TextView>(Resource.Id.txtGender);
            var txtPhoneNumber = FindViewById<TextView>(Resource.Id.txtPhoneNumber);
            var txtEmail = FindViewById<TextView>(Resource.Id.txtEmail);

            var imageBitmap = GetImageBitmapFromUrl(user.PictureLg);
            imgProfile.SetImageBitmap(imageBitmap);

            txtfullName.Text = (user.Abbreviation + ". " + user.FisrtName + " " + user.LastName);
            txtAge.Text = user.Age + "years";

            txtDateofBirth.Text = user.DateOfBirth;
            txtGender.Text = user.Gender;
            txtPhoneNumber.Text = user.Phone;
            txtEmail.Text = user.Email;

            var btnBack = FindViewById<Button>(Resource.Id.btnBack);
            btnBack.Click += BtnBack_Click;
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(UserActivity));
            StartActivity(intent);
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