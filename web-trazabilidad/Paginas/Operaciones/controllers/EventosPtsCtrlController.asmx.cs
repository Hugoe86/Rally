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
using web_cambios_procesos.Models.Negocio.Operacion.Solicitudes;
using web_cambios_procesos.Models.Ayudante;
using System.IO;
using OfficeOpenXml;
using Newtonsoft.Json.Converters;
using System.Globalization;

namespace web_trazabilidad.Paginas.Operaciones.controllers
{
    /// <summary>
    /// Summary description for EventosPtsCtrlController
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class EventosPtsCtrlController : System.Web.Services.WebService
    {

        #region Metodos

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string Alta(String jsonObject)
        {
            Cls_Mensaje Mensaje = new Cls_Mensaje();
            Cls_Ope_Eventos_Puntos_Control_Negocio Obj_Punto_Control = new Cls_Ope_Eventos_Puntos_Control_Negocio();
            List<Cls_Ope_Eventos_Pnt_Ctrl_Operador_Negocio> List_Operadores = new List<Cls_Ope_Eventos_Pnt_Ctrl_Operador_Negocio>();
            List<Cls_Ope_Eventos_Pnt_Ctrl_Categ_Tiempos_Negocio> List_Categoria_TiempoIdeal = new List<Cls_Ope_Eventos_Pnt_Ctrl_Categ_Tiempos_Negocio>();


            string jsonResultado = "";
            try
            {
                Mensaje.Titulo = "Alta";

                Obj_Punto_Control = JsonConvert.DeserializeObject<Cls_Ope_Eventos_Puntos_Control_Negocio>(jsonObject);

                List_Operadores = JsonConvert.DeserializeObject<List<Cls_Ope_Eventos_Pnt_Ctrl_Operador_Negocio>>(Obj_Punto_Control.tbl_operadores);
                List_Categoria_TiempoIdeal = JsonConvert.DeserializeObject<List<Cls_Ope_Eventos_Pnt_Ctrl_Categ_Tiempos_Negocio>>(Obj_Punto_Control.tbl_categoria_tiempoIdeal);


                using (var dbContext = new Sistema_TrazabilidadEntities())
                {
                    //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
                    //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
                    //  evento
                    Ope_Eventos_Puntos_Control Punto_Control = new Ope_Eventos_Puntos_Control();
                    Ope_Eventos_Puntos_Control Punto_Control_Nuevo = new Ope_Eventos_Puntos_Control();

                    Punto_Control.Jornada_Id = Convert.ToInt32(Obj_Punto_Control.Jornada_Id);
                    Punto_Control.Evento_Id = Convert.ToInt32(Obj_Punto_Control.Evento_Id);
                    Punto_Control.Clave = Obj_Punto_Control.Clave;
                    Punto_Control.Estatus = "ACTIVO";
                    Punto_Control.Numero = Obj_Punto_Control.Numero;
                    Punto_Control.Fecha = Obj_Punto_Control.Fecha;
                    Punto_Control.Ubicacion = Obj_Punto_Control.Ubicacion;
                    Punto_Control.Renglon = Obj_Punto_Control.Renglon;
                    Punto_Control.Distancia = Obj_Punto_Control.Distancia;
                    Punto_Control.Seña = Obj_Punto_Control.Seña;

                    Punto_Control.Tiempo_Ideal = Obj_Punto_Control.Tiempo_Ideal;
                    Punto_Control.Hora_Inicio = Obj_Punto_Control.Hora_Inicio;
                    Punto_Control.Hora_Fin = Obj_Punto_Control.Hora_Fin;
                    Punto_Control.Intervalo = Obj_Punto_Control.Intervalo;
                    Punto_Control.Usuario_Creo = Cls_Sesiones.Usuario;
                    Punto_Control.Fecha_Creo = DateTime.Now;
                    Punto_Control_Nuevo = dbContext.Ope_Eventos_Puntos_Control.Add(Punto_Control);



                    //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
                    //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
                    if (List_Operadores.Any())
                    {
                        foreach (var Detalles in List_Operadores)
                        {
                            if (Detalles.Relacion_Operador_Id == 0)
                            {
                                Ope_Eventos_Pnt_Ctrl_Operador Operador = new Ope_Eventos_Pnt_Ctrl_Operador();

                                Operador.Responsable_Id = Convert.ToInt32(Detalles.Responsable_Id);
                                Operador.Punto_Control_Id = Punto_Control.Punto_Control_Id;
                                Operador.Evento_Id = Punto_Control.Evento_Id;
                                Operador.Estatus = "ACTIVO";
                                Operador.Cargo = Detalles.Cargo;
                                Operador.Hora_Llegada = Detalles.Hora_Llegada;
                                Operador.Hora_Salida = Detalles.Hora_Salida;
                                Operador.Usuario_Creo = Cls_Sesiones.Usuario;
                                Operador.Fecha_Creo = DateTime.Now;

                                dbContext.Ope_Eventos_Pnt_Ctrl_Operador.Add(Operador);

                            }
                        }
                    }

                    //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
                    //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
                    if (List_Categoria_TiempoIdeal.Any())
                    {
                        foreach (var Detalles in List_Categoria_TiempoIdeal)
                        {
                            if (Detalles.Relacion_Id == 0)
                            {
                                Ope_Eventos_Pnt_Ctrl_Categ_Tiempos TiempoIdeal = new Ope_Eventos_Pnt_Ctrl_Categ_Tiempos();

                                TiempoIdeal.Punto_Control_Id = Punto_Control.Punto_Control_Id;
                                TiempoIdeal.Categoria_Id = Convert.ToInt32(Detalles.Categoria_Id);
                                TiempoIdeal.Tiempo_IDeal = Detalles.Tiempo_Ideal;
                                TiempoIdeal.Usuario_Creo = Cls_Sesiones.Usuario;
                                TiempoIdeal.Fecha_Creo = DateTime.Now;

                                dbContext.Ope_Eventos_Pnt_Ctrl_Categ_Tiempos.Add(TiempoIdeal);

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
            Cls_Ope_Eventos_Puntos_Control_Negocio Obj_Punto_Control = new Cls_Ope_Eventos_Puntos_Control_Negocio();
            List<Cls_Ope_Eventos_Pnt_Ctrl_Operador_Negocio> List_Operadores = new List<Cls_Ope_Eventos_Pnt_Ctrl_Operador_Negocio>();
            List<Cls_Ope_Eventos_Pnt_Ctrl_Operador_Negocio> List_Operadores_Eliminados = new List<Cls_Ope_Eventos_Pnt_Ctrl_Operador_Negocio>();


            List<Cls_Ope_Eventos_Pnt_Ctrl_Categ_Tiempos_Negocio> List_Categoria_TiempoIdeal = new List<Cls_Ope_Eventos_Pnt_Ctrl_Categ_Tiempos_Negocio>();
            List<Cls_Ope_Eventos_Pnt_Ctrl_Categ_Tiempos_Negocio> List_Categoria_TiempoIdeal_Eliminados = new List<Cls_Ope_Eventos_Pnt_Ctrl_Categ_Tiempos_Negocio>();

            string jsonResultado = "";
            try
            {
                Mensaje.Titulo = "Modificar";

                Obj_Punto_Control = JsonConvert.DeserializeObject<Cls_Ope_Eventos_Puntos_Control_Negocio>(jsonObject);
                List_Operadores = JsonConvert.DeserializeObject<List<Cls_Ope_Eventos_Pnt_Ctrl_Operador_Negocio>>(Obj_Punto_Control.tbl_operadores);
                List_Operadores_Eliminados = JsonConvert.DeserializeObject<List<Cls_Ope_Eventos_Pnt_Ctrl_Operador_Negocio>>(Obj_Punto_Control.tbl_operadores_eliminados);

                List_Categoria_TiempoIdeal = JsonConvert.DeserializeObject<List<Cls_Ope_Eventos_Pnt_Ctrl_Categ_Tiempos_Negocio>>(Obj_Punto_Control.tbl_categoria_tiempoIdeal);
                List_Categoria_TiempoIdeal_Eliminados = JsonConvert.DeserializeObject<List<Cls_Ope_Eventos_Pnt_Ctrl_Categ_Tiempos_Negocio>>(Obj_Punto_Control.tbl_categoria_tiempoIdeal_Eliminar);


                using (var dbContext = new Sistema_TrazabilidadEntities())
                {
                    //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
                    //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
                    Ope_Eventos_Puntos_Control Punto_Control = new Ope_Eventos_Puntos_Control();
                    Punto_Control = dbContext.Ope_Eventos_Puntos_Control.Where(w => w.Punto_Control_Id == Obj_Punto_Control.Punto_Control_Id).FirstOrDefault();

               
                    Punto_Control.Clave = Obj_Punto_Control.Clave;
                    Punto_Control.Estatus = "ACTIVO";
                    Punto_Control.Numero = Obj_Punto_Control.Numero;
                    Punto_Control.Fecha = Obj_Punto_Control.Fecha;
                    Punto_Control.Ubicacion = Obj_Punto_Control.Ubicacion;
                    Punto_Control.Renglon = Obj_Punto_Control.Renglon;
                    Punto_Control.Distancia = Obj_Punto_Control.Distancia;
                    Punto_Control.Seña = Obj_Punto_Control.Seña;
                    //Punto_Control.Tiempo_Ideal = Obj_Punto_Control.Tiempo_Ideal;
                    //Punto_Control.Intervalo = Obj_Punto_Control.Intervalo;
                    Punto_Control.Usuario_Modifico = Cls_Sesiones.Usuario;
                    Punto_Control.Fecha_Modifico = DateTime.Now;

                    Punto_Control.Hora_Inicio = Obj_Punto_Control.Hora_Inicio;
                    Punto_Control.Hora_Fin = Obj_Punto_Control.Hora_Fin;


                    //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
                    //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
                    if (List_Operadores.Any())
                    {
                        foreach (var Detalles in List_Operadores)
                        {
                            if (Detalles.Relacion_Operador_Id == 0)
                            {
                                Ope_Eventos_Pnt_Ctrl_Operador Operador = new Ope_Eventos_Pnt_Ctrl_Operador();

                                Operador.Responsable_Id = Convert.ToInt32(Detalles.Responsable_Id);
                                Operador.Punto_Control_Id = Punto_Control.Punto_Control_Id;
                                Operador.Evento_Id = Punto_Control.Evento_Id;
                                Operador.Estatus = "ACTIVO";
                                Operador.Cargo = Detalles.Cargo;
                                Operador.Hora_Llegada = Detalles.Hora_Llegada;
                                Operador.Hora_Salida = Detalles.Hora_Salida;
                                Operador.Usuario_Creo = Cls_Sesiones.Usuario;
                                Operador.Fecha_Creo = DateTime.Now;

                                dbContext.Ope_Eventos_Pnt_Ctrl_Operador.Add(Operador);

                            }
                        }
                    }


                    //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
                    //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

                    if (List_Operadores_Eliminados.Any())
                    {
                        foreach (var Detalles in List_Operadores_Eliminados)
                        {
                            Ope_Eventos_Pnt_Ctrl_Operador Operador_eliminado = new Ope_Eventos_Pnt_Ctrl_Operador();
                            Operador_eliminado = dbContext.Ope_Eventos_Pnt_Ctrl_Operador.Where(w => w.Relacion_Operador_Id == Detalles.Relacion_Operador_Id).FirstOrDefault();

                            Operador_eliminado.Estatus = "INACTIVO";
                            Operador_eliminado.Usuario_Modifico = Cls_Sesiones.Usuario;
                            Operador_eliminado.Fecha_Modifico = DateTime.Now;
                        }
                    }
                    
                    //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
                    //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
                    if (List_Categoria_TiempoIdeal.Any())
                    {
                        foreach (var Detalles in List_Categoria_TiempoIdeal)
                        {
                            if (Detalles.Relacion_Id == 0)
                            {
                                Ope_Eventos_Pnt_Ctrl_Categ_Tiempos TiempoIdeal = new Ope_Eventos_Pnt_Ctrl_Categ_Tiempos();
                                
                                TiempoIdeal.Punto_Control_Id = Punto_Control.Punto_Control_Id;
                                TiempoIdeal.Categoria_Id = Convert.ToInt32(Detalles.Categoria_Id);
                                TiempoIdeal.Tiempo_IDeal = Detalles.Tiempo_Ideal;
                                TiempoIdeal.Usuario_Creo = Cls_Sesiones.Usuario;
                                TiempoIdeal.Fecha_Creo = DateTime.Now;

                                dbContext.Ope_Eventos_Pnt_Ctrl_Categ_Tiempos.Add(TiempoIdeal);

                            }
                        }
                    }


                    //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
                    //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

                    if (List_Categoria_TiempoIdeal_Eliminados.Any())
                    {
                        foreach (var Detalles in List_Categoria_TiempoIdeal_Eliminados)
                        {
                            Ope_Eventos_Pnt_Ctrl_Categ_Tiempos Operador_eliminado = new Ope_Eventos_Pnt_Ctrl_Categ_Tiempos();
                            Operador_eliminado = dbContext.Ope_Eventos_Pnt_Ctrl_Categ_Tiempos.Where(w => w.Relacion_Id == Detalles.Relacion_Id).FirstOrDefault();

                            dbContext.Ope_Eventos_Pnt_Ctrl_Categ_Tiempos.Remove(Operador_eliminado);
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
            Cls_Ope_Eventos_Puntos_Control_Negocio Obj_Puntos_Control = new Cls_Ope_Eventos_Puntos_Control_Negocio();
            string jsonResultado = "";
            try
            {
                Mensaje.Titulo = "Cancelacion";

                Obj_Puntos_Control = JsonConvert.DeserializeObject<Cls_Ope_Eventos_Puntos_Control_Negocio>(jsonObject);

                using (var dbContext = new Sistema_TrazabilidadEntities())
                {
                    //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
                    //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
                    Ope_Eventos_Puntos_Control Puntos_Control = new Ope_Eventos_Puntos_Control();
                    Puntos_Control = dbContext.Ope_Eventos_Puntos_Control.Where(w => w.Punto_Control_Id == Obj_Puntos_Control.Punto_Control_Id).FirstOrDefault();

                    Puntos_Control.Estatus = "INACTIVO";
                    Puntos_Control.Usuario_Modifico = Cls_Sesiones.Usuario;
                    Puntos_Control.Fecha_Modifico = DateTime.Now;

                    //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
                    //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
                    var _var_responsables = (from _pts in dbContext.Ope_Eventos_Pnt_Ctrl_Operador
                                               where _pts.Punto_Control_Id == Obj_Puntos_Control.Punto_Control_Id
                                               select _pts);

                    if (_var_responsables.Any())
                    {
                        foreach (var Detalle in _var_responsables.ToList())
                        {
                            Ope_Eventos_Pnt_Ctrl_Operador Responsables = new Ope_Eventos_Pnt_Ctrl_Operador();
                            Responsables = dbContext.Ope_Eventos_Pnt_Ctrl_Operador.Where(w => w.Responsable_Id == Detalle.Responsable_Id).FirstOrDefault();

                            Responsables.Estatus = "INACTIVO";
                            Responsables.Usuario_Modifico = Cls_Sesiones.Usuario;
                            Responsables.Fecha_Modifico = DateTime.Now;
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

        #endregion


        #region Consultas

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string Consultar_Puntos_Control(string jsonObject)
        {
            string Json_Resultado = string.Empty;
            Cls_Ope_Eventos_Puntos_Control_Negocio Obj = new Cls_Ope_Eventos_Puntos_Control_Negocio();

            try
            {

                Obj = JsonConvert.DeserializeObject<Cls_Ope_Eventos_Puntos_Control_Negocio>(jsonObject);



                using (var dbContext = new Sistema_TrazabilidadEntities())
                {

                    var _puntos_control = (from _ev in dbContext.Ope_Eventos_Puntos_Control
                                           where _ev.Jornada_Id == Obj.Jornada_Id
                                           && _ev.Estatus == "ACTIVO"

                                           select new Cls_Ope_Eventos_Puntos_Control_Negocio
                                           {
                                               Punto_Control_Id = _ev.Punto_Control_Id,
                                               Jornada_Id = _ev.Jornada_Id,
                                               Evento_Id = _ev.Evento_Id,
                                               Clave = _ev.Clave,
                                               Estatus = _ev.Estatus,
                                               Numero = _ev.Numero ?? 0,
                                               Fecha = _ev.Fecha.Value,
                                               Ubicacion = _ev.Ubicacion,
                                               Str_Tiempo_Ideal = _ev.Tiempo_Ideal.ToString(),
                                               Str_Intervalo = _ev.Intervalo.ToString(),
                                               Str_Hora_Inicio = _ev.Hora_Inicio.ToString(),
                                               Str_Hora_Fin = _ev.Hora_Fin.ToString(),

                                               Renglon = _ev.Renglon,
                                               Distancia = _ev.Distancia,
                                               Seña = _ev.Seña,
                                           })
                                      .OrderBy(x => x.Numero).ToList();


                    Json_Resultado = JsonMapper.ToJson(_puntos_control.ToList());
                }
            }
            catch (Exception e)
            {

            }

            return Json_Resultado;
        }


        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void Consultar_Puntos_Combo()
        {
            string Json_Resultado = string.Empty;
            List<Cls_Select2> Lst_Combo = new List<Cls_Select2>();

            try
            {
                string q = string.Empty;
                string tipo = string.Empty;
                string jornada_id = string.Empty;

                NameValueCollection nvc = Context.Request.Form;

                if (!String.IsNullOrEmpty(nvc["q"]))
                {
                    q = nvc["q"].ToString().Trim();
                }
                if (!String.IsNullOrEmpty(nvc["jornada_id"]))
                {
                    jornada_id = nvc["jornada_id"].ToString().Trim();
                }

                using (var dbContext = new Sistema_TrazabilidadEntities())
                {

                    var _Busqueda = (from _puntos in dbContext.Ope_Eventos_Puntos_Control
                                     where _puntos.Estatus == "ACTIVO"
                                     && _puntos.Jornada_Id.ToString() == jornada_id
                                     && (_puntos.Clave.Contains(q) || _puntos.Ubicacion.Contains(q))

                                     select new Cls_Select2
                                     {

                                         id = _puntos.Punto_Control_Id.ToString(),
                                         text = _puntos.Clave + " - " + _puntos.Ubicacion,

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
        public string Consultar_Clave_Automatica_Punto_Control(string jsonObject)
        {
            string Json_Resultado = string.Empty;
            Cls_Ope_Eventos_Puntos_Control_Negocio Obj = new Cls_Ope_Eventos_Puntos_Control_Negocio();

            try
            {

                Obj = JsonConvert.DeserializeObject<Cls_Ope_Eventos_Puntos_Control_Negocio>(jsonObject);



                using (var dbContext = new Sistema_TrazabilidadEntities())
                {

                    var _jornadas_ = (from _ev in dbContext.Ope_Eventos_Puntos_Control

                                      where _ev.Evento_Id == Obj.Evento_Id
                                      && _ev.Jornada_Id == Obj.Jornada_Id

                                      select new Cls_Ope_Eventos_Puntos_Control_Negocio
                                      {
                                          Punto_Control_Id = _ev.Punto_Control_Id,
                                          Jornada_Id = _ev.Jornada_Id,
                                          Evento_Id = _ev.Evento_Id,
                                          Clave = _ev.Clave,
                                          Estatus = _ev.Estatus,
                                      })
                                      .OrderBy(x => x.Punto_Control_Id).ToList();


                    Json_Resultado = JsonMapper.ToJson(_jornadas_.ToList());
                }
            }
            catch (Exception e)
            {

            }

            return Json_Resultado;
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string Consultar_Responables_Punto_Control(string jsonObject)
        {
            string Json_Resultado = string.Empty;
            Cls_Ope_Eventos_Pnt_Ctrl_Operador_Negocio Obj = new Cls_Ope_Eventos_Pnt_Ctrl_Operador_Negocio();

            try
            {

                Obj = JsonConvert.DeserializeObject<Cls_Ope_Eventos_Pnt_Ctrl_Operador_Negocio>(jsonObject);



                using (var dbContext = new Sistema_TrazabilidadEntities())
                {

                    var _Responsables = (from _respon in dbContext.Ope_Eventos_Pnt_Ctrl_Operador

                                         join _catalogo_resp in dbContext.Cat_Responsables
                                            on _respon.Responsable_Id equals _catalogo_resp.Responsable_Id


                                         where _respon.Punto_Control_Id == Obj.Punto_Control_Id
                                         && _respon.Estatus == "ACTIVO"


                                      select new Cls_Ope_Eventos_Pnt_Ctrl_Operador_Negocio
                                      {
                                          Relacion_Operador_Id = _respon.Relacion_Operador_Id,
                                          Responsable_Id = _respon.Responsable_Id,
                                          Punto_Control_Id = _respon.Punto_Control_Id,
                                          Evento_Id = _respon.Evento_Id,
                                          Estatus = _respon.Estatus,
                                          Cargo = _respon.Cargo,
                                          Str_Hora_Llegada = _respon.Hora_Llegada.ToString(),
                                          Str_Hora_Salida = _respon.Hora_Salida.ToString(),
                                          Responsable = _catalogo_resp.Nombre,
                                      })
                                      .OrderByDescending(x => x.Cargo).ToList();


                    Json_Resultado = JsonMapper.ToJson(_Responsables.ToList());
                }
            }
            catch (Exception e)
            {

            }

            return Json_Resultado;
        }



        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string Consultar_TiempoIdeal_Categorias_Punto_Control(string jsonObject)
        {
            string Json_Resultado = string.Empty;
            Cls_Ope_Eventos_Pnt_Ctrl_Operador_Negocio Obj = new Cls_Ope_Eventos_Pnt_Ctrl_Operador_Negocio();

            try
            {

                Obj = JsonConvert.DeserializeObject<Cls_Ope_Eventos_Pnt_Ctrl_Operador_Negocio>(jsonObject);



                using (var dbContext = new Sistema_TrazabilidadEntities())
                {

                    var _Responsables = (from _tiempos in dbContext.Ope_Eventos_Pnt_Ctrl_Categ_Tiempos

                                         join _categorias in dbContext.Ope_Eventos_Categorias
                                            on _tiempos.Categoria_Id equals _categorias.Categoria_Id


                                         where _tiempos.Punto_Control_Id == Obj.Punto_Control_Id


                                         select new Cls_Ope_Eventos_Pnt_Ctrl_Categ_Tiempos_Negocio
                                         {
                                             Relacion_Id = _tiempos.Relacion_Id,
                                             Categoria_Id = _tiempos.Categoria_Id,
                                             Categoria = _categorias.Nombre,
                                             Punto_Control_Id = _tiempos.Punto_Control_Id,
                                             Str_Tiempo_Ideal = _tiempos.Tiempo_IDeal.ToString(),
                                         })
                                      .OrderByDescending(x => x.Categoria_Id).ToList();


                    Json_Resultado = JsonMapper.ToJson(_Responsables.ToList());
                }
            }
            catch (Exception e)
            {

            }

            return Json_Resultado;
        }


        #endregion


        #region Layout

        /// <summary>
        /// realiza la lectura del archivo layout
        /// </summary>
        /// <param name="json_object"></param>
        /// <returns></returns>
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string Leer_Archivo_Layout_Puntos_Control(string json_object)
        {
            Cls_Ope_Anexos_Negocio obj_datos = new Cls_Ope_Anexos_Negocio();//  variable de negocio que contendrá la información recibida
            DataTable dt_excel = new DataTable();//   variable para contener los datos del archivo excel
            Cls_Respuesta respuesta = new Cls_Respuesta();//    variable que indica la respuesta del proceso
            List<Cls_Ope_Eventos_Puntos_Control_Negocio> lista_puntos_control = new List<Cls_Ope_Eventos_Puntos_Control_Negocio>();//  variable para indicar los valores del datatable
            List<Cls_Ope_Eventos_Puntos_Control_Negocio> lista_puntos_control_registradas = new List<Cls_Ope_Eventos_Puntos_Control_Negocio>();//  variable para buscar los valores registrados en la base de datos
            DateTime dateTime_fecha_excel = new DateTime();

            try
            {
                //  se carga la información
                obj_datos = JsonMapper.ToObject<Cls_Ope_Anexos_Negocio>(json_object);


                var ruta = Server.MapPath(obj_datos.Ruta);//   se obtiene la ruta del archivo


                if (File.Exists(ruta))
                {
                    byte[] bin = File.ReadAllBytes(ruta);

                    //create a new Excel package in a memorystream
                    using (MemoryStream stream = new MemoryStream(bin))
                    {
                        //  se abre el archivo de excel
                        using (ExcelPackage excelPackage = new ExcelPackage(stream))
                        {
                            //  variable para manejar el entity
                            using (var dbContext = new Sistema_TrazabilidadEntities())
                            {
                                //  >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
                                //  >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
                                //  se carga la información del archivo layout
                                dt_excel = Cls_Ayudante_Epplus.ExcelPackageToDataTable(excelPackage, "Puntos_Control");

                                foreach (DataRow registro in dt_excel.Rows)
                                {
                                   
                                    registro.BeginEdit();

                                    string dateString = registro["Fecha"].ToString();
                                    string format = "MM/dd/yyyy";

                                    try
                                    {
                                        dateTime_fecha_excel = DateTime.ParseExact(dateString, format, CultureInfo.InvariantCulture);
                                    }
                                    catch
                                    {
                                        String[] arreglo = dateString.Split('/');
                                        dateString = arreglo[1] + "/" + arreglo[0] + "/" + arreglo[2];
                                        dateTime_fecha_excel = DateTime.ParseExact(dateString, format, CultureInfo.InvariantCulture);

                                    }

                                    registro["Fecha"] = dateTime_fecha_excel.ToShortDateString();

                                    registro.EndEdit();
                                    registro.AcceptChanges();
                                }

                                //  >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
                                //  >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
                                //  se valida que tenga información la variable
                                if (dt_excel != null && dt_excel.Rows.Count > 0)
                                {

                                    


                                    //  se convierte el datatable a lista
                                    var serie_jornadas = JsonConvert.SerializeObject(dt_excel);
                                    lista_puntos_control = JsonConvert.DeserializeObject<List<Cls_Ope_Eventos_Puntos_Control_Negocio>>(serie_jornadas);

                                    //  >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
                                    //  >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
                                    //  se ingresan los nuevos detalles
                                    foreach (var item in lista_puntos_control)//    variable para obtener los datos de la lista
                                    {
                                        //  >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
                                        //  >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>


                                        #region CLAVE
                                        //  validamos que tenga información el campo
                                        if (!String.IsNullOrEmpty(item.Clave))
                                        {
                                        }
                                        //  se indica que tiene error el registro
                                        else
                                        {
                                            item.Error = true;
                                            item.Mensaje_Error += "La CLAVE no esta capturado en el excel </br>";

                                        }
                                        #endregion

                                        //  >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
                                        //  >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>


                                        #region Numero
                                        //  validamos que tenga información el campo
                                        if (item.Numero > 0)
                                        {
                                        }
                                        //  se indica que tiene error el registro
                                        else
                                        {
                                            item.Error = true;
                                            item.Mensaje_Error += "El NÚMERO no esta capturado en el excel </br>";

                                        }
                                        #endregion

                                        //  >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
                                        //  >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>


                                        #region Fecha_Inicio
                                        //  validamos que tenga información el campo
                                        if (!String.IsNullOrEmpty(item.Fecha.Value.ToString()))
                                        {
                                        }
                                        //  se indica que tiene error el registro
                                        else
                                        {
                                            item.Error = true;
                                            item.Mensaje_Error += "La FECHA no esta capturado en el excel </br>";

                                        }
                                        #endregion

                                        //  >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
                                        //  >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>


                                        #region Hora_Inicio
                                        //  validamos que tenga información el campo
                                        if (!String.IsNullOrEmpty(item.Hora_Inicio.Value.ToString()))
                                        {
                                        }
                                        //  se indica que tiene error el registro
                                        else
                                        {
                                            item.Error = true;
                                            item.Mensaje_Error += "La HORA DE INICIO no esta capturado en el excel </br>";

                                        }
                                        #endregion


                                        //  >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
                                        //  >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>


                                        #region Hora_Fin
                                        //  validamos que tenga información el campo
                                        if (!String.IsNullOrEmpty(item.Hora_Fin.Value.ToString()))
                                        {
                                        }
                                        //  se indica que tiene error el registro
                                        else
                                        {
                                            item.Error = true;
                                            item.Mensaje_Error += "La HORA DE TERMINO no esta capturado en el excel </br>";

                                        }
                                        #endregion
                                        //  >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
                                        //  >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>



                                        #region Ubicacion
                                        //  validamos que tenga información el campo
                                        if (!String.IsNullOrEmpty(item.Ubicacion))
                                        {
                                        }
                                        //  se indica que tiene error el registro
                                        else
                                        {
                                            item.Error = true;
                                            item.Mensaje_Error += "La UBICACIÓN no esta capturado en el excel </br>";

                                        }
                                        #endregion



                                        //  >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
                                        //  >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

                                        #region Renglon
                                        //  validamos que tenga información el campo
                                        if (!String.IsNullOrEmpty(item.Renglon))
                                        {
                                        }
                                        //  se indica que tiene error el registro
                                        else
                                        {
                                            item.Error = true;
                                            item.Mensaje_Error += "El RENGLÓN no esta capturado en el excel </br>";

                                        }
                                        #endregion




                                        //  >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
                                        //  >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>


                                        #region Distancia
                                        //  validamos que tenga información el campo
                                        if (!String.IsNullOrEmpty(item.Distancia))
                                        {
                                        }
                                        //  se indica que tiene error el registro
                                        else
                                        {
                                            item.Error = true;
                                            item.Mensaje_Error += "La DISTANCIA no esta capturado en el excel </br>";

                                        }
                                        #endregion

                                        //  >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
                                        //  >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>


                                        #region Seña
                                        //  validamos que tenga información el campo
                                        if (!String.IsNullOrEmpty(item.Seña))
                                        {
                                        }
                                        //  se indica que tiene error el registro
                                        else
                                        {
                                            item.Error = true;
                                            item.Mensaje_Error += "La SEÑA no esta capturado en el excel </br>";

                                        }
                                        #endregion

                                        //  >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
                                        //  >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>


                                        #region Tiempo_Ideal
                                        //  validamos que tenga información el campo
                                        if (!String.IsNullOrEmpty(item.Tiempo_Ideal.Value.ToString()))
                                        {
                                        }
                                        //  se indica que tiene error el registro
                                        else
                                        {
                                            item.Error = true;
                                            item.Mensaje_Error += "El TIEMPO IDEAL no esta capturado en el excel </br>";

                                        }
                                        #endregion



                                        //  >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
                                        //  >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>


                                        #region Responsable
                                        //  validamos que tenga información el campo
                                        if (!String.IsNullOrEmpty(item.Responsable))
                                        {
                                            //  se consultara si existe información registrada 
                                            var lista_responsable = (from _responsable in dbContext.Cat_Responsables
                                                                     where _responsable.Nombre == item.Responsable

                                                                     select new Cls_Cat_Responsables_Negocio
                                                                     {
                                                                         Responsable_Id = _responsable.Responsable_Id,
                                                                         Nombre = _responsable.Nombre,
                                                                     }
                                                                            ).ToList();//   variable con la que se comparara si el registro ya existe

                                            //  validamos que no existe la curp
                                            if (lista_responsable.Any())
                                            {
                                                //  se recorre la lista de los cuentas
                                                foreach (var _detalle_registro in lista_responsable)//  variable que almacena la información de la lista
                                                {
                                                    item.Responsable_Id_Layout = _detalle_registro.Responsable_Id;
                                                }
                                            }
                                            //  se indica que ya esta registrado
                                            else
                                            {
                                                //item.Repetido = true;
                                                item.Error = true;
                                                item.Mensaje_Error += "No se encuentra registrado con el responsable [" + item.Responsable + "]</br>";
                                            }


                                        }
                                        //  se indica que tiene error el registro
                                        else
                                        {
                                            item.Error = true;
                                            item.Mensaje_Error += "El RESPOSNABLE no esta capturado en el excel </br>";

                                        }
                                        #endregion


                                        //  >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
                                        //  >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

                                    }
                                }
                                else
                                {
                                    respuesta.Estatus = false;
                                    respuesta.Mensaje = "No se encontraron registros. Favor de revisar los datos del archivo en la hoja beneficiarios.";
                                }

                                var lista_beneficiarios_nuevos = lista_puntos_control.Where(x => (x.Repetido == false && x.Error == false)).ToList();//   variable para obtener los nuevos elementos

                                respuesta.Estatus = true;
                                respuesta.Nuevos = JsonConvert.SerializeObject(lista_beneficiarios_nuevos);

                            }


                        }
                    }

                    File.Delete(ruta);

                }

                else
                {
                    respuesta.Estatus = false;
                    respuesta.Mensaje = "No se encontró el archivo. Favor de revisar el archivo.";
                }
            }
            catch (Exception e)
            {
                respuesta.Estatus = false;
                respuesta.Mensaje = "Error al leer el archivo. " + e.Message;
            }

            return JsonConvert.SerializeObject(respuesta);
        }



        /// <summary>
        /// Método que realiza el alta de los centros de trabajo.
        /// </summary>
        /// <param name="json_object">Variable que recibe los datos de los js</param>
        /// <returns>Objeto serializado con los resultados de la operación</returns>
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string Alta_Layout(string json_object)
        {
            Cls_Ope_Eventos_Puntos_Control_Negocio obj_ = null;//variable para obtener la información ingresada en el javascript
            List<Cls_Ope_Eventos_Puntos_Control_Negocio> lista_jornadas = new List<Cls_Ope_Eventos_Puntos_Control_Negocio>();// lista para contener a los beneficiarios
            string json_resultado = string.Empty;//variable para guardar el resultado de la función
            Cls_Mensaje mensaje = new Cls_Mensaje();//variable para guardar el mensaje de salida
            Int32 punto_control_id = 0;//   variable para captar el id del punto de control

            try
            {
                mensaje.Titulo = "Alta_Layout";
                obj_ = JsonMapper.ToObject<Cls_Ope_Eventos_Puntos_Control_Negocio>(json_object);
                lista_jornadas = JsonConvert.DeserializeObject<List<Cls_Ope_Eventos_Puntos_Control_Negocio>>(obj_.Tabla_Layout);


                using (var dbContext = new Sistema_TrazabilidadEntities())//variable para manejar el entity
                {
                    //  se inicializa la transacción
                    using (var transaction = dbContext.Database.BeginTransaction())//   variable que se utiliza para la transaccion
                    {
                        try
                        {
                            //  se recorre la lista de los cuentas
                            foreach (var _detalle_registro in lista_jornadas)//  variable que almacena la información de la lista
                            {


                                //  se consultara si existe información registrada 
                                var _consulta_punto_control = (from _punto_control in dbContext.Ope_Eventos_Puntos_Control
                                                               where _punto_control.Clave == _detalle_registro.Clave
                                                               && _punto_control.Evento_Id == obj_.Evento_Id
                                                               && _punto_control.Jornada_Id == obj_.Jornada_Id

                                                               select new Cls_Ope_Eventos_Puntos_Control_Negocio
                                                               {
                                                                   Punto_Control_Id = _punto_control.Punto_Control_Id,
                                                               }
                                                                ).ToList();//   variable con la que se comparara si el registro ya existe



                                //  validamos que no existe
                                if (!_consulta_punto_control.Any())
                                {
                                    Ope_Eventos_Puntos_Control Punto_Control = new Ope_Eventos_Puntos_Control();
                                    Ope_Eventos_Puntos_Control Punto_Control_Nuevo = new Ope_Eventos_Puntos_Control();

                                    Punto_Control.Jornada_Id = Convert.ToInt32(obj_.Jornada_Id);
                                    Punto_Control.Evento_Id = Convert.ToInt32(obj_.Evento_Id);
                                    Punto_Control.Clave = _detalle_registro.Clave;
                                    Punto_Control.Estatus = "ACTIVO";
                                    Punto_Control.Numero = _detalle_registro.Numero;
                                    Punto_Control.Fecha = _detalle_registro.Fecha;
                                    Punto_Control.Ubicacion = _detalle_registro.Ubicacion;
                                    Punto_Control.Renglon = _detalle_registro.Renglon;
                                    Punto_Control.Distancia = _detalle_registro.Distancia;
                                    Punto_Control.Seña = _detalle_registro.Seña;

                                    Punto_Control.Tiempo_Ideal = _detalle_registro.Tiempo_Ideal;
                                    Punto_Control.Hora_Inicio = _detalle_registro.Hora_Inicio;
                                    Punto_Control.Hora_Fin = _detalle_registro.Hora_Fin;
                                    Punto_Control.Intervalo = _detalle_registro.Intervalo;
                                    Punto_Control.Usuario_Creo = Cls_Sesiones.Usuario;
                                    Punto_Control.Fecha_Creo = DateTime.Now;
                                    Punto_Control_Nuevo = dbContext.Ope_Eventos_Puntos_Control.Add(Punto_Control);

                                    dbContext.SaveChanges();



                                    punto_control_id = Punto_Control_Nuevo.Punto_Control_Id;



                                    //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
                                    //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
                                    Ope_Eventos_Pnt_Ctrl_Operador Operador = new Ope_Eventos_Pnt_Ctrl_Operador();

                                    Operador.Responsable_Id = Convert.ToInt32(_detalle_registro.Responsable_Id_Layout);
                                    Operador.Punto_Control_Id = punto_control_id;
                                    Operador.Evento_Id = Convert.ToInt32(obj_.Evento_Id);
                                    Operador.Estatus = "ACTIVO";
                                    Operador.Cargo = "RESPONSABLE";
                                    Operador.Hora_Llegada = _detalle_registro.Hora_Inicio;
                                    Operador.Hora_Salida = _detalle_registro.Hora_Fin;
                                    Operador.Usuario_Creo = Cls_Sesiones.Usuario;
                                    Operador.Fecha_Creo = DateTime.Now;

                                    dbContext.Ope_Eventos_Pnt_Ctrl_Operador.Add(Operador);
                                    dbContext.SaveChanges();


                                    //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
                                    //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<


                                }
                                //  se le asigna el id del punto de control
                                else
                                { //  se recorre la lista de los cuentas
                                    foreach (var _detalle_consulta in _consulta_punto_control)//  variable que almacena la información de la lista
                                    {
                                        punto_control_id = Convert.ToInt32(_detalle_consulta.Punto_Control_Id);
                                    }
                                }

                                //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
                                //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
                                #region tiempo ideal


                                //  se consultara si existe información registrada 
                                var _consultar_tiempos = (from _tiempo in dbContext.Ope_Eventos_Pnt_Ctrl_Categ_Tiempos
                                                               where _tiempo.Punto_Control_Id ==  punto_control_id
                                                               && _tiempo.Categoria_Id == obj_.Categoria_Id_Layout

                                                               select new Cls_Ope_Eventos_Pnt_Ctrl_Categ_Tiempos_Negocio
                                                               {
                                                                   Relacion_Id = _tiempo.Relacion_Id,
                                                               }
                                                                ).ToList();//   variable con la que se comparara si el registro ya existe


                                //  validamos que no existe
                                if (!_consultar_tiempos.Any())
                                {
                                    Ope_Eventos_Pnt_Ctrl_Categ_Tiempos TiempoIdeal = new Ope_Eventos_Pnt_Ctrl_Categ_Tiempos();

                                    TiempoIdeal.Punto_Control_Id = punto_control_id;
                                    TiempoIdeal.Categoria_Id = Convert.ToInt32(obj_.Categoria_Id_Layout);
                                    TiempoIdeal.Tiempo_IDeal = _detalle_registro.Tiempo_Ideal;
                                    TiempoIdeal.Usuario_Creo = Cls_Sesiones.Usuario;
                                    TiempoIdeal.Fecha_Creo = DateTime.Now;

                                    dbContext.Ope_Eventos_Pnt_Ctrl_Categ_Tiempos.Add(TiempoIdeal);
                                    dbContext.SaveChanges();
                                }


                                #endregion

                                //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
                                //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
                            }


                            //  se ejecuta la transacción
                            transaction.Commit();


                            mensaje.Estatus = "success";
                            mensaje.Mensaje = "La operación se completó correctamente.";

                        }
                        catch (Exception ex)//  variable para atrapar el error
                        {
                            //  se realiza el rollback de la transacción
                            transaction.Rollback();

                            //  se indica cual es el error que se presento
                            mensaje.Mensaje = "Error Técnico. " + ex.Message;
                            mensaje.Estatus = "error";

                        }
                    }
                }
            }
            catch (Exception e)//  variable para atrapar el error
            {
                mensaje.Titulo = "Informe Técnico";
                mensaje.Estatus = "error";
                if (e.InnerException.Message.Contains("Los datos de cadena o binarios se truncarían"))//se valida elmensaje de la excepción
                    mensaje.Mensaje =
                        "Alguno de los campos que intenta insertar tiene un tamaño mayor al establecido en la base de datos. <br /><br />" +
                        "<i class='fa fa-angle-double-right' ></i>&nbsp;&nbsp; Los datos de cadena o binarios se truncarían";
                else if (e.InnerException.InnerException.Message.Contains("Cannot insert duplicate key row in object"))//se valida elmensaje de la excepción
                    mensaje.Mensaje =
                        "Existen campos definidos como claves que no pueden duplicarse. <br />" +
                        "<i class='fa fa-angle-double-right' ></i>&nbsp;&nbsp; Por favor revisar que no este ingresando datos duplicados.";
                else//sino fue ninguno de los errores anteriores se muestra el error obtenido
                    mensaje.Mensaje = "Informe técnico: " + e.Message;
            }
            finally
            {
                json_resultado = JsonMapper.ToJson(mensaje);
            }
            return json_resultado;
        }



        /// <summary>
        /// Método que descarga la plantilla del layout
        /// </summary>
        /// <returns>Objeto serializado con los resultados de la operación</returns>
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string Descargar_Plantilla_Layout()
        {

            string json_resultado = string.Empty;//variable para guardar el resultado de la función
            Cls_Mensaje mensaje = new Cls_Mensaje();//variable para guardar el mensaje de salida

            try
            {
                mensaje.Titulo = "Descarga";



                var ruta = Server.MapPath("../../../PlantillaExcel/plantilla_puntos_control.xlsx");//   se obtiene la ruta del archivo



                mensaje.Estatus = "success";
                mensaje.Mensaje = "La operación se completó correctamente.";
                mensaje.Ruta_Archivo_Excel = ruta;
                mensaje.Nombre_Excel = "Plantilla_Puntos_Control.xlsx";
            }

            catch (Exception e)//  variable para atrapar el error
            {
                mensaje.Titulo = "Informe Técnico";
                mensaje.Estatus = "error";
                if (e.InnerException.Message.Contains("Los datos de cadena o binarios se truncarían"))//se valida elmensaje de la excepción
                    mensaje.Mensaje =
                        "Alguno de los campos que intenta insertar tiene un tamaño mayor al establecido en la base de datos. <br /><br />" +
                        "<i class='fa fa-angle-double-right' ></i>&nbsp;&nbsp; Los datos de cadena o binarios se truncarían";
                else if (e.InnerException.InnerException.Message.Contains("Cannot insert duplicate key row in object"))//se valida elmensaje de la excepción
                    mensaje.Mensaje =
                        "Existen campos definidos como claves que no pueden duplicarse. <br />" +
                        "<i class='fa fa-angle-double-right' ></i>&nbsp;&nbsp; Por favor revisar que no este ingresando datos duplicados.";
                else//sino fue ninguno de los errores anteriores se muestra el error obtenido
                    mensaje.Mensaje = "Informe técnico: " + e.Message;
            }
            finally
            {
                json_resultado = JsonMapper.ToJson(mensaje);
            }
            return json_resultado;
        }
        #endregion

    }
}
