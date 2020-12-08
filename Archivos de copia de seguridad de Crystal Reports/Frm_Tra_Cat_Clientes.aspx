<%@ Page Title="Catálogo de Clientes" Language="C#" MasterPageFile="~/Paginas/Paginas_Generales/MasterPage.Master" AutoEventWireup="true" CodeBehind="Frm_Tra_Cat_Clientes.aspx.cs" Inherits="web_trazabilidad.Paginas.Catalogos.Frm_Tra_Cat_Clientes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <link href="../../Recursos/bootstrap-table/bootstrap-table.min.css" rel="stylesheet" />
    <link href="../../Recursos/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../../Recursos/estilos/center_loader.css" rel="stylesheet" />
    <link href="../../Recursos/estilos/demo_form.css" rel="stylesheet" />
    <link href="../../Recursos/estilos/css_producto.css" rel="stylesheet" />
    <script src="../../Recursos/plugins/center-loader.min.js"></script>
    <script src="../../Recursos/plugins/parsley.js"></script>
    <script src="../../Recursos/bootstrap-table/bootstrap-table.min.js"></script>
    <script src="../../Recursos/bootstrap-table/locale/bootstrap-table-es-MX.min.js"></script>
    <script src="../../Recursos/javascript/seguridad/Js_Controlador_Sesion.js"></script>
    <script src="../../Recursos/javascript/catalogos/Js_Tra_Cat_Clientes.js"></script>

    <script src="../../Recursos/bootstrap-table-current/bootstrap-table.js"></script>
    <script src="../../Recursos/bootstrap-table-current/locale/bootstrap-table-es-MX.js"></script>
    <script src="../../Recursos/bootstrap-table-current/extensions/export/bootstrap-table-export.js"></script>
    <script src="../../Recursos/bootstrap-table-current/extensions/export/tableExport.js"></script>

    <!-- Angular - Clientes -->
    <script src="../../Recursos/angular/app/services/ClienteService.js"></script>
    <script src="../../Recursos/angular/app/controllers/ClienteController.js"></script>

     <style>
        /*.pull-right {
            margin-top: 10px !important;
        }*/
        .search input:first-of-type {
            min-width: 200px !important;
        }
        /*.nav.nav-tabs > li.active > a {
            background-color: #E0ECFF;
            border-color: #95B8E7;
        }*/
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="container-fluid" style="height:100vh;">

        <div class="page-header">
            <h3 style="font-family: 'Roboto Light', cursive !important; font-size: 24px; font-weight: bold; color: #808080;">Cat&aacute;logo de Clientes</h3>
        </div>

        <div class="panel panel-color panel-info" id="pnl_filtros">
            <div class="panel-heading" id="head">
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
                            <div class="col-md-3">
                                <label class="fuente_lbl_controles" for="txt_busqueda_por_clave">Clave</label>
                                <input id="txt_busqueda_por_clave" type="text" class="form-control" placeholder="Búsqueda por clave"/>
                            </div>
                            <div class="col-md-4">
                                <label class="fuente_lbl_controles" for="txt_busqueda_por_nombre">Nombre</label>
                                <input id="txt_busqueda_por_nombre" type="text" class="form-control" placeholder="Búsqueda por nombre"/>
                            </div>
                            <div class="col-md-3">
                                <label class="fuente_lbl_controles" for="cmb_estatus_filtro">Estatus</label>
                                <select id="cmb_estatus_filtro" class="form-control" ></select>
                            </div>
                            <div class="col-md-2" style="margin-top: 24px !important; text-align: right !important;">
                                <button type="button" id="btn_busqueda"
                                    class="btn btn-secondary btn-icon btn-icon-standalone btn-lg" style="width: 100% !important;">
                                    <i class="fa fa-search"></i>
                                    <span>Buscar cliente</span>
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
        <table id="tbl_clientes" data-toolbar="#toolbar" class="table table-responsive"></table>
    </div>
</asp:Content>
