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
    /// Summary description for EtapaDiaController
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class EtapaDiaController : System.Web.Services.WebService
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
            Cls_Rpt_Etapas_Negocio Obj = new Cls_Rpt_Etapas_Negocio();
            List<Cls_Rpt_Etapas_Negocio> List_Reporte = new List<Cls_Rpt_Etapas_Negocio>();
            List<Dictionary<string, object>> Lista = new List<Dictionary<string, object>>();
            SqlParameter sqlEvento_Id;
            SqlParameter sqlJornada_Id;
            SqlParameter sqlColumnas;
            SqlParameter sqlColumnas_Null;
            String Columnas = "";
            String Columnas_Null = "";
            DataTable DtResponse = new DataTable();
            Int32 Total = 0;
            Int32 Cont_For = 0;
             
            try
            {
                Obj = JsonConvert.DeserializeObject<Cls_Rpt_Etapas_Negocio>(jsonObject);


                using (var dbContext = new Sistema_TrazabilidadEntities())
                {
                    var _puntos_control = (from _pts in dbContext.Ope_Eventos_Puntos_Control
                                           where _pts.Evento_Id == Obj.Evento_Id
                                           && _pts.Jornada_Id == Obj.Jornada_Id
                                           && _pts.Estatus == "ACTIVO"

                                           select new Cls_Ope_Eventos_Puntos_Control_Negocio
                                           {
                                               Numero = _pts.Numero,
                                           }).ToList();

                    Columnas = "";
                    Columnas_Null = "";
                    //  <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
                    //  <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
                    if (_puntos_control.Any())
                    {
                        foreach (var registro in _puntos_control)
                        {
                            Columnas += "[" + registro.Numero + "],";
                            Columnas_Null += ",isnull([" + registro.Numero + "], 0)[x" + registro.Numero + "]";

                        }


                        //  se quita la ultima coma
                        Columnas = Columnas.Remove(Columnas.Length - 1);

                        sqlEvento_Id = new SqlParameter("@Evento_Id", Obj.Evento_Id);
                        sqlJornada_Id = new SqlParameter("@Jornada_Id", Obj.Jornada_Id);
                        sqlColumnas = new SqlParameter("@Columnas", Columnas);
                        sqlColumnas_Null = new SqlParameter("@Columnas_Null", Columnas_Null);

                        using (var command = dbContext.Database.Connection.CreateCommand())
                        {
                            dbContext.Database.Connection.Open();
                            command.CommandText = "SP_Rpt_Por_Etapa";
                            command.CommandType = CommandType.StoredProcedure;

                            command.Parameters.Add(sqlEvento_Id);
                            command.Parameters.Add(sqlJornada_Id);
                            command.Parameters.Add(sqlColumnas);
                            command.Parameters.Add(sqlColumnas_Null);

                            using (var reader = command.ExecuteReader())
                            {
                                Lista = read(reader).ToList();

                                dbContext.Database.Connection.Close();
                            }


                            DtResponse.Clear();
                            DtResponse = Cls_Metodos_Generales.ToDataTable_List_Dict(Lista);
                            List_Reporte = Cls_Metodos_Generales.DataTableToList<Cls_Rpt_Etapas_Negocio>(DtResponse);


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
        public string Consultar_Numero_Puntos_Control(string jsonObject)
        {
            string Json_Resultado = string.Empty;
            Cls_Rpt_Etapas_Negocio Obj = new Cls_Rpt_Etapas_Negocio();

            try
            {
                Obj = JsonConvert.DeserializeObject<Cls_Rpt_Etapas_Negocio>(jsonObject);


                using (var dbContext = new Sistema_TrazabilidadEntities())
                {
                    var _puntos_control = (from _pts in dbContext.Ope_Eventos_Puntos_Control
                                           where _pts.Evento_Id == Obj.Evento_Id
                                           && _pts.Jornada_Id == Obj.Jornada_Id
                                           && _pts.Estatus == "ACTIVO"

                                           select new Cls_Ope_Eventos_Puntos_Control_Negocio
                                           {
                                               Numero = _pts.Numero,

                                           }).OrderBy(o => o.Numero).ToList();



                    Json_Resultado = JsonMapper.ToJson(_puntos_control);


                }
            }
            catch (Exception e)
            {

            }

            return Json_Resultado;
        }
    }
}
