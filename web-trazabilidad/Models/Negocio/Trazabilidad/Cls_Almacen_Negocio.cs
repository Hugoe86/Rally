using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_trazabilidad.Models.Negocio.Trazabilidad
{
    public class Cls_Almacen_Negocio
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Prefijo { get; set; }
        public string TipoUbicacion { get; set; }
        public string TipoSurtido { get; set; }
        public string EmpresaId { get; set; }
        public string SucursalId { get; set; }
        public string NombrePrefijo
        {
            get
            {
                return $"{this.Prefijo} {this.Nombre}";
            }
        }

        public virtual IList<Cls_Ubicacion_Negocio> Ubicaciones { get; set; }
    }

    public class Cls_Ubicacion_Negocio
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Prefijo { get; set; }
        public string TipoUbicacion { get; set; }
        public string TipoSurtido { get; set; }
        public string NombrePrefijo
        {
            get
            {
                return $"{this.Prefijo} {this.Nombre}";
            }
        }
        public string Estatus { get; set; }

        public virtual IList<Cls_Producto_Negocio> Productos { get; set; }
    }

    public class Cls_Producto_Negocio
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Codigo { get; set; }
        public int Existencia { get; set; }
        public int CantidadFisica { get; set; }
        public int Diferencia { get; set; }
        public int No_His_Inventario { get; set; }
    }
}