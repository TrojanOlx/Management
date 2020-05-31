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
    public class FieldServer
    {
        public class Field
        {
            public Field()
            {

            }
            public Field(int id, string name, string key)
            {
                Id = id;
                Name = name;
                Key = key;
            }

            public int Id { get; set; }
            public string Name { get; set; }
            public string Key { get; set; }

        }

        public List<Field> Fields { get; set; }

        public List<Field> GetFields()
        {
            List<Field> fields = new List<Field>();

            fields.Add(new Field(1,"编号","id"));
            fields.Add(new Field(2, "名称", "name"));
            fields.Add(new Field(1, "备注", "remark"));
            fields.Add(new Field(1, "编号", "id"));
            fields.Add(new Field(2, "名称", "name"));
            fields.Add(new Field(1, "备注", "remark"));
            fields.Add(new Field(1, "编号", "id"));
            fields.Add(new Field(2, "名称", "name"));
            fields.Add(new Field(1, "备注", "remark"));
            fields.Add(new Field(1, "编号", "id"));
            fields.Add(new Field(2, "名称", "name"));
            fields.Add(new Field(1, "备注", "remark"));

            Fields = fields;
            return fields;
        }



    }





}