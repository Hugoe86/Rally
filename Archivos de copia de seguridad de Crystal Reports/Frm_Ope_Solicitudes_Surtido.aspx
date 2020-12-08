<%@ Page Title="" Language="C#" MasterPageFile="~/Paginas/Paginas_Generales/MasterPage.Master" AutoEventWireup="true" CodeBehind="Frm_Ope_Solicitudes_Surtido.aspx.cs" Inherits="web_trazabilidad.Paginas.Trazabilidad.Frm_Ope_Solicitudes_Surtido" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../../Recursos/bootstrap-date/moment.min.js"></script>
    <link href="../../Recursos/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../../Recursos/estilos/demo_form.css" rel="stylesheet" />
    <link href="../../Recursos/bootstrap-combo/select2.css" rel="stylesheet" />
    <link href="../../Recursos/bootstrap-table/bootstrap-table.css" rel="stylesheet" />
    <link href="../../Recursos/bootstrap-table/extensions/editable/bootstrap-editable.css" rel="stylesheet" />
    <link href="../../Recursos/bootstrap-date/bootstrap-datetimepicker.css" rel="stylesheet" />

    <script src="../../Recursos/bootstrap-table-current/bootstrap-table.js"></script>
    <script src="../../Recursos/bootstrap-table-current/locale/bootstrap-table-es-MX.js"></script>
    <script src="../../Recursos/bootstrap-table-current/extensions/export/bootstrap-table-export.js"></script>
    <script src="../../Recursos/bootstrap-table-current/extensions/export/tableExport.js"></script>

    <script src="../../Recursos/bootstrap-combo/select2.js"></script>
    <script src="../../Recursos/bootstrap-combo/es.js"></script>
    <script src="../../Recursos/bootstrap-table/bootstrap-table.min.js"></script>
    <script src="../../Recursos/bootstrap-table/locale/bootstrap-table-es-MX.min.js"></script>
    <script src="../../Recursos/bootstrap-table/extensions/editable/bootstrap-editable.js"></script>
    <script src="../../Recursos/bootstrap-table/extensions/editable/bootstrap-table-editable.min.js"></script>
    <script src="../../Recursos/plugins/parsley.js"></script>
    <script src="../../Recursos/bootstrap-date/bootstrap-datetimepicker.min.js"></script>
    <script src="../../Recursos/plugins/jquery.formatCurrency-1.4.0.min.js"></script>
    <script src="../../Recursos/plugins/jquery.formatCurrency.all.js"></script>
    <script src="../../Recursos/plugins/accounting.min.js"></script>
    <script src="../../Recursos/javascript/seguridad/Js_Controlador_Sesion.js"></script>
    <script src="../../Recursos/plugins/jquery.qtip-1.0.0-rc3.min.js"></script>
    <script src="../../Recursos/javascript/trazabilidad/Js_Ope_Solicitudes_Surtido.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container-fluid" style="height: 100vh;">
        <div id="div_consultar_solicitudes">
            <div class="row">
                <div class="col-sm-12 text-left" style="background-color: white!important;">
                    <h3>Solicitudes</h3>
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
                                for="txt_busqueda_no_solicitud">
                                No. Solicitud</label>
                            <input id="txt_busqueda_no_solicitud" style="margin: 1px !important;"
                                type="text" class="form-control" placeholder="No. de solicitud" />
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
                                <span>Buscar Solicitudes</span>
                            </button>
                        </div>
                    </div>
                </div>
            </div>
            <div id="toolbar" style="margin-left: 5px; text-align: right">
                <div class="btn-group" role="group" style="margin-left: 5px;">
                    <button id="btn_inicio" type="button" class="btn btn-info btn-sm" style="border-radius: 0px !important; border-top-left-radius: 6px !important; border-bottom-left-radius: 6px !important;"><i class="glyphicon glyphicon-home"></i></button>
                     <button id="btn_exportar" type="button" class="btn btn-info btn-sm" title="Exportar Excel"><i class="fa fa-download"></i></button> 
                    <button id="btn_nuevo" type="button" class="btn btn-info btn-sm" style="border-radius: 0px !important; border-top-right-radius: 6px !important; border-bottom-right-radius: 6px !important;"><i class="glyphicon glyphicon-plus"></i></button>
                </div>
            </div>
            <table id="tbl_consulta_solicitudes" data-toolbar="#toolbar" class="table table-responsive"></table>
        </div>


        <div id="div_generar_solicitudes" style="display:none">
            <div class="row">
                <div class="col-sm-9 text-left" style="background-color: white !important;">
                    <h3>Generar Solicitudes</h3>
                </div>
                <div class="col-sm-3" style="text-align: right">
                    <button id="btn_generar_solicitud" type="button" class="btn btn-primary"><i class="fa fa-save"></i><span>&nbsp;&nbsp;Generar</span></button>
                    <button id="btn_cancelar" type="button" class="btn btn-primary"><i class="fa fa-remove"></i><span style="font-weight: bold">&nbsp;&nbsp;Cancelar</span></button>
                </div>
            </div>

            <hr />
            <div class="row">
                <div class="col-sm-6">
                    <span style="width: 100%; text-align: center" class="fuente_lbl_controles">Generar Solicitud por:</span><br />
                    <div class="col-sm-6">
                        <input type="radio" name="rd_tipo" id="rd_general" value="General" checked="checked" />
                        General         
                    </div>
                    <div class="col-sm-6">
                        <input type="radio" name="rd_tipo" id="rd_almacen" value="Almacen" />
                        Almacenes-Ubicaciones      
                    </div>
                </div>
            </div>
            <div style="margin: 8px"></div>
            <div class="row">
                <div class="col-md-1">
                    <label class="fuente_lbl_controles">Almac&eacute;n</label>
                </div>
                <div class="col-md-2">
                    <select id="cmb_almacen" style="width: 100%"></select>
                </div>
                <div class="col-md-1">
                    <label class="fuente_lbl_controles">Ubicaci&oacute;n</label>
                </div>
                <div class="col-md-2">
                    <select id="cmb_ubicacion" style="width: 100%"></select>
                </div>
                <div class="col-md-1">
                    <%--<label class="fuente_lbl_controles">Productos</label>--%>
                </div>
                <div class="col-md-2">
                    <%--<select id="cmb_producto" style="width: 100%"></select>--%>
                </div>
                <div class="col-md-1">
                    <label class="fuente_lbl_controles">&nbsp;</label>
                </div>
                <div class="col-md-2" style="text-align: right">
                    <button id="btn_consultar" type="button" class="btn btn-secondary"><i class="fa fa-search"></i><span style="font-weight: bold">&nbsp;&nbsp;Búsqueda</span></button>
                   
                </div>
            </div>
            <table id="tbl_surtido" class="table table-responsive"></table>
        </div>
    </div>
</asp:Content>
