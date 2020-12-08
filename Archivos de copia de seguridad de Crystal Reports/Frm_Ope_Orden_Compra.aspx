<%@ Page Title="Orden de Compra" Language="C#" MasterPageFile="~/Paginas/Paginas_Generales/MasterPage.Master" AutoEventWireup="true"
    CodeBehind="Frm_Ope_Orden_Compra.aspx.cs" Inherits="web_trazabilidad.Paginas.Trazabilidad.Frm_Ope_Orden_Compra" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../../Recursos/bootstrap-date/moment.min.js"></script>
    <link href="../../Recursos/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../../Recursos/estilos/demo_form.css" rel="stylesheet" />
    <link href="../../Recursos/bootstrap-combo/select2.css" rel="stylesheet" />
    <link href="../../Recursos/bootstrap-table/bootstrap-table.css" rel="stylesheet" />
    <link href="../../Recursos/bootstrap-table/extensions/editable/bootstrap-editable.css" rel="stylesheet" />
    <link href="../../Recursos/bootstrap-date/bootstrap-datetimepicker.css" rel="stylesheet" />
    <link href="../../Recursos/estilos/css_orden_compra.css" rel="stylesheet" />
    <link href="../../Recursos/estilos/center_loader.css" rel="stylesheet" />

    <script src="../../Recursos/bootstrap-table-current/bootstrap-table.js"></script>
    <script src="../../Recursos/bootstrap-table-current/locale/bootstrap-table-es-MX.js"></script>
    <script src="../../Recursos/bootstrap-table-current/extensions/export/bootstrap-table-export.js"></script>
    <script src="../../Recursos/bootstrap-table-current/extensions/export/tableExport.js"></script>
    <script src="../../Recursos/bootstrap-combo/select2.js"></script>
    <script src="../../Recursos/bootstrap-combo/es.js"></script>


    <script src="../../Recursos/bootstrap-table/extensions/editable/bootstrap-editable.js"></script>
    <script src="../../Recursos/bootstrap-table-current/extensions/editable/bootstrap-table-editable.js"></script>

    <script src="../../Recursos/plugins/parsley.js"></script>
    <script src="../../Recursos/bootstrap-date/bootstrap-datetimepicker.min.js"></script>
    <script src="../../Recursos/plugins/jquery.formatCurrency-1.4.0.min.js"></script>
    <script src="../../Recursos/plugins/jquery.formatCurrency.all.js"></script>
    <script src="../../Recursos/plugins/accounting.min.js"></script>
    <script src="../../Recursos/javascript/seguridad/Js_Controlador_Sesion.js"></script>
    <script src="../../Recursos/plugins/jquery.qtip-1.0.0-rc3.min.js"></script>
    <link href="../../Recursos/bootstrap-pdf-viewer/view-pdf.css" rel="stylesheet" />
    <script src="../../Recursos/bootstrap-pdf-viewer/view-pdf.js"></script>
    <script src="../../Recursos/javascript/trazabilidad/Js_Ope_Orden_Compra.js"></script>

    <!-- Angular - Productos -->
    <script src="../../Recursos/angular/app/services/ProductoService.js"></script>
    <script src="../../Recursos/angular/app/controllers/ProductoController.js"></script>

    <!-- Agnular - Detalle Producto -->
    <script src="../../Recursos/angular/app/controllers/DetalleProductoController.js"></script>

    <!-- Angular - Proveedores -->
    <script src="../../Recursos/angular/app/services/ProveedorService.js"></script>
    <script src="../../Recursos/angular/app/controllers/ProveedorController.js"></script>

    <style>
        .panel {
            padding-bottom: 10px !important;
            border: none !important;
            box-shadow: none !important;
        }

        #tbl_ordenes_compra thead tr th {
            border: none !important;
        }

            #tbl_ordenes_compra thead tr th:nth-child(n+6) {
                border-left: 2px solid #ddd !important;
                border-right: 2px solid #ddd !important;
            }

        .modal-open[style] {
            padding-right: 0px !important;
        }

        body {
            overflow: scroll !important;
            background-color: whitesmoke;
        }

        .btn-circle {
            width: 30px;
            height: 30px;
            text-align: center;
            padding: 6px 0;
            font-size: 12px;
            line-height: 1.428571429;
            border-radius: 15px;
        }

            .btn-circle.btn-lg {
                width: 50px;
                height: 50px;
                padding: 10px 16px;
                font-size: 18px;
                line-height: 1.33;
                border-radius: 25px;
            }

            .btn-circle.btn-xl {
                width: 70px;
                height: 70px;
                padding: 10px 16px;
                font-size: 24px;
                line-height: 1.33;
                border-radius: 35px;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div id="div_principal_orden_compra">

        <div class="container-fluid" style="height: 100vh;">
            <div class="row">
                <div class="col-sm-12 text-left" style="background-color: white!important;">
                    <h3>Órdenes de Compra</h3>
                </div>
            </div>

            <hr />

            <div class="panel panel-color panel-info" id="panel_1">
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
                <div class="panel-body">

                    <div class="row" style="display: block;">
                        <div class="col-md-4" style="display: block">
                            <label for="cmb_proveedor_filtro" class="text-bold text-left text-medium fuente_lbl_controles">Proveedor</label>
                            <select id="cmb_proveedor_filtro"></select>
                        </div>
                        <div class="col-md-2">
                            <label class="text-bold text-left text-medium fuente_lbl_controles">No. Orden</label>
                            <input type="text" id="txt_busqueda_por_no_orden" class="form-control" style="margin: 0px !important; border-radius: 3px !important" placeholder="No. Orden" />
                        </div>
                        <div class="col-md-2">
                            <label for="txt_fecha_inicio" class="text-bold text-left text-medium fuente_lbl_controles">Fecha Inicio</label>
                            <%--     <div class="form-group" style="margin: 3px;">--%>
                            <div class="input-group date" id="dtp_fecha_inicio" style="z-index: 1000 !important;">
                                <input type="text" id="txt_fecha_inicio" class="form-control" style="margin: 0px;" placeholder="dd/mm/aaaa" />
                                <span class="input-group-addon">
                                    <span class="glyphicon glyphicon-calendar"></span>
                                </span>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <label for="txt_fecha_termino" class="text-bold text-left text-medium fuente_lbl_controles">Fecha Termino</label>
                            <div class="input-group date" id="dtp_fecha_termino">
                                <input type="text" id="txt_fecha_termino" class="form-control" style="margin: 0px;" placeholder="dd/mm/aaaa" />
                                <span class="input-group-addon">
                                    <span class="glyphicon glyphicon-calendar"></span>
                                </span>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <label class="text-bold text-left text-medium fuente_lbl_controles">Estatus</label>
                            <select id="cmb_estatusfiltro" name="cmb_estatusfiltro" class="form-control input-sm" style="border-radius: inherit"></select>
                        </div>
                    </div>

                    <div class="row" style="margin-top: 7px;"></div>


                    <div class="row">
                        <div class="col-md-2 col-md-offset-10" align="right">
                            <button type="button" id="btn_busqueda" class="btn btn-secondary btn-icon btn-icon-standalone btn-lg" style="width: 100% !important;">
                                <i class="fa fa-search"></i>
                                <span>Buscar</span>
                            </button>
                        </div>
                    </div>
                </div>
            </div>

            <hr />

            <div id="toolbar" style="margin-left: 5px; margin-top: 11px; text-align: right">
                <div class="btn-group" role="group" style="margin-left: 5px;">
                    <button id="btn_inicio" type="button" class="btn btn-info btn-sm" style="border-radius: 0px !important; border-top-left-radius: 6px !important; border-bottom-left-radius: 6px !important;"><i class="glyphicon glyphicon-home"></i></button>
                    <button id="btn_exportar" type="button" class="btn btn-info btn-sm" style="border-radius: 0px !important; height: 30px" title="Exportar Excel"><i class="fa fa-download"></i></button>
                    <button id="btn_nueva" type="button" class="btn btn-info btn-sm" style="border-radius: 0px !important; border-top-right-radius: 6px !important; border-bottom-right-radius: 6px !important;"><i class="glyphicon glyphicon-plus"></i></button>
                </div>
            </div>
            <table id="tbl_ordenes_compra" data-toolbar="#toolbar" class="table table-responsive"></table>
        </div>

    </div>
    <div id="div_crear_orden_compra" style="display: none">
        <div class="container-fluid" style="height: 100vh;">
            <div class="row" style="display: none !important;">
                <div class="col-sm-12 text-left" style="background-color: white!important;">
                    <h3>Generar Orden de Compra</h3>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-12" align="right">
                    <button id="btn_salir" type="button" class="btn btn-primary btn-sm" title=""><i class="glyphicon glyphicon-remove"></i>&nbsp;&nbsp;Cancelar Orden de Compra</button>
                    <button id="btn_guardar_solicitud" type="button" class="btn btn-primary btn-sm" title="">
                        <i class="glyphicon glyphicon-floppy-disk"></i>&nbsp;&nbsp;Guardar Orden de Compra
                    </button>
                    <input type="hidden" id="txt_no_orden_compra" />
                </div>
            </div>

            <hr />
            <%--Contenedor de rows de formulario--%>

            <div class="row">
                <div class="col-md-1 col-xs-1">
                    <label for="txt_justificacion" class="fuente_lbl_controles">Justificaci&oacute;n</label>
                </div>
                <div class="col-md-11 col-xs-11">
                    <textarea id="txt_justificacion" rows="2" class="form-control" style="resize: none; width: 100%"></textarea>
                </div>
            </div>

            <div class="row" style="margin-top: 8px;"></div>

            <div class="row">
                <div class="col-md-4">
                    <div class="form-inline">
                        <label for="cmb_proveedores" class="fuente_lbl_controles">Proveedores</label>
                        <span id="btnCrearProveedor" class="fa fa-plus-circle" title="Crear proveedor" style="background: '#F5F6CE'; color: '#2d2d30'; cursor: pointer;"></span>
                        <a id="ver_info_proveedor" href="#" style="text-align:right">
                            <i id="info_proveedor" class="fa fa-info-circle" style="font-size: 20px !important; margin-left: 0px;" title="Mostrar datos del proveedor"></i>
                        </a>
                    </div>
                    <%--<label for="cmb_proveedores" class="fuente_lbl_controles"><a id="lnk_proveedor" title="">Proveedores</a></label>--%>
                    <select id="cmb_proveedores"></select>
                </div>

                <div class="col-md-3">
                    <div class="form-inline">
                        <label for="cmb_productos" class="fuente_lbl_controles">Productos</label>
                        <span id="btnCrearProductoProveedor" class="fa fa-plus-circle" title="Crear producto" style="background: '#F5F6CE'; color: '#2d2d30'; cursor: pointer;"></span>
                    </div>
                    <select id="cmb_productos" class="form-control"></select>
                </div>
                <div class="col-md-2">
                     <label for="txt_precio_modificable" class="fuente_lbl_controles">Precio</label>
                    <input type="text" id="txt_precio_modificable" class="form-control" style="margin-top:0px !important" />
                </div>
                <div class="col-md-2" style="display: block;">
                    <label for="cmb_tipos_impuestos" class="fuente_lbl_controles">Impuesto</label>
                    <select id="cmb_tipos_impuestos" class="form-control"></select>
                </div>
                <div class="col-md-1">
                    <button id="btn_nuevo" type="button" class="btn btn-success" style="margin-top: 18px">
                        <i class="fa fa-plus"></i>&nbsp;&nbsp;Agregar
                    </button>
                </div>
            </div>
            <div class="row" style="margin-top: 8px;"></div>
            <%--Contenedor de div emergente--%>
            <div class="panel panel-color panel-black" id="panel1" style="display: none; max-width: 100%;">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        <i style="color: white;" class="fa fa-info-circle"></i>&nbsp;Datos del Proveedor
                    </h3>

                    <div class="panel-options">
                        <a href="#" data-toggle="panel">
                            <span class="collapse-icon">–</span>
                            <span class="expand-icon">+</span>
                        </a>
                        <a href="#" data-toggle="remove">×</a>
                    </div>
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-md-2 col-xs-2">
                            <label class="fuente_lbl_controles">Clave:</label>
                        </div>
                        <div class="col-md-10 col-xs-10">
                            <span id="etq_clave"></span>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-2 col-xs-2">
                            <label class="fuente_lbl_controles">Nombre:</label>
                        </div>
                        <div class="col-md-10 col-xs-10">
                            <span id="etq_nombre"></span>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-2 col-xs-2">
                            <label class="fuente_lbl_controles">Contacto:</label>
                        </div>
                        <div class="col-md-10 col-xs-10">
                            <span id="etq_contacto"></span>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-2 col-xs-2">
                            <label class="fuente_lbl_controles">Telefono:</label>
                        </div>
                        <div class="col-md-10 col-xs-10">
                            <span id="etq_contacto_telefono"></span>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-2 col-xs-2">
                            <label class="fuente_lbl_controles">Email:</label>
                        </div>
                        <div class="col-md-10 col-xs-10">
                            <span id="etq_contacto_email"></span>
                        </div>
                    </div>
                </div>
            </div>


            <hr />

            <%--Contenedor detalle de producto y de tabla--%>
            <div class="row" style="display: none">
                <div class="col-md-1 col-xs-1">
                    <label for="txt_clave_producto" class="text-center fuente_lbl_controles" style="width: 100%;">Clave</label>
                    <input id="txt_clave_producto" type="text" class="form-control" disabled="disabled" />
                    <input id="txt_producto_id" type="hidden" />
                </div>
                <div class="col-md-5 col-xs-5">
                    <label for="txt_nombre_producto" class="text-left fuente_lbl_controles" style="width: 100%;">Nombre Producto</label>
                    <input id="txt_nombre_producto" type="text" class="form-control" disabled="disabled" />
                </div>
                <div class="col-md-2 col-xs-2">
                    <label for="txt_precio" class="text-center fuente_lbl_controles" style="width: 100%;">Precio</label>
                    <input id="txt_precio" type="text" class="form-control btn-shopping-car" placeholder="$0.00" disabled="disabled"
                        onblur="javascript:Formato_Moneda(this);" />
                </div>
                <div class="col-md-2 col-xs-2">
                    <label for="txt_tasa" class="text-center fuente_lbl_controles" style="width: 100%;">Impuesto</label>
                    <input id="txt_tasa" type="text" class="form-control btn-shopping-car" placeholder="0" disabled="disabled"
                        onchange="javascript:Formato_Moneda(this);" />
                </div>
                <div class="col-md-2 col-xs-2">
                    <label for="txt_precio_con_iva" class="text-center fuente_lbl_controles" style="width: 100%;">Precio C/IVA</label>
                    <input id="txt_precio_con_iva" type="text" class="form-control btn-shopping-car" placeholder="$0.00" disabled="disabled"
                        onchange="javascript:Formato_Moneda(this);" />
                </div>
            </div>
            <div class="row">
                <div class="col-md-2 col-xs-2" style="display: none">
                    <label for="txt_unidad_compra_id" class="text-center" style="width: 100%;">Precio C/IVA</label>
                    <input id="txt_unidad_compra_id" type="text" class="form-control btn-shopping-car" />
                    <input id="txt_unidad_compra" type="text" class="form-control btn-shopping-car" />
                    <input id="txt_controlstock" type="hidden" />
                </div>
                <div class="col-md-10 col-xs-10">
                </div>
            </div>
            <div class="row">
                <div class="col-sm-10">
                    <table id="tbl_productos_orden_compra" class="table table-responsive"></table>
                </div>
                <div class="col-sm-2">
                    <table class="table table-responsive tbl_totales">
                        <tr>
                            <td>
                                <label for="txt_Subtotal" class="fuente_lbl_controles">Subtotal</label>
                                <input type="text" id="txt_Subtotal" class="form-control text-right" style="border-radius: 10px;" disabled="disabled" />
                            </td>
                        </tr>

                        <tr>
                            <td>
                                <label for="txt_Iva" class="fuente_lbl_controles">Impuesto</label>
                                <input type="text" id="txt_Iva" class="form-control text-right" style="border-radius: 10px;" disabled="disabled" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label for="txt_total" class="fuente_lbl_controles">Total</label>
                                <input type="text" id="txt_total" class="form-control text-right" style="border-radius: 10px;" disabled="disabled" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
