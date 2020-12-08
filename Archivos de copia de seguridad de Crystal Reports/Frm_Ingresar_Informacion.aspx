<%@ Page Title="" Language="C#" MasterPageFile="~/Paginas/Paginas_Generales/MasterPage.Master" AutoEventWireup="true" CodeBehind="Frm_Ingresar_Informacion.aspx.cs" Inherits="web_trazabilidad.Paginas.Trazabilidad.Frm_Ingresar_Informacion" %>

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
                <h3>Ingresar Datos de Inventario</h3>
            </div>
        </div>
        <hr />
        <div class="row">
            <asp:Button ID="btn_ingresar" runat="server" CssClass="btn btn-success" style="width:100%; height:150px; font-size:50px" OnClick="btn_ingresar_Click" Text="Ingresar Informacion" />
            <asp:Label ID="lbl_text" runat="server" Text="" Style="font-size:xx-large; font-family:'Comic Sans MS'; color:#007acc; text-transform:uppercase; text-align:center"></asp:Label>
        </div>
    </div>
</asp:Content>