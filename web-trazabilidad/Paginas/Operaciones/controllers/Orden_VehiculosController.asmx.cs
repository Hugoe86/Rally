using datos_trazabilidad;
using Elmah;
using LitJson;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using web_trazabilidad.Models.Ayudante;
using web_trazabilidad.Models.Negocio;
using web_trazabilidad.Models.Negocio.Operaciones;
using web_trazabilidad.Models.Negocio.Trazabilidad;

namespace web_trazabilidad.Paginas.Operaciones.controllers
{
    /// <summary>
    /// Summary description for Orden_VehiculosController
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class Orden_VehiculosController : System.Web.Services.WebService
    {

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string Consultar_Vehiculos(string jsonObject)
        {
            StringBuilder Lista_Vehiculos = new StringBuilder();
            string Json_Resultado = string.Empty;
            Cls_Ope_Eventos_Vehiculo_Participante_Negocio Obj_Vehiculo = new Cls_Ope_Eventos_Vehiculo_Participante_Negocio();

            try
            {
                Obj_Vehiculo = JsonMapper.ToObject<Cls_Ope_Eventos_Vehiculo_Participante_Negocio>(jsonObject);

                using (var dbContext = new Sistema_TrazabilidadEntities())
                {


                    #region vehiculos

                    var _vehiculos = (from _vp in dbContext.Ope_Eventos_Vehiculo_Participante
                                      join _v in dbContext.Cat_Vehiculos on _vp.Vehiculo_Id equals _v.Vehiculo_Id

                                      where _vp.Evento_Id == Obj_Vehiculo.Evento_Id
                                      && (Obj_Vehiculo.Categoria_Id > 0 ? _vp.Categoria_Participante_Id == Obj_Vehiculo.Categoria_Id : true)

                                      select new Cls_Ope_Eventos_Vehiculo_Participante_Negocio
                                      {
                                          Vehiculo_Participante_Id = _vp.Vehiculo_Participante_Id,
                                          Vehiculo_Id = _vp.Vehiculo_Id,
                                          Numero_Participante = _vp.Numero_Participante,
                                          Color_Fondo_Hex_Rgb = _v.Color_Fondo_Hex_Rgb,
                                          Color_Hex_Rgb = _v.Color_Hex_Rgb,
                                          Marca = _v.Marca,
                                          Modelo = _v.Modelo,
                                          NS = _v.NS,
                                          Placas = _v.Placas,
                                          Año = _v.Año,

                                          text = (from _aux1 in dbContext.Cat_Participantes
                                                  where _aux1.Participante_ID == _vp.Participante_Piloto_Id
                                                  select new
                                                  {
                                                      nombre = _aux1.Nombre
                                                  }).FirstOrDefault().nombre,

                                          text2 = (from _aux1 in dbContext.Cat_Participantes
                                                   where _aux1.Participante_ID == _vp.Participante_Copiloto_Id
                                                   select new
                                                   {
                                                       nombre = _aux1.Nombre
                                                   }).FirstOrDefault().nombre,

                                          text3 = (from _aux1 in dbContext.Ope_Eventos
                                                   where _aux1.Evento_Id == _vp.Evento_Id
                                                   select new
                                                   {
                                                       nombre = _aux1.Nombre,
                                                   }).FirstOrDefault().nombre,

                                          text_categoria = (from _aux1 in dbContext.Ope_Eventos_Categorias
                                                   where _aux1.Categoria_Id == _vp.Categoria_Id
                                                   select new
                                                   {
                                                       nombre = _aux1.Nombre
                                                   }).FirstOrDefault().nombre,


                                          text_categoriaParticipante = (from _aux1 in dbContext.Ope_Eventos_Categorias
                                                            where _aux1.Categoria_Id == _vp.Categoria_Participante_Id
                                                            select new
                                                            {
                                                                nombre = _aux1.Nombre
                                                            }).FirstOrDefault().nombre,

                                      }).OrderBy(o => o.Numero_Participante);

                    #endregion vehiculos


                    if (_vehiculos.Any())
                    {
                        Lista_Vehiculos.Append("<h4><i class='fa fa-road' aria-hidden='true'></i>&nbsp;Vehiculos - " + _vehiculos.ToList()[0].text3 + "</h4>");

                        Lista_Vehiculos.Append("<div id='izquierdo' class='list-group col'>");


                        foreach (var _registro_veh in _vehiculos)
                        {
                            Lista_Vehiculos.Append("<div class='list-group-item '>");
                            Lista_Vehiculos.Append("    <div class='grid_vehiculos' >");
                            Lista_Vehiculos.Append("        <div class='' data-category='transition'>");
                            Lista_Vehiculos.Append("            <div class='row'>");

                            Lista_Vehiculos.Append("                <div class='col-md-2 text-center' style=''>");
                            Lista_Vehiculos.Append("                    <i class='fa fa-flag-checkered' style='font-size: 32px;'>");
                            Lista_Vehiculos.Append("                        <label class='No_Participante'>" + _registro_veh.Numero_Participante + "</label>");
                            Lista_Vehiculos.Append("                    </i>");
                            Lista_Vehiculos.Append("                        &nbsp;");
                            Lista_Vehiculos.Append("                    <i class='fa fa-circle' style='background-color:#" + _registro_veh.Color_Fondo_Hex_Rgb +"; color:#" + _registro_veh.Color_Hex_Rgb + ";font-size: 32px;'>");
                            Lista_Vehiculos.Append("                    </i>");
                            Lista_Vehiculos.Append("                </div>");
                            
                            Lista_Vehiculos.Append("                <div class='col-md-2'>");
                            Lista_Vehiculos.Append("                    <label class='id'  style='display: none;'>" + _registro_veh.Vehiculo_Participante_Id + "</label>");
                            Lista_Vehiculos.Append("                    <p class='name-a'>NS: " + _registro_veh.NS + "</p>");
                            Lista_Vehiculos.Append("                    <p class='name-a'>Marca: " + _registro_veh.Marca + "</p>");
                            Lista_Vehiculos.Append("                    <p class='name-a'>Modelo: " + _registro_veh.Modelo + "</p>");
                            Lista_Vehiculos.Append("                </div>");

                            Lista_Vehiculos.Append("                <div class='col-md-2'>");
                            Lista_Vehiculos.Append("                    <p class='name-a'>Año: " + _registro_veh.Año + "</p>");
                            Lista_Vehiculos.Append("                    <p class='name-a'>Placas: " + _registro_veh.Placas + "</p>");
                            Lista_Vehiculos.Append("                </div>");


                            Lista_Vehiculos.Append("                <div class='col-md-2'>");
                            Lista_Vehiculos.Append("                    <i class='fa fa-map' style='font-size: 22px; color: #2CA66F'>");
                            Lista_Vehiculos.Append("                        <label class='name-a'>Categoria</label>");
                            Lista_Vehiculos.Append("                    </i>");
                            
                            Lista_Vehiculos.Append("                    <p class='name-a'>Registrado: " + _registro_veh.text_categoria + "</p>");
                            Lista_Vehiculos.Append("                    <p class='name-a'>Competencia: " + _registro_veh.text_categoriaParticipante + "</p>");
                            Lista_Vehiculos.Append("                </div>");

                            Lista_Vehiculos.Append("                <div class='col-md-4'>");
                            Lista_Vehiculos.Append("                    <i class='fa fa-user-circle' style='font-size: 22px; color: #2CA66F'>");
                            Lista_Vehiculos.Append("                    </i>");
                            Lista_Vehiculos.Append("                    <p class='name-a'>Piloto: " + _registro_veh.text + "</p>");
                            Lista_Vehiculos.Append("                    <p class='name-a'>Copiloto: " + _registro_veh.text2 + "</p>");
                            Lista_Vehiculos.Append("                </div>");

                            Lista_Vehiculos.Append("                <div class='col-md-3'>");
                            Lista_Vehiculos.Append("                </div>");

                            Lista_Vehiculos.Append("            </div>");
                            Lista_Vehiculos.Append("        </div>");
                            Lista_Vehiculos.Append("    </div>");
                            Lista_Vehiculos.Append("</div>");
                        }
                    }

                }// fin using

                Lista_Vehiculos.Append("</div>");

                Json_Resultado = ("{\"html\": " + "\"" + Lista_Vehiculos + "\"}");

            }
            catch (Exception Ex)
            {
                ErrorSignal.FromCurrentContext().Raise(Ex);
                //Cls_Jira.Create_Issue(Ex, Cls_Jira.Descripcion_Referencia(Cls_Jira.IssueTypes.Bug), Cls_Jira.Descripcion_Referencia(Cls_Jira.IssuePriority.High));
                throw new Exception("Error en el método (Consultar_Vehiculos): " + Ex.Message);
            }
            return Json_Resultado;
        }



        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string Actualizar_Orden(String jsonObject)
        {
            Cls_Mensaje Mensaje = new Cls_Mensaje();
            Cls_Ope_Eventos_Vehiculo_Participante_Negocio Obj_Vehiculo = new Cls_Ope_Eventos_Vehiculo_Participante_Negocio();
            List<Cls_Ope_Eventos_Vehiculo_Arreglo> Obj_Reorden = new List<Cls_Ope_Eventos_Vehiculo_Arreglo>();

            string jsonResultado = "";
            String Color = "#8A2BE2";
            String Icono = "fa fa-close";

            try
            {
                Mensaje.Titulo = "Actualización";

                Obj_Vehiculo = JsonMapper.ToObject<Cls_Ope_Eventos_Vehiculo_Participante_Negocio>(jsonObject);
                Obj_Reorden = JsonConvert.DeserializeObject<List<Cls_Ope_Eventos_Vehiculo_Arreglo>>(Obj_Vehiculo.tbl_datos_reorden);
                using (var dbContext = new Sistema_TrazabilidadEntities())
                {
                    using (var transaction = dbContext.Database.BeginTransaction())
                    {
                        try
                        {
                            foreach (var registro in Obj_Reorden)
                            {
                                Ope_Eventos_Vehiculo_Participante Veh = new Ope_Eventos_Vehiculo_Participante();
                                Veh = dbContext.Ope_Eventos_Vehiculo_Participante.Where(p => p.Vehiculo_Participante_Id == registro.id).First();
                                Veh.Numero_Participante = registro.no;
                                dbContext.SaveChanges();
                            }

                            transaction.Commit();
                            Mensaje.Estatus = "success";
                            Mensaje.Mensaje = "<i class='fa fa-check'style = 'color: #00A41E;' ></ i > &nbsp; Actualación realizada.";
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();

                            Mensaje.Mensaje = "Error Técnico. " + ex.Message;
                            Mensaje.Estatus = "error"; 
                            
                        }
                    }
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
    }
}
