﻿<%@ Page Title="BOM de Materiales" Language="C#" MasterPageFile="~/Paginas/Paginas_Generales/MasterPage.Master" AutoEventWireup="true" CodeBehind="Frm_Com_Ope_Lista_Materiales_BOM.aspx.cs"
    Inherits="web_trazabilidad.Paginas.Trazabilidad.Frm_Com_Ope_Lista_Materiales_BOM" %>

<%@ Import Namespace="web_trazabilidad.Models.Ayudante" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../Recursos/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../../Recursos/estilos/demo_form.css" rel="stylesheet" />
    <link href="../../Recursos/bootstrap-combo/select2.css" rel="stylesheet" />
    <link href="../../Recursos/bootstrap-table-current/bootstrap-table.css" rel="stylesheet" />
    <link href="../../Recursos/estilos/center_loader.css" rel="stylesheet" />
    <link href="../../Recursos/bootstrap-table/extensions/editable/bootstrap-editable.css" rel="stylesheet" />
    <link href="../../Recursos/estilos/css_lista_materiales_BOM.css" rel="stylesheet" />

    <script src="../../Recursos/plugins/parsley.js"></script>
    <script src="../../Recursos/plugins/jquery.formatCurrency-1.4.0.min.js"></script>
    <script src="../../Recursos/plugins/jquery.formatCurrency.all.js"></script>
    <script src="../../Recursos/plugins/accounting.min.js"></script>
    <script src="../../Recursos/plugins/jquery.qtip-1.0.0-rc3.min.js"></script>
    <script src="../../Recursos/bootstrap-combo/select2.js"></script>
    <script src="../../Recursos/bootstrap-combo/es.js"></script>
    <script src="../../Recursos/bootstrap-table-current/bootstrap-table.js"></script>
    <script src="../../Recursos/bootstrap-table-current/locale/bootstrap-table-es-MX.js"></script>
    <script src="../../Recursos/bootstrap-table/extensions/editable/bootstrap-editable.js"></script>
    <script src="../../Recursos/bootstrap-table-current/extensions/editable/bootstrap-table-editable.min.js"></script>
    <script src="../../Recursos/bootstrap-table-current/extensions/export/bootstrap-table-export.js"></script>
    <script src="../../Recursos/bootstrap-table-current/extensions/export/tableExport.js"></script>
    <script src="../../Recursos/bootstrap-box/bootstrap-number-input.js"></script>
    <script src="../../Recursos/javascript/seguridad/Js_Controlador_Sesion.js"></script>
    <script src="../../Recursos/bootstrap-box/bootstrap-number-input.js"></script>
    <script src="../../Recursos/javascript/trazabilidad/Js_Ope_Lista_Materiales_BOM.js"></script>

    <!-- Angular - Productos -->
    <script src="../../Recursos/angular/app/services/ProductoService.js"></script>
    <script src="../../Recursos/angular/app/controllers/ProductoController.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container-fluid" style="height: 100vh;">
        <div class="page-header">
            <div class="row">
                <div class="col-sm-12 text-left" style="background-color: white!important;">
                    <h3>
                        <label id="lbl_titulo_">BOM de Materiales</label>
                    </h3>
                </div>
            </div>
        </div>

        <div id="div_BOM_mostrar">

            <div id="pnl_filtros" class="panel panel-color panel-info">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        <i style="color: white;" class="glyphicon glyphicon-filter"></i>&nbsp;Filtros de b&uacute;squeda
                    </h3>
                    <div class="panel-options">
                        <a id="ctrl_panel" href="#" data-toggle="panel">
                            <span class="collapse-icon">-</span>
                            <span class="expand-icon">+</span>
                        </a>
                    </div>
                </div>

                <div class="panel-body">
                    <div class="row">
                        <div class="col-md-2" style="display: block; margin-top: 8px;">
                            <label for="txt_busqueda_por_producto" style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap;">No Parte/Nombre</label>
                            <input type="hidden" value="<%=Cls_Sesiones.Habilitar_Contenerdor_No_Piezas %>" id="txt_habilitar_contenerdor_no_piezas" class="form-control" />
                        </div>
                        <div class="col-md-4">
                            <input type="text" id="txt_busqueda_por_producto" class="form-control" placeholder="Ingrese b&uacute;squeda" />
                        </div>

                        <div class="col-md-6" align="right">
                            <button type="button" id="btn_busqueda" class="btn btn-secondary btn-icon btn-icon-standalone btn-lg">
                                <i class="fa fa-search"></i>
                                <span>Buscar</span>
                            </button>
                        </div>
                    </div>
                </div>
            </div>

            <div id="toolbar" style="margin-left: 5px; margin-top: 9px;">
                <div class="btn-group" role="group" style="margin-left: 5px;">
                    <button id="btn_salir" type="button" class="btn btn-info btn-sm" title=""><i class="glyphicon glyphicon-home"></i></button>
                    <button id="btn_exportar" type="button" class="btn btn-info btn-sm" title=""><i class="glyphicon glyphicon-print"></i></button>
                    <button id="btn_exportar_excel" type="button" class="btn btn-info btn-sm" title="Exportar Excel"><i class="fa fa-download"></i></button>
                    <button id="btn_nuevo" type="button" class="btn btn-info btn-sm" title=""><i class="glyphicon glyphicon-plus"></i></button>
                </div>
            </div>

            <div class="row space"></div>

            <div class="table-responsive">
                <table id="tbl_BOM" data-toolbar="#toolbar" class="table table-responsive"></table>
            </div>
        </div>

        <div id="div_BOM_alta" style="display: none">
            <div class="tab-pane" id="lista_productos">
                <div class="row">
                    <div class="col-md-1 col-md-offset-5 pull-right">
                        <button id="btn_regresar_tabla_general" type="button" class="btn btn-info btn-sm" title="Regresar"><i class="glyphicon glyphicon-circle-arrow-left"></i>&nbsp;&nbsp;Regresar</button>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3" style="padding-top: 8px !important;">
                        <div class="form-inline">
                            <label for="cmb_productos" class="fuente_lbl_controles">Productos</label>
                            <span id="btnCrearProductoMateriales" class="fa fa-plus-circle" title="Crear producto" style="background: #F5F6CE; color: #2d2d30; cursor: pointer;"></span>
                        </div>
                        <select id="cmb_lista_materiales" class="form-control small"></select>
                    </div>

                    <div class="col-md-2">
                        <label for="txt_cantidad" class="fuente_lbl_controles">Cantidad Receta</label>
                        <input id="txt_cantidad_Productos" type="text" class="form-control" title="Ingresar la cantidad" pattern="[0-9]" />
                        <input id="txt_no_explosion_material_auxiliar" type="text" class="form-control" style="display: none;" />
                    </div>
                    <div class="col-md-1">
                        <label class="fuente_lbl_controles">Unidad</label>
                        <span id="span_empaque" class="form-control"></span>
                    </div>
                    <div class="col-md-1">
                        <label class="fuente_lbl_controles">Rendimiento</label>
                        <span id="span_rendimiento" class="form-control"></span>
                    </div>
                    <div class="col-md-2">
                        <label for="txt_cantidad" class="fuente_lbl_controles">Cantidad Real</label>
                        <span id="txt_cantidad_real" class="form-control"></span>
                    </div>
                    <div class="col-md-2">

                        <br />
                        <input type="checkbox" id="chk_mostrar_en_ventas" /><label for="txt_cantidad" class="fuente_lbl_controles">Mostrar en venta</label>
                    </div>
                    <div class="col-md-1" style="margin-top: 18px;">
                        <label class="fuente_lbl_controles">&nbsp;</label>
                        <button id="btn_nuevo_lista" type="button" class="btn btn-info btn-sm" title="Agregar Lista de Materiales" style="padding-top: 8px;">
                            <i class="fa fa-plus"></i>&nbsp;&nbsp;Agregar
                        </button>
                    </div>

                </div>

                <hr />
                <table id="tbl_lista_productos" class="table table-responsive"></table>
            </div>
        </div>

        <div id="div_BOM_alta_extras" style="display: none">
            <div class="tab-pane" id="lista_productos_extras">
                <div class="row">
                    <div class="col-md-1 col-md-offset-5 pull-right">
                        <button id="btn_regresar_tabla_general_extra" type="button" class="btn btn-info btn-sm" title="Regresar"><i class="glyphicon glyphicon-circle-arrow-left"></i>&nbsp;&nbsp;Regresar</button>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <div class="form-inline">
                            <label for="cmb_productos" class="fuente_lbl_controles">Productos</label>
                        </div>
                        <input id="txt_no_explosion_material_auxiliar_extra" type="text" class="form-control" style="display: none;" />
                        <select id="cmb_lista_materiales_extras" class="form-control small"></select>
                    </div>
                    <div class="col-md-2">
                        <label class="fuente_lbl_controles">*Cantidad Receta</label>
                        <input id="txt_cantidad_receta_pe" type="text" class="form-control" />
                    </div>
                    <div class="col-md-1">
                        <label class="fuente_lbl_controles">Unidad</label>
                        <span id="txt_empaque_pe" class="form-control" />
                    </div>
                    <div class="col-md-1">
                        <label class="fuente_lbl_controles">Rendimiento</label>
                        <span id="txt_rendimiento_pe" class="form-control" />
                    </div>
                    <div class="col-md-2">
                        <label class="fuente_lbl_controles">Cantidad</label>
                        <span id="txt_cantidad_pe" class="form-control" />
                    </div>
                    <div class="col-md-2">
                        <label for="txt_costo_extra" class="fuente_lbl_controles">Costo</label>
                        <input id="txt_costo_extra" type="text" class="form-control" />
                    </div>
                    <div class="col-md-1">
                        <label class="fuente_lbl_controles">&nbsp;</label>
                        <button id="btn_nuevo_lista_extra" type="button" class="btn btn-info btn-sm" title="Agregar Lista de Materiales" style="margin-left: 8px;">
                            <i class="fa fa-plus"></i>&nbsp;&nbsp;Agregar
                        </button>
                    </div>

                </div>

                <hr />
                <table id="tbl_lista_productos_extras" class="table table-responsive"></table>
            </div>
        </div>

        <div id="div_caracteristicas" style="display:none;">
            <div class="tab-pane" id="list_caracteristicas">
                <div class="row">
                    <div class="col-md-1 col-md-offset-5 pull-right">
                        <button id="btn_regresar_caracteristicas" type="button" class="btn btn-info btn-sm" title="Regresar"><i class="glyphicon glyphicon-circle-arrow-left"></i>&nbsp;&nbsp;Regresar</button>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-11">
                        <div class="form-inline">
                            <label for="txt_descripcion_caracteristicas" class="fuente_lbl_controles">Descripci&oacute;n</label>
                        </div>
                        <input id="txt_no_expmat_aux_caracteristica" type="text" class="form-control" style="display: none;" />
                        <input id="txt_descripcion_caracteristicas" type="text" class="form-control" />
                    </div>
                    <div class="col-md-1">
                        <label class="fuente_lbl_controles">&nbsp;</label>
                        <button id="btn_add_caracteristica" type="button" class="btn btn-info btn-sm" title="Agregar Caracteristicas" style="margin-left: 8px;">
                            <i class="fa fa-plus"></i>&nbsp;&nbsp;Agregar
                        </button>
                    </div>
                </div>
                <hr />
                <table id="tbl_caracteristicas" class="table table-responsive"></table>
            </div>
        </div>
    </div>
</asp:Content>
