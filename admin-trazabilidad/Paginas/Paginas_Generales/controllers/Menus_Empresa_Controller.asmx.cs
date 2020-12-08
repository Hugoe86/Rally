using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using datos_trazabilidad;
using LitJson;
using admin_trazabilidad.Models.Negocio;
using admin_trazabilidad.Models.Ayudante;
using System.Collections.Specialized;
using Newtonsoft.Json;

namespace admin_trazabilidad.Paginas.Paginas_Generales.controllers
{
    /// <summary>
    /// Summary description for Menus_Empresa_Controller
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class Menus_Empresa_Controller : System.Web.Services.WebService
    {

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string Consultar_Menus(string jsonObject) {
            List<Cls_Apl_Menus_Empresa_Negocio> lst = new List<Cls_Apl_Menus_Empresa_Negocio>();
            Cls_Apl_Menus_Empresa_Negocio objmenuemp = new Cls_Apl_Menus_Empresa_Negocio();
            string Json_Resultado = "[]";
            try {
                objmenuemp = JsonMapper.ToObject<Cls_Apl_Menus_Empresa_Negocio>(jsonObject);
                using (var dbContext = new Sistema_TrazabilidadEntities()) {
                    var select_menus = (from _menu in dbContext.Apl_Menus
                                        where _menu.Parent_ID == "0"
                                        select new Cls_Apl_Menus_Empresa_Negocio
                                        {
                                            Menu_Descripcion = _menu.Menu_Descripcion,
                                            Menu_ID = _menu.Menu_ID,
                                            Parent_ID = _menu.Parent_ID.Trim(),
                                            Menu_Empresa_ID = (from _menu_emp in dbContext.Apl_Menus_Empresa
                                                               where _menu_emp.Empresa_ID == objmenuemp.Empresa_ID && _menu_emp.Menu_ID == _menu.Menu_ID
                                                               select _menu_emp.Menu_Empresa_ID).FirstOrDefault()
                                        });

                    foreach(var _menu in select_menus.ToList())
                    {
                        var select_submenus = (from _submenu in dbContext.Apl_Menus
                                            where _submenu.Parent_ID == _menu.Menu_ID.ToString()
                                            select new Cls_Apl_Menus_Empresa_Negocio
                                            {
                                                Menu_Descripcion = _submenu.Menu_Descripcion,
                                                Menu_ID = _submenu.Menu_ID,
                                                Parent_ID = _submenu.Parent_ID.Trim(),
                                                Menu_Empresa_ID = (from _menu_emp in dbContext.Apl_Menus_Empresa
                                                                   where _menu_emp.Empresa_ID == objmenuemp.Empresa_ID && _menu_emp.Menu_ID == _submenu.Menu_ID
                                                                   select _menu_emp.Menu_Empresa_ID).FirstOrDefault()
                                            }).OrderBy(x => x.Menu_ID);
                        lst.Add(_menu);
                        foreach(var submenu in select_submenus.ToList())
                        {
                            lst.Add(submenu);
                        }
                    }


                    Json_Resultado = JsonMapper.ToJson(lst);
                }

            } catch (Exception Ex) {

            }
           
            return Json_Resultado;
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void Consultar_Empresas() {

            string Json_Resultado = string.Empty;
            List<Cls_Select2> Lista_Clientes = new List<Cls_Select2>();

            try
            {
                string q = string.Empty;
                NameValueCollection nvc = Context.Request.Form;

                if (!String.IsNullOrEmpty(nvc["q"]))
                    q = nvc["q"].ToString().Trim();

                using (var dbContext = new Sistema_TrazabilidadEntities())
                {

                    var lst_cliente = from _cliente in dbContext.Apl_Empresas
                                      join _estatus in dbContext.Tra_Cat_Estatus on _cliente.Estatus_ID equals _estatus.Estatus_ID
                                      where _estatus.Estatus == "ACTIVO"
                                      && (_cliente.Nombre.Contains(q) || _cliente.Clave.Contains(q))
                                      select new Cls_Select2
                                      {
                                          id = _cliente.Empresa_ID.ToString(),
                                          text = _cliente.Nombre,
                                          tag = String.Empty,
                                          detalle_1 = _cliente.Clave

                                      };

                    if (lst_cliente.Any())
                    {
                        foreach (var p in lst_cliente)
                            Lista_Clientes.Add(p);
                    }

                    Json_Resultado = JsonMapper.ToJson(Lista_Clientes);
                }
            }
            catch (Exception Ex)
            {
            }
            finally
            {
                Context.Response.Write(Json_Resultado);
            }
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string Guardar(string jsonObject)
        {
            Cls_Apl_Menus_Empresa_Negocio obj = new Cls_Apl_Menus_Empresa_Negocio();
            List<Cls_Apl_Menus_Empresa_Negocio> lst = new List<Cls_Apl_Menus_Empresa_Negocio>();
            Cls_Mensaje Mensaje = new Cls_Mensaje();
            obj = JsonMapper.ToObject<Cls_Apl_Menus_Empresa_Negocio>(jsonObject);
            lst = JsonConvert.DeserializeObject<List<Cls_Apl_Menus_Empresa_Negocio>>(obj.datos);
            string Json_Resultado = string.Empty;
            try
            {
                Mensaje.Titulo = "Alta";        
                using (var dbContext = new Sistema_TrazabilidadEntities())
                {
                    Apl_Menus_Empresa menu = new Apl_Menus_Empresa();
                    var lst_nuevos = lst.Where(x => x.Menu_Empresa_ID == 0 && x.chk == true).ToList();
                    var lst_eliminados = lst.Where(x => x.Menu_Empresa_ID != 0 && x.chk == false).ToList();

                    var roles = from _roles in dbContext.Apl_Roles
                                where _roles.Empresa_ID == obj.Empresa_ID
                                select _roles;

                    foreach (var i in lst_nuevos)
                    {
                        menu.Empresa_ID = obj.Empresa_ID;
                        menu.Menu_ID = i.Menu_ID;
                        menu.Usuario_Creo = Cls_Sesiones.Usuario;
                        menu.Fecha_Creo = DateTime.Now;

                        dbContext.Apl_Menus_Empresa.Add(menu);
                        var roles_administradores = roles.ToList().Where(x => x.Default_Admin_Empresa == "S");
                        foreach (var rol in roles_administradores)
                        {
                            var sel = dbContext.Apl_Accesos.Where(x => x.Menu_ID == i.Menu_ID && x.Rol_ID == rol.Rol_ID);
                            if (sel.Any())
                                dbContext.Apl_Accesos.Remove(sel.First());

                            var _roles_accesos = new Apl_Accesos();
                            _roles_accesos.Rol_ID = rol.Rol_ID;
                            _roles_accesos.Menu_ID = i.Menu_ID;
                            _roles_accesos.Estatus_ID = dbContext.Tra_Cat_Estatus.Where(x => x.Estatus == "ACTIVO").Select(x => x.Estatus_ID).FirstOrDefault();
                            _roles_accesos.Habilitado = "S";
                            _roles_accesos.Alta = "S";
                            _roles_accesos.Cambio = "S";
                            _roles_accesos.Eliminar = "S";
                            _roles_accesos.Consultar = "S";
                            _roles_accesos.Usuario_Creo = Cls_Sesiones.Usuario;
                            _roles_accesos.Fecha_Creo = new DateTime?(DateTime.Now).Value;
                            dbContext.Apl_Accesos.Add(_roles_accesos);
                            dbContext.SaveChanges();
                        }
                        dbContext.SaveChanges();
                    }
                    foreach (var e in lst_eliminados)
                    {
                        foreach (var acc in roles)
                        {
                            var _access = dbContext.Apl_Accesos.Where(x => x.Menu_ID == e.Menu_ID && x.Rol_ID == acc.Rol_ID);
                            if (_access.Any())
                                dbContext.Apl_Accesos.Remove(_access.First());
                        }

                        var _clientes = dbContext.Apl_Menus_Empresa.Where(u => u.Menu_Empresa_ID == e.Menu_Empresa_ID);
                        if (_clientes.Any())
                            dbContext.Apl_Menus_Empresa.Remove(_clientes.First());

                        dbContext.SaveChanges();
                    }
               
                    Mensaje.Estatus = "success";
                    Mensaje.Mensaje = "Alta Existosa";
                }
            }
            catch (Exception Ex)
            {
                Mensaje.Estatus = "error";
                Mensaje.Mensaje = Ex.Message;
            }
            finally
            {
               Json_Resultado = JsonMapper.ToJson(Mensaje);
            }
            return Json_Resultado;
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string Consultar_Menus_Empresas(string jsonObject)
        {
            string Json_Resultado = "[]";
            Cls_Apl_Menus_Empresa_Negocio objmenuemp = new Cls_Apl_Menus_Empresa_Negocio();
            try
            {
                objmenuemp = JsonMapper.ToObject<Cls_Apl_Menus_Empresa_Negocio>(jsonObject);
                using (var dbContext = new Sistema_TrazabilidadEntities())
                {
                    var select_menus = (from _menu in dbContext.Apl_Menus
                                        select new Cls_Apl_Menus_Empresa_Negocio
                                        {
                                            Menu_Descripcion = _menu.Menu_Descripcion,
                                            //Nombre_Mostrar = _menu.Nombre_Mostrar,
                                            Menu_ID = _menu.Menu_ID,
                                            //URL_LINK = _menu.URL_LINK,
                                            Menu_Empresa_ID = (from _menu_emp in dbContext.Apl_Menus_Empresa
                                                                              where _menu_emp.Empresa_ID == objmenuemp.Empresa_ID && _menu_emp.Menu_ID == _menu.Menu_ID
                                                                              select _menu_emp.Menu_Empresa_ID).FirstOrDefault()
                                        });
                    Json_Resultado = JsonMapper.ToJson(select_menus.ToList());
                }

            }
            catch (Exception Ex)
            {

            }

            return Json_Resultado;
        }
    }
}
