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

namespace web_trazabilidad.Paginas.Catalogos.controllers
{
    /// <summary>
    /// Summary description for ResponsablesController
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class ResponsablesController : System.Web.Services.WebService
    {




        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string Alta(String jsonObject)
        {
            Cls_Mensaje Mensaje = new Cls_Mensaje();
            Cls_Cat_Responsables_Negocio Obj_Responsable = new Cls_Cat_Responsables_Negocio();
            string jsonResultado = "";
            String Color = "#8A2BE2";
            String Icono = "fa fa-close";
            try
            {
                Mensaje.Titulo = "Alta";

                Obj_Responsable = JsonConvert.DeserializeObject<Cls_Cat_Responsables_Negocio>(jsonObject);

                using (var dbContext = new Sistema_TrazabilidadEntities())
                {
                    
                    //var Resposable_Clave = (from _resp in dbContext.Cat_Responsables
                    //                        where _resp.Clave == Obj_Responsable.Clave
                    //                        select new Cls_Cat_Responsables_Negocio
                    //                        {
                    //                            Clave = _resp.Clave
                    //                        });


                    //if (Resposable_Clave.Any())
                    //{
                    //    Mensaje.Mensaje = "<i class='" + Icono + "'style = 'color:" + Color + " ;' ></i> &nbsp; La clave ya se encuentra registrada" + " <br />";
                    //    Mensaje.Estatus = "error";
                    //}
                    //else
                    //{
                    //    var Responsable_Nombre = (from _resp in dbContext.Cat_Responsables
                    //                              where _resp.Nombre == Obj_Responsable.Nombre
                    //                              select new Cls_Cat_Responsables_Negocio
                    //                              {
                    //                                  Nombre = _resp.Nombre
                    //                              });

                    //    if (Responsable_Nombre.Any())
                    //    {
                    //        Mensaje.Mensaje = "<i class='" + Icono + "'style = 'color:" + Color + ";' ></i> &nbsp; El nombre ya se encuentra registrada" + " <br />";
                    //        Mensaje.Estatus = "error";
                    //    }
                    //    else
                    //    {
                    var Responsable_Email = (from _resp in dbContext.Cat_Responsables
                                             where _resp.Email == Obj_Responsable.Email
                                             select new Cls_Cat_Responsables_Negocio
                                             {
                                                 Email = _resp.Email
                                             });
                    if (Responsable_Email.Any())
                    {
                        Mensaje.Titulo = "Alta (validación)";
                        Mensaje.Mensaje = "<i class='" + Icono + "'style = 'color:" + Color + ";' ></i> &nbsp; El email [" + Obj_Responsable.Email + "] ya se encuentra registrada" + " <br />";
                        Mensaje.Estatus = "error";
                    }
                    else
                    {
                        Cat_Responsables Responsable = new Cat_Responsables();
                        Cat_Responsables Responsable_Nuevo = new Cat_Responsables();

                        Responsable.Clave = Obj_Responsable.Clave;
                        Responsable.Nombre = Obj_Responsable.Nombre;
                        Responsable.Email = Obj_Responsable.Email;
                        Responsable.Password = Cls_Seguridad.Encriptar(Obj_Responsable.Password);
                        Responsable.Estatus = Obj_Responsable.Estatus;
                        Responsable.Direccion = Obj_Responsable.Direccion;
                        Responsable.Colonia = Obj_Responsable.Colonia;
                        Responsable.CP = Obj_Responsable.CP;
                        Responsable.Ciudad = Obj_Responsable.Ciudad;
                        Responsable.Estado = Obj_Responsable.Estado;
                        Responsable.Telefono = Obj_Responsable.Telefono;
                        Responsable.Celular = Obj_Responsable.Celular;
                        Responsable.Usuario_Creo = Cls_Sesiones.Usuario;
                        Responsable.Fecha_Creo = DateTime.Now;

                        Responsable_Nuevo = dbContext.Cat_Responsables.Add(Responsable);
                        dbContext.SaveChanges();


                        Mensaje.Mensaje = "La operación se realizo correctamente.";
                        Mensaje.Estatus = "success";
                    }
                    //    }
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
            Cls_Cat_Responsables_Negocio Obj_Responsable = new Cls_Cat_Responsables_Negocio();
            string jsonResultado = "";

            try
            {
                Mensaje.Titulo = "Alta de vehiculo";

                Obj_Responsable = JsonConvert.DeserializeObject<Cls_Cat_Responsables_Negocio>(jsonObject);
           
                using (var dbContext = new Sistema_TrazabilidadEntities())
                {
                    Cat_Responsables Responsable = new Cat_Responsables();
                    Responsable = dbContext.Cat_Responsables.Where(w => w.Responsable_Id == Obj_Responsable.Responsable_Id).FirstOrDefault();


                    Responsable.Clave = Obj_Responsable.Clave;
                    Responsable.Nombre = Obj_Responsable.Nombre;
                    Responsable.Email = Obj_Responsable.Email;
                    Responsable.Password = Cls_Seguridad.Encriptar(Obj_Responsable.Password);
                    Responsable.Estatus = Obj_Responsable.Estatus;
                    Responsable.Direccion = Obj_Responsable.Direccion;
                    Responsable.Colonia = Obj_Responsable.Colonia;
                    Responsable.CP = Obj_Responsable.CP;
                    Responsable.Ciudad = Obj_Responsable.Ciudad;
                    Responsable.Estado = Obj_Responsable.Estado;
                    Responsable.Telefono = Obj_Responsable.Telefono;
                    Responsable.Celular = Obj_Responsable.Celular;
                    Responsable.Usuario_Modifico = Cls_Sesiones.Usuario;
                    Responsable.Fecha_Modifico = DateTime.Now;

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
            Cls_Cat_Responsables_Negocio Obj_Responsable = new Cls_Cat_Responsables_Negocio();
            string jsonResultado = "";

            try
            {
                Mensaje.Titulo = "Eliminar";

                Obj_Responsable = JsonConvert.DeserializeObject<Cls_Cat_Responsables_Negocio>(jsonObject);

                using (var dbContext = new Sistema_TrazabilidadEntities())
                {
                    Cat_Responsables Responsable = new Cat_Responsables();
                    Responsable = dbContext.Cat_Responsables.Where(w => w.Responsable_Id == Obj_Responsable.Responsable_Id).FirstOrDefault();
                    
                    Responsable.Estatus = "INACTIVO";
                    Responsable.Usuario_Modifico = Cls_Sesiones.Usuario;
                    Responsable.Fecha_Modifico = DateTime.Now;

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
        public string Consultar_Responsables_Filtro(string jsonObject)
        {
            string Json_Resultado = string.Empty;
            Cls_Cat_Responsables_Negocio Obj = new Cls_Cat_Responsables_Negocio();

            try
            {

                Obj = JsonConvert.DeserializeObject<Cls_Cat_Responsables_Negocio>(jsonObject);
                

                using (var dbContext = new Sistema_TrazabilidadEntities())
                {

                    var _responsables = (from _res in dbContext.Cat_Responsables

                                      select new Cls_Cat_Responsables_Negocio
                                      {
                                          Responsable_Id = _res.Responsable_Id,
                                          Clave = _res.Clave,
                                          Nombre = _res.Nombre,
                                          Email = _res.Email,
                                          Password = (_res.Password),
                                          Estatus = _res.Estatus,
                                          Telefono = _res.Telefono,
                                          Celular = _res.Celular,
                                          Direccion = _res.Direccion,
                                          Colonia = _res.Colonia,
                                          CP = _res.CP,
                                          Ciudad = _res.Ciudad,
                                          Estado = _res.Estado,

                                      })
                                      .OrderBy(x => x.Clave).ToList();

                    //  filtro Clave
                    if (!String.IsNullOrEmpty(Obj.Clave))
                    {
                        _responsables = _responsables.Where(x => x.Clave.Contains(Obj.Clave)).ToList();
                    }

                    //  filtro Clave
                    if (!String.IsNullOrEmpty(Obj.Nombre))
                    {
                        _responsables = _responsables.Where(x => x.Nombre.Contains(Obj.Nombre)).ToList();
                    }
                    //  filtro Estatus
                    if (!String.IsNullOrEmpty(Obj.Estatus))
                    {
                        _responsables = _responsables.Where(x => x.Estatus == (Obj.Estatus)).ToList();
                    }
                    //  filtro email
                    if (!String.IsNullOrEmpty(Obj.Email))
                    {
                        _responsables = _responsables.Where(x => x.Email.Contains(Obj.Email)).ToList();
                    }
                    Json_Resultado = JsonMapper.ToJson(_responsables.ToList());
                }
            }
            catch (Exception e)
            {

            }

            return Json_Resultado;
        }


        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string Consultar_Password(string jsonObject)
        {
            string Json_Resultado = string.Empty;
            Cls_Cat_Responsables_Negocio Obj = new Cls_Cat_Responsables_Negocio();

            try
            {

                Obj = JsonConvert.DeserializeObject<Cls_Cat_Responsables_Negocio>(jsonObject);


                using (var dbContext = new Sistema_TrazabilidadEntities())
                {

                    var _responsables = (from _res in dbContext.Cat_Responsables

                                         where _res.Responsable_Id == Obj.Responsable_Id

                                         select new Cls_Cat_Responsables_Negocio
                                         {
                                             Nombre = _res.Nombre,
                                             Password = _res.Password,
                                         })
                                      .OrderBy(x => x.Nombre).ToList();


                    if (_responsables.Any())
                    {
                        foreach (var detalle in _responsables.ToList())
                        {
                            detalle.Password = Cls_Seguridad.Desencriptar(detalle.Password);
                        }
                    }
                   
                   
                    Json_Resultado = JsonMapper.ToJson(_responsables.ToList());
                }
            }
            catch (Exception e)
            {

            }

            return Json_Resultado;
        }


        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void Consultar_Responsables_Combo()
        {
            string Json_Resultado = string.Empty;
            List<Cls_Select2> Lst_Combo = new List<Cls_Select2>();

            try
            {
                string q = string.Empty;
                string tipo = string.Empty;

                NameValueCollection nvc = Context.Request.Form;

                if (!String.IsNullOrEmpty(nvc["q"]))
                    q = nvc["q"].ToString().Trim();
                if (!String.IsNullOrEmpty(nvc["tipo"]))
                    tipo = nvc["tipo"].ToString().Trim();

                using (var dbContext = new Sistema_TrazabilidadEntities())
                {

                    var _responsables = (from _res in dbContext.Cat_Responsables
                                         where _res.Estatus == "ACTIVO"
                                         && (_res.Nombre.Contains(q))


                                         select new Cls_Select2
                                         {

                                             id = _res.Responsable_Id.ToString(),
                                             text = _res.Nombre,

                                         });

                    
                    Json_Resultado = JsonMapper.ToJson(_responsables.ToList());
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

    }
}
