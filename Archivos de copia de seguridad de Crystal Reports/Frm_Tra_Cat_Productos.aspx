<%@ Page Title="" Language="C#" MasterPageFile="~/Paginas/Paginas_Generales/MasterPage.Master" AutoEventWireup="true" CodeBehind="Frm_Tra_Cat_Productos.aspx.cs" Inherits="web_trazabilidad.Paginas.Catalogos.Frm_Tra_Cat_Productos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../Recursos/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../../Recursos/bootstrap-table-current/bootstrap-table.css" rel="stylesheet" />
    <link href="../../Recursos/estilos/demo_form.css" rel="stylesheet" />
    <link href="../../Recursos/bootstrap-combo/select2.css" rel="stylesheet" />
    <link href="../../Recursos/bootstrap-table/extensions/editable/bootstrap-editable.css" rel="stylesheet" />
    <link href="../../Recursos/estilos/css_producto.css" rel="stylesheet" />

    <script src="../../Recursos/bootstrap-table-current/bootstrap-table.js"></script>
    <script src="../../Recursos/bootstrap-table-current/locale/bootstrap-table-es-MX.js"></script>
    <script src="../../Recursos/bootstrap-table-current/extensions/export/bootstrap-table-export.js"></script>
    <script src="../../Recursos/bootstrap-table-current/extensions/export/tableExport.js"></script>

    <script src="../../Recursos/plugins/parsley.js"></script>
    <script src="../../Recursos/plugins/jquery.formatCurrency-1.4.0.min.js"></script>
    <script src="../../Recursos/plugins/jquery.formatCurrency.all.js"></script>
    <script src="../../Recursos/plugins/accounting.min.js"></script>

    <script src="../../Recursos/bootstrap-table/bootstrap-table.min.js"></script>
    <script src="../../Recursos/bootstrap-table/locale/bootstrap-table-es-MX.min.js"></script>
    <script src="../../Recursos/bootstrap-table/extensions/editable/bootstrap-editable.js"></script>
    <script src="../../Recursos/bootstrap-table/extensions/editable/bootstrap-table-editable.min.js"></script>

    <script src="../../Recursos/javascript/seguridad/Js_Controlador_Sesion.js"></script>
    <script src="../../Recursos/plugins/jquery.qtip-1.0.0-rc3.min.js"></script>
    <script src="../../Recursos/bootstrap-combo/select2.js"></script>
    <script src="../../Recursos/bootstrap-combo/es.js"></script>

    <!-- jQuery UI -->
    <link href="../../Recursos/jquery-ui/jquery-ui.min.css" rel="stylesheet" />
    <script src="../../Recursos/jquery-ui/jquery-ui.min.js"></script>

    <script src="../../Recursos/javascript/catalogos/Js_Tra_Cat_Productos_Proveedores.js"></script>
    <script src="../../Recursos/javascript/catalogos/Js_Tra_Cat_Productos.js"></script>

    <!-- Angular - Productos -->
    <script src="../../Recursos/angular/app/services/ProductoService.js"></script>
    <script src="../../Recursos/angular/app/controllers/ProductoController.js"></script>

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
    <div id="div_principal_Productos">
        <div class="container-fluid" style="height: 100vh;">
            <div class="page-header">
                <div class="row">
                    <div class="col-sm-12 text-left" style="background-color: white!important;">
                        <h3>Cat&aacute;logos de Productos</h3>
                    </div>
                </div>
            </div>

            <div class="panel panel-color panel-info collapsed" id="panel1">
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
                            <label style="margin-top: 10px;">Nombre</label>
                        </div>
                        <div class="col-md-3">
                            <input type="text" id="txt_busqueda_por_nombre" class="form-control" placeholder="B&uacute;squeda por nombre" />
                        </div>
                        <div class="col-md-1">
                            <label style="margin-top: 10px;">C&oacute;digo</label>
                        </div>
                        <div class="col-md-3">
                            <input type="text" id="txt_busqueda_por_codigo" class="form-control" placeholder="B&uacute;squeda por c&oacute;digo" />
                        </div>
                        <div class="col-md-1">
                            <label style="margin-top: 10px;">Estatus</label>
                        </div>
                        <div class="col-md-3">
                            <select id="cmb_estatusfiltro" name="cmb_estatusfiltro" class="form-control input-sm" style="border-radius: inherit"></select>
                        </div>
                    </div>
                    <div class ="row" style="margin-top:8px;"></div>
                    <div class="row">
                        <div class="col-md-1">
                            <label style="margin-top: 10px;">C. Stock</label>
                        </div>
                        <div class="col-md-3">
                            <div style="margin-left:5px; margin-top:5px;"><select id="cmb_control_stock_b" class="form-control"></select></div>
                        </div>
                        <div class="col-md-1">
                            <label style="margin-top: 10px;">T. Prod.</label>
                        </div>
                        <div class="col-md-3">
                            <div style="margin-left:5px; margin-top:5px;"><select id="cmb_tipo_producto_b" class="form-control"></select></div>
                        </div>
                        <div class="col-md-4" style="text-align:right; margin-top: 8px !important;">
                            <button type="button" id="btn_busqueda" class="btn btn-secondary btn-icon btn-icon-standalone btn-lg">
                                <i class="fa fa-search"></i>
                                <span>Buscar</span>
                            </button>
                        </div>
                    </div>
                </div>
            </div>

            <div id="toolbar" style="margin-left: 5px;  display:block;">
                <div class="btn-group" role="group" style="margin-left: 5px;">
                    <button id="btn_salir" type="button" class="btn btn-info btn-sm" title="Salir"><i class="glyphicon glyphicon-home"></i></button>
                    <button id="btn_nuevo" type="button" class="btn btn-info btn-sm" title="Nuevo"><i class="glyphicon glyphicon-plus"></i></button>
                    <button id="btn_exportar" type="button" class="btn btn-info btn-sm" title="Exportar Excel"><i class="fa fa-download"></i></button>
                </div>
            </div>
            <table id="tbl_productos" data-toolbar="#toolbar" class="table table-responsive"></table>

        </div>
    </div>

    <div id="div_Informacion" style="display:none">
        <div id="div_crear_Catalogo_Productos">
            <div class="container-fluid" style="height: 100vh;">
                <div id="div_solo_informacion_productos">
                    <div class="page-header">
                        <div class="row">
                            <div class="col-sm-8 text-left" style="background-color: white!important;">
                                <h3>Detalle del Producto</h3>
                            </div>
                            <div class="col-sm-4" style="text-align:right">
                                <button id="btn_solo_salir_productos" type="button" class="btn btn-info btn-sm" title="Cancelar"><i class="glyphicon glyphicon-circle-arrow-left"></i>&nbsp;&nbsp;Regresar</button>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-2 etq" style="text-align:left">
                            <label class="fuente_lbl_controles">C&oacute;digo</label>
                        </div>
                        <div class="col-md-4 ctrl" style="text-align:left">
                            <label id="lbl_codigo_detalle"></label>
                            <input type="hidden" id="lbl_producto_detalle_id" />
                        </div>
                        <div class="col-md-2 etq" style="text-align:left">
                            <label class="fuente_lbl_controles">Nombre</label>
                        </div>
                        <div class="col-md-4 form-inline ctrl_right">
                            <label id="lbl_nombre_detalle"></label>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-2 etq" style="text-align:left">
                            <label class="fuente_lbl_controles">Estatus</label>
                        </div>
                        <div class="col-md-4 ctrl" style="text-align:left">
                            <label id="lbl_estatus_detalle"></label>
                        </div>
                        <div class="col-md-2 etq" style="text-align:left">
                            <label class="fuente_lbl_controles">Control Stock</label>
                        </div>
                        <div class="col-md-4 ctrl_right" style="text-align:left">
                            <label id="lbl_control_stock_detalle"></label>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-2 etq" style="text-align:left">
                            <label class="fuente_lbl_controles">Tipo de Producto</label>
                        </div>
                        <div class="col-md-4 ctrl" style="text-align:left">
                            <label id="lbl_tipo_producto_detalle"></label>
                        </div>
                        <div class="col-md-2 etq" style="text-align:left">
                            <label class="fuente_lbl_controles">Categoria Producto</label>
                        </div>
                        <div class="col-md-4 ctrl_right" style="text-align:left">
                            <label id="lbl_categoria_detalle"></label>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-2 etq" style="text-align:left">
                            <label class="fuente_lbl_controles">Unidad de Compra</label>
                        </div>
                        <div class="col-md-4 ctrl" style="text-align:left">
                            <label id="lbl_unidad_compra_detalle"></label>
                        </div>
                        <div class="col-md-2 etq" style="text-align:left">
                            <label class="fuente_lbl_controles">Unidad de Almacenamiento</label>
                        </div>
                        <div class="col-md-4 ctrl_right" style="text-align:left">
                            <label id="lbl_unidad_almacenamiento_detalle"></label>
                        </div>
                    </div>
            
                    <div class="row">
                        <div class="col-md-2 etq" style="text-align:left">
                            <label class="fuente_lbl_controles" style="width: 100%;">Costo Promedio</label>
                        </div>
                        <div class="col-md-4 ctrl" style="text-align:left">
                            <label id="lbl_costo_promedio_detalle"></label>
                        </div>
                        <div class="col-md-2 etq" style="text-align:left">
                            <label class="fuente_lbl_controles" style="width: 100%;">&Uacute;ltimo Costo</label>
                        </div>
                        <div class="col-md-4 ctrl_right" style="text-align:left">
                            <label id="lbl_ultimo_costo_detalle"></label>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-2 etq" style="text-align:left">
                            <label class="fuente_lbl_controles" style="width: 100%;">Volumen</label>
                        </div>
                        <div class="col-md-4 ctrl" style="text-align:left">
                            <label id="lbl_volumen_detalle"></label>
                        </div>
                        <div class="col-md-2 etq" style="text-align:left">
                            <label class="fuente_lbl_controles">Unidad de Volumen</label>
                        </div>
                        <div class="col-md-4 ctrl_right" style="text-align:left">
                            <label id="lbl_unidad_volumen_detalle"></label>
                        </div>
                    </div>

                    <div class="row">
                       <div class="col-md-2 etq" style="text-align:left;">
                            <label class="fuente_lbl_controles" style="width: 100%;">Peso</label>
                       </div>
                       <div class="col-md-4 ctrl" style="text-align:left">
                            <label id="lbl_peso_detalle"></label>
                       </div>
                       <div class="col-md-2 etq" style="text-align:left; border-bottom: solid 0.1px #95B8E7;">
                            <label class="fuente_lbl_controles">Unidad de Peso</label>
                       </div>
                       <div class="col-md-4 ctrl_right" style="text-align:left;">
                           <label id="lbl_unidad_peso_detalle"></label>
                       </div>
                    </div>

                    <div class="row">
                        <div class="col-md-2 etq" style="text-align:left; border-bottom: solid 0.1px #95B8E7;">
                            <label class="fuente_lbl_controles" style="width: 100%;">Observaciones</label>
                        </div>
                        <div class="col-md-10 ctrl_right" style="text-align:left; border-bottom-color: #ccc; border-bottom-width: 0.1px; border-bottom-style: dotted;">
                            <label id="lbl_observaciones_detalle"></label>
                        </div>
                    </div>
                </div>

                <div class="row space"></div>
                <hr />

                <div class="row">
                    <div class="col-md-12" style="padding-left: 3px !important;"> 
                        <div id="div_Tabs">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="panel panel-primary" style="padding: 0px !important;">
                                        <div class="panel-heading">
                                            <h4 class="panel-title" style="color: #000 !important;">&nbsp;</h4>
                                            <span class="pull-left">
						                        <ul id="tabs" class="nav panel-tabs" data-tabs="tab">
							                        <li id="proveedores_id" class="active"><a href="#proveedores" data-toggle="tab">Proveedores</a></li>
							                        <li id="lista_productos_id" ><a href="#lista_productos" data-toggle="tab">BOM</a></li>
							                        <li  id="producto_terminado_id" ><a href="#producto_terminado" data-toggle="tab">Producto Terminado</a></li>                                
						                        </ul>
                                            </span>
                                        </div>
                                        <div class="panel-body">
					                        <div id="my-tab-content" class="tab-content">
						                        <div class="tab-pane active" id="proveedores">
							                        <table id="tbl_productos_proveedores_detalles" class="table table-responsive"></table>
						                        </div>
						                        <div class="tab-pane" id="lista_productos">
							                        <div class="row">
								                        <div class="col-md-1" style="padding-top: 8px !important;">
									                        <label for="cmb_lista_materiales" class="fuente_lbl_controles">Productos</label>
								                        </div>
								                        <div class="col-md-3" style="text-align:left; padding-top: 8px !important;">
									                        <select id="cmb_lista_materiales" class="form-control"></select>
								                        </div>
								                        <div class="col-md-1" style="padding-top: 8px !important;">
									                        <label for="txt_cantidad" class="fuente_lbl_controles">Cantidad</label>
								                        </div>
								                        <div class="col-md-3 form-inline">
									                        <input id="txt_cantidad_Productos" type="text" class="form-control" style="width: 60%; padding-top: 5px;" title="Ingresar la cantidad"  pattern="[0-9]"/>
									                        <button id="btn_nuevo_lista" type="button" class="btn btn-info btn-sm" title="Agregar Lista de Materiales" style="margin: 0px; padding: 4px 6px;">
										                        <i class="fa fa-plus"></i>&nbsp;&nbsp;Agregar
									                        </button>
								                        </div>
							                        </div>

							                        <hr />
							                        <table id="tbl_lista_productos" class="table table-responsive"></table>
						                        </div>
						                        <div class="tab-pane" id="producto_terminado">
							                        <table id="tbl_producto_terminado" class="table table-responsive"></table>
						                        </div>
					                        </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
