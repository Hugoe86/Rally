using admin_trazabilidad.Models.Negocio;
using datos_trazabilidad;
using Elmah;
using LitJson;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Web;
using System.Web.Script.Services;
using System.Web.Security;
using System.Web.Services;
using web_trazabilidad.Models.Ayudante;
using web_trazabilidad.Models.Negocio;
using web_trazabilidad.Models.Negocio.Generales;
using web_trazabilidad.Models.Negocio.Trazabilidad;

namespace web_trazabilidad.Paginas.Paginas_Generales.controllers
{
    /// <summary>
    /// Summary description for Autentificacion_Controller
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class Autentificacion_Controller : System.Web.Services.WebService
    {
        /// <summary>
        /// Método que realiza la autentificación del usuario.
        /// </summary>
        /// <param name="jsonObject">Datos requeridos del usuario para la autentificación</param>
        /// <returns>Estatus de la autentificación</returns>
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string autentificacion(string jsonObject)
        {
            Cls_Apl_Login Obj_Usuario = null;
            string Json_Resultado = string.Empty;
            Cls_Mensaje Mensaje = new Cls_Mensaje();

            try
            {
                Mensaje.Titulo = "Autentificación";
                Obj_Usuario = LitJson.JsonMapper.ToObject<Cls_Apl_Login>(jsonObject);
                string pwd = Cls_Seguridad.Encriptar(Obj_Usuario.Password);

                using (var dbContext = new Sistema_TrazabilidadEntities())
                {
                    var _usuarios = from _usuario in dbContext.Apl_Usuarios
                                    join _estatus in dbContext.Tra_Cat_Estatus on _usuario.Estatus_ID equals _estatus.Estatus_ID
                                    join rel in dbContext.Apl_Rel_Usuarios_Roles on _usuario.Usuario_ID equals rel.Usuario_ID

                                    join rol_sucursal in dbContext.Apl_Roles_Sucursales 
                                        on new { rel.Rol_ID, Sucursal_ID = rel.Sucursal_ID.Value } equals new { rol_sucursal.Rol_ID, rol_sucursal.Sucursal_ID }

                                    join rol in dbContext.Apl_Roles 
                                        on rol_sucursal.Rol_ID equals rol.Rol_ID

                                    where
                                        _estatus.Estatus.Equals("ACTIVO")
                                        && _usuario.Email.Equals(Obj_Usuario.Usuario)
                                        && _usuario.Password.ToString() == pwd
                                        && rol_sucursal.Sucursal_ID.ToString() == Obj_Usuario.Sucursal_ID
                                    select new Cls_Apl_Login
                                    {
                                        Usuario_ID = _usuario.Usuario_ID.ToString(),
                                        Usuario = _usuario.Usuario,
                                        Rol_ID = rel.Rol_ID.ToString(),
                                        Empresa_ID = rel.Empresa_ID.ToString(),
                                        Sucursal_ID = rol_sucursal.Sucursal_ID.ToString(),
                                        Default_Admin_Empresa = rol.Default_Admin_Empresa,
                                    };

                    if (_usuarios.Any())
                    {
                        var usuario = _usuarios.First();
                        var Sucursal = (from _sucursal in dbContext.Apl_Sucursales
                                        where _sucursal.Sucursal_ID.ToString() == Obj_Usuario.Sucursal_ID
                                        select new Cls_Apl_Sucursales
                                        {
                                            Sucursal_ID = _sucursal.Sucursal_ID,
                                            Nombre = _sucursal.Nombre,
                                            Direccion = _sucursal.Direccion + " Col. " + _sucursal.Colonia + " C.P. " + _sucursal.CP + " " + _sucursal.Ciudad + ", " + _sucursal.Estado,
                                            Telefono = _sucursal.Telefono,
                                            Email = _sucursal.Email
                                        }).First();
                        var Empresa = (from _empresa in dbContext.Apl_Empresas
                                       where _empresa.Empresa_ID.ToString() == usuario.Empresa_ID
                                       select new Cls_Apl_Cat_Empresas_Negocio
                                       {
                                           Empresa_ID = _empresa.Empresa_ID,
                                           Nombre = _empresa.Nombre,
                                           Direccion = _empresa.Direccion + " Col. " + _empresa.Colonia + " C.P. " + _empresa.CP + " " + _empresa.Ciudad + ", " + _empresa.Estado,
                                           Telefono = _empresa.Telefono,
                                           Email = _empresa.Email,
                                           Nombre_Carpeta=_empresa.Nombre_Carpeta
                                       }).First();

                        ////////var _parametros_tra = dbContext.Tra_Cat_Parametros.Where(u => u.Empresa_ID== Empresa.Empresa_ID && u.Sucursal_ID==Sucursal.Sucursal_ID);
                        ////////Cls_Sesiones.Habilitar_Contenerdor_No_Piezas = _parametros_tra.Any() ? String.IsNullOrEmpty(_parametros_tra.First().Habilitar_Contenerdor_No_Piezas.ToString()) ? "False" : _parametros_tra.First().Habilitar_Contenerdor_No_Piezas.ToString() : "False";

                        ////////var _parametros_conta = dbContext.Cat_Con_Parametros.Where(u => u.Empresa_ID == Empresa.Empresa_ID && u.Sucursal_ID == Sucursal.Sucursal_ID);
                        ////////Cls_Sesiones.Generar_Folio_Poliza = _parametros_conta.Any() ? String.IsNullOrEmpty(_parametros_conta.First().Generar_Folio_Poliza.ToString()) ? "False" : _parametros_conta.First().Generar_Folio_Poliza.ToString() : "False";
                        ////////Cls_Sesiones.Separador_Cuenta_Contable = _parametros_conta.Any() ? String.IsNullOrEmpty(_parametros_conta.First().Separador_Cuenta_Contable.ToString()) ? "" : _parametros_conta.First().Separador_Cuenta_Contable.ToString() : "";

                        Cls_Sesiones.Usuario = usuario.Usuario;
                        Cls_Sesiones.Usuario_ID = usuario.Usuario_ID.ToString();
                        Cls_Sesiones.Rol_ID = usuario.Rol_ID.ToString();
                        Cls_Sesiones.Empresa_ID = usuario.Empresa_ID.ToString();
                        Cls_Sesiones.Sucursal_ID = usuario.Sucursal_ID;
                        Cls_Sesiones.Default_Admin_Empresa = usuario.Default_Admin_Empresa;
                        Cls_Sesiones.Correo_Usuario = Obj_Usuario.Usuario;
                        Cls_Sesiones.Sucursal_Nombre = Obj_Usuario.Sucursal;
                        Cls_Sesiones.Empresa_Nombre = Empresa.Nombre;
                        Cls_Sesiones.Empresa_Email = Empresa.Email;
                        Cls_Sesiones.Empresa_Telefono = Empresa.Telefono;
                        Cls_Sesiones.Empresa_Direccion = Empresa.Direccion;
                        Cls_Sesiones.Sucursal_Direccion = Sucursal.Direccion;
                        Cls_Sesiones.Sucursal_Email = Sucursal.Email;
                        Cls_Sesiones.Sucursal_Telefono = Sucursal.Telefono;
                        Cls_Sesiones.Bloqueo_Pantalla = "NO";
                        Cls_Sesiones.Nombre_Carpeta = Empresa.Nombre_Carpeta;
                       

                        ACL(dbContext);

                        var _user = dbContext.Apl_Usuarios.Where(u => u.Usuario_ID.ToString().Equals(usuario.Usuario_ID)).Select(u => u);
                        Cls_Sesiones.Datos_Usuario = _user.Any() ? _user.First() : null;

                        Mensaje.Estatus = "success";
                        Mensaje.Mensaje = "La operación se completo sin problemas.";
                        Mensaje.ID = usuario.Empresa_ID;

                        FormsAuthentication.Initialize();
                    }
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

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string Pre_autentificacion(string jsonObject)
        {
            Cls_Apl_Login Obj_Usuario = null;
            string Json_Resultado = string.Empty;
            Cls_Mensaje Mensaje = new Cls_Mensaje();
            Mensaje.LoginDirectly = false;

            try
            {
                Mensaje.Titulo = "Pre_Autentificación";
                Obj_Usuario = LitJson.JsonMapper.ToObject<Cls_Apl_Login>(jsonObject);
                string pwd = Cls_Seguridad.Encriptar(Obj_Usuario.Password);

                using (var dbContext = new Sistema_TrazabilidadEntities())
                {

                    var _usuarios = from _usuario in dbContext.Apl_Usuarios

                                    join _estatus in dbContext.Tra_Cat_Estatus
                                        on _usuario.Estatus_ID equals _estatus.Estatus_ID
                                    join _rel in dbContext.Apl_Rel_Usuarios_Roles
                                        on _usuario.Usuario_ID equals _rel.Usuario_ID
                                    join _roles in dbContext.Apl_Roles
                                        on _rel.Rol_ID equals _roles.Rol_ID
                                    join _rol_sucursal in dbContext.Apl_Roles_Sucursales
                                        on new { _rel.Rol_ID, Sucursal_ID = _rel.Sucursal_ID.Value } equals new { _rol_sucursal.Rol_ID, _rol_sucursal.Sucursal_ID }
                                    join _sucursal in dbContext.Apl_Sucursales
                                        on _rol_sucursal.Sucursal_ID equals _sucursal.Sucursal_ID
                                    join _empresa in dbContext.Apl_Empresas
                                        on _sucursal.Empresa_ID equals _empresa.Empresa_ID

                                    where _estatus.Estatus.Equals("ACTIVO")
                                      && _usuario.Email.Equals(Obj_Usuario.Usuario)
                                      && _usuario.Password.ToString() == pwd
                                      && _roles.Tipo == "WEB"
                                    select new Cls_Apl_Login
                                    {
                                        Usuario_ID = _usuario.Usuario_ID.ToString(),
                                        Usuario = _usuario.Usuario,
                                        Rol_ID = _rel.Rol_ID.ToString(),
                                        Tipo = _roles.Tipo,
                                        Empresa_ID = _empresa.Empresa_ID.ToString(),
                                        Sucursal_ID = _sucursal.Sucursal_ID.ToString(),
                                        Sucursal = _sucursal.Nombre,
                                        Direccion_Sucursal = _sucursal.Direccion + " Col. " + _sucursal.Colonia + " C.P. " + _sucursal.CP + " " + _sucursal.Ciudad + ", " + _sucursal.Estado,
                                        Tel_Sucursal = _sucursal.Telefono,
                                        Email_Sucursal = _sucursal.Email,
                                        Empresa = _empresa.Nombre,
                                        Direccion_Empresa = _empresa.Direccion + " Col. " + _empresa.Colonia + " C.P. " + _empresa.CP + " " + _empresa.Ciudad + ", " + _empresa.Estado,
                                        Tel_Empresa = _empresa.Telefono,
                                        Nombre_Carpeta = _empresa.Nombre_Carpeta,
                                        Email_Empresa = _empresa.Email,
                                        Email = _usuario.Email,
                                        Default_Admin_Empresa = _roles.Default_Admin_Empresa
                                    };

                    if (_usuarios.Any())
                    {
                        if (_usuarios.Count() == 1)
                        {
                            var usuario = _usuarios.First();

                            Cls_Sesiones.Usuario = usuario.Usuario;
                            Cls_Sesiones.Usuario_ID = usuario.Usuario_ID.ToString();
                            Cls_Sesiones.Rol_ID = usuario.Rol_ID.ToString();
                            Cls_Sesiones.Empresa_ID = usuario.Empresa_ID.ToString();
                            Cls_Sesiones.Sucursal_ID = usuario.Sucursal_ID;
                            Cls_Sesiones.Default_Admin_Empresa = usuario.Default_Admin_Empresa;
                            Cls_Sesiones.Correo_Usuario = Obj_Usuario.Usuario;
                            Cls_Sesiones.Sucursal_Nombre = usuario.Sucursal;
                            Cls_Sesiones.Sucursal_Direccion = usuario.Direccion_Sucursal;
                            Cls_Sesiones.Sucursal_Email = usuario.Email_Sucursal;
                            Cls_Sesiones.Sucursal_Telefono = usuario.Tel_Sucursal;
                            Cls_Sesiones.Empresa_Nombre = usuario.Empresa;
                            Cls_Sesiones.Empresa_Direccion = usuario.Direccion_Empresa;
                            Cls_Sesiones.Empresa_Email = usuario.Email_Empresa;
                            Cls_Sesiones.Empresa_Telefono = usuario.Tel_Empresa;
                            Cls_Sesiones.Nombre_Carpeta = usuario.Nombre_Carpeta;
                            ACL(dbContext);

                            //var _user = dbContext.Apl_Usuarios.Where(u => u.Usuario_ID.ToString().Equals(usuario.Usuario_ID)).Select(u => u);
                            //Cls_Sesiones.Datos_Usuario = _user.Any() ? _user.First() : null;

                            //var _parametros_tra = dbContext.Tra_Cat_Parametros.Where(u => u.Empresa_ID.ToString() == usuario.Empresa_ID && u.Sucursal_ID.ToString() == usuario.Sucursal_ID);
                            //Cls_Sesiones.Habilitar_Contenerdor_No_Piezas = _parametros_tra.Any() ? String.IsNullOrEmpty(_parametros_tra.First().Habilitar_Contenerdor_No_Piezas.ToString()) ? "False" : _parametros_tra.First().Habilitar_Contenerdor_No_Piezas.ToString() : "False";

                            //var _parametros_conta = dbContext.Cat_Con_Parametros.Where(u => u.Empresa_ID.ToString() == usuario.Empresa_ID && u.Sucursal_ID.ToString() == usuario.Sucursal_ID);
                            //Cls_Sesiones.Generar_Folio_Poliza = _parametros_conta.Any() ? String.IsNullOrEmpty(_parametros_conta.First().Generar_Folio_Poliza.ToString()) ? "False" : _parametros_conta.First().Generar_Folio_Poliza.ToString() : "False";
                            //Cls_Sesiones.Separador_Cuenta_Contable = _parametros_conta.Any() ? String.IsNullOrEmpty(_parametros_conta.First().Separador_Cuenta_Contable.ToString()) ? "" : _parametros_conta.First().Separador_Cuenta_Contable.ToString() : "";

                            Mensaje.Estatus = "success";
                            Mensaje.Mensaje = "La operación se completo sin problemas.";
                            Mensaje.ID = usuario.Empresa_ID;

                            FormsAuthentication.Initialize();
                            Mensaje.LoginDirectly = true;//bandera para acceder directamente
                        }
                        //mesage de exito, significa que si se accedio correctamente, pero el usuario tiene mas de una sucursal
                        Mensaje.Estatus = "success";
                        Mensaje.Mensaje = "La operación se completo sin problemas.";
                    }
                    else
                    {
                        //Mensaje.Estatus = "error";
                        //Mensaje.Mensaje = "";
                    }
                }
            }
            catch (Exception Ex)
            {
                Mensaje.Estatus = "error";
                Mensaje.Mensaje = "Informe técnico: " + Ex.Message;
                ErrorSignal.FromCurrentContext().Raise(Ex);
            }
            finally
            {
                Json_Resultado = JsonMapper.ToJson(Mensaje);
            }
            return Json_Resultado;
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string cerrar_sesion()
        {
            Cls_Mensaje Mensaje = new Cls_Mensaje();
            Session.RemoveAll();
            FormsAuthentication.SignOut();
            Mensaje.Estatus = "logout";
            return LitJson.JsonMapper.ToJson(Mensaje);
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string recuperar_password(string jsonObject)
        {
            Cls_Apl_Login Obj_Usuario = null;
            string Json_Resultado = string.Empty;
            Cls_Mensaje Mensaje = new Cls_Mensaje();

            try
            {
                Mensaje.Titulo = "<h3 style='color:#000;'>Recuperar password</h3>";
                Obj_Usuario = LitJson.JsonMapper.ToObject<Cls_Apl_Login>(jsonObject);

                using (var dbContext = new Sistema_TrazabilidadEntities())
                {
                    var _usuarios = from _user in dbContext.Apl_Usuarios
                                    where _user.Email.Equals(Obj_Usuario.Email)
                                    select _user;

                    if (_usuarios.Any())
                    {
                        var _usuario = _usuarios.First();
                        Envia_Mail(_usuario.Email, Cls_Seguridad.Desencriptar(_usuario.Password));
                        Mensaje.Estatus = "success";
                        Mensaje.Mensaje = "<p><i class='fa fa-check' style='color: #00A41E;'></i>&nbsp;Si <b style='color: #000;'>" + Obj_Usuario.Email +
                            "</b> coincide con la dirección de correo electrónico de tu cuenta, te enviaremos un correo con tu password.</p>";
                    }
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
        /// Método para desbloquear pantalla
        /// </summary>
        /// <param name="jsonObject"></param>
        /// <returns></returns>
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string autentificar_bloqueo(string jsonObject)
        {
            Cls_Apl_Login Obj_Usuario = null;
            string Json_Resultado = string.Empty;
            Cls_Mensaje Mensaje = new Cls_Mensaje();

            try
            {
                Mensaje.Titulo = "Autentificación";
                Obj_Usuario = LitJson.JsonMapper.ToObject<Cls_Apl_Login>(jsonObject);
                string pwd = Cls_Seguridad.Encriptar(Obj_Usuario.Password);

                using (var dbContext = new Sistema_TrazabilidadEntities())
                {
                    var _usuarios = from _usuario in dbContext.Apl_Usuarios
                                    join _estatus in dbContext.Tra_Cat_Estatus on _usuario.Estatus_ID equals _estatus.Estatus_ID
                                    join rel in dbContext.Apl_Rel_Usuarios_Roles on _usuario.Usuario_ID equals rel.Usuario_ID
                                    join rol in dbContext.Apl_Roles on rel.Rol_ID equals rol.Rol_ID
                                    where
                                        _estatus.Estatus.Equals("ACTIVO")
                                        && _usuario.Usuario.Equals(Cls_Sesiones.Usuario)
                                        && _usuario.Password.ToString() == pwd
                                    select _usuario;

                    if (_usuarios.Any())
                    {
                        Mensaje.Estatus = "success";
                        Mensaje.Mensaje = "Desbloqueo";
                        Cls_Sesiones.Bloqueo_Pantalla = "NO";

                        FormsAuthentication.Initialize();
                    }
                    else
                    {
                        Cls_Sesiones.Bloqueo_Pantalla = "SI";
                    }
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
        /// Método para guardar en cache bloqueo pantalla
        /// </summary>
        /// <returns></returns>
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string bloqueo_pantalla()
        {
            string Json_Resultado = string.Empty;
            Cls_Mensaje Mensaje = new Cls_Mensaje();

            try
            {
                Mensaje.Estatus = "success";
                Mensaje.Mensaje = "Listo";
                Cls_Sesiones.Bloqueo_Pantalla = "SI";

                FormsAuthentication.Initialize();
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
        /// Método para verificar bloqueo desde otro tab navegador
        /// </summary>
        /// <returns></returns>
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string verificar_bloqueo_sistema()
        {
            string Json_Resultado = string.Empty;
            Cls_Mensaje Mensaje = new Cls_Mensaje();

            try
            {
                Mensaje.Estatus = "success";
                Mensaje.Mensaje = Cls_Sesiones.Bloqueo_Pantalla;

                FormsAuthentication.Initialize();
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

        #region(Metodos)
        internal static Boolean Envia_Mail(string Para, string password)
        {
            MailMessage Correo = new MailMessage(); //obtenemos el objeto del correo
            String Correo_Origen = String.Empty;
            String Host = String.Empty;
            String Contrasenia = String.Empty;
            String Puerto = String.Empty;
            String Asunto = String.Empty;
            String Texto_Correo = String.Empty;
            Boolean Operacion_Completa = false;
            String Adjunto = String.Empty;

            try
            {
                Correo_Origen = ConfigurationManager.AppSettings["Email_From"];
                Contrasenia = ConfigurationManager.AppSettings["Contrasenia_Email"];
                Puerto = ConfigurationManager.AppSettings["Puerto_Email"];
                Host = ConfigurationManager.AppSettings["Host"];
                Asunto = "Recuperar password";
                Texto_Correo = password;

                if (!String.IsNullOrEmpty(Para) && !String.IsNullOrEmpty(Puerto)
                        && !String.IsNullOrEmpty(Correo_Origen)
                        && !String.IsNullOrEmpty(Host) && !String.IsNullOrEmpty(Contrasenia))
                {
                    Correo.To.Clear();
                    Correo.To.Add(Para);
                    Correo.From = new MailAddress(Correo_Origen, "CONTEL", System.Text.Encoding.UTF8);
                    Correo.Subject = Asunto;
                    Correo.SubjectEncoding = System.Text.Encoding.UTF8;

                    if ((!Correo.From.Equals("") || Correo.From != null) && (!Correo.To.Equals("") || Correo.To != null))
                    {
                        Correo.Body = "<html>" +
                                        "<body style=\"font-family:Consolas; font-size:10pt;\"> " +
                                            Texto_Correo + " <br />" +
                                        "</body>" +
                                        "</html>";
                        Correo.BodyEncoding = System.Text.Encoding.UTF8;
                        Correo.IsBodyHtml = true;

                        if (!String.IsNullOrEmpty(Adjunto))
                        {
                            //agregamos el dato adjunto
                            Attachment Data = new Attachment(Adjunto, MediaTypeNames.Application.Octet);
                            // Agrega la informacion del tiempo del archivo.
                            ContentDisposition disposition = Data.ContentDisposition;
                            disposition.DispositionType = DispositionTypeNames.Attachment;
                            // Agrega los archivos adjuntos al mensaje
                            Correo.Attachments.Add(Data);
                        }

                        SmtpClient Cliente_Correo = new SmtpClient();
                        Cliente_Correo.DeliveryMethod = SmtpDeliveryMethod.Network;
                        Cliente_Correo.UseDefaultCredentials = false;
                        Cliente_Correo.Credentials = new System.Net.NetworkCredential(Correo_Origen, Contrasenia);
                        Cliente_Correo.Port = int.Parse(Puerto);
                        Cliente_Correo.Host = Host;
                        Cliente_Correo.EnableSsl = true;

                        System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate (object s,
                          System.Security.Cryptography.X509Certificates.X509Certificate certificate,
                          System.Security.Cryptography.X509Certificates.X509Chain chain,
                          System.Net.Security.SslPolicyErrors sslPolicyErrors)
                        {
                            return true;
                        };

                        Cliente_Correo.Send(Correo);
                        Correo = null;
                        Operacion_Completa = true;
                    }
                    else
                    {
                        Operacion_Completa = false;
                    }
                }
            }
            catch (Exception Ex)
            {
                Operacion_Completa = false;
                ErrorSignal.FromCurrentContext().Raise(Ex);
                //Cls_Jira.Create_Issue(Ex, Cls_Jira.Descripcion_Referencia(Cls_Jira.IssueTypes.Bug), Cls_Jira.Descripcion_Referencia(Cls_Jira.IssuePriority.High));
            }

            return Operacion_Completa;
        }

        internal void ACL(Sistema_TrazabilidadEntities dbContext)
        {
            List<Cls_Apl_Menus_Negocio> Lista_Menus = new List<Cls_Apl_Menus_Negocio>();

            var menus = from _acceso in dbContext.Apl_Accesos
                        join _menu in dbContext.Apl_Menus on _acceso.Menu_ID equals _menu.Menu_ID
                        join _rol in dbContext.Apl_Roles on _acceso.Rol_ID equals _rol.Rol_ID
                        join _estatus in dbContext.Tra_Cat_Estatus on new { a = _acceso.Estatus_ID, b = _menu.Estatus_ID, c = _rol.Estatus_ID }
                        equals new { a = _estatus.Estatus_ID, b = _estatus.Estatus_ID, c = _estatus.Estatus_ID }
                        where _menu.URL_LINK != null && _acceso.Habilitado == "S"
                        select new Cls_Apl_Menus_Negocio{
                            URL_LINK = _menu.URL_LINK
                        };

            if (menus.Any())
            {
                Lista_Menus = menus.ToList<Cls_Apl_Menus_Negocio>();
                Cls_Sesiones.Menu_Control_Acceso = Lista_Menus;
            }            
        }


        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void Consultar_Sucursales_Inicio()
        {
            string _selectParameters = string.Empty;
            NameValueCollection nvc = Context.Request.Form;

            if (!String.IsNullOrEmpty(nvc["datos"]))
                _selectParameters = nvc["datos"].ToString().Trim();


            Cls_Apl_Login Obj_Usuario = null;
            string Json_Resultado = string.Empty;
            List<Cls_Select2> Lista_Sucursal = new List<Cls_Select2>();
              Obj_Usuario = LitJson.JsonMapper.ToObject<Cls_Apl_Login>(_selectParameters);

            try
            {
                using (var dbContext = new Sistema_TrazabilidadEntities())
                {

                    //var Parametros = (from _usuario in dbContext.Apl_Usuarios
                    //                  join _relacion in dbContext.Apl_Rel_Usuarios_Roles on _usuario.Usuario_ID equals _relacion.Usuario_ID
                    //                  where _usuario.Usuario_ID ==
                    //                  dbContext.Apl_Usuarios.Where(user => user.Email.Equals(Obj_Usuario.Usuario)).Select(user => user.Usuario_ID).FirstOrDefault()
                    //                  select new 
                    //                  {
                    //                     Rol_ID=_relacion.Rol_ID,
                    //                     Empresa_ID=_usuario.Empresa_ID 
                    //                  }).FirstOrDefault();


                    //var Sucursales = (from _sucursal in dbContext.Apl_Sucursales
                    //                  join _rol_sucursal in dbContext.Apl_Roles_Sucursales on _sucursal.Sucursal_ID equals _rol_sucursal.Sucursal_ID
                    //                  where _sucursal.Empresa_ID.Equals(Parametros.Empresa_ID)
                    //                  select new Cls_Select2
                    //                  {
                    //                      id = _sucursal.Sucursal_ID.ToString(),
                    //                      text = _sucursal.Nombre 
                    //                  });

                    var Sucursales = (from _usuario in dbContext.Apl_Usuarios
                                      join _usuarios_roles in dbContext.Apl_Rel_Usuarios_Roles
                                      on _usuario.Usuario_ID equals _usuarios_roles.Usuario_ID

                                      join _roles in dbContext.Apl_Roles 
                                        on _usuarios_roles.Rol_ID equals _roles.Rol_ID

                                      join _roles_sucursales in dbContext.Apl_Roles_Sucursales
                                      on new { _usuarios_roles.Rol_ID, Sucursal_ID = _usuarios_roles.Sucursal_ID.Value } equals new { _roles_sucursales.Rol_ID, _roles_sucursales.Sucursal_ID }
                                      join _sucursal in dbContext.Apl_Sucursales
                                      on _roles_sucursales.Sucursal_ID equals _sucursal.Sucursal_ID
                                      where _usuario.Email == Obj_Usuario.Usuario
                                       && _roles.Tipo == "WEB"
                                      select new Cls_Select2
                                      {
                                          id = _sucursal.Sucursal_ID.ToString(),
                                          text = _sucursal.Nombre
                                      });

                    if (Sucursales.Any())
                        foreach (var p in Sucursales)
                            Lista_Sucursal.Add((Cls_Select2)p);

                    Json_Resultado = JsonMapper.ToJson(Lista_Sucursal);
                }
            }
            catch (Exception Ex)
            {
                ErrorSignal.FromCurrentContext().Raise(Ex);
                //Cls_Jira.Create_Issue(Ex, Cls_Jira.Descripcion_Referencia(Cls_Jira.IssueTypes.Bug), Cls_Jira.Descripcion_Referencia(Cls_Jira.IssuePriority.High));
            }
            finally
            {

                Context.Response.Write(string.IsNullOrEmpty(Json_Resultado) ? "[]" : Json_Resultado);

            }
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void AdjuntarImagenes() {
            bool result = false;


        }


        /// <summary>
        /// COnsulta si el usuario logueado tiene permisos para realizar alguna operacion
        /// </summary>
        /// <param name="jsonObject"></param>
        /// <returns></returns>
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string Consultar_Permisos_Procesos(string jsonObject)
        {
            Cls_Permisos_Usuario_Mensaje Obj_Detalles_Requisiones = null;
            string Json_Resultado = string.Empty;
            Cls_Permisos_Usuario_Mensaje Mensaje = new Cls_Permisos_Usuario_Mensaje();

            try
            {
                Obj_Detalles_Requisiones = JsonMapper.ToObject<Cls_Permisos_Usuario_Mensaje>(jsonObject);

                Mensaje = Cls_Seguridad.Validar_Permisos_Usuario(Obj_Detalles_Requisiones.Proceso);

            }
            catch (Exception Ex)
            {
                //Mensaje
                ErrorSignal.FromCurrentContext().Raise(Ex);
                Json_Resultado = JsonMapper.ToJson("{}");
            }
            finally
            {
                Json_Resultado = JsonConvert.SerializeObject(Mensaje);
            }
            return Json_Resultado;
        }


        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string Crear_Notificacion(string jsonObject)
        {
            Cls_Datos_Notificacion_Negocio obj = null;
            string Json_Resultado = string.Empty;
            Cls_Permisos_Usuario_Mensaje Mensaje = new Cls_Permisos_Usuario_Mensaje();

            try
            {
                obj = JsonMapper.ToObject<Cls_Datos_Notificacion_Negocio>(jsonObject);

                //Notificaciones_Controller.Generar_Notificacion(obj.List_Usuario, obj.Url_Notificacion, obj.Icono, obj.Tipo_Notificacion_ID, obj.Mensaje);
                    

            }
            catch (Exception Ex)
            {
                //Mensaje
                ErrorSignal.FromCurrentContext().Raise(Ex);
                Json_Resultado = JsonMapper.ToJson("{}");
            }
            finally
            {
                Json_Resultado = JsonConvert.SerializeObject(Mensaje);
            }
            return Json_Resultado;
        }



        #endregion
    }

}
