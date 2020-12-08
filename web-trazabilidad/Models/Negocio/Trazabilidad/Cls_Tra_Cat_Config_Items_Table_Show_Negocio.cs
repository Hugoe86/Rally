using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_trazabilidad.Models.Negocio.Trazabilidad
{
    public class Cls_Tra_Cat_Config_Items_Table_Show_Negocio
    {
        public int Config_ID { get; set; }
        public int Empresa_ID { get; set; }
        public int Sucursal_ID { get; set; }
        public string Pagina { get; set; }
        public string Tabla { get; set; }
        public string ID { get; set; }
        public int ID_Value { get; set; }
        public string Usuario_Creo { get; set; }
        public DateTime Fecha_Creo { get; set; }
        public string Usuario_Modifico { get; set; }
        public DateTime fecha_Modifico { get; set; }
        public string NombrePK { get; set; }
        public string Descripcion { get; set; }
    }
}