using datos_trazabilidad;
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
using web_trazabilidad.Models.Negocio.Catalogos;

namespace web_trazabilidad.Paginas.Catalogos.controllers
{
    /// <summary>
    /// Summary description for ParticipantesController
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class ParticipantesController : System.Web.Services.WebService
    {
        #region Metodos

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string Alta(String jsonObject)
        {
            Cls_Mensaje Mensaje = new Cls_Mensaje();
            Cls_Cat_Participantes_Negocio Obj_Participante = new Cls_Cat_Participantes_Negocio();
            List<Cls_Cat_Participantes_Adjuntos_Negocio> List_Documentos = new List<Cls_Cat_Participantes_Adjuntos_Negocio>();
            string jsonResultado = "";
            String Directorio_Guardar = "";
            String Directorio_Temporales = "";
            String Color = "#8A2BE2";
            String Icono = "fa fa-close";


            try
            {
                Mensaje.Titulo = "Alta de vehiculo";

                Obj_Participante = JsonConvert.DeserializeObject<Cls_Cat_Participantes_Negocio>(jsonObject);
                //List_Documentos = JsonConvert.DeserializeObject<List<Cls_Cat_Participantes_Adjuntos_Negocio>>(Obj_Participante.tbl_documentos);

                using (var dbContext = new Sistema_TrazabilidadEntities())
                {
                    //var Participante_Registrado = (from par in dbContext.Cat_Participantes
                    //                           //where (par.Nombre.ToString().Trim() == Obj_Participante.Nombre.ToString().Trim()
                    //                           // || par.Email.ToString().Trim() == Obj_Participante.Email.ToString().Trim())
                    //                           select par
                    //                         );

                    //if (Participante_Registrado.Any())
                    //{
                    //    Mensaje.Mensaje = "<i class='" + Icono + "'style = 'color:" + Color + ";' ></i> &nbsp; El participante ya se encuentra registrada" + " <br />";
                    //    Mensaje.Estatus = "error";
                    //}
                    //else
                    //{

                    Cat_Participantes Participante = new Cat_Participantes();

                    Participante.Clave = Obj_Participante.Clave;
                    Participante.Nombre = Obj_Participante.Nombre;
                    Participante.Email = Obj_Participante.Email;
                    Participante.Telefono = Obj_Participante.Telefono;
                    Participante.Celular = Obj_Participante.Celular;
                    Participante.Fecha_Nacimiento = Obj_Participante.Fecha_Nacimiento;
                    Participante.Sexo = Obj_Participante.Sexo;
                    Participante.Notas = Obj_Participante.Notas;
                    Participante.Direccion = Obj_Participante.Direccion;
                    Participante.Colonia = Obj_Participante.Colonia;
                    Participante.Nacionalidad = Obj_Participante.Nacionalidad;
                    Participante.Estatus = Obj_Participante.Estatus;
                    Participante.Usuario_Creo = Cls_Sesiones.Usuario;
                    Participante.Fecha_Creo = DateTime.Now;

                    Participante = dbContext.Cat_Participantes.Add(Participante);

                    dbContext.SaveChanges();

                    //  se crea la carpeta para los documentos
                    Directorio_Guardar = HttpContext.Current.Server.MapPath("~") + "/Participantes/" + Participante.Participante_ID;
                    Directorio_Temporales = HttpContext.Current.Server.MapPath("~") + "/Participantes/Temporales";

                    if (!System.IO.Directory.Exists(Directorio_Guardar))
                    {
                        System.IO.Directory.CreateDirectory(Directorio_Guardar);
                    }

                    //  documentos del vehiculo
                    foreach (var Detalles in List_Documentos)
                    {
                        Cat_Participantes_Adjuntos Documentos = new Cat_Participantes_Adjuntos();

                        Documentos.Participante_ID = Detalles.Participante_ID;
                        Documentos.Nombre = Detalles.Nombre;
                        Documentos.Nombre_Documento = Detalles.Nombre_Documento;
                        Documentos.Ruta = "/Participantes/" + Participante.Participante_ID + "/" + Detalles.Nombre;
                        Documentos.Estatus = Detalles.Estatus;
                        Documentos.Usuario_Creo = Cls_Sesiones.Usuario;
                        Documentos.Fecha_Creo = DateTime.Now;

                        Documentos = dbContext.Cat_Participantes_Adjuntos.Add(Documentos);

                        //  se cambia la ubicacion del archivo
                        if (!System.IO.File.Exists(Directorio_Guardar + "/" + Detalles.Nombre))
                        {
                            System.IO.File.Copy(Directorio_Temporales + "/" + Detalles.Nombre, Directorio_Guardar + "/" + Detalles.Nombre);
                        }

                        System.IO.File.Delete(Directorio_Temporales + "/" + Detalles.Nombre);
                    }

                    dbContext.SaveChanges();

                    Mensaje.Mensaje = "La operación se realizo correctamente.";
                    Mensaje.Estatus = "success";
                    //}
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
            Cls_Cat_Participantes_Negocio Obj_Participante = new Cls_Cat_Participantes_Negocio();
            List<Cls_Cat_Participantes_Adjuntos_Negocio> List_Documentos = new List<Cls_Cat_Participantes_Adjuntos_Negocio>();
            List<Cls_Cat_Participantes_Adjuntos_Negocio> List_Documentos_Eliminados = new List<Cls_Cat_Participantes_Adjuntos_Negocio>();
            string jsonResultado = "";
            String Directorio_Guardar = "";
            String Directorio_Temporales = "";

            try
            {
                Mensaje.Titulo = "Alta de vehiculo";

                Obj_Participante = JsonConvert.DeserializeObject<Cls_Cat_Participantes_Negocio>(jsonObject);
                //List_Documentos = JsonConvert.DeserializeObject<List<Cls_Cat_Participantes_Adjuntos_Negocio>>(Obj_Participante.tbl_documentos);
                //List_Documentos_Eliminados = JsonConvert.DeserializeObject<List<Cls_Cat_Participantes_Adjuntos_Negocio>>(Obj_Participante.tbl_documentos_eliminados);

                using (var dbContext = new Sistema_TrazabilidadEntities())
                {
                    Cat_Participantes Participante = new Cat_Participantes();
                    Participante = dbContext.Cat_Participantes.Where(w => w.Participante_ID == Obj_Participante.Participante_ID).FirstOrDefault();

                    Participante.Clave = Obj_Participante.Clave;
                    Participante.Nombre = Obj_Participante.Nombre;
                    Participante.Email = Obj_Participante.Email;
                    Participante.Telefono = Obj_Participante.Telefono;
                    Participante.Celular = Obj_Participante.Celular;
                    Participante.Fecha_Nacimiento = Obj_Participante.Fecha_Nacimiento;
                    Participante.Sexo = Obj_Participante.Sexo;
                    Participante.Notas = Obj_Participante.Notas;
                    Participante.Direccion = Obj_Participante.Direccion;
                    Participante.Colonia = Obj_Participante.Colonia;
                    Participante.Nacionalidad = Obj_Participante.Nacionalidad;
                    Participante.Estatus = Obj_Participante.Estatus;
                    Participante.Usuario_Modifico = Cls_Sesiones.Usuario;
                    Participante.Fecha_Modifico = DateTime.Now;

                    dbContext.SaveChanges();

                    //  documentos del participante
                    foreach (var Detalles in List_Documentos)
                    {
                        //  se crea la carpeta para los documentos
                        Directorio_Guardar = HttpContext.Current.Server.MapPath("~") + "/Participantes/" + Participante.Participante_ID;
                        Directorio_Temporales = HttpContext.Current.Server.MapPath("~") + "/Participantes/Temporales";

                        if (!System.IO.Directory.Exists(Directorio_Guardar))
                        {
                            System.IO.Directory.CreateDirectory(Directorio_Guardar);
                        }

                        Cat_Participantes_Adjuntos Documentos = new Cat_Participantes_Adjuntos();

                        if (Detalles.Adjunto_ID == 0)
                        {
                            Documentos.Participante_ID = Participante.Participante_ID;
                            Documentos.Nombre = Detalles.Nombre;
                            Documentos.Nombre_Documento = Detalles.Nombre_Documento;
                            Documentos.Ruta = "/Participantes/" + Participante.Participante_ID + "/" + Detalles.Nombre;
                            Documentos.Estatus = "ACTIVO";
                            Documentos.Usuario_Creo = Cls_Sesiones.Usuario;
                            Documentos.Fecha_Creo = DateTime.Now;
                            Documentos = dbContext.Cat_Participantes_Adjuntos.Add(Documentos);

                            //  se cambia la ubicacion del archivo
                            if (!System.IO.File.Exists(Directorio_Guardar + "/" + Detalles.Nombre))
                            {
                                System.IO.File.Copy(Directorio_Temporales + "/" + Detalles.Nombre, Directorio_Guardar + "/" + Detalles.Nombre);
                            }

                            System.IO.File.Delete(Directorio_Temporales + "/" + Detalles.Nombre);
                        }
                        
                        dbContext.SaveChanges();

                    }

                    //  documentos del vehiculo
                    foreach (var Detalles in List_Documentos_Eliminados)
                    {
                        Cat_Participantes_Adjuntos Documentos = new Cat_Participantes_Adjuntos();
                        Documentos = dbContext.Cat_Participantes_Adjuntos.Where(w => w.Adjunto_ID == Detalles.Adjunto_ID).FirstOrDefault();
                        Documentos.Estatus = "INACTIVO";
                        Documentos.Usuario_Modifico = Cls_Sesiones.Usuario;
                        Documentos.Fecha_Modifico = DateTime.Now;

                        dbContext.SaveChanges();
                    }

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
            Cls_Cat_Participantes_Negocio Obj_Participante = new Cls_Cat_Participantes_Negocio();
            string jsonResultado = "";
            try
            {
                Mensaje.Titulo = "Cancelacion de participante";

                Obj_Participante = JsonConvert.DeserializeObject<Cls_Cat_Participantes_Negocio>(jsonObject);

                using (var dbContext = new Sistema_TrazabilidadEntities())
                {
                    Cat_Participantes Vehiculo = new Cat_Participantes();
                    Vehiculo = dbContext.Cat_Participantes.Where(w => w.Participante_ID == Obj_Participante.Participante_ID).FirstOrDefault();
                    
                    Vehiculo.Estatus = "INACTIVO";
                    Vehiculo.Usuario_Modifico = Cls_Sesiones.Usuario;
                    Vehiculo.Fecha_Modifico = DateTime.Now;

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
        public string Bloquear(String jsonObject)
        {
            Cls_Mensaje Mensaje = new Cls_Mensaje();
            Cls_Cat_Participantes_Negocio Obj_Participante = new Cls_Cat_Participantes_Negocio();
            string jsonResultado = "";
            try
            {
                Mensaje.Titulo = "Bloquear participante";

                Obj_Participante = JsonConvert.DeserializeObject<Cls_Cat_Participantes_Negocio>(jsonObject);

                using (var dbContext = new Sistema_TrazabilidadEntities())
                {
                    Cat_Participantes Vehiculo = new Cat_Participantes();
                    Vehiculo = dbContext.Cat_Participantes.Where(w => w.Participante_ID == Obj_Participante.Participante_ID).FirstOrDefault();

                    Vehiculo.Estatus = "BLOQUEADO";
                    Vehiculo.Notas = Obj_Participante.Notas;
                    Vehiculo.Usuario_Modifico = Cls_Sesiones.Usuario;
                    Vehiculo.Fecha_Modifico = DateTime.Now;

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
        public string Consultar_Participantes_Filtro(string jsonObject)
        {
            string Json_Resultado = string.Empty;
            Cls_Cat_Participantes_Negocio Obj = new Cls_Cat_Participantes_Negocio();

            try
            {

                Obj = JsonConvert.DeserializeObject<Cls_Cat_Participantes_Negocio>(jsonObject);

                using (var dbContext = new Sistema_TrazabilidadEntities())
                {

                    var _participantes = (from _partic in dbContext.Cat_Participantes
                                          
                                          select new Cls_Cat_Participantes_Negocio
                                          {
                                              Participante_ID = _partic.Participante_ID,
                                              Clave = _partic.Clave,
                                              Nombre = _partic.Nombre,
                                              Email = _partic.Email,
                                              Telefono = _partic.Telefono,
                                              Celular = _partic.Celular,
                                              Fecha_Nacimiento = _partic.Fecha_Nacimiento,
                                              Sexo = _partic.Sexo,
                                              Direccion = _partic.Direccion,
                                              Colonia = _partic.Colonia,
                                              Nacionalidad = _partic.Nacionalidad,
                                              Estatus = _partic.Estatus,
                                              Notas = _partic.Notas,
                                          })
                                          .OrderBy(x => x.Nombre).ToList();

                    //  filtro modelo
                    if (!String.IsNullOrEmpty(Obj.Nombre))
                    {
                        _participantes = _participantes.Where(x => x.Nombre.ToUpper().Trim().Contains(Obj.Nombre.Trim().ToUpper())).ToList();
                    }
                    
                    //  filtro estatus
                    if (!String.IsNullOrEmpty(Obj.Estatus))
                    {
                        _participantes = _participantes.Where(x => x.Estatus == (Obj.Estatus)).ToList();
                    }

                    //  filtro email
                    if (!String.IsNullOrEmpty(Obj.Email))
                    {
                        _participantes = _participantes.Where(x => x.Email == (Obj.Email)).ToList();
                    }

                    Json_Resultado = JsonMapper.ToJson(_participantes.ToList());
                }
            }
            catch (Exception e)
            {

            }

            return Json_Resultado;
        }


        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string Consultar_Documentos_Participantes(string jsonObject)
        {
            string Json_Resultado = string.Empty;
            Cls_Cat_Participantes_Adjuntos_Negocio Obj = new Cls_Cat_Participantes_Adjuntos_Negocio();

            try
            {

                Obj = JsonConvert.DeserializeObject<Cls_Cat_Participantes_Adjuntos_Negocio>(jsonObject);

                using (var dbContext = new Sistema_TrazabilidadEntities())
                {

                    var _documentos = (from _doc in dbContext.Cat_Participantes_Adjuntos

                                       where _doc.Participante_ID == Obj.Participante_ID

                                       select new Cls_Cat_Participantes_Adjuntos_Negocio
                                       {
                                           Adjunto_ID = _doc.Adjunto_ID,
                                           Participante_ID = _doc.Participante_ID,
                                           Estatus = _doc.Estatus,
                                           Nombre = _doc.Nombre,
                                           Nombre_Documento = _doc.Nombre_Documento,
                                           Ruta = _doc.Ruta,

                                       })
                                      .OrderBy(x => x.Nombre_Documento).ToList();
                    
                    Json_Resultado = JsonMapper.ToJson(_documentos.ToList());
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
