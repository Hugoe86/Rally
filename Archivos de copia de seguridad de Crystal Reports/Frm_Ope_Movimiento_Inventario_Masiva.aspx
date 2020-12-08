<%@ Page Title="" Language="C#" MasterPageFile="~/Paginas/Paginas_Generales/MasterPage.Master" AutoEventWireup="true" CodeBehind="Frm_Ope_Transferencia_Inventario_Almacen.aspx.cs" Inherits="web_trazabilidad.Paginas.Trazabilidad.Frm_Ope_Transferencia_Inventario_Almacen" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--Hojas de estilo --%>
    <link href="../../Recursos/bootstrap/css/bootstrap.css" rel="stylesheet" />
    <link href="../../Recursos/estilos/demo_form.css" rel="stylesheet" />
    <link href="../../Recursos/bootstrap-combo/select2.css" rel="stylesheet" />
    <link href="../../Recursos/bootstrap-combo/select2-bootstrap.css" rel="stylesheet" />
    <link href="../../Recursos/bootstrap-table/bootstrap-table.css" rel="stylesheet" />
    <link href="../../Recursos/bootstrap-table/extensions/editable/bootstrap-editable.css" rel="stylesheet" />

    <%--Javascript --%>
    <script src="../../Recursos/bootstrap-combo/select2.js"></script>
    <script src="../../Recursos/bootstrap-combo/es.js"></script>
    <script src="../../Recursos/bootstrap-table/bootstrap-table.min.js"></script>
    <script src="../../Recursos/bootstrap-table/locale/bootstrap-table-es-MX.min.js"></script>
    <script src="../../Recursos/bootstrap-table/extensions/editable/bootstrap-editable.js"></script>
    <script src="../../Recursos/bootstrap-table/extensions/editable/bootstrap-table-editable.min.js"></script>
    <script src="../../Recursos/plugins/parsley.js"></script>
    <script src="../../Recursos/javascript/seguridad/Js_Controlador_Sesion.js"></script>
    <script src="../../Recursos/javascript/trazabilidad/Js_Ope_Transferencia_Inventario_Masivo.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid" style="height: 100vh;">
        <div class="page-header">
            <div class="row">
                <div class="col-sm-12 text-left" style="background-color: white!important;">
                    <h3>Transferencia de Inventario Masivo</h3>
                </div>
            </div>
        </div>
        <div id="div_principal">
            <div class="row">
                <div class="col-md-3">
                    <label class="fuente_lbl_controles">Producto</label>
                    <select id="cmb_producto_filtro" style="width: 100% !important"></select>
                </div>
                <div class="col-md-3">
                    <label class="fuente_lbl_controles">(*) Almacen de Origen</label>
                    <select id="cmb_almacen_filtro" style="width: 100% !important"></select>
                </div>
                <div class="col-md-3">
                    <label class="fuente_lbl_controles">(*) Ubicación de Origen</label>
                    <select id="cmb_ubicacion_filtro" style="width: 100% !important"></select>
                </div>
                <div class="col-md-3" style="text-align: right">
                    <br />
                    <button id="btn_consultar" class="btn btn-info"><i class="fa fa-search"></i>&nbsp;Buscar</button>
                </div>
            </div>

            <div class="row">
 
            </div>
            <div class="row">
                <div class="col-md-3">
                    <label class="fuente_lbl_controles">(*) Almacen Destino</label>
                    <select id="cmb_almacen" style="width: 100% !important"></select>
                </div>
                <div class="col-md-3">
                    <label class="fuente_lbl_controles">(*) Ubicación Destino</label>
                    <select id="cmb_ubicacion_almacenamiento" style="width: 100% !important"></select>
                </div>
                <div class="col-md-6" style="text-align: right">
                    <br />
                    <button id="btn_transferir" class="btn btn-success"><i class="fa fa-exchange"></i>&nbsp;Transferir</button>
                </div>
            </div>


            <div class="row">
                <div class="col-md-12">
                    <table id="tbl_productos" class="table table-responsive"></table>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
