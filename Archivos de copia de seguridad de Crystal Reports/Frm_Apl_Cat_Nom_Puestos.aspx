<%@ Page Title="" Language="C#" MasterPageFile="~/Paginas/Paginas_Generales/MasterPage.Master" AutoEventWireup="true" CodeBehind="Frm_Apl_Cat_Nom_Puestos.aspx.cs" Inherits="web_trazabilidad.Paginas.Nomina.Frm_Apl_Cat_Nom_Puestos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="../../Recursos/bootstrap-table/bootstrap-table.min.css" rel="stylesheet" />
    <link href="../../Recursos/estilos/center_loader.css" rel="stylesheet" />
    <link href="../../Recursos/estilos/demo_form.css" rel="stylesheet" />
    <link href="../../Recursos/estilos/css_percepciones_deducciones.css" rel="stylesheet" />
    <link href="../../Recursos/plugins/toastr/toastr.css" rel="stylesheet" />

    <script src="../../Recursos/bootstrap-table-current/bootstrap-table.js"></script>
    <script src="../../Recursos/bootstrap-table-current/locale/bootstrap-table-es-MX.js"></script>
    <script src="../../Recursos/bootstrap-table-current/extensions/export/bootstrap-table-export.js"></script>
    <script src="../../Recursos/bootstrap-table-current/extensions/export/tableExport.js"></script>

    <script src="../../Recursos/plugins/center-loader.min.js"></script>
    <script src="../../Recursos/plugins/parsley.js"></script>
    <script src="../../Recursos/bootstrap-table/bootstrap-table.min.js"></script>
    <script src="../../Recursos/bootstrap-table/locale/bootstrap-table-es-MX.min.js"></script>
    <script src="../../Recursos/javascript/seguridad/Js_Controlador_Sesion.js"></script>
    <script src="../../Recursos/plugins/toastr/toastr.min.js"></script>
    <script src="../../Recursos/plugins/jquery.formatCurrency-1.4.0.min.js"></script>
    <script src="../../Recursos/plugins/jquery.formatCurrency.all.js"></script>
    <script src="../../Recursos/javascript/nomina/Js_Apl_Cat_Nom_Puestos.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid" style="height: 100vh;">

        <div class="page-header">
            <h3 style="font-family: 'Roboto Light', cursive !important; font-size: 24px; font-weight: bold; color: #808080;">Cat&aacute;logo de Puestos</h3>
        </div>

        <div id="Controles_Generales"></div>
        <div id="Operaciones_Vista"></div>

    </div>
</asp:Content>
