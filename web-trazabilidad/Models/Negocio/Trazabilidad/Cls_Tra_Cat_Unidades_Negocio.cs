using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace web_trazabilidad.Models.Negocio
{
    class Cls_Tra_Cat_Unidades_Negocio
    {
        public int Unidad_ID { set; get; }
        public string Nombre { set; get; }
        public string Grupo { set; get; }
        public string Usuario_Creo { set; get; }
        public string Fecha_Creo { set; get; }
        public string Usuario_Modifico { set; get; }
        public string Fecha_Modifico { set; get; }
    }
}
