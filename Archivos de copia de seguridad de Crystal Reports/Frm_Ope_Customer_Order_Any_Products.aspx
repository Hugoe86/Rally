<%@ Page Title="Orden del Cliente" Language="C#" MasterPageFile="~/Paginas/Paginas_Generales/MasterPage.Master" AutoEventWireup="true"
    CodeBehind="Frm_Ope_Customer_Order_Any_Products.aspx.cs" Inherits="web_trazabilidad.Paginas.Trazabilidad.Frm_Ope_Customer_Order_Any_Products" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../../Recursos/bootstrap-date/moment.min.js"></script>
    <link href="../../Recursos/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../../Recursos/estilos/demo_form.css" rel="stylesheet" />
    <link href="../../Recursos/bootstrap-combo/select2.css" rel="stylesheet" />
    <link href="../../Recursos/bootstrap-table/bootstrap-table.css" rel="stylesheet" />
    <link href="../../Recursos/bootstrap-table/extensions/editable/bootstrap-editable.css" rel="stylesheet" />
    <link href="../../Recursos/bootstrap-date/bootstrap-datetimepicker.css" rel="stylesheet" />
    <link href="../../Recursos/estilos/css_orden_compra.css" rel="stylesheet" />
    <link href="../../Recursos/estilos/center_loader.css" rel="stylesheet" />

    <script src="../../Recursos/bootstrap-table-current/bootstrap-table.js"></script>
    <script src="../../Recursos/bootstrap-table-current/locale/bootstrap-table-es-MX.js"></script>
    <script src="../../Recursos/bootstrap-table-current/extensions/export/bootstrap-table-export.js"></script>
    <script src="../../Recursos/bootstrap-table-current/extensions/export/tableExport.js"></script>

    <script src="../../Recursos/bootstrap-combo/select2.js"></script>
    <script src="../../Recursos/bootstrap-combo/es.js"></script>

    <script src="../../Recursos/bootstrap-table-current/bootstrap-table.js"></script>
    <script src="../../Recursos/bootstrap-table-current/locale/bootstrap-table-es-MX.js"></script>
    <script src="../../Recursos/bootstrap-table/extensions/editable/bootstrap-editable.js"></script>
    <script src="../../Recursos/bootstrap-table-current/extensions/editable/bootstrap-table-editable.js"></script>

    <script src="../../Recursos/plugins/parsley.js"></script>
    <script src="../../Recursos/bootstrap-date/bootstrap-datetimepicker.min.js"></script>
    <script src="../../Recursos/plugins/jquery.formatCurrency-1.4.0.min.js"></script>
    <script src="../../Recursos/plugins/jquery.formatCurrency.all.js"></script>
    <script src="../../Recursos/plugins/accounting.min.js"></script>
    <script src="../../Recursos/javascript/seguridad/Js_Controlador_Sesion.js"></script>
    <script src="../../Recursos/plugins/jquery.qtip-1.0.0-rc3.min.js"></script>

    <script src="../../Recursos/javascript/trazabilidad/Js_Customer_Order_Any_Products.js" type="text/javascript"></script>

    <!-- Angular - Proveedores -->
    <script src="../../Recursos/angular/app/services/ClienteService.js"></script>
    <script src="../../Recursos/angular/app/controllers/ClienteController.js"></script>

    <!-- Angular - Productos -->
    <script src="../../Recursos/angular/app/services/ProductoService.js"></script>
    <script src="../../Recursos/angular/app/controllers/ProductoController.js"></script>

    <style>
        hr {
            margin-top: 5px !important;
            margin-bottom: 10px !important;
        }

        .panel-heading a.collapsed {
            background-color: transparent !important;
        }

        .panel .panel-heading {
            font-size: 11px;
        }

            .panel .panel-heading > .panel-options a[data-toggle="remove"] {
                font-size: 11px;
            }

            .panel .panel-heading > .panel-options {
                font-size: 11px;
            }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="div_principal">

        <div class="container-fluid" style="height: 100vh;">
            <div class="row">
                <div class="col-sm-12 text-left" style="background-color: white!important;">
                    <h3>Órdenes de Cliente</h3>
                </div>
            </div>

            <hr />
            <div class="panel panel-color panel-info" id="pnl_filtros">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        <i style="color: white;" class="glyphicon glyphicon-filter"></i>&nbsp;Filtros de b&uacute;squeda
                    </h3>
                    <div class="panel-options">
                        <a id="ctrl_panel" href="#" data-toggle="collapse" data-parent="#pnl_filtros" data-target="#pnl_filtros_body">
                            <span class="glyphicon glyphicon-minus"></span>
                        </a>

                    </div>
                </div>
                <div id="pnl_filtros_body" class="panel-collapse collapse in">
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-md-3">
                                <label class="text-bold text-left text-medium fuente_lbl_controles" for="txt_orden_cliente_filtro">No. Orden</label>
                                <input type="text" id="txt_orden_cliente_filtro" class="form-control" style="margin: 0px !important;" placeholder="Busqueda por No. Orden" />
                            </div>
                            <div class="col-md-4">
                                <label for="cmb_cliente_filtro" class="text-bold text-left text-medium fuente_lbl_controles">Cliente</label>
                                <select id="cmb_cliente_filtro" class="form-control input-sm" style="border-radius: inherit"></select>
                            </div>
                            <div class="col-md-3">
                                <label class="text-bold text-left text-medium fuente_lbl_controles">Estatus</label>
                                <select id="cmb_estatus_filtro" name="cmb_estatus_filtro" class="form-control input-sm" style="border-radius: inherit"></select>
                            </div>
                            <div class="col-md-2 text-right" style="margin-top: 20px !important;">
                                <button type="button" id="btn_buscar" class="btn btn-secondary btn-icon btn-icon-standalone btn-lg">
                                    <i class="fa fa-search"></i>
                                    <span>Buscar</span>
                                </button>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
            <div id="toolbar" style="margin-left: 5px; text-align: right; margin-top: 10px;">
                <div class="btn-group" role="group" style="margin-left: 5px;">
                    <button id="btn_inicio" type="button" class="btn btn-info btn-sm" style="border-radius: 0px !important; border-top-left-radius: 6px !important; border-bottom-left-radius: 6px !important;"><i class="glyphicon glyphicon-home"></i></button>
                    <button id="btn_exportar" type="button" class="btn btn-info btn-sm" style="border-radius: 0px !important; height: 30px;" title="Exportar Excel"><i class="fa fa-download"></i></button>
                    <button id="btn_nuevo" type="button" class="btn btn-info btn-sm" style="border-radius: 0px !important; border-top-right-radius: 6px !important; border-bottom-right-radius: 6px !important;"><i class="glyphicon glyphicon-plus"></i></button>
                </div>
            </div>
            <table id="tbl_ordenes_cliente" data-toolbar="#toolbar" class="table table-responsive"></table>
        </div>

    </div>
    <div id="div_orden_cliente" style="display: none;">
        <div class="container-fluid" style="height: 100vh;">

            <%--<div class="page-header">--%>
            <div class="row" style="display: none !important;">
                <div class="col-sm-12 text-left" style="background-color: white!important;">
                    <h3>Generar Orden del Cliente</h3>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-12 text-right">
                    <button id="btn_cancelar" type="button" class="btn btn-primary btn-sm" title="">
                        <i class="glyphicon glyphicon-remove"></i>&nbsp;&nbsp;Cancelar Orden del Cliente
                    </button>
                    <button id="btn_guardar_orden_cliente" type="button" class="btn btn-primary btn-sm" title="">
                        <i class="glyphicon glyphicon-floppy-disk"></i>&nbsp;&nbsp;Guardar Orden del Cliente
                    </button>
                </div>
            </div>
            <hr />
            <div class="row">
                <div class="col-md-3">
                    <%--<label class="text-bold text-left text-medium fuente_lbl_controles">Cliente</label>--%>
                    <div class="form-inline">
                        <label for="cmb_cliente" class="fuente_lbl_controles">Cliente</label>
                        <span id="btnCrearCliente" class="fa fa-plus-circle" title="Crear cliente"
                            style="background: '#F5F6CE'; color: '#2d2d30'; cursor: pointer;"></span>
                    </div>
                    <select class="form-control" id="cmb_cliente" data-parsley-required="true"></select>
                </div>
                <div class="col-md-3">
                    <%--<label class="text-bold text-left text-medium fuente_lbl_controles">Producto</label>--%>
                    <div class="form-inline">
                        <label for="cmb_producto" class="fuente_lbl_controles">Producto</label>
                        <%--<span id="btnCrearProducto" class="fa fa-plus-circle" title="Crear producto" 
                            style="background: '#F5F6CE'; color: '#2d2d30'; cursor: pointer;"></span>--%>
                    </div>
                    <select class="form-control" id="cmb_producto" data-parsley-required="true"></select>
                </div>
                <div class="col-md-1">
                    <label class="text-bold text-left text-medium fuente_lbl_controles">Cantidad</label>
                    <input type="text" class="form-control" id="txt_no_piezas_contenedor" data-parsley-required="true" style="margin-top: 0px" />
                </div>
                <div class="col-md-2">
                    <label class="text-bold text-left text-medium fuente_lbl_controles">Precio</label>
                    <input type="text" class="form-control" id="txt_precio" data-parsley-required="true" style="margin-top: 0px" />
                </div>
                <div class="col-md-3">
                    <label class="text-bold text-left text-medium fuente_lbl_controles">Impuesto</label>
                    <select class="form-control" id="cmb_impuesto" data-parsley-required="true"></select>
                </div>

            </div>
            <div class="row" style="/*margin-top: 7px; */"></div>
            <div class="row">
                <div class="col-md-3">
                    <label class="text-bold text-left text-medium fuente_lbl_controles">No. Orden Cliente</label>
                    <input type="text" class="form-control" id="txt_no_orden_cliente" data-parsley-required="true" style="margin-top: 0px" />
                </div>
                <div class="col-md-3">
                    <label class="text-bold text-left text-medium fuente_lbl_controles">Inicio Vigencia</label>
                    <div class="input-group date" id="dtp_fecha_inicio_vig">
                        <input type="text" id="txt_fecha_inicio_vig" class="form-control" style="margin: 0px;" placeholder="dd/mm/aaaa" data-parsley-required="true" />
                        <span class="input-group-addon">
                            <span class="glyphicon glyphicon-calendar"></span>
                        </span>
                    </div>
                </div>
                <div class="col-md-3">
                    <label class="text-bold text-left text-medium fuente_lbl_controles">Termino Vigencia</label>
                    <div class="input-group date" id="dtp_fecha_termino_vig">
                        <input type="text" id="txt_fecha_termino_vig" class="form-control" style="margin: 0px;" placeholder="dd/mm/aaaa" data-parsley-required="true" />
                        <span class="input-group-addon">
                            <span class="glyphicon glyphicon-calendar"></span>
                        </span>
                    </div>
                </div>
            </div>
            <div class="row" style="margin-top: 7px;"></div>
            <div class="row">
                <div class="col-md-12">
                    <label class="text-bold text-left text-medium fuente_lbl_controles">Observaciones</label>
                    <textarea id="txt_observaciones" class="form-control input-sm" rows="5" placeholder="Observaciones" data-parsley-required="true" maxlength="250" style="min-height: 50px !important; resize: none; margin-top: 0px"></textarea>
                </div>
            </div>
            <div class="row" style="margin-top: 7px;"></div>
            <div class="row" style="display: none">
                <div class="col-md-1">
                    <label class="text-bold text-left text-medium fuente_lbl_controles">Comentarios</label>
                </div>
                <div class="col-md-11">
                    <textarea id="txt_comentario" class="form-control input-sm" rows="5" placeholder="Comentarios" data-parsley-required="true" maxlength="250" style="min-height: 50px !important; resize: none;"></textarea>
                </div>
            </div>
            <div class="row">
                <div class="col-md-10"></div>
                <div class="col-md-2">
                    <button id="btn_agregar" type="button" class="btn btn-success form-control" title="" style="margin: 0px; padding: 4px 0px; font-family: Calibri; font-weight: 600; letter-spacing: .5px;">
                        <i class="fa fa-angle-double-right"></i>&nbsp;&nbsp;Agregar No. Parte
                    </button>
                </div>
            </div>
            <div class="row" style="margin-top: 7px;"></div>
            <div class="row">
                <div class="col-md-12">
                    <table class="table table-responsive" id="tbl_productos"></table>
                </div>

            </div>
            <div style="display: none">
                <input id="txt_nombre_producto" type="hidden" />
                <input id="txt_orden_cliente_mod" type="hidden" />
                <input id="txt_modificar_producto" type="hidden" />
                <%--<input id="txt_no_piezas_contenedor" type="text" />--%>
                <input id="txt_cantidad_embarque" type="text" />
                <input type="text" id="txt_tasa" />
            </div>
        </div>

    </div>
</asp:Content>
