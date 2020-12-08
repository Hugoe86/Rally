using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_trazabilidad.Models.Ayudante.Util
{
    [AttributeUsage(AttributeTargets.Property)]
    public class FormatPdfAttribute : Attribute
    {
        private string format;

        public string Format { get { return format; } set { format = value; } }

        public FormatPdfAttribute(string format)
        {
            this.format = format;
        }
    }
}