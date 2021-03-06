﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using admin_trazabilidad.Models.Negocio;
using LitJson;
using datos_trazabilidad;
using web_trazabilidad.Models.Negocio;
using web_trazabilidad.Models.Ayudante;
using Elmah;

namespace web_trazabilidad.Paginas.Catalogos.controller
{
    /// <summary>
    /// Summary description for Tipos_Usuarios_Controller
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class Tipos_Usuarios_Controller : System.Web.Services.WebService
    {
        #region Metodos
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string Consultar_Tipos_Usuarios_Por_Nombre(string jsonObject)
        {
            Cls_Apl_Tipos_Usuarios_Negocio ObjTipos_usuarios = null;
            string Json_Resultado = string.Empty;
            List<Cls_Apl_Tipos_Usuarios_Negocio> Lista_Tipos_Usuarios = new List<Cls_Apl_Tipos_Usuarios_Negocio>();
            Cls_Mensaje Mensaje = new Cls_Mensaje();

            try
            {
                Mensaje.Titulo = "Validaciones";
                ObjTipos_usuarios = JsonMapper.ToObject<Cls_Apl_Tipos_Usuarios_Negocio>(jsonObject);

                using (var dbContext = new Sistema_TrazabilidadEntities())
                {
                    var _tipos_usuarios = (from _select in dbContext.Apl_Tipos_Usuarios
                                     where 
                                     _select.Nombre.Equals(ObjTipos_usuarios.Nombre)
                                     select new Cls_Apl_Tipos_Usuarios_Negocio
                                     {
                                         Tipo_Usuario_ID = _select.Tipo_Usuario_ID,
                                         Nombre = _select.Nombre
                                     }).OrderByDescending(u => u.Tipo_Usuario_ID);

                    if (_tipos_usuarios.Any())
                    {
                        if (ObjTipos_usuarios.Tipo_Usuario_ID == 0)
                        {
                            Mensaje.Estatus = "error";
                            if (!string.IsNullOrEmpty(ObjTipos_usuarios.Nombre))
                                Mensaje.Mensaje = "El nombre ingresado ya se encuentra registrado.";
                        }
                        else
                        {
                            var item_edit = _tipos_usuarios.Where(u => u.Tipo_Usuario_ID == ObjTipos_usuarios.Tipo_Usuario_ID);

                            if (item_edit.Count() == 1)
                                Mensaje.Estatus = "success";
                            else
                            {
                                Mensaje.Estatus = "error";
                                if (!string.IsNullOrEmpty(ObjTipos_usuarios.Nombre))
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

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string Consultar_Tipos_Usuarios_Por_Filtros(string jsonObject)
        {
            Cls_Apl_Tipos_Usuarios_Negocio objTipos_Usuarios = null;
            string Json_Resultado = string.Empty;
            List<Cls_Apl_Tipos_Usuarios_Negocio> Lista_tipos_usuarios = new List<Cls_Apl_Tipos_Usuarios_Negocio>();

            try
            {
                objTipos_Usuarios = JsonMapper.ToObject<Cls_Apl_Tipos_Usuarios_Negocio>(jsonObject);

                using (var dbContext = new Sistema_TrazabilidadEntities())
                {
                    var _tipos_usuarios = (from _select in dbContext.Apl_Tipos_Usuarios
                                    where
                                        ((_select.Nombre.ToLower().Contains(objTipos_Usuarios.Nombre.ToLower()) && !string.IsNullOrEmpty(objTipos_Usuarios.Nombre)) ||

                                        (string.IsNullOrEmpty(objTipos_Usuarios.Nombre)))

                                    select new Cls_Apl_Tipos_Usuarios_Negocio
                                    {
                                        Tipo_Usuario_ID = _select.Tipo_Usuario_ID,
                                        Nombre = _select.Nombre,
                                       
                                    }).OrderByDescending(u => u.Tipo_Usuario_ID);

                    foreach (var p in _tipos_usuarios)
                        Lista_tipos_usuarios.Add((Cls_Apl_Tipos_Usuarios_Negocio)p);

                    Json_Resultado = JsonMapper.ToJson(Lista_tipos_usuarios);
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
            Cls_Apl_Tipos_Usuarios_Negocio ObjTipos_Usuarios = null;
            string Json_Resultado = string.Empty;
            Cls_Mensaje Mensaje = new Cls_Mensaje();

            try
            {
                Mensaje.Titulo = "Alta registro";
                ObjTipos_Usuarios = JsonMapper.ToObject<Cls_Apl_Tipos_Usuarios_Negocio>(jsonObject);

                using (var dbContext = new Sistema_TrazabilidadEntities())
                {
                    var _tipos_usuarios = new Apl_Tipos_Usuarios();
                  
                    _tipos_usuarios.Nombre = ObjTipos_Usuarios.Nombre;
                    _tipos_usuarios.Usuario_Creo = Cls_Sesiones.Datos_Usuario.Usuario;
                    _tipos_usuarios.Fecha_Creo = new DateTime?(DateTime.Now).Value;

                    dbContext.Apl_Tipos_Usuarios.Add(_tipos_usuarios);
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

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string Actualizar(string jsonObject)
        {
            Cls_Apl_Tipos_Usuarios_Negocio ObjTipos_Usuarios = null;
            string Json_Resultado = string.Empty;
            Cls_Mensaje Mensaje = new Cls_Mensaje();

            try
            {
                Mensaje.Titulo = "Actualizar registro";
                ObjTipos_Usuarios = JsonMapper.ToObject<Cls_Apl_Tipos_Usuarios_Negocio>(jsonObject);

                using (var dbContext = new Sistema_TrazabilidadEntities())
                {
                    var _tipos_usuarios = dbContext.Apl_Tipos_Usuarios.Where(u => u.Tipo_Usuario_ID == ObjTipos_Usuarios.Tipo_Usuario_ID).First();

                    _tipos_usuarios.Nombre = ObjTipos_Usuarios.Nombre;
                    _tipos_usuarios.Usuario_Modifico = Cls_Sesiones.Datos_Usuario.Usuario;
                    _tipos_usuarios.Fecha_Modifico = new DateTime?(DateTime.Now);

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

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string Eliminar(string jsonObject)
        {
            Cls_Apl_Tipos_Usuarios_Negocio ObjTipos_Usuarios = null;
            string Json_Resultado = string.Empty;
            Cls_Mensaje Mensaje = new Cls_Mensaje();

            try
            {
                Mensaje.Titulo = "Eliminar registro";
                ObjTipos_Usuarios = JsonMapper.ToObject<Cls_Apl_Tipos_Usuarios_Negocio>(jsonObject);

                using (var dbContext = new Sistema_TrazabilidadEntities())
                {
                    var _tipos_usuarios = dbContext.Apl_Tipos_Usuarios.Where(u => u.Tipo_Usuario_ID == ObjTipos_Usuarios.Tipo_Usuario_ID).First();
                    dbContext.Apl_Tipos_Usuarios.Remove(_tipos_usuarios);
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

        #endregion
    }
}
