using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_trazabilidad.Models.Negocio.Trazabilidad
{
    public class Cls_Ope_Pos_Paquete_Detalles_Negocio
    {
        public int No_Paquete_Detalle { get; set; }
        public int No_Paquete { get; set; }
        public string Descripcion { get; set; }
        public int Cantidad { get; set; }
        public string Estatus { get; set; }
        public Nullable<System.DateTime> Fecha_Inicio_Vigencia { get; set; }
        public Nullable<System.DateTime> Fecha_Fin_Vigencia { get; set; }
        public Nullable<bool> Obligatorio { get; set; }
        public Nullable<bool> Obtener_Precio_Producto { get; set; }
        public string Usuario_Creo { get; set; }
        public Nullable<System.DateTime> Fecha_Creo { get; set; }
        public string Usuario_Modifico { get; set; }
        public Nullable<System.DateTime> Fecha_Modifico { get; set; }
        public string Detalles { get; set; }
    }
}