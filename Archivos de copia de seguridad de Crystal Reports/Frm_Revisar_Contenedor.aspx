<%@ Page Title="" Language="C#" MasterPageFile="~/Paginas/Paginas_Generales/MasterPage.Master" AutoEventWireup="true" CodeBehind="Frm_Revisar_Contenedor.aspx.cs" Inherits="web_trazabilidad.Paginas.Trazabilidad.Frm_Revisar_Contenedor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--Hojas de estilo --%>
    <link href="../../Recursos/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../../Recursos/estilos/demo_form.css" rel="stylesheet" />
    <link href="../../Recursos/bootstrap-table/bootstrap-table.css" rel="stylesheet" />
    <link href="../../Recursos/bootstrap-table/extensions/editable/bootstrap-editable.css" rel="stylesheet" />

    <%--Javascript--%>
    <script src="../../Recursos/bootstrap-table/bootstrap-table.min.js"></script>
    <script src="../../Recursos/bootstrap-table/locale/bootstrap-table-es-MX.min.js"></script>
    <script src="../../Recursos/bootstrap-table/extensions/editable/bootstrap-editable.js"></script>
    <script src="../../Recursos/bootstrap-table/extensions/editable/bootstrap-table-editable.min.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid" style="height: 100vh;">
        <div class="row">
            <div class="col-sm-12 text-left" style="">
                <h3>Revisar Contenedores</h3>
            </div>
        </div>
        <hr />
        <div class="row">
            
        </div>
    </div>
</asp:Content>
