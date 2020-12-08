using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using web_trazabilidad.Models.Ayudante;

namespace web_trazabilidad.Models.Negocio.Catalogos
{
    public class Cls_Cat_Vehiculos_Documentos_Negocio: Cls_Auditoria
    {
        public int Documento_Id { get; set; }
        public int Vehiculo_Id { get; set; }
        public string Nombre { get; set; }
        public string Nombre_Documento { get; set; }
        public string Ruta { get; set; }
        public string Estatus { get; set; }

    }
}