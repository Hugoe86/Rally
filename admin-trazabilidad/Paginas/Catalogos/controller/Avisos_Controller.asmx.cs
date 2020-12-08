using admin_trazabilidad.Models.Negocio;
using datos_trazabilidad;
using LitJson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using web_trazabilidad.Models.Negocio;

namespace web_trazabilidad.Paginas.Trazabilidad.controllers
{
    /// <summary>
    /// Summary description for Avisos_Controller
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class Avisos_Controller : System.Web.Services.WebService
    {

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string Guardar_Aviso(string jsonObject)
        {
            string Json_Resultado = "{}";
            Cls_Apl_Avisos objAvisos = new Cls_Apl_Avisos();
            Cls_Mensaje Mensaje = new Cls_Mensaje();
            try
            {
                objAvisos = JsonMapper.ToObject<Cls_Apl_Avisos>(jsonObject); //Datos generales de embarques
                Mensaje.Titulo = "Alta de aviso";
                //Lista con los detalles del embarque
                using (var dbContext = new Sistema_TrazabilidadEntities())
                {
                    if (Verificar_Avisos(objAvisos.Fecha_Inicio_Vigencia))
                    {
                        Apl_Avisos aviso = new Apl_Avisos();
                        aviso.Fecha_Inicio_Vigencia = objAvisos.Fecha_Inicio_Vigencia;
                        aviso.Fecha_Fin_Vigencia = objAvisos.Fecha_Fin_Vigencia;
                        aviso.Mensaje = objAvisos.Mensaje;

                        dbContext.Apl_Avisos.Add(aviso);
                        dbContext.SaveChanges();
                        Mensaje.Estatus = "success";
                        Mensaje.Mensaje = "Alta Exitosa";
                    }
                    else
                    {
                        Mensaje.Estatus = "error";
                        Mensaje.Mensaje = "En el rango de fechas ya se encuentra un aviso registrado.";
                    }
                }
            }
            catch (Exception Ex)
            {
                Mensaje.Estatus = "error";
                Mensaje.Mensaje = "<i class='fa fa-times' style='color:#FF0004;'></i>&nbsp;Informe técnico: " + Ex.Message;
            
            }

            Json_Resultado = JsonMapper.ToJson(Mensaje);

            return Json_Resultado;
        }

        public bool Verificar_Avisos(DateTime fecha)
        {
            bool Mensaje = false;
            try
            {
                using (var dbContext = new Sistema_TrazabilidadEntities())
                {
                    var _select = (from _s in dbContext.Apl_Avisos
                                   where _s.Fecha_Fin_Vigencia > fecha
                                   select _s).OrderBy(x => x.Fecha_Fin_Vigencia);
                    if (_select.Any())
                    {
                        Mensaje = false;
                    }
                    else
                    {
                        Mensaje = true;
                    }
                }
            }
            catch (Exception Ex)
            {
                Mensaje = false;
                        }

            return Mensaje;
        }
        public bool Verificar_Avisos(Cls_Apl_Avisos aviso)
        {
            bool Mensaje = false;
            try
            {
                using (var dbContext = new Sistema_TrazabilidadEntities())
                {
                    var _select = (from _s in dbContext.Apl_Avisos
                                   where _s.Aviso_ID != aviso.Aviso_ID && _s.Fecha_Inicio_Vigencia >= aviso.Fecha_Inicio_Vigencia && _s.Fecha_Fin_Vigencia <= aviso.Fecha_Fin_Vigencia
                                   select _s).OrderBy(x => x.Fecha_Fin_Vigencia);
                    if (_select.Any())
                    {
                        Mensaje = false;
                    }
                    else
                    {
                        Mensaje = true;
                    }
                }
            }
            catch (Exception Ex)
            {
                Mensaje = false;
                }

            return Mensaje;
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string Consultar_Avisos()
        {
            string Json_Resultado = "[]";
            List<Cls_Apl_Avisos> Lst_Aviso = new List<Cls_Apl_Avisos>();
            Cls_Mensaje Mensaje = new Cls_Mensaje();

            try
            {

                using (var dbContext = new Sistema_TrazabilidadEntities())
                {
                    var Estatus = (from _clientes in dbContext.Apl_Avisos
                                   where _clientes.Fecha_Inicio_Vigencia <= DateTime.Now && _clientes.Fecha_Fin_Vigencia >= DateTime.Now
                                   select new Cls_Apl_Avisos
                                   {
                                       Mensaje = _clientes.Mensaje,
                                       Fecha_Fin_Vigencia = _clientes.Fecha_Fin_Vigencia.Value,
                                       Fecha_Inicio_Vigencia = _clientes.Fecha_Inicio_Vigencia.Value
                                   }).OrderBy(x => x.Fecha_Fin_Vigencia);

                    foreach (var p in Estatus)
                        Lst_Aviso.Add(p);

                    if (Lst_Aviso.Count > 0)
                    {
                        Mensaje.Mensaje = Lst_Aviso[0].Mensaje;//+ "\n Fecha " + Lst_Aviso[0].Fecha_Inicio_Vigencia.ToString("dd/MMM/yyyy hh:mm tt").ToUpper() + " A " + Lst_Aviso[0].Fecha_Fin_Vigencia.ToString("dd/MMM/yyyy hh:mm tt").ToUpper();
                        Mensaje.Estatus = "success";
                    }
                    else
                    {
                        Mensaje.Mensaje = "";
                        Mensaje.Estatus = "error";
                    }

                }
            }
            catch (Exception Ex)
            {
                Mensaje.Mensaje = "Error Tecnico: " + Ex.Message;
                Mensaje.Estatus = "error";
                        }
            Json_Resultado = JsonMapper.ToJson(Mensaje);
            return Json_Resultado;
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string Consultar_Avisos_Vigentes()
        {
            string Json_Resultado = "[]";
            List<Cls_Apl_Avisos> Lst_Aviso = new List<Cls_Apl_Avisos>();
            Cls_Mensaje Mensaje = new Cls_Mensaje();

            try
            {

                using (var dbContext = new Sistema_TrazabilidadEntities())
                {
                    var Estatus = (from _clientes in dbContext.Apl_Avisos
                                   //where _clientes.Fecha_Inicio_Vigencia >= DateTime.Now.Date
                                   select new Cls_Apl_Avisos
                                   {
                                       Aviso_ID = _clientes.Aviso_ID,
                                       Mensaje = _clientes.Mensaje,
                                       Fecha_Fin_Vigencia = _clientes.Fecha_Fin_Vigencia.Value,
                                       Fecha_Inicio_Vigencia = _clientes.Fecha_Inicio_Vigencia.Value
                                   }).OrderBy(x => x.Fecha_Fin_Vigencia);

                    foreach (var p in Estatus)
                        Lst_Aviso.Add(p);
                }
            }
            catch (Exception Ex)
            {

            }
            Json_Resultado = JsonMapper.ToJson(Lst_Aviso);
            return Json_Resultado;
        }


        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string Consultar_Aviso(string jsonObject)
        {
            string Json_Resultado = "{}";
            List<Cls_Apl_Avisos> Lst_Aviso = new List<Cls_Apl_Avisos>();
            Cls_Apl_Avisos objAvisos = new Cls_Apl_Avisos();

            try
            {
                objAvisos = JsonMapper.ToObject<Cls_Apl_Avisos>(jsonObject); //Datos generales de embarques

                using (var dbContext = new Sistema_TrazabilidadEntities())
                {
                    var _avisos = (from _clientes in dbContext.Apl_Avisos
                                       where _clientes.Aviso_ID == objAvisos.Aviso_ID
                                   select new Cls_Apl_Avisos
                                   {
                                       Aviso_ID = _clientes.Aviso_ID,
                                       Mensaje = _clientes.Mensaje,
                                       Fecha_Fin_Vigencia = _clientes.Fecha_Fin_Vigencia.Value,
                                       Fecha_Inicio_Vigencia = _clientes.Fecha_Inicio_Vigencia.Value
                                   }).OrderBy(x => x.Fecha_Fin_Vigencia);

                    if (_avisos.Any())
                        objAvisos = _avisos.FirstOrDefault<Cls_Apl_Avisos>();
                }
            }
            catch (Exception Ex)
            {
                      }
            finally {
                Json_Resultado = JsonMapper.ToJson(objAvisos);
            }
            return Json_Resultado;
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string Actualizar_Aviso(string jsonObject)
        {
            string Json_Resultado = "{}";
            Cls_Apl_Avisos objAvisos = new Cls_Apl_Avisos();
            Cls_Mensaje Mensaje = new Cls_Mensaje();
            try
            {
                objAvisos = JsonMapper.ToObject<Cls_Apl_Avisos>(jsonObject); //Datos generales de embarques
                Mensaje.Titulo = "Alta de aviso";
                //Lista con los detalles del embarque
                using (var dbContext = new Sistema_TrazabilidadEntities())
                {
                    if (Verificar_Avisos(objAvisos))
                    {
                        var aviso = dbContext.Apl_Avisos.Where(z => z.Aviso_ID == objAvisos.Aviso_ID).First();

                        aviso.Fecha_Inicio_Vigencia = objAvisos.Fecha_Inicio_Vigencia;
                        aviso.Fecha_Fin_Vigencia = objAvisos.Fecha_Fin_Vigencia;
                        aviso.Mensaje = objAvisos.Mensaje;

                        dbContext.SaveChanges();
                        Mensaje.Estatus = "success";
                        Mensaje.Mensaje = "Actualizacion del Aviso Exitosa.";
                    }
                    else
                    {
                        Mensaje.Estatus = "error";
                        Mensaje.Mensaje = "Ya se encuentra un aviso vigente";
                    }
                }
            }
            catch (Exception Ex)
            {
                Mensaje.Estatus = "error";
                Mensaje.Mensaje = "<i class='fa fa-times' style='color:#FF0004;'></i>&nbsp;Informe técnico: " + Ex.Message;
             
            }
            Json_Resultado = JsonMapper.ToJson(Mensaje);
            return Json_Resultado;
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string Eliminar_Aviso(string jsonObject)
        {
            string Json_Resultado = "{}";
            Cls_Apl_Avisos objAvisos = new Cls_Apl_Avisos();
            Cls_Mensaje Mensaje = new Cls_Mensaje();
            try
            {
                objAvisos = JsonMapper.ToObject<Cls_Apl_Avisos>(jsonObject); //Datos generales de embarques
                Mensaje.Titulo = "Eliminar aviso";
                //Lista con los detalles del embarque
                using (var dbContext = new Sistema_TrazabilidadEntities())
                {
                    var aviso = dbContext.Apl_Avisos.Where(z => z.Aviso_ID == objAvisos.Aviso_ID).First();

                    dbContext.Apl_Avisos.Remove(aviso);

                    dbContext.SaveChanges();
                    Mensaje.Estatus = "success";
                    Mensaje.Mensaje = "Actualizacion del Aviso Exitosa.";

                }
            }
            catch (Exception Ex)
            {
                Mensaje.Estatus = "error";
                Mensaje.Mensaje = "<i class='fa fa-times' style='color:#FF0004;'></i>&nbsp;Informe técnico: " + Ex.Message;

            }
            Json_Resultado = JsonMapper.ToJson(Mensaje);
            return Json_Resultado;
        }
    }
}
