using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using web_trazabilidad.Models.Ayudante;
using web_trazabilidad.Models.Negocio.Catalogos;

namespace web_trazabilidad.Models.Negocio.Operaciones
{
    public class Cls_Ope_Eventos_Vehiculo_Participante_Negocio 
            : Cls_Auditoria
    {
        public int? Vehiculo_Participante_Id { get; set; }
        public int? Vehiculo_Id { get; set; }
        public int? Evento_Id { get; set; }
        public int? Categoria_Id { get; set; }
        public int? Categoria_Participante_Id { get; set; }
        public int? Numero_Participante { get; set; }
        public int? Numero_Registro { get; set; }
        public String Estatus { get; set; }
        public Boolean? Revision_Mecanica { get; set; }
        public String Comentario { get; set; }

        //  datos del vehiculo (consulta)
        public String NS { get; set; }
        public String Marca { get; set; }
        public String Modelo { get; set; }
        public decimal? Año { get; set; }
        public String Placas { get; set; }
        public string Color_Hex_Rgb { get; set; }
        public string Color_Fondo_Hex_Rgb { get; set; }
        public String Categoria { get; set; }
        public String vehiculo_cmb { get; set; }
        public String categoria_id_cmb { get; set; }
        public String categoria_participante_cmb { get; set; }

        public String Participante_Piloto_Id_Cmb { get; set; }
        public String Piloto { get; set; }
        public String Participante_Copiloto_Id_Cmb { get; set; }


        public String text { get; set; }
        public String text2 { get; set; }
        public String text3 { get; set; }
        public String text_categoria { get; set; }
        public String text_categoriaParticipante { get; set; }
        public String tbl_datos_reorden { get; set; }
    }

    public class Cls_Ope_Eventos_Vehiculo_Datos_Vehiculo
        : Cls_Vehiculos_Negocios
    {
    }

    public class Cls_Ope_Eventos_Vehiculo_Datos_Piloto
        : Cls_Cat_Participantes_Negocio
    {
        public int? Participante_Piloto_Id { get; set; }
        public Boolean? Revision_Medica_Piloto { get; set; }
        public String Comentario_Piloto { get; set; }
        public String text { get; set; }


    }

    public class Cls_Ope_Eventos_Vehiculo_Datos_Copiloto
        : Cls_Cat_Participantes_Negocio
    {
        public int? Participante_Copiloto_Id { get; set; }
        public Boolean? Revision_Medica_Copiloto { get; set; }
        public String Comentario_Copiloto { get; set; }
        public String text { get; set; }

    }


    public class Cls_Ope_Eventos_Vehiculo_Arreglo
    {
        public int? id { get; set; }
        public int? no { get; set; }

    }
}