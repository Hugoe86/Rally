<%@ Page Title="" Language="C#" MasterPageFile="~/Paginas/Paginas_Generales/MasterPage.Master" AutoEventWireup="true" CodeBehind="Frm_Ope_Inventarios_Productos.aspx.cs" Inherits="web_trazabilidad.Paginas.Trazabilidad.Frm_Ope_Inventarios_Productos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../Recursos/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../../Recursos/estilos/demo_form.css" rel="stylesheet" />
    <link href="../../Recursos/bootstrap-combo/select2.css" rel="stylesheet" />
    <link href="../../Recursos/bootstrap-table-current/bootstrap-table.css" rel="stylesheet" />
    <link href="../../Recursos/bootstrap-date/bootstrap-datetimepicker.css" rel="stylesheet" />
    <link href="../../Recursos/estilos/center_loader.css" rel="stylesheet" />

    <script src="../../Recursos/bootstrap-date/moment.min.js"></script>
    <script src="../../Recursos/bootstrap-date/bootstrap-datetimepicker.min.js"></script>
    <script src="../../Recursos/bootstrap-combo/select2.js"></script>
    <script src="../../Recursos/bootstrap-combo/es.js"></script>
    <script src="../../Recursos/plugins/parsley.js"></script>
    <script src="../../Recursos/bootstrap-table-current/bootstrap-table.js"></script>
    <script src="../../Recursos/bootstrap-table-current/locale/bootstrap-table-es-MX.js"></script>
    <script src="../../Recursos/javascript/seguridad/Js_Controlador_Sesion.js"></script>
    <script src="../../Recursos/bootstrap-box/bootstrap-number-input.js"></script>
    <script src="../../Recursos/javascript/trazabilidad/Js_Ope_Inventario_Productos.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid" style="height: 100vh;">
        <div id="div_transacciones">
            <div class="page-header">
                <div class="row">
                    <div class="col-sm-12 text-left" style="background-color: white!important;">
                        <h3>Transacciones por Producto</h3>
                    </div>
                </div>
            </div>

            <div class="panel panel-color panel-info" id="pnl_filtros">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        <i style="color: white;" class="glyphicon glyphicon-filter"></i>&nbsp;Filtros de b&uacute;squeda
                    </h3>
                    <div class="panel-options">
                        <a href="#" class="btn-popover-secondary"
                            data-toggle="popover"
                            data-trigger="hover"
                            data-placement="bottom"
                            data-original-title="Filtros"
                            data-content="Filtros para Consultar Inventario">
                            <i class="fa fa-question-circle"></i>
                        </a>
                        <a id="ctrl_panel" href="#" data-toggle="collapse" data-parent="#pnl_filtros" data-target="#pnl_filtros_body">
                            <span class="glyphicon glyphicon-minus"></span>
                        </a>
                    </div>
                </div>
                <div id="pnl_filtros_body" class="panel-collapse collapse in">
                    <div class="panel-body">

                        <div class="row">
                            <div class="col-md-4" >
                                <label for="cmb_producto_filtro" class="fuente_lbl_controles">No. de Parte</label>
                                <select id="cmb_producto_filtro" style="width: 100% !important"></select>
                            </div>
                            <div class="col-md-2" >
                                <label class="fuente_lbl_controles">Control Stock</label>
                                <select id="cmb_ctrl_stock_filtro" style="width: 100% !important"></select>
                            </div>
                            <div class="col-md-2" >
                                <label for="txt_lote_serie_filtro" class="text-bold text-left fuente_lbl_controles">No. Lote/Serie</label>
                                <input type="text" id="txt_lote_serie_filtro" style="margin: 0px;" class="form-control" placeholder="Ingrese Lote o Serie" />
                            </div>
                            <div class="col-md-2">
                                <label class="fuente_lbl_controles">Fecha Inicio</label>
                                <div class="input-group date" id="dtp_fecha_inicio" style="z-index: 1000 !important;">
                                    <input type="text" id="fi" class="form-control" style="margin: 0px;" placeholder="mm/dd/aaaa" />
                                    <span class="input-group-addon">
                                        <span class="glyphicon glyphicon-calendar"></span>
                                    </span>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <label class="fuente_lbl_controles">Fecha Fin</label>
                                <div class="input-group date" id="dtp_fecha_termino" style="z-index: 1000 !important;">
                                    <input type="text" id="ff" class="form-control" style="margin: 0px;" placeholder="mm/dd/aaaa" />
                                    <span class="input-group-addon">
                                        <span class="glyphicon glyphicon-calendar"></span>
                                    </span>
                                </div>
                            </div>
                        </div>
                        <div class="row" style="margin-top: 8px !important;">
                            <div class="col-md-2  col-md-offset-10" align="right">
                                <button type="button" id="btn_busqueda" class="btn btn-secondary btn-icon btn-icon-standalone btn-lg" style="width: 100% !important">
                                    <i class="fa fa-search"></i>
                                    <span>Buscar</span>
                                </button>
                            </div>
                        </div> 
                    </div>
                </div>
            </div>
            <hr />
            <div class="row">
                <div class="col-md-12">
                    <div id="toolbar" style="margin-left: 5px;">
                        <div class="btn-group" role="group" style="margin-left: 5px;">
                            <button id="btn_inicio" type="button" class="btn btn-info btn-sm" title="Salir"><i class="glyphicon glyphicon-home"></i></button>
                            <button id="btn_exportar" type="button" class="btn btn-info btn-sm" title="Exportar Excel"><i class="fa fa-download"></i></button>
                        </div>
                    </div>
                    <table id="tbl_inventario" data-toolbar="#toolbar" class="table table-responsive"></table>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
