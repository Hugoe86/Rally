<%-- --------------------------------------------------------------------------------------------------------------------------------------- --%>
<%-- --------------------------------------------------------------------------------------------------------------------------------------- --%>
<%-- --------------------------------------------------------------------------------------------------------------------------------------- --%>

<%@ Page Language="C#" AutoEventWireup="true"
    CodeBehind="Frm_Ope_Orden_Vehiculos.aspx.cs"
    MasterPageFile="~/Paginas/Paginas_Generales/MasterPage.Master"
    Inherits="web_trazabilidad.Paginas.Operaciones.Frm_Ope_Orden_Vehiculos" %>


<%-- --------------------------------------------------------------------------------------------------------------------------------------- --%>
<%-- --------------------------------------------------------------------------------------------------------------------------------------- --%>
<%-- --------------------------------------------------------------------------------------------------------------------------------------- --%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%-- ---------------------------------------------------------------------------------------------------------------------------------------------------- --%>
    <%-- ---------------------------------------------------------------------------------------------------------------------------------------------------- --%>
    <%--Hojas de estilo --%>
    <%--<link href="../../Recursos/bootstrap/css/bootstrap.css" rel="stylesheet" />--%>
    <link href="../../Recursos/estilos/demo_form.css" rel="stylesheet" />
    <link href="../../Recursos/bootstrap-combo/select2.css" rel="stylesheet" />
    <link href="../../Recursos/bootstrap-combo/select2-bootstrap.css" rel="stylesheet" />
    <link href="../../Recursos/bootstrap-table/bootstrap-table.css" rel="stylesheet" />
  

    <%-- ---------------------------------------------------------------------------------------------------------------------------------------------------- --%>
    <%-- ---------------------------------------------------------------------------------------------------------------------------------------------------- --%>
    <%--Javascript --%>
    <script src="../../Recursos/bootstrap-combo/select2.js"></script>
    <script src="../../Recursos/bootstrap-combo/es.js"></script>
   
    <script src="../../Recursos/javascript/seguridad/Js_Controlador_Sesion.js"></script>
    <script src="../../Recursos/plugins/URI.min.js"></script>
    <script src="../../Recursos/plugins/isotope/isotope.pkgd.min.js"></script>
    <script src="../../Recursos/javascript/Operaciones/Orden_Vehiculos/Js_Ope_Orden_Vehiculos.js"></script>

    <script src="../../Recursos/SortableJS/Sortable.js"></script>
    <script src="../../Recursos/SortableJS/prettify.js"></script>
    <script src="../../Recursos/SortableJS/run_prettify.js"></script>

</asp:Content>
<%-- --------------------------------------------------------------------------------------------------------------------------------------- --%>
<%-- --------------------------------------------------------------------------------------------------------------------------------------- --%>
<%-- --------------------------------------------------------------------------------------------------------------------------------------- --%>


<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container-fluid" style="height: 100vh;">
        <div class="page-header">
            <h3 style="font-family: 'Roboto Light', cursive !important; font-size: 24px; font-weight: bold; color: #808080;">Asignar orden de vehiculos</h3>
        </div>
        <%-- --------------------------------------------------------------- --%>
        <div id="Principal"></div>
        <%-- --------------------------------------------------------------- --%>
    </div>

</asp:Content>

   <%--
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">



    <div id="div_principal">
        <div class="row">

            <div class="col-md-6">
                <div id="contenedor_sin_asignacion"></div>
            </div>

            <div class="col-md-6">
             <div id="example1" class="list-group col">
                    <div class="list-group-item">Item 1</div>
                    <div class="list-group-item">Item 2</div>
                    <div class="list-group-item">Item 3</div>
                    <div class="list-group-item">Item 4</div>
                    <div class="list-group-item">Item 5</div>
                    <div class="list-group-item">Item 6</div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>--%>
<%-- --------------------------------------------------------------------------------------------------------------------------------------- --%>
<%-- --------------------------------------------------------------------------------------------------------------------------------------- --%>
<%-- --------------------------------------------------------------------------------------------------------------------------------------- --%>