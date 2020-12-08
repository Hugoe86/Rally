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
using web_trazabilidad.Models.Negocio.Generales;

namespace web_trazabilidad.Paginas.Paginas_Generales.controllers
{
    /// <summary>
    /// Summary description for Ctrl_Apl_Roles
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class Ctrl_Apl_Roles : System.Web.Services.WebService
    {
        /// <summary>
        /// metodo para la consulta de los datos del catalogo
        /// </summary>
        /// <param name="Parametros">Parametros de la consulta</param>
        /// <returns>json serializado con datos </returns>
        /// <creo>Juan Alberto Hernandez Negrete</creo>
        /// <fecha_creo>25-Mayo-2017</fecha_creo>
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string Consultar_Registros(string Parametros)
        {
            Cls_Apl_Roles_Negocio Obj_ = new Cls_Apl_Roles_Negocio();
            List<Cls_Apl_Roles_Negocio> Lista_ = new List<Cls_Apl_Roles_Negocio>();
            string Json_Resultado = string.Empty;
            Cls_Mensaje Mensaje = new Cls_Mensaje();

            try
            {
                Obj_ = JsonConvert.DeserializeObject<Cls_Apl_Roles_Negocio>(Parametros);

                using (var dbContext = new Sistema_TrazabilidadEntities())
                {
                    var Roles = (from _Roles in dbContext.Apl_Roles
                                 join _Estatus in dbContext.Tra_Cat_Estatus on
                                 _Roles.Estatus_ID equals _Estatus.Estatus_ID
                                 where _Estatus.Estatus.ToLower() == "activo" && _Roles.Tipo == "WEB"
                                 select new Cls_Apl_Roles_Negocio
                                 {
                                     Rol_ID = _Roles.Rol_ID,
                                     Nombre = _Roles.Nombre,
                                     Descripcion = _Roles.Descripcion,
                                     Estatus = dbContext.Tra_Cat_Estatus.Where(e => e.Estatus_ID == _Roles.Estatus_ID).Select(e => e.Estatus).FirstOrDefault()
                                 }).OrderBy(Reg => Reg.Nombre);

                    if (Roles.Any())
                    {
                        Lista_ = Roles.ToList<Cls_Apl_Roles_Negocio>();
                        Mensaje.Registros = JsonConvert.SerializeObject(Lista_);
                        Mensaje.Estatus = "success";
                    }
                }
            }
            catch (Exception Ex)
            {
                Mensaje.Estatus = "error";
                Mensaje.Mensaje = "Informe técnico: " + Ex.Message;
            }
            finally
            {
                Json_Resultado = JsonMapper.ToJson(Mensaje);
            }
            return Json_Resultado;
        }

        /// <summary>
        /// metodo para la consulta de los datos del catalogo
        /// </summary>
        /// <param name="Parametros">Parametros de la consulta</param>
        /// <returns>json serializado con datos </returns>
        /// <creo>Juan Alberto Hernandez Negrete</creo>
        /// <fecha_creo>25-Mayo-2017</fecha_creo>
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string Consultar_Accesos(string Parametros)
        {
            Cls_Apl_Roles_Negocio Obj_ = new Cls_Apl_Roles_Negocio();
            List<Cls_Apl_Roles_Accesos_Negocio> Lista_ = new List<Cls_Apl_Roles_Accesos_Negocio>();
            string Json_Resultado = string.Empty;
            Cls_Mensaje Mensaje = new Cls_Mensaje();

            try
            {
                Obj_ = JsonConvert.DeserializeObject<Cls_Apl_Roles_Negocio>(Parametros);

                using (var dbContext = new Sistema_TrazabilidadEntities())
                {
                    var Acceso_ = (from Accesos_ in dbContext.Apl_Accesos
                                   where Accesos_.Rol_ID == Obj_.Rol_ID
                                   select new Cls_Apl_Roles_Accesos_Negocio
                                   {
                                       Menu_ID = Accesos_.Menu_ID,
                                       Habilitado = Accesos_.Habilitado,
                                       Alta = Accesos_.Alta,
                                       Cambio = Accesos_.Cambio,
                                       Eliminar = Accesos_.Eliminar,
                                       Consultar = Accesos_.Consultar
                                   });

                    if (Acceso_.Any())
                    {
                        Lista_ = Acceso_.ToList<Cls_Apl_Roles_Accesos_Negocio>();
                        Mensaje.Registros = JsonConvert.SerializeObject(Lista_);
                        Mensaje.Estatus = "success";
                    }
                }
            }
            catch (Exception Ex)
            {
                Mensaje.Estatus = "error";
                Mensaje.Mensaje = "Informe técnico: " + Ex.Message;
            }
            finally
            {
                Json_Resultado = JsonMapper.ToJson(Mensaje);
            }
            return Json_Resultado;
        }

        /// <summary>
        /// metodo para la consulta de los datos de los menus
        /// </summary>
        /// <returns>json serializado con datos </returns>
        /// <creo>Juan Alberto Hernandez Negrete</creo>
        /// <fecha_creo>25-Mayo-2017</fecha_creo>
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string Consultar_Menus()
        {
            List<Cls_Apl_Menus_Negocio> Lista_ = new List<Cls_Apl_Menus_Negocio>();
            string Json_Resultado = string.Empty;
            Cls_Mensaje Mensaje = new Cls_Mensaje();

            try
            {
                using (var dbContext = new Sistema_TrazabilidadEntities())
                {
                    var Menu = (from Menus_Empresas in dbContext.Apl_Menus_Empresa
                                join Menus_ in dbContext.Apl_Menus on Menus_Empresas.Menu_ID equals Menus_.Menu_ID

                                where Menus_Empresas.Empresa_ID.ToString() == Cls_Sesiones.Empresa_ID

                                orderby Menus_.Orden ascending
                                select new Cls_Apl_Menus_Negocio
                                {
                                    Menu_ID = Menus_.Menu_ID.ToString(),
                                    _Menu_ID = Menus_.Menu_ID.ToString(),
                                    Nombre_Mostrar = (Menus_.Nombre_Mostrar != null) ? Menus_.Nombre_Mostrar : "",
                                    //Sistema = Menus_.Sistema,
                                    Parent_ID = Menus_.Parent_ID
                                }).ToList().Where(x => x.Parent_ID.Equals("0    "));

                    if (Menu.Any())
                    {
                        foreach (var R in Menu)
                        {
                            //Obtenemos los submenus
                            var Submenu_ = (from Menus_Empresas in dbContext.Apl_Menus_Empresa
                                            join Menus_ in dbContext.Apl_Menus on Menus_Empresas.Menu_ID equals Menus_.Menu_ID

                                            where Menus_.Parent_ID != "0    " && Menus_.Parent_ID == R.Menu_ID && Menus_Empresas.Empresa_ID.ToString() == Cls_Sesiones.Empresa_ID

                                            orderby Menus_.Orden ascending
                                            select new Cls_Apl_Menus_Negocio
                                            {
                                                Menu_ID = Menus_.Menu_ID.ToString(),
                                                _Menu_ID = Menus_.Menu_ID.ToString(),
                                                Nombre_Mostrar = (Menus_.Nombre_Mostrar != null) ? Menus_.Nombre_Mostrar : "",
                                                //Sistema = Menus_.Sistema,
                                                Parent_ID = Menus_.Parent_ID,
                                            });

                            //creamos el registro del menu
                            Lista_.Add(new Cls_Apl_Menus_Negocio()
                            {
                                Menu_ID = R.Menu_ID,
                                _Menu_ID = R._Menu_ID,
                                Nombre_Mostrar = R.Nombre_Mostrar,
                                //Sistema = R.Sistema,
                                Submenus = JsonConvert.SerializeObject(Submenu_)
                            });
                        }

                        Mensaje.Registros = JsonConvert.SerializeObject(Lista_);
                        Mensaje.Estatus = "success";
                    }
                }
            }
            catch (Exception Ex)
            {
                Mensaje.Estatus = "error";
                Mensaje.Mensaje = "Informe técnico: " + Ex.Message;
            }
            finally
            {
                Json_Resultado = JsonMapper.ToJson(Mensaje);
            }
            return Json_Resultado;
        }

        /// <summary>
        /// metodo para la insercion de los datos del catalogo
        /// </summary>
        /// <param name="Parametros">Parametros de la consulta</param>
        /// <returns>json serializado con datos </returns>
        /// <creo>Juan Alberto Hernandez Negrete</creo>
        /// <fecha_creo>29-Mayo-2017</fecha_creo>
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string Alta(string Parametros, string Accesos)
        {
            Cls_Apl_Roles_Negocio Obj_Roles = null;
            List<Cls_Apl_Roles_Accesos_Negocio> Accesos_ = new List<Cls_Apl_Roles_Accesos_Negocio>();
            List<Cls_Apl_Roles_Accesos_Negocio> List_ = new List<Cls_Apl_Roles_Accesos_Negocio>();
            string Json_Resultado = string.Empty;
            Cls_Mensaje Mensaje = new Cls_Mensaje();
            string Sql = string.Empty;
            int Rol_ID = 0;
            int Menu_ID = 0;

            try
            {
                Mensaje.Titulo = "Alta registro";
                Obj_Roles = JsonConvert.DeserializeObject<Cls_Apl_Roles_Negocio>(Parametros);
                Accesos_ = JsonConvert.DeserializeObject<List<Cls_Apl_Roles_Accesos_Negocio>>(Accesos);

                using (var dbContext = new Sistema_TrazabilidadEntities())
                {
                    //insertamos el rol
                    var _Roles = new Apl_Roles();
                    _Roles.Nombre = Obj_Roles.Nombre;
                    _Roles.Empresa_ID = Convert.ToInt32(Cls_Sesiones.Empresa_ID);
                    _Roles.Sucursal_ID = Convert.ToInt32(Cls_Sesiones.Sucursal_ID);
                    _Roles.Nivel_ID = 1;
                    _Roles.Tipo = "WEB";
                    _Roles.Descripcion = (Obj_Roles.Descripcion == null || Obj_Roles.Descripcion == string.Empty) ? null : Obj_Roles.Descripcion;
                    _Roles.Estatus_ID = dbContext.Tra_Cat_Estatus.Where(x=>x.Estatus.ToLower() == Obj_Roles.Estatus.ToLower()).Select(x=>x.Estatus_ID).FirstOrDefault();
                    _Roles.Usuario_Creo = Cls_Sesiones.Usuario;
                    _Roles.Fecha_Creo = new DateTime?(DateTime.Now).Value;
                    dbContext.Apl_Roles.Add(_Roles);
                    dbContext.SaveChanges();
                    Rol_ID = Convert.ToInt32(_Roles.Rol_ID);

                    //insertamos los detalles
                    Sql = "insert into Apl_Accesos (Rol_ID, Menu_ID, Habilitado, Alta, Cambio, Eliminar, Usuario_Creo, Fecha_Creo, Consultar, Estatus_ID)";
                    Sql += "select " + Rol_ID + ", Menu_ID, 'N','N','N','N', '" + Cls_Sesiones.Usuario + "',getdate(),'N', " + _Roles.Estatus_ID + " from Apl_Menus;";
                    dbContext.Database.ExecuteSqlCommand(Sql);
                    dbContext.SaveChanges();

                    //insertamos los accesos
                    for (int i = 0; i < Accesos_.Count; i++)
                    {
                        Menu_ID = Convert.ToInt32(Accesos_[i].Menu_ID);

                        var _Rol_Acceso = (from Acceso_ in dbContext.Apl_Accesos
                                           where Acceso_.Rol_ID == Rol_ID
                                           && Acceso_.Menu_ID == Menu_ID
                                           select Acceso_).First();

                        if (!String.IsNullOrEmpty(Accesos_[i].Habilitado))
                            _Rol_Acceso.Habilitado = Accesos_[i].Habilitado;

                        if (!String.IsNullOrEmpty(Accesos_[i].Alta))
                            _Rol_Acceso.Alta = Accesos_[i].Alta;

                        if (!String.IsNullOrEmpty(Accesos_[i].Cambio))
                            _Rol_Acceso.Cambio = Accesos_[i].Cambio;

                        if (!String.IsNullOrEmpty(Accesos_[i].Eliminar))
                            _Rol_Acceso.Eliminar = Accesos_[i].Eliminar;

                        if (!String.IsNullOrEmpty(Accesos_[i].Consultar))
                            _Rol_Acceso.Consultar = Accesos_[i].Consultar;

                        dbContext.SaveChanges();
                    }
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
            }
            finally
            {
                Json_Resultado = JsonMapper.ToJson(Mensaje);
            }
            return Json_Resultado;
        }

        /// <summary>
        /// metodo para la modificacion de los datos del catalogo
        /// </summary>
        /// <param name="Parametros">Parametros de la consulta</param>
        /// <returns>json serializado con datos </returns>
        /// <creo>Juan Alberto Hernandez Negrete</creo>
        /// <fecha_creo>29-Mayo-2017</fecha_creo>
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string Actualizar(string Parametros, string Accesos)
        {
            Cls_Apl_Roles_Negocio Obj_Roles = null;
            List<Cls_Apl_Roles_Accesos_Negocio> Accesos_ = new List<Cls_Apl_Roles_Accesos_Negocio>();
            string Json_Resultado = string.Empty;
            Cls_Mensaje Mensaje = new Cls_Mensaje();
            string Sql = string.Empty;
            int Menu_ID = 0;

            try
            {
                Mensaje.Titulo = "Actualizar registro";
                Obj_Roles = JsonConvert.DeserializeObject<Cls_Apl_Roles_Negocio>(Parametros);
                Accesos_ = JsonConvert.DeserializeObject<List<Cls_Apl_Roles_Accesos_Negocio>>(Accesos);

                using (var dbContext = new Sistema_TrazabilidadEntities())
                {
                    var _Roles = dbContext.Apl_Roles.Where(m => m.Rol_ID == Obj_Roles.Rol_ID).First();
                    _Roles.Rol_ID = Obj_Roles.Rol_ID.Value;
                    _Roles.Nombre = Obj_Roles.Nombre;
                    _Roles.Descripcion = (Obj_Roles.Descripcion == null || Obj_Roles.Descripcion == string.Empty) ? null : Obj_Roles.Descripcion;
                    _Roles.Estatus_ID = dbContext.Tra_Cat_Estatus.Where(x => x.Estatus.ToLower() == Obj_Roles.Estatus.ToLower()).Select(x => x.Estatus_ID).FirstOrDefault(); ;
                    _Roles.Usuario_Modifico = Cls_Sesiones.Usuario;
                    _Roles.Fecha_Modifico = new DateTime?(DateTime.Now).Value;
                    dbContext.SaveChanges();

                    //eliminamos los accesos del rol
                    Sql = "delete Apl_Accesos where Rol_ID = " + Obj_Roles.Rol_ID;
                    dbContext.Database.ExecuteSqlCommand(Sql);
                    dbContext.SaveChanges();

                    //insertamos los detalles
                    Sql = "insert into Apl_Accesos (Rol_ID, Menu_ID, Habilitado, Alta, Cambio, Eliminar, Usuario_Creo, Fecha_Creo, Consultar, Estatus_ID)";
                    Sql += "select " + Obj_Roles.Rol_ID + ", Menu_ID, 'N','N','N','N', '" + Cls_Sesiones.Usuario + "',getdate(),'N', " + _Roles.Estatus_ID + " from Apl_Menus;";
                    dbContext.Database.ExecuteSqlCommand(Sql);
                    dbContext.SaveChanges();

                    //insertamos los accesos
                    for (int i = 0; i < Accesos_.Count; i++)
                    {
                        Menu_ID = Convert.ToInt32(Accesos_[i].Menu_ID);

                        var _Rol_Acceso = (from Acceso_ in dbContext.Apl_Accesos
                                           where Acceso_.Rol_ID == Obj_Roles.Rol_ID
                                           && Acceso_.Menu_ID == Menu_ID
                                           select Acceso_).First();

                        if (!String.IsNullOrEmpty(Accesos_[i].Habilitado))
                            _Rol_Acceso.Habilitado = Accesos_[i].Habilitado;

                        if (!String.IsNullOrEmpty(Accesos_[i].Alta))
                            _Rol_Acceso.Alta = Accesos_[i].Alta;

                        if (!String.IsNullOrEmpty(Accesos_[i].Cambio))
                            _Rol_Acceso.Cambio = Accesos_[i].Cambio;

                        if (!String.IsNullOrEmpty(Accesos_[i].Eliminar))
                            _Rol_Acceso.Eliminar = Accesos_[i].Eliminar;

                        if (!String.IsNullOrEmpty(Accesos_[i].Consultar))
                            _Rol_Acceso.Consultar = Accesos_[i].Consultar;

                        dbContext.SaveChanges();
                    }
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
            }
            finally
            {
                Json_Resultado = JsonMapper.ToJson(Mensaje);
            }
            return Json_Resultado;
        }

        /// <summary>
        /// metodo para la eliminacion de los datos del catalogo
        /// </summary>
        /// <param name="Parametros">Parametros de la consulta</param>
        /// <returns>json serializado con datos </returns>
        /// <creo>Juan Alberto Hernandez Negrete</creo>
        /// <fecha_creo>29-Mayo-2017</fecha_creo>
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string Eliminar(string Parametros)
        {
            Cls_Apl_Roles_Negocio Obj_Roles = null;
            string Json_Resultado = string.Empty;
            Cls_Mensaje Mensaje = new Cls_Mensaje();
            String Sql = String.Empty;

            try
            {
                Mensaje.Titulo = "Eliminar registro";
                Obj_Roles = JsonConvert.DeserializeObject<Cls_Apl_Roles_Negocio>(Parametros);

                using (var dbContext = new Sistema_TrazabilidadEntities())
                {
                    //revisamos si algun usuario tiene asigando el rol
                    var Usuarios_ = (from Rel_Roles_Usuarios in dbContext.Apl_Rel_Usuarios_Roles
                                     where Rel_Roles_Usuarios.Rol_ID == Obj_Roles.Rol_ID && 
                                     Rel_Roles_Usuarios.Empresa_ID.ToString() == Cls_Sesiones.Empresa_ID &&
                                     Rel_Roles_Usuarios.Sucursal_ID.ToString() == Cls_Sesiones.Sucursal_ID
                                     select Rel_Roles_Usuarios);

                    if (Usuarios_.Any())
                    {
                        Mensaje.Estatus = "error";
                        Mensaje.Mensaje =
                        "La operación de eliminar el registro fue revocada. <br /><br />" +
                        "<i class='fa fa-angle-double-right' ></i>&nbsp;&nbsp; El registro que intenta eliminar ya se encuentra en uso.";
                    }
                    else
                    {
                        //eliminamos los accesos del rol
                        Sql = "delete Apl_Accesos where Rol_ID = " + Obj_Roles.Rol_ID;
                        dbContext.Database.ExecuteSqlCommand(Sql);
                        dbContext.SaveChanges();

                        var _Roles = dbContext.Apl_Roles.Where(m => m.Rol_ID == Obj_Roles.Rol_ID).First();
                        dbContext.Apl_Roles.Remove(_Roles);
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
                if (Ex.InnerException.InnerException.Message.Contains("The DELETE statement conflicted with the REFERENCE constraint"))
                    Mensaje.Mensaje =
                        "La operación de eliminar el registro fue revocada. <br /><br />" +
                        "<i class='fa fa-angle-double-right' ></i>&nbsp;&nbsp; El registro que intenta eliminar ya se encuentra en uso.";
                else if (Ex.InnerException.InnerException.Message.Contains("Instrucción DELETE en conflicto con la restricción REFERENCE"))
                    Mensaje.Mensaje =
                        "La operación de eliminar el registro fue revocada. <br /><br />" +
                        "<i class='fa fa-angle-double-right' ></i>&nbsp;&nbsp; El registro que intenta eliminar ya se encuentra en uso.";
                else
                    Mensaje.Mensaje = "Informe técnico: " + Ex.Message;
            }
            finally
            {
                Json_Resultado = JsonMapper.ToJson(Mensaje);
            }
            return Json_Resultado;
        }
    }
}
