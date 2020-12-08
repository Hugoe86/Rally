using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_trazabilidad.Models.Negocio.Trazabilidad
{
    public class Cls_Tra_Ope_Historico_Conteo_Inventarios_Negocio
    {
        public int No_His_Inventario { get; set; }
        public int Empresa_ID { get; set; }
        public int Sucursal_ID { get; set; }
        public int Almacen_ID { get; set; }
        public int Ubicacion_ID { get; set; }
        public int Producto_ID { get; set; }
        public double Existencia { get; set; }
        public double Cantidad_Fisica { get; set; }
        public double Diferencia { get; set; }
        public string Estatus { get; set; }
        public DateTime Fecha { get; set; }
        public string Usuario_Creo { get; set; }
        public DateTime Fecha_Creo { get; set; }
        public string Usuario_Modifico { get; set; }
        public DateTime Fecha_Modifico { get; set; }
        
        public string Usuario { get; set; }
        public string FechaStr
        {
            get
            {
                return $"{this.Fecha.ToString("dd/MM/yyyy")}";
            }
        }
        public bool EsTerminado { get; set; }
    }

    public class Cls_Estatus_Historico_Conteo_Inventarios
    {
        public const string Borrador = "Borrador";
        public const string Terminado = "Terminado";
    }

    public class Cls_Existencia_Inventario
    {
        public int Empresa_ID { get; set; }
        public int Sucursal_ID { get; set; }
        public int Almacen_ID { get; set; }
        public int Ubicacion_ID { get; set; }
        public int Producto_ID { get; set; }
        public double Cantidad { get; set; }
    }
}
