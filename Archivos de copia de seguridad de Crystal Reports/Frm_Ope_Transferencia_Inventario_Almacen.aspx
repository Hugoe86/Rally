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
    <script src="../../Recursos/javascript/trazabilidad/Js_Transferencia_Inventario_Almacenamiento.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid" style="height: 100vh;">
        <div class="page-header">
            <div class="row">
                <div class="col-sm-12 text-left" style="background-color: white!important;">
                    <h3>Transferencia de Inventario de Centro de Producción a Ubicación de Almacenamiento</h3>
                </div>
            </div>
        </div>
        <div id="div_principal">
            <div class="row">
                <div class="col-md-3">
                    <label class="fuente_lbl_controles">No. Orden de Producción</label>
                    <input id="txt_no_orden_filtro" type="text" class="form-control" style="margin-top:0px" placeholder="No Orden de Producción."/>
                </div>
                <div class="col-md-3">
                    <label class="fuente_lbl_controles">Centro de Produccion</label>
                    <select id="cmb_ubicacion_filtro" style="width: 100% !important"></select>
                </div>
                <div class="col-md-6" style="text-align: right">
                    <br />
                    <button id="btn_consultar" class="btn btn-info"><i class="fa fa-search"></i>&nbsp;Buscar</button>
                    <%--<button id="btn_transferir" class="btn btn-success"><i class="fa fa-exchange"></i>&nbsp;Transferir</button>--%>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <table id="tbl_ordenes_produccion" class="table table-responsive"></table>
                </div>
            </div>
        </div>
        <div id="div_transferencia">
            <div class="row">
                <div class="col-md-12" style="text-align: right">
                    <br />
                    <button id="btn_cancelar" class="btn btn-danger"><i class="fa fa-times"></i>&nbsp;Cancelar</button>
                    <button id="btn_transferir" class="btn btn-success"><i class="fa fa-exchange"></i>&nbsp;Transferir</button>
                </div>
            </div>
            <div class="row">
                <div class="col-md-3">
                    <label class="fuente_lbl_controles">Orden de Producción</label>
                    <input type="text" class="form-control" id="txt_orden_produccion" style="margin-top:0px" />
                </div>
                <div class="col-md-3">
                    <label class="fuente_lbl_controles">(*) Centro de Produccion</label>
                    <select id="cmb_centro_trabajo" style="width: 100% !important"></select>
                </div>
                <div class="col-md-3">
                    <label class="fuente_lbl_controles">(*) Almacen de Almacenamiento</label>
                    <select id="cmb_almacen" style="width: 100% !important"></select>
                </div>
                <div class="col-md-3">
                    <label class="fuente_lbl_controles">(*) Ubicación de Almacenamiento</label>
                    <select id="cmb_ubicacion_almacenamiento" style="width: 100% !important"></select>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <table id="tbl_transacciones" class="table table-responsive"></table>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
