using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_trazabilidad.Models.Negocio
{
    public class Issue
    {
        public Fields fields { get; set; }
        public Issue()
        {
            fields = new Fields();
        }
    }
}