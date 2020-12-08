using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using web_trazabilidad.Models.Ayudante;

namespace web_trazabilidad.Models.Negocio.Catalogos 
{
    public class Cls_Vehiculos_Negocios  : Cls_Auditoria
    {
        public int? Vehiculo_Id { get; set; }
        public String NS { get; set; }
        public decimal Año { get; set; }
        public String Marca { get; set; }
        public String Estatus { get; set; }
        public String Modelo { get; set; }
        public String Placas { get; set; }
        public String Notas { get; set; }
        public String Compañia { get; set; }
        public String Numero_Poliza { get; set; }
        public DateTime? Vigencia_Inicial { get; set; }
        public DateTime? Vigencia_Final { get; set; }
        public string Color_Hex_Rgb { get; set; }
        public string Color_Fondo_Hex_Rgb { get; set; }

        public string tbl_documentos { get; set; }
        public string tbl_documentos_eliminados { get; set; }
    }
}