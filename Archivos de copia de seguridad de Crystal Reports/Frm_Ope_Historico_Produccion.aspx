<%@ Page Title="" Language="C#" MasterPageFile="~/Paginas/Paginas_Generales/MasterPage.Master" AutoEventWireup="true" CodeBehind="Frm_Ope_Historico_Produccion.aspx.cs" Inherits="web_trazabilidad.Paginas.Trazabilidad.Frm_Ope_Historico_Produccion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
            <link href="../../Recursos/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../../Recursos/estilos/demo_form.css" rel="stylesheet" />
    <link href="../../Recursos/bootstrap-combo/select2.css" rel="stylesheet" />

    <%--<link href="../../Recursos/EasyUI 1.3.6/themes/default/easyui.css" rel="stylesheet" />--%>
    <link href="../../Recursos/EasyUI%201.3.6/themes/default/easyui.css" rel="stylesheet" />
    <link href="../../Recursos/EasyUI%201.3.6/themes/icon.css" rel="stylesheet" />

    <link href="../../Recursos/bootstrap-combo/select2-bootstrap.css" rel="stylesheet" />
    <link href="../../Recursos/bootstrap-table/bootstrap-table.css" rel="stylesheet" />
    <link href="../../Recursos/bootstrap-table/extensions/editable/bootstrap-editable.css" rel="stylesheet" />
    <link href="../../Recursos/bootstrap-date/bootstrap-datetimepicker.css" rel="stylesheet" />
    <link href="../../Recursos/estilos/css_orden_compra.css" rel="stylesheet" />
    <link href="../../Recursos/estilos/css_listado_ordenes_trabajo.css" rel="stylesheet" />

    <%--Javascript --%>
    <script src="../../Recursos/bootstrap-combo/select2.js"></script>
    <script src="../../Recursos/bootstrap-combo/es.js"></script>
    <script src="../../Recursos/bootstrap-table/bootstrap-table.min.js"></script>
    <script src="../../Recursos/bootstrap-table/locale/bootstrap-table-es-MX.min.js"></script>
    <script src="../../Recursos/bootstrap-table/extensions/editable/bootstrap-editable.js"></script>
    <script src="../../Recursos/bootstrap-table/extensions/editable/bootstrap-table-editable.min.js"></script>
    <script src="../../Recursos/bootstrap-date/moment.min.js"></script>
    <script src="../../Recursos/bootstrap-date/bootstrap-datetimepicker.min.js"></script>
    <script src="../../Recursos/plugins/jquery.formatCurrency-1.4.0.min.js"></script>
    <script src="../../Recursos/plugins/jquery.formatCurrency.all.js"></script>
    <script src="../../Recursos/plugins/accounting.min.js"></script>
    <script src="../../Recursos/plugins/parsley.js"></script>
    <script src="../../Recursos/EasyUI%201.3.6/jquery.easyui.min.js"></script>
    <script src="../../Recursos/EasyUI%201.3.6/locale/easyui-lang-es.js"></script>
    <script src="../../Recursos/javascript/seguridad/Js_Controlador_Sesion.js"></script>
    <script src="../../Recursos/javascript/trazabilidad/Js_Ope_Historico_Produccion.js"></script>
    <style>
        .datagrid-body td {
            font-family: Calibri !important;
        }

        .propertygrid .datagrid-view2 .datagrid-group {
            font-family: Calibri !important;
            font-size: 10px !important;
            color: #000 !important;
            background: linear-gradient(to bottom,#EFF5FF 0,#E0ECFF 100%);
            background-repeat: repeat-x;
            border: solid 0.1px #95B8E7;
            text-transform: uppercase;
        }

        .panel {
            overflow: initial;
        }
        .panel-body {
            overflow: initial;
            padding-top: 0px !important;
        }
        .panel-heading{
            margin-left: 0px !important;
            margin-right: 0px !important;
        }

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <div class="container-fluid" style="height: 100vh;">
        <div class="page-header">
            <div class="row">
                <div class="col-sm-12 text-left" style="background-color: white!important;">
                    <h3>Hist&oacute;rico de Producci&oacute;n</h3>
                </div>
            </div>
        </div>

        <div class="panel panel-color panel-info" id="panel_1" style="overflow: inherit;">
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
            <div class="panel-body" style="overflow: inherit;">

                <div id="pnl_filtros">
                    <div class="row" style="margin-top: 8px;"></div>
                     <div class="row">
                        <div class="col-md-6">
                             <label class="text-bold text-left text-medium fuente_lbl_controles">Centro de Trabajo</label>
                             <select id="cmb_ubicacion_busqueda" class="form-control"></select>
                        </div>
                        
                        <div class="col-md-6">
                            <label class="text-bold text-left text-medium fuente_lbl_controles">No. Parte</label>
                            <select id="cmb_producto" class="form-control"></select>
                        </div>
                    </div>
                    
                    <div class="row">
                       
                        <div class="col-md-3">
                            <label class="text-bold text-left text-medium fuente_lbl_controles">Fecha Inicio</label>
                            <div class="input-group date" id="dtp_fecha_inicio" style="z-index: 1000 !important;">
                                <input type="text" id="fi" class="form-control" style="margin: 0px;" placeholder="mm/dd/aaaa" />
                                <span class="input-group-addon">
                                    <span class="glyphicon glyphicon-calendar"></span>
                                </span>
                            </div>
                        </div>

                        <div class="col-md-3">
                            <label class="text-bold text-left text-medium fuente_lbl_controles">Fecha Fin</label>
                            <div class="input-group date" id="dtp_fecha_termino" style="z-index: 1000 !important;">
                                <input type="text" id="ff" class="form-control" style="margin: 0px;" placeholder="mm/dd/aaaa" />
                                <span class="input-group-addon">
                                    <span class="glyphicon glyphicon-calendar"></span>
                                </span>
                            </div>
                        </div>

                        <div class="col-md-4">
                           <label class="text-bold text-left text-medium fuente_lbl_controles"> Usuario</label>
                           <input type="text" id="txt_usuario"  class="form-control" placeholder="Usuario" style="margin: 0px;" />
                        </div> 

                        <div class="col-md-2"  style="margin-top: 15px;">
                            <button id="btn_busqueda" type="button" class="btn btn-info btn-sm" title="" style="width: 100%;">
                                <i class="glyphicon glyphicon-search"></i>&nbsp;&nbsp;Buscar
                            </button>
                         </div>
                    </div>


<%--                    <div class="row text-right" style="/*border-bottom: .5px solid #c3c3c6;*/">
                        <div class="col-md-2 col-md-offset-10">
                            <button id="btn_busqueda" type="button" class="btn btn-info btn-sm" title="" style="width: 100%;">
                                <i class="glyphicon glyphicon-search"></i>&nbsp;&nbsp;Buscar
                            </button>
                        </div>
                        <div class="col-md-2 col-md-offset-10" style="display: none !important;">
                            <button id="btn_Exportar" type="button" class="btn btn-info btn-sm" title="" style="width: 100%;">
                                <i class="fa fa-file"></i>&nbsp;&nbsp;Exportar
                            </button>
                        </div>
                    </div>--%>
                </div>
            </div>
        </div>


        <%--DIV DE LA BUSQUEDA --%>
        <div id="div_busqueda">
            <div class="row">
                <div class="col-md-12">
                    <table id="tbl_historico" class="easyui-propertygrid"></table>
                </div>
            </div>
        </div>

    </div>
</asp:Content>
