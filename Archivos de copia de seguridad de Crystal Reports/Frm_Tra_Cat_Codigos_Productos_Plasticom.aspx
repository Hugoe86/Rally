<%@ Page Title="" Language="C#" MasterPageFile="~/Paginas/Paginas_Generales/MasterPage.Master" AutoEventWireup="true" CodeBehind="Frm_Tra_Cat_Codigos_Productos_Plasticom.aspx.cs" Inherits="web_trazabilidad.Paginas.Trazabilidad.Frm_Tra_Cat_Codigos_Productos_Plasticom" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../Recursos/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../../Recursos/estilos/demo_form.css" rel="stylesheet" />
    <link href="../../Recursos/bootstrap-combo/select2.css" rel="stylesheet" />
    <link href="../../Recursos/bootstrap-table/bootstrap-table.css" rel="stylesheet" />
    <script src="../../Recursos/bootstrap-table-current/bootstrap-table.js"></script>
    <link href="../../Recursos/bootstrap-table-current/bootstrap-table.css" rel="stylesheet" />
    <link href="../../Recursos/estilos/center_loader.css" rel="stylesheet" />
    <link href="../../Recursos/estilos/css_transacciones.css" rel="stylesheet" />
    <script src="../../Recursos/bootstrap-combo/select2.js"></script>
    <script src="../../Recursos/bootstrap-combo/es.js"></script>
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
    <script src="../../Recursos/plugins/parsley.js"></script>
    <script src="../../Recursos/bootstrap-table/locale/bootstrap-table-es-MX.min.js"></script>
    <script src="../../Recursos/bootstrap-table-current/bootstrap-table.js"></script>
    <script src="../../Recursos/javascript/seguridad/Js_Controlador_Sesion.js"></script>
    <script src="../../Recursos/javascript/trazabilidad/Js_Tra_Codigos_Productos_Plasticom.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

        <div class="container-fluid" style="height: 100vh;">
            <div class="page-header">
                <div class="row">
                    <div class="col-sm-12 text-left" style="background-color: white!important;">
                        <h3>Tipo Parte</h3>
                    </div>
                </div>
            </div>

            <div class="panel panel-color panel-info" id="pnl_filtros">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        <i style="color: white;" class="glyphicon glyphicon-filter"></i>&nbsp;Filtros de b&uacute;squeda
                    </h3>
                    <div class="panel-options">
                        <a href="#" class="btn-popover-secondary"
                            data-toggle="popover"
                            data-trigger="hover"
                            data-placement="bottom"
                            data-original-title="Filtros"
                            data-content="Filtros para Consultar Productos">
                            <i class="fa fa-question-circle"></i>
                        </a>
                        <a id="ctrl_panel" href="#" data-toggle="collapse" data-parent="#pnl_filtros" data-target="#pnl_filtros_body">
                            <span class="glyphicon glyphicon-minus"></span>
                        </a>
                    </div>
                </div>
                <div id="pnl_filtros_body" class="panel-collapse collapse in">
                    <div class="panel-body">
                       
                        <div class="row">
                              <div class="col-md-6" style="margin-top: 6px;">
                                <label class="fuente_lbl_controles" for="cmb_productos" style="margin-bottom: 5px !important;">Producto</label>
                                <select id="cmb_producto" style="width: 100% !important"></select>
                            </div>

                            <div class="col-md-4">
                                <label class="fuente_lbl_controles" for="cmb_tipo">Tipo</label>
                                <select id="cmb_tipo" style="width: 100% !important"class="form-control">
                                    <option value=""><-TODOS-></option>
                                    <option value="TERMINADO">TERMINADO</option>
                                    <option value="INJECCION">INJECCION</option>
                                    <option value="PINTURA">PINTURA</option>
                                </select>
                            </div>
                          
                          <div class="col-md-2" style="margin-top: 26px !important; text-align: right !important;">
                                <button type="button" id="btn_busqueda"
                                    class="btn btn-secondary btn-icon btn-icon-standalone btn-lg" style="width: 110% !important;">
                                    <i class="fa fa-search"></i>
                                    <span>Buscar</span>
                                </button>
                            </div>
                        </div>

                    </div>
                </div>
            </div>

            <div id="toolbar" style="margin-left: 5px;">
                <div class="btn-group" role="group" style="margin-left: 5px;">
                    <button id="btn_inicio" type="button" class="btn btn-info btn-sm" title="Salir"><i class="glyphicon glyphicon-home"></i></button>
                    <button id="btn_nuevo" type="button" class="btn btn-info btn-sm" title="Nuevo"><i class="glyphicon glyphicon-plus"></i></button>
                </div>
            </div>
            <div class="table-responsive">
                <table id="tbl_codigos_productos" data-toolbar="#toolbar" class="table table-responsive"></table>
            </div>
        </div>

</asp:Content>
