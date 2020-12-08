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

namespace web_trazabilidad.Paginas.Operaciones.controllers
{
    /// <summary>
    /// Summary description for EventosController
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class EventosController : System.Web.Services.WebService
    {




        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string Alta(String jsonObject)
        {
            Cls_Mensaje Mensaje = new Cls_Mensaje();
            Cls_Ope_Eventos_Negocio Obj_Evento = new Cls_Ope_Eventos_Negocio();
            List<Cls_Ope_Eventos_Redes_Sociales_Negocio> List_Redes_Sociales = new List<Cls_Ope_Eventos_Redes_Sociales_Negocio>();
            List<Cls_Ope_Eventos_Redes_Sociales_Negocio> List_Redes_Sociales_eliminados = new List<Cls_Ope_Eventos_Redes_Sociales_Negocio>();

            string jsonResultado = "";
            try
            {
                Mensaje.Titulo = "Alta";

                Obj_Evento = JsonConvert.DeserializeObject<Cls_Ope_Eventos_Negocio>(jsonObject);
                List_Redes_Sociales = JsonConvert.DeserializeObject<List<Cls_Ope_Eventos_Redes_Sociales_Negocio>>(Obj_Evento.tbl_redes_sociales);

                using (var dbContext = new Sistema_TrazabilidadEntities())
                {
                    //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
                    //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
                    //  evento
                    Ope_Eventos Evento = new Ope_Eventos();
                    Ope_Eventos Evento_Nuevo = new Ope_Eventos();


                    Evento.Clave = Obj_Evento.Clave;
                    Evento.Nombre = Obj_Evento.Nombre;
                    Evento.Estatus = Obj_Evento.Estatus;
                    Evento.Fecha_Inicio = Obj_Evento.Fecha_Inicio;
                    Evento.Fecha_Fin = Obj_Evento.Fecha_Fin;
                    Evento.Fecha_Salida = Obj_Evento.Fecha_Salida;
                    Evento.Recorrido = Obj_Evento.Recorrido;
                    Evento.Punto_Salida = Obj_Evento.Punto_Salida;
                    Evento.Punto_Meta = Obj_Evento.Punto_Meta;
                    Evento.Hora_Salida = Obj_Evento.Hora_Salida;
                    //Evento.Intervalo_Salida = Obj_Evento.Intervalo_Salida;
                    Evento.Intervalo = Obj_Evento.Intervalo;
                    Evento.Comentarios = Obj_Evento.Comentarios;
                    Evento.Usuario_Creo = Cls_Sesiones.Usuario;
                    Evento.Fecha_Creo = DateTime.Now;

                    Evento_Nuevo = dbContext.Ope_Eventos.Add(Evento);


                    //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
                    //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
                    //  redes sociales
                    if (List_Redes_Sociales.Any())
                    {
                        foreach (var Detalles in List_Redes_Sociales)
                        {

                            if (Detalles.Evento_Id == 0)
                            {

                                Ope_Eventos_Redes_Sociales Evento_Link = new Ope_Eventos_Redes_Sociales();

                                Evento_Link.Evento_Id = Evento.Evento_Id;
                                Evento_Link.Nombre = Detalles.Nombre;
                                Evento_Link.Link = Detalles.Link;
                                Evento_Link.Estatus = Detalles.Estatus;
                                Evento_Link.Usuario_Creo = Cls_Sesiones.Usuario;
                                Evento_Link.Fecha_Creo = DateTime.Now;

                                dbContext.Ope_Eventos_Redes_Sociales.Add(Evento_Link);
                            }
                        }
                    }

                    //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
                    //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

                    dbContext.SaveChanges();

                    //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
                    //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

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
            Cls_Ope_Eventos_Negocio Obj_Evento = new Cls_Ope_Eventos_Negocio();
            List<Cls_Ope_Eventos_Redes_Sociales_Negocio> List_Redes_Sociales = new List<Cls_Ope_Eventos_Redes_Sociales_Negocio>();
            List<Cls_Ope_Eventos_Redes_Sociales_Negocio> List_Redes_Sociales_eliminados = new List<Cls_Ope_Eventos_Redes_Sociales_Negocio>();

            string jsonResultado = "";
            try
            {
                Mensaje.Titulo = "Modificar";

                Obj_Evento = JsonConvert.DeserializeObject<Cls_Ope_Eventos_Negocio>(jsonObject);
                List_Redes_Sociales = JsonConvert.DeserializeObject<List<Cls_Ope_Eventos_Redes_Sociales_Negocio>>(Obj_Evento.tbl_redes_sociales);
                List_Redes_Sociales_eliminados = JsonConvert.DeserializeObject<List<Cls_Ope_Eventos_Redes_Sociales_Negocio>>(Obj_Evento.tbl_redes_sociales_eliminados);

                using (var dbContext = new Sistema_TrazabilidadEntities())
                {
                    //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
                    //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
                    //  evento
                    Ope_Eventos Evento = new Ope_Eventos();
                    Evento = dbContext.Ope_Eventos.Where(w => w.Evento_Id == Obj_Evento.Evento_Id).FirstOrDefault();


                    Evento.Clave = Obj_Evento.Clave;
                    Evento.Nombre = Obj_Evento.Nombre;
                    Evento.Estatus = Obj_Evento.Estatus;
                    Evento.Fecha_Inicio = Obj_Evento.Fecha_Inicio;
                    Evento.Fecha_Fin = Obj_Evento.Fecha_Fin;
                    Evento.Fecha_Salida = Obj_Evento.Fecha_Salida;
                    Evento.Recorrido = Obj_Evento.Recorrido;
                    Evento.Punto_Salida = Obj_Evento.Punto_Salida;
                    Evento.Punto_Meta = Obj_Evento.Punto_Meta;
                    Evento.Hora_Salida = Obj_Evento.Hora_Salida;
                    //Evento.Intervalo_Salida = Obj_Evento.Intervalo_Salida;
                    Evento.Intervalo = Obj_Evento.Intervalo;
                    Evento.Comentarios = Obj_Evento.Comentarios;
                    Evento.Usuario_Modifico = Cls_Sesiones.Usuario;
                    Evento.Fecha_Modifico = DateTime.Now;


                    //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
                    //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
                    //  redes sociales
                    if (List_Redes_Sociales.Any())
                    {
                        foreach (var Detalles in List_Redes_Sociales)
                        {

                            if (Detalles.Evento_Id == 0)
                            {

                                Ope_Eventos_Redes_Sociales Evento_Link = new Ope_Eventos_Redes_Sociales();

                                Evento_Link.Evento_Id = Evento.Evento_Id;
                                Evento_Link.Nombre = Detalles.Nombre;
                                Evento_Link.Link = Detalles.Link;
                                Evento_Link.Estatus = Detalles.Estatus;
                                Evento_Link.Usuario_Creo = Cls_Sesiones.Usuario;
                                Evento_Link.Fecha_Creo = DateTime.Now;

                                dbContext.Ope_Eventos_Redes_Sociales.Add(Evento_Link);
                            }
                        }
                    }
                    //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
                    //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
                    //  eliminar enlaces

                    if (List_Redes_Sociales_eliminados.Any())
                    {

                        foreach (var Detalles in List_Redes_Sociales_eliminados)
                        {

                            Ope_Eventos_Redes_Sociales Evento_Link = new Ope_Eventos_Redes_Sociales();
                            Evento_Link = dbContext.Ope_Eventos_Redes_Sociales.Where(w => w.Link_ID == Detalles.Link_ID).FirstOrDefault();
                            Evento_Link.Estatus = "INACTIVO";
                            Evento_Link.Usuario_Modifico = Cls_Sesiones.Usuario;
                            Evento_Link.Fecha_Modifico = DateTime.Now;

                            dbContext.SaveChanges();
                        }
                    }

                    //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
                    //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

                    dbContext.SaveChanges();

                    //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
                    //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

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
            Cls_Ope_Eventos_Negocio Obj_Vehiculo = new Cls_Ope_Eventos_Negocio();
            string jsonResultado = "";
            try
            {
                Mensaje.Titulo = "Cancelacion de vehiculo";

                Obj_Vehiculo = JsonConvert.DeserializeObject<Cls_Ope_Eventos_Negocio>(jsonObject);

                using (var dbContext = new Sistema_TrazabilidadEntities())
                {
                    Ope_Eventos evento = new Ope_Eventos();
                    evento = dbContext.Ope_Eventos.Where(w => w.Evento_Id == Obj_Vehiculo.Evento_Id).FirstOrDefault();


                    evento.Estatus = "INACTIVO";
                    evento.Usuario_Modifico = Cls_Sesiones.Usuario;
                    evento.Fecha_Modifico = DateTime.Now;

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
        public string Iniciar_Evento(String jsonObject)
        {
            Cls_Mensaje Mensaje = new Cls_Mensaje();
            Cls_Ope_Eventos_Negocio Obj_Evento = new Cls_Ope_Eventos_Negocio();
            string jsonResultado = "";
            try
            {
                Mensaje.Titulo = "Iniciar";

                Obj_Evento = JsonConvert.DeserializeObject<Cls_Ope_Eventos_Negocio>(jsonObject);

                using (var dbContext = new Sistema_TrazabilidadEntities())
                {
                    Ope_Eventos evento = new Ope_Eventos();
                    evento = dbContext.Ope_Eventos.Where(w => w.Evento_Id == Obj_Evento.Evento_Id).FirstOrDefault();


                    evento.Estatus = "INICIADO";
                    evento.Usuario_Modifico = Cls_Sesiones.Usuario;
                    evento.Fecha_Modifico = DateTime.Now;

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
        public string Pausar_Evento(String jsonObject)
        {
            Cls_Mensaje Mensaje = new Cls_Mensaje();
            Cls_Ope_Eventos_Negocio Obj_Evento = new Cls_Ope_Eventos_Negocio();
            string jsonResultado = "";
            try
            {
                Mensaje.Titulo = "Pausar";

                Obj_Evento = JsonConvert.DeserializeObject<Cls_Ope_Eventos_Negocio>(jsonObject);

                using (var dbContext = new Sistema_TrazabilidadEntities())
                {
                    Ope_Eventos evento = new Ope_Eventos();
                    evento = dbContext.Ope_Eventos.Where(w => w.Evento_Id == Obj_Evento.Evento_Id).FirstOrDefault();


                    evento.Estatus = "ACTIVO";
                    evento.Usuario_Modifico = Cls_Sesiones.Usuario;
                    evento.Fecha_Modifico = DateTime.Now;

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
        public string Terminar_Evento(String jsonObject)
        {
            Cls_Mensaje Mensaje = new Cls_Mensaje();
            Cls_Ope_Eventos_Negocio Obj_Evento = new Cls_Ope_Eventos_Negocio();
          

            string jsonResultado = "";
            try
            {
                Mensaje.Titulo = "Terminar";

                Obj_Evento = JsonConvert.DeserializeObject<Cls_Ope_Eventos_Negocio>(jsonObject);

                using (var dbContext = new Sistema_TrazabilidadEntities())
                {
                    //  se calculan la puntuacion.
                    ////<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
                    ////<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
                    //#region Tiempos 
                    //var _registros = (from _tiempo in dbContext.Ope_Eventos_Registro_Tiempo
                    //                  where _tiempo.Evento_Id == Obj_Evento.Evento_Id
                    //                  select new Cls_Ope_Evento_Registro_Tiempo_Negocio
                    //                  {
                    //                      Registro_Id = _tiempo.Registro_Id,
                    //                      Tiempo_Ideal = _tiempo.Tiempo_Ideal,
                    //                      Tiempo_Real = _tiempo.Tiempo_Real,
                    //                  }).OrderBy(o => o.Registro_Id);

                    //if (_registros.Any())
                    //{
                    //    Cls_Calcular_Tiempo.Calcular_Tiempos(_registros.ToList());
                    //}
                    //#endregion   
                                     
                    //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
                    //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
                    #region Cambio de estatus
                    Ope_Eventos evento = new Ope_Eventos();

                    evento = dbContext.Ope_Eventos.Where(w => w.Evento_Id == Obj_Evento.Evento_Id).FirstOrDefault();

                    //  se cambia el estatus al evento
                    evento.Estatus = "TERMINADO";
                    evento.Usuario_Modifico = Cls_Sesiones.Usuario;
                    evento.Fecha_Modifico = DateTime.Now;
                    #endregion
                    //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
                    //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
                    dbContext.SaveChanges();
                    //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
                    //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
                    Mensaje.Mensaje = "La operación se realizo correctamente.";
                    Mensaje.Estatus = "success";
                    //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
                    //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
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
        public string Consultar_Evento_Filtro(string jsonObject)
        {
            string Json_Resultado = string.Empty;
            Cls_Ope_Eventos_Negocio Obj = new Cls_Ope_Eventos_Negocio();

            try
            {

                Obj = JsonConvert.DeserializeObject<Cls_Ope_Eventos_Negocio>(jsonObject);


                Obj.Fecha_Inicio = Obj.Fecha_Inicio == null ? DateTime.MinValue : Obj.Fecha_Inicio.Value;
                Obj.Fecha_Fin = Obj.Fecha_Fin == null ? DateTime.MinValue : Obj.Fecha_Fin.Value;
                Obj.Fecha_Salida = Obj.Fecha_Salida == null ? DateTime.MinValue : Obj.Fecha_Salida.Value;


                using (var dbContext = new Sistema_TrazabilidadEntities())
                {

                    var _eventos = (from _ev in dbContext.Ope_Eventos

                                    select new Cls_Ope_Eventos_Negocio
                                    {
                                        Evento_Id = _ev.Evento_Id,
                                        Clave = _ev.Clave,
                                        Nombre = _ev.Nombre,
                                        Estatus = _ev.Estatus,
                                        Fecha_Inicio = _ev.Fecha_Inicio,
                                        Fecha_Fin = _ev.Fecha_Fin,
                                        Fecha_Salida = _ev.Fecha_Salida,
                                        Recorrido = _ev.Recorrido ?? 0,
                                        Punto_Salida = _ev.Punto_Salida,
                                        Punto_Meta = _ev.Punto_Meta,
                                        Str_Hora_Salida = _ev.Hora_Salida.ToString(),
                                        //Str_Intervalo_Salida = _ev.Intervalo_Salida.ToString(),
                                        Str_Intervalo = _ev.Intervalo.ToString(),
                                        //Hora_Salida = _ev.Hora_Salida,
                                        //Intervalo_Salida = _ev.Intervalo_Salida,
                                        Comentarios = _ev.Comentarios,

                                    })
                                      .OrderBy(x => x.Clave).ToList();

                 
                    //  filtro estatus
                    if (!String.IsNullOrEmpty(Obj.Estatus))
                    {
                        _eventos = _eventos.Where(x => x.Estatus == (Obj.Estatus)).ToList();
                    }

                    //  filtro Clave
                    if (!String.IsNullOrEmpty(Obj.Clave))
                    {
                        _eventos = _eventos.Where(x => x.Clave.Contains(Obj.Clave)).ToList();
                    }

                    //  filtro Nombre
                    if (!String.IsNullOrEmpty(Obj.Nombre))
                    {
                        _eventos = _eventos.Where(x => x.Nombre.Contains(Obj.Nombre)).ToList();
                    }

                    //  filtro Fecha_Inicio
                    if (Obj.Fecha_Inicio != DateTime.MinValue)
                    {
                        _eventos = _eventos.Where(x => (x.Fecha_Inicio >= Obj.Fecha_Inicio)).ToList();

                    }

                    //  filtro Fecha_Fin
                    if (Obj.Fecha_Fin != DateTime.MinValue)
                    {
                        _eventos = _eventos.Where(x => (x.Fecha_Fin <= Obj.Fecha_Fin)).ToList();

                    }

                    //  filtro Fecha_Salida
                    if (Obj.Fecha_Salida != DateTime.MinValue)
                    {
                        _eventos = _eventos.Where(x => (x.Fecha_Salida >= Obj.Fecha_Salida)).ToList();

                    }


                    Json_Resultado = JsonMapper.ToJson(_eventos.ToList());
                }
            }
            catch (Exception e)
            {

            }

            return Json_Resultado;
        }


        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string Consultar_Link_Evento(string jsonObject)
        {
            string Json_Resultado = string.Empty;
            Cls_Ope_Eventos_Negocio Obj = new Cls_Ope_Eventos_Negocio();

            try
            {

                Obj = JsonConvert.DeserializeObject<Cls_Ope_Eventos_Negocio>(jsonObject);


               
                using (var dbContext = new Sistema_TrazabilidadEntities())
                {

                    var _eventos = (from _ev in dbContext.Ope_Eventos_Redes_Sociales
                                    where _ev.Estatus == "ACTIVO"

                                    && (Obj.Evento_Id > 0 ? _ev.Evento_Id == Obj.Evento_Id : true)

                                    select new Cls_Ope_Eventos_Redes_Sociales_Negocio
                                    {
                                        Link_ID = _ev.Link_ID,
                                        Evento_Id = _ev.Evento_Id,
                                        Nombre = _ev.Nombre,
                                        Link = _ev.Link,
                                        Estatus = _ev.Estatus,

                                    })
                                      .OrderBy(x => x.Nombre).ToList();

                    
                    Json_Resultado = JsonMapper.ToJson(_eventos.ToList());
                }
            }
            catch (Exception e)
            {

            }

            return Json_Resultado;
        }


        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void Consultar_Eventos_Combo()
        {
            string Json_Resultado = string.Empty;
            List<Cls_Select2> Lst_Combo = new List<Cls_Select2>();
            Cls_Ope_Evento_Registro_Tiempo_Negocio Obj = new Cls_Ope_Evento_Registro_Tiempo_Negocio();

            try
            {

                string q = string.Empty;
                string evento_id = string.Empty;

                NameValueCollection nvc = Context.Request.Form;

                if (!String.IsNullOrEmpty(nvc["q"]))
                    q = nvc["q"].ToString().Trim();


                using (var dbContext = new Sistema_TrazabilidadEntities())
                {

                    var _Busqueda = (from _evento in dbContext.Ope_Eventos
                                     //where _evento.Estatus == "INICIADO"
                                     where ((_evento.Nombre.Contains(q) || _evento.Clave.Contains(q)))

                                     select new Cls_Select2
                                     {
                                         id = _evento.Evento_Id.ToString(),
                                         text = _evento.Clave + " - " + _evento.Nombre,
                                     });


                    Json_Resultado = JsonMapper.ToJson(_Busqueda.ToList());
                }
            }
            catch (Exception Ex)
            {
                ErrorSignal.FromCurrentContext().Raise(Ex);
            }
            finally
            {
                Context.Response.Write(Json_Resultado);
            }
        }


        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string Consultar_Evento_Iniciado()
        {
            string Json_Resultado = string.Empty;
            //Cls_Ope_Eventos_Negocio Obj = new Cls_Ope_Eventos_Negocio();

            try
            {
                //Obj = JsonConvert.DeserializeObject<Cls_Ope_Eventos_Negocio>(jsonObject);
                
                using (var dbContext = new Sistema_TrazabilidadEntities())
                {

                    var _eventos = (from _ev in dbContext.Ope_Eventos
                                    where _ev.Estatus == "INICIADO"

                                    select new Cls_Ope_Eventos_Negocio
                                    {
                                        Evento_Id = _ev.Evento_Id,
                                        Nombre = _ev.Clave + " - " + _ev.Nombre,
                                       
                                    })
                                      .OrderBy(x => x.Nombre).ToList();


                    Json_Resultado = JsonMapper.ToJson(_eventos.ToList());
                }
            }
            catch (Exception e)
            {

            }

            return Json_Resultado;
        }

    }
}
