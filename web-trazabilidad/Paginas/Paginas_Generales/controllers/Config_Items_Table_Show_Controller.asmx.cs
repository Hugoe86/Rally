using datos_trazabilidad;
using Elmah;
using LitJson;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using web_trazabilidad.Models.Ayudante;
using web_trazabilidad.Models.Negocio;
using web_trazabilidad.Models.Negocio.Trazabilidad;

namespace web_trazabilidad.Paginas.Paginas_Generales.controllers
{
    /// <summary>
    /// Summary description for Config_Items_Table_Show_Controller
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class Config_Items_Table_Show_Controller : System.Web.Services.WebService
    {
        #region (Métodos)

        /// <summary>
        /// Método que realiza la búsqueda de Configurador de Items.
        /// </summary>
        /// <returns>Listado serializado con las Tipos_Productos según los filtros aplícados</returns>
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string Consultar_Config_Items(string jsonObject)
        {
            Cls_Tra_Cat_Config_Items_Table_Show_Negocio Obj_Items = null;
            string Json_Resultado = string.Empty;
            List<Cls_Tra_Cat_Config_Items_Table_Show_Negocio> Lista_Config_Items = new List<Cls_Tra_Cat_Config_Items_Table_Show_Negocio>();
            List<Cls_Tra_Cat_Config_Items_Table_Show_Negocio> Lista_Config_Items_Filter = new List<Cls_Tra_Cat_Config_Items_Table_Show_Negocio>();

            try
            {
                Obj_Items = JsonMapper.ToObject<Cls_Tra_Cat_Config_Items_Table_Show_Negocio>(jsonObject);

                using (var dbContext = new Sistema_TrazabilidadEntities())
                {
                    SqlParameter sqlEmpresaID = new SqlParameter("@Empresa_ID", Cls_Sesiones.Empresa_ID);
                    SqlParameter sqlSucursalID = new SqlParameter("@Sucursal_ID", Cls_Sesiones.Sucursal_ID);

                    Lista_Config_Items = dbContext.Database.SqlQuery<Cls_Tra_Cat_Config_Items_Table_Show_Negocio>("Sp_consultar_configurador_items @Empresa_ID, @Sucursal_ID",
                    sqlEmpresaID, sqlSucursalID).ToList();
                }
                if (!string.IsNullOrEmpty(Obj_Items.Pagina) || !string.IsNullOrEmpty(Obj_Items.Tabla))
                {
                    Lista_Config_Items_Filter = Lista_Config_Items.Where(x => (!string.IsNullOrEmpty(Obj_Items.Pagina) ? x.Pagina.ToLower().Contains(Obj_Items.Pagina.ToLower()) : true) && (!string.IsNullOrEmpty(Obj_Items.Tabla) ? x.Tabla.ToLower().Contains(Obj_Items.Tabla.ToLower()) : true)).ToList();
                    Json_Resultado = JsonMapper.ToJson(Lista_Config_Items_Filter);
                }
                else
                {
                    Json_Resultado = JsonMapper.ToJson(Lista_Config_Items);
                }


            }
            catch (Exception Ex)
            {
                ErrorSignal.FromCurrentContext().Raise(Ex);
            }
            return Json_Resultado;
        }
        /// <summary>
        /// Método que sirve para llenar el combo con los formularios de la empresa
        /// </summary>
        /// <returns></returns>
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string Consultar_Formularios()
        {
            string Json_Resultado = string.Empty;
            List<Cls_Apl_Menus_Negocio> Lista_Formularios = new List<Cls_Apl_Menus_Negocio>();
            int Empresa_ID = Convert.ToInt32(Cls_Sesiones.Empresa_ID);

            try
            {
                using (var dbContext = new Sistema_TrazabilidadEntities())
                {
                    var Formularios = (from _apl_menus in dbContext.Apl_Menus
                                       join _apl_menus_empresas in dbContext.Apl_Menus_Empresa
                                       on _apl_menus.Menu_ID equals _apl_menus_empresas.Menu_ID
                                       where
                                       _apl_menus_empresas.Empresa_ID == Empresa_ID
                                       && _apl_menus.Parent_ID != "0"
                                       //&& _apl_menus.URL_LINK.Contains("catalogo")
                                       select new Cls_Apl_Menus_Negocio
                                       {
                                           URL_LINK = _apl_menus.URL_LINK
                                       }).OrderByDescending(m => m.URL_LINK);

                    foreach (var p in Formularios)
                    {
                        if (!string.IsNullOrEmpty(p.URL_LINK))
                        {
                            string[] tokens = p.URL_LINK.Split('/');
                            if (tokens != null)
                            {
                                int position = tokens.Length;
                                Lista_Formularios.Add(new Cls_Apl_Menus_Negocio { URL_LINK = tokens[position - 1] });
                            }
                        }
                    }

                    Json_Resultado = JsonMapper.ToJson(Lista_Formularios);
                }
            }
            catch (Exception Ex)
            {
                ErrorSignal.FromCurrentContext().Raise(Ex);
            }
            return Json_Resultado;
        }
        /// <summary>
        /// Método que sirve para cargar las tablas del sistema
        /// </summary>
        /// <returns></returns>
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string Consultar_Tablas_Sistema()
        {
            string Json_Resultado = string.Empty;
            List<Cls_Table> ListaTablas = new List<Cls_Table>();

            try
            {
                using (var dbContext = new Sistema_TrazabilidadEntities())
                {
                    ListaTablas = dbContext.Database.SqlQuery<Cls_Table>("SP_Consultar_Tablas_Catalogos").ToList();
                }
                Json_Resultado = JsonMapper.ToJson(ListaTablas);
            }
            catch (Exception Ex)
            {
                ErrorSignal.FromCurrentContext().Raise(Ex);
            }
            return Json_Resultado;
        }
        /// <summary>
        /// Método que sirve para consultar los registros de la tabla seleccionado
        /// </summary>
        /// <returns>Listado serializado con las Tipos_Productos según los filtros aplícados</returns>
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string Consultar_Data(string jsonObject)
        {
            Cls_Tra_Cat_Config_Items_Table_Show_Negocio Obj_Items = null;
            string Json_Resultado = string.Empty;
            List<Cls_Tra_Cat_Config_Items_Table_Show_Negocio> Lista_Datos = new List<Cls_Tra_Cat_Config_Items_Table_Show_Negocio>();

            try
            {
                Obj_Items = JsonMapper.ToObject<Cls_Tra_Cat_Config_Items_Table_Show_Negocio>(jsonObject);

                using (var dbContext = new Sistema_TrazabilidadEntities())
                {
                    SqlParameter sqlEmpresaID = new SqlParameter("@Empresa_ID", Cls_Sesiones.Empresa_ID);
                    SqlParameter sqlSucursalID = new SqlParameter("@Sucursal_ID", Cls_Sesiones.Sucursal_ID);
                    SqlParameter Tabla = new SqlParameter("@Tabla", Obj_Items.Tabla);

                    Lista_Datos = dbContext.Database.SqlQuery<Cls_Tra_Cat_Config_Items_Table_Show_Negocio>("SP_Consultar_Data @Empresa_ID, @Sucursal_ID, @Tabla",
                    sqlEmpresaID, sqlSucursalID, Tabla).ToList();
                }
                Json_Resultado = JsonMapper.ToJson(Lista_Datos);
            }
            catch (Exception Ex)
            {
                ErrorSignal.FromCurrentContext().Raise(Ex);
            }
            return Json_Resultado;
        }
        /// <summary>
        /// Método que verifica que no existan verificados
        /// </summary>
        /// <param name="jsonObject"></param>
        /// <returns></returns>
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string Verificar_Duplicados(string jsonObject)
        {
            Cls_Tra_Cat_Config_Items_Table_Show_Negocio Obj_Datos = null;
            string Json_Resultado = string.Empty;
            List<Cls_Tra_Cat_Config_Items_Table_Show_Negocio> Lista_Duplicados = new List<Cls_Tra_Cat_Config_Items_Table_Show_Negocio>();
            Cls_Mensaje Mensaje = new Cls_Mensaje();

            try
            {
                Mensaje.Titulo = "Validaciones";
                Obj_Datos = JsonMapper.ToObject<Cls_Tra_Cat_Config_Items_Table_Show_Negocio>(jsonObject);
                int Empresa_ID = Convert.ToInt32(Cls_Sesiones.Empresa_ID);
                int Sucursal_ID = Convert.ToInt32(Cls_Sesiones.Sucursal_ID);
                int ID = Convert.ToInt32(Obj_Datos.ID);

                using (var dbContext = new Sistema_TrazabilidadEntities())
                {
                    var _duplicados = (from _config_items in dbContext.Tra_Cat_Config_Items_Table_Show
                                    where
                                    _config_items.Pagina == Obj_Datos.Pagina
                                    && _config_items.Tabla == Obj_Datos.Tabla
                                    && _config_items.ID == Obj_Datos.ID
                                    && _config_items.Empresa_ID == Empresa_ID
                                    && _config_items.Sucursal_ID == Sucursal_ID
                                       select new Cls_Tra_Cat_Config_Items_Table_Show_Negocio
                                       {
                                           Config_ID = _config_items.Config_ID,
                                           Pagina = _config_items.Pagina,
                                           ID = _config_items.ID
                                       });

                    if (_duplicados.Any())
                    {
                        if (Obj_Datos.Config_ID == 0)
                        {
                            Mensaje.Estatus = "error";
                            if (!string.IsNullOrEmpty(Obj_Datos.ID))
                                Mensaje.Mensaje = "Esta combinación ya se encuentra registrada.";
                        }
                        else
                        {
                            var item_edit = _duplicados.Where(c => c.Config_ID == Obj_Datos.Config_ID && c.ID == Obj_Datos.ID && c.Pagina == Obj_Datos.Pagina);

                            if (item_edit.Count() == 1)
                                Mensaje.Estatus = "success";
                            else
                            {
                                Mensaje.Estatus = "error";
                                if (!string.IsNullOrEmpty(Obj_Datos.ID))
                                    Mensaje.Mensaje = "Esta combinación ya se encuentra registrada.";
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
            }
            return Json_Resultado;
        }
        /// <summary>
        /// Método que realiza el alta de las configuraciones.
        /// </summary>
        /// <returns>Objeto serializado con los resultados de la operación</returns>
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string Alta(string jsonObject)
        {
            Cls_Tra_Cat_Config_Items_Table_Show_Negocio Obj_Config = null;
            string Json_Resultado = string.Empty;
            Cls_Mensaje Mensaje = new Cls_Mensaje();

            try
            {
                Mensaje.Titulo = "Alta registro";
                Obj_Config = JsonMapper.ToObject<Cls_Tra_Cat_Config_Items_Table_Show_Negocio>(jsonObject);

                using (var dbContext = new Sistema_TrazabilidadEntities())
                {
                    var _config_items = new Tra_Cat_Config_Items_Table_Show();
                    _config_items.Pagina = Obj_Config.Pagina;
                    _config_items.Empresa_ID = Int32.Parse(Cls_Sesiones.Empresa_ID);
                    _config_items.Sucursal_ID = Int32.Parse(Cls_Sesiones.Sucursal_ID);
                    _config_items.Tabla = Obj_Config.Tabla;
                    _config_items.ID = Obj_Config.ID;
                    _config_items.Usuario_Creo = Cls_Sesiones.Usuario;
                    _config_items.Fecha_Creo = new DateTime?(DateTime.Now).Value;

                    dbContext.Tra_Cat_Config_Items_Table_Show.Add(_config_items);
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
                        "Existen campos definidos como nombre que no pueden duplicarse. <br />" +
                        "<i class='fa fa-angle-double-right' ></i>&nbsp;&nbsp; Por favor revisar que no este ingresando datos duplicados.";
                else
                    Mensaje.Mensaje = "Informe técnico: " + Ex.Message;
                ErrorSignal.FromCurrentContext().Raise(Ex);
            }
            finally
            {
                Json_Resultado = JsonMapper.ToJson(Mensaje);
            }
            return Json_Resultado;
        }
        /// <summary>
        /// Método que realiza la actualización de los datos de las configuraciones.
        /// </summary>
        /// <returns>Objeto serializado con los resultados de la operación</returns>
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string Actualizar(string jsonObject)
        {
            Cls_Tra_Cat_Config_Items_Table_Show_Negocio Obj_Configuraciones = null;
            string Json_Resultado = string.Empty;
            Cls_Mensaje Mensaje = new Cls_Mensaje();

            try
            {
                Mensaje.Titulo = "Actualizar registro";
                Obj_Configuraciones = JsonMapper.ToObject<Cls_Tra_Cat_Config_Items_Table_Show_Negocio>(jsonObject);

                using (var dbContext = new Sistema_TrazabilidadEntities())
                {
                    var _config_items = dbContext.Tra_Cat_Config_Items_Table_Show.Where(c => c.Config_ID == Obj_Configuraciones.Config_ID).First();

                    _config_items.Pagina = Obj_Configuraciones.Pagina;
                    _config_items.Tabla = Obj_Configuraciones.Tabla;
                    _config_items.ID = Obj_Configuraciones.ID;
                    _config_items.Usuario_Modifico = Cls_Sesiones.Usuario;
                    _config_items.fecha_Modifico = new DateTime?(DateTime.Now);

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
                        "Existen campos definidos como nombre que no pueden duplicarse. <br />" +
                        "<i class='fa fa-angle-double-right' ></i>&nbsp;&nbsp; Por favor revisar que no este ingresando datos duplicados.";
                else
                    Mensaje.Mensaje = "Informe técnico: " + Ex.Message;

                ErrorSignal.FromCurrentContext().Raise(Ex);
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
            Cls_Tra_Cat_Config_Items_Table_Show_Negocio Obj_Configuraciones = null;
            string Json_Resultado = string.Empty;
            Cls_Mensaje Mensaje = new Cls_Mensaje();

            try
            {
                Mensaje.Titulo = "Eliminar registro";
                Obj_Configuraciones = JsonMapper.ToObject<Cls_Tra_Cat_Config_Items_Table_Show_Negocio>(jsonObject);

                using (var dbContext = new Sistema_TrazabilidadEntities())
                {
                    var _config_items = dbContext.Tra_Cat_Config_Items_Table_Show.Where(c => c.Config_ID == Obj_Configuraciones.Config_ID).First();
                    dbContext.Tra_Cat_Config_Items_Table_Show.Remove(_config_items);
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
            }
            finally
            {
                Json_Resultado = JsonMapper.ToJson(Mensaje);
            }
            return Json_Resultado;
        }

        #endregion
    }
}
