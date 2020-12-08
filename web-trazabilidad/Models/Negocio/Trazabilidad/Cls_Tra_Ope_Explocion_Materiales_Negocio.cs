using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_trazabilidad.Models.Negocio
{
    public class Cls_Tra_Ope_Explocion_Materiales_Negocio
    {
        public  int No_Explosion_Material { get; set; }
        public int Empresa_ID { get; set; }
        public int Producto_ID { get; set; }
        public string Producto  { get; set; }
        public string Codigo { get; set; }
        public string Unidad { get; set; }
        public decimal Cantidad { set; get; }
        public string Tipo_Producto { get; set; }
        public int Producto_lista_ID { get; set; }
        public string Usuario_Creo { set; get; }
        public string Fecha_Creo { set; get; }
        public string Usuario_Modifico { set; get; }
        public string Fecha_Modifico { set; get; }
        

    }
}