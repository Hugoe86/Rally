using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_cambios_procesos.Models.Ayudante
{
    public class Cls_Respuesta
    {
        public string Titulo { get; internal set; }
        public bool? Estatus { get; set; }
        public string Mensaje { get; set; }
        public int? ID { get; set; }
        public object Nuevos { get; set; }
        public object Ya_Registrados { get; set; }
        public object Registros { get; set; }
        public object Errores { get; set; }
        public string Archivo_Url { get; set; }

    }
}