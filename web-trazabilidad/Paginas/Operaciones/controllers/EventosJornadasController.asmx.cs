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

namespace web_trazabilidad.Paginas.Operaciones.controllers
{
    /// <summary>
    /// Summary description for EventosJornadasController
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class EventosJornadasController : System.Web.Services.WebService
    {

        #region Metodos 

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string Alta(String jsonObject)
        {
            Cls_Mensaje Mensaje = new Cls_Mensaje();
            Cls_Ope_Eventos_Jornadas_Negocio Obj_Jornada = new Cls_Ope_Eventos_Jornadas_Negocio();

            string jsonResultado = "";
            try
            {
                Mensaje.Titulo = "Alta";

                Obj_Jornada = JsonConvert.DeserializeObject<Cls_Ope_Eventos_Jornadas_Negocio>(jsonObject);

                using (var dbContext = new Sistema_TrazabilidadEntities())
                {
                    //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
                    //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
                    //  evento
                    Ope_Eventos_Jornadas Jornada = new Ope_Eventos_Jornadas();
                    Ope_Eventos_Jornadas Jornada_Nueva = new Ope_Eventos_Jornadas();

                    Jornada.Evento_Id = Convert.ToInt32(Obj_Jornada.Evento_Id);
                    Jornada.Clave = Obj_Jornada.Clave;
                    Jornada.Nombre = Obj_Jornada.Nombre;
                    Jornada.Estatus = "ACTIVO";
                    Jornada.Tipo = Obj_Jornada.Tipo;
                    Jornada.Punto_Inicial = Obj_Jornada.Punto_Inicial;
                    Jornada.Punto_Final = Obj_Jornada.Punto_Final;
                    Jornada.Fecha_Inicio = Obj_Jornada.Fecha_Inicio;
                    Jornada.Comentarios = Obj_Jornada.Comentarios;
                    Jornada.Usuario_Creo = Cls_Sesiones.Usuario;
                    Jornada.Fecha_Creo = DateTime.Now;

                    Jornada_Nueva = dbContext.Ope_Eventos_Jornadas.Add(Jornada);
                    
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
            Cls_Ope_Eventos_Jornadas_Negocio Obj_Jornada = new Cls_Ope_Eventos_Jornadas_Negocio();
            string jsonResultado = "";
            try
            {
                Mensaje.Titulo = "Modificar";

                Obj_Jornada = JsonConvert.DeserializeObject<Cls_Ope_Eventos_Jornadas_Negocio>(jsonObject);

                using (var dbContext = new Sistema_TrazabilidadEntities())
                {
                    //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
                    //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
                    Ope_Eventos_Jornadas Jornada = new Ope_Eventos_Jornadas();
                    Jornada = dbContext.Ope_Eventos_Jornadas.Where(w => w.Jornada_Id == Obj_Jornada.Jornada_Id).FirstOrDefault();

                    Jornada.Clave = Obj_Jornada.Clave;
                    Jornada.Nombre = Obj_Jornada.Nombre;
                    Jornada.Tipo = Obj_Jornada.Tipo;
                    Jornada.Punto_Inicial = Obj_Jornada.Punto_Inicial;
                    Jornada.Punto_Final = Obj_Jornada.Punto_Final;
                    Jornada.Fecha_Inicio = Obj_Jornada.Fecha_Inicio;
                    Jornada.Comentarios = Obj_Jornada.Comentarios;
                    Jornada.Usuario_Modifico = Cls_Sesiones.Usuario;
                    Jornada.Fecha_Modifico = DateTime.Now;


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
            Cls_Ope_Eventos_Jornadas_Negocio Obj_Jornada = new Cls_Ope_Eventos_Jornadas_Negocio();
            string jsonResultado = "";
            try
            {
                Mensaje.Titulo = "Cancelacion";

                Obj_Jornada = JsonConvert.DeserializeObject<Cls_Ope_Eventos_Jornadas_Negocio>(jsonObject);

                using (var dbContext = new Sistema_TrazabilidadEntities())
                {
                    //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
                    //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
                    #region Jornada
                    Ope_Eventos_Jornadas Jornada = new Ope_Eventos_Jornadas();
                    Jornada = dbContext.Ope_Eventos_Jornadas.Where(w => w.Jornada_Id == Obj_Jornada.Jornada_Id).FirstOrDefault();


                    Jornada.Estatus = "INACTIVO";
                    Jornada.Usuario_Modifico = Cls_Sesiones.Usuario;
                    Jornada.Fecha_Modifico = DateTime.Now;
                    #endregion

                    //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
                    //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
                    var _var_Puntos_Control = (from _pts in dbContext.Ope_Eventos_Puntos_Control
                                               where _pts.Jornada_Id == Obj_Jornada.Jornada_Id
                                               select _pts);

                    if (_var_Puntos_Control.Any())
                    {
                        foreach (var Detalle in _var_Puntos_Control.ToList())
                        {
                            #region Punto Control
                            
                            Ope_Eventos_Puntos_Control Puntos_Control = new Ope_Eventos_Puntos_Control();
                            Puntos_Control = dbContext.Ope_Eventos_Puntos_Control.Where(w => w.Punto_Control_Id == Detalle.Punto_Control_Id).FirstOrDefault();

                            Puntos_Control.Estatus = "INACTIVO";
                            Puntos_Control.Usuario_Modifico = Cls_Sesiones.Usuario;
                            Puntos_Control.Fecha_Modifico = DateTime.Now;
                            #endregion


                            //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
                            //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
                            var _var_responsables = (from _pts in dbContext.Ope_Eventos_Pnt_Ctrl_Operador
                                                     where _pts.Punto_Control_Id == Detalle.Punto_Control_Id
                                                     select _pts);

                            if (_var_responsables.Any())
                            {
                                foreach (var Detalle_Responsable in _var_responsables.ToList())
                                {
                                    #region Responsable
                                    
                                    Ope_Eventos_Pnt_Ctrl_Operador Responsables = new Ope_Eventos_Pnt_Ctrl_Operador();
                                    Responsables = dbContext.Ope_Eventos_Pnt_Ctrl_Operador.Where(w => w.Responsable_Id == Detalle_Responsable.Responsable_Id).FirstOrDefault();

                                    Responsables.Estatus = "INACTIVO";
                                    Responsables.Usuario_Modifico = Cls_Sesiones.Usuario;
                                    Responsables.Fecha_Modifico = DateTime.Now;
                                    #endregion
                                }

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


        #endregion




        #region Consultas
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string Consultar_Jornadas_Evento(string jsonObject)
        {
            string Json_Resultado = string.Empty;
            Cls_Ope_Eventos_Jornadas_Negocio Obj = new Cls_Ope_Eventos_Jornadas_Negocio();

            try
            {

                Obj = JsonConvert.DeserializeObject<Cls_Ope_Eventos_Jornadas_Negocio>(jsonObject);



                using (var dbContext = new Sistema_TrazabilidadEntities())
                {

                    var _jornadas_ = (from _ev in dbContext.Ope_Eventos_Jornadas
                                    where _ev.Evento_Id == Obj.Evento_Id
                                    && _ev.Estatus == "ACTIVO"

                                    select new Cls_Ope_Eventos_Jornadas_Negocio
                                    {
                                        Jornada_Id = _ev.Jornada_Id,
                                        Evento_Id = _ev.Evento_Id,
                                        Clave = _ev.Clave,
                                        Nombre = _ev.Nombre,
                                        Estatus = _ev.Estatus,
                                        Tipo = _ev.Tipo,
                                        Punto_Inicial = _ev.Punto_Inicial,
                                        Fecha_Inicio = _ev.Fecha_Inicio,
                                        Punto_Final = _ev.Punto_Final,
                                        Comentarios = _ev.Comentarios,
                                    })
                                      .OrderBy(x => x.Clave).ToList();


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
        public string Consultar_Clave_Automatica_Jornada(string jsonObject)
        {
            string Json_Resultado = string.Empty;
            Cls_Ope_Eventos_Jornadas_Negocio Obj = new Cls_Ope_Eventos_Jornadas_Negocio();

            try
            {

                Obj = JsonConvert.DeserializeObject<Cls_Ope_Eventos_Jornadas_Negocio>(jsonObject);



                using (var dbContext = new Sistema_TrazabilidadEntities())
                {

                    var _jornadas_ = (from _ev in dbContext.Ope_Eventos_Jornadas

                                      where _ev.Evento_Id == Obj.Evento_Id

                                      select new Cls_Ope_Eventos_Jornadas_Negocio
                                      {
                                          Jornada_Id = _ev.Jornada_Id,
                                          Evento_Id = _ev.Evento_Id,
                                          Clave = _ev.Clave,
                                          Nombre = _ev.Nombre,
                                          Estatus = _ev.Estatus,
                                          Tipo = _ev.Tipo,
                                          Punto_Inicial = _ev.Punto_Inicial,
                                          Punto_Final = _ev.Punto_Final,
                                          Comentarios = _ev.Comentarios,
                                      })
                                      .OrderBy(x => x.Jornada_Id).ToList();


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
        public void Consultar_Jornadas_Combo()
        {
            string Json_Resultado = string.Empty;
            List<Cls_Select2> Lst_Combo = new List<Cls_Select2>();

            try
            {
                string q = string.Empty;
                string tipo = string.Empty;
                string evento_id = string.Empty;

                NameValueCollection nvc = Context.Request.Form;

                if (!String.IsNullOrEmpty(nvc["q"]))
                {
                    q = nvc["q"].ToString().Trim();
                }
                if (!String.IsNullOrEmpty(nvc["evento_id"]))
                {
                    evento_id = nvc["evento_id"].ToString().Trim();
                }

                using (var dbContext = new Sistema_TrazabilidadEntities())
                {

                    var _Busqueda = (from _jornada in dbContext.Ope_Eventos_Jornadas
                                      where _jornada.Estatus == "ACTIVO"
                                      && _jornada.Evento_Id.ToString() == evento_id
                                      && (_jornada.Clave.Contains(q) || _jornada.Nombre.Contains(q))

                                      select new Cls_Select2
                                      {

                                          id = _jornada.Jornada_Id.ToString(),
                                          text = _jornada.Clave + " - " + _jornada.Nombre,

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

        #endregion



        #region Layout

        /// <summary>
        /// realiza la lectura del archivo layout
        /// </summary>
        /// <param name="json_object"></param>
        /// <returns></returns>
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string Leer_Archivo_Layout_Jornadas(string json_object)
        {
            Cls_Ope_Anexos_Negocio obj_datos = new Cls_Ope_Anexos_Negocio();//  variable de negocio que contendrá la información recibida
            DataTable dt_excel = new DataTable();//   variable para contener los datos del archivo excel
             Cls_Respuesta respuesta = new Cls_Respuesta();//    variable que indica la respuesta del proceso
            List<Cls_Ope_Eventos_Jornadas_Negocio> lista_jornada = new List<Cls_Ope_Eventos_Jornadas_Negocio>();//  variable para indicar los valores del datatable
            List<Cls_Ope_Eventos_Jornadas_Negocio> lista_jornadas_registradas = new List<Cls_Ope_Eventos_Jornadas_Negocio>();//  variable para buscar los valores registrados en la base de datos

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
                                dt_excel = Cls_Ayudante_Epplus.ExcelPackageToDataTable(excelPackage, "Jornadas");

                                //  >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
                                //  >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
                                //  se valida que tenga información la variable
                                if (dt_excel != null && dt_excel.Rows.Count > 0)
                                {
                                    //  se convierte el datatable a lista
                                    var serie_jornadas = JsonConvert.SerializeObject(dt_excel);
                                    lista_jornada = JsonConvert.DeserializeObject<List<Cls_Ope_Eventos_Jornadas_Negocio>>(serie_jornadas);

                                    //  >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
                                    //  >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
                                    //  se ingresan los nuevos detalles
                                    foreach (var item in lista_jornada)//    variable para obtener los datos de la lista
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


                                        #region Nombre
                                        //  validamos que tenga información el campo
                                        if (!String.IsNullOrEmpty(item.Nombre))
                                        {
                                        }
                                        //  se indica que tiene error el registro
                                        else
                                        {
                                            item.Error = true;
                                            item.Mensaje_Error += "El nombre no esta capturado en el excel </br>";

                                        }
                                        #endregion

                                        //  >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
                                        //  >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

                                        #region tipo
                                        //  validamos que tenga información el campo
                                        if (!String.IsNullOrEmpty(item.Tipo))
                                        {
                                        }
                                        //  se indica que tiene error el registro
                                        else
                                        {
                                            item.Error = true;
                                            item.Mensaje_Error += "El TIPO no esta capturado en el excel </br>";

                                        }
                                        #endregion

                                        //  >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
                                        //  >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>


                                        #region Punto_Inicial
                                        //  validamos que tenga información el campo
                                        if (!String.IsNullOrEmpty(item.Punto_Inicial))
                                        {
                                        }
                                        //  se indica que tiene error el registro
                                        else
                                        {
                                            item.Error = true;
                                            item.Mensaje_Error += "El PUNTO DE INICIO no esta capturado en el excel </br>";

                                        }
                                        #endregion

                                        //  >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
                                        //  >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>


                                        #region Punto_Final
                                        //  validamos que tenga información el campo
                                        if (!String.IsNullOrEmpty(item.Punto_Final))
                                        {
                                        }
                                        //  se indica que tiene error el registro
                                        else
                                        {
                                            item.Error = true;
                                            item.Mensaje_Error += "El PUNTO FINAL no esta capturado en el excel </br>";

                                        }
                                        #endregion

                                        //  >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
                                        //  >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>


                                        #region Fecha_Inicio
                                        //  validamos que tenga información el campo
                                        if (!String.IsNullOrEmpty(item.Fecha_Inicio.Value.ToString()))
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

                                    }
                                }
                                else
                                {
                                    respuesta.Estatus = false;
                                    respuesta.Mensaje = "No se encontraron registros. Favor de revisar los datos del archivo en la hoja beneficiarios.";
                                }

                                var lista_beneficiarios_nuevos = lista_jornada.Where(x => (x.Repetido == false && x.Error == false)).ToList();//   variable para obtener los nuevos elementos
                              
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
            Cls_Ope_Eventos_Jornadas_Negocio obj_ = null;//variable para obtener la información ingresada en el javascript
            List<Cls_Ope_Eventos_Jornadas_Negocio> lista_jornadas = new List<Cls_Ope_Eventos_Jornadas_Negocio>();// lista para contener a los beneficiarios
            string json_resultado = string.Empty;//variable para guardar el resultado de la función
            Cls_Mensaje mensaje = new Cls_Mensaje();//variable para guardar el mensaje de salida

            try
            {
                mensaje.Titulo = "Alta_Layout";
                obj_ = JsonMapper.ToObject<Cls_Ope_Eventos_Jornadas_Negocio>(json_object);
                lista_jornadas = JsonConvert.DeserializeObject<List<Cls_Ope_Eventos_Jornadas_Negocio>>(obj_.Tabla_Layout);


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

                                var jornada = new Ope_Eventos_Jornadas();//   variable para colocar los datos a guardar
                                var _jornadas = new Ope_Eventos_Jornadas();// variable para colocar los datos a guardar

                                //  se ingresan los valores
                                jornada.Evento_Id = Convert.ToInt32(obj_.Evento_Id);
                                jornada.Clave = _detalle_registro.Clave;
                                jornada.Nombre = _detalle_registro.Nombre;
                                jornada.Estatus = "ACTIVO";
                                jornada.Tipo = _detalle_registro.Tipo;
                                jornada.Punto_Inicial = _detalle_registro.Punto_Inicial;
                                jornada.Punto_Final = _detalle_registro.Punto_Final;
                                jornada.Fecha_Inicio = _detalle_registro.Fecha_Inicio;
                                jornada.Comentarios = _detalle_registro.Comentarios;
                                jornada.Usuario_Creo = Cls_Sesiones.Usuario;
                                jornada.Fecha_Creo = DateTime.Now;

                                _jornadas = dbContext.Ope_Eventos_Jornadas.Add(jornada);
                                dbContext.SaveChanges();

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



                var ruta = Server.MapPath("../../../PlantillaExcel/plantilla jornadas.xlsx");//   se obtiene la ruta del archivo



                mensaje.Estatus = "success";
                mensaje.Mensaje = "La operación se completó correctamente.";
                mensaje.Ruta_Archivo_Excel = ruta;
                mensaje.Nombre_Excel = "Plantilla_Jornadas.xlsx";
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
