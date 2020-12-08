using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_trazabilidad.Models.Negocio.Trazabilidad
{
    public class Cls_Cat_Corporativos_Negocio
    {
     public int Corporativo_ID { get; set; }
	 public int Empresa_ID { get; set; }
	 public int Sucursal_ID { get; set; }
     public string Clave { get; set; }
	 public string Nombre { get; set; }
	 public string Pais { get; set; }
	 public string Estado { get; set; }
	 public string Municipio { get; set; }
	 public string Ciudad { get; set; }
	 public string Localidad { get; set; }
	 public string Calle { get; set; }
	 public string Num_Int { get; set; }
	 public string Num_Ext { get; set; }
	 public string CP { get; set; }
	 public string RFC { get; set; }
	 public string Telefono { get; set; }
     public string Email { get; set; }
	 public string Observaciones { get; set; }
	 public string Usuario_Creo { get; set; }
	 public  DateTime Fecha_Creo { get; set; }
     public string Usuario_Modifico { get; set; }
	 public DateTime Fecha_Modifico { get; set; }
    public string Ruta_Imagen { get; set; }


    }
}