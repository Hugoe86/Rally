using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_trazabilidad.Models.Ayudante.Util
{
    [AttributeUsage(AttributeTargets.Property)]
    public class NameAttribute : Attribute
    {
        private string name;

        public string Name { get { return name; } set { name = value; } }

        public NameAttribute(string name)
        {
            this.name = name;
        }
    }
}