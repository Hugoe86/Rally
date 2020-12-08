using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_trazabilidad.Models.Ayudante.Util
{
    [AttributeUsage(AttributeTargets.Property)]
    public class DataSimpleAttribute : Attribute
    {
        public DataSimpleAttribute()
        {

        }
    }
}