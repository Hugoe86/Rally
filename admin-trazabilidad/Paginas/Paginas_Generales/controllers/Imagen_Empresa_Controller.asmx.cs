using admin_trazabilidad.Models.Ayudante;
using admin_trazabilidad.Models.Negocio;
using datos_trazabilidad;
using LitJson;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;

namespace admin_trazabilidad.Paginas.Paginas_Generales.controllers
{
    /// <summary>
    /// Summary description for Imagen_Empresa_Controller
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class Imagen_Empresa_Controller : System.Web.Services.WebService
    {
        #region

        ///*************************************************************************************
        ///NOMBRE DE LA FUNCIÓN : Obtener_Empresas
        ///DESCRIPCIÓN          : Metodo para obtener las empresas de acuerdo a su nombre
        ///PARÁMETROS           : JsonObject con los filtros de busqueda
        ///CREO                 : Josué Daniel Sámano García
        ///FECHA_CREO           : 14/Noviembre/2016
        ///MODIFICO             :
        ///FECHA_MODIFICO       :
        ///CAUSA_MODIFICACIÓN   :
        ///*************************************************************************************
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string Obtener_Empresas(string jsonObject)
        {
            Cls_Apl_Cat_Empresas_Negocio ObjEmpresa = null;
            string Json_Resultado = string.Empty;

            try
            {
                ObjEmpresa = JsonMapper.ToObject<Cls_Apl_Cat_Empresas_Negocio>(jsonObject);
                using (var dbContext = new Sistema_TrazabilidadEntities())
                {
                    var empresas = from emp in dbContext.Apl_Empresas
                                   where emp.Nombre.Contains(ObjEmpresa.Nombre)
                                   select new
                                   {
                                       emp.Empresa_ID,
                                       emp.Nombre,
                                       emp.Ruta_Imagen,
                                   };

                    Json_Resultado = JsonMapper.ToJson(empresas.ToList());
                }
            }
            catch (Exception Ex)
            {

            }

            return Json_Resultado;
        }

        #endregion
    }
}
