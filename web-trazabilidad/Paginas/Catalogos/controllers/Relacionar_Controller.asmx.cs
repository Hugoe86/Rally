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
    /// Descripción breve de Relacionar_Controller
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    [System.Web.Script.Services.ScriptService]
    public class Relacionar_Controller : System.Web.Services.WebService
    {
        #region Operaciones
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string alta_relacion(String jsonObject)
        {
            Cls_Mensaje Mensaje = new Cls_Mensaje();
            Cls_Cat_Relacion_Participante_Vehiculo_Negocio obj_datos = new Cls_Cat_Relacion_Participante_Vehiculo_Negocio();
            string jsonResultado = "";
            String Directorio_Guardar = "";
            String Directorio_Temporales = "";
            String Color = "#8A2BE2";
            String Icono = "fa fa-close";

            try
            {
                Mensaje.Titulo = "Alta de participante-vehiculo";

                obj_datos = JsonConvert.DeserializeObject<Cls_Cat_Relacion_Participante_Vehiculo_Negocio>(jsonObject);
                //List_Documentos = JsonConvert.DeserializeObject<List<Cls_Cat_Vehiculos_Documentos_Negocio>>(Obj_Vehiculo.tbl_documentos);

                using (var dbContext = new Sistema_TrazabilidadEntities())
                {

                    var dato_registrado = (from _relacion in dbContext.Cat_Relacion_Participante_Vehiculo
                                               where _relacion.Vehiculo_Id == obj_datos.Vehiculo_Id
                                               && _relacion.Participante_Id == obj_datos.Participante_Id
                                               select _relacion
                                              );


                    if (dato_registrado.Any())
                    {
                        Mensaje.Mensaje = "<i class='" + Icono + "'style = 'color:" + Color + ";' ></i> &nbsp; El vehiculo ya se encuentra relacionado" + " <br />";
                        Mensaje.Estatus = "error";
                    }
                    else
                    {
                        using (var transaction = dbContext.Database.BeginTransaction())
                        {
                            try
                            {
                                Cat_Relacion_Participante_Vehiculo obj_nuevo = new Cat_Relacion_Participante_Vehiculo();

                                obj_nuevo.Participante_Id = obj_datos.Participante_Id;
                                obj_nuevo.Vehiculo_Id = obj_datos.Vehiculo_Id;

                                dbContext.Cat_Relacion_Participante_Vehiculo.Add(obj_nuevo);

                                dbContext.SaveChanges();


                                transaction.Commit();
                                Mensaje.Mensaje = "La operación se realizo correctamente.";
                                Mensaje.Estatus = "success";
                            }
                            catch (Exception ex)
                            {
                                transaction.Rollback();

                                Mensaje.Mensaje = "Error Técnico. " + ex.Message;
                                Mensaje.Estatus = "error";

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
        public string Eliminar(String jsonObject)
        {
            Cls_Mensaje Mensaje = new Cls_Mensaje();
            Cls_Cat_Relacion_Participante_Vehiculo_Negocio obj_datos = new Cls_Cat_Relacion_Participante_Vehiculo_Negocio();
            string jsonResultado = "";
            try
            {
                Mensaje.Titulo = "Eliminar relacionon participante - vehiculo";

                obj_datos = JsonConvert.DeserializeObject<Cls_Cat_Relacion_Participante_Vehiculo_Negocio>(jsonObject);

                using (var dbContext = new Sistema_TrazabilidadEntities())
                {
                    using (var transaction = dbContext.Database.BeginTransaction())
                    {
                        try
                        {
                            Cat_Relacion_Participante_Vehiculo obj_relacion = new Cat_Relacion_Participante_Vehiculo();
                            obj_relacion = dbContext.Cat_Relacion_Participante_Vehiculo.Where(w => w.Relacion_Id == obj_datos.Relacion_Id).FirstOrDefault();

                            dbContext.Cat_Relacion_Participante_Vehiculo.Remove(obj_relacion);

                            dbContext.SaveChanges();

                            transaction.Commit();
                            Mensaje.Mensaje = "La operación se realizo correctamente.";
                            Mensaje.Estatus = "success";
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();

                            Mensaje.Mensaje = "Error Técnico. " + ex.Message;
                            Mensaje.Estatus = "error";

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
        #endregion


        #region Consultas

        /// <summary>
        /// se consultan los participantes
        /// </summary>
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void consultar_participantes_Combo()
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

                using (var dbContext = new Sistema_TrazabilidadEntities())
                {

                    var _responsables = (from _res in dbContext.Cat_Participantes
                                         where _res.Estatus == "ACTIVO"
                                         && (_res.Nombre.Contains(q))


                                         select new Cls_Select2
                                         {

                                             id = _res.Participante_ID.ToString(),
                                             text = _res.Nombre,

                                         }).OrderBy(o => o.text);


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


        /// <summary>
        /// se consultan los participantes
        /// </summary>
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void consultar_vehiculos_Combo()
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

                using (var dbContext = new Sistema_TrazabilidadEntities())
                {

                    var _responsables = (from _res in dbContext.Cat_Vehiculos
                                         where (_res.Marca.Contains(q))


                                         select new Cls_Select2
                                         {

                                             id = _res.Vehiculo_Id.ToString(),
                                             text = _res.Marca + " - " + _res.Modelo + " - " +_res.Año + " - "  + _res.Placas,

                                         }).OrderBy(o => o.text);


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



        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string consultar_vehiculos_relacionados_filtro(string jsonObject)
        {
            string Json_Resultado = string.Empty;
            Cls_Cat_Participantes_Negocio Obj = new Cls_Cat_Participantes_Negocio();

            try
            {

                Obj = JsonConvert.DeserializeObject<Cls_Cat_Participantes_Negocio>(jsonObject);

                using (var dbContext = new Sistema_TrazabilidadEntities())
                {

                    var _consulta = (from _relacion in dbContext.Cat_Relacion_Participante_Vehiculo

                                    //  participante
                                     join _participante in dbContext.Cat_Participantes on _relacion.Participante_Id equals _participante.Participante_ID

                                     //  vehiculo
                                     join _vehiculo in dbContext.Cat_Vehiculos on _relacion.Vehiculo_Id equals _vehiculo.Vehiculo_Id

                                     where _relacion.Participante_Id == Obj.Participante_ID


                                     select new Cls_Cat_Relacion_Participante_Vehiculo_Negocio
                                     {
                                         Relacion_Id = _relacion.Relacion_Id,
                                         Participante_Id = _relacion.Participante_Id,
                                         Vehiculo_Id = _relacion.Vehiculo_Id,
                                         Participante = _participante.Nombre,
                                         Vehiculo = _vehiculo.Marca + " - " + _vehiculo.Modelo + " - " + _vehiculo.Año + " - " + _vehiculo.Placas,
                                     })
                                          .OrderBy(x => x.Vehiculo).ToList();

                   
                    Json_Resultado = JsonMapper.ToJson(_consulta.ToList());
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
