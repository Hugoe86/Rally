using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_trazabilidad.Models.Negocio
{
    public class Cls_Tra_Cat_Clientes_Negocio
    {
        public int Empresa_ID { set; get; }
        public int Cliente_ID { get; set; }
        public int Corporativo_ID { get; set; }
        public int? Pais_ID { get; set; } 
        public int? Estado_ID { get; set; }
        public int? Localidad_ID { get; set; }
        public int? Codigo_Postal_ID { get; set; }
        public int? Municipio_Localidad_ID { get; set; }
        public int? Ciudad_ID { get; set; }
        public int Estatus_ID { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public string Observaciones { get; set; }
        public string Estatus { get; set; }
        public string RFC { get; set; }
        public string Telefono { get; set; }
        public string Extension { get; set; }
        public string Celular { get; set; }
        public string Fax { get; set; }
        public string Nextel { get; set; }
        public string Email { get; set; }
        public string Usuario_Creo { set; get; }
        public string Fecha_Creo { set; get; }
        public string Usuario_Modifico { set; get; }
        public string Fecha_Modifico { set; get; }
        public int? Cuenta_Contable_ID { set; get; }

        //datos facturacion
        public string Pais_Facturacion { get; set; }
        public string Estado_Facturacion { get; set; }
        public string Municipio_Facturacion { get; set; }
        public string Ciudad_Facturacion { get; set; }
        public string Localidad_Facturacion { get; set; }
        public string Colonia_Facturacion { get; set; }
        public string Calle_Facturacion { get; set; }
        public string Num_Int_Facturacion { get; set; }
        public string Num_Ext_Facturacion { get; set; }
        public string CP_Facturacion { get; set; }

        //datos Embarque
        public string Pais_Embarque { get; set; }
        public string Estado_Embarque { get; set; }
        public string Municipio_Embarque { get; set; }
        public string Ciudad_Embarque { get; set; }
        public string Localidad_Embarque { get; set; }
        public string Colonia_Embarque { get; set; }
        public string Calle_Embarque { get; set; }
        public string Num_Int_Embarque { get; set; }
        public string Num_Ext_Embarque { get; set; }
        public int? CP_Embarque { get; set; }
        public decimal Kilometraje_Aproximado { get; set; }

        //contabilidad
        public int? Dias_Credito { get; set; }
        public double? Limite_Credito { get; set; }

        public string Clave_Pais_Receptor { get; set; }
       
    }
}