using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_trazabilidad.Models.Negocio
{
    public class Cls_Ope_Punto_Reorden_SP_Negocio
    {
        public int Producto_ID { get; set; }
        public bool TienePuntoReorden { get; set; }
        public int? NoPuntoReorden { get; set; }
        public string Nombre { get; set; }
        public string Codigo { get; set; }
        public string Maximo { get; set; }
        public string Minimo { get; set; }
        public string PuntoReorden { get; set; }
        public int NoInventario { get; set; }
        public double Existencia { get; set; }
        public bool TieneUbicaciones { get; set; }
        public int CantidadUbicaciones { get; set; }
    }
}