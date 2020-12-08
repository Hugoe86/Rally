using datos_trazabilidad;
using Elmah;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using web_trazabilidad.Models.Ayudante;

namespace web_trazabilidad.Paginas.Paginas_Generales
{
    public partial class Breadcrumb : System.Web.UI.UserControl
    {
        #region Init/Load

        /// ****************************************************************************************************************************
        /// NOMBRE: Page_Load
        /// 
        /// DESCRIPCIÓN: Ejecuta la carga inicial de la página.
        /// 
        /// USUARIO CREO: Juan Alberto Hernández Negrete.
        /// FECHA CREÓ: Enero/2012
        /// USUARIO MODIFICO: 
        /// FECHA MODIFICO: 
        /// CAUSA MODIFICACIÓN: 
        /// *****************************************************************************************************************************
        protected void Page_Load(object sender, EventArgs e)
        {
            Crear_Breadcrumb();
        }

        #endregion Init/Load

        #region (Métodos)

        /// ****************************************************************************************************************************
        /// NOMBRE: Crear_Breadcrumb
        /// DESCRIPCIÓN: Ejecuta la construcción del breadcrumb del sistema SIAS. 
        /// USUARIO CREO: Juan Alberto Hernández Negrete.
        /// FECHA CREÓ: Enero/2012
        /// USUARIO MODIFICO: 
        /// FECHA MODIFICO: 
        /// CAUSA MODIFICACIÓN: 
        /// *****************************************************************************************************************************
        public void Crear_Breadcrumb()
        {
            StringBuilder Breadcrumb = new StringBuilder();
            string ruta_relativa = "..";
            string nombre_mostrar = string.Empty;

            try
            {
               // Convert.ToInt32("Nan");
                string url = HttpContext.Current.Request.Url.AbsolutePath;
                string ruta_sin_valor_url = string.Empty;
                string[] url_ = url.Replace('/', ' ').Split(new string[] { " " }, StringSplitOptions.None);

                int conteo_carpetas = url_.Count();

                if(conteo_carpetas == 4)
                {
                    #region Localhost

                    ruta_relativa += "/" + url_[2];
                    if (url_[3].ToString().IndexOf('?') != -1)
                    {
                        string[] _url = url_[5].ToString().Replace('?', ' ').Split(new string[] { " " }, StringSplitOptions.None);
                        ruta_sin_valor_url = _url[0].ToString();
                    }
                    ruta_relativa += "/" + ((string.IsNullOrEmpty(ruta_sin_valor_url)) ? url_[3] : ruta_sin_valor_url);

                    List<string> Lista_URL = new List<string>(url_);
                    Lista_URL.RemoveAll(x => String.IsNullOrEmpty(x));

                    using (var dbContext = new Sistema_TrazabilidadEntities())
                    {
                        Breadcrumb.Append("<ol class='breadcrumb bc-1' style='background-color: #fff !important;'>");
                        #region Construccion

                        var valores = from _menus in dbContext.Apl_Menus
                                      where _menus.URL_LINK == ruta_relativa
                                      select _menus;

                        if (valores.Any())
                        {
                            #region Obtiene Valor Nombre

                            foreach (var _menu in valores)
                            {
                                nombre_mostrar = _menu.Menu_Descripcion.ToString();
                            }

                            #endregion Obtiene Valor Nombre

                            Breadcrumb.Append("<li><a href = '../../Paginas/Paginas_Generales/Frm_Apl_Principal.aspx'><i class='fa-home'></i><span id='_inicio'>Inicio</span></a></li>");
                            Breadcrumb.Append("<li><a href=''>" + Lista_URL[1].Replace('_', ' ') + "</a></li>");
                            Breadcrumb.Append("<li class='active'><strong>" + nombre_mostrar + "</strong></li>");
                        }
                        else
                        {
                            Breadcrumb.Append("<li><a href = '../../Paginas/Paginas_Generales/Frm_Apl_Principal.aspx'><i class='fa-home'></i><span id='_inicio'>Inicio</span></a></li>");
                        }

                        #endregion Construccion
                        Breadcrumb.Append("</ol>");

                        Lbl_Breadcrumb.Text = Breadcrumb.ToString();
                    }

                    #endregion Localhost
                }
                else
                {
                    #region Servidor

                    ruta_relativa += "/" + url_[2 + (conteo_carpetas -4)];
                    if (url_[3 + (conteo_carpetas - 4)].ToString().IndexOf('?') != -1)
                    {
                        string[] _url = url_[5 + (conteo_carpetas - 4)].ToString().Replace('?', ' ').Split(new string[] { " " }, StringSplitOptions.None);
                        ruta_sin_valor_url = _url[0 + (conteo_carpetas - 4)].ToString();
                    }
                    ruta_relativa += "/" + ((string.IsNullOrEmpty(ruta_sin_valor_url)) ? url_[3 + (conteo_carpetas - 4)] : ruta_sin_valor_url);

                    List<string> Lista_URL = new List<string>(url_);
                    Lista_URL.RemoveAll(x => String.IsNullOrEmpty(x));

                    using (var dbContext = new Sistema_TrazabilidadEntities())
                    {
                        Breadcrumb.Append("<ol class='breadcrumb bc-1' style='background-color: #fff !important;'>");
                        #region Construccion

                        var valores = from _menus in dbContext.Apl_Menus
                                      where _menus.URL_LINK == ruta_relativa
                                      select _menus;

                        if (valores.Any())
                        {
                            #region Obtiene Valor Nombre

                            foreach (var _menu in valores)
                            {
                                nombre_mostrar = _menu.Menu_Descripcion.ToString();
                            }

                            #endregion Obtiene Valor Nombre

                            Breadcrumb.Append("<li><a href = '../../Paginas/Paginas_Generales/Frm_Apl_Principal.aspx'><i class='fa-home'></i>Inicio</a></li>");
                            Breadcrumb.Append("<li><a href=''>" + Lista_URL[1 + (conteo_carpetas - 4)].Replace('_', ' ') + "</a></li>");
                            Breadcrumb.Append("<li class='active'><strong>" + nombre_mostrar + "</strong></li>");
                        }
                        else
                        {
                            Breadcrumb.Append("<li><a href = '../../Paginas/Paginas_Generales/Frm_Apl_Principal.aspx'><i class='fa-home'></i>Inicio</a></li>");
                        }

                        #endregion Construccion
                        Breadcrumb.Append("</ol>");

                        Lbl_Breadcrumb.Text = Breadcrumb.ToString();
                    }
                    #endregion Servidor
                }
            }
            catch (Exception Ex)
            {
                Exception e = new Exception("Error al ejecutar la construcción del breadcrumb del sistema. Error: [" + Ex.Message + "]");
                ErrorSignal.FromCurrentContext().Raise(e);
                //Cls_Jira.Create_Issue(e, Cls_Jira.Descripcion_Referencia(Cls_Jira.IssueTypes.Bug), Cls_Jira.Descripcion_Referencia(Cls_Jira.IssuePriority.High));
            }
        }
        #endregion (Métodos)
    }
}