<%@ Page Title="" Language="C#" MasterPageFile="~/Paginas/Paginas_Generales/MasterPage.Master" AutoEventWireup="true" CodeBehind="Frm_Ope_Reabastecimiento.aspx.cs" Inherits="web_trazabilidad.Paginas.Trazabilidad.Frm_Ope_Reabastecimiento" %>

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
    <script src="../../Recursos/javascript/trazabilidad/Js_Ope_Reabastecimiento.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid" style="height: 100vh;">
        <div id="div_consultar_solicitudes">
            <div class="row">
                <div class="col-sm-12 text-left" style="background-color: white!important;">
                    <h3>Reabastecimiento de productos</h3>
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

             <div id="toolbar" style="margin-left: 5px;">
            <div class="btn-group" role="group" style="margin-left: 5px;">
                <button id="btn_salir" type="button" class="btn btn-info btn-sm" title="Salir"><i class="glyphicon glyphicon-home"></i></button>
                 <button id="btn_exportar" type="button" class="btn btn-info btn-sm" title="Exportar Excel"><i class="fa fa-download"></i></button>
                <%--<button id="btn_nuevo" type="button" class="btn btn-info btn-sm" title="Nuevo"><i class="glyphicon glyphicon-plus"></i></button>--%>
            </div>
           </div>

            <table id="tbl_consulta_solicitudes" data-toolbar="#toolbar" class="table table-responsive"></table>

            <br />

        </div>


        <div id="div_reabastecimiento" style="display: none">
            <div class="row">
                <div class="col-sm-10 text-left" style="background-color: white!important;">
                    <h3>Reabastecimiento de productos</h3>
                </div>
                <div class="col-sm-2" style="text-align: right !important; display:none;">
                    <button id="btn_reabastecer" type="button" class="btn btn-success"><i class="fa fa-check"></i><span>&nbsp;Reabastecer</span></button>
                </div>
                <div class="col-sm-2" style="">
                    <button id="btn_regresar" type="button" class="btn btn-primary form-control"><i class="fa fa-reply"></i><span>&nbsp;Regresar</span></button>
                </div>

            </div>
            <div style="">

            </div>
            <hr />
            <div class="row">
                <div class="col-md-12">
                    <table id="tbl_consulta_detalles_solicitud" class="table table-responsive"></table>
                </div>
            </div>
        </div>

    </div>
</asp:Content>
