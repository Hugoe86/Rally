using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_trazabilidad.Models.Negocio.Trazabilidad
{
    public class Cls_Explosion_Materiales_Detalles_Cdis_Negocio
    {
        public int Index { get; set; }
        public int Exp_Mat_Det { get; set; }
        public int Exp_Mat { get; set; }
        public int Producto_ID { get; set; }
        public int Unidad_ID { get; set; }
        public int? Familia_ID { get; set; }
        public string Familia { get; set; }
        public string Codigo { get; set; }
        public string Producto { get; set; }
        public decimal? Cant_Rec { get; set; }
        public decimal? Rendimiento { get; set; }
        public decimal? Cant_Unidad { get; set; }
        public decimal? Cant_Prod { get; set; }
        public decimal? Cant_Usar { get; set; }
        public string CUnidad { get; set; }
        public string Unidad { get; set; }
        public double Existencia { get; set; }
        public int Relacion { get; set; }
        public string Detalles { get; set; }
        public int No_Orden_Produccion { get; set; }

        public decimal Cant_Sin_Rend { get; set; }
        public decimal Cant_Rend { get; set; }
        public decimal Cant_Utilizada { get; set; }
        public string Estatus { get; set; }
        public decimal Cant_Receta { get; set; }
        public decimal Cant { get; set; }
    }
}