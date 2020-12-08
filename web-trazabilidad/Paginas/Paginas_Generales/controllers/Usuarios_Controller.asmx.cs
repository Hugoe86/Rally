using datos_trazabilidad;
using Elmah;
using LitJson;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using web_trazabilidad.Models.Ayudante;
using web_trazabilidad.Models.Negocio;

namespace web_trazabilidad.Paginas.Catalogos.controller
{
    /// <summary>
    /// Summary description for Usuarios_Controller
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class Usuarios_Controller : System.Web.Services.WebService
    {
        #region Metodos

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string Consultar_Usuarios_Por_Nombre(string jsonObject)
        {
            Cls_Usuarios_Negocio ObjUsuarios = null;
            string Json_Resultado = string.Empty;
            List<Cls_Usuarios_Negocio> Lista_Usuarios = new List<Cls_Usuarios_Negocio>();
            Cls_Mensaje Mensaje = new Cls_Mensaje();

            try
            {
                Mensaje.Titulo = "Validaciones";
                ObjUsuarios = JsonMapper.ToObject<Cls_Usuarios_Negocio>(jsonObject);

                using (var dbContext = new Sistema_TrazabilidadEntities())
                {
                    var _usuarios = (from _select in dbContext.Apl_Usuarios
                                     where
                                     _select.Usuario.Equals(ObjUsuarios.Usuario)
                                     || _select.Email.Equals(ObjUsuarios.Email)
                                     select new Cls_Usuarios_Negocio
                                     {
                                         Usuario_ID = _select.Usuario_ID,
                                         Usuario = _select.Usuario,
                                         Email = _select.Email
                                     }).OrderByDescending(u => u.Usuario_ID);

                    if (_usuarios.Any())
                    {
                        if (ObjUsuarios.Usuario_ID == 0)
                        {
                            Mensaje.Estatus = "error";
                            if (!string.IsNullOrEmpty(ObjUsuarios.Usuario))
                                Mensaje.Mensaje = "El usuario ingresado ya se encuentra registrado.";
                            else if (!string.IsNullOrEmpty(ObjUsuarios.Email))
                                Mensaje.Mensaje = "El email ingresado ya se encuentra registrado.";
                        }
                        else
                        {
                            var item_edit = _usuarios.Where(u => u.Usuario_ID == ObjUsuarios.Usuario_ID);

                            if (item_edit.Count() == 1)
                                Mensaje.Estatus = "success";
                            else
                            {
                                Mensaje.Estatus = "error";
                                if (!string.IsNullOrEmpty(ObjUsuarios.Usuario))
                                    Mensaje.Mensaje = "El usuario ingresado ya se encuentra registrado.";
                                else if (!string.IsNullOrEmpty(ObjUsuarios.Email))
                                    Mensaje.Mensaje = "El email ingresado ya se encuentra registrado.";
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

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string Consultar_Usuarios_Por_Filtros(string jsonObject)
        {
            Cls_Usuarios_Negocio objUsuarios = null;
            string Json_Resultado = string.Empty;
            List<Cls_Usuarios_Negocio> Lista_usuarios = new List<Cls_Usuarios_Negocio>();
            int empresa = string.IsNullOrEmpty(Cls_Sesiones.Empresa_ID) ? -1 : Convert.ToInt32(Cls_Sesiones.Empresa_ID);
            int Sucursal = String.IsNullOrEmpty(Cls_Sesiones.Sucursal_ID) ? -1 : Convert.ToInt32(Cls_Sesiones.Sucursal_ID);
            try
            {
                objUsuarios = JsonMapper.ToObject<Cls_Usuarios_Negocio>(jsonObject);

                using (var dbContext = new Sistema_TrazabilidadEntities())
                {
                    var _usuarios = (from _select in dbContext.Apl_Usuarios
                                         //join _rol in dbContext.Apl_Rel_Usuarios_Roles on _select.Usuario_ID equals _rol.Usuario_ID
                                     where _select.Empresa_ID.Equals(empresa) &&
                                            _select.Sucursal_ID.Equals(Sucursal) &&
                                     (!string.IsNullOrEmpty(objUsuarios.Usuario) ? _select.Usuario.ToLower().Contains(objUsuarios.Usuario.ToLower()) : true) &&
                                     ((objUsuarios.Estatus_ID != 0) ? _select.Estatus_ID.Equals(objUsuarios.Estatus_ID) : true)
                                     select new Cls_Usuarios_Negocio
                                     {
                                         Usuario_ID = _select.Usuario_ID,
                                         Empresa_ID = _select.Empresa_ID,
                                         Tipo_Usuario_ID = _select.Tipo_Usuario_ID,
                                         Estatus_ID = _select.Estatus_ID,
                                         Usuario = _select.Usuario,
                                         Password = _select.Password,
                                         Email = _select.Email,
                                         Nombre = _select.Nombre,
                                         //Rol_ID = _rol.Rol_ID,
                                         //Rel_Usuarios_Rol_ID = _rol.Rel_Usuario_Rol_ID


                                     }).OrderByDescending(u => u.Usuario_ID);

                    foreach (var p in _usuarios)
                    {
                        p.Password = Cls_Seguridad.Desencriptar(p.Password);
                        Lista_usuarios.Add((Cls_Usuarios_Negocio)p);
                    }


                    Json_Resultado = JsonMapper.ToJson(Lista_usuarios);
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
        public string Alta(string jsonObject)
        {
            Cls_Usuarios_Negocio ObjUsuarios = null;
            Cls_Usuarios_Negocio ObjRol = null;
            string Json_Resultado = string.Empty;
            Cls_Mensaje Mensaje = new Cls_Mensaje();

            try
            {
                Mensaje.Titulo = "Alta registro";
                ObjUsuarios = JsonMapper.ToObject<Cls_Usuarios_Negocio>(jsonObject);
                ObjRol = JsonMapper.ToObject<Cls_Usuarios_Negocio>(jsonObject);
                int Empleado_ID = !string.IsNullOrEmpty(ObjUsuarios.Empleado_ID) ? Convert.ToInt32(ObjUsuarios.Empleado_ID) : -1;
                int Empresa_ID = Convert.ToInt32(Cls_Sesiones.Empresa_ID);
                int Sucursal_ID = Convert.ToInt32(Cls_Sesiones.Sucursal_ID);
                DateTime date = DateTime.Now.AddMonths(5);


                using (var dbContext_Sucursal = new Sistema_TrazabilidadEntities())
                {
                    using (var dbContext = new Sistema_TrazabilidadEntities())
                    {
                        var _usuarios = new Apl_Usuarios();

                        _usuarios.Empresa_ID = Convert.ToInt32(Cls_Sesiones.Empresa_ID);
                        _usuarios.Sucursal_ID = Convert.ToInt32(Cls_Sesiones.Sucursal_ID);
                        _usuarios.Estatus_ID = ObjUsuarios.Estatus_ID;
                        _usuarios.Tipo_Usuario_ID = ObjUsuarios.Tipo_Usuario_ID;
                        _usuarios.Usuario = ObjUsuarios.Usuario;
                        _usuarios.Password = Cls_Seguridad.Encriptar(ObjUsuarios.Password);
                        _usuarios.No_Intentos_Recuperar = "9";
                        _usuarios.Email = ObjUsuarios.Email;
                        _usuarios.Fecha_Expira_Contrasenia = date;
                        _usuarios.Usuario_Creo = Cls_Sesiones.Usuario;
                        _usuarios.Fecha_Creo = new DateTime?(DateTime.Now).Value;
                        _usuarios.Fecha_Token = date;
                        _usuarios.Nombre = ObjUsuarios.Nombre;
                        dbContext.Apl_Usuarios.Add(_usuarios);
                        
                        dbContext.SaveChanges();

                      
                        Mensaje.Estatus = "success";
                        Mensaje.Mensaje = "La operación se completo sin problemas.";
                    }
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
        public string Actualizar(string jsonObject)
        {
            Cls_Usuarios_Negocio ObjUsuarios = null;
            Cls_Apl_Rel_Usuarios_Roles_Negocio ObjRol = null;
            string Json_Resultado = string.Empty;
            Cls_Mensaje Mensaje = new Cls_Mensaje();

            try
            {
                Mensaje.Titulo = "Actualizar registro";
                ObjUsuarios = JsonMapper.ToObject<Cls_Usuarios_Negocio>(jsonObject);
                ObjRol = JsonMapper.ToObject<Cls_Apl_Rel_Usuarios_Roles_Negocio>(jsonObject);
                string Password = Cls_Seguridad.Encriptar(ObjUsuarios.Password);
                int Empleado_ID = !string.IsNullOrEmpty(ObjUsuarios.Empleado_ID) ? Convert.ToInt32(ObjUsuarios.Empleado_ID) : -1;
                int Empresa_ID = Convert.ToInt32(Cls_Sesiones.Empresa_ID);
                int Sucursal_ID = Convert.ToInt32(Cls_Sesiones.Sucursal_ID);

                using (var dbContext = new Sistema_TrazabilidadEntities())
                {
                    var _usuarios = dbContext.Apl_Usuarios.Where(u => u.Usuario_ID == ObjUsuarios.Usuario_ID).First();

                    _usuarios.Estatus_ID = ObjUsuarios.Estatus_ID;
                    _usuarios.Tipo_Usuario_ID = ObjUsuarios.Tipo_Usuario_ID;
                    _usuarios.Usuario = ObjUsuarios.Usuario;
                    _usuarios.Password = Password;
                    _usuarios.Email = ObjUsuarios.Email;
                    _usuarios.Usuario_Modifico = Cls_Sesiones.Usuario;
                    _usuarios.Fecha_Modifico = new DateTime?(DateTime.Now);
                    _usuarios.Nombre = ObjUsuarios.Nombre;
                    
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
        public string Eliminar(string jsonObject)
        {
            Cls_Usuarios_Negocio ObjUsuarios = null;
            Cls_Apl_Rel_Usuarios_Roles_Negocio ObjRol = null;
            string Json_Resultado = string.Empty;
            Cls_Mensaje Mensaje = new Cls_Mensaje();

            try
            {
                Mensaje.Titulo = "Eliminar registro";
                ObjUsuarios = JsonMapper.ToObject<Cls_Usuarios_Negocio>(jsonObject);
                ObjRol = JsonMapper.ToObject<Cls_Apl_Rel_Usuarios_Roles_Negocio>(jsonObject);

                using (var dbContext = new Sistema_TrazabilidadEntities())
                {
                    //var _empleado = dbContext.Tra_Cat_Empleados.Where(e => e.Usuario_ID == ObjUsuarios.Usuario_ID).First();
                    //_empleado.Usuario_ID = null;
                    //_empleado.Usuario_Modifico = Cls_Sesiones.Datos_Usuario.Usuario;
                    //_empleado.Fecha_Modifico = new DateTime?(DateTime.Now).Value;
                    //dbContext.SaveChanges();


                    var _roles = dbContext.Apl_Rel_Usuarios_Roles.Where(a => a.Usuario_ID == ObjRol.Usuario_ID);
                    if (_roles.Any())
                    {
                        var _rol = dbContext.Apl_Rel_Usuarios_Roles.Where(a => a.Usuario_ID == ObjRol.Usuario_ID).First();
                        dbContext.Apl_Rel_Usuarios_Roles.Remove(_rol);
                        dbContext.SaveChanges();
                    }

                    var _usuarios = dbContext.Apl_Usuarios.Where(u => u.Usuario_ID == ObjUsuarios.Usuario_ID).First();
                    dbContext.Apl_Usuarios.Remove(_usuarios);

                    dbContext.SaveChanges();
                    Mensaje.Estatus = "success";
                    Mensaje.Mensaje = "La operación se completo sin problemas.";
                }
            }
            catch (Exception Ex)
            {
                Mensaje.Titulo = "Informe Técnico";
                Mensaje.Estatus = "error";
                if (Ex.InnerException.InnerException.Message.Contains("The DELETE statement conflicted with the REFERENCE constraint"))
                    Mensaje.Mensaje =
                        "La operación de eliminar el registro fue revocada. <br /><br />" +
                        "<i class='fa fa-angle-double-right' ></i>&nbsp;&nbsp; El registro que intenta eliminar ya se encuentra en uso.";
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
        public string ConsultarFiltroEstatus()
        {
            string Json_Resultado = string.Empty;
            List<Tra_Cat_Estatus> Lista_filtro = new List<Tra_Cat_Estatus>();

            try
            {

                using (var dbContext = new Sistema_TrazabilidadEntities())
                {
                    var estatus = from _estatus in dbContext.Tra_Cat_Estatus
                                  select new { _estatus.Estatus_ID, _estatus.Estatus };


                    Json_Resultado = JsonMapper.ToJson(estatus.ToList());


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
        public string ConsultarEstatus()
        {
            string Json_Resultado = string.Empty;
            List<Tra_Cat_Estatus> Lista_estatus = new List<Tra_Cat_Estatus>();

            try
            {

                using (var dbContext = new Sistema_TrazabilidadEntities())
                {
                    var Estatus = from _select in dbContext.Tra_Cat_Estatus
                                  select new { _select.Estatus, _select.Estatus_ID };
                    Json_Resultado = JsonMapper.ToJson(Estatus.ToList());
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
        public string ConsultarTipoUsuario()
        {
            string Json_Resultado = string.Empty;
            List<Apl_Tipos_Usuarios> Lista_tipo_usuario = new List<Apl_Tipos_Usuarios>();

            try
            {

                using (var dbContext = new Sistema_TrazabilidadEntities())
                {
                    var tipo_usuario = from _select in dbContext.Apl_Tipos_Usuarios
                                       select new { _select.Tipo_Usuario_ID, _select.Nombre };
                    Json_Resultado = JsonMapper.ToJson(tipo_usuario.ToList());
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
        public string ConsultarRolS()
        {
            string Json_Resultado = string.Empty;
            List<Apl_Roles> Lista_tipo_usuario = new List<Apl_Roles>();
            int sucursal_ID;
            int empresa = Convert.ToInt32(Cls_Sesiones.Empresa_ID);
            int Sucursal = String.IsNullOrEmpty(Cls_Sesiones.Sucursal_ID) ? -1 : Convert.ToInt32(Cls_Sesiones.Sucursal_ID);
            try
            {

                using (var dbContext_Sucursal = new Sistema_TrazabilidadEntities())
                {
                    //var _sucursal = dbContext_Sucursal.Apl_Sucursales.First();
                    //sucursal_ID = _sucursal.Sucursal_ID;

                    using (var dbContext = new Sistema_TrazabilidadEntities())
                    {
                        var rol = from _rolesucursales in dbContext.Apl_Roles_Sucursales
                                  join _select in dbContext.Apl_Roles on _rolesucursales.Rol_ID equals _select.Rol_ID

                                  where (_select.Sucursal_ID.Equals(Sucursal))
                                  select new { _select.Rol_ID, _select.Nombre };
                        Json_Resultado = JsonMapper.ToJson(rol.ToList());
                    }

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
        public void ConsultarRol()
        {
            string Json_Resultado = string.Empty;
            Cls_Mensaje Mensaje = new Cls_Mensaje();
            List<Cls_Select2> Lst_Roles = new List<Cls_Select2>();

            try
            {
                string q = string.Empty;
                string sucursal = string.Empty;
                NameValueCollection nvc = Context.Request.Form;

                if (!String.IsNullOrEmpty(nvc["q"]))
                    q = nvc["q"].ToString().Trim();
                if (!String.IsNullOrEmpty(nvc["sucursal"]))
                    sucursal = nvc["sucursal"].ToString().Trim();

                using (var dbContext = new Sistema_TrazabilidadEntities())
                {
                    var _roles = from _rol in dbContext.Apl_Roles
                                 join _rs in dbContext.Apl_Roles_Sucursales on _rol.Rol_ID equals _rs.Rol_ID

                                 where _rs.Empresa_ID.ToString() == Cls_Sesiones.Empresa_ID && _rs.Sucursal_ID.ToString() == sucursal
                                 select new Cls_Select2
                                 {
                                     id = _rol.Rol_ID.ToString(),
                                     text = _rol.Tipo + " - " + _rol.Nombre,
                                     detalle_1 = _rol.Tipo,
                                     detalle_2 = _rs.Sucursal_ID.ToString(),
                                     tag = String.Empty
                                 };

                    if (_roles.Any())
                        foreach (var p in _roles)
                            Lst_Roles.Add((Cls_Select2)p);

                    Json_Resultado = JsonMapper.ToJson(Lst_Roles);
                }
            }
            catch (Exception Ex)
            {
                Mensaje.Estatus = "error";
                Mensaje.Mensaje = "<i class='fa fa-times' style='color: #FF0004;'></i>&nbsp;Informe técnico: " + Ex.Message;
                // Cls_Jira.Create_Issue(Ex, Cls_Jira.Descripcion_Referencia(Cls_Jira.IssueTypes.Bug), Cls_Jira.Descripcion_Referencia(Cls_Jira.IssuePriority.High));
                ErrorSignal.FromCurrentContext().Raise(Ex);
            }
            finally
            {
                Context.Response.Write(Json_Resultado);
            }
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void Consultar_Sucursales()
        {
            string Json_Resultado = string.Empty;
            Cls_Mensaje Mensaje = new Cls_Mensaje();
            List<Cls_Select2> Lst_Sucursales = new List<Cls_Select2>();

            try
            {
                string q = string.Empty;
                NameValueCollection nvc = Context.Request.Form;

                if (!String.IsNullOrEmpty(nvc["q"]))
                    q = nvc["q"].ToString().Trim();

                using (var dbContext = new Sistema_TrazabilidadEntities())
                {
                    var _sucursales = from _sucursal in dbContext.Apl_Sucursales


                                      where _sucursal.Empresa_ID.ToString() == Cls_Sesiones.Empresa_ID
                                      select new Cls_Select2
                                      {
                                          id = _sucursal.Sucursal_ID.ToString(),
                                          text = _sucursal.Nombre,

                                          tag = String.Empty
                                      };

                    if (_sucursales.Any())
                        foreach (var p in _sucursales)
                            Lst_Sucursales.Add((Cls_Select2)p);

                    Json_Resultado = JsonMapper.ToJson(Lst_Sucursales);
                }
            }
            catch (Exception Ex)
            {
                Mensaje.Estatus = "error";
                Mensaje.Mensaje = "<i class='fa fa-times' style='color: #FF0004;'></i>&nbsp;Informe técnico: " + Ex.Message;
                // Cls_Jira.Create_Issue(Ex, Cls_Jira.Descripcion_Referencia(Cls_Jira.IssueTypes.Bug), Cls_Jira.Descripcion_Referencia(Cls_Jira.IssuePriority.High));
                ErrorSignal.FromCurrentContext().Raise(Ex);
            }
            finally
            {
                Context.Response.Write(Json_Resultado);
            }
        }


        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string Consultar_Roles_Usuarios(string jsonObject)
        {
            string Json_Rsp = "[]";
            Cls_Apl_Rel_Usuarios_Roles_Negocio Usuario_Roles = new Cls_Apl_Rel_Usuarios_Roles_Negocio();
            try
            {
                Usuario_Roles = JsonMapper.ToObject<Cls_Apl_Rel_Usuarios_Roles_Negocio>(jsonObject);

                using (var dbContext = new Sistema_TrazabilidadEntities())
                {
                    var _roles_usuarios = from _rel in dbContext.Apl_Rel_Usuarios_Roles
                                          join _rol in dbContext.Apl_Roles on _rel.Rol_ID equals _rol.Rol_ID
                                          join _usuario in dbContext.Apl_Usuarios on _rel.Usuario_ID equals _usuario.Usuario_ID
                                          join _sucursal in dbContext.Apl_Sucursales on _rel.Sucursal_ID equals _sucursal.Sucursal_ID
                                          where _rel.Usuario_ID == Usuario_Roles.Usuario_ID
                                          select new
                                          {
                                              Usuario_ID = _rel.Usuario_ID,
                                              Rol_ID = _rel.Rol_ID,
                                              Rol = _rol.Nombre,
                                              Usuario = _usuario.Usuario,
                                              Sucursal_ID = _sucursal.Sucursal_ID,
                                              Sucursal = _sucursal.Nombre,
                                              Tipo = _rol.Tipo,
                                              Rel_Usuario_Rol_ID = _rel.Rel_Usuario_Rol_ID
                                          };

                    Json_Rsp = JsonMapper.ToJson(_roles_usuarios.ToList());
                }
            }
            catch (Exception ex)
            {

            }
            return Json_Rsp;
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string Alta_Rol_Usuario(string jsonObject)
        {
            Cls_Mensaje Mensaje = new Cls_Mensaje();
            Cls_Apl_Rel_Usuarios_Roles_Negocio Rel = new Cls_Apl_Rel_Usuarios_Roles_Negocio();
            try
            {
                Mensaje.Titulo = "Alta de roles-usuario";
                Rel = JsonMapper.ToObject<Cls_Apl_Rel_Usuarios_Roles_Negocio>(jsonObject);
                using (var dbContext = new Sistema_TrazabilidadEntities())
                {
                    Apl_Rel_Usuarios_Roles Rel_Usuario_Rol = new Apl_Rel_Usuarios_Roles();
                    Rel_Usuario_Rol.Usuario_ID = Rel.Usuario_ID;
                    Rel_Usuario_Rol.Rol_ID = Rel.Rol_ID;
                    Rel_Usuario_Rol.Empresa_ID = Convert.ToInt32(Cls_Sesiones.Empresa_ID);
                    Rel_Usuario_Rol.Sucursal_ID = Rel.Sucursal_ID;

                    dbContext.Apl_Rel_Usuarios_Roles.Add(Rel_Usuario_Rol);
                    dbContext.SaveChanges();

                    Mensaje.Estatus = "success";
                    Mensaje.Mensaje = "Alta exitosa.";

                }
            }
            catch (Exception ex)
            {
                Mensaje.Estatus = "error";
                Mensaje.Mensaje = "Error al intentar dar de alta el rol";
            }

            return JsonMapper.ToJson(Mensaje);
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string Eliminar_Relacion_Usuario_Rol(string jsonObject)
        {
            Cls_Apl_Rel_Usuarios_Roles_Negocio Rel = new Cls_Apl_Rel_Usuarios_Roles_Negocio();
            Cls_Mensaje Mensaje = new Cls_Mensaje();
            try
            {
                Mensaje.Titulo = "Eliminar Registro";
                Rel = JsonMapper.ToObject<Cls_Apl_Rel_Usuarios_Roles_Negocio>(jsonObject);
                using (var dbContext = new Sistema_TrazabilidadEntities())
                {
                    var _roles_usuario = dbContext.Apl_Rel_Usuarios_Roles.Where(u => u.Rel_Usuario_Rol_ID == Rel.Rel_Usuario_Rol_ID).First();
                    dbContext.Apl_Rel_Usuarios_Roles.Remove(_roles_usuario);

                    dbContext.SaveChanges();
                    Mensaje.Estatus = "success";
                    Mensaje.Mensaje = "La operación se completo sin problemas.";
                }
            }
            catch (Exception ex)
            {
                Mensaje.Estatus = "error";
                Mensaje.Mensaje = "Error al eliminar la relación. " + ex.Message;
            }
            return JsonMapper.ToJson(Mensaje);
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void Consultar_Empleados()
        {
            string Json_Resultado = string.Empty;
            List<Cls_Select2> Lst_Combo = new List<Cls_Select2>();

            try
            {
                string q = string.Empty;
                string usuario_id = string.Empty;

                NameValueCollection nvc = Context.Request.Form;

                if (!String.IsNullOrEmpty(nvc["q"]))
                    q = nvc["q"].ToString().Trim();


                if (!String.IsNullOrEmpty(nvc["id_usuario"]))
                    usuario_id = nvc["id_usuario"].ToString().Trim();

                int Empresa_ID = Convert.ToInt32(Cls_Sesiones.Empresa_ID);
                int Sucursal_ID = Convert.ToInt32(Cls_Sesiones.Sucursal_ID);
                int Usuario_ID = !string.IsNullOrEmpty(usuario_id) ? Convert.ToInt32(usuario_id) : -1;

                //using (var dbContext = new Sistema_TrazabilidadEntities())
                //{
                //    var Empleados = (from _empleado in dbContext.Tra_Cat_Empleados
                //                     where
                //                    (string.IsNullOrEmpty(q) ? true : _empleado.Nombre.Contains(q))
                //                     && _empleado.Empresa_ID == Empresa_ID
                //                     && _empleado.Sucursal_ID == Sucursal_ID
                //                     && (string.IsNullOrEmpty(usuario_id) ? _empleado.Usuario_ID == null : (_empleado.Usuario_ID == null || _empleado.Usuario_ID == Usuario_ID))
                //                     select new Cls_Select2
                //                     {
                //                         id = _empleado.Empleado_ID.ToString(),
                //                         text = _empleado.Nombre + " " + _empleado.Apellido_Paterno + " " + _empleado.Apellido_Materno
                //                     });

                //    foreach (var p in Empleados)
                //        Lst_Combo.Add(p);

                //    Json_Resultado = JsonMapper.ToJson(Lst_Combo);
                //}
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

