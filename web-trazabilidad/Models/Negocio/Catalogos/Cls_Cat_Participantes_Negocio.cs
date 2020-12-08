using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_trazabilidad.Models.Negocio.Catalogos
{
    public class Cls_Cat_Participantes_Negocio
    {
        public int Participante_ID { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public string Celular { get; set; }
        public string Notas { get; set; }
        public DateTime? Fecha_Nacimiento { get; set; }
        public string Sexo { get; set; }
        public string Direccion { get; set; }
        public string Colonia { get; set; }
        public string Nacionalidad { get; set; }
        public int Estado_ID { get; set; }
        public int Municipio_ID { get; set;}
        public string Estatus { get; set; }
        public string tbl_documentos { get; set; }
        public string tbl_documentos_eliminados { get; set; }
    }
}