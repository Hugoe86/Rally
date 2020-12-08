<%@ Page Title="" Language="C#" MasterPageFile="~/Paginas/Paginas_Generales/MasterPage.Master" AutoEventWireup="true" CodeBehind="Frm_Rpt_Kardex_Inventario.aspx.cs" Inherits="web_trazabilidad.Paginas.Trazabilidad.Frm_Rpt_Kardex_Inventario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <%--Hojas de estilo --%>
    <link href="../../Recursos/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../../Recursos/estilos/demo_form.css" rel="stylesheet" />
    <link href="../../Recursos/EasyUI 1.3.6/themes/default/easyui.css" rel="stylesheet" />
    <link href="../../Recursos/EasyUI%201.3.6/themes/icon.css" rel="stylesheet" />
    <link href="../../Recursos/bootstrap-combo/select2.css" rel="stylesheet" />
    <link href="../../Recursos/bootstrap-combo/select2-bootstrap.css" rel="stylesheet" />
    <link href="../../Recursos/bootstrap-table/bootstrap-table.css" rel="stylesheet" />
    <link href="../../Recursos/bootstrap-date/bootstrap-datetimepicker.css" rel="stylesheet" />
    <link href="../../Recursos/estilos/css_kardex.css" rel="stylesheet" />

    <%--JavaScript --%>
    <script src="../../Recursos/bootstrap-combo/select2.js"></script>
    <script src="../../Recursos/bootstrap-combo/es.js"></script>
    <script src="../../Recursos/bootstrap-table/bootstrap-table.min.js"></script>
    <script src="../../Recursos/bootstrap-table/locale/bootstrap-table-es-MX.min.js"></script>
    <script src="../../Recursos/bootstrap-date/moment.min.js"></script>
    <script src="../../Recursos/bootstrap-date/bootstrap-datetimepicker.min.js"></script>
    <script src="../../Recursos/EasyUI%201.3.6/jquery.easyui.min.js"></script>
    <script src="../../Recursos/EasyUI%201.3.6/locale/easyui-lang-es.js"></script>
    <script src="../../Recursos/javascript/seguridad/Js_Controlador_Sesion.js"></script>
    <script src="../../Recursos/javascript/trazabilidad/Js_Rpt_Kardex_Inventario.js"></script>
    <style>
        .panel {
            overflow: initial;
        }

        .panel-body {
            overflow: initial;
            padding-top: 0px !important;
        }

        .panel-heading {
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
                    <h3>Kardex de Inventario</h3>
                </div>
            </div>
        </div>



        <hr />
        <div class="panel panel-color panel-info" id="pnl_filtros" style="overflow: inherit;">
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
            <div class="panel-body" style="overflow: inherit; padding: 0px 10px !important;">
                <div class="row" style="margin-top: 8px; padding-right:4px; padding-left:4px"></div>

                <div class="row">
                    <div class="col-md-4">
                        <label class="text-bold text-left text-medium fuente_lbl_controles">Producto</label>

                        <select id="cmb_productos" class="form-control"></select>
                    </div>
                    <div class="col-md-4">
                        <label class="text-bold text-left text-medium fuente_lbl_controles">Inventario</label>

                        <select id="cmb_inventario" class="form-control"></select>
                    </div>
                    <div class="col-md-2">
                        <label class="text-bold text-left text-medium fuente_lbl_controles">Fecha Inicio</label>
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
                        <label class="text-bold text-left text-medium fuente_lbl_controles">Fecha Fin</label>
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

                <div class="row" style="margin-top: 7px;"></div>

                <div class="row">

                    <div class="col-md-12" style="text-align:right">
                        <button type="button" id="btn_busqueda"
                            class="btn btn-secondary btn-icon btn-icon-standalone btn-lg" >
                            <i class="fa fa-search"></i>
                            <span>Buscar</span>
                        </button>
                    </div>

                </div>

                <div class="row">

                    <div class="col-md-10">
                    </div>
                    <div class="col-md-2" style="margin-top: 5px !important; text-align: right !important;">
                    </div>
                </div>

            </div>
            <div class="row">
                <div class="col-md-12">
                    <table id="grid_kardex" ></table>
                </div>
            </div>
        </div>
</div>
</asp:Content>
