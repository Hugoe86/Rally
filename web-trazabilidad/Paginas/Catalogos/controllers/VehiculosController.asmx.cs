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
    /// Summary description for VehiculosController
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class VehiculosController : System.Web.Services.WebService
    {



        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string Alta(String jsonObject)
        {
            Cls_Mensaje Mensaje = new Cls_Mensaje();
            Cls_Vehiculos_Negocios Obj_Vehiculo = new Cls_Vehiculos_Negocios();
            List<Cls_Cat_Vehiculos_Documentos_Negocio> List_Documentos = new List<Cls_Cat_Vehiculos_Documentos_Negocio>();
            string jsonResultado = "";
            String Directorio_Guardar = "";
            String Directorio_Temporales = "";
            String Color = "#8A2BE2";
            String Icono = "fa fa-close";

            try
            {
                Mensaje.Titulo = "Alta de vehiculo";

                Obj_Vehiculo = JsonConvert.DeserializeObject<Cls_Vehiculos_Negocios>(jsonObject);
                //List_Documentos = JsonConvert.DeserializeObject<List<Cls_Cat_Vehiculos_Documentos_Negocio>>(Obj_Vehiculo.tbl_documentos);

                using (var dbContext = new Sistema_TrazabilidadEntities())
                {

                    //var Vehiculo_Registrado = (from veh in dbContext.Cat_Vehiculos
                    //                           where veh.NS == Obj_Vehiculo.NS
                    //                           && veh.Año.ToString().Trim() == Obj_Vehiculo.Año.ToString().Trim()
                    //                           && veh.Marca.ToString().Trim() == Obj_Vehiculo.Marca.ToString().Trim()
                    //                           && veh.Modelo.ToString().Trim() == Obj_Vehiculo.Modelo.ToString().Trim()
                    //                           //&& veh.Placas.ToString().Trim() == Obj_Vehiculo.Placas .ToString().Trim()
                    //                           select veh
                    //                          );


                    //if (Vehiculo_Registrado.Any())
                    //{
                    //    Mensaje.Mensaje = "<i class='" + Icono + "'style = 'color:" + Color + ";' ></i> &nbsp; El vehiculo ya se encuentra registrada" + " <br />";
                    //    Mensaje.Estatus = "error";
                    //}
                    //else
                    //{

                    Cat_Vehiculos Vehiculo = new Cat_Vehiculos();
                    Cat_Vehiculos Vehiculo_Nuevo = new Cat_Vehiculos();


                    Vehiculo.NS = Obj_Vehiculo.NS;
                    Vehiculo.Año = Convert.ToInt32(Obj_Vehiculo.Año);
                    Vehiculo.Marca = Obj_Vehiculo.Marca;
                    Vehiculo.Modelo = Obj_Vehiculo.Modelo;
                    Vehiculo.Placas = Obj_Vehiculo.Placas;
                    Vehiculo.Estatus = Obj_Vehiculo.Estatus;
                    Vehiculo.Notas = Obj_Vehiculo.Notas;
                    //Vehiculo.Tarjeta_Circulacion = Obj_Vehiculo.Tarjeta_Circulacion;
                    Vehiculo.Color_Hex_Rgb = Obj_Vehiculo.Color_Hex_Rgb;
                    Vehiculo.Color_Fondo_Hex_Rgb = Obj_Vehiculo.Color_Fondo_Hex_Rgb;
                    Vehiculo.Compañia = Obj_Vehiculo.Compañia;
                    Vehiculo.Numero_Poliza = Obj_Vehiculo.Numero_Poliza;
                    Vehiculo.Vigencia_Inicial = Obj_Vehiculo.Vigencia_Inicial;
                    Vehiculo.Vigencia_Final = Obj_Vehiculo.Vigencia_Final;
                    Vehiculo.Usuario_Creo = Cls_Sesiones.Usuario;
                    Vehiculo.Fecha_Creo = DateTime.Now;

                    Vehiculo_Nuevo = dbContext.Cat_Vehiculos.Add(Vehiculo);

                    dbContext.SaveChanges();

                    //  se crea la carpeta para los documentos
                    Directorio_Guardar = HttpContext.Current.Server.MapPath("~") + "/Vehiculos/" + Vehiculo.Vehiculo_Id;
                    Directorio_Temporales = HttpContext.Current.Server.MapPath("~") + "/Vehiculos/Temporales";

                    if (!System.IO.Directory.Exists(Directorio_Guardar))
                    {
                        System.IO.Directory.CreateDirectory(Directorio_Guardar);
                    }

                    //  documentos del vehiculo
                    foreach (var Detalles in List_Documentos)
                    {
                        Cat_Vehiculos_Documentos Documentos = new Cat_Vehiculos_Documentos();

                        Documentos.Vehiculo_Id = Detalles.Vehiculo_Id;
                        Documentos.Nombre = Detalles.Nombre;
                        Documentos.Nombre_Documento = Detalles.Nombre_Documento;
                        Documentos.Ruta = "/Vehiculos/" + Vehiculo.Vehiculo_Id + "/" + Detalles.Nombre;
                        Documentos.Estatus = Detalles.Estatus;
                        Documentos.Usuario_Creo = Cls_Sesiones.Usuario;
                        Documentos.Fecha_Creo = DateTime.Now;

                        Documentos = dbContext.Cat_Vehiculos_Documentos.Add(Documentos);


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
            Cls_Vehiculos_Negocios Obj_Vehiculo = new Cls_Vehiculos_Negocios();
            List<Cls_Cat_Vehiculos_Documentos_Negocio> List_Documentos = new List<Cls_Cat_Vehiculos_Documentos_Negocio>();
            List<Cls_Cat_Vehiculos_Documentos_Negocio> List_Documentos_Eliminados = new List<Cls_Cat_Vehiculos_Documentos_Negocio>();
            string jsonResultado = "";
            String Directorio_Guardar = "";
            String Directorio_Temporales = "";

            try
            {
                Mensaje.Titulo = "Alta de vehiculo";

                Obj_Vehiculo = JsonConvert.DeserializeObject<Cls_Vehiculos_Negocios>(jsonObject);
                //List_Documentos = JsonConvert.DeserializeObject<List<Cls_Cat_Vehiculos_Documentos_Negocio>>(Obj_Vehiculo.tbl_documentos);
                //List_Documentos_Eliminados = JsonConvert.DeserializeObject<List<Cls_Cat_Vehiculos_Documentos_Negocio>>(Obj_Vehiculo.tbl_documentos_eliminados);

                using (var dbContext = new Sistema_TrazabilidadEntities())
                {
                    Cat_Vehiculos Vehiculo = new Cat_Vehiculos();
                    Vehiculo = dbContext.Cat_Vehiculos.Where(w => w.Vehiculo_Id == Obj_Vehiculo.Vehiculo_Id).FirstOrDefault();

                    Vehiculo.NS = Obj_Vehiculo.NS;
                    Vehiculo.Año = Convert.ToInt32(Obj_Vehiculo.Año);
                    Vehiculo.Marca = Obj_Vehiculo.Marca;
                    Vehiculo.Modelo = Obj_Vehiculo.Modelo;
                    Vehiculo.Placas = Obj_Vehiculo.Placas;
                    Vehiculo.Notas = Obj_Vehiculo.Notas;

                    Vehiculo.Estatus = Obj_Vehiculo.Estatus;
                    //Vehiculo.Tarjeta_Circulacion = Obj_Vehiculo.Tarjeta_Circulacion;
                    Vehiculo.Color_Hex_Rgb = Obj_Vehiculo.Color_Hex_Rgb;
                    Vehiculo.Color_Fondo_Hex_Rgb = Obj_Vehiculo.Color_Fondo_Hex_Rgb;
                    Vehiculo.Compañia = Obj_Vehiculo.Compañia;
                    Vehiculo.Numero_Poliza = Obj_Vehiculo.Numero_Poliza;
                    Vehiculo.Vigencia_Inicial = Obj_Vehiculo.Vigencia_Inicial;
                    Vehiculo.Vigencia_Final = Obj_Vehiculo.Vigencia_Final;
                    Vehiculo.Usuario_Modifico = Cls_Sesiones.Usuario;
                    Vehiculo.Fecha_Modifico = DateTime.Now;

                    dbContext.SaveChanges();


                 
                    //  documentos del vehiculo
                    foreach (var Detalles in List_Documentos)
                    {

                        //  se crea la carpeta para los documentos
                        Directorio_Guardar = HttpContext.Current.Server.MapPath("~") + "/Vehiculos/" + Vehiculo.Vehiculo_Id;
                        Directorio_Temporales = HttpContext.Current.Server.MapPath("~") + "/Vehiculos/Temporales";

                        if (!System.IO.Directory.Exists(Directorio_Guardar))
                        {
                            System.IO.Directory.CreateDirectory(Directorio_Guardar);
                        }

                        Cat_Vehiculos_Documentos Documentos = new Cat_Vehiculos_Documentos();

                        if (Detalles.Documento_Id == 0)
                        {
                            Documentos.Vehiculo_Id = Vehiculo.Vehiculo_Id;
                            Documentos.Nombre = Detalles.Nombre;
                            Documentos.Nombre_Documento = Detalles.Nombre_Documento;
                            Documentos.Ruta = "/Vehiculos/" + Vehiculo.Vehiculo_Id + "/" + Detalles.Nombre;
                            Documentos.Estatus = "ACTIVO";
                            Documentos.Usuario_Creo = Cls_Sesiones.Usuario;
                            Documentos.Fecha_Creo = DateTime.Now;
                            Documentos = dbContext.Cat_Vehiculos_Documentos.Add(Documentos);

                            //  se cambia la ubicacion del archivo
                            if (!System.IO.File.Exists(Directorio_Guardar + "/" + Detalles.Nombre))
                            {
                                System.IO.File.Copy(Directorio_Temporales + "/" + Detalles.Nombre, Directorio_Guardar + "/" + Detalles.Nombre);
                            }

                            System.IO.File.Delete(Directorio_Temporales + "/" + Detalles.Nombre);


                            dbContext.SaveChanges();
                        }



                    }

                    //  documentos del vehiculo
                    foreach (var Detalles in List_Documentos_Eliminados)
                    {
                        Cat_Vehiculos_Documentos Documentos = new Cat_Vehiculos_Documentos();
                        Documentos = dbContext.Cat_Vehiculos_Documentos.Where(w => w.Documento_Id == Detalles.Documento_Id).FirstOrDefault();
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
            Cls_Vehiculos_Negocios Obj_Vehiculo = new Cls_Vehiculos_Negocios();
            string jsonResultado = "";
            try
            {
                Mensaje.Titulo = "Cancelacion de vehiculo";

                Obj_Vehiculo = JsonConvert.DeserializeObject<Cls_Vehiculos_Negocios>(jsonObject);

                using (var dbContext = new Sistema_TrazabilidadEntities())
                {
                    Cat_Vehiculos Vehiculo = new Cat_Vehiculos();
                    Vehiculo = dbContext.Cat_Vehiculos.Where(w => w.Vehiculo_Id == Obj_Vehiculo.Vehiculo_Id).FirstOrDefault();

                 
                    Vehiculo.Estatus ="INACTIVO";
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
        public string Consultar_Vehiculos_Filtro(string jsonObject)
        {
            string Json_Resultado = string.Empty;
            Cls_Vehiculos_Negocios Obj = new Cls_Vehiculos_Negocios();

            try
            {

                Obj = JsonConvert.DeserializeObject<Cls_Vehiculos_Negocios>(jsonObject);


                Obj.Vigencia_Inicial = Obj.Vigencia_Inicial == null ? DateTime.MinValue : Obj.Vigencia_Inicial.Value;
                Obj.Vigencia_Final = Obj.Vigencia_Final == null ? DateTime.MinValue : Obj.Vigencia_Final.Value;


                using (var dbContext = new Sistema_TrazabilidadEntities())
                {

                    var _vehiculos = (from _veh in dbContext.Cat_Vehiculos
                                      
                                      select new Cls_Vehiculos_Negocios
                                      {
                                          Vehiculo_Id = _veh.Vehiculo_Id,
                                          NS = _veh.NS,
                                          Año = _veh.Año ?? 0,
                                          Marca = _veh.Marca,
                                          Modelo = _veh.Modelo,
                                          Placas = _veh.Placas,
                                          Color_Hex_Rgb = _veh.Color_Hex_Rgb,
                                          Color_Fondo_Hex_Rgb = _veh.Color_Fondo_Hex_Rgb,
                                          Compañia = _veh.Compañia,
                                          Numero_Poliza = _veh.Numero_Poliza,
                                          Vigencia_Inicial = _veh.Vigencia_Inicial,
                                          Vigencia_Final = _veh.Vigencia_Final,
                                          Estatus= _veh.Estatus,
                                          Notas= _veh.Notas,
                                      })
                                      .OrderBy(x => x.Modelo).ToList();


                    //  filtro modelo
                    if (Obj.Vehiculo_Id != 0)
                    {
                        _vehiculos = _vehiculos.Where(x => x.Vehiculo_Id == (Obj.Vehiculo_Id)).ToList();
                    }

                    //  filtro modelo
                    if (!String.IsNullOrEmpty(Obj.Modelo))
                    {
                        _vehiculos = _vehiculos.Where(x => x.Modelo.ToUpper().Contains(Obj.Modelo.ToUpper())).ToList();
                    }

                    //  filtro año
                    if (Obj.Año != 0)
                    {
                        _vehiculos = _vehiculos.Where(x => x.Año == Obj.Año).ToList();
                    }
                    
                    //  filtro estatus
                    if (!String.IsNullOrEmpty(Obj.Estatus))
                    {
                        _vehiculos = _vehiculos.Where(x => x.Estatus == (Obj.Estatus)).ToList();
                    }

                    //  filtro NS
                    if (!String.IsNullOrEmpty(Obj.NS))
                    {
                        _vehiculos = _vehiculos.Where(x => x.NS.ToUpper().Contains(Obj.NS.ToUpper())).ToList();
                    }

                    //  filtro Compañia
                    if (!String.IsNullOrEmpty(Obj.Compañia))
                    {
                        _vehiculos = _vehiculos.Where(x => x.Compañia.ToUpper().Contains(Obj.Compañia.ToUpper())).ToList();
                    }

                   
                    //  filtro Placas
                    if (!String.IsNullOrEmpty(Obj.Placas))
                    {
                        _vehiculos = _vehiculos.Where(x => x.Placas.Contains(Obj.Placas)).ToList();
                    }

                    //  filtro Marca
                    if (!String.IsNullOrEmpty(Obj.Marca))
                    {
                        _vehiculos = _vehiculos.Where(x => x.Marca.ToUpper().Contains(Obj.Marca.ToUpper())).ToList();
                    }

                    //  filtro Vigencia_Inicial
                    if (Obj.Vigencia_Inicial != DateTime.MinValue)
                    {
                        _vehiculos = _vehiculos.Where(x => (x.Vigencia_Inicial >= Obj.Vigencia_Inicial)).ToList();

                    }

                    //  filtro Vigencia_Final
                    if (Obj.Vigencia_Final != DateTime.MinValue)
                    {
                        _vehiculos = _vehiculos.Where(x => (x.Vigencia_Final <= Obj.Vigencia_Final)).ToList();

                    }




                    Json_Resultado = JsonMapper.ToJson(_vehiculos.ToList());
                }
            }
            catch (Exception e)
            {

            }

            return Json_Resultado;
        }


        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string Consultar_Documentos_Vehiculos(string jsonObject)
        {
            string Json_Resultado = string.Empty;
            Cls_Cat_Vehiculos_Documentos_Negocio Obj = new Cls_Cat_Vehiculos_Documentos_Negocio();

            try
            {

                Obj = JsonConvert.DeserializeObject<Cls_Cat_Vehiculos_Documentos_Negocio>(jsonObject);

                using (var dbContext = new Sistema_TrazabilidadEntities())
                {

                    var _documentos = (from _doc in dbContext.Cat_Vehiculos_Documentos

                                      where _doc.Vehiculo_Id == Obj.Vehiculo_Id
                                      && _doc.Estatus == "ACTIVO"

                                      select new Cls_Cat_Vehiculos_Documentos_Negocio
                                      {
                                          Documento_Id= _doc.Documento_Id,
                                          Vehiculo_Id = _doc.Vehiculo_Id,
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
    }
}
