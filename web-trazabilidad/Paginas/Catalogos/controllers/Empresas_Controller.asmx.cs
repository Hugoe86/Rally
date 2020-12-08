
using datos_trazabilidad;
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

namespace web_trazabilidad.Paginas.Catalogos.controllers
{
    /// <summary>
    /// Descripción breve de Empresas_Controller
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    [System.Web.Script.Services.ScriptService]
    public class Empresas_Controller : System.Web.Services.WebService
    {
        #region (Métodos)
        /// <summary>
        /// Método que realiza el alta de la unidad.
        /// </summary>
        /// <returns>Objeto serializado con los resultados de la operación</returns>
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string Alta(string jsonObject)
        {
            Cls_Apl_Cat_Empresas_Negocio Obj_Empresas = null;
            string Json_Resultado = string.Empty;
            Cls_Mensaje Mensaje = new Cls_Mensaje();
            int Nueva_Empresa_ID;
            String Nueva_Email;

            try
            {
                Mensaje.Titulo = "Alta registro";
                Obj_Empresas = JsonMapper.ToObject<Cls_Apl_Cat_Empresas_Negocio>(jsonObject);

                using (var dbContext = new Sistema_TrazabilidadEntities())
                {
                    var _empresas = new Apl_Empresas();
                    _empresas.Nombre = Obj_Empresas.Nombre;
                    _empresas.Clave = Obj_Empresas.Clave;
                    _empresas.Estatus_ID = Obj_Empresas.Estatus_ID;
                    _empresas.Comentarios = string.IsNullOrEmpty(Obj_Empresas.Comentarios) ? null : Obj_Empresas.Comentarios;
                    _empresas.Entidad_Empresa_ID = Obj_Empresas.Entidad_Empresa_ID;
                    _empresas.Direccion = string.IsNullOrEmpty(Obj_Empresas.Direccion) ? null : Obj_Empresas.Direccion;
                    _empresas.Colonia = string.IsNullOrEmpty(Obj_Empresas.Colonia) ? null : Obj_Empresas.Colonia;
                    _empresas.RFC = string.IsNullOrEmpty(Obj_Empresas.RFC) ? null : Obj_Empresas.RFC;
                    _empresas.Codigo_Postal_ID = Obj_Empresas.Codigo_Postal_ID;
                    _empresas.CP = string.IsNullOrEmpty(Obj_Empresas.CP) ? null : Obj_Empresas.CP;
                    _empresas.Ciudad_ID = Obj_Empresas.Ciudad_ID;
                    _empresas.Ciudad = string.IsNullOrEmpty(Obj_Empresas.Ciudad) ? null : Obj_Empresas.Ciudad;
                    _empresas.Estado_ID = Obj_Empresas.Estado_ID;
                    _empresas.Estado = string.IsNullOrEmpty(Obj_Empresas.Estado) ? null : Obj_Empresas.Estado;
                    _empresas.Telefono = string.IsNullOrEmpty(Obj_Empresas.Telefono) ? null : Obj_Empresas.Telefono;
                    _empresas.Fax = string.IsNullOrEmpty(Obj_Empresas.Fax) ? null : Obj_Empresas.Fax;
                    _empresas.Email = Obj_Empresas.Email;
                    _empresas.Usuario_Creo = Cls_Sesiones.Datos_Usuario.Usuario;
                    _empresas.Fecha_Creo = new DateTime?(DateTime.Now).Value;
                    _empresas.Nombre_Carpeta = Obj_Empresas.Nombre_Carpeta;
                    _empresas.Pais_ID = Obj_Empresas.Pais_ID;
                    _empresas.Pais = string.IsNullOrEmpty(Obj_Empresas.Pais) ? null : Obj_Empresas.Pais;
                    _empresas.Localidad_ID = Obj_Empresas.Localidad_ID;
                    _empresas.Localidad = string.IsNullOrEmpty(Obj_Empresas.Localidad) ? null : Obj_Empresas.Localidad;
                    _empresas.Municipio_Localidad_ID = Obj_Empresas.Municipio_Localidad_ID;


                    dbContext.Apl_Empresas.Add(_empresas);
                    dbContext.SaveChanges();
                    Mensaje.Estatus = "success";
                    Mensaje.Mensaje = "La operación se completo sin problemas.";
                    Nueva_Empresa_ID = _empresas.Empresa_ID;
                    Nueva_Email = _empresas.Email;
                    Alta_Sucursal(Nueva_Empresa_ID, _empresas.Estatus_ID, Nueva_Email, _empresas.Nombre);
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
        /// Método que realiza la actualización de los datos de la unidad seleccionada.
        /// </summary>
        /// <returns>Objeto serializado con los resultados de la operación</returns>
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string Actualizar(string jsonObject)
        {
            Cls_Apl_Cat_Empresas_Negocio Obj_Empresas = null;
            string Json_Resultado = string.Empty;
            Cls_Mensaje Mensaje = new Cls_Mensaje();

            try
            {
                Mensaje.Titulo = "Actualizar registro";
                Obj_Empresas = JsonMapper.ToObject<Cls_Apl_Cat_Empresas_Negocio>(jsonObject);

                using (var dbContext = new Sistema_TrazabilidadEntities())
                {
                    var _empresas = dbContext.Apl_Empresas.Where(u => u.Empresa_ID == Obj_Empresas.Empresa_ID).First();


                    _empresas.Nombre = Obj_Empresas.Nombre;
                    _empresas.Clave = Obj_Empresas.Clave;
                    _empresas.Estatus_ID = Obj_Empresas.Estatus_ID;
                    _empresas.Comentarios = Obj_Empresas.Comentarios;
                    _empresas.Entidad_Empresa_ID = Obj_Empresas.Entidad_Empresa_ID;
                    _empresas.Direccion = Obj_Empresas.Direccion;
                    _empresas.Colonia = Obj_Empresas.Colonia;
                    _empresas.RFC = Obj_Empresas.RFC;
                    _empresas.Codigo_Postal_ID = Obj_Empresas.Codigo_Postal_ID;
                    _empresas.CP = Obj_Empresas.CP;
                    _empresas.Ciudad_ID = Obj_Empresas.Ciudad_ID;
                    _empresas.Ciudad = Obj_Empresas.Ciudad;
                    _empresas.Estado_ID = Obj_Empresas.Estado_ID;
                    _empresas.Estado = Obj_Empresas.Estado;
                    _empresas.Telefono = Obj_Empresas.Telefono;
                    _empresas.Fax = Obj_Empresas.Fax;
                    _empresas.Email = Obj_Empresas.Email;
                    _empresas.Pais_ID = Obj_Empresas.Pais_ID;
                    _empresas.Pais = Obj_Empresas.Pais;
                    _empresas.Localidad_ID = Obj_Empresas.Localidad_ID;
                    _empresas.Localidad = Obj_Empresas.Localidad;
                    _empresas.Municipio_Localidad_ID = Obj_Empresas.Municipio_Localidad_ID;
                    _empresas.Usuario_Modifico = Cls_Sesiones.Datos_Usuario.Usuario;
                    _empresas.Fecha_Modifico = new DateTime?(DateTime.Now);
                    _empresas.Nombre_Carpeta = Obj_Empresas.Nombre_Carpeta;
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
            Cls_Apl_Cat_Empresas_Negocio Obj_Empresas = null;
            string Json_Resultado = string.Empty;
            Cls_Mensaje Mensaje = new Cls_Mensaje();

            try
            {
                Mensaje.Titulo = "Eliminar registro";
                Obj_Empresas = JsonMapper.ToObject<Cls_Apl_Cat_Empresas_Negocio>(jsonObject);

                using (var dbContext = new Sistema_TrazabilidadEntities())
                {
                    var _empresas = dbContext.Apl_Empresas.Where(u => u.Empresa_ID == Obj_Empresas.Empresa_ID).First();
                    dbContext.Apl_Empresas.Remove(_empresas);
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
            }
            finally
            {
                Json_Resultado = JsonMapper.ToJson(Mensaje);
            }
            return Json_Resultado;
        }


        /// <summary>
        /// Método que realiza la búsqueda de fases.
        /// </summary>
        /// <returns>Listado de fases filtradas por clave</returns>
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string Consultar_Empresas_Por_Clave(string jsonObject)
        {
            Cls_Apl_Cat_Empresas_Negocio Obj_Empresa = null;
            string Json_Resultado = string.Empty;
            List<Cls_Apl_Cat_Empresas_Negocio> Lista_Empresa = new List<Cls_Apl_Cat_Empresas_Negocio>();
            Cls_Mensaje Mensaje = new Cls_Mensaje();

            try
            {
                Mensaje.Titulo = "Validaciones";
                Obj_Empresa = JsonMapper.ToObject<Cls_Apl_Cat_Empresas_Negocio>(jsonObject);

                using (var dbContext = new Sistema_TrazabilidadEntities())
                {
                    var _empresas = (from _empresa in dbContext.Apl_Empresas
                                     where _empresa.Clave.Equals(Obj_Empresa.Clave) ||
                                     _empresa.Nombre.Equals(Obj_Empresa.Nombre) ||
                                     _empresa.Email.Equals(Obj_Empresa.Email)
                                     select new Cls_Apl_Cat_Empresas_Negocio
                                     {
                                         Empresa_ID = _empresa.Empresa_ID,
                                         Nombre = _empresa.Nombre,
                                         Clave = _empresa.Clave,
                                         Email = _empresa.Email
                                     }).OrderByDescending(u => u.Empresa_ID);

                    if (_empresas.Any())
                    {
                        if (Obj_Empresa.Empresa_ID == 0)
                        {
                            Mensaje.Estatus = "error";
                            if (!string.IsNullOrEmpty(Obj_Empresa.Clave))
                                Mensaje.Mensaje = "El clave ingresado ya se encuentra registrado.";
                            else if (!string.IsNullOrEmpty(Obj_Empresa.Nombre))
                                Mensaje.Mensaje = "El nombre ingresado ya se encuentra registrado.";
                            else if (!string.IsNullOrEmpty(Obj_Empresa.Email))
                                Mensaje.Mensaje = "El email ingresado ya se encuentra registrado.";
                        }
                        else
                        {
                            var item_edit = _empresas.Where(u => u.Empresa_ID == Obj_Empresa.Empresa_ID);

                            if (item_edit.Count() == 1)
                                Mensaje.Estatus = "success";
                            else
                            {
                                Mensaje.Estatus = "error";
                                if (!string.IsNullOrEmpty(Obj_Empresa.Clave))
                                    Mensaje.Mensaje = "La clave ingresada ya se encuentra registrado.";
                                else if (!string.IsNullOrEmpty(Obj_Empresa.Nombre))
                                    Mensaje.Mensaje = "El nombre ingresado ya se encuentra registrado.";
                                else if (!string.IsNullOrEmpty(Obj_Empresa.Email))
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

            }
            return Json_Resultado;
        }

        /// <summary>
        /// Método que realiza la búsqueda de proveedores.
        /// </summary>
        /// <returns>Listado serializado con las proveedores según los filtros aplícados</returns>
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string Consultar_Empresas_Por_Filtros(string jsonObject)
        {
            Cls_Apl_Cat_Empresas_Negocio Obj_Empresas = null;
            string Json_Resultado = string.Empty;
            List<Object> Lista_Empresas = new List<Object>();

            try
            {
                Obj_Empresas = JsonMapper.ToObject<Cls_Apl_Cat_Empresas_Negocio>(jsonObject);

                using (var dbContext = new Sistema_TrazabilidadEntities())
                {
                    var Empresas = (from _empresas in dbContext.Apl_Empresas
                                    join
_estatus in dbContext.Tra_Cat_Estatus on _empresas.Estatus_ID equals _estatus.Estatus_ID
                                    where
                                    (!string.IsNullOrEmpty(Obj_Empresas.Nombre) ? _empresas.Nombre.ToLower().Contains(Obj_Empresas.Nombre.ToLower()) : true) &&
                                    (!string.IsNullOrEmpty(Obj_Empresas.Clave) ? _empresas.Clave.ToLower().Contains(Obj_Empresas.Clave.ToLower()) : true) &&
                                    ((Obj_Empresas.Estatus_ID != 0) ? _empresas.Estatus_ID.Equals(Obj_Empresas.Estatus_ID) : true)

                                    select new
                                    {
                                        Empresa_ID = _empresas.Empresa_ID,
                                        Nombre = _empresas.Nombre,
                                        Clave = _empresas.Clave,
                                        Comentarios = _empresas.Comentarios,
                                        Entidad_Empresa_ID = _empresas.Entidad_Empresa_ID,
                                        Direccion = _empresas.Direccion,
                                        Colonia = _empresas.Colonia,
                                        RFC = _empresas.RFC,
                                        Codigo_Postal_ID = _empresas.Codigo_Postal_ID,
                                        CP = _empresas.CP,
                                        Ciudad_ID = _empresas.Ciudad_ID,
                                        Ciudad = _empresas.Ciudad,
                                        Estado_ID = _empresas.Estado_ID,
                                        Estado = _empresas.Estado,
                                        Telefono = _empresas.Telefono,
                                        Fax = _empresas.Fax,
                                        Email = _empresas.Email,
                                        Estatus_ID = _empresas.Estatus_ID,
                                        Estatus = _estatus.Estatus,
                                        Nombre_Carpeta = _empresas.Nombre_Carpeta,
                                        Pais = _empresas.Pais,
                                        Pais_ID = _empresas.Pais_ID,
                                        Localidad_ID = _empresas.Localidad_ID,
                                        Localidad = _empresas.Localidad,
                                        Municipio_Localidad_ID = _empresas.Municipio_Localidad_ID,

                                    }).OrderByDescending(u => u.Empresa_ID);

                    foreach (var p in Empresas)
                        Lista_Empresas.Add(p);

                    Json_Resultado = JsonMapper.ToJson(Lista_Empresas);
                }
            }
            catch (Exception Ex)
            {

            }
            return Json_Resultado;
        }

        //Método que crea una sucursal
        private void Alta_Sucursal(int Parametro_Empresa_ID, int Parametro_Estatus_ID, String Parametro_Email, string Empresa)
        {
            string Json_Resultado = string.Empty;
            Cls_Mensaje Mensaje = new Cls_Mensaje();
            try
            {
                Mensaje.Titulo = "Alta registro";

                using (var dbContext = new Sistema_TrazabilidadEntities())
                {
                    var _sucursal = new Apl_Sucursales();
                    _sucursal.Empresa_ID = Parametro_Empresa_ID;
                    _sucursal.Nombre = "ADMIN " + Empresa.Trim();
                    _sucursal.Clave = "ADMIN";
                    _sucursal.Estatus_ID = Parametro_Estatus_ID;
                    _sucursal.Email = Parametro_Email;//"Admin";
                    _sucursal.Usuario_Creo = Cls_Sesiones.Datos_Usuario.Usuario;
                    _sucursal.Fecha_Creo = new DateTime?(DateTime.Now).Value;

                    dbContext.Apl_Sucursales.Add(_sucursal);
                    dbContext.SaveChanges();
                    Mensaje.Estatus = "success";
                    Mensaje.Mensaje = "La operación se completo sin problemas.";
                    Alta_Sucursal_Roles(_sucursal.Empresa_ID, _sucursal.Sucursal_ID, Parametro_Estatus_ID, Empresa);
                    Alta_Usuario(_sucursal.Empresa_ID, Parametro_Estatus_ID, _sucursal.Sucursal_ID, Parametro_Email, Empresa);
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
            }
            finally
            {
                Json_Resultado = JsonMapper.ToJson(Mensaje);
            }

        }
        //Método que hace una copia de los roles de la empresa 1 para que tambien existan para la empresa actual
        //y los asigna en la relacion sucursal roles
        private void Alta_Sucursal_Roles(int Parametro_Empresa_ID, int Parametro_Sucursal_ID, int Parametro_Estatus_ID, String Empresa)
        {
            string Json_Resultado = string.Empty;
            Cls_Mensaje Mensaje = new Cls_Mensaje();

            try
            {
                Mensaje.Titulo = "Alta registro";

                using (var dbContext4 = new Sistema_TrazabilidadEntities())
                {
                    int Nivel_ID = dbContext4.Apl_Niveles.Select(x => x.Nivel_ID).First();
                    var _roles = new Apl_Roles();
                    _roles.Nombre = "ADMIN " + Empresa;
                    _roles.Nivel_ID = Nivel_ID;
                    _roles.Estatus_ID = Parametro_Estatus_ID;
                    _roles.Empresa_ID = Parametro_Empresa_ID;
                    _roles.Sucursal_ID = Parametro_Sucursal_ID;
                    _roles.Default_Admin_Empresa = "S";
                    _roles.Tipo = "WEB";

                    dbContext4.Apl_Roles.Add(_roles);
                    dbContext4.SaveChanges();


                    var _sucursal_rol = new Apl_Roles_Sucursales();
                    _sucursal_rol.Empresa_ID = Parametro_Empresa_ID;
                    _sucursal_rol.Sucursal_ID = Parametro_Sucursal_ID;
                    _sucursal_rol.Rol_ID = _roles.Rol_ID;

                    using (var dbContext2 = new Sistema_TrazabilidadEntities())
                    {
                        dbContext2.Apl_Roles_Sucursales.Add(_sucursal_rol);
                        dbContext2.SaveChanges();
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
                else
                    Mensaje.Mensaje = "Informe técnico: " + Ex.Message;
            }
            finally
            {
                Json_Resultado = JsonMapper.ToJson(Mensaje);
            }
        }


        //Método que crea una sucursal
        private void Alta_acceso_rol(int Parametro_Rol_ID)
        {
            string Json_Resultado = string.Empty;
            Cls_Mensaje Mensaje = new Cls_Mensaje();
            try
            {
                Mensaje.Titulo = "Alta registro";

                using (var dbContext = new Sistema_TrazabilidadEntities())
                {

                    var _accesos = (from _acceso in dbContext.Apl_Menus
                                    select _acceso
                                  );
                    using (var dbContext2 = new Sistema_TrazabilidadEntities())
                    {

                        foreach (var p in _accesos)
                        {

                            var _roles_accesos = new Apl_Accesos();
                            _roles_accesos.Rol_ID = Parametro_Rol_ID;
                            _roles_accesos.Menu_ID = p.Menu_ID;
                            _roles_accesos.Estatus_ID = p.Estatus_ID;
                            _roles_accesos.Habilitado = "S";
                            _roles_accesos.Alta = "S";
                            _roles_accesos.Cambio = "S";
                            _roles_accesos.Eliminar = "S";
                            _roles_accesos.Consultar = "S";
                            _roles_accesos.Usuario_Creo = Cls_Sesiones.Usuario;
                            _roles_accesos.Fecha_Creo = new DateTime?(DateTime.Now).Value;
                            dbContext2.Apl_Accesos.Add(_roles_accesos);
                            dbContext2.SaveChanges();
                        }
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
                else
                    Mensaje.Mensaje = "Informe técnico: " + Ex.Message;
            }
            finally
            {
                Json_Resultado = JsonMapper.ToJson(Mensaje);
            }

        }

        //Método que da de alta el usuario administrador de la emrpresa creada
        private void Alta_Usuario(int Parametro_Empresa_ID, int Parametro_Estatus_ID, int Parametro_Sucursal_ID, String Parametro_Email, string Empresa)
        {
            string Json_Resultado = string.Empty;
            Cls_Mensaje Mensaje = new Cls_Mensaje();

            try
            {
                Mensaje.Titulo = "Alta registro";
                DateTime date = DateTime.Now.AddMonths(5);
                using (var dbContext3 = new Sistema_TrazabilidadEntities())
                {
                    int Tipo_usuario_id = dbContext3.Apl_Tipos_Usuarios.Select(u => u.Tipo_Usuario_ID).First();

                    using (var dbContext = new Sistema_TrazabilidadEntities())
                    {
                        //alta del usuario administrador
                        var _usuarios = new Apl_Usuarios();
                        _usuarios.Empresa_ID = Parametro_Empresa_ID;
                        _usuarios.Estatus_ID = Parametro_Estatus_ID;
                        _usuarios.Tipo_Usuario_ID = Tipo_usuario_id;
                        _usuarios.Sucursal_ID = Parametro_Sucursal_ID;
                        _usuarios.Usuario = Empresa; //+ Parametro_Empresa_ID;
                        _usuarios.Password = Cls_Seguridad.Encriptar("123456z$");
                        //_usuarios.No_Intentos_Recuperar = "9";
                        _usuarios.Email = Parametro_Email;
                        //_usuarios.Fecha_Expira_Contrasenia = date;
                        _usuarios.Usuario_Creo = Cls_Sesiones.Datos_Usuario.Usuario;
                        _usuarios.Fecha_Creo = new DateTime?(DateTime.Now).Value;
                        _usuarios.Fecha_Token = date;
                        dbContext.Apl_Usuarios.Add(_usuarios);
                        dbContext.SaveChanges();

                        //Se trae el ID del rol SUPER-ADMINISTRADOR perteneciente a la sucursal y empresa actual
                        var Rol = dbContext3.Apl_Roles.Where(u => (u.Nombre == "ADMIN " + Empresa) &&
                        (u.Empresa_ID == Parametro_Empresa_ID) && (u.Sucursal_ID == Parametro_Sucursal_ID) && (u.Estatus_ID == Parametro_Estatus_ID)).First();

                        //dar de alta la relacion usuario y rol
                        var _roles_usuarios = new Apl_Rel_Usuarios_Roles();
                        _roles_usuarios.Empresa_ID = Parametro_Empresa_ID;
                        _roles_usuarios.Sucursal_ID = Parametro_Sucursal_ID;
                        _roles_usuarios.Usuario_ID = _usuarios.Usuario_ID;
                        _roles_usuarios.Rol_ID = Rol.Rol_ID;
                        dbContext.Apl_Rel_Usuarios_Roles.Add(_roles_usuarios);
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
            }
            finally
            {
                Json_Resultado = JsonMapper.ToJson(Mensaje);
            }

        }

        /// <summary>
        /// Método para consultar los estatus.
        /// </summary>
        /// <returns>Listado serializado de los estatus</returns>
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
                    var Estatus = from _empresas in dbContext.Tra_Cat_Estatus
                                  select new { _empresas.Estatus, _empresas.Estatus_ID };


                    Json_Resultado = JsonMapper.ToJson(Estatus.ToList());


                }
            }
            catch (Exception Ex)
            {
            }
            return Json_Resultado;
        }

        /// <summary>
        /// Método para consultar los estatus.
        /// </summary>
        /// <returns>Listado serializado de los estatus</returns>
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string ConsultarFiltroEstatus()
        {
            string Json_Resultado = string.Empty;
            List<Tra_Cat_Estatus> Lista_fase = new List<Tra_Cat_Estatus>();

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
            }
            return Json_Resultado;
        }


        /// <summary>
        /// Método para consultar los estatus.
        /// </summary>
        /// <returns>Listado serializado de los estatus</returns>
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string Consultar_Entidad_Empresa()
        {
            string Json_Resultado = string.Empty;
            List<Apl_Entidades_Empresas> Lista_estatus = new List<Apl_Entidades_Empresas>();
            try
            {
                using (var dbContext = new Sistema_TrazabilidadEntities())
                {
                    var Entidad = from _entidad in dbContext.Apl_Entidades_Empresas
                                  select new { _entidad.Nombre, _entidad.Entidad_Empresa_ID };
                    Json_Resultado = JsonMapper.ToJson(Entidad.ToList());
                }
            }
            catch (Exception Ex)
            {
            }
            return Json_Resultado;
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void Consultar_Paises()
        {
            string Json_Resultado = string.Empty;
            List<Cls_Select2> Lst_Combo = new List<Cls_Select2>();

            try
            {
                string q = string.Empty;
                NameValueCollection nvc = Context.Request.Form;

                if (!String.IsNullOrEmpty(nvc["q"]))
                    q = nvc["q"].ToString().Trim();

                using (var dbContext = new Sistema_TrazabilidadEntities())
                {
                    var Paises = (from _paises in dbContext.Cat_Nom_Paises
                                  where (string.IsNullOrEmpty(q) ? true : _paises.Nombre.Contains(q))

                                  select new Cls_Select2
                                  {
                                      id = _paises.Pais_ID.ToString(),
                                      text = _paises.Nombre
                                  });

                    foreach (var p in Paises)
                        Lst_Combo.Add(p);

                    Json_Resultado = JsonMapper.ToJson(Lst_Combo);
                }
            }
            catch (Exception Ex)
            {
                //ErrorSignal.FromCurrentContext().Raise(Ex);
            }
            finally
            {
                Context.Response.Write(Json_Resultado);
            }
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void Consultar_Estados()
        {
            string Json_Resultado = string.Empty;
            List<Cls_Select2> Lst_Combo = new List<Cls_Select2>();

            try
            {
                string q = string.Empty;
                NameValueCollection nvc = Context.Request.Form;

                if (!String.IsNullOrEmpty(nvc["q"]))
                    q = nvc["q"].ToString().Trim();

                using (var dbContext = new Sistema_TrazabilidadEntities())
                {
                    var Estados = (from _estados in dbContext.Cat_Nom_Estados
                                   where (string.IsNullOrEmpty(q) ? true : _estados.Nombre.Contains(q))

                                   select new Cls_Select2
                                   {
                                       id = _estados.Estado_ID.ToString(),
                                       text = _estados.Nombre
                                   });

                    foreach (var p in Estados)
                        Lst_Combo.Add(p);

                    Json_Resultado = JsonMapper.ToJson(Lst_Combo);
                }
            }
            catch (Exception Ex)
            {
                //ErrorSignal.FromCurrentContext().Raise(Ex);
            }
            finally
            {
                Context.Response.Write(Json_Resultado);
            }
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void Consultar_Localidades()
        {
            string Json_Resultado = string.Empty;
            List<Cls_Select2> Lst_Combo = new List<Cls_Select2>();

            try
            {
                string q = string.Empty;
                string estado = string.Empty;
                NameValueCollection nvc = Context.Request.Form;

                if (!String.IsNullOrEmpty(nvc["q"]))
                    q = nvc["q"].ToString().Trim();

                if (!String.IsNullOrEmpty(nvc["estado"]))
                    estado = nvc["estado"].ToString().Trim();

                using (var dbContext = new Sistema_TrazabilidadEntities())
                {
                    var Localidades = (from _localidades in dbContext.Cat_Nom_Localidades
                                       where (string.IsNullOrEmpty(q) ? true : _localidades.Nombre.Contains(q)) &&
                                        (string.IsNullOrEmpty(estado) ? false : _localidades.Estado_ID.ToString() == estado)

                                       select new Cls_Select2
                                       {
                                           id = _localidades.Localidad_ID.ToString(),
                                           text = _localidades.Nombre
                                       });

                    foreach (var p in Localidades)
                        Lst_Combo.Add(p);

                    Json_Resultado = JsonMapper.ToJson(Lst_Combo);
                }
            }
            catch (Exception Ex)
            {
                //ErrorSignal.FromCurrentContext().Raise(Ex);
            }
            finally
            {
                Context.Response.Write(Json_Resultado);
            }
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void Consultar_Codigos_Postales()
        {
            string Json_Resultado = string.Empty;
            List<Cls_Select2> Lst_Combo = new List<Cls_Select2>();

            try
            {
                string q = string.Empty;
                string estado = string.Empty;
                NameValueCollection nvc = Context.Request.Form;

                if (!String.IsNullOrEmpty(nvc["q"]))
                    q = nvc["q"].ToString().Trim();

                if (!String.IsNullOrEmpty(nvc["estado"]))
                    estado = nvc["estado"].ToString().Trim();
                using (var dbContext = new Sistema_TrazabilidadEntities())
                {
                    var Codigos = (from _codigos in dbContext.Cat_Nom_Codigos_Postales
                                   where (string.IsNullOrEmpty(q) ? true : _codigos.Codigo_Postal.Contains(q))
                                   && (string.IsNullOrEmpty(estado) ? false : _codigos.Estado_ID.ToString() == estado)
                                   select new Cls_Select2
                                   {
                                       id = _codigos.Codigo_Postal_ID.ToString(),
                                       text = _codigos.Codigo_Postal
                                   });

                    foreach (var p in Codigos)
                        Lst_Combo.Add(p);

                    Json_Resultado = JsonMapper.ToJson(Lst_Combo);
                }
            }
            catch (Exception Ex)
            {
                //ErrorSignal.FromCurrentContext().Raise(Ex);
            }
            finally
            {
                Context.Response.Write(Json_Resultado);
            }
        }


        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void Consultar_Municipio_Localidad()
        {
            string Json_Resultado = string.Empty;
            List<Cls_Select2> Lst_Combo = new List<Cls_Select2>();

            try
            {
                string q = string.Empty;
                string estado = string.Empty;
                NameValueCollection nvc = Context.Request.Form;

                if (!String.IsNullOrEmpty(nvc["q"]))
                    q = nvc["q"].ToString().Trim();
                if (!String.IsNullOrEmpty(nvc["estado"]))
                    estado = nvc["estado"].ToString().Trim();
                using (var dbContext = new Sistema_TrazabilidadEntities())
                {
                    var MuniLocalidades = (from _municipios_loc in dbContext.Cat_Nom_Municipios_Localidades
                                           where (string.IsNullOrEmpty(q) ? true : _municipios_loc.Nombre.Contains(q))
                                           && (string.IsNullOrEmpty(estado) ? false : _municipios_loc.Estado_ID.ToString() == estado)

                                           select new Cls_Select2
                                           {
                                               id = _municipios_loc.Municipio_Localidad_ID.ToString(),
                                               text = _municipios_loc.Nombre
                                           });

                    foreach (var p in MuniLocalidades)
                        Lst_Combo.Add(p);

                    Json_Resultado = JsonMapper.ToJson(Lst_Combo);
                }
            }
            catch (Exception Ex)
            {
                //ErrorSignal.FromCurrentContext().Raise(Ex);
            }
            finally
            {
                Context.Response.Write(Json_Resultado);
            }
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void Consultar_Ciudades()
        {
            string Json_Resultado = string.Empty;
            List<Cls_Select2> Lst_Combo = new List<Cls_Select2>();

            try
            {
                string q = string.Empty;
                NameValueCollection nvc = Context.Request.Form;

                if (!String.IsNullOrEmpty(nvc["q"]))
                    q = nvc["q"].ToString().Trim();

                using (var dbContext = new Sistema_TrazabilidadEntities())
                {
                    var Ciudades = (from _ciudades in dbContext.Cat_Nom_Ciudades
                                    where (string.IsNullOrEmpty(q) ? true : _ciudades.Nombre.Contains(q))

                                    select new Cls_Select2
                                    {
                                        id = _ciudades.Ciudad_ID.ToString(),
                                        text = _ciudades.Nombre
                                    });

                    foreach (var p in Ciudades)
                        Lst_Combo.Add(p);

                    Json_Resultado = JsonMapper.ToJson(Lst_Combo);
                }
            }
            catch (Exception Ex)
            {
                //ErrorSignal.FromCurrentContext().Raise(Ex);
            }
            finally
            {
                Context.Response.Write(Json_Resultado);
            }
        }
        #endregion
    }
}

