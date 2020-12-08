using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_trazabilidad.Models.Negocio.Nomina
{
    public class Cls_Cat_Nom_Ciudades_Negocio
    {
        public int Ciudad_ID { get; set; }
        public int Municipio_Localidad_ID { get; set; }
        public string Clave_SAT { get; set; }
        public string Nombre { get; set; }
        public string Comentarios { get; set; }
        public string Estatus { get; set; }
        public string Usuario_Creo { get; set; }
        public DateTime Fecha_Creo { get; set; }
        public string Usuario_Modifico { get; set; }
        public DateTime Fecha_Modifico { get; set; }
        public string Municipio_Localidad { get; set; }
    }
}