using datos_trazabilidad;
using Elmah;
using LitJson;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.Script.Services;
using System.Web.Services;
using web_trazabilidad.Models.Ayudante;
using web_trazabilidad.Models.Negocio;
using web_trazabilidad.Models.Negocio.Catalogos;
using web_trazabilidad.Models.Negocio.Operaciones;
using web_trazabilidad.Models.Negocio.Reportes;
using System.Data.Common;
using System.Dynamic;
using System.Data.SqlClient;
using System.ComponentModel;

namespace web_trazabilidad.Paginas.Reportes.controllers
{
    /// <summary>
    /// Summary description for RptEventosController
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class RptEventosController : System.Web.Services.WebService
    {


        /// <summary>
        /// Metodo que sive para leer una lista de diccionario de datos
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        private static List<Dictionary<string, object>> read(DbDataReader reader)
        {
            List<Dictionary<string, object>> Lista = new List<Dictionary<string, object>>();

            foreach (var item in reader)
            {
                IDictionary<string, object> expando = new ExpandoObject();

                foreach (PropertyDescriptor propertyDescriptor in TypeDescriptor.GetProperties(item))
                {
                    var obj = propertyDescriptor.GetValue(item);
                    expando.Add(propertyDescriptor.Name, obj);
                }

                Lista.Add(new Dictionary<string, object>(expando));
            }

            return Lista;
        }




        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string Consultar_Reporte(string jsonObject)
        {
            string Json_Resultado = string.Empty;
            Cls_Rpt_Eventos_Negocio Obj = new Cls_Rpt_Eventos_Negocio();
            List<Cls_Rpt_Eventos_Negocio> List_Reporte = new List<Cls_Rpt_Eventos_Negocio>();
            List<Dictionary<string, object>> Lista = new List<Dictionary<string, object>>();
            SqlParameter sqlEvento_Id;
            SqlParameter sqlColumnas;
            SqlParameter sqlColumnas_Null;
            SqlParameter sqlColumnas_Grupo;
            String Columnas = "";
            String Columnas_Null = "";
            String Columnas_Grupo = "";
            DataTable DtResponse = new DataTable();

            try
            {
                Obj = JsonConvert.DeserializeObject<Cls_Rpt_Eventos_Negocio>(jsonObject);


                using (var dbContext = new Sistema_TrazabilidadEntities())
                {
                    var _jornadas_activas = (from _jornada in dbContext.Ope_Eventos_Jornadas
                                           where _jornada.Evento_Id == Obj.Evento_Id
                                           && _jornada.Estatus == "ACTIVO"

                                           select new Cls_Ope_Eventos_Jornadas_Negocio
                                           {
                                               Jornada_Id = _jornada.Jornada_Id,
                                               Clave = _jornada.Clave,
                                           }).ToList();

                    Columnas = "";
                    Columnas_Null = "";
                    Columnas_Grupo = "";

                    //  <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
                    //  <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
                    if (_jornadas_activas.Any())
                    {
                        Int32 cont_registros = 1;

                        foreach (var registro in _jornadas_activas)
                        {
                            Columnas += "[" + registro.Jornada_Id + "],";
                            Columnas_Null += ", sum(isnull([" + registro.Jornada_Id + "], 0)) as [x" + registro.Clave + "]";

                            cont_registros++;
                        }


                        //  se quita la ultima coma
                        Columnas = Columnas.Remove(Columnas.Length - 1);

                        sqlEvento_Id = new SqlParameter("@Evento_Id", Obj.Evento_Id);
                        sqlColumnas = new SqlParameter("@Columnas", Columnas);
                        sqlColumnas_Null = new SqlParameter("@Columnas_Null", Columnas_Null);

                        

                        using (var command = dbContext.Database.Connection.CreateCommand())
                        {
                            dbContext.Database.Connection.Open();
                            command.CommandText = "SP_Rpt_Por_Evento";
                            command.CommandType = CommandType.StoredProcedure;

                            command.Parameters.Add(sqlEvento_Id);
                            command.Parameters.Add(sqlColumnas);
                            command.Parameters.Add(sqlColumnas_Null);

                            using (var reader = command.ExecuteReader())
                            {
                                Lista = read(reader).ToList();

                                dbContext.Database.Connection.Close();
                            }


                            DtResponse.Clear();
                            DtResponse = Cls_Metodos_Generales.ToDataTable_List_Dict(Lista);
                            List_Reporte = Cls_Metodos_Generales.DataTableToList<Cls_Rpt_Eventos_Negocio>(DtResponse);


                            if (Obj.Categoria_Id > 0) {

                                List_Reporte = List_Reporte.Where(x => x.Categoria_Id.Equals(Obj.Categoria_Id)).ToList();
                            }

                            if (Obj.Categoria_Participante_Id > 0)
                            {

                                List_Reporte = List_Reporte.Where(x => x.Categoria_Participante_Id.Equals(Obj.Categoria_Participante_Id)).ToList();
                            }

                            Json_Resultado = JsonMapper.ToJson(List_Reporte);

                        }
                    }
                    //  <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
                    //  <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
                }
            }
            catch (Exception e)
            {
                
            }

            return Json_Resultado;
        }



        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string Consultar_Numero_Jornadas(string jsonObject)
        {
            string Json_Resultado = string.Empty;
            Cls_Rpt_Etapas_Negocio Obj = new Cls_Rpt_Etapas_Negocio();

            try
            {
                Obj = JsonConvert.DeserializeObject<Cls_Rpt_Etapas_Negocio>(jsonObject);


                using (var dbContext = new Sistema_TrazabilidadEntities())
                {
                    var _consulta = (from _jornada in dbContext.Ope_Eventos_Jornadas
                                     where _jornada.Evento_Id == Obj.Evento_Id
                                     && _jornada.Estatus == "ACTIVO"

                                     select new Cls_Ope_Eventos_Jornadas_Negocio
                                     {
                                         Jornada_Id = _jornada.Jornada_Id,
                                         Clave = _jornada.Clave

                                     }).OrderBy(o => o.Clave).ToList();



                    Json_Resultado = JsonMapper.ToJson(_consulta);


                }
            }
            catch (Exception e)
            {

            }

            return Json_Resultado;
        }
    }
}
