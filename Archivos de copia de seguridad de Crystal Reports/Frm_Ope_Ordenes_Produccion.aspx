<%@ Page Title="" Language="C#" MasterPageFile="~/Paginas/Paginas_Generales/MasterPage.Master" AutoEventWireup="true" CodeBehind="Frm_Ope_Ordenes_Produccion.aspx.cs" Inherits="web_trazabilidad.Paginas.Trazabilidad.Frm_Ope_Ordenes_Produccion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../../Recursos/bootstrap-date/moment.min.js"></script>
    <link href="../../Recursos/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../../Recursos/estilos/demo_form.css" rel="stylesheet" />
    <link href="../../Recursos/bootstrap-combo/select2.css" rel="stylesheet" />
    <link href="../../Recursos/bootstrap-table/bootstrap-table.css" rel="stylesheet" />
    <link href="../../Recursos/bootstrap-table/extensions/editable/bootstrap-editable.css" rel="stylesheet" />
    <link href="../../Recursos/bootstrap-date/bootstrap-datetimepicker.css" rel="stylesheet" />
    <link href="../../Recursos/CreativeCSS3/css/style10.css" rel="stylesheet" />
    <script src="../../Recursos/bootstrap-combo/select2.js"></script>
    <script src="../../Recursos/bootstrap-combo/es.js"></script>

    <script src="../../Recursos/bootstrap-table/bootstrap-table.min.js"></script>
    <script src="../../Recursos/bootstrap-table/locale/bootstrap-table-es-MX.min.js"></script>
    <script src="../../Recursos/bootstrap-table/extensions/editable/bootstrap-editable.js"></script>
    <script src="../../Recursos/bootstrap-table/extensions/editable/bootstrap-table-editable.min.js"></script>
    <script src="../../Recursos/bootstrap-table/bootstrap-table-contextmenu.min.js"></script>

    <script src="../../Recursos/bootstrap-table-current/bootstrap-table.js"></script>
    <script src="../../Recursos/bootstrap-table-current/locale/bootstrap-table-es-MX.js"></script>
    <script src="../../Recursos/bootstrap-table-current/extensions/export/bootstrap-table-export.js"></script>
    <script src="../../Recursos/bootstrap-table-current/extensions/export/tableExport.js"></script>

    <script src="../../Recursos/plugins/parsley.js"></script>
    <script src="../../Recursos/plugins/jquery.formatCurrency-1.4.0.min.js"></script>
    <script src="../../Recursos/plugins/jquery.formatCurrency.all.js"></script>
    <script src="../../Recursos/plugins/accounting.min.js"></script>
    <script src="../../Recursos/plugins/pinch.js"></script>
    <script src="../../Recursos/javascript/seguridad/Js_Controlador_Sesion.js"></script>
    <script src="../../Recursos/bootstrap-date/moment.min.js"></script>
    <script src="../../Recursos/bootstrap-date/bootstrap-datetimepicker.min.js"></script>
    <script src="../../Recursos/plugins/jquery.qtip-1.0.0-rc3.min.js"></script>
    <link href="../../Recursos/bootstrap-pdf-viewer/view-pdf.css" rel="stylesheet" />
    <script src="../../Recursos/bootstrap-pdf-viewer/view-pdf.js"></script>
    <script src="../../Recursos/javascript/trazabilidad/Js_Ope_Ordenes_Produccion.js"></script>

    <style>
        .panel {
            padding-bottom: 10px !important;
            border: none !important;
            box-shadow: none !important;
        }

        #tbl_ordenes_produccion thead tr th {
            border: none !important;
        }

            #tbl_ordenes_produccion thead tr th:nth-child(n+4) {
                border-left: 2px solid #ddd !important;
                border-right: 2px solid #ddd !important;
            }

        .select2-container {
            width: 100% !important;
        }

        /*.modal-body
        {
            background-color: #f00;
        }*/
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div id="div_botones">
        <div class="container-fluid" style="height: 100vh;">
            <div class="row" align="center">
                <div class="col-md-12">
                    <ul class="ca-menu">
                        <li>
                            <a href="#" id="btn_administracion">
                                <span class="ca-icon"><i class="fa fa-send"></i></span>
                                <div class="ca-content">
                                    <h2 class="ca-main">Administración O. Producci&oacute;n</h2>
                                </div>
                            </a>
                        </li>
                        <li>
                            <a href="#" id="btn_nuevo_grande">
                                <span class="ca-icon"><i class="fa fa-cogs"></i></span>
                                <div class="ca-content">
                                    <h2 class="ca-main">Nueva Orden</h2>
                                </div>
                            </a>
                        </li>
                        <li>
                            <a href="#" id="btn_listado">
                                <span class="ca-icon"><i class="fa fa-list-ul"></i></span>
                                <div class="ca-content">
                                    <h2 class="ca-main">Ordenes de Producci&oacute;n</h2>
                                </div>
                            </a>
                        </li>
                    </ul>
                </div>
            </div>
        </div>
    </div>

    <div id="div_principal_orden_produccion" style="display: none;">

        <div class="container-fluid" style="height: 100vh;">
            <div class="row">
                <div class="col-sm-12 text-left" style="background-color: white!important;">
                    <h3>Órdenes de Trabajo</h3>
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
                    <div class="row">
                        <div class="col-md-3">
                            <label class="fuente_lbl_controles" for="txt_busqueda_por_no_orden">No. Orden Trabajo</label>
                            <input id="txt_busqueda_por_no_orden" type="text"  style="margin-top:0px" class="form-control" placeholder="No. Orden" />
                        </div>
                        <div class="col-md-3">
                            <label class="fuente_lbl_controles" for="cmb_estatus_busqueda">Estatus</label>
                            <select id="cmb_estatus_busqueda" class="form-control" style="margin-top:0px">
                                <option value="">Selecciona el Estatus</option>
                                <option value="Generada">Generada</option>
                                <option value="Produccion">Produccion</option>
                                <option value="Cancelada">Cancelada</option>
                                <option value="Completa">Completa</option>
                                <option value="Suspendida">Suspendida</option>
                            </select>
                        </div>
                        <div class="col-md-6">
                            <label class="fuente_lbl_controles" for="cmb_no_parte_busqueda">No. Parte</label>
                            <select id="cmb_no_parte_busqueda" class="form-control"></select>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3">
                            <label class="fuente_lbl_controles" for="cmb_prioridad_busqueda">Prioridad</label>
                            <select id="cmb_prioridad_busqueda" class="form-control"  style="margin-top:0px">
                                <option value="">Selecciona la prioridad</option>
                                <option value="Baja">Baja</option>
                                <option value="Media">Media</option>
                                <option value="Alta">Alta</option>
                            </select>
                        </div>
                        <div class="col-md-3">
                            <label class="fuente_lbl_controles" for="cmb_ubicacion_busqueda">Ubicacion</label>
                            <%--<select id="cmb_ubicacion_busqueda" class="form-control"></select>--%>
                            <select id="cmb_ubicacion_busqueda" name="cmb_ubicacion_busqueda"  style="margin-left:10px !important" class="form-control input-sm" style="border-radius: inherit"></select>
                        </div>
                        <div class="col-md-3">
                            <label class="text-bold text-left text-medium fuente_lbl_controles" title="Fecha Inicio">Fecha Inicio</label>
                            <div class="input-group date" id="dtp_fecha_inicio">
                                <input type="text" id="fi" class="form-control" style="margin: 0px;" placeholder="dd/mm/aaaa" />
                                <span class="input-group-addon">
                                    <span class="glyphicon glyphicon-calendar"></span>
                                </span>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <label class="text-bold text-left text-medium fuente_lbl_controles" title="Fecha Termino">Fecha Termino</label>
                            <div class="input-group date" id="dtp_fecha_termino">
                                <input type="text" id="ff" class="form-control" style="margin: 0px;" placeholder="dd/mm/aaaa" />
                                <span class="input-group-addon">
                                    <span class="glyphicon glyphicon-calendar"></span>
                                </span>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3">
                            <label class="fuente_lbl_controles" for="cmb_empaque_busqueda">Unidad de Medida</label>
                            <select id="cmb_empaque_busqueda" class="form-control"></select>
                        </div>
                        <div class="col-md-4">
                        </div>
                        <div class="col-md-5" style="margin-top: 18px !important; text-align: right !important;">
                            <button type="button" id="btn_busqueda" class="btn btn-info" style="font-size: 20px; height: 40px !important; font-weight: bold">
                                <i class="fa fa-search"></i>
                                <span>Buscar &Oacute;rdenes de Trabajo</span>
                            </button>
                        </div>
                    </div>
                </div>
            </div>
            <div id="toolbar" style="margin-left: 4px; text-align: right;">
                <div class="btn-group" role="group" style="margin-left: 4px;">
                    <button id="btn_inicio" type="button" class="btn btn-info btn-sm" style="border-radius: 0px !important; border-top-left-radius: 6px !important; border-bottom-left-radius: 6px !important;"><i class="glyphicon glyphicon-home"></i></button>
                    <button id="btn_regresar" class="btn btn-info btn-sm" type="button"><i class="fa fa-reply"></i></button>
                    <button id="btn_exportar" type="button" class="btn btn-info btn-sm" title="Exportar Excel"><i class="fa fa-download"></i></button>
                    <button id="btn_nueva" type="button" class="btn btn-info btn-sm" style="border-radius: 0px !important; border-top-right-radius: 6px !important; border-bottom-right-radius: 6px !important;"><i class="glyphicon glyphicon-plus"></i></button>
                </div>
            </div>
            <table id="tbl_ordenes_produccion" data-toolbar="#toolbar" class="table table-responsive"></table>
        </div>

    </div>
    <div id="div_orden_produccion" style="display: none; height: 100vh !important;">
        <div class="row">
            <div class="col-sm-12 text-left" style="background-color: white!important;">
                <h3>Generar Orden de Trabajo</h3>
            </div>
        </div>
        <div class="row">
            <div class="col-sm-12" style="text-align: right">
                <button id="btn_salir" type="button" class="btn btn-primary btn-sm" title=""><i class="fa fa-reply"></i>&nbsp;&nbsp;Salir Orden de Producción</button>
                <button id="btn_guardar_orden" type="button" class="btn btn-primary btn-sm" title="">
                    <i class="glyphicon glyphicon-floppy-disk"></i>&nbsp;&nbsp;Guardar Orden de Producción
                </button>
            </div>
        </div>
        <hr />
        <%--Contenedor de rows de formulario--%>
        <div class="row" style="display: none">
            <div class="col-md-2">
                <label for="txt_no_orden_produccion" class="fuente_lbl_controles"><i class="fa fa-cogs"></i>&nbsp;&nbsp;No. Orden Producción</label>
                <input id="txt_no_orden_produccion" type="text" class="form-control" disabled="disabled" />
            </div>
        </div>
        <div class="row">
            <div class="col-md-6">
                <label class="fuente_lbl_controles" for="cmb_no_parte">(*)No. Parte</label>
                <select id="cmb_no_parte" class="form-control"></select>
            </div>
            <div class="col-md-3">
                <label class="text-bold text-left text-medium fuente_lbl_controles" title="Fecha Orden Trabajo" style="margin-top: 0px">(*) Fecha Orden Trabajo</label>
                <div class="input-group date" id="dtp_fecha_orden_trabajo">
                    <input type="text" id="fp" class="form-control" style="margin-top: 0px;" placeholder="dd/mm/aaaa" />
                    <span class="input-group-addon">
                        <span class="glyphicon glyphicon-calendar"></span>
                    </span>
                </div>
            </div>
           <div class="col-md-3 ">
                <label class="fuente_lbl_controles" for="cmb_estatus">(*) Estatus</label>
                <select id="cmb_estatus" class="form-control" style="margin-top: 0px">
                    <option value="">Selecciona el Estatus</option>
                    <option value="Generada">Generada</option>
                    <option value="Produccion">Produccion</option>
                    <option value="Cancelada">Cancelada</option>
                    <option value="Completa">Completa</option>
                    <option value="Suspendida">Suspendida</option>
                </select>
            </div>
        </div>
        <div class="row">
             <div class="col-md-2">
                <label class="fuente_lbl_controles" for="cmb_prioridad">(*) Prioridad</label>
                <select id="cmb_prioridad" class="form-control" style="margin-top: 0px">
                    <option value="">Selecciona la prioridad</option>
                    <option value="Baja">Baja</option>
                    <option value="Media">Media</option>
                    <option value="Alta">Alta</option>
                </select>
            </div>
             <div class="col-md-2">
                <label for="txt_cantidad" class="fuente_lbl_controles">(*) Cantidad</label>
                <input id="txt_cantidad" type="text" class="form-control" placeholder="Cantidad" style="margin-top: 0px" />
            </div>
             <div class="col-md-2">
                <label for="cmb_empaque" class="fuente_lbl_controles">(*) Unidad de Medida</label>
                <select id="cmb_empaque" class="form-control"></select>
            </div>
             <div class="col-md-3 ">
                <label class="fuente_lbl_controles" for="cmb_ubicacion">(*) Almacen</label>
                <select id="cmb_almacen_ubicacion"></select>
            </div>
            <div class="col-md-3 ">
                <label class="fuente_lbl_controles" for="cmb_ubicacion">(*) Almacen Ubicacion</label>
                <select id="cmb_ubicacion"></select>
            </div>
        </div>
        <div class="row" id="div_generar_lote" style="display:none">
            <div class="col-md-2">
                <label class="fuente_lbl_controles">Generar No. Lote</label>
                <span id="txt_generar_no_lote" class="form-control" disabled="disabled"></span>
            </div>
            <div class="col-md-2">
                <label class="fuente_lbl_controles">(*) No. Lote</label>
                <input type="text" id="txt_no_lote" class="form-control" placeholder="No. Lote"/>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12 ">
                <label for="txt_descripcion" class="fuente_lbl_controles">Descripcion</label>
                <textarea id="txt_descripcion" class="form-control input-sm" rows="5" placeholder="Descripcion" data-parsley-required="true" maxlength="250" style="margin-top: 0px; min-height: 50px !important; resize: none">
                 </textarea>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12 ">
                <label for="txt_nota" class="fuente_lbl_controles">Nota</label>
                <textarea id="txt_nota" class="form-control input-sm" rows="5" placeholder="Nota" data-parsley-required="true" maxlength="250" style="min-height: 50px !important; resize: none">
                 </textarea>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12" align="center" style="margin-top: 15px">
                <button id="btn_producir" type="button" class="btn btn-primary" style="font-size: 20px; font-weight: bold; padding: 15px"><i class="fa fa-send"></i>&nbsp;&nbsp;Guardar Orden De Producción</button>
            </div>
        </div>

    </div>

    <ul id="menu_orden_trabajo" class="dropdown-menu dropdown-blue">
        <li data-item="enviar"><a><i class="glyphicon glyphicon-send"></i>&nbsp;Enviar a Producci&oacute;n</a></li>
        <li data-item="ver"><a><i class="glyphicon glyphicon-cog"></i>&nbsp;Entrar al Panel de Control</a></li>
    </ul>
</asp:Content>
