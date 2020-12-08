using admin_trazabilidad.Models.Ayudante;
using admin_trazabilidad.Models.Negocio;
using datos_trazabilidad;
using LitJson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;

namespace admin_trazabilidad.Paginas.Paginas_Generales.controllers
{
    /// <summary>
    /// Summary description for Parametros_Controller
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
     [System.Web.Script.Services.ScriptService]
    public class Parametros_Controller : System.Web.Services.WebService
    {

        /// <summary>
        /// Método que realiza la alta de un registro en la tabla Apl_Cat_Parametros
        /// </summary>
        /// <returns>Objeto serializado con los resultados de la operación</returns>
		[WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string Alta_Parametro(string jsonObject)
        {
            Cls_Apl_Cat_Parametros_Negocio ObjParametro = null;
            string Json_Resultado = string.Empty;
            Cls_Mensaje Mensaje = new Cls_Mensaje();

            try
            {
                Mensaje.Titulo = "Alta registro";
                ObjParametro = JsonMapper.ToObject<Cls_Apl_Cat_Parametros_Negocio>(jsonObject);

                using (var dbContext = new Sistema_TrazabilidadEntities())
                {
                    var _parametros = new Apl_Cat_Parametros();
                    _parametros.Email = ObjParametro.Email == "" ? null : ObjParametro.Email;
                    _parametros.Contrasena = ObjParametro.Contrasena == "" ? null : Cls_Seguridad.Encriptar(ObjParametro.Contrasena);
                    _parametros.Puerto = ObjParametro.Puerto == -1 ? null : ObjParametro.Puerto;
                    _parametros.Host = ObjParametro.Host == "" ? null : ObjParametro.Host;
                    _parametros.UseDefaultCredentials = ObjParametro.UseDefaultCredentials;
                    _parametros.EnableSsl = ObjParametro.EnableSsl;
                    _parametros.Url_Jira_Service = ObjParametro.Url_Jira_Service == "" ? null : ObjParametro.Url_Jira_Service;
                    _parametros.Usuario_Jira = ObjParametro.Usuario_Jira == "" ? null : ObjParametro.Usuario_Jira;
                    _parametros.Password_Jira = ObjParametro.Password_Jira == "" ? null : Cls_Seguridad.Encriptar(ObjParametro.Password_Jira);
                    _parametros.Name_Jira_Project = ObjParametro.Name_Jira_Project == "" ? null : ObjParametro.Name_Jira_Project;
                    _parametros.Usuario_Creo = Cls_Sesiones.Usuario;
                    _parametros.Fecha_Creo = new DateTime?(DateTime.Now).Value;

                    dbContext.Apl_Cat_Parametros.Add(_parametros);
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
                else if (Ex.InnerException.InnerException.Message.Contains("Cannot insert duplicate key row in object"))
                    Mensaje.Mensaje =
                        "Existen campos definidos como claves que no pueden duplicarse. <br />" +
                        "<i class='fa fa-angle-double-right' ></i>&nbsp;&nbsp; Por favor revisar que no este ingresando datos duplicados.";
                else
                    Mensaje.Mensaje = "Informe técnico: " + Ex.Message;

                //ErrorSignal.FromCurrentContext().Raise(Ex);
            }
            finally
            {
                Json_Resultado = JsonMapper.ToJson(Mensaje);
            }
            return Json_Resultado;
        }

        /// <summary>
        /// Método que realiza la actualización de los datos del registro de la tabla Apl_Cat_Parametros.
        /// </summary>
        /// <returns>Objeto serializado con los resultados de la operación</returns>
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string Actualizar(string jsonObject)
        {
            Cls_Apl_Cat_Parametros_Negocio ObjParametro = null;
            string Json_Resultado = string.Empty;
            Cls_Mensaje Mensaje = new Cls_Mensaje();

            try
            {
                Mensaje.Titulo = "Actualizar registro";
                ObjParametro = JsonMapper.ToObject<Cls_Apl_Cat_Parametros_Negocio>(jsonObject);

                using (var dbContext = new Sistema_TrazabilidadEntities())
                {
                    var _parametros = dbContext.Apl_Cat_Parametros.Where(u => u.Parametro_ID == ObjParametro.Parametro_ID).First();

                    _parametros.Email = ObjParametro.Email == "" ? null : ObjParametro.Email ;
                    _parametros.Contrasena = ObjParametro.Contrasena == "" ? null : Cls_Seguridad.Encriptar(ObjParametro.Contrasena);
                    _parametros.Puerto = ObjParametro.Puerto == -1 ? null : ObjParametro.Puerto;
                    _parametros.Host = ObjParametro.Host == "" ? null : ObjParametro.Host;
                    _parametros.UseDefaultCredentials = ObjParametro.UseDefaultCredentials;
                    _parametros.EnableSsl = ObjParametro.EnableSsl;
                    _parametros.Url_Jira_Service = ObjParametro.Url_Jira_Service == "" ? null : ObjParametro.Url_Jira_Service;
                    _parametros.Usuario_Jira = ObjParametro.Usuario_Jira == "" ? null : ObjParametro.Usuario_Jira;
                    _parametros.Password_Jira = ObjParametro.Password_Jira == "" ? null : Cls_Seguridad.Encriptar(ObjParametro.Password_Jira);
                    _parametros.Name_Jira_Project = ObjParametro.Name_Jira_Project == "" ? null : ObjParametro.Name_Jira_Project;
                    _parametros.Usuario_Modifico = Cls_Sesiones.Usuario;
                    _parametros.Fecha_Modifico = new DateTime?(DateTime.Now).Value;

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
                else if (Ex.InnerException.InnerException.Message.Contains("Cannot insert duplicate key row in object"))
                    Mensaje.Mensaje =
                        "Existen campos definidos como claves que no pueden duplicarse. <br />" +
                        "<i class='fa fa-angle-double-right' ></i>&nbsp;&nbsp; Por favor revisar que no este ingresando datos duplicados.";
                else
                    Mensaje.Mensaje = "Informe técnico: " + Ex.Message;

                //ErrorSignal.FromCurrentContext().Raise(Ex);
            }
            finally
            {
                Json_Resultado = JsonMapper.ToJson(Mensaje);
            }
            return Json_Resultado;
        }



        /// <summary>
        /// Método que realiza la consulta de registro en la tabla de parámetros.
        /// </summary>
        /// <returns>Listado serializado del registro encontrado</returns>
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string Consultar_Parametro()
        {
            string Json_Resultado = string.Empty;
            Cls_Mensaje Mensaje = new Cls_Mensaje();
            try
            {
                using (var dbContext = new Sistema_TrazabilidadEntities())
                {
                    var parametro = dbContext.Apl_Cat_Parametros.FirstOrDefault();
                    parametro.Contrasena = parametro.Contrasena == null ? null : Cls_Seguridad.Desencriptar(parametro.Contrasena);
                    parametro.Password_Jira = parametro.Password_Jira == null ? null : Cls_Seguridad.Desencriptar(parametro.Password_Jira);

                    Mensaje.Estatus = "success";
                    Mensaje.Mensaje = "La operación se completo sin problemas.";
                    Json_Resultado = JsonMapper.ToJson(parametro);
                }
            }
            catch (Exception Ex)
            {
                //ErrorSignal.FromCurrentContext().Raise(Ex);
                Mensaje.Titulo = "Informe Técnico";
                Mensaje.Estatus = "error";
                Mensaje.Mensaje = "Informe técnico: " + Ex.Message;
            }

            return string.IsNullOrEmpty(Json_Resultado) ? "[]" : Json_Resultado ;
        }
    }
}
