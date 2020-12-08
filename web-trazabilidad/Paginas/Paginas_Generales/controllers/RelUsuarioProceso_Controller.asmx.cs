using datos_trazabilidad;
using LitJson;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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
    /// Descripción breve de RelUsuarioProceso_Controller
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    [System.Web.Script.Services.ScriptService]
    public class RelUsuarioProceso_Controller : System.Web.Services.WebService
    {

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void Consultar_Usuarios()
        {
            string Json_Resultado = string.Empty;
            List<Cls_Select2> Lst_Usuarios = new List<Cls_Select2>();

            try
            {
                string q = string.Empty;
                string sucursal = string.Empty;
                NameValueCollection nvc = Context.Request.Form;

                if (!String.IsNullOrEmpty(nvc["q"]))
                    q = nvc["q"].ToString().Trim();

                using (var dbContext = new Sistema_TrazabilidadEntities())
                {
                    var _Usuario = from _usuario in dbContext.Apl_Usuarios

                                 where _usuario.Empresa_ID.ToString() == Cls_Sesiones.Empresa_ID &&
                                    _usuario.Sucursal_ID.ToString() == Cls_Sesiones.Sucursal_ID
                                 select new Cls_Select2
                                 {
                                     id = _usuario.Usuario_ID.ToString(),
                                     text = _usuario.Nombre,
                                     detalle_1= _usuario.Email,

                                 };

                    if (_Usuario.Any())
                        foreach (var p in _Usuario)
                            Lst_Usuarios.Add((Cls_Select2)p);

                    Json_Resultado = JsonMapper.ToJson(Lst_Usuarios);
                }
            }
            catch (Exception Ex)
            {
                Json_Resultado = JsonMapper.ToJson(Lst_Usuarios);

            }
            finally
            {
                Context.Response.Write(Json_Resultado);
            }
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string Consultar_Procesos(string jsonObject) {
            string jsonResultado = "[]";
            Cls_Rel_Usuario_Proceso_Negocio UsuarioP = new Cls_Rel_Usuario_Proceso_Negocio();
            try {
                UsuarioP = JsonConvert.DeserializeObject<Cls_Rel_Usuario_Proceso_Negocio>(jsonObject);

                using (Sistema_TrazabilidadEntities dbContext = new Sistema_TrazabilidadEntities())
                {
                    var select = (from _procesos in dbContext.Cat_Procesos_Sistema
                                 join _rel in dbContext.Cat_Rel_Usuarios_Procesos_Sistema
                                     on new { _procesos.Proceso_ID, Usuario_ID = UsuarioP.Usuario_ID } equals new { _rel.Proceso_ID, _rel.Usuario_ID }
                                     into Rel
                                 from _rel in Rel.DefaultIfEmpty()
                                 select new Cls_Rel_Usuario_Proceso_Negocio
                                 {
                                     Proceso_ID = _procesos.Proceso_ID,
                                     Nombre = _procesos.Nombre,
                                     Proceso = _procesos.Proceso ?? "",
                                     Select = string.IsNullOrEmpty(_rel.Relacion_ID.ToString()) ? false : true,
                                     Relacion_ID = string.IsNullOrEmpty(_rel.Relacion_ID.ToString()) ? 0 : _rel.Relacion_ID,
                                     Comentarios = _procesos.Comentarios // dbContext.Cat_Procesos_Sistema.Where(x => x.Proceso_ID == _procesos.Proceso_ID).FirstOrDefault().Comentarios
                                 });

                    jsonResultado = JsonMapper.ToJson(select.ToList());

                }
            } catch (Exception e) {

            }


            return jsonResultado;
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string AltaEliminar(string jsonObject)
        {
            string jsonResultado = "{}";
            Cls_Rel_Usuario_Proceso_Negocio UsuarioP = new Cls_Rel_Usuario_Proceso_Negocio();
            List<Cls_Rel_Usuario_Proceso_Negocio> Datos = new List<Cls_Rel_Usuario_Proceso_Negocio>();
            Cls_Mensaje Mensaje = new Cls_Mensaje();
            try
            {
                Mensaje.Titulo = "Alta/Eliminacion Procesos Usuario";
                UsuarioP = JsonConvert.DeserializeObject<Cls_Rel_Usuario_Proceso_Negocio>(jsonObject);
                Datos = JsonConvert.DeserializeObject<List<Cls_Rel_Usuario_Proceso_Negocio>>(UsuarioP.datos);

                using (Sistema_TrazabilidadEntities dbContext = new Sistema_TrazabilidadEntities())
                {


                    foreach (var item in Datos) {
                        if (item.Select && item.Relacion_ID == 0)
                        {
                            Cat_Rel_Usuarios_Procesos_Sistema Rel = new Cat_Rel_Usuarios_Procesos_Sistema();
                            Rel.Usuario_ID = UsuarioP.Usuario_ID;
                            Rel.Proceso_ID = item.Proceso_ID;
                            Rel.Usuario_Creo = Cls_Sesiones.Usuario;
                            Rel.Fecha_Creo = DateTime.Now;
                            dbContext.Cat_Rel_Usuarios_Procesos_Sistema.Add(Rel);
                        }
                        else  if(!item.Select && item.Relacion_ID != 0){
                            Cat_Rel_Usuarios_Procesos_Sistema Rel = dbContext.Cat_Rel_Usuarios_Procesos_Sistema.Where(x => x.Relacion_ID == item.Relacion_ID).FirstOrDefault();
                            dbContext.Cat_Rel_Usuarios_Procesos_Sistema.Remove(Rel);
                        }


                    }
                    dbContext.SaveChanges();
                    Mensaje.Estatus = "success";
                    Mensaje.Mensaje = "Se completo la operacion correctamente";
                }
            }
            catch (Exception e)
            {
                Mensaje.Estatus = "error";
                Mensaje.Mensaje = "Error Tecnico. " + e.Message;
            }
            jsonResultado = JsonMapper.ToJson(Mensaje);

            return jsonResultado;
        }
    }
}
