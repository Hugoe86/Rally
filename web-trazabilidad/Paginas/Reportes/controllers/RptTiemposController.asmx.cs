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
    /// Summary description for RptTiemposController
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class RptTiemposController : System.Web.Services.WebService
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
            Cls_Rpt_Tiempos_Negocio Obj = new Cls_Rpt_Tiempos_Negocio();
            List<Cls_Rpt_Tiempos_Negocio> List_Reporte = new List<Cls_Rpt_Tiempos_Negocio>();
            List<Dictionary<string, object>> Lista = new List<Dictionary<string, object>>();
            SqlParameter sqlEvento_Id;
            SqlParameter sqlJornada_Id;
            SqlParameter sqlCategoria_Id;
            SqlParameter sqlCategoria_Participante_Id;
            DataTable DtResponse = new DataTable();

            try
            {
                Obj = JsonConvert.DeserializeObject<Cls_Rpt_Tiempos_Negocio>(jsonObject);


                using (var dbContext = new Sistema_TrazabilidadEntities())
                {
                    //  <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
                    //  <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
                    sqlEvento_Id = new SqlParameter("@Evento_Id", Obj.Evento_Id);
                    sqlJornada_Id = new SqlParameter("@Jornada_Id", Obj.Jornada_Id);

                    if (Obj.Categoria_Id > 0)
                    {
                        sqlCategoria_Id = new SqlParameter("@Categoria", Obj.Categoria_Id);
                    }
                    else
                    {
                        sqlCategoria_Id = new SqlParameter("@Categoria", DBNull.Value);
                    }

                    //  <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
                    //  <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
                    if (Obj.Categoria_Participante_Id > 0)
                    {
                        sqlCategoria_Participante_Id = new SqlParameter("@Categoria_Participante", Obj.Categoria_Participante_Id);
                    }
                    else
                    {
                        sqlCategoria_Participante_Id = new SqlParameter("@Categoria_Participante", DBNull.Value);
                    }

                    //  <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
                    //  <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
                    using (var command = dbContext.Database.Connection.CreateCommand())
                    {
                        dbContext.Database.Connection.Open();
                        command.CommandText = "SP_Rpt_Tiempos";
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add(sqlEvento_Id);
                        command.Parameters.Add(sqlJornada_Id);
                        command.Parameters.Add(sqlCategoria_Id);
                        command.Parameters.Add(sqlCategoria_Participante_Id);

                        using (var reader = command.ExecuteReader())
                        {
                            Lista = read(reader).ToList();

                            dbContext.Database.Connection.Close();
                        }


                        DtResponse.Clear();
                        DtResponse = Cls_Metodos_Generales.ToDataTable_List_Dict(Lista);
                        List_Reporte = Cls_Metodos_Generales.DataTableToList<Cls_Rpt_Tiempos_Negocio>(DtResponse);


                        Json_Resultado = JsonMapper.ToJson(List_Reporte);

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
    }
}
