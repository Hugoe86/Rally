using datos_trazabilidad;
using LitJson;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using web_trazabilidad.Models.Ayudante;
using web_trazabilidad.Models.Negocio;
using web_trazabilidad.Models.Negocio.Operaciones;
using System.Collections.Specialized;
using Elmah;

namespace web_trazabilidad.Paginas.Operaciones.controllers
{
    /// <summary>
    /// Summary description for CategoriasController
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class CategoriasController : System.Web.Services.WebService
    {
        #region Metodos

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string Alta(String jsonObject)
        {
            Cls_Mensaje Mensaje = new Cls_Mensaje();
            Cls_Ope_Eventos_Categorias_Negocio Obj_Categoria = new Cls_Ope_Eventos_Categorias_Negocio();
            string jsonResultado = "";

            try
            {
                Mensaje.Titulo = "Alta de categoria";
                Obj_Categoria = JsonConvert.DeserializeObject<Cls_Ope_Eventos_Categorias_Negocio>(jsonObject);

                using (var dbContext = new Sistema_TrazabilidadEntities())
                {
                    Ope_Eventos_Categorias Categoria = new Ope_Eventos_Categorias();

                    Categoria.Evento_Id = Obj_Categoria.Evento_Id.Value;
                    Categoria.Clave = Obj_Categoria.Clave;
                    Categoria.Nombre = Obj_Categoria.Nombre;
                    Categoria.Año_Desde = Obj_Categoria.Año_Desde;
                    Categoria.Año_Hasta = Obj_Categoria.Año_Hasta;
                    Categoria.Folio_Inicio = Obj_Categoria.Folio_Inicio;
                    Categoria.Folio_Fin = Obj_Categoria.Folio_Fin;
                    Categoria.Estatus = Obj_Categoria.Estatus;
                    Categoria.Usuario_Creo = Cls_Sesiones.Usuario;
                    Categoria.Fecha_Creo = DateTime.Now;

                    dbContext.Ope_Eventos_Categorias.Add(Categoria);

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
        public string Modificar(String jsonObject)
        {
            Cls_Mensaje Mensaje = new Cls_Mensaje();
            Cls_Ope_Eventos_Categorias_Negocio Obj_Categoria = new Cls_Ope_Eventos_Categorias_Negocio();
            string jsonResultado = "";

            try
            {
                Mensaje.Titulo = "Modificación de categoria";
                Obj_Categoria = JsonConvert.DeserializeObject<Cls_Ope_Eventos_Categorias_Negocio>(jsonObject);

                using (var dbContext = new Sistema_TrazabilidadEntities())
                {
                    Ope_Eventos_Categorias Categoria = new Ope_Eventos_Categorias();
                    Categoria = dbContext.Ope_Eventos_Categorias.Where(w => w.Categoria_Id == Obj_Categoria.Categoria_Id).FirstOrDefault();

                    //Categoria.Evento_Id = Obj_Categoria.Evento_Id.Value;
                    Categoria.Clave = Obj_Categoria.Clave;
                    Categoria.Nombre = Obj_Categoria.Nombre;
                    Categoria.Año_Desde = Obj_Categoria.Año_Desde;
                    Categoria.Año_Hasta = Obj_Categoria.Año_Hasta;
                    Categoria.Folio_Inicio = Obj_Categoria.Folio_Inicio;
                    Categoria.Folio_Fin = Obj_Categoria.Folio_Fin;
                    Categoria.Estatus = Obj_Categoria.Estatus;
                    Categoria.Usuario_Modifico = Cls_Sesiones.Usuario;
                    Categoria.Fecha_Modifico = DateTime.Now;

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
            Cls_Ope_Eventos_Categorias_Negocio Obj_Categoria = new Cls_Ope_Eventos_Categorias_Negocio();
            string jsonResultado = "";
            try
            {
                Mensaje.Titulo = "Cancelacion de vehiculo";

                Obj_Categoria = JsonConvert.DeserializeObject<Cls_Ope_Eventos_Categorias_Negocio>(jsonObject);

                using (var dbContext = new Sistema_TrazabilidadEntities())
                {
                    Ope_Eventos_Categorias Categoria = new Ope_Eventos_Categorias();
                    Categoria = dbContext.Ope_Eventos_Categorias.Where(w => w.Categoria_Id == Obj_Categoria.Categoria_Id).FirstOrDefault();

                    Categoria.Estatus = "INACTIVO";
                    Categoria.Usuario_Modifico = Cls_Sesiones.Usuario;
                    Categoria.Fecha_Modifico = DateTime.Now;

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
        public string Consultar_Categorias(string jsonObject)
        {
            string Json_Resultado = string.Empty;
            Cls_Ope_Eventos_Categorias_Negocio Obj = new Cls_Ope_Eventos_Categorias_Negocio();

            try
            {
                Obj = JsonConvert.DeserializeObject<Cls_Ope_Eventos_Categorias_Negocio>(jsonObject);
                
                using (var dbContext = new Sistema_TrazabilidadEntities())
                {
                    var _participantes = (from _Cat in dbContext.Ope_Eventos_Categorias
                                          where _Cat.Evento_Id == Obj.Evento_Id
                                          && _Cat.Estatus == "ACTIVO"
                                          && ((Obj.Categoria_Id != 0) ? _Cat.Categoria_Id.Equals(Obj.Categoria_Id) : true)
                                          select new Cls_Ope_Eventos_Categorias_Negocio
                                          {
                                              Categoria_Id = _Cat.Categoria_Id,
                                              Clave = _Cat.Clave,
                                              Nombre = _Cat.Nombre,
                                              Año_Desde = _Cat.Año_Desde.Value,
                                              Año_Hasta = _Cat.Año_Hasta.Value,
                                              Folio_Inicio = _Cat.Folio_Inicio.Value,
                                              Folio_Fin = _Cat.Folio_Fin.Value,
                                              Estatus = _Cat.Estatus
                                          })
                                          .OrderBy(x => x.Nombre).ToList();

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
        public void Consultar_Categorias_Combo()
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

                    var _Busqueda = (from _jornada in dbContext.Ope_Eventos_Categorias
                                     where _jornada.Estatus == "ACTIVO"
                                     && _jornada.Evento_Id.ToString() == evento_id
                                     && (_jornada.Clave.Contains(q) || _jornada.Nombre.Contains(q))

                                     select new Cls_Select2
                                     {

                                         id = _jornada.Categoria_Id.ToString(),
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
    }
}
