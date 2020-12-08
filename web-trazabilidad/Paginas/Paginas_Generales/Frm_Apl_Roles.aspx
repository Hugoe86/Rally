<%@ Page Title="" Language="C#" MasterPageFile="~/Paginas/Paginas_Generales/MasterPage.Master" AutoEventWireup="true" CodeBehind="Frm_Apl_Roles.aspx.cs" Inherits="web_trazabilidad.Paginas.Paginas_Generales.Frm_Apl_Roles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <link href="../../Recursos/estilos/center_loader.css" rel="stylesheet" />
    <link href="../../Recursos/estilos/demo_form.css" rel="stylesheet" />
    <link href="../../Recursos/estilos/Css_Paginas.css" rel="stylesheet" />
    <link href="../../Recursos/bootstrap-table-current/bootstrap-table.css" rel="stylesheet" />
    <script src="../../Recursos/bootstrap-table-current/bootstrap-table.js"></script>
    <script src="../../Recursos/bootstrap-table-current/locale/bootstrap-table-es-MX.js"></script>
    <script src="../../Recursos/javascript/seguridad/Js_Controlador_Sesion.js"></script>
    <%-- <script src="../../Recursos/Javascript/Generales/Js_Menu_Accesos.js"></script>--%>
    <script src="../../Recursos/javascript/generales/Js_Apl_Roles.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid" style="height: 100vh; background-color: white;">

        <div class="page-header" align="left">
            <h3 style="font-family: 'Roboto Light', cursive !important; font-size: 24px; font-weight: bold; color: #808080;">Cat&aacute;logo de Roles</h3>
        </div>

        <div style="margin-left: 5px;" align="right">
            <div class="btn-group" role="group" style="margin-left: 5px;">

                <button id="Btn_Salir" type="button" class="btn btn-info btn-sm" title="Salir"><i class="fa fa-home"></i></button>
                <button id="Btn_Nuevo" type="button" class="btn btn-info btn-sm" title="Nuevo"><i class="fa fa-plus"></i></button>
                <button id="Btn_Modificar" type="button" class="btn btn-info btn-sm" title="Modificar"><i class="fa fa-pencil-square-o"></i></button>
                <button id="Btn_Eliminar" type="button" class="btn btn-info btn-sm" title="Eliminar"><i class="fa fa-trash-o"></i></button>
                <button id="Btn_Guardar" type="button" class="btn btn-info btn-sm" title="Guardar"><i class="fa fa-save"></i></button>
                <button id="Btn_Cancelar" type="button" class="btn btn-info btn-sm" title="Cancelar"><i class="fa fa-ban"></i></button>

            </div>
        </div>

        <div class="row" style="margin-top: 15px; margin-left: 20px; margin-right: 20px;">
            <div class="col-md-12">
                <div id="Sumary_Error" class="alert alert-danger text-left alert-dismissible" style="width: 277.78px !important; display: none">
                    <label id="Lbl_Msg_Error" />
                </div>
            </div>
        </div>

        <div id="Div_Contenido" style="background-color: white;">
            <input type="hidden" id="Hf_Rol_ID" />

            <div class="row">
                <div class="col-md-9">
                    <label for="Txt_Nombre" class="form-label"><i class="fa fa-asterisk"></i>Nombre</label>
                    <input id="Txt_Nombre" type="text" class="form-control" disabled="disabled" maxlength="100" />
                </div>
                <div class="col-md-3">
                    <label for="Cmb_Estatus" class="form-label"><i class="fa fa-asterisk"></i>Estatus</label>
                    <select id="Cmb_Estatus" class="form-control" disabled="disabled">
                        <option value="">- Seleccione - </option>
                        <option value="ACTIVO">ACTIVO</option>
                        <option value="INACTIVO">INACTIVO</option>
                    </select>
                </div>
            </div>

            <div class="row">
                <div class="col-md-12">
                    <label for="Txt_Descripcion" class="form-label">Descripción</label>
                    <input id="Txt_Descripcion" type="text" class="form-control" disabled="disabled" maxlength="250" />
                </div>
            </div>

            <div class="row">
                <div class="col-md-12">
                    <br />
                </div>
            </div>


            <div class="row">
                <div class="col-md-12">

                    <ul class="nav nav-tabs nav-tabs-justified Tabs_">
                        <li class="active" id="Li_Rol">
                            <a href="#Tab_Rol" data-toggle="tab">
                                <span class="visible-xs"><i class="fa fa-clipboard"></i></span>
                                <span class="hidden-xs">Rol</span>
                            </a>
                        </li>
                        <li id="Li_Accesos">
                            <a href="#Tab_Accesos" data-toggle="tab">
                                <span class="visible-xs"><i class="fa fa-check-square-o"></i></span>
                                <span class="hidden-xs">Acceso</span>
                            </a>
                        </li>
                    </ul>

                    <div class="tab-content">
                        <div class="tab-pane active" id="Tab_Rol">
                            <div id="Div_Tabla">
                                <table id="Tbl_Registros" class="table table-responsive"></table>
                            </div>
                        </div>
                        <div class="tab-pane" id="Tab_Accesos">
                            <table id="Tbl_Accesos" class="table table-responsive"></table>
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </div>

    <div id="progressBackgroundFilter" class="progressBackgroundFilter">
        <div class="processMessage" id="div_progress">
            <img alt="" src="../../Recursos/img/Updating2.gif" /></div>
    </div>
</asp:Content>
