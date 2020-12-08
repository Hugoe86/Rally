<%@ Page Title="" Language="C#" MasterPageFile="~/Paginas/Paginas_Generales/MasterPage.Master" AutoEventWireup="true" CodeBehind="Frm_Rel_Usuarios_Procesos_Sistema.aspx.cs" Inherits="web_trazabilidad.Paginas.Paginas_Generales.Frm_Rel_Usuarios_Procesos_Sistema" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../../Recursos/bootstrap-date/moment.min.js"></script>
    <link href="../../Recursos/bootstrap-table/bootstrap-table.min.css" rel="stylesheet" />
    <link href="../../Recursos/estilos/center_loader.css" rel="stylesheet" />
    <link href="../../Recursos/estilos/demo_form.css" rel="stylesheet" />
    <link href="../../Recursos/bootstrap-combo/select2.css" rel="stylesheet" />

    <script src="../../Recursos/bootstrap-table-current/bootstrap-table.js"></script>
    <script src="../../Recursos/bootstrap-table-current/locale/bootstrap-table-es-MX.js"></script>
    <script src="../../Recursos/bootstrap-table-current/extensions/export/bootstrap-table-export.js"></script>
    <script src="../../Recursos/bootstrap-table-current/extensions/export/tableExport.js"></script>
    <script src="../../Recursos/plugins/center-loader.min.js"></script>
    <script src="../../Recursos/plugins/parsley.js"></script>
    <script src="../../Recursos/bootstrap-table/bootstrap-table.min.js"></script>
    <script src="../../Recursos/bootstrap-table/locale/bootstrap-table-es-MX.min.js"></script>
    <link href="../../Recursos/bootstrap-date/bootstrap-datetimepicker.min.css" rel="stylesheet" />
    <script src="../../Recursos/bootstrap-date/bootstrap-datetimepicker.min.js"></script>
    <link href="../../Recursos/bootstrap-date/datepicker.css" rel="stylesheet" />
    <script src="../../Recursos/bootstrap-date/bootstrap-datepicker.js"></script>
    <script src="../../Recursos/bootstrap-combo/select2.js"></script>
    <script src="../../Recursos/javascript/seguridad/Js_Controlador_Sesion.js"></script>
    <script src="../../Recursos/javascript/seguridad/Js_Rel_Usuario_Procesos.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid" style="height: 100vh;">

        <div class="page-header" align="left">
            <h3 style="font-family: 'Roboto Light', cursive !important; font-size: 24px; font-weight: bold; color: #808080;">Configuracion Relacion Usuarios-Procesos  </h3>
        </div>

        <div class="row" style="margin-top: 5px">
            <div class="col-md-12">
                <label class="fuente_lbl_controles">(*) Usuario</label>
                <select class="form-control" style="width: 100%" id="Cmb_Usuario"  data-parsley-required="true"></select>
            </div>
        </div>
        <div class="row" style="margin-top: 15px">
            <div class="col-md-12" style="text-align:right">
                <%--<div id="toolbar" style="margin-left: 5px;">--%>
                    <div class="btn-group" role="group" style="margin-left: 5px;">
                        <button id="btn_salir" type="button" class="btn btn-info btn-sm" title="Salir"><i class="glyphicon glyphicon-home"></i></button>
                        <button id="btn_nuevo" type="button" class="btn btn-info btn-sm" title="Guardar"><i class="fa fa-save"></i></button>
               <%--     </div>--%>
                </div>
            </div>
        </div>

        <table id="tbl_procesos" data-toolbar="#toolbar" class="table table-responsive"></table>
    </div>
</asp:Content>
