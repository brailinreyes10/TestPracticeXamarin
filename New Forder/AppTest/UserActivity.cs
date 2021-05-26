using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using AndroidX.AppCompat.App;
using AppTest.Models;
using AppTest.Resources;
using EDMTDialog;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace AppTest
{
    [Activity(Label = "List of Users", Theme = "@style/AppTheme")]
    public class UserActivity : AppCompatActivity
    {
        static List<User> users = new List<User>();

        List<string> items = null;
        private ListView listView;

        public async void GetUsersFromJSON()
        {
            string url = "https://randomuser.me/api/";

            users.Clear();

            var request = new HttpRequestMessage();
            request.RequestUri = new Uri(url);
            request.Method = HttpMethod.Get;
            request.Headers.Add("accept", "application/json");

            var client = new HttpClient();
            HttpResponseMessage response = await client.SendAsync(request);

            string json_data = "";

            if (response.StatusCode == HttpStatusCode.OK)
            {
                json_data = await response.Content.ReadAsStringAsync();
            }

            //json_data = "{\"results\":[{\"gender\":\"female\",\"name\":{\"title\":\"Mrs\",\"first\":\"Maria\",\"last\":\"Kristensen\"},\"location\":{\"street\":{\"number\":1882,\"name\":\"Kratvej\"},\"city\":\"Vesterborg\",\"state\":\"Midtjylland\",\"country\":\"Denmark\",\"postcode\":96618,\"coordinates\":{\"latitude\":\"-35.6598\",\"longitude\":\"75.5195\"},\"timezone\":{\"offset\":\"+1:00\",\"description\":\"Brussels, Copenhagen, Madrid, Paris\"}},\"email\":\"maria.kristensen@example.com\",\"login\":{\"uuid\":\"a68737e5-f302-4f4c-8809-d5b6f50ec047\",\"username\":\"organicsnake428\",\"password\":\"787878\",\"salt\":\"lbj9sUsG\",\"md5\":\"1beb585975789dcb073a6a985939c031\",\"sha1\":\"e786a7720ee7e6c872430ffe5bb5772b6641b50e\",\"sha256\":\"ef433f466fd7ee8ce78ebbbd08f0f6f89787eda8127757dfc8be3ebc813c4361\"},\"dob\":{\"date\":\"1982-03-24T02:48:33.012Z\",\"age\":39},\"registered\":{\"date\":\"2009-06-15T04:50:10.219Z\",\"age\":12},\"phone\":\"30870345\",\"cell\":\"84969297\",\"id\":{\"name\":\"CPR\",\"value\":\"240382-8858\"},\"picture\":{\"large\":\"https://randomuser.me/api/portraits/women/93.jpg\",\"medium\":\"https://randomuser.me/api/portraits/med/women/93.jpg\",\"thumbnail\":\"https://randomuser.me/api/portraits/thumb/women/93.jpg\"},\"nat\":\"DK\"}," +
            //    "{\"gender\":\"female\",\"name\":{\"title\":\"Mrs\",\"first\":\"Maggria\",\"last\":\"Kristensen\"},\"location\":{\"street\":{\"number\":1882,\"name\":\"Kratvej\"},\"city\":\"Vesterborg\",\"state\":\"Midtjylland\",\"country\":\"Denmark\",\"postcode\":96618,\"coordinates\":{\"latitude\":\"-35.6598\",\"longitude\":\"75.5195\"},\"timezone\":{\"offset\":\"+1:00\",\"description\":\"Brussels, Copenhagen, Madrid, Paris\"}},\"email\":\"maria.kristensen@example.com\",\"login\":{\"uuid\":\"a68737e5-f302-4f4c-8809-d5b6f50ec047\",\"username\":\"organicsnake428\",\"password\":\"787878\",\"salt\":\"lbj9sUsG\",\"md5\":\"1beb585975789dcb073a6a985939c031\",\"sha1\":\"e786a7720ee7e6c872430ffe5bb5772b6641b50e\",\"sha256\":\"ef433f466fd7ee8ce78ebbbd08f0f6f89787eda8127757dfc8be3ebc813c4361\"},\"dob\":{\"date\":\"1982-03-24T02:48:33.012Z\",\"age\":39},\"registered\":{\"date\":\"2009-06-15T04:50:10.219Z\",\"age\":12},\"phone\":\"30870345\",\"cell\":\"84969297\",\"id\":{\"name\":\"CPR\",\"value\":\"240382-8858\"},\"picture\":{\"large\":\"https://randomuser.me/api/portraits/women/93.jpg\",\"medium\":\"https://randomuser.me/api/portraits/med/women/93.jpg\",\"thumbnail\":\"https://randomuser.me/api/portraits/thumb/women/93.jpg\"},\"nat\":\"DK\"}]}";

            dynamic data = null;

            if (!string.IsNullOrEmpty(json_data))
            {
                data = JObject.Parse(json_data);
            }

            if (data != null)
            {
                for (int i = 0; i < data.results.Count; i++)
                {
                    User user = new User();

                    DateTime dateN = Convert.ToDateTime(data.results[i].dob.date);

                    user.UserID = i + 1;
                    user.Abbreviation = data.results[i].name.title;
                    user.FisrtName = data.results[i].name.first;
                    user.LastName = data.results[i].name.last;
                    user.Age = data.results[i].dob.age;
                    user.DateOfBirth = dateN.Date.ToLongDateString();
                    user.Gender = data.results[i].gender;
                    user.Email = data.results[i].email;
                    user.Phone = data.results[i].phone;
                    user.Picture = data.results[i].picture.medium;
                    user.PictureLg = data.results[i].picture.large;

                    users.Add(user);
                }
            }

            items = new List<string>();

            //var img = FindViewById<ImageView>(Resource.Id.imgUser);

            //foreach (var x in users)
            //{
            //    img?.SetImageURI(Android.Net.Uri.Parse("https://randomuser.me/api/portraits/med/women/93.jpg"));

            //    items.Add(x.FisrtName + " " + x.LastName);
            //}

            //ArrayAdapter<string> adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, items);

            //listView.Adapter = adapter;

            listView = FindViewById<ListView>(Resource.Id.userList);

            var adapter = new CustomAdapter(this, users);
            listView.Adapter = adapter;

            listView.ItemClick += ListView_ItemClick;
        }

        private void ListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var position = e.Position;
            var user = users[position] as User;

            Intent intent = new Intent(this, typeof(UserDetailActivity));
            intent.PutExtra("userSelected", JsonConvert.SerializeObject(user));
            StartActivity(intent);
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            GetUsersFromJSON();

            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_user);

            var btnBack = FindViewById<Button>(Resource.Id.btnBackUser);
            var btnRefresh = FindViewById<Button>(Resource.Id.btnRefresh);

            btnBack.Click += BtnBack_Click;
            btnRefresh.Click += BtnRefresh_Click;
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            GetUsersFromJSON();
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(MainActivity));
            StartActivity(intent);
        }
    }
}