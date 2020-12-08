<%@ Page Language="C#"  MasterPageFile="~/Paginas/Paginas_Generales/MasterPage.Master" AutoEventWireup="true" CodeBehind="Frm_Apl_Cat_Parametros.aspx.cs" Inherits="admin_trazabilidad.Paginas.Paginas_Generales.Frm_Apl_Cat_Parametros" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../Recursos/estilos/center_loader.css" rel="stylesheet" />
    <link href="../../Recursos/estilos/demo_form.css" rel="stylesheet" />

    <script src="../../Recursos/plugins/center-loader.min.js"></script>
    <script src="../../Recursos/plugins/parsley.js"></script>
    <script src="../../Recursos/javascript/seguridad/Js_Controlador_Sesion.js"></script>
     <script src="../../Recursos/javascript/seguridad/Js_Apl_Cat_Parametros.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid" style="height:100vh;">

        <div class="page-header" align="left">
            <h3 style="font-family: 'Roboto Light', cursive !important; font-size: 24px; font-weight: bold; color: #808080;">Cat&aacute;logo de Par&aacute;metros Globales</h3>
        </div>

        <div id="toolbar" style="margin-left: 5px;" align="right">
            <div class="btn-group" role="group" style="margin-left: 5px;">
                <button id="btn_salir" type="button" class="btn btn-info btn-sm" title="Salir"><i class="glyphicon glyphicon-home"></i></button>
                <button id="btn_guardar" type="button" class="btn btn-info btn-sm" title="Guardar"><i class="fa fa-floppy-o"></i></button>
            </div>
        </div>

        <div class="panel panel-color panel-info" id="panel1">
            <div class="panel-heading" >
            </div>
            <div class="panel-body">
                     <div class="row">
                      <div class="col-md-3"">
                      <label class="fuente_lbl_controles">Email</label>
                      <input type="text" id="txt_email" class="form-control" placeholder="Email" />
                    </div>
                    <div class="col-md-3">
                        <label class="fuente_lbl_controles">Contraseña</label>
                      <input type="password" id="txt_contrasena" class="form-control" placeholder="Contraseña" />
                    </div>
                    <div class="col-md-3">
                        <label class="fuente_lbl_controles">Puerto</label>
                      <input type="text" id="txt_puerto" class="form-control" placeholder="Puerto" />
                    </div>
                     <div class="col-md-3">
                        <label class="fuente_lbl_controles">Host</label>
                      <input type="text" id="txt_host" class="form-control" placeholder="Host" />
                    </div>
                  </div>
                    <div class="row">
                         <div class="col-md-3">
                      <label class="fuente_lbl_controles">Usuario JIRA</label>
                      <input type="text" id="txt_usuario_jira" class="form-control" placeholder="Usuario JIRA"/>
                    </div>
                    <div class="col-md-3">
                        <label class="fuente_lbl_controles">Contraseña JIRA</label>
                      <input type="password" id="txt_password_jira" class="form-control" placeholder="Contraseña JIRA"/>
                    </div>
                    <div class="col-md-6">
                        <label class="fuente_lbl_controles">URL de servicio JIRA </label>
                      <input type="text" id="txt_url_jira_service" class="form-control" placeholder="URL de servicio JIRA" />
                    </div>
                  </div>
                    <div class="row">
                        <div class="col-md-3">
                      <label class="fuente_lbl_controles">Nombre de proyecto JIRA</label>
                      <input type="text" id="txt_name_jira_project" class="form-control" placeholder="Nombre de proyecto JIRA"/>
                    </div>
                        <div class="col-md-3"">                          
                             <label class="fuente_lbl_controles">¿Usar las credenciales default?</label>
                             <input type="checkbox" id="chck_usedefaultcredentials" class="form-control"/>
                         </div>
                    <div class="col-md-2">
                        <label class="fuente_lbl_controles">¿Habilitar SSL?</label>
                      <input style="text-align:left !important;" type="checkbox" id="chck_enablessl" class="form-control"/>
                    </div>
                       <div class="col-md-4">
                           <input type="hidden" id="txt_parametro_id"/>
                    </div>
                  </div>
            </div>
        </div>

    </div>
</asp:Content>

