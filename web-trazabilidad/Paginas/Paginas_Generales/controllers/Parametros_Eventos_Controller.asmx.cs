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
using web_trazabilidad.Models.Negocio.Generales;

namespace web_trazabilidad.Paginas.Paginas_Generales.controllers
{
    /// <summary>
    /// Summary description for Parametros_Eventos_Controller
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class Parametros_Eventos_Controller : System.Web.Services.WebService
    {
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string Consultar_Parametros(string jsonObject)
        {
            string Json_Resultado = string.Empty;
            Cls_Apl_Cat_Parametros_Eventos_Negocio Obj = new Cls_Apl_Cat_Parametros_Eventos_Negocio();

            try
            {

                Obj = JsonConvert.DeserializeObject<Cls_Apl_Cat_Parametros_Eventos_Negocio>(jsonObject);
                
                using (var dbContext = new Sistema_TrazabilidadEntities())
                {

                    var _parametros = (from _par in dbContext.Apl_Cat_Parametros_Eventos
                                       where _par.Estatus == Obj.Estatus

                                       select new Cls_Apl_Cat_Parametros_Eventos_Negocio
                                      {
                                         Parametro_ID = _par.Parametro_ID,
                                         Puntos_Penalizacion = _par.Puntos_Penalizacion,
                                      }).ToList();
                    
                    Json_Resultado = JsonMapper.ToJson(_parametros.ToList());
                }
            }
            catch (Exception e)
            {

            }

            return Json_Resultado;
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string Alta(String jsonObject)
        {
            Cls_Mensaje Mensaje = new Cls_Mensaje();
            Cls_Apl_Cat_Parametros_Eventos_Negocio Obj_Datos = new Cls_Apl_Cat_Parametros_Eventos_Negocio();
            string jsonResultado = "";
            String Color = "#8A2BE2";
            String Icono = "fa fa-close";

            try
            {
                Mensaje.Titulo = "Alta";

                Obj_Datos = JsonConvert.DeserializeObject<Cls_Apl_Cat_Parametros_Eventos_Negocio>(jsonObject);

                using (var dbContext = new Sistema_TrazabilidadEntities())
                {

                    var parametro_registrado = (from _parametro in dbContext.Apl_Cat_Parametros_Eventos
                                                where _parametro.Parametro_ID == Obj_Datos.Parametro_ID
                                               select _parametro
                                            );


                    if (parametro_registrado.Any())
                    {
                        Apl_Cat_Parametros_Eventos parametro_modificacion = new Apl_Cat_Parametros_Eventos();
                        parametro_modificacion = dbContext.Apl_Cat_Parametros_Eventos.Where(w => w.Parametro_ID == Obj_Datos.Parametro_ID).FirstOrDefault();

                        parametro_modificacion.Puntos_Penalizacion = Obj_Datos.Puntos_Penalizacion;
                        parametro_modificacion.Usuario_Modifico = Cls_Sesiones.Usuario;
                        parametro_modificacion.Fecha_Modifico = DateTime.Now;

                        dbContext.SaveChanges();

                        Mensaje.Mensaje = "La operación se realizo correctamente.";
                        Mensaje.Estatus = "success";

                    }
                    else
                    {
                        Apl_Cat_Parametros_Eventos Parametro = new Apl_Cat_Parametros_Eventos();
                        Apl_Cat_Parametros_Eventos Parametro_Nuevo = new Apl_Cat_Parametros_Eventos();

                        Parametro.Puntos_Penalizacion = Obj_Datos.Puntos_Penalizacion;
                        Parametro.Estatus = Obj_Datos.Estatus;
                        Parametro.Usuario_Creo = Cls_Sesiones.Usuario;
                        Parametro.Fecha_Creo = DateTime.Now;

                        Parametro_Nuevo = dbContext.Apl_Cat_Parametros_Eventos.Add(Parametro);

                        dbContext.SaveChanges();

                        Mensaje.Mensaje = "La operación se realizo correctamente.";
                        Mensaje.Estatus = "success";
                    }
                }
            }
            catch (Exception e)
            {

                Mensaje.Mensaje = "Error Técnico. " + e.Message;
                Mensaje.Estatus = "error";
            }
            finally
            {
                jsonResultado = JsonMapper.ToJson(Mensaje);
            }

            return jsonResultado;
        }



    }
}
