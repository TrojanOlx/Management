using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Management.Android.Models
{
    public class Fruit
    {
        public Fruit(string name, int imageId)
        {
            Name = name;
            ImageId = imageId;
        }

        public string Name { get; set; }
        public int ImageId { get; set; }
    }
}