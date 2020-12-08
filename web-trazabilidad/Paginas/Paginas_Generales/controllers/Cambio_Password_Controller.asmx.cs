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
    /// Summary description for Cambio_Password_Controller
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
     [System.Web.Script.Services.ScriptService]
    public class Cambio_Password_Controller : System.Web.Services.WebService
    {
        #region Metodos

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string validar_pass(string jsonObject) {

            Cls_Apl_Usuarios_Password_Negocio ObjPassword = null;
            string Json_Resultado = string.Empty;
            Cls_Mensaje Mensaje = new Cls_Mensaje();
            int usuario = Convert.ToInt32(Cls_Sesiones.Usuario_ID);
            
            try
            {
                Mensaje.Titulo = "Validaciones";
                ObjPassword = JsonMapper.ToObject<Cls_Apl_Usuarios_Password_Negocio>(jsonObject);

                using (var dbContext = new Sistema_TrazabilidadEntities())
                {
                    var consulta = (from _select in dbContext.Apl_Usuarios_Password
                                    where _select.Usuario_ID.Equals(usuario)
                                    && _select.Password == ObjPassword.Password
                                    select new Cls_Apl_Usuarios_Password_Negocio
                                    {
                                        No_Registro = _select.No_Registro,
                                        Password = _select.Password
                                    });

                    if (consulta.Any())
                    {
                        Mensaje.Estatus = "error";
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
                
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string Modificar(string jsonObject)
        {

            Cls_Apl_Usuarios_Password_Negocio ObjPassword = null;
            string Json_Resultado = string.Empty;
            Cls_Mensaje Mensaje = new Cls_Mensaje();
            int usuario = Convert.ToInt32(Cls_Sesiones.Usuario_ID);
            
            try
            {
                Mensaje.Titulo = "Alta registro";
                ObjPassword = JsonMapper.ToObject<Cls_Apl_Usuarios_Password_Negocio>(jsonObject);

                using (var dbContext = new Sistema_TrazabilidadEntities())
                {

                        var _password = new Apl_Usuarios_Password();

                        _password.Empresa_ID = Convert.ToInt32(Cls_Sesiones.Empresa_ID);
                        _password.Usuario_ID = Convert.ToInt32(Cls_Sesiones.Usuario_ID);
                        _password.Sucursal_ID = Convert.ToInt32(Cls_Sesiones.Sucursal_ID);
                        _password.Password = ObjPassword.Password_Actual;
                        _password.Fecha_Password = DateTime.Now;

                        dbContext.Apl_Usuarios_Password.Add(_password);

                        var _usuario = new Apl_Usuarios();

                        _usuario = dbContext.Apl_Usuarios.Where(a => a.Usuario_ID == usuario).First();
                        _usuario.Password = Cls_Seguridad.Encriptar(ObjPassword.Password);


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

                ErrorSignal.FromCurrentContext().Raise(Ex);
                //Cls_Jira.Create_Issue(Ex, Cls_Jira.Descripcion_Referencia(Cls_Jira.IssueTypes.Bug), Cls_Jira.Descripcion_Referencia(Cls_Jira.IssuePriority.High));
            }
            finally
            {
                Json_Resultado = JsonMapper.ToJson(Mensaje);
            }
            return Json_Resultado;
        }


        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string validar_pass_actual(string jsonObject)
        {

            Cls_Apl_Usuarios_Password_Negocio ObjPassword = null;
            string Json_Resultado = string.Empty;
            Cls_Mensaje Mensaje = new Cls_Mensaje();
            int usuario = Convert.ToInt32(Cls_Sesiones.Usuario_ID);
            List<Cls_Apl_Usuarios_Password_Negocio> usuarioList = new List<Cls_Apl_Usuarios_Password_Negocio>();
            string pass_actual = "";
            try
            {
                Mensaje.Titulo = "Validaciones";
                ObjPassword = JsonMapper.ToObject<Cls_Apl_Usuarios_Password_Negocio>(jsonObject);

                using (var dbContext = new Sistema_TrazabilidadEntities())
                {
                    var consulta = (from _select in dbContext.Apl_Usuarios
                                    where _select.Usuario_ID.Equals(usuario)
                                    select new Cls_Apl_Usuarios_Password_Negocio
                                    {
                                        Usuario_ID = _select.Usuario_ID,
                                        Password = _select.Password
                                    });

                    usuarioList = consulta.ToList();

                    if (consulta.Any())
                    {
                        pass_actual = Cls_Seguridad.Desencriptar(usuarioList[0].Password);
                        if (pass_actual.Trim() == ObjPassword.Password.Trim())
                        {
                            Mensaje.Estatus = "success";
                        }
                        else
                        {
                            Mensaje.Estatus = "error";
                        }
                    }
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

        #endregion

    }
}
