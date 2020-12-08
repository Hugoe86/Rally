using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_trazabilidad.Models.Negocio
{
    public class Cls_Tra_Cat_Productos_Negocio
    {
        public int Empresa_ID { get; set; }
        public int Producto_ID { get; set; }
        //public int Subproceso_ID { get; set; }
        public int Estatus_ID { get; set; }
        //public Nullable<int> Unidad_ID { get; set; }
        public int? Tipo_Producto_ID { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Observaciones { get; set; }
        public string Usuario_Creo { get; set; }
        public System.DateTime Fecha_Creo { get; set; }
        public string Usuario_Modifico { get; set; }
        public DateTime? Fecha_Modifico { get; set; }
        public double Costo_Promedio { get; set; }
        public double Ultimo_Costo { get; set; }
        public string Volumen { get; set; }
        public string Peso { get; set; }
        public byte[] Imagen { get; set; }
        public int? UnidadPeso_ID { get; set; }
        public string Unidad_Peso { get; set; }
        public int? UnidadVolumen_ID { get; set; }
        public string Unidad_Volumen { get; set; }
        public int ControlStock_ID { get; set; }
        public int UnidadAlmacenamiento_ID { get; set; }
        public string Unidad_Almacenamiento { get; set; }
        public int UnidadCompra_ID { get; set; }
        public int? Categoria_ID { get; set; }
        public int? Categoria_Pv_ID { get; set; }
        public string Categoria { get; set; }
        public string Tipo_Stock { get; set; }
        public string Control_Stock { get; set; }
        public string Tipo_Producto { get; set; }
        public string Estatus { get; set; }
        public string Unidad_Compra { get; set; }
        public int? Producto_ID_Sat { get; set; }
        public int? Unidad_Longitud_ID { get; set; }
        public int? Clasificacion_Bom_ID { get; set; }
        public double Longitud { get; set; }
        public double Ancho { get; set; }
        public int? Unidad_Ancho_ID { get; set; }
        public decimal? Primer_Costo { get; set; }
        public decimal? Costo_Estandar { get; set; }


        /*Solo utilizado para una consulta*/
        public double Cantidad_Producir { get; set; }
        public int Ubicacion_ID { get; set; }
        public string Color { get; set; }
        public string No_Parte_Cliente { get; set; }
        public int Empaque_ID { get; set; }
        public string Tiene_Fecha_Expiracion { get; set; }
        public int? Dias_Expiracion { get; set; }
        public string Filtro { get; set; }
        public int? Tipo_Impuesto_ID { get; set; }
        public double Precio { get; set; }
        public double Impuesto { get; set; }
        public double Cantidad_Producida { get; set; }
        public double Porcentaje_Rendimiento { get; set; }

        public int? OC_Tipo_Impuesto_ID { get; set; }
       
        public bool Tiene_Costo_Ad_Cmb { get; set; }
        public double Costo_Ad_Cmb { get; set; }

        public int? Familia_ID { get; set; }
        public double Costo_Compra { get; set; }
        public int? Familia_Costo_ID { get; set; }
        public double? Costo { get; set; }
        public int? Cuenta_Contable_ID { get; set; }
    }
}