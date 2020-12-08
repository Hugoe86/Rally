using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_trazabilidad.Models.Negocio.Trazabilidad
{
    public class Cls_Cat_Lista_Contactos_Negocio
    {
       public int Contacto_ID { get; set; }
       public int Cliente_ID { get; set; }
       public int Empresa_ID { get; set; }
       public int Sucursal_ID { get; set; }
       public string Nombre { get; set; }
       public string Departamento { get; set; }
       public string Telefono { get; set; }
       public string Extension { get; set; }
       public string Celular { get; set; }
       public string Email { get; set; }
       public string Estatus { get; set; }
       public string Tipo { get; set; }
       public string Usuario_Creo { get; set; }
       public DateTime Fecha_Creo { get; set; }
       public string Usuario_Modifico { get; set; }
       public DateTime Fecha_Modifico { get; set; }
       public string Cliente { get; set; }
    }
}