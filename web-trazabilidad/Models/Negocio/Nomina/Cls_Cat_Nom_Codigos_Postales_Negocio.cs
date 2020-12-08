using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_trazabilidad.Models.Negocio.Nomina
{
    public class Cls_Cat_Nom_Codigos_Postales_Negocio
    {
        public int Codigo_Postal_ID { get; set; }
        public int Estado_ID { get; set; }
        public string Estado { get; set; }
        public string Clave_SAT { get; set; }
        public string Clave_Municipio { get; set; }
        public string Clave_Localidad { get; set; }
        public string Estatus { get; set; }
        public string Comentarios { get; set; }
        public string Usuario_Creo { get; set; }
        public DateTime? Fecha_Creo { get; set; }
        public string Usuario_Modifico { get; set; }
        public DateTime? Fecha_Modifico { get; set; }
        public string Codigo_Postal { get; set; }
        public int Localidad_ID { get; set; }
        public string Localidad { get; set; }
        public int Municipio_Localidad_ID { get; set; }
    }
}