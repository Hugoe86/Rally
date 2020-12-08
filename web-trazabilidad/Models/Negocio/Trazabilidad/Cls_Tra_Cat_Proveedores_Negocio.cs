using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_trazabilidad.Models.Negocio
{
    public class Cls_Tra_Cat_Proveedores_Negocio
    {
        public int Empresa_ID { set; get; }
        public int Proveedor_ID { get; set; }
        public int Estatus_ID { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public string Observaciones { get; set; }
        public string Usuario_Creo { set; get; }
        public string Fecha_Creo { set; get; }
        public string Usuario_Modifico { set; get; }
        public string Fecha_Modifico { set; get; }
        public string Contacto_Nombre { set; get; }
        public string Contacto_Telefono { set; get; }
        public string Contacto_Email { set; get; }
        public bool Lunes { get; set; }
        public bool Martes { get; set; }
        public bool Miercoles { get; set; }
        public bool Jueves { get; set; }
        public bool Viernes { get; set; }
        public bool Sabado { get; set; }
        public bool Domingo { get; set; }
        public int? Cuenta_Contable_ID { get; set; }
        public string Cuenta_Contable_Nombre { get; set; }
        public string Celular { get; set; }
        public string RFC { get; set; }
        public string Extension { get; set; }
        public int? Pais_ID { get; set; }
        public string Pais { get; set; }
        public int? Estado_ID { get; set; }
        public string Estado { get; set; }
        public int? Municipio_Localidad_ID { get; set; }
        public string Municipio { get; set; }
        public string Ciudad { get;set; }
        public int? Localidad_ID { get; set; }
        public string Localidad { get; set; }
        public string Colonia { get; set; }
        public string Calle { get; set; }
        public string Num_Ext { get; set; }
        public string Num_Int { get; set; }
        public int? Codigo_Postal_ID { get; set; }

    }
}