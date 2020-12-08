﻿<%@ Page Language="C#" Title="Solicitud de material" MasterPageFile="~/Paginas/Paginas_Generales/MasterPage.Master" AutoEventWireup="true" CodeBehind="Frm_Rpt_Solicitud_Material.aspx.cs" Inherits="web_trazabilidad.Paginas.Reporting.Frm_Rpt_Solicitud" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<link href="../../Recursos/bootstrap-table/bootstrap-table.min.css" rel="stylesheet" />
    <link href="../../Recursos/estilos/center_loader.css" rel="stylesheet" />
    <link href="../../Recursos/estilos/demo_form.css" rel="stylesheet" />
    <link href="../../Recursos/bootstrap-combo/select2.css" rel="stylesheet" />
    <link href="../../Recursos/bootstrap-date/bootstrap-datetimepicker.css" rel="stylesheet" />
    <link href="../../Recursos/estilos/css_reporte_existencias.css" rel="stylesheet" />

    <script src="../../Recursos/bootstrap-combo/select2.js"></script>
    <script src="../../Recursos/bootstrap-combo/es.js"></script>       

    <script src="../../Recursos/bootstrap-table-current/bootstrap-table.js"></script>
    <script src="../../Recursos/bootstrap-table-current/locale/bootstrap-table-es-MX.js"></script>
    <script src="../../Recursos/bootstrap-table-current/extensions/export/bootstrap-table-export.js"></script>
    <script src="../../Recursos/bootstrap-table-current/extensions/export/tableExport.js"></script>

    <script src="../../Recursos/bootstrap-table-current/bootstrap-table.js"></script>
    <script src="../../Recursos/bootstrap-table-current/locale/bootstrap-table-es-MX.js"></script>
    <script src="../../Recursos/bootstrap-table/extensions/editable/bootstrap-editable.js"></script>
    <script src="../../Recursos/bootstrap-table-current/extensions/editable/bootstrap-table-editable.js"></script>
    <script src="../../Recursos/bootstrap-date/moment.min.js"></script>
    <script src="../../Recursos/bootstrap-date/bootstrap-datetimepicker.min.js"></script>

    <script src="../../Recursos/plugins/center-loader.min.js"></script>
    <script src="../../Recursos/plugins/parsley.js"></script>
    <script src="../../Recursos/javascript/seguridad/Js_Controlador_Sesion.js"></script>
    <script src="../../Recursos/javascript/Reporting/Js_Rpt_Solicitud_Material.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid" style="height:100vh;">

        <div class="page-header" align="left">
            <h3 style="font-family: 'Roboto Light', cursive !important; font-size: 24px; font-weight: bold; color: #808080;">Reporte solicitud de material</h3>
        </div>

        <div class="panel panel-color panel-info" id="panel1">
            <div class="panel-heading" >
                <h3 class="panel-title">
                    <i style="color: white;" class="glyphicon glyphicon-filter"></i>&nbsp;Filtros de b&uacute;squeda
                </h3>
                <div class="panel-options">
                    <a href="#" data-toggle="panel">
                        <span class="collapse-icon">–</span>
                        <span class="expand-icon">+</span>
                    </a>
                </div>
            </div>
            <div class="panel-body">
               <div class="row">
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
                   <div class="col-md-2 col-md-offset-6" style="margin-top: 24px;">
                        <button type="button" id="btn_busqueda" class="btn btn-secondary btn-icon btn-icon-standalone btn-lg" style="width:100%;">
                            <i class="fa fa-search"></i>
                            <span>Buscar</span>
                        </button>
                    </div>
                  </div>

            </div>
        </div>

        <div id="toolbar" style="margin-left: 5px; margin-top: 10px;">
            <div class="btn-group" role="group" style="margin-left: 5px;">
                <button id="btn_salir" type="button" class="btn btn-info btn-sm" title="Salir"><i class="glyphicon glyphicon-home"></i></button>
                <button id="btn_exportar" type="button" class="btn btn-info btn-sm" title="Exportar Excel"><i class="fa fa-download"></i></button>
            </div>
        </div>
        <table id="tbl_existencias" data-toolbar="#toolbar" class="table table-responsive"></table>
    </div>
</asp:Content>

