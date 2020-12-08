<%@ Page Title="" Language="C#" MasterPageFile="~/Paginas/Paginas_Generales/MasterPage.Master" AutoEventWireup="true" CodeBehind="Frm_Ope_Embarques_Productos.aspx.cs" Inherits="web_trazabilidad.Paginas.Trazabilidad.Frm_Ope_Embarques_Productos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../../Recursos/bootstrap-date/moment.min.js"></script>
    <link href="../../Recursos/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../../Recursos/estilos/demo_form.css" rel="stylesheet" />
    <link href="../../Recursos/bootstrap-combo/select2.css" rel="stylesheet" />
    <link href="../../Recursos/bootstrap-table/bootstrap-table.css" rel="stylesheet" />
    <link href="../../Recursos/bootstrap-table/extensions/editable/bootstrap-editable.css" rel="stylesheet" />
    <link href="../../Recursos/bootstrap-date/bootstrap-datetimepicker.css" rel="stylesheet" />
    <link href="../../Recursos/estilos/css_embarques.css" rel="stylesheet" />


    <script src="../../Recursos/bootstrap-combo/select2.js"></script>
    <script src="../../Recursos/bootstrap-combo/es.js"></script>
    <script src="../../Recursos/bootstrap-table/bootstrap-table.min.js"></script>
    <script src="../../Recursos/bootstrap-table/locale/bootstrap-table-es-MX.min.js"></script>

    
    <script src="../../Recursos/bootstrap-table-current/bootstrap-table.js"></script>
    <script src="../../Recursos/bootstrap-table-current/locale/bootstrap-table-es-MX.js"></script>
    <script src="../../Recursos/bootstrap-table-current/extensions/export/bootstrap-table-export.js"></script>
    <script src="../../Recursos/bootstrap-table-current/extensions/export/tableExport.js"></script>

    <script src="../../Recursos/bootstrap-table/extensions/editable/bootstrap-editable.js"></script>
    <script src="../../Recursos/bootstrap-table/extensions/editable/bootstrap-table-editable.min.js"></script>
    <script src="../../Recursos/bootstrap-table-current/extensions/editable/bootstrap-table-editable.js"></script>
    <script src="../../Recursos/plugins/parsley.js"></script>
    <script src="../../Recursos/bootstrap-date/bootstrap-datetimepicker.min.js"></script>
    <script src="../../Recursos/plugins/jquery.formatCurrency-1.4.0.min.js"></script>
    <script src="../../Recursos/plugins/jquery.formatCurrency.all.js"></script>
    <script src="../../Recursos/plugins/accounting.min.js"></script>
    <script src="../../Recursos/javascript/seguridad/Js_Controlador_Sesion.js"></script>
    <script src="../../Recursos/plugins/jquery.qtip-1.0.0-rc3.min.js"></script>
    <script src="../../Recursos/javascript/trazabilidad/Js_Ope_Embarques_Productos.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="div_principal_embarques">

        <div class="container-fluid" style="height: 100vh;">
            <div class="row">
                <div class="col-sm-12 text-left" style="background-color: white!important;">
                    <h3>Salidas</h3>
                </div>
            </div>

            <hr />
             <div class="panel panel-color panel-info" id="pnl_filtros">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        <i style="color: white;" class="glyphicon glyphicon-filter"></i>&nbsp;Filtros de b&uacute;squeda
                    </h3>
                    <div class="panel-options">
                        <a id="ctrl_panel" href="#" data-toggle="panel">
                            <span class="collapse-icon">–</span>
                            <span class="expand-icon">+</span>
                        </a>
                    </div>
                </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-md-2">
                                <label class="fuente_lbl_controles"
                                    for="txt_busqueda_por_no_orden_cliente">
                                    No. Orden del Cliente</label>
                                <input id="txt_busqueda_por_no_orden_cliente" style="margin: 1px !important;"
                                    type="text" class="form-control" placeholder="No. Orden del Cliente" />
                            </div>
                            <div class="col-md-6">
                                <label class="fuente_lbl_controles"
                                    for="cmb_clientes_filtro" style="margin-bottom: 5px !important;">
                                    Clientes</label>
                                <select id="cmb_clientes_filtro"
                                    class="form-control" style="width: 60%;">
                                </select>
                            </div>
                            <div class="col-md-2">
                                <label class="text-bold text-left text-medium fuente_lbl_controles" title="Fecha Inicio" style="margin-bottom: 5px !important;">Fecha Inicio</label>
                                <div class="input-group date"
                                    id="dtp_fecha_inicio">
                                    <input type="text" id="txt_fecha_inicio"
                                        class="form-control" style="margin: 0px;" placeholder="dd/mm/aaaa"
                                        data-parsley-required="true" />
                                    <span class="input-group-addon">
                                        <span class="glyphicon glyphicon-calendar"></span>
                                    </span>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <label class="text-bold text-left text-medium fuente_lbl_controles" title="Fecha Termino" style="margin-bottom: 5px !important;">Fecha Termino</label>
                                <div class="input-group date"
                                    id="dtp_fecha_termino">
                                    <input type="text" id="txt_fecha_termino"
                                        class="form-control" style="margin: 0px;" placeholder="dd/mm/aaaa"
                                        data-parsley-required="true" />
                                    <span class="input-group-addon">
                                        <span class="glyphicon glyphicon-calendar"></span>
                                    </span>
                                </div>
                            </div>
                        </div>

                        <div class="row">

                            <div class="col-md-10">
                            </div>
                            <div class="col-md-2" style="margin-top: 5px !important; text-align: right !important;">
                                <button type="button" id="btn_busqueda"
                                    class="btn btn-secondary btn-icon btn-icon-standalone btn-lg" style="width: 110% !important;">
                                    <i class="fa fa-search"></i>
                                    <span>Buscar Embarques</span>
                                </button>
                            </div>
                        </div>
                    </div>
            </div>
            <div id="toolbar" style="margin-left: 0px; text-align: right">
                <div class="btn-group" role="group" style="margin-left: 0px;">
                    <button id="btn_inicio" type="button" class="btn btn-info btn-sm" style="border-radius: 0px !important; border-top-left-radius: 6px !important; border-bottom-left-radius: 6px !important;"><i class="glyphicon glyphicon-home"></i></button>
                     <button id="btn_exportar" type="button" class="btn btn-info btn-sm" style="border-radius: 0px !important;  height:29px;" title="Exportar Excel"><i class="fa fa-download"></i></button>
                    <button id="btn_nuevo" type="button" class="btn btn-info btn-sm" style="border-radius: 0px !important; border-top-right-radius: 6px !important; border-bottom-right-radius: 6px !important;"><i class="glyphicon glyphicon-plus"></i></button>
                </div>
            </div>
            <table id="tbl_embarques_consulta" data-toolbar="#toolbar" class="table table-responsive"></table>
        </div>

    </div>
    <div id="div_crear_embarques" style="display: none">
        <div class="container-fluid" style="height: 100vh;">
            <div class="row">
                <div class="col-sm-8 text-left" style="background-color: white!important;">
                    <h3>Salidas</h3>
                </div>
                <div class="col-sm-4" align="right">
                    <div id=""></div>
                    <button id="btn_salir" type="button" class="btn btn-primary btn-sm" title=""><i class="glyphicon glyphicon-remove"></i>&nbsp;&nbsp;Cancelar Embarque</button>
                    <button id="btn_regresar" type="button" class="btn btn-primary btn-sm" title=""><i class="glyphicon glyphicon-arrow-left"></i>&nbsp;&nbsp;Regresar</button>
                    <button id="btn_guardar" type="button" class="btn btn-primary btn-sm" title="">
                        <i class="glyphicon glyphicon-floppy-disk"></i>&nbsp;&nbsp;Guardar Orden de Salida
                    </button>
                </div>
            </div>

            <hr style="margin-top: 10px; margin-bottom: 10px;" />

            <div class="row">
                <div class="col-sm-3">
                    <label class="fuente_lbl_controles">(*) No Orden Cliente</label>
                    <div id="div_busqueda_no_orden">
                        <div class="input-group date" id="dtp_search" style="padding-top: 6px">
                            <input type="text" id="txt_no_orden_cliente" class="form-control" style="margin: 0px;" placeholder="(*) No. Orden Cliente" disabled="disabled" />
                            <span class="input-group-addon btn" id="span_buscar"
                                aria-disabled="false">
                                <span class="fa fa-search"></span>
                            </span>
                        </div>
                    </div>
                    <input class="form-control" id="txt_no_orden_cliente_visual" disabled="disabled" style="display:none" />
                </div>
                <div class="col-sm-3">
                    <label class="fuente_lbl_controles">Nombre del Transporte</label>
                    <input id="txt_nombre_transporte" type="text" class="form-control" placeholder="Nombre del Transporte" />
                </div>
                <div class="col-sm-6">
                    <label class="fuente_lbl_controles">(*) Nombre del Operador</label>
                    <input id="txt_nombre_operador" type="text" class="form-control" placeholder="Nombre del Operador" />
                </div>

            </div>
            <div class="row">
                <div class="col-sm-3">
                    <label class="fuente_lbl_controles">Sello</label>
                    <input id="txt_sello" type="text" class="form-control" placeholder="Sello" />
                </div>
                <div class="col-sm-3">
                    <label class="fuente_lbl_controles">(*) No. de Placas</label>
                    <input id="txt_no_placas" type="text" class="form-control" placeholder="No. de Placas" />
                </div>
                <div class="col-sm-3">
                    <label class="fuente_lbl_controles">Numero de Caja</label>
                    <input id="txt_numero_caja" type="text" class="form-control" placeholder="Numero de Caja" />
                </div>
                <div class="col-sm-3">
                    <label class="fuente_lbl_controles">(*) Fecha de Embarque</label>
                    <div class="input-group date" id="dtp_fecha_embarque" style="padding-top: 6px">
                        <input type="text" id="txt_fecha_embarque" class="form-control" style="margin: 0px;" placeholder="dd/mm/aaaa" />
                        <span class="input-group-addon">
                            <span class="glyphicon glyphicon-calendar"></span>
                        </span>
                    </div>
                </div>
            </div>
            <div>
                <input type="hidden" id="txt_subtotal" />
                <input type="hidden" id="txt_impuestos" />
                <input type="hidden" id="txt_total" />
            </div>
            <div class="row">
                <div class="col-sm-12 text-right">
                    <button id="btn_mostrar_contenedores" type="button" style="display: none" class="btn btn-primary btn-sm" title="">
                        <i class="fa fa-th-list"></i>&nbsp;&nbsp;Mostrar Contenedores
                    </button>

                    <button id="btn_mostrar_orden_cliente" type="button" class="btn btn-primary btn-sm" title="" data-toggle="collapse" data-target="#row_oc">
                        <i id="icon_oc" class="fa fa-folder-o"></i>&nbsp;&nbsp;Mostrar Orden Cliente
                    </button>
                </div>
            </div>
            <hr />
            <div id="row_oc" class="row collapse">
                <div class="col-md-12">
                    <table id="tbl_detalles" class="table table-responsive"></table>
                </div>
            </div>
            <div class="row" style="margin-top: 7px">
                <div id="div_contenedor">
                    <div class="row">
                        <div class="col-md-12">
                            <table id="tbl_contenedores" class="table table-responsive"></table>

                        </div>
                    </div>
                    <div class="row" style="margin-top: 7px">
                    </div>
                    <div class="row">
                        <div class="col-md-12 text-right">
                            <button id="btn_aplicar" type="button" class="btn btn-primary btn-sm"><i class="fa fa-check"></i>&nbsp;&nbsp;Agregar a la Orden</button>
                        </div>
                    </div>
                    <div class="row" style="margin-top: 7px">
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <table id="tbl_detalles_embarques" class="table table-responsive"></table>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
