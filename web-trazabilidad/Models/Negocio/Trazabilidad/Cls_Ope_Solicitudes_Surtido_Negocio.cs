using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_trazabilidad.Models.Negocio
{
    public class Cls_Ope_Solicitudes_Surtido_Negocio
    {
        public int Producto_ID { get; set; }
        public int Almacen_ID { get; set; }
        public int Ubicacion_ID { get; set; }
        public string Codigo { get; set; }
        public string Producto { get; set; }
        public string Alm_Sol { get; set; }
        public string Ubic_Sol { get; set; }
        public string Almacen_Rea { get; set; }
        public string Ubicacion_Rea { get; set; }
        public string Estatus { get; set; }
        public decimal Max { get; set; }
        public decimal Min { get; set; }
        public decimal PR { get; set; }
        public double Existencia { get; set; }
        public string Tipo_Surt { get; set; }
        public int Ubic_Rea { get; set; }
        public int Alm_Rea { get; set; }
        public int No_Solicitud { get; set; }
        public DateTime Fecha_Solicitud { get; set; }
        public string Fecha_Inicio { get; set; }
        public string Fecha_Termino { get; set; }
    }
}