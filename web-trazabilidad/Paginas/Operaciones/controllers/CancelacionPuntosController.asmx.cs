using datos_trazabilidad;
using Elmah;
using LitJson;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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
    /// Summary description for CancelacionPuntosController
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class CancelacionPuntosController : System.Web.Services.WebService
    {
        #region Metodos
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string Cancelacion(String jsonObject)
        {
            Cls_Mensaje Mensaje = new Cls_Mensaje();
            Cls_Ope_Eventos_Puntos_Control_Negocio Obj_Punto = new Cls_Ope_Eventos_Puntos_Control_Negocio();

            string jsonResultado = "";
            
            try
            {
                Mensaje.Titulo = "Cancelar";

                Obj_Punto = JsonConvert.DeserializeObject<Cls_Ope_Eventos_Puntos_Control_Negocio>(jsonObject);
                
                using (var dbContext = new Sistema_TrazabilidadEntities())
                {
                    Ope_Eventos_Puntos_Control Punto = new Ope_Eventos_Puntos_Control();
                    Punto = dbContext.Ope_Eventos_Puntos_Control.Where(w => w.Punto_Control_Id == Obj_Punto.Punto_Control_Id).FirstOrDefault();

                    Punto.Estatus = "INACTIVO";
                    Punto.Comentarios = Obj_Punto.Comentarios;
                    Punto.Usuario_Cancelo = Cls_Sesiones.Usuario;

                    Punto.Usuario_Modifico = Cls_Sesiones.Usuario;
                    Punto.Fecha_Modifico = DateTime.Now;
                    
                    dbContext.SaveChanges();
                    
                    Mensaje.Mensaje = "La operación se realizó correctamente.";
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
        public string Consultar_Puntos_Filtros(string jsonObject)
        {
            string Json_Resultado = string.Empty;
            Cls_Ope_Eventos_Puntos_Control_Negocio Obj = new Cls_Ope_Eventos_Puntos_Control_Negocio();

            try
            {
                Obj = JsonConvert.DeserializeObject<Cls_Ope_Eventos_Puntos_Control_Negocio>(jsonObject);
                
                using (var dbContext = new Sistema_TrazabilidadEntities())
                {
                    var _Tiempos = (from _pts in dbContext.Ope_Eventos_Puntos_Control
                                    //  evento
                                    join _evento in dbContext.Ope_Eventos on _pts.Evento_Id equals _evento.Evento_Id

                                    //  jornada
                                    join _jor in dbContext.Ope_Eventos_Jornadas on _pts.Jornada_Id equals _jor.Jornada_Id

                                    //  punto de control
                                    join _rel in dbContext.Ope_Eventos_Pnt_Ctrl_Operador on _pts.Punto_Control_Id equals _rel.Punto_Control_Id
                                    
                                    //  responsables
                                    join _resp in dbContext.Cat_Responsables on _rel.Responsable_Id equals _resp.Responsable_Id

                                    where _pts.Evento_Id == Obj.Evento_Id
                                        && _pts.Jornada_Id == Obj.Jornada_Id
                                        && _rel.Cargo == "RESPONSABLE"

                                    select new Cls_Ope_Eventos_Puntos_Control_Negocio
                                    {
                                        Punto_Control_Id = _pts.Punto_Control_Id,
                                        Evento_Id = _evento.Evento_Id,
                                        Jornada_Id = _jor.Jornada_Id,
                                        Clave = _pts.Clave,
                                        Ubicacion = _pts.Ubicacion,
                                        Estatus = _pts.Estatus,
                                        Responsable = _resp.Nombre,
                                        Comentarios = _pts.Comentarios,
                                        Sincronizacion = _pts.Sincronizacion ?? false,
                                        Usuario_Cancelo = _pts.Usuario_Cancelo,
                                    }
                                    ).OrderBy(x => x.Clave).ToList();

                    // filtro evento
                    if (Obj.Evento_Id != null && Obj.Evento_Id > 0)
                    {
                        _Tiempos = _Tiempos.Where(x => x.Evento_Id == Obj.Evento_Id).ToList();
                    }

                    // filtro jornada
                    if (Obj.Jornada_Id != null && Obj.Jornada_Id > 0)
                    {
                        _Tiempos = _Tiempos.Where(x => x.Jornada_Id == Obj.Jornada_Id).ToList();
                    }

                    Json_Resultado = JsonMapper.ToJson(_Tiempos.ToList());
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
