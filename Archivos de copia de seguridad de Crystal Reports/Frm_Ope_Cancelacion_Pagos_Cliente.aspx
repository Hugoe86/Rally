<%@ Page Title="Cancelacion pagos clientes" Language="C#" AutoEventWireup="true" MasterPageFile="~/Paginas/Paginas_Generales/MasterPage.Master" CodeBehind="Frm_Ope_Cancelacion_Pagos_Cliente.aspx.cs" Inherits="web_trazabilidad.Paginas.Facturacion.Frm_Ope_Cancelacion_Pagos_Cliente" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../Recursos/bootstrap-table/bootstrap-table.min.css" rel="stylesheet" />
    <link href="../../Recursos/estilos/center_loader.css" rel="stylesheet" />
    <link href="../../Recursos/estilos/demo_form.css" rel="stylesheet" />
    <link href="../../Recursos/bootstrap-combo/select2.css" rel="stylesheet" />
    <link href="../../Recursos/bootstrap-date/bootstrap-datetimepicker.css" rel="stylesheet" />


    <script src="../../Recursos/bootstrap-combo/select2.js"></script>
    <script src="../../Recursos/bootstrap-combo/es.js"></script>
    <script src="../../Recursos/bootstrap-date/moment.min.js"></script>
    <script src="../../Recursos/bootstrap-date/bootstrap-datetimepicker.min.js"></script>

    <script src="../../Recursos/bootstrap-table-current/bootstrap-table.js"></script>
    <script src="../../Recursos/bootstrap-table-current/locale/bootstrap-table-es-MX.js"></script>
    <script src="../../Recursos/bootstrap-table-current/extensions/export/bootstrap-table-export.js"></script>
    <script src="../../Recursos/bootstrap-table-current/extensions/export/tableExport.js"></script>

    <script src="../../Recursos/plugins/center-loader.min.js"></script>
    <script src="../../Recursos/plugins/parsley.js"></script>
    <script src="../../Recursos/bootstrap-table/bootstrap-table.min.js"></script>
    <script src="../../Recursos/bootstrap-table/locale/bootstrap-table-es-MX.min.js"></script>
    <script src="../../Recursos/javascript/seguridad/Js_Controlador_Sesion.js"></script>
    <script src="../../Recursos/jquery-numeric/jquery.numeric.js"></script>
    <script src="../../Recursos/javascript/facturacion/Js_Ope_Cancelacion_Pago_Clientes.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid" style="height: 100vh;">

        <div class="page-header">
            <h3 style="font-family: 'Roboto Light', cursive !important; font-size: 24px; font-weight: bold; color: #808080;">Cancelación pago cliente</h3>
        </div>

        <div id="div_principal">

            <div class="panel panel-color panel-info" id="panel1">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        <i style="color: white;" class="glyphicon glyphicon-filter"></i>&nbsp;Filtros de b&uacute;squeda
                    </h3>
                    <div class="panel-options">
                    </div>
                </div>
                <div class="panel-body" id="panel_body">

                    <div class="row">
                        <div class="col-md-4">
                            <label class="fuente_lbl_controles">Cliente</label>
                            <select id="cmb_cliente" class="form-control"></select>
                        </div>
                        <div class="col-md-2">
                            <label class="fuente_lbl_controles">No. Factura</label>
                            <input type="text" id="txt_no_factura" class="form-control" placeholder="No. Factura" />
                        </div>
                        <div class="col-md-2">
                            <label class="fuente_lbl_controles">No. Recibo</label>
                            <input type="text" id="txt_no_recibo" class="form-control" placeholder="No. Recibo" />
                        </div>
                        <div class="col-md-2">
                            <label class="fuente_lbl_controles" style="margin-bottom: 8px;">Fecha Inicio</label>
                            <div class="input-group date" id="dtp_fecha_inicio">
                                <input type="text" id="fi" class="form-control" style="margin: 0px;" placeholder="dd/mm/aaaa" />
                                <span class="input-group-addon">
                                    <span class="glyphicon glyphicon-calendar"></span>
                                </span>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <label class="fuente_lbl_controles" style="margin-bottom: 8px;">Fecha Termino</label>
                            <div class="input-group date" id="dtp_fecha_termino">
                                <input type="text" id="ff" class="form-control" style="margin: 0px;" placeholder="dd/mm/aaaa" />
                                <span class="input-group-addon">
                                    <span class="glyphicon glyphicon-calendar"></span>
                                </span>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-12" style="text-align: right; margin-top: 18px">
                            <button type="button" id="btn_busqueda" class="btn btn-secondary btn-icon btn-icon-standalone btn-lg">
                                <i class="fa fa-search"></i>
                                <span>Buscar</span>
                            </button>
                        </div>
                    </div>

                </div>
            </div>

            <div id="toolbar" style="margin-left: 5px;">
                <div class="btn-group" role="group" style="margin-left: 5px;">
                    <button id="btn_salir" type="button" class="btn btn-info btn-sm" title="Salir"><i class="glyphicon glyphicon-home"></i></button>
                </div>
            </div>
            <table id="tbl_pagos_cliente" data-toolbar="#toolbar" class="table table-responsive">
                <caption><strong>Movimientos realizados</strong></caption>
            </table>
        </div>


        <div id="div_detalles_factura" style="display: none">
            <div class="tab-pane" id="factura">
                <div class="panel panel-color panel-info" id="panel2">
                    <div class="panel-heading">
                        <h3 class="panel-title">
                            <i style="color: white;" class="fa fa-address-card-o"></i>&nbsp;Datos de pago
                        </h3>
                    </div>

                    <div class="row">
                        <div class="col-md-4">
                            <label class="fuente_lbl_controles">Cliente</label>
                            <input type="text" id="txt_cliente_detalle" class="form-control" placeholder="Proveedor" />
                            <input type="hidden" id="txt_no_movimiento" />
                        </div>
                        <div class="col-md-3">
                            <label class="fuente_lbl_controles">No. Factura</label>
                            <input type="text" id="txt_no_factura_detalle" class="form-control" placeholder="No Factura" />
                        </div>
                        <div class="col-md-2">
                            <label class="fuente_lbl_controles">Serie</label>
                            <input type="text" id="txt_serie" class="form-control" placeholder="Serie" />
                        </div>
                        <div class="col-md-3">
                            <label class="fuente_lbl_controles">Forma de pago</label>
                            <input type="text" id="txt_forma_de_pago" class="form-control" placeholder="Forma de pago" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                            <label class="fuente_lbl_controles" style="margin-bottom: 8px;">Fecha</label>
                            <div class="input-group date" id="dtp_fecha">
                                <input type="text" id="txt_fecha" class="form-control" style="margin: 0px;" placeholder="dd/mm/aaaa" />
                                <span class="input-group-addon">
                                    <span class="glyphicon glyphicon-calendar"></span>
                                </span>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <label class="fuente_lbl_controles">Moneda</label>
                            <input type="text" id="txt_moneda" class="form-control" placeholder="Moneda" />
                        </div>
                        <div class="col-md-3">
                            <label class="fuente_lbl_controles">Pago</label>
                            <input type="text" id="txt_pago" class="form-control" placeholder="pago" />
                        </div>
                        <div class="col-md-3">
                        </div>
                        <div class="col-md-1 pull-right"  style="margin-top: 21px;">
                            <button id="btn_regresar_principal" type="button" class="btn btn-info btn-sm" title="Regresar"><i class="glyphicon glyphicon-circle-arrow-left"></i>&nbsp;&nbsp;Regresar</button>
                        </div>
                        <div class="col-md-1 pull-right" style="margin-top: 21px;">
                            <button id="btn_cancelar" type="button" class="btn btn-info btn-sm" title="Cancelar" style="margin-left: 8px;">
                                <i class="fa fa-times"></i>&nbsp;&nbsp;Cancelar
                            </button>
                        </div>
                    </div>

                </div>
            </div>
        </div>

    </div>
</asp:Content>

