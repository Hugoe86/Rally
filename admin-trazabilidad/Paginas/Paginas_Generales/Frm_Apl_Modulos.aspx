﻿<%@ Page Title="Catálogo de Módulos" Language="C#" AutoEventWireup="true" 
    CodeBehind="Frm_Apl_Modulos.aspx.cs" 
    MasterPageFile="~/Paginas/Paginas_Generales/MasterPage.Master"
    Inherits="web_trazabilidad.Paginas.Paginas_Generales.Frm_Apl_Modulos" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../Recursos/bootstrap-table/bootstrap-table.min.css" rel="stylesheet" />
    <link href="../../Recursos/estilos/center_loader.css" rel="stylesheet" />
    <link href="../../Recursos/icon-picker/css/icon-picker.css" rel="stylesheet" />
    <link href="../../Recursos/estilos/demo_form.css" rel="stylesheet" />

    <script src="../../Recursos/bootstrap-table-current/bootstrap-table.js"></script>
    <script src="../../Recursos/bootstrap-table-current/locale/bootstrap-table-es-MX.js"></script>
    <script src="../../Recursos/bootstrap-table-current/extensions/export/bootstrap-table-export.js"></script>
    <script src="../../Recursos/bootstrap-table-current/extensions/export/tableExport.js"></script>
    <style>
        .fixed-table-toolbar .bars, .fixed-table-toolbar .search, .fixed-table-toolbar .columns {
            margin-top: 0px !important;
        }

        .form-control-icono {
            /*Color de fondo de los controles*/
            background-color: inactiveborder;
            height: 25px;
            font-size: 90%;
            margin-top: 0px;
            margin-bottom: 0px;
        }
    </style>
    <script src="../../Recursos/plugins/center-loader.min.js"></script>
    <script src="../../Recursos/plugins/parsley.js"></script>
    <script src="../../Recursos/bootstrap-table/bootstrap-table.min.js"></script>
    <script src="../../Recursos/bootstrap-table/locale/bootstrap-table-es-MX.min.js"></script>
    <script src="../../Recursos/javascript/seguridad/Js_Controlador_Sesion.js"></script>
    <script src="../../Recursos/javascript/seguridad/Js_Apl_Modulos.js"></script>
    <script src="../../Recursos/icon-picker/js/iconPicker.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid" style="height:100vh;">

        <div class="page-header" align="left">
            <h3 style="font-family: 'Roboto Light', cursive !important; font-size: 24px; font-weight: bold; color: #808080;">Cat&aacute;logo de Módulos</h3>
        </div>

        <div class="panel panel-color panel-info collapsed" id="panel1">
            <div class="panel-heading">
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
                    <div class="col-md-1" style="margin-top: 8px;">
                        <label>Nombre</label>
                    </div>
                    <div class="col-md-3">
                        <input type="text" id="txt_busqueda_por_nombre" class="form-control" placeholder="Búsqueda por nombre" />
                    </div>
                     <div class="col-md-8" align="right">
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
                <button id="btn_nuevo" type="button" class="btn btn-info btn-sm" title="Nuevo"><i class="glyphicon glyphicon-plus"></i></button>
                <button id="btn_exportar" type="button" class="btn btn-info btn-sm" title="Exportar Excel"><i class="fa fa-download"></i></button>
            </div>
        </div>
        <table id="tbl_modulos" data-toolbar="#toolbar" class="table table-responsive"></table>
    </div>
</asp:Content>
