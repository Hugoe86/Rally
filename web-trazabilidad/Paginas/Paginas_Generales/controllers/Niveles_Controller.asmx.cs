using datos_trazabilidad;
using Elmah;
using LitJson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using web_trazabilidad.Models.Ayudante;
using web_trazabilidad.Models.Negocio;

namespace web_trazabilidad.Paginas.Paginas_Generales.controllers
{
    /// <summary>
    /// Summary description for Niveles_Controller
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class Niveles_Controller : System.Web.Services.WebService
    {

       #region (Métodos)
        /// <summary>
        /// Método que realiza el alta de la Tipos_Productos.
        /// </summary>
        /// <returns>Objeto serializado con los resultados de la operación</returns>
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string Alta(string jsonObject)
        {
            Cls_Apl_Niveles_Negocio Obj_Niveles = null;
            string Json_Resultado = string.Empty;
            Cls_Mensaje Mensaje = new Cls_Mensaje();

            try
            {
                Mensaje.Titulo = "Alta registro";
                Obj_Niveles = JsonMapper.ToObject<Cls_Apl_Niveles_Negocio>(jsonObject);

                using (var dbContext = new Sistema_TrazabilidadEntities())
                {
                    var _niveles = new Apl_Niveles();
                    _niveles.Nombre = Obj_Niveles.Nombre;
                    _niveles.Usuario_Creo = Cls_Sesiones.Datos_Usuario.Usuario;
                    _niveles.Fecha_Creo = new DateTime?(DateTime.Now).Value;

                    dbContext.Apl_Niveles.Add(_niveles);
                    dbContext.SaveChanges();
                    Mensaje.Estatus = "success";
                    Mensaje.Mensaje = "La operación se completo sin problemas.";
                }
            }
            catch (Exception Ex)
            {
                Mensaje.Titulo = "Informe Técnico";
                Mensaje.Estatus = "error";
                if (Ex.InnerException.Message.Contains("Los datos de cadena o binarios se truncarían"))
                    Mensaje.Mensaje =
                        "Alguno de los campos que intenta insertar tiene un tamaño mayor al establecido en la base de datos. <br /><br />" +
                        "<i class='fa fa-angle-double-right' ></i>&nbsp;&nbsp; Los datos de cadena o binarios se truncarían";
                else
                    Mensaje.Mensaje = "Informe técnico: " + Ex.Message;

                ErrorSignal.FromCurrentContext().Raise(Ex);
                //Cls_Jira.Create_Issue(Ex, Cls_Jira.Descripcion_Referencia(Cls_Jira.IssueTypes.Bug), Cls_Jira.Descripcion_Referencia(Cls_Jira.IssuePriority.High));
            }
            finally
            {
                Json_Resultado = JsonMapper.ToJson(Mensaje);
            }
            return Json_Resultado;
        }
        /// <summary>
        /// Método que realiza la actualización de los datos de la Tipos_Productos seleccionada.
        /// </summary>
        /// <returns>Objeto serializado con los resultados de la operación</returns>
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string Actualizar(string jsonObject)
        {
            Cls_Apl_Niveles_Negocio Obj_Niveles = null;
            string Json_Resultado = string.Empty;
            Cls_Mensaje Mensaje = new Cls_Mensaje();

            try
            {
                Mensaje.Titulo = "Actualizar registro";
                Obj_Niveles = JsonMapper.ToObject<Cls_Apl_Niveles_Negocio>(jsonObject);

                using (var dbContext = new Sistema_TrazabilidadEntities())
                {
                    var _niveles = dbContext.Apl_Niveles.Where(u => u.Nivel_ID == Obj_Niveles.Nivel_ID).First();

                    _niveles.Nombre = Obj_Niveles.Nombre;
                    _niveles.Usuario_Modifico = Cls_Sesiones.Datos_Usuario.Usuario;
                    _niveles.Fecha_Modifico = new DateTime?(DateTime.Now);

                    dbContext.SaveChanges();
                    Mensaje.Estatus = "success";
                    Mensaje.Mensaje = "La operación se completo sin problemas.";
                }
            }
            catch (Exception Ex)
            {
                Mensaje.Estatus = "error";
                Mensaje.Mensaje = "Informe técnico: " + Ex.Message;
                ErrorSignal.FromCurrentContext().Raise(Ex);
                //Cls_Jira.Create_Issue(Ex, Cls_Jira.Descripcion_Referencia(Cls_Jira.IssueTypes.Bug), Cls_Jira.Descripcion_Referencia(Cls_Jira.IssuePriority.High));
            }
            finally
            {
                Json_Resultado = JsonMapper.ToJson(Mensaje);
            }
            return Json_Resultado;
        }
        /// <summary>
        /// Método que elimina el registro seleccionado.
        /// </summary>
        /// <returns>Objeto serializado con los resultados de la operación</returns>
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string Eliminar(string jsonObject)
        {
            Cls_Apl_Niveles_Negocio Obj_Niveles = null;
            string Json_Resultado = string.Empty;
            Cls_Mensaje Mensaje = new Cls_Mensaje();

            try
            {
                Mensaje.Titulo = "Eliminar registro";
                Obj_Niveles = JsonMapper.ToObject<Cls_Apl_Niveles_Negocio>(jsonObject);

                using (var dbContext = new Sistema_TrazabilidadEntities())
                {
                    var _niveles = dbContext.Apl_Niveles.Where(u => u.Nivel_ID == Obj_Niveles.Nivel_ID).First();
                    dbContext.Apl_Niveles.Remove(_niveles);
                    dbContext.SaveChanges();
                    Mensaje.Estatus = "success";
                    Mensaje.Mensaje = "La operación se completo sin problemas.";
                }
            }
            catch (Exception Ex)
            {
                Mensaje.Estatus = "error";
                Mensaje.Mensaje = "Informe técnico: " + Ex.Message;
                ErrorSignal.FromCurrentContext().Raise(Ex);
                //Cls_Jira.Create_Issue(Ex, Cls_Jira.Descripcion_Referencia(Cls_Jira.IssueTypes.Bug), Cls_Jira.Descripcion_Referencia(Cls_Jira.IssuePriority.High));
            }
            finally
            {
                Json_Resultado = JsonMapper.ToJson(Mensaje);
            }
            return Json_Resultado;
        }
        /// <summary>
        /// Método que realiza la búsqueda de Tipos_Productos.
        /// </summary>
        /// <returns>Listado de Tipos_Productos filtradas por clave</returns>
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string Consultar_Niveles_Por_Nombre(string jsonObject)
        {
            Cls_Apl_Niveles_Negocio Obj_Niveles = null;
            string Json_Resultado = string.Empty;
            List<Cls_Apl_Niveles_Negocio> Lista_Niveles = new List<Cls_Apl_Niveles_Negocio>();
            Cls_Mensaje Mensaje = new Cls_Mensaje();

            try
            {
                Mensaje.Titulo = "Validaciones";
                Obj_Niveles = JsonMapper.ToObject<Cls_Apl_Niveles_Negocio>(jsonObject);

                using (var dbContext = new Sistema_TrazabilidadEntities())
                {
                    var _niveles = (from _Niveles in dbContext.Apl_Niveles
                                            where
                                            _Niveles.Nombre.Equals(Obj_Niveles.Nombre)
                                            select new Cls_Apl_Niveles_Negocio
                                            {
                                                Nivel_ID = _Niveles.Nivel_ID,
                                                Nombre = _Niveles.Nombre
                                            }).OrderByDescending(u => u.Nivel_ID);

                    if (_niveles.Any())
                    {
                        if (Obj_Niveles.Nivel_ID == 0)
                        {
                            Mensaje.Estatus = "error";
                            if (!string.IsNullOrEmpty(Obj_Niveles.Nombre))
                                Mensaje.Mensaje = "El nombre ingresado ya se encuentra registrado.";
                        }
                        else
                        {
                            var item_edit = _niveles.Where(u => u.Nivel_ID == Obj_Niveles.Nivel_ID);

                            if (item_edit.Count() == 1)
                                Mensaje.Estatus = "success";
                            else
                            {
                                Mensaje.Estatus = "error";
                                if (!string.IsNullOrEmpty(Obj_Niveles.Nombre))
                                    Mensaje.Mensaje = "El nombre ingresado ya se encuentra registrado.";
                            }
                        }
                    }
                    else
                        Mensaje.Estatus = "success";

                    Json_Resultado = JsonMapper.ToJson(Mensaje);
                }
            }
            catch (Exception Ex)
            {
                ErrorSignal.FromCurrentContext().Raise(Ex);
                //Cls_Jira.Create_Issue(Ex, Cls_Jira.Descripcion_Referencia(Cls_Jira.IssueTypes.Bug), Cls_Jira.Descripcion_Referencia(Cls_Jira.IssuePriority.High));
            }
            return Json_Resultado;
        }
        /// <summary>
        /// Método que realiza la búsqueda de Tipos_Productos.
        /// </summary>
        /// <returns>Listado serializado con las Tipos_Productos según los filtros aplícados</returns>
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string Consultar_Niveles_Por_Filtros(string jsonObject)
        {
            Cls_Apl_Niveles_Negocio Obj_Niveles = null;
            string Json_Resultado = string.Empty;
            List<Cls_Apl_Niveles_Negocio> Lista_Niveles = new List<Cls_Apl_Niveles_Negocio>();

            try
            {
                Obj_Niveles = JsonMapper.ToObject<Cls_Apl_Niveles_Negocio>(jsonObject);

                using (var dbContext = new Sistema_TrazabilidadEntities())
                {
                    var Niveles = (from _niveles in dbContext.Apl_Niveles
                                    where
                                     (!string.IsNullOrEmpty(Obj_Niveles.Nombre) ? _niveles.Nombre.ToLower().Contains(Obj_Niveles.Nombre.ToLower()) : true)
                                    select new Cls_Apl_Niveles_Negocio
                                    {
                                        Nivel_ID = _niveles.Nivel_ID,
                                        Nombre = _niveles.Nombre,
                                    }).OrderByDescending(u => u.Nivel_ID);

                    foreach (var p in Niveles)
                        Lista_Niveles.Add((Cls_Apl_Niveles_Negocio)p);

                    Json_Resultado = JsonMapper.ToJson(Lista_Niveles);
                }
            }
            catch (Exception Ex)
            {
                ErrorSignal.FromCurrentContext().Raise(Ex);
                //Cls_Jira.Create_Issue(Ex, Cls_Jira.Descripcion_Referencia(Cls_Jira.IssueTypes.Bug), Cls_Jira.Descripcion_Referencia(Cls_Jira.IssuePriority.High));
            }
            return Json_Resultado;
        }
        #endregion
    }
}
