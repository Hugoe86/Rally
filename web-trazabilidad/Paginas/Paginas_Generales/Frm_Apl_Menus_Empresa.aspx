<%@ Page Title="" Language="C#" MasterPageFile="~/Paginas/Paginas_Generales/MasterPage.Master" AutoEventWireup="true" CodeBehind="Frm_Apl_Menus_Empresa.aspx.cs" Inherits="admin_trazabilidad.Paginas.Paginas_Generales.Frm_Apl_Menus_Empresa" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../Recursos/bootstrap-table-current/bootstrap-table.css" rel="stylesheet" />
    <%--<link href="../../Recursos/bootstrap-table/bootstrap-table.min.css" rel="stylesheet" />--%>
    <link href="../../Recursos/estilos/center_loader.css" rel="stylesheet" />
    <link href="../../Recursos/estilos/demo_form.css" rel="stylesheet" />
    <link href="../../Recursos/estilos/css_master.css" rel="stylesheet" />
    <link href="../../Recursos/bootstrap-combo/select2.css" rel="stylesheet" />
    <%--    <link href="../../Recursos/xenon/css/xenon-core.css" rel="stylesheet" />--%>

    <script src="../../Recursos/plugins/center-loader.min.js"></script>
    <script src="../../Recursos/plugins/parsley.js"></script>
    <script src="../../Recursos/bootstrap-combo/select2.js"></script>
    <script src="../../Recursos/bootstrap-table-current/bootstrap-table.js"></script>
    <script src="../../Recursos/bootstrap-table-current/locale/bootstrap-table-es-MX.js"></script>
 <%--   <script src="../../Recursos/bootstrap-table/bootstrap-table.min.js"></script>
    <script src="../../Recursos/bootstrap-table/locale/bootstrap-table-es-MX.min.js"></script>--%>
    <script src="../../Recursos/javascript/seguridad/Js_Controlador_Sesion.js"></script>
    <script src="../../Recursos/javascript/seguridad/Js_Apl_Menus_Empresa.js"></script>
    <style>

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid" style="height: 100vh;">

        <div class="page-header" align="left">
            <h3 style="font-family: 'Roboto'; font-size: 24px; font-weight: bold; color: #808080;">Cat&aacute;logo de Menus por Empresa</h3>
        </div>
        <div class="row">
            <div class="col-md-10">
                <select id="cmb_empresa" class="form-control"></select>
            </div>
            <div class="col-md-2" style="right: auto">
                <button id="btn_aplicar" class="btn btn-success"><i class="glyphicon glyphicon-ok">&nbsp;Aplicar</i></button>
            </div>
        </div>

        <table id="tbl_menus" data-toolbar="#toolbar" class="table table-responsive"></table>

        <div style="margin-top: 100px"></div>
    </div>

    
</asp:Content>











