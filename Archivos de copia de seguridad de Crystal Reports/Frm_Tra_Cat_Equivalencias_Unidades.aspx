<%@ Page Title="" Language="C#" MasterPageFile="~/Paginas/Paginas_Generales/MasterPage.Master" AutoEventWireup="true" CodeBehind="Frm_Tra_Cat_Equivalencias_Unidades.aspx.cs" Inherits="web_trazabilidad.Paginas.Catalogos.Frm_Tra_Cat_Equivalencias_Unidades" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../Recursos/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../../Recursos/bootstrap-table-current/bootstrap-table.css" rel="stylesheet" />
    <link href="../../Recursos/estilos/demo_form.css" rel="stylesheet" />
    <link href="../../Recursos/bootstrap-combo/select2.css" rel="stylesheet" />
    <link href="../../Recursos/bootstrap-table/extensions/editable/bootstrap-editable.css" rel="stylesheet" />
    <link href="../../Recursos/estilos/css_producto.css" rel="stylesheet" />

    <script src="../../Recursos/plugins/parsley.js"></script>
    <script src="../../Recursos/plugins/jquery.formatCurrency-1.4.0.min.js"></script>
    <script src="../../Recursos/plugins/jquery.formatCurrency.all.js"></script>
    <script src="../../Recursos/plugins/accounting.min.js"></script>

    <script src="../../Recursos/bootstrap-table-current/bootstrap-table.js"></script>
    <script src="../../Recursos/bootstrap-table-current/locale/bootstrap-table-es-MX.js"></script>
    <script src="../../Recursos/bootstrap-table-current/extensions/export/bootstrap-table-export.js"></script>
    <script src="../../Recursos/bootstrap-table-current/extensions/export/tableExport.js"></script>

    <script src="../../Recursos/bootstrap-table/bootstrap-table.min.js"></script>
    <script src="../../Recursos/bootstrap-table/locale/bootstrap-table-es-MX.min.js"></script>
    <script src="../../Recursos/bootstrap-table/extensions/editable/bootstrap-editable.js"></script>
    <script src="../../Recursos/bootstrap-table/extensions/editable/bootstrap-table-editable.min.js"></script>

    <script src="../../Recursos/javascript/seguridad/Js_Controlador_Sesion.js"></script>
    <script src="../../Recursos/plugins/jquery.qtip-1.0.0-rc3.min.js"></script>
    <script src="../../Recursos/bootstrap-combo/select2.js"></script>
    <script src="../../Recursos/bootstrap-combo/es.js"></script>

    <script src="../../Recursos/javascript/catalogos/Js_Tra_Cat_Equivalencias_Unidades.js"></script>

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
        .btn:hover, .btn:focus, .btn.focus{
            color:white !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="container-fluid" style="height:100vh;">
            <div class="page-header">
                <div class="row">
                    <div class="col-sm-12 text-left" style="background-color: white!important;">
                        <h3>Cat&aacute;logos de Equivalencias Unidades</h3>
                    </div>
                </div>
            </div>
           <%-- <div class="panel panel-color panel-info collapsed" id="panel1">
                <div class="panel-heading filter">
                    <h3 class="panel-title">
                        <i style="color: white;" class="glyphicon glyphicon-filter"></i>&nbsp;Filtros de b&uacute;squeda
                    </h3>
                    <div class="panel-options">
                        <a href="#" data-toggle="panel">
                            <span id="ctrl_panel_collapse" class="collapse-icon">-</span>
                            <span id="ctrl_panel_expand" class="expand-icon">+</span>
                        </a>
                    </div>
                </div>
                <div class="panel-body">
                        <div class="row">
                            <div class="col-md-1">
                                <label style="margin-top: 10px;">UM_De</label>
                            </div>
                            <div class="col-md-3">
                                <div style="margin-left:5px; margin-top:5px;"><select id="cmb_UM_De_Busqueda" class="form-control"></select></div>
                            </div>
                            <div class="col-md-1">
                                <label style="margin-top: 10px;">UM_A</label>
                            </div>
                            <div class="col-md-3">
                                <div style="margin-left:5px; margin-top:5px;"><select id="cmb_UM_A_Busqueda" class="form-control"></select></div>
                            </div>
                            <div class="col-md-4" style="text-align:right; margin-top: 8px !important;">
                                <button type="button" id="btn_busqueda" class="btn btn-secondary btn-icon btn-icon-standalone btn-lg">
                                    <i class="fa fa-search"></i>
                                    <span>Buscar</span>
                                </button>
                            </div>
                       </div>
                </div>
            </div>--%>
            <div id="toolbar" style="margin-left: 5px;  display:block;">
                <div class="btn-group" role="group" style="margin-left: 5px;">
                    <button id="btn_salir" type="button" class="btn btn-info btn-sm" title="Salir"><i class="glyphicon glyphicon-home"></i></button>
                    <button id="btn_nuevo" type="button" class="btn btn-info btn-sm" title="Nuevo"><i class="glyphicon glyphicon-plus"></i></button>
                    <button id="btn_exportar" type="button" class="btn btn-info btn-sm" title="Exportar Excel"><i class="fa fa-download"></i></button>
                </div>
            </div>
            <table id="tbl_equivalencia_unidades" data-toolbar="#toolbar" class="table table-responsive"></table>

     </div>
</asp:Content>
