﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Paginas/Paginas_Generales/MasterPage.Master" AutoEventWireup="true" CodeBehind="Frm_Apl_Sucursales.aspx.cs" Inherits="admin_trazabilidad.Paginas.Catalogos.Frm_Apl_Sucursales" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <link href='https://fonts.googleapis.com/css?family=Roboto' rel='stylesheet' type='text/css'>
    <link href='https://fonts.googleapis.com/css?family=Indie+Flower' rel='stylesheet' type='text/css'>
    <link href="../../Recursos/bootstrap-table/bootstrap-table.min.css" rel="stylesheet" />
    <link href="../../Recursos/estilos/center_loader.css" rel="stylesheet" />
    <link href="../../Recursos/estilos/css_master.css" rel="stylesheet" />
    <link href="../../Recursos/bootstrap-combo/select2.css" rel="stylesheet" />
    <link href="../../Recursos/estilos/demo_form.css" rel="stylesheet" />

    <script src="../../Recursos/bootstrap-table-current/bootstrap-table.js"></script>
    <script src="../../Recursos/bootstrap-table-current/locale/bootstrap-table-es-MX.js"></script>
    <script src="../../Recursos/bootstrap-table-current/extensions/export/bootstrap-table-export.js"></script>
    <script src="../../Recursos/bootstrap-table-current/extensions/export/tableExport.js"></script>

    <script src="../../Recursos/plugins/center-loader.min.js"></script>
    <script src="../../Recursos/plugins/parsley.js"></script>
    <script src="../../Recursos/bootstrap-combo/select2.js"></script>
    <script src="../../Recursos/bootstrap-table/bootstrap-table.min.js"></script>
    <script src="../../Recursos/bootstrap-table/locale/bootstrap-table-es-MX.min.js"></script>
    <script src="../../Recursos/javascript/seguridad/Js_Controlador_Sesion.js"></script>

    <script src="../../Recursos/javascript/catalogos/Js_Apl_Roles_Sucursales.js"></script>
    <script src="../../Recursos/javascript/catalogos/Js_Apl_Sucursales.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid" style="height: 100vh;">

        <div class="page-header" align="left">
            <h3 style="font-family: 'Roboto', cursive; font-size: 24px; font-weight: bold; color: #808080;">Cat&aacute;logo de Sucursales</h3>
        </div>

        <div class="panel panel-color panel-info" id="panel1">
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
                <div class="row" style="display: none;">
                    <div class="col-md-4 col-md-offset-0">
                        <input type="text" id="txt_busqueda_por_clave" class="form-control" placeholder="Busqueda por clave" />
                    </div>
                </div>

                <div class="row" style="display: none;">
                    <div class="col-md-4 col-md-offset-0">
                        <input type="text" id="txt_busqueda_por_nombre" class="form-control" placeholder="Busqueda por nombre" />
                    </div>
                </div>

                <div class="row" style="display: none;">
                    <div class="col-md-4 col-md-offset-0">
                        <input type="text" id="txt_busqueda_por_comentarios" class="form-control" placeholder="Busqueda por comentarios" />
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-3">
                        <div class="form-inline" align="left">
                            <label>Estatus</label>
                            <select id="cmb_busqueda_por_estatus" class="form-control">
<%--                                <option value="">-- SELECCIONE --</option>
                                <option value="Activo" selected="selected">Activo</option>
                                <option value="Inactivo">Inactivo</option>--%>
                            </select>
                        </div>
                    </div>
                    <div class="col-md-9" align="right">
                        <button type="button" id="btn_busqueda" class="btn btn-secondary btn-icon btn-icon-standalone btn-lg">
                            <i class="fa fa-search"></i>
                            <span>Buscar</span>
                        </button>
                    </div>
                </div>
            </div>
            <%--<div class="panel-footer">
                <div class="row">
                    <div class="col-md-2 col-md-offset-10" align="right">

                    </div>
                </div>
            </div>--%>
        </div>

        <div id="toolbar" style="margin-left: 5px;">
            <div class="btn-group" role="group" style="margin-left: 5px;">
                <button id="btn_salir" type="button" class="btn btn-info btn-sm" title="Salir"><i class="glyphicon glyphicon-home"></i></button>
                <button id="btn_exportar" type="button" class="btn btn-info btn-sm" title="Exportar Excel"><i class="fa fa-download"></i></button>
                <button id="btn_nuevo" type="button" class="btn btn-info btn-sm" title="Nuevo"><i class="glyphicon glyphicon-plus"></i></button>
            </div>
        </div>
        <table id="tbl_sucursales" data-toolbar="#toolbar" class="table table-responsive"></table>
    </div>
</asp:Content>
