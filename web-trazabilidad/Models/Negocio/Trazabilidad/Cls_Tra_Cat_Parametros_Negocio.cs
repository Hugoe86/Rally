using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_trazabilidad.Models.Negocio
{
    public class Cls_Tra_Cat_Parametros_Negocio
    {
        public int Parametro_ID { get; set; }
        public int? EP_Ubicacion_ID { get; set; }
        public int Empresa_ID { get; set; }
        public string Tipo_Usuario { get; set; }
        public string Prefijo { get; set; }
        public string No_Intentos_Acceso { get; set; }
        public int? Menu_ID { get; set; }
        public string Pedir_Confirmacion { get; set; }
        public string Mostrar_Panel { get; set; }
        public string URL_LINK { get; set; }
        public string Generar_Contenedor { get; set; }
        public int? UM_ID_Serie { get; set; }
        public int? Almacen_Entrada { get; set; }
        public int? Cuarentena_Ubicacion_ID { get; set; }
        public int? Almacen_Cuarentena { get; set; }

        public int? Alm_Emb_Ubicacion_ID { get; set; }
        public int? Emb_Ubicacion_ID { get; set; }
        public bool Tiene_Alm_Embarque { get; set; }
        public bool Permitir_Neg_Produccion { get; set; }
        public bool Permitir_Neg_Embarques { get; set; }
        public string Generar_No_Lote { get; set; }
        public int? Cant_Dec_Redondear { get; set; }
        public int? Rol_Val_Calidad { get; set; }
        public string Titulo_Reporte { get; set; }
        public string Subtitulo_Reporte { get; set; }
        public string Path_Write_File_Cashdro { get; set; }
        public string Path_Read_File_Cashdro { get; set; }
        public string Email { get; set; }
        public string Contrasena { get; set; }
        public int? Puerto { get; set; }
        public string Host { get; set; }
        public bool UseDefaultCredentials { get; set; }
        public bool EnableSsl { get; set; }
        public string Tener_OrdProd_Ubicacion { get; set; }
        public bool Modificar_Precio_OC { get; set; }
        public bool Modificar_Tipo_Impuesto_OC { get; set; }
        public int? Categoria_Bebida_ID { get; set; }
        public int? Categoria_Combo_ID { get; set; }
        public bool Habilitar_Contenerdor_No_Piezas { get; set; }
        public int? Ubicacion_Produccion_ID { get; set; }
        public int? Almacen_Produccion_ID { get; set; }
        public int? Categoria_Paquete_ID { get; set; }
        public int? U_Medida_Sol_ID { get; set; }
        public int? Compra_Impuesto_ID { get; set; }
        public int? Rubro_Material_ID { get; set; }
        public string Rubro_Material { get; set; }
        public int? Rubro_Mano_Obra_ID { get; set; }
        public string Rubro_Mano_Obra { get; set; }
        public int? Rubro_Otros_Gastos_ID { get; set; }
        public string Rubro_Otros_Gastos { get; set; }
        public decimal? Porc_Excede_Costo_OC { get; set; }
        public int? Division_Solicitud_RQ_ID { get; set; }
    }
}