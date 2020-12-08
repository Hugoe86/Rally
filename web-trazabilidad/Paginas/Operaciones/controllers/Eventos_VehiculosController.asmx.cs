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
    /// Summary description for Eventos_VehiculosController
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class Eventos_VehiculosController : System.Web.Services.WebService
    {

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string Alta(String jsonObject)
        {
            Cls_Mensaje Mensaje = new Cls_Mensaje();
            Cls_Ope_Eventos_Vehiculo_Participante_Negocio Obj_Vehiculo = new Cls_Ope_Eventos_Vehiculo_Participante_Negocio();
            Cls_Ope_Eventos_Vehiculo_Datos_Piloto Obj_Piloto = new Cls_Ope_Eventos_Vehiculo_Datos_Piloto();
            Cls_Ope_Eventos_Vehiculo_Datos_Copiloto Obj_Copiloto = new Cls_Ope_Eventos_Vehiculo_Datos_Copiloto();


            string jsonResultado = "";
            String Directorio_Guardar = "";
            String Directorio_Temporales = "";
            String Color = "#8A2BE2";
            String Icono = "fa fa-close";

            try
            {
                Mensaje.Titulo = "Alta";

                Obj_Vehiculo = JsonConvert.DeserializeObject<Cls_Ope_Eventos_Vehiculo_Participante_Negocio>(jsonObject);
                Obj_Piloto = JsonConvert.DeserializeObject<Cls_Ope_Eventos_Vehiculo_Datos_Piloto>(jsonObject);
                Obj_Copiloto = JsonConvert.DeserializeObject<Cls_Ope_Eventos_Vehiculo_Datos_Copiloto>(jsonObject);


                using (var dbContext = new Sistema_TrazabilidadEntities())
                {
                    Ope_Eventos_Vehiculo_Participante Vehiculo = new Ope_Eventos_Vehiculo_Participante();

                    var Vehiculo_Registrado = (from veh in dbContext.Ope_Eventos_Vehiculo_Participante
                                               where veh.Vehiculo_Id == Obj_Vehiculo.Vehiculo_Id
                                                && veh.Evento_Id == Obj_Vehiculo.Evento_Id
                                               select veh
                                               );


                    if (Vehiculo_Registrado.Any())
                    {
                        Mensaje.Mensaje = "<i class='" + Icono + "'style = 'color:" + Color + ";' ></i> &nbsp; El vehiculo ya se encuentra registrada" + " <br />";
                        Mensaje.Estatus = "error";
                    }
                    else
                    {

                        //  consulta para el piloto
                        var Piloto_Registrado = (from veh in dbContext.Ope_Eventos_Vehiculo_Participante
                                                 where veh.Participante_Piloto_Id == Obj_Piloto.Participante_Piloto_Id
                                                  && veh.Evento_Id == Obj_Vehiculo.Evento_Id
                                                 select veh
                                             );


                        if (Piloto_Registrado.Any())
                        {
                            Mensaje.Mensaje = "<i class='" + Icono + "'style = 'color:" + Color + ";' ></i> &nbsp; El piloto ya se encuentra registrada" + " <br />";
                            Mensaje.Estatus = "error";
                        }
                        else
                        {


                            //  consulta para el copiloto
                            var Copiloto_Registrado = (from veh in dbContext.Ope_Eventos_Vehiculo_Participante
                                                       where veh.Participante_Copiloto_Id == Obj_Copiloto.Participante_Copiloto_Id
                                                        && veh.Evento_Id == Obj_Vehiculo.Evento_Id
                                                       select veh
                                                 );
                            if (Copiloto_Registrado.Any())
                            {
                                Mensaje.Mensaje = "<i class='" + Icono + "'style = 'color:" + Color + ";' ></i> &nbsp; El copiloto ya se encuentra registrada" + " <br />";
                                Mensaje.Estatus = "error";
                            }
                            else
                            {
                                //  vehiculo
                                Vehiculo.Vehiculo_Id = Convert.ToInt32(Obj_Vehiculo.Vehiculo_Id);
                                Vehiculo.Evento_Id = Convert.ToInt32(Obj_Vehiculo.Evento_Id);
                                Vehiculo.Categoria_Id = Convert.ToInt32(Obj_Vehiculo.Categoria_Id);
                                Vehiculo.Categoria_Participante_Id = Convert.ToInt32(Obj_Vehiculo.Categoria_Participante_Id);
                                Vehiculo.Numero_Participante = Obj_Vehiculo.Numero_Participante;
                                Vehiculo.Numero_Registro = Obj_Vehiculo.Numero_Registro;
                                Vehiculo.Estatus = "ACTIVO";
                                Vehiculo.Revision_Mecanica = Obj_Vehiculo.Revision_Mecanica;
                                Vehiculo.Comentario = Obj_Vehiculo.Comentario;

                                //  piloto
                                Vehiculo.Participante_Piloto_Id = Convert.ToInt32(Obj_Piloto.Participante_Piloto_Id);
                                Vehiculo.Revision_Medica_Piloto = Obj_Piloto.Revision_Medica_Piloto;
                                Vehiculo.Comentario_Piloto = Obj_Piloto.Comentario_Piloto;

                                //  copiloto
                                if (Obj_Copiloto.Participante_Copiloto_Id > 0)
                                {
                                    Vehiculo.Participante_Copiloto_Id = Convert.ToInt32(Obj_Copiloto.Participante_Copiloto_Id);
                                }
                                else
                                {
                                    Vehiculo.Participante_Copiloto_Id = Convert.ToInt32(Obj_Piloto.Participante_Piloto_Id);
                                }

                                Vehiculo.Revision_Medica_Copiloto = Obj_Copiloto.Revision_Medica_Copiloto;
                                Vehiculo.Comentario_Copiloto = Obj_Copiloto.Comentario_Copiloto;

                                Vehiculo.Usuario_Creo = Cls_Sesiones.Usuario;
                                Vehiculo.Fecha_Creo = DateTime.Now;

                                dbContext.Ope_Eventos_Vehiculo_Participante.Add(Vehiculo);

                                dbContext.SaveChanges();


                                Mensaje.Mensaje = "La operación se realizo correctamente.";
                                Mensaje.Estatus = "success";
                            }
                        }

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

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string Modificar(String jsonObject)
        {
            Cls_Mensaje Mensaje = new Cls_Mensaje();
            Cls_Ope_Eventos_Vehiculo_Participante_Negocio Obj_Vehiculo = new Cls_Ope_Eventos_Vehiculo_Participante_Negocio();
            Cls_Ope_Eventos_Vehiculo_Datos_Piloto Obj_Piloto = new Cls_Ope_Eventos_Vehiculo_Datos_Piloto();
            Cls_Ope_Eventos_Vehiculo_Datos_Copiloto Obj_Copiloto = new Cls_Ope_Eventos_Vehiculo_Datos_Copiloto();
            
            string jsonResultado = "";


            try
            {
                Mensaje.Titulo = "Modificar";

                Obj_Vehiculo = JsonConvert.DeserializeObject<Cls_Ope_Eventos_Vehiculo_Participante_Negocio>(jsonObject);
                Obj_Piloto = JsonConvert.DeserializeObject<Cls_Ope_Eventos_Vehiculo_Datos_Piloto>(jsonObject);
                Obj_Copiloto = JsonConvert.DeserializeObject<Cls_Ope_Eventos_Vehiculo_Datos_Copiloto>(jsonObject);


                using (var dbContext = new Sistema_TrazabilidadEntities())
                {
                    Ope_Eventos_Vehiculo_Participante Vehiculo = new Ope_Eventos_Vehiculo_Participante();
                    Vehiculo = dbContext.Ope_Eventos_Vehiculo_Participante.Where(w => w.Vehiculo_Participante_Id == Obj_Vehiculo.Vehiculo_Participante_Id).FirstOrDefault(); 


                    //  vehiculo
                    Vehiculo.Vehiculo_Id = Convert.ToInt32(Obj_Vehiculo.Vehiculo_Id);
                    Vehiculo.Categoria_Id = Convert.ToInt32(Obj_Vehiculo.Categoria_Id);
                    Vehiculo.Categoria_Participante_Id = Convert.ToInt32(Obj_Vehiculo.Categoria_Participante_Id);
                    Vehiculo.Numero_Participante = Obj_Vehiculo.Numero_Participante;
                    Vehiculo.Numero_Registro = Obj_Vehiculo.Numero_Registro;
                    Vehiculo.Estatus = "ACTIVO";
                    Vehiculo.Revision_Mecanica = Obj_Vehiculo.Revision_Mecanica;
                    Vehiculo.Comentario = Obj_Vehiculo.Comentario;

                    //  piloto
                    Vehiculo.Participante_Piloto_Id = Convert.ToInt32(Obj_Piloto.Participante_Piloto_Id);
                    Vehiculo.Revision_Medica_Piloto = Obj_Piloto.Revision_Medica_Piloto;
                    Vehiculo.Comentario_Piloto = Obj_Piloto.Comentario_Piloto;

                    //  copiloto
                    if (Obj_Copiloto.Participante_Copiloto_Id > 0)
                    {
                        Vehiculo.Participante_Copiloto_Id = Convert.ToInt32(Obj_Copiloto.Participante_Copiloto_Id);
                    }
                    else
                    {
                        Vehiculo.Participante_Copiloto_Id = Convert.ToInt32(Obj_Piloto.Participante_Piloto_Id);
                    }

                    Vehiculo.Revision_Medica_Copiloto = Obj_Copiloto.Revision_Medica_Copiloto;
                    Vehiculo.Comentario_Copiloto = Obj_Copiloto.Comentario_Copiloto;

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
        public string Consultar_Categorias_Vehiculo_Participante(string jsonObject)
        {
            string Json_Resultado = string.Empty;
            Cls_Ope_Eventos_Vehiculo_Participante_Negocio Obj = new Cls_Ope_Eventos_Vehiculo_Participante_Negocio();

            try
            {
                Obj = JsonConvert.DeserializeObject<Cls_Ope_Eventos_Vehiculo_Participante_Negocio>(jsonObject);

                using (var dbContext = new Sistema_TrazabilidadEntities())
                {
                    var _participantes = (from vp in dbContext.Ope_Eventos_Vehiculo_Participante

                                          join cp in dbContext.Ope_Eventos_Categorias on vp.Categoria_Id equals cp.Categoria_Id

                                          where vp.Vehiculo_Participante_Id == Obj.Vehiculo_Participante_Id

                                          select new Cls_Ope_Eventos_Vehiculo_Participante_Negocio
                                          {
                                              Categoria_Id = cp.Categoria_Id,
                                              text = cp.Clave + " - " + cp.Nombre + " (" + cp.Año_Desde + "-" + cp.Año_Hasta + ")",
                                          })
                                          .OrderBy(x => x.Categoria_Id).ToList();

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
        public string Consultar_Categorias_Competencia_Vehiculo_Participante(string jsonObject)
        {
            string Json_Resultado = string.Empty;
            Cls_Ope_Eventos_Vehiculo_Participante_Negocio Obj = new Cls_Ope_Eventos_Vehiculo_Participante_Negocio();

            try
            {
                Obj = JsonConvert.DeserializeObject<Cls_Ope_Eventos_Vehiculo_Participante_Negocio>(jsonObject);

                using (var dbContext = new Sistema_TrazabilidadEntities())
                {
                    var _participantes = (from vp in dbContext.Ope_Eventos_Vehiculo_Participante

                                          join cp in dbContext.Ope_Eventos_Categorias on vp.Categoria_Participante_Id equals cp.Categoria_Id

                                          where vp.Vehiculo_Participante_Id == Obj.Vehiculo_Participante_Id

                                          select new Cls_Ope_Eventos_Vehiculo_Participante_Negocio
                                          {
                                              Categoria_Id = cp.Categoria_Id,
                                              text = cp.Clave + " - " + cp.Nombre + " (" + cp.Año_Desde + "-" + cp.Año_Hasta + ")",
                                          })
                                          .OrderBy(x => x.Categoria_Id).ToList();

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
        public string Consultar_Vehiculos_Evento(string jsonObject)
        {
            string Json_Resultado = string.Empty;
            Cls_Ope_Eventos_Vehiculo_Participante_Negocio Obj = new Cls_Ope_Eventos_Vehiculo_Participante_Negocio();

            try
            {

                Obj = JsonConvert.DeserializeObject<Cls_Ope_Eventos_Vehiculo_Participante_Negocio>(jsonObject);
                

                using (var dbContext = new Sistema_TrazabilidadEntities())
                {

                    var _vehiculos_participaciones = (from _part in dbContext.Ope_Eventos_Vehiculo_Participante

                                                      join _veh in dbContext.Cat_Vehiculos on _part.Vehiculo_Id equals _veh.Vehiculo_Id
                                                      join _cat in dbContext.Ope_Eventos_Categorias on _part.Categoria_Id equals _cat.Categoria_Id

                                                      //join _piloto in dbContext.Cat_Participantes on _part.Participante_Piloto_Id equals _piloto.Participante_ID

                                                      where _part.Evento_Id == Obj.Evento_Id
                                                          //&& _part.Estatus == "ACTIVO"

                                                      select new Cls_Ope_Eventos_Vehiculo_Participante_Negocio
                                                      {
                                                          Vehiculo_Participante_Id = _part.Vehiculo_Participante_Id,
                                                          Vehiculo_Id = _part.Vehiculo_Id,
                                                          Evento_Id = _part.Evento_Id,
                                                          Categoria_Id = _part.Categoria_Id,
                                                          Categoria_Participante_Id = _part.Categoria_Participante_Id,
                                                          Numero_Participante = _part.Numero_Participante,
                                                          Estatus = _part.Estatus,
                                                          Revision_Mecanica = _part.Revision_Mecanica,
                                                          Comentario = _part.Comentario,

                                                          // datos del vehiculo
                                                          Marca = _veh.Marca,
                                                          Modelo = _veh.Modelo,
                                                          Año = _veh.Año,
                                                          Color_Hex_Rgb = _veh.Color_Hex_Rgb,
                                                          Color_Fondo_Hex_Rgb = _veh.Color_Fondo_Hex_Rgb,
                                                          Placas = _veh.Placas,
                                                          Categoria = _cat.Nombre,
                                                          vehiculo_cmb = (_veh.NS + " - " + _veh.Marca + " - " + _veh.Modelo),
                                                          categoria_id_cmb = (from _aux1 in dbContext.Ope_Eventos_Categorias
                                                                              where _aux1.Categoria_Id == _part.Categoria_Id
                                                                              select new
                                                                              {
                                                                                  categoria_id_cmb = _aux1.Clave + " - " + _aux1.Nombre + " (" + _aux1.Año_Desde + "-" + _aux1.Año_Hasta + ")"
                                                                              }
                                                                              ).FirstOrDefault().categoria_id_cmb,
                                                          categoria_participante_cmb = (from _aux1 in dbContext.Ope_Eventos_Categorias
                                                                                        where _aux1.Categoria_Id == _part.Categoria_Participante_Id
                                                                                        select new
                                                                                        {
                                                                                            categoria_participante_cmb = _aux1.Clave + " - " + _aux1.Nombre + " (" + _aux1.Año_Desde + "-" + _aux1.Año_Hasta + ")"
                                                                                        }
                                                                              ).FirstOrDefault().categoria_participante_cmb,

                                                          Participante_Piloto_Id_Cmb = (from _aux1 in dbContext.Cat_Participantes
                                                                                        where _aux1.Participante_ID == _part.Participante_Piloto_Id
                                                                                        select new
                                                                                        {
                                                                                            Participante_Piloto_Id_Cmb = _aux1.Clave + " - " + _aux1.Nombre,
                                                                                        }
                                                                              ).FirstOrDefault().Participante_Piloto_Id_Cmb,

                                                          Piloto = (from _aux1 in dbContext.Cat_Participantes
                                                                                        where _aux1.Participante_ID == _part.Participante_Piloto_Id
                                                                                        select new
                                                                                        {
                                                                                            Piloto =  _aux1.Nombre,
                                                                                        }
                                                                              ).FirstOrDefault().Piloto,


                                                          Participante_Copiloto_Id_Cmb = (from _aux1 in dbContext.Cat_Participantes
                                                                                          where _aux1.Participante_ID == _part.Participante_Copiloto_Id
                                                                                          select new
                                                                                          {
                                                                                              Participante_Copiloto_Id_Cmb = _aux1.Clave + " - " + _aux1.Nombre,
                                                                                          }
                                                                              ).FirstOrDefault().Participante_Copiloto_Id_Cmb,
                                                      }).OrderBy(x => x.Numero_Participante).ToList();


                    //  se consultan los datos del vehiculo

                    

                    Json_Resultado = JsonMapper.ToJson(_vehiculos_participaciones.ToList());
                }
            }
            catch (Exception e)
            {

            }

            return Json_Resultado;
        }


        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string Consultar_Participantes_Piloto_Evento(string jsonObject)
        {
            string Json_Resultado = string.Empty;
            Cls_Ope_Eventos_Vehiculo_Participante_Negocio Obj = new Cls_Ope_Eventos_Vehiculo_Participante_Negocio();

            try
            {

                Obj = JsonConvert.DeserializeObject<Cls_Ope_Eventos_Vehiculo_Participante_Negocio>(jsonObject);


                using (var dbContext = new Sistema_TrazabilidadEntities())
                {

                    var _vehiculos_participaciones = (from _part in dbContext.Ope_Eventos_Vehiculo_Participante
                                                      
                                                      join _piloto in dbContext.Cat_Participantes on _part.Participante_Piloto_Id equals _piloto.Participante_ID

                                                      where _part.Vehiculo_Participante_Id == Obj.Vehiculo_Participante_Id

                                                      select new Cls_Ope_Eventos_Vehiculo_Datos_Piloto
                                                      {
                                                          Participante_Piloto_Id = _part.Participante_Piloto_Id,
                                                          Comentario_Piloto = _part.Comentario_Piloto,
                                                          Revision_Medica_Piloto = _part.Revision_Medica_Piloto,
                                                          text = _piloto.Clave + " - " + _piloto.Nombre,

                                                      }).OrderBy(x => x.Participante_Piloto_Id).ToList();


                    //  se consultan los datos del vehiculo



                    Json_Resultado = JsonMapper.ToJson(_vehiculos_participaciones.ToList());
                }
            }
            catch (Exception e)
            {

            }

            return Json_Resultado;
        }


        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string Consultar_Participantes_Copiloto_Evento(string jsonObject)
        {
            string Json_Resultado = string.Empty;
            Cls_Ope_Eventos_Vehiculo_Participante_Negocio Obj = new Cls_Ope_Eventos_Vehiculo_Participante_Negocio();

            try
            {

                Obj = JsonConvert.DeserializeObject<Cls_Ope_Eventos_Vehiculo_Participante_Negocio>(jsonObject);


                using (var dbContext = new Sistema_TrazabilidadEntities())
                {

                    var _vehiculos_participaciones = (from _part in dbContext.Ope_Eventos_Vehiculo_Participante

                                                      join _copiloto in dbContext.Cat_Participantes on _part.Participante_Copiloto_Id equals _copiloto.Participante_ID
                                                      
                                                      where _part.Vehiculo_Participante_Id == Obj.Vehiculo_Participante_Id

                                                      select new Cls_Ope_Eventos_Vehiculo_Datos_Copiloto
                                                      {
                                                          Participante_Copiloto_Id = _part.Participante_Copiloto_Id,
                                                          Comentario_Copiloto = _part.Comentario_Copiloto,
                                                          Revision_Medica_Copiloto = _part.Revision_Medica_Copiloto,
                                                          text = _copiloto.Clave + " - " + _copiloto.Nombre,

                                                      }).OrderBy(x => x.Participante_Copiloto_Id).ToList();


                    //  se consultan los datos del vehiculo



                    Json_Resultado = JsonMapper.ToJson(_vehiculos_participaciones.ToList());
                }
            }
            catch (Exception e)
            {

            }

            return Json_Resultado;
        }


        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void Consultar_Vehiculo_Combo()
        {
            string Json_Resultado = string.Empty;
            List<Cls_Select2> Lst_Combo = new List<Cls_Select2>();

            try
            {
                string q = string.Empty;
                string tipo = string.Empty;
                string marca_model = string.Empty;

                NameValueCollection nvc = Context.Request.Form;

                if (!String.IsNullOrEmpty(nvc["q"]))
                {
                    q = nvc["q"].ToString().Trim();
                }
             

                using (var dbContext = new Sistema_TrazabilidadEntities())
                {

                    var _vehiculos = (from _veh in dbContext.Cat_Vehiculos
                                         where _veh.Estatus == "ACTIVO"
                                         && (_veh.NS.Contains(q) || _veh.Modelo.Contains(q) || _veh.Marca.Contains(q))
                                         select new Cls_Select2
                                         {

                                             id = _veh.Vehiculo_Id.ToString(),
                                             text = _veh.NS + " - " + _veh.Marca + " - " + _veh.Modelo ,
                                             detalle_1 = _veh.Vehiculo_Id.ToString(),
                                             detalle_2 = _veh.Color_Hex_Rgb,
                                             detalle_3 = _veh.Color_Fondo_Hex_Rgb,
                                         });

                  

                    Json_Resultado = JsonMapper.ToJson(_vehiculos.ToList());
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
        public void Consultar_Categoria_Vehiculo_Combo()
        {
            string Json_Resultado = string.Empty;
            List<Cls_Select2> Lst_Combo = new List<Cls_Select2>();
            Cls_Ope_Eventos_Vehiculo_Participante_Negocio Obj = new Cls_Ope_Eventos_Vehiculo_Participante_Negocio();

            try
            {

                string q = string.Empty;
                string evento_id = string.Empty;

                NameValueCollection nvc = Context.Request.Form;

                if (!String.IsNullOrEmpty(nvc["q"]))
                    q = nvc["q"].ToString().Trim();

                if (!String.IsNullOrEmpty(nvc["evento_id"]))
                    evento_id = nvc["evento_id"].ToString().Trim();

                using (var dbContext = new Sistema_TrazabilidadEntities())
                {

                    var _Categorias = (from _cat in dbContext.Ope_Eventos_Categorias
                                       where _cat.Estatus == "ACTIVO"
                                       && _cat.Evento_Id.ToString() == evento_id
                                       && (_cat.Clave.Contains(q) || _cat.Nombre.Contains(q) || _cat.Año_Desde.ToString().Contains(q) || _cat.Año_Desde.ToString().Contains(q))


                                       select new Cls_Select2
                                       {
                                           id = _cat.Categoria_Id.ToString(),
                                           text = _cat.Clave + " - " + _cat.Nombre + " (" + _cat.Año_Desde + "-" + _cat.Año_Hasta + ")",
                                       });


                    Json_Resultado = JsonMapper.ToJson(_Categorias.ToList());
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
        public void Consultar_participantes_Vehiculo_Combo()
        {
            string Json_Resultado = string.Empty;
            List<Cls_Select2> Lst_Combo = new List<Cls_Select2>();
            Cls_Ope_Eventos_Vehiculo_Participante_Negocio Obj = new Cls_Ope_Eventos_Vehiculo_Participante_Negocio();

            try
            {

                string q = string.Empty;
                string evento_id = string.Empty;

                NameValueCollection nvc = Context.Request.Form;

                if (!String.IsNullOrEmpty(nvc["q"]))
                    q = nvc["q"].ToString().Trim();

            
                using (var dbContext = new Sistema_TrazabilidadEntities())
                {

                    var _Participantes = (from _part in dbContext.Cat_Participantes
                                       where _part.Estatus == "ACTIVO"
                                       && (_part.Clave.Contains(q) || _part.Nombre.Contains(q))

                                       select new Cls_Select2
                                       {
                                           id = _part.Participante_ID.ToString(),
                                           text = _part.Clave + " - " + _part.Nombre ,
                                       });


                    Json_Resultado = JsonMapper.ToJson(_Participantes.ToList());
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
        public string Obtener_Clave_Participante(string jsonObject)
        {
            string Json_Resultado = string.Empty;
            Cls_Ope_Eventos_Vehiculo_Participante_Negocio Obj = new Cls_Ope_Eventos_Vehiculo_Participante_Negocio();

            try
            {

                Obj = JsonConvert.DeserializeObject<Cls_Ope_Eventos_Vehiculo_Participante_Negocio>(jsonObject);


                using (var dbContext = new Sistema_TrazabilidadEntities())
                {

                    var _vehiculos_participaciones = (from _part in dbContext.Ope_Eventos_Vehiculo_Participante
                                                      where _part.Evento_Id == Obj.Evento_Id
                                                      select new Cls_Ope_Eventos_Vehiculo_Participante_Negocio
                                                      {
                                                          Vehiculo_Participante_Id = _part.Vehiculo_Participante_Id,
                                                      }
                                                      ).ToList();


                    //  se consultan los datos del vehiculo



                    Json_Resultado = JsonMapper.ToJson(_vehiculos_participaciones.ToList());
                }
            }
            catch (Exception e)
            {

            }

            return Json_Resultado;
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string Obtener_Clave_Participante_Por_Categoria(string jsonObject)
        {
            string Json_Resultado = string.Empty;
            Cls_Ope_Eventos_Vehiculo_Participante_Negocio Obj = new Cls_Ope_Eventos_Vehiculo_Participante_Negocio();
            Cls_Ope_Eventos_Vehiculo_Participante_Negocio Obj_No_Participante = new Cls_Ope_Eventos_Vehiculo_Participante_Negocio();
            List<Cls_Ope_Eventos_Vehiculo_Participante_Negocio> List_Numero_Participante = new List<Cls_Ope_Eventos_Vehiculo_Participante_Negocio>();

            try
            {

                Obj = JsonConvert.DeserializeObject<Cls_Ope_Eventos_Vehiculo_Participante_Negocio>(jsonObject);


                using (var dbContext = new Sistema_TrazabilidadEntities())
                {

                    var _categoria_folios = (from _cate in dbContext.Ope_Eventos_Categorias
                                             where _cate.Categoria_Id == Obj.Categoria_Id
                                             select new Cls_Ope_Eventos_Categorias_Negocio
                                             {
                                                 Categoria_Id = _cate.Categoria_Id,
                                                 Folio_Inicio = _cate.Folio_Inicio,
                                                 Folio_Fin = _cate.Folio_Fin,
                                             }).ToList();

                    //  se consultan los datos del vehiculo
                    var _vehiculos_participaciones = (from _part in dbContext.Ope_Eventos_Vehiculo_Participante
                                                      where _part.Evento_Id == Obj.Evento_Id
                                                      && _part.Categoria_Participante_Id == Obj.Categoria_Id

                                                      select new Cls_Ope_Eventos_Vehiculo_Participante_Negocio
                                                      {
                                                          Numero_Participante = _part.Numero_Participante,
                                                      }
                                                    )
                                                    .OrderByDescending(o => o.Numero_Participante)
                                                    .Take(1)
                                                    .ToList();


                    if (_categoria_folios.Any())
                    {
                        if (_vehiculos_participaciones.Any())
                        {
                            if (_vehiculos_participaciones.Count > 0)
                            {
                                //  se recorren los valores 
                                foreach (var registro in _vehiculos_participaciones)
                                {
                                    if (registro.Numero_Participante > _categoria_folios[0].Folio_Fin)
                                    {
                                        registro.Numero_Participante = 0;
                                    }
                                    else
                                    {
                                        registro.Numero_Participante = registro.Numero_Participante + 1;
                                    }

                                    break;
                                }


                                Json_Resultado = JsonMapper.ToJson(_vehiculos_participaciones.ToList());

                            }
                            else
                            {
                                Obj_No_Participante.Numero_Participante = _categoria_folios[0].Folio_Inicio;
                                List_Numero_Participante.Add(Obj_No_Participante);
                                Json_Resultado = JsonMapper.ToJson(List_Numero_Participante.ToList());
                            }
                        }
                        else
                        {
                            Obj_No_Participante.Numero_Participante = _categoria_folios[0].Folio_Inicio;
                            List_Numero_Participante.Add(Obj_No_Participante);
                            Json_Resultado = JsonMapper.ToJson(List_Numero_Participante.ToList());
                        }


                    }
                    
                   
                }
            }
            catch (Exception e)
            {

            }

            return Json_Resultado;
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string Cancelacion(String jsonObject)
        {
            Cls_Mensaje Mensaje = new Cls_Mensaje();
            Cls_Ope_Eventos_Vehiculo_Participante_Negocio Obj_Vehiculo = new Cls_Ope_Eventos_Vehiculo_Participante_Negocio();
            string jsonResultado = "";
            try
            {
                Mensaje.Titulo = "Cancelacion de vehiculo";

                Obj_Vehiculo = JsonConvert.DeserializeObject<Cls_Ope_Eventos_Vehiculo_Participante_Negocio>(jsonObject);

                using (var dbContext = new Sistema_TrazabilidadEntities())
                {
                    Ope_Eventos_Vehiculo_Participante Vehiculo = new Ope_Eventos_Vehiculo_Participante();
                    Vehiculo = dbContext.Ope_Eventos_Vehiculo_Participante.Where(w => w.Vehiculo_Participante_Id == Obj_Vehiculo.Vehiculo_Participante_Id).FirstOrDefault();


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
        public string Reactivacion(String jsonObject)
        {
            Cls_Mensaje Mensaje = new Cls_Mensaje();
            Cls_Ope_Eventos_Vehiculo_Participante_Negocio Obj_Vehiculo = new Cls_Ope_Eventos_Vehiculo_Participante_Negocio();
            string jsonResultado = "";
            try
            {
                Mensaje.Titulo = "Reactivacion de vehículo";

                Obj_Vehiculo = JsonConvert.DeserializeObject<Cls_Ope_Eventos_Vehiculo_Participante_Negocio>(jsonObject);

                using (var dbContext = new Sistema_TrazabilidadEntities())
                {
                    Ope_Eventos_Vehiculo_Participante Vehiculo = new Ope_Eventos_Vehiculo_Participante();
                    Vehiculo = dbContext.Ope_Eventos_Vehiculo_Participante.Where(w => w.Vehiculo_Participante_Id == Obj_Vehiculo.Vehiculo_Participante_Id).FirstOrDefault();


                    Vehiculo.Estatus = "ACTIVO";
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


    }
}
