using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_trazabilidad.Models.Negocio.Catalogos
{
    public class Cls_Cat_Participantes_Adjuntos_Negocio
    {
        public int Adjunto_ID { get; set; }
        public int Participante_ID { get; set; }
        public string Nombre_Documento { get; set; }
        public string Nombre { get; set; }
        public string Ruta { get; set; }
        public string Estatus { get; set; }
        public string Usuario_Creo { get; set; }
        public DateTime? Fecha_Creo { get; set; }
        public string Usuario_Modifico { get; set; }
        public DateTime? Fecha_Modifico { get; set; }

    }
}