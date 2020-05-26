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
    class MaterialServer
    {
        public Dictionary<string, object> Material { get; set; } = new Dictionary<string, object> {
            { "id", 1 },
            { "name", "材料名称" },
            { "remark", "备注备注备注备注备注备注备注备注备注备注备注备注备注备注备注备注备注备注备注备注备注备注备注" }
        };
    }
}