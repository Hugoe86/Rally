﻿<%-- --------------------------------------------------------------------------------------------------------------------------------------- --%>
<%-- --------------------------------------------------------------------------------------------------------------------------------------- --%>
<%-- --------------------------------------------------------------------------------------------------------------------------------------- --%>

<%@ Page Language="C#" 
    AutoEventWireup="true" 
    MasterPageFile="~/Paginas/Paginas_Generales/MasterPage.Master"
    CodeBehind="Frm_Ope_Cancelacion_Puntos.aspx.cs" 
    Inherits="web_trazabilidad.Paginas.Operaciones.Frm_Ope_Cancelacion_Puntos" %>

<%-- --------------------------------------------------------------------------------------------------------------------------------------- --%>
<%-- --------------------------------------------------------------------------------------------------------------------------------------- --%>
<%-- --------------------------------------------------------------------------------------------------------------------------------------- --%>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link href="../../Recursos/bootstrap-table/bootstrap-table.min.css" rel="stylesheet" />
    <link href="../../Recursos/bootstrap-table/extensions/editable/bootstrap-editable.css" rel="stylesheet" />
    <link href="../../Recursos/estilos/center_loader.css" rel="stylesheet" />
    <link href="../../Recursos/estilos/demo_form.css" rel="stylesheet" />
    <link href="../../Recursos/plugins/toastr/toastr.css" rel="stylesheet" />
    <link href="../../Recursos/bootstrap-date/bootstrap-datetimepicker.css" rel="stylesheet" />
    <link href="../../Recursos/bootstrap-combo/select2.css" rel="stylesheet" />
    <script src="../../Recursos/lightbox/ekko-lightbox.min.js"></script>
    <script src="../../Recursos/jquery-validate/jquery.validate.min.js"></script>
    <script src="../../Recursos/xenon/js/xenon-widgets.js"></script>
    <script src="../../Recursos/xenon/js/xenon-custom.js"></script>
    <script src="../../Recursos/bootstrap-table-current/bootstrap-table.js"></script>
    <script src="../../Recursos/bootstrap-table-current/locale/bootstrap-table-es-MX.js"></script>
    <script src="../../Recursos/bootstrap-table/extensions/editable/bootstrap-editable.js"></script>
    <script src="../../Recursos/bootstrap-table/extensions/editable/bootstrap-table-editable.min.js"></script>
    <script src="../../Recursos/bootstrap-table-current/extensions/editable/bootstrap-table-editable.js"></script>
    <script src="../../Recursos/bootstrap-combo/select2.js"></script>
    <script src="../../Recursos/bootstrap-combo/es.js"></script>
    <script src="../../Recursos/plugins/center-loader.min.js"></script>
    <script src="../../Recursos/plugins/parsley.js"></script>
    <script src="../../Recursos/jquery-numeric/jquery.numeric.js"></script>
    <script src="../../Recursos/jquery-numeric/accounting.min.js"></script>
    <script src="../../Recursos/jquery-numeric/jquery.formatCurrency-1.4.0.min.js"></script>
    <script src="../../Recursos/bootstrap-date/bootstrap-datetimepicker.min.js"></script>
    <script src="../../Recursos/javascript/seguridad/Js_Controlador_Sesion.js"></script>
    <script src="../../Recursos/javascript/Operaciones/Js_Ope_Cancelacion_Puntos.js"></script>

</asp:Content>
<%-- --------------------------------------------------------------------------------------------------------------------------------------- --%>
<%-- --------------------------------------------------------------------------------------------------------------------------------------- --%>
<%-- --------------------------------------------------------------------------------------------------------------------------------------- --%>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container-fluid" style="height: 100vh;">
        <div class="page-header">
            <h3 style="font-family: 'Roboto Light', cursive !important; font-size: 24px; font-weight: bold; color: #808080;">Cancelaci&oacute;n de Puntos de Control</h3>
        </div>
        <div id="Principal"></div>
        <div id="Operacion"></div>
    </div>

</asp:Content>

<%-- --------------------------------------------------------------------------------------------------------------------------------------- --%>
<%-- --------------------------------------------------------------------------------------------------------------------------------------- --%>
<%-- --------------------------------------------------------------------------------------------------------------------------------------- --%>
