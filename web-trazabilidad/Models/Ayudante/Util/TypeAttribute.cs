using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_trazabilidad.Models.Ayudante.Util
{
    [AttributeUsage(AttributeTargets.Property)]
    public class TypeAttribute : Attribute
    {
        private Type type;

        public Type Type { get { return type; } set { type = value; } }

        public TypeAttribute(Type type)
        {
            this.type = type;
        }
    }
}