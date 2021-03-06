﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using web_trazabilidad.Models.Ayudante;
using web_trazabilidad.Models.Negocio;

namespace web_trazabilidad.Paginas.Paginas_Generales
{
    public partial class MasterPage : System.Web.UI.MasterPage
    {
        public string UrlApp = Cls_Config.UrlApp;
        public int EmpresaId = String.IsNullOrEmpty(Cls_Sesiones.Empresa_ID) ? -1 : Convert.ToInt32(Cls_Sesiones.Empresa_ID);
        public int SucursalId = String.IsNullOrEmpty(Cls_Sesiones.Sucursal_ID) ? -1 : Convert.ToInt32(Cls_Sesiones.Sucursal_ID);
        public string Usuario = Cls_Sesiones.Usuario;

        /// <summary>
        /// Método que realiza la carga inicial del MasterPage.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            //Se realiza la validación de los accesos
            if (Cls_Sesiones.Menu_Control_Acceso != null)
            {
                if (!Control_Acceso())
                    Response.Redirect("../../Paginas/Paginas_Generales/Frm_Apl_Principal.aspx");
            }
            else Response.Redirect("../../Paginas/Paginas_Generales/Frm_Apl_Login.html");
        }

        /// <summary>
        /// Método que realiza la validación de los menus configurados en para el rol actual.
        /// </summary>
        /// <returns>True si la página esta configurada al rol y False en caso contrario</returns>
        internal bool Control_Acceso()
        {
            if (this.Request.Url.AbsolutePath.ToLower().Contains("frm_apl_principal"))
                return true;
            bool Continuar = false;
            int elementos = (this.Request.Url.AbsolutePath.Split('/').Length <= 0) ? 0 : this.Request.Url.AbsolutePath.Split('/').Length;
            string form = this.Request.Url.AbsolutePath.Split('/')[elementos - 1];

            int pos = -1;
            if (!string.IsNullOrEmpty(form))
            {
                pos = form.IndexOf("?");
                if (pos != -1) form = form.Substring(0, pos - 1);
            }

            var menus = Cls_Sesiones.Menu_Control_Acceso.Where(menu=> menu.URL_LINK.Split('/')[menu.URL_LINK.Split('/').Length - 1].Equals(form));
            if (menus.Any())
                Continuar = true;
            return Continuar;
        }
    }
}