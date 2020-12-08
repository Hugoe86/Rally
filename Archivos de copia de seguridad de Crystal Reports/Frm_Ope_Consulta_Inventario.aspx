<%@ Page Title="" Language="C#" MasterPageFile="~/Paginas/Paginas_Generales/MasterPage.Master" AutoEventWireup="true" CodeBehind="Frm_Ope_Consulta_Inventario.aspx.cs" Inherits="web_trazabilidad.Paginas.Trazabilidad.Frm_Ope_Consulta_Inventario" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../../Recursos/bootstrap-date/moment.min.js"></script>
    <link href="../../Recursos/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../../Recursos/estilos/demo_form.css" rel="stylesheet" />
    <link href="../../Recursos/bootstrap-combo/select2.css" rel="stylesheet" />

    <link href="../../Recursos/bootstrap-table-current/bootstrap-table.css" rel="stylesheet" />
    <link href="../../Recursos/bootstrap-date/bootstrap-datetimepicker.css" rel="stylesheet" />

    <script src="../../Recursos/bootstrap-combo/select2.js"></script>
    <script src="../../Recursos/bootstrap-combo/es.js"></script>
    <script src="../../Recursos/bootstrap-table-current/bootstrap-table.js"></script>
    <script src="../../Recursos/bootstrap-table-current/locale/bootstrap-table-es-MX.js"></script>
    <script src="../../Recursos/bootstrap-table-current/extensions/export/bootstrap-table-export.min.js"></script>
    <script src="../../Recursos/bootstrap-table-current/extensions/export/tableExport.js"></script>

    <script src="../../Recursos/plugins/parsley.js"></script>
    <script src="../../Recursos/plugins/jquery.formatCurrency-1.4.0.min.js"></script>
    <script src="../../Recursos/plugins/jquery.formatCurrency.all.js"></script>
    <script src="../../Recursos/plugins/accounting.min.js"></script>
    <script src="../../Recursos/plugins/pinch.js"></script>
    <script src="../../Recursos/javascript/seguridad/Js_Controlador_Sesion.js"></script>
    <script src="../../Recursos/bootstrap-date/moment.min.js"></script>
    <script src="../../Recursos/bootstrap-date/bootstrap-datetimepicker.min.js"></script>
    <script src="../../Recursos/plugins/jquery.qtip-1.0.0-rc3.min.js"></script>
    <link href="../../Recursos/bootstrap-pdf-viewer/view-pdf.css" rel="stylesheet" />
    <script src="../../Recursos/bootstrap-pdf-viewer/view-pdf.js"></script>
    <script src="../../Recursos/javascript/trazabilidad/Js_Imprimir_Datos_Etiqueta.js"></script>
    <script src="../../Recursos/javascript/trazabilidad/Js_Ope_Consulta_Inventario.js"></script>
    <style>
        .panel {
            padding-bottom: 10px !important;
            border: none!important;
            box-shadow:none !important;
        }

        #tbl_inventarios thead tr th{
            border: none !important;
        }
        .select2-container {
            width: 100% !important;
        }

        .fixed-table-toolbar .btn-group > .btn-group {
            display: inline-block;
            margin-left: 5px !important;
            margin-top: 5px !important;
        }

        .fixed-table-toolbar .btn-group > .btn-group:last-child > .btn {
            height: 30px !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        table{
            width:100%;
        }
        table td{
            white-space: nowrap; 
        }
        table td:last-child{
            width:100%;
        }
    </style>
    <div id="div_principal_orden_produccion">

        <div class="container-fluid" style="height: 100vh;">

            <div class="row">
                <div class="col-sm-12 text-left" style="background-color: white!important;">
                    <h3>Consulta Información Inventario</h3>
                </div>
            </div>

            <hr />
            <div class="panel panel-color panel-info" id="panel_1">
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
                            <label class="fuente_lbl_controles" for="txt_busqueda_por_no_orden">No. Parte</label>
                            <input id="txt_busqueda_por_no_Parte" type="text" class="form-control" />
                        </div>
                        <div class="col-md-2">
                            <label class="fuente_lbl_controles" for="txt_busqueda_por_no_orden_cliente">No. Parte Cliente</label>
                            <input id="txt_busqueda_por_no_Parte_cliente" type="text" class="form-control" />
                        </div>

                        <div class="col-md-2">
                            <label class="fuente_lbl_controles" for="txt_busqueda_serie">Serie</label>
                            <input id="txt_busqueda_serie" type="text" class="form-control" />
                        </div>
                        <div class="col-md-2">
                            <label class="fuente_lbl_controles" for="txt_busqueda_lote">Lote</label>
                            <input id="txt_busqueda_lote" type="text" class="form-control" />
                        </div>

                        <div class="col-md-2">
                            <label class="text-bold text-left text-medium fuente_lbl_controles" title="Fecha Inicio" style="margin-bottom: 5px !important;">Fecha Inicio</label>
                            <div class="input-group date" id="dtp_fecha_inicio">
                                <input type="text" id="fi" class="form-control" style="margin: 0px;" placeholder="dd/mm/aaaa" />
                                <span class="input-group-addon">
                                    <span class="glyphicon glyphicon-calendar"></span>
                                </span>
                            </div>
                        </div>
                        <div class="col-md-2">
                             <label class="text-bold text-left text-medium fuente_lbl_controles" title="Fecha Termino" style="margin-bottom: 5px !important;">Fecha Termino</label>
                             <div class="input-group date" id="dtp_fecha_termino">
                                <input type="text" id="ff" class="form-control" style="margin: 0px;" placeholder="dd/mm/aaaa" />
                                <span class="input-group-addon">
                                    <span class="glyphicon glyphicon-calendar"></span>
                                </span>
                            </div>
                        </div>

                    </div>

                    <div class="row" style="margin-top: 5px;">
                    </div>

                    <div class="row">
                        <div class="col-md-1 col-md-offset-10" align="right">
                            <button type="button" id="btn_busqueda" class="btn btn-secondary btn-icon btn-icon-standalone btn-lg">
                                <i class="fa fa-search"></i>
                                <span>Buscar</span>
                            </button>
                        </div>
                          <div class="col-md-1" align="right">
                            <button type="button" id="btn_reporte" class="btn btn-secondary btn-icon btn-icon-standalone btn-lg">
                                <i class="fa fa-print"></i>
                            </button>
                       </div>
                    </div>
             </div>
          </div>

            <%--<hr />--%>
             <div id="toolbar" style="margin-left: 5px; text-align: right; margin-top: 10px;">
            <div class="btn-group" role="group" style="margin-left: 5px;">
                <button id="btn_salir" type="button" class="btn btn-info btn-sm" title="Salir"><i class="glyphicon glyphicon-home"></i></button>
                <button id="btn_exportar" type="button" class="btn btn-info btn-sm" title="Exportar Excel"><i class="fa fa-download"></i></button>
            </div>
        </div>
            <table id="tbl_inventarios" data-toolbar="#toolbar" class="table table-hover table-striped table-bordered"></table>
        </div>

    </div>
</asp:Content>
