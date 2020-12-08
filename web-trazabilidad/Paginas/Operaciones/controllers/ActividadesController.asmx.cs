using datos_trazabilidad;
using LitJson;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using web_trazabilidad.Models.Ayudante;
using web_trazabilidad.Models.Negocio;
using web_trazabilidad.Models.Negocio.Operaciones;

namespace web_trazabilidad.Paginas.Operaciones.controllers
{
    /// <summary>
    /// Summary description for ActividadesController
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class ActividadesController : System.Web.Services.WebService
    {
        #region Metodos

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string Alta(String jsonObject)
        {
            Cls_Mensaje Mensaje = new Cls_Mensaje();
            Cls_Ope_Eventos_Actividades_Negocio Obj_Actividad = new Cls_Ope_Eventos_Actividades_Negocio();
            string jsonResultado = "";

            try
            {
                Mensaje.Titulo = "Alta de actividad";
                Obj_Actividad = JsonConvert.DeserializeObject<Cls_Ope_Eventos_Actividades_Negocio>(jsonObject);

                using (var dbContext = new Sistema_TrazabilidadEntities())
                {
                    Ope_Eventos_Actividades Actividad = new Ope_Eventos_Actividades();

                    Actividad.Evento_Id = Obj_Actividad.Evento_Id.Value;
                    Actividad.Clave = Obj_Actividad.Clave;
                    Actividad.Nombre = Obj_Actividad.Nombre;
                    Actividad.Fecha_Inicio = Obj_Actividad.Fecha_Inicio;
                    Actividad.Fecha_Fin = Obj_Actividad.Fecha_Fin;
                    Actividad.Estatus = Obj_Actividad.Estatus;
                    Actividad.Comentarios = Obj_Actividad.Comentarios;
                    Actividad.Usuario_Creo = Cls_Sesiones.Usuario;
                    Actividad.Fecha_Creo = DateTime.Now;

                    dbContext.Ope_Eventos_Actividades.Add(Actividad);
                    
                    dbContext.SaveChanges();

                    Mensaje.Mensaje = "La operación se realizo correctamente.";
                    Mensaje.Estatus = "success";
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


        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string Modificar(String jsonObject)
        {
            Cls_Mensaje Mensaje = new Cls_Mensaje();
            Cls_Ope_Eventos_Actividades_Negocio Obj_Actividad = new Cls_Ope_Eventos_Actividades_Negocio();
            string jsonResultado = "";
            
            try
            {
                Mensaje.Titulo = "Modificación de actividad";
                Obj_Actividad = JsonConvert.DeserializeObject<Cls_Ope_Eventos_Actividades_Negocio>(jsonObject);
                
                using (var dbContext = new Sistema_TrazabilidadEntities())
                {
                    Ope_Eventos_Actividades Actividad = new Ope_Eventos_Actividades();
                    Actividad = dbContext.Ope_Eventos_Actividades.Where(w => w.Actividad_Id == Obj_Actividad.Actividad_Id).FirstOrDefault();

                    //Actividad.Evento_Id = Obj_Actividad.Evento_Id.Value;
                    Actividad.Clave = Obj_Actividad.Clave;
                    Actividad.Nombre = Obj_Actividad.Nombre;
                    Actividad.Fecha_Inicio = Obj_Actividad.Fecha_Inicio;
                    Actividad.Fecha_Fin = Obj_Actividad.Fecha_Fin;
                    Actividad.Estatus = Obj_Actividad.Estatus;
                    Actividad.Comentarios = Obj_Actividad.Comentarios;
                    Actividad.Usuario_Modifico = Cls_Sesiones.Usuario;
                    Actividad.Fecha_Modifico = DateTime.Now;

                    dbContext.SaveChanges();

                    Mensaje.Mensaje = "La operación se realizo correctamente.";
                    Mensaje.Estatus = "success";
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


        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string Cancelacion(String jsonObject)
        {
            Cls_Mensaje Mensaje = new Cls_Mensaje();
            Cls_Ope_Eventos_Actividades_Negocio Obj_Actividad = new Cls_Ope_Eventos_Actividades_Negocio();
            string jsonResultado = "";
            try
            {
                Mensaje.Titulo = "Cancelacion de vehiculo";

                Obj_Actividad = JsonConvert.DeserializeObject<Cls_Ope_Eventos_Actividades_Negocio>(jsonObject);

                using (var dbContext = new Sistema_TrazabilidadEntities())
                {
                    Ope_Eventos_Actividades Actividad = new Ope_Eventos_Actividades();
                    Actividad = dbContext.Ope_Eventos_Actividades.Where(w => w.Actividad_Id == Obj_Actividad.Actividad_Id).FirstOrDefault();
                    
                    Actividad.Estatus = "INACTIVO";
                    Actividad.Usuario_Modifico = Cls_Sesiones.Usuario;
                    Actividad.Fecha_Modifico = DateTime.Now;

                    dbContext.SaveChanges();

                    Mensaje.Mensaje = "La operación se realizo correctamente.";
                    Mensaje.Estatus = "success";
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


        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string Consultar_Actividades(string jsonObject)
        {
            string Json_Resultado = string.Empty;
            Cls_Ope_Eventos_Actividades_Negocio Obj = new Cls_Ope_Eventos_Actividades_Negocio();

            try
            {
                Obj = JsonConvert.DeserializeObject<Cls_Ope_Eventos_Actividades_Negocio>(jsonObject);

                Obj.Fecha_Inicio = Obj.Fecha_Inicio == null ? DateTime.MinValue : Obj.Fecha_Inicio.Value;
                Obj.Fecha_Fin = Obj.Fecha_Fin == null ? DateTime.MinValue : Obj.Fecha_Fin.Value;

                using (var dbContext = new Sistema_TrazabilidadEntities())
                {
                    var _participantes = (from _act in dbContext.Ope_Eventos_Actividades
                                          where _act.Evento_Id == Obj.Evento_Id
                                          && _act.Estatus == "ACTIVO"

                                          select new Cls_Ope_Eventos_Actividades_Negocio
                                          {
                                              Actividad_Id = _act.Actividad_Id,
                                              Clave = _act.Clave,
                                              Nombre = _act.Nombre,
                                              Fecha_Inicio = _act.Fecha_Inicio,
                                              Fecha_Fin = _act.Fecha_Fin,
                                              Comentarios = _act.Comentarios,
                                              Estatus = _act.Estatus
                                          })
                                          .OrderBy(x => x.Nombre).ToList();
                    
                    Json_Resultado = JsonMapper.ToJson(_participantes.ToList());
                }
            }
            catch (Exception e)
            {

            }

            return Json_Resultado;
        }
        
        #endregion
    }
}
