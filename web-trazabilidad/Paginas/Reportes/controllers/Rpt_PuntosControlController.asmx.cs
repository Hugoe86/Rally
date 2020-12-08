using datos_trazabilidad;
using LitJson;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using web_trazabilidad.Models.Negocio.Operaciones;

namespace web_trazabilidad.Paginas.Reportes.controllers
{
    /// <summary>
    /// Summary description for Rpt_PuntosControlController
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class Rpt_PuntosControlController : System.Web.Services.WebService
    {

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string Consultar_Puntos_Control(string jsonObject)
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

                                    // categoria
                                    join _categoria in dbContext.Ope_Eventos_Categorias on _veh_part.Categoria_Participante_Id equals _categoria.Categoria_Id

                                    //  usuario
                                    join _usuario in dbContext.Apl_Usuarios on _reg.Usuario_Modifica_Id equals _usuario.Usuario_ID into _usua
                                    from _usuario in _usua.DefaultIfEmpty()


                                    where _reg.Evento_Id == (Obj.Evento_Id)
                                        && _reg.Jornada_Id == (Obj.Jornada_Id)
                                        && _reg.Punto_Control_Id == (Obj.Punto_Control_Id)


                                    select new Cls_Ope_Evento_Registro_Tiempo_Negocio
                                    {
                                        Registro_Id = _reg.Registro_Id,

                                        No_Vehiculo = _veh_part.Numero_Participante,
                                        Categoria = _categoria.Nombre,
                                        Str_Tiempo_Real = _reg.Tiempo_Real.ToString()??"00:00:00",
                                        str_Tiempo_Ideal = _reg.Tiempo_Ideal.ToString(),
                                        Puntuacion = _reg.Puntuacion ?? 0,
                                        Observaciones = _reg.Motivo_Cambio
                                    }
                                    ).OrderBy(x => x.No_Vehiculo).ToList();



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
        public string Consultar_Datos_Puntos_Control(string jsonObject)
        {
            string Json_Resultado = string.Empty;
            Cls_Ope_Evento_Registro_Tiempo_Negocio Obj = new Cls_Ope_Evento_Registro_Tiempo_Negocio();

            try
            {
                Obj = JsonConvert.DeserializeObject<Cls_Ope_Evento_Registro_Tiempo_Negocio>(jsonObject);

                using (var dbContext = new Sistema_TrazabilidadEntities())
                {
                    var _datos = (from _ep in dbContext.Ope_Eventos_Puntos_Control

                                        //  operadores del punto de control
                                    join _eop in dbContext.Ope_Eventos_Pnt_Ctrl_Operador on _ep.Punto_Control_Id equals _eop.Punto_Control_Id

                                    //  responsable
                                    join _r in dbContext.Cat_Responsables on _eop.Responsable_Id equals _r.Responsable_Id

                                    where _eop.Cargo == "RESPONSABLE"
                                    && _ep.Punto_Control_Id == Obj.Punto_Control_Id

                                    select new
                                    {
                                        Responsable = _r.Nombre,
                                        Ubicacion = _ep.Ubicacion,
                                        Tiempo_Ideal = _ep.Tiempo_Ideal.ToString(),
                                        Intervalo = _ep.Intervalo.ToString(),
                                    }
                                    ).OrderBy(x => x.Responsable).ToList();



                    Json_Resultado = JsonMapper.ToJson(_datos.ToList());
                }
            }
            catch (Exception e)
            {

            }

            return Json_Resultado;
        }
    }
}
