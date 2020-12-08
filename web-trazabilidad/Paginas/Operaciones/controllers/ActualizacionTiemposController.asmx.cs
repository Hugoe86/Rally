using datos_trazabilidad;
using Elmah;
using LitJson;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.Script.Services;
using System.Web.Services;
using web_trazabilidad.Models.Ayudante;
using web_trazabilidad.Models.Negocio;
using web_trazabilidad.Models.Negocio.Catalogos;
using web_trazabilidad.Models.Negocio.Operaciones;
namespace web_trazabilidad.Paginas.Operaciones.controllers
{
    /// <summary>
    /// Summary description for ActualizacionTiemposController
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]

    public class ActualizacionTiemposController : System.Web.Services.WebService
    {

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string Actalizar_Hora_Registro(String jsonObject)
        {
            Cls_Mensaje Mensaje = new Cls_Mensaje();
            Cls_Ope_Evento_Registro_Tiempo_Negocio Obj_Registro = new Cls_Ope_Evento_Registro_Tiempo_Negocio();

            string jsonResultado = "";


            try
            {
                Mensaje.Titulo = "Modificar";

                Obj_Registro = JsonConvert.DeserializeObject<Cls_Ope_Evento_Registro_Tiempo_Negocio>(jsonObject);


                using (var dbContext = new Sistema_TrazabilidadEntities())
                {
                    Ope_Eventos_Registro_Tiempo Registro = new Ope_Eventos_Registro_Tiempo();
                    Registro = dbContext.Ope_Eventos_Registro_Tiempo.Where(w => w.Registro_Id == Obj_Registro.Registro_Id).FirstOrDefault();
                    
                    Registro.Tiempo_Real = Obj_Registro.Tiempo_Real;
                    Registro.Motivo_Cambio = Obj_Registro.Motivo_Cambio;
                    Registro.Usuario_Modifica_Id = Convert.ToInt32(Cls_Sesiones.Usuario_ID);

                    Registro.Usuario_Modifico = Cls_Sesiones.Usuario;
                    Registro.Fecha_Modifico = DateTime.Now;


                    dbContext.SaveChanges();


                    //  se calculan la puntuacion.
                    ////<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
                    ////<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
                    #region Tiempos 
                    var _registros = (from _tiempo in dbContext.Ope_Eventos_Registro_Tiempo
                                      where _tiempo.Registro_Id == Obj_Registro.Registro_Id
                                      select new Cls_Ope_Evento_Registro_Tiempo_Negocio
                                      {
                                          Registro_Id = _tiempo.Registro_Id,
                                          Tiempo_Ideal = _tiempo.Tiempo_Ideal,
                                          Tiempo_Real = _tiempo.Tiempo_Real,
                                      }).OrderBy(o => o.Registro_Id);

                    if (_registros.Any())
                    {
                        Cls_Calcular_Tiempo.Calcular_Tiempos(_registros.ToList());
                    }
                    #endregion   

                    //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
                    //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

                    Mensaje.Mensaje = "La operación se realizo correctamente.";
                    Mensaje.Estatus = "success";

                }
            }
            catch (Exception e)
            {

                Mensaje.Mensaje = "Error Técnico. " + e.Message;
                Mensaje.Estatus = "error";
            }
            finally
            {
                jsonResultado = JsonMapper.ToJson(Mensaje);
            }

            return jsonResultado;
        }


        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string Consultar_Tiempos(string jsonObject)
        {
            string Json_Resultado = string.Empty;
            Cls_Ope_Evento_Registro_Tiempo_Negocio Obj = new Cls_Ope_Evento_Registro_Tiempo_Negocio();

            try
            {

                Obj = JsonConvert.DeserializeObject<Cls_Ope_Evento_Registro_Tiempo_Negocio>(jsonObject);


                using (var dbContext = new Sistema_TrazabilidadEntities())
                {
                    var _Tiempos = (from _reg in dbContext.Ope_Eventos_Registro_Tiempo

                                        //  evento
                                    join _evento in dbContext.Ope_Eventos on _reg.Evento_Id equals _evento.Evento_Id

                                    //  jornada
                                    join _jor in dbContext.Ope_Eventos_Jornadas on _reg.Jornada_Id equals _jor.Jornada_Id

                                    //  punto de control
                                    join _punto in dbContext.Ope_Eventos_Puntos_Control on _reg.Punto_Control_Id equals _punto.Punto_Control_Id

                                    //  vehiculo
                                    join _veh_part in dbContext.Ope_Eventos_Vehiculo_Participante on _reg.Vehiculo_Participante_Id equals _veh_part.Vehiculo_Participante_Id
                                    join _veh in dbContext.Cat_Vehiculos on _veh_part.Vehiculo_Id equals _veh.Vehiculo_Id

                                    //  usuario
                                    join _usuario in dbContext.Apl_Usuarios on _reg.Usuario_Modifica_Id equals _usuario.Usuario_ID into _usua
                                    from _usuario in _usua.DefaultIfEmpty()


                                    where _reg.Evento_Id == (Obj.Evento_Id)
                                        && _reg.Jornada_Id == (Obj.Jornada_Id)
                                        && _reg.Vehiculo_Participante_Id == (Obj.Vehiculo_Participante_Id)



                                    select new Cls_Ope_Evento_Registro_Tiempo_Negocio
                                    {
                                        Registro_Id = _reg.Registro_Id,

                                        Punto_Control = _punto.Ubicacion,
                                        Str_Tiempo_Real = _reg.Tiempo_Real.ToString(),
                                        Usuario_Modifica_Registro = _usuario.Nombre,
                                        Motivo_Cambio = _reg.Motivo_Cambio,
                                        Clave_Punto_Control = _punto.Clave,
                                    }
                                    ).OrderBy(x => x.Clave_Punto_Control).ToList();


                   
                    Json_Resultado = JsonMapper.ToJson(_Tiempos.ToList());
                }
            }
            catch (Exception e)
            {

            }

            return Json_Resultado;
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void Consultar_participantes_Vehiculo_Combo()
        {
            string Json_Resultado = string.Empty;
            List<Cls_Select2> Lst_Combo = new List<Cls_Select2>();

            try
            {

                string q = string.Empty;
                string evento_id = string.Empty;

                NameValueCollection nvc = Context.Request.Form;

                if (!String.IsNullOrEmpty(nvc["q"]))
                {
                    q = nvc["q"].ToString().Trim();
                }
                if (!String.IsNullOrEmpty(nvc["evento_id"]))
                {
                    evento_id = nvc["evento_id"].ToString().Trim();
                }

              
                using (var dbContext = new Sistema_TrazabilidadEntities())
                {

                    var _Participantes = (from _part_veh in dbContext.Ope_Eventos_Vehiculo_Participante


                                          join _veh in dbContext.Cat_Vehiculos on _part_veh.Vehiculo_Id equals _veh.Vehiculo_Id

                                          where _part_veh.Estatus == "ACTIVO"

                                                && _part_veh.Evento_Id.ToString() == evento_id

                                                && (_part_veh.Numero_Participante.ToString().Contains(q)
                                                        || _veh.NS.Contains(q)
                                                        || _veh.Modelo.Contains(q)
                                                        || _veh.Marca.Contains(q))

                                          select new Cls_Select2
                                          {
                                              id = _part_veh.Vehiculo_Participante_Id.ToString(),
                                              text = "[" + _part_veh.Numero_Participante + "] [" + _veh.NS + "] [" + _veh.Marca + "] [" + _veh.Modelo + "]",
                                              detalle_1 = _veh.Vehiculo_Id.ToString(),
                                              detalle_2 = _veh.Color_Hex_Rgb,
                                              detalle_3 = _veh.NS + " - " + _veh.Marca + " - " + _veh.Modelo,
                                              detalle_7 = _part_veh.Numero_Participante,
                                          }).OrderBy(o => o.detalle_7);


                    Json_Resultado = JsonMapper.ToJson(_Participantes.ToList());
                }
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




    }
}
