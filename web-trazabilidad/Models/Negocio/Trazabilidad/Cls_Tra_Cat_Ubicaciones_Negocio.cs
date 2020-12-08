using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
namespace web_trazabilidad.Models.Negocio
{
    public class Cls_Tra_Cat_Ubicaciones_Negocio
    {
        public int Ubicacion_ID { set; get; }
        public string Prefijo { set; get; }
        public string Nombre { set; get; }
        public string Direccion { set; get; }
        public string CP { set; get; }
        public string Ciudad { set; get; }
        public string Telefono { set; get; }
        public string Entrada { set; get; }
        public string Salida { set; get; }
        public string Comentarios { set; get; }
        public string Usuario_Creo { set; get; }
        public string Fecha_Creo { set; get; }
        public string Usuario_Modifico { set; get; }
        public string Fecha_Modifico { set; get; }
        int Empresa_ID { set; get; }
        int Sucursal_ID { set; get; }

        public string Tipo_Ubicacion { get; set; }
        public Nullable<int> Almacen_ID { get; set; }
        public string Almacen { set; get; }

        public string  Tipo_Surtido { get; set; }

        public Nullable<int> Alm_Reabastecimiento { get; set; }
        public string Almacen_Reabastecimiento { set; get; }
        public Nullable<int> Ubicacion_Reabastecimiento { get; set; }
        public string Ubi_Reabastecimiento { set; get; }
        public string Volumen { get; set; }
        public string Peso { get; set; }
        public int? UnidadPeso_ID { get; set; }
        public string Unidad_Peso { get; set; }
        public int? UnidadVolumen_ID { get; set; }
        public string Unidad_Volumen { get; set; }

        public string Ubicacion_Completa { set; get; }

    }
}