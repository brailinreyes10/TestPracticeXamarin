using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppTest.Models
{
    public class User
    {
        public int UserID { get; set; }
        public string Abbreviation { get; set; }
        public string FisrtName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string Age { get; set; }
        public string DateOfBirth { get; set; }
        public string Phone { get; set; }
        public string Location { get; set; }
        public string Picture { get; set; }
        public string PictureLg { get; set; }
    }
}