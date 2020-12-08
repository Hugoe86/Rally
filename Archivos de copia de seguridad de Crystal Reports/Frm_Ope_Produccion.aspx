<%@ Page Title="" Language="C#" MasterPageFile="~/Paginas/Paginas_Generales/MasterPage.Master" AutoEventWireup="true" CodeBehind="Frm_Ope_Produccion.aspx.cs" Inherits="web_trazabilidad.Paginas.Trazabilidad.Frm_Ope_Produccion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--Hojas de estilo --%>
    <link href="../../Recursos/bootstrap/css/bootstrap.css" rel="stylesheet" />
    <link href="../../Recursos/estilos/demo_form.css" rel="stylesheet" />
    <link href="../../Recursos/bootstrap-combo/select2.css" rel="stylesheet" />
    <link href="../../Recursos/bootstrap-combo/select2-bootstrap.css" rel="stylesheet" />
    <link href="../../Recursos/bootstrap-table/bootstrap-table.css" rel="stylesheet" />
    <link href="../../Recursos/bootstrap-table/extensions/editable/bootstrap-editable.css" rel="stylesheet" />
    <link href="../../Recursos/bootstrap-date/bootstrap-datetimepicker.css" rel="stylesheet" />
    <link href="../../Recursos/EasyUI 1.3.6/themes/default/easyui.css" rel="stylesheet" />
    <link href="../../Recursos/EasyUI%201.3.6/themes/icon.css" rel="stylesheet" />
    <link href="../../Recursos/estilos/isotope.css" rel="stylesheet" />
    <link href="../../Recursos/estilos/css_orden_trabajo.css" rel="stylesheet" />
    <link href="../../Recursos/estilos/css_orden_trabajo_produccion.css" rel="stylesheet" />

    <%--Javascript --%>
    <script src="../../Recursos/bootstrap-combo/select2.js"></script>
    <script src="../../Recursos/bootstrap-combo/es.js"></script>
    <script src="../../Recursos/bootstrap-table/bootstrap-table.min.js"></script>
    <script src="../../Recursos/bootstrap-table/locale/bootstrap-table-es-MX.min.js"></script>
    <script src="../../Recursos/bootstrap-table-current/bootstrap-table.js"></script>
    <script src="../../Recursos/bootstrap-table-current/locale/bootstrap-table-es-MX.js"></script>
    <script src="../../Recursos/bootstrap-table/extensions/editable/bootstrap-editable.js"></script>
    <script src="../../Recursos/bootstrap-table-current/extensions/editable/bootstrap-table-editable.min.js"></script>
    <script src="../../Recursos/bootstrap-date/moment.min.js"></script>
    <script src="../../Recursos/bootstrap-date/bootstrap-datetimepicker.min.js"></script>
    <script src="../../Recursos/plugins/jquery.formatCurrency-1.4.0.min.js"></script>
    <script src="../../Recursos/plugins/jquery.formatCurrency.all.js"></script>
    <script src="../../Recursos/plugins/accounting.min.js"></script>
    <script src="../../Recursos/plugins/pinch.js"></script>
    <script src="../../Recursos/plugins/parsley.js"></script>
    <script src="../../Recursos/EasyUI%201.3.6/jquery.easyui.min.js"></script>
    <script src="../../Recursos/javascript/seguridad/Js_Controlador_Sesion.js"></script>
    <script src="../../Recursos/plugins/URI.min.js"></script>
    <script src="../../Recursos/plugins/isotope/isotope.pkgd.min.js"></script>
    <script src="../../Recursos/javascript/trazabilidad/Js_Ope_Produccion.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid" style="height: 100vh;">

        <div id="div_orden_trabajo" class="panel panel-color panel-info collapsed">
            <div class="panel-heading">
                <h3 class="panel-title">
                    <i style="color: white;" class="glyphicon glyphicon-list-alt"></i>&nbsp;<label id="lbl_titulo_panel"></label>
                </h3>
                <div class="panel-options">
                    <a href="#" id="btn_inicio" title="Salir">
                        <span class="fa fa-home" style="font-size: 16px; color: #fff;"></span>
                    </a>
                    <a href="#" data-toggle="panel">
                        <span class="collapse-icon">-</span>
                        <span class="expand-icon">+</span>
                    </a>
                </div>
            </div>

            <div class="panel-body">
                <div class="row">
                    <div class="col-md-2 etq">
                        <label>No Orden</label>
                    </div>
                    <div class="col-md-2 ctrl">
                        <label id="lbl_no_orden_trabajo"></label>
                    </div>
                    <div class="col-md-2 etq">
                        <label>Prioridad</label>
                    </div>
                    <div class="col-md-2 ctrl">
                        <label id="lbl_prioridad"></label>
                    </div>
                    <div class="col-md-2 etq">
                        <label>Estatus</label>
                    </div>
                    <div class="col-md-2 ctrl ctrl-right">
                        <label id="lbl_estatus"></label>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-2 etq">
                        <label>Cantidad Producir</label>
                    </div>
                    <div class="col-md-2 ctrl">
                        <label id="lbl_cantidad_producir"></label>
                    </div>
                    <div class="col-md-2 etq">
                        <label>Cantidad Producida</label>
                    </div>
                    <div class="col-md-2 ctrl">
                        <label id="lbl_cantidad_producida"></label>
                    </div>
                    <div class="col-md-2 etq">
                        <label>Faltante</label>
                    </div>
                    <div class="col-md-2 ctrl ctrl-right">
                        <label id="lbl_faltante"></label>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-2 etq">
                        <label>Descripci&oacute;n</label>
                    </div>
                    <div class="col-md-10 ctrl ctrl-right">
                        <label id="lbl_descripcion"></label>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-2 etq">
                        <label>Nota</label>
                    </div>
                    <div class="col-md-10 ctrl ctrl-right">
                        <label id="lbl_nota"></label>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-2 etq">
                        <label>Producto</label>
                    </div>
                    <div class="col-md-10 ctrl ctrl-right">
                        <label id="lbl_producto"></label>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-2 etq">
                        <label>Unidad de Medida</label>
                    </div>
                    <div class="col-md-10 ctrl ctrl-right">
                        <label id="lbl_empaque"></label>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-2 etq etq-bottom">
                        <label>Ubicaci&oacute;n</label>
                    </div>
                    <div class="col-md-10 ctrl ctrl-right ctrl-bottom">
                        <label id="lbl_ubicacion"></label>
                    </div>
                </div>
            </div>

        </div>
        <div id="div_principal">
            <div class="row">
                <div class="col-md-12">
                    <button id="btn_cambiar_bom" class="btn btn-info btn-sm" value=""><i class ="fa fa-edit"></i>&nbsp;Actualizar Lista O.P.</button>
                </div>
            </div>
            <div id="contenedor_isotope"></div>

            <br />

            <div class="row">
                <div class="col-md-4">
                    <table id="grid_series" class="table table-responsive"></table>
                </div>
                <div class="col-md-8" align="left">
                    <div class="panel panel-color panel-info" style="border: solid 0.1px #ccc;">
                        <div class="panel-heading">
                            <h3 class="panel-title">
                                <i style="color: white; font-size: 16px;" class="fa fa-wrench"></i>&nbsp;<label id="lbl_titulo_modal"></label>
                            </h3>
                            <div class="panel-options">
                                <a href="#" data-toggle="panel">
                                    <span class="collapse-icon">-</span>
                                    <span class="expand-icon">+</span>
                                </a>
                            </div>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-sm-10 col-sm-offset-1 etq" align="left">
                                    <label id="lbl_validaciones_produccion" class="fuente_lbl_controles"></label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-3 col-sm-offset-1 etq" align="left">
                                    <label class="fuente_lbl_controles">Cantidad Producida</label>
                                </div>
                                <div class="col-sm-4 ctrl ctrl-right ctrl-bottom" align="left">
                                    <label id="lbl_cant_producida" class="fuente_lbl_controles"></label>
                                </div>
                                <div class="col-sm-3 ctrl ctrl-right ctrl-bottom">
                                    <input type="checkbox" id="chk_completar_orden" title="Completar orden" /><label class="fuente_lbl_controles">Completar Orden.</label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-3 col-sm-offset-1 etq" align="left">
                                    <label class="fuente_lbl_controles">Cantidad a Generar</label>
                                </div>
                                <div class="col-sm-7 ctrl ctrl-right ctrl-bottom" align="left">
                                    <input type="text" id="txt_cantidad_producir" class="form-control" style="height: 25px !important; margin: 2px; font-size: 10px;" />
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-sm-3 col-sm-offset-1 etq etq-bottom" align="left">
                                    <label class="fuente_lbl_controles" id="lbl_stock"></label>
                                </div>
                                <div class="col-sm-5 ctrl ctrl-right" align="left">
                                    <input type="text" id="txt_no_serie_lote_producto" maxlength="100" class="form-control" style="height: 25px !important; margin: 2px; font-size: 10px;" />
                                </div>
                                <div class="col-sm-2 ctrl ctrl-right ctrl-bottom" align="right" valign="top">
                                    <button id="btn_asignar_serial" type="button" class="btn btn-primary btn-sm" style="border-radius: 0px !important; border-top-left-radius: 6px !important; border-top-right-radius: 6px !important; border-bottom-left-radius: 6px !important; border-bottom-right-radius: 6px !important;">Agregar</button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div id="div_actualizar_op_detalles" style="display: none;">

            <div class="row" style="margin-top: 5px">
                <div class="col-md-12" style="text-align: right;">
                    <button id="btn_guardar_op" class="btn btn-success"><i class="fa fa-save"></i>&nbsp;Guardar</button>

                    <button id="btn_regresar_op" class="btn btn-danger"><i class="fa fa-arrow-circle-left"></i>&nbsp;Regresar</button>
                </div>
            </div>

            <div class="row" style="margin-top: 5px">
                <div class="col-md-3">
                    <label class="fuente_lbl_controles">Cantidad a Producir</label>
                    <input type="text" id="txt_cant_producir" class="form-control" disabled="disabled" />
                </div>
                <div class="col-md-3">
                    <label class="fuente_lbl_controles">Cantidad Producida</label>
                    <input type="text" id="txt_cant_producida" class="form-control" />
                </div>
<%--                <div class="col-md-3">
                    <label class="fuente_lbl_controles">Faltante</label>
                    <input type="text" id="txt_cant_diferencia" class="form-control" disabled="disabled" />
                </div>--%>
            </div>

            <div class="row" style="margin-top: 5px">
                <div class="col-md-12">
                    <table id="tbl_lst_op_detalles" class="table table-responsive"></table>
                </div>
            </div>

        </div>
    </div>


    <div class="modal fade" id="mdl_bom" name="mdl_bom" role="dialog" aria-hidden="true" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog" style="width: 80% !important">
            <div class="modal-content">

                <div class="modal-header">
                    <button type="button" class="close cancelar" data-dismiss="modal" aria-label="Close" onclick="_set_close_modal(true);"><i class="fa fa-times"></i></button>
                    <h4 class="modal-title" id="myModalLabel">
                        <label id="lbl_title"></label>
                    </h4>
                </div>

                <div class="modal-body">
                    <div class="row" style="margin-top: 8px !important">
                        <div class="col-md-12">
                            <table id="tbl_bom_detalles" class="table table-responsive"></table>
                        </div>
                    </div>
                    <div class="row" style="margin-top: 8px !important">
                        <div class="col-md-12">
                            <label class="text-bold" id="lbl_Producto">Producto</label>
                        </div>
                    </div>
                    <div class="row" style="margin-top: 8px !important">
                        <div class="col-md-2">
                            <label class="fuente_lbl_controles">Cantidad Producir</label>
                            <input type="text" id="txt_cantidad_producir_" class="form-control" disabled="disabled" />
                        </div>
                        <div class="col-md-2">
                            <label class="fuente_lbl_controles">Cantidad Seleccionada</label>
                            <input type="text" id="txt_cantidad_seleccionada" class="form-control" disabled="disabled" />
                        </div>
                        <div class="col-md-2">
                            <label class="fuente_lbl_controles">Diferencia</label>
                            <input type="text" id="txt_diferencia" class="form-control disabled" disabled="disabled" />
                        </div>
                    </div>

                    <div class="row" style="margin-top: 8px !important">
                        <div class="col-md-12">
                            <table id="tbl_bom_detalles_cdis" class="table table-responsive"></table>
                        </div>
                    </div>
                </div>

                <div class="modal-footer">
                    <div class="row">
                        <div class="col-md-7">
                            <div id="sumary_error" class="alert alert-danger text-left" style="width: 277.78px !important; display: none;">
                                <label id="lbl_msg_error" />
                            </div>
                        </div>
                        <div class="col-md-5">
                            <div class="form-inline">
                                <button type="submit" class="btn btn-info btn-icon btn-icon-standalone btn-xs" id="btn_guardar_bom" title="Guardar"><i class="fa fa-check"></i><span>Aceptar</span></button>
                                <button type="button" class="btn btn-danger btn-icon btn-icon-standalone btn-xs cancelar" data-dismiss="modal" id="btn_cancel_series" aria-label="Close" onclick="_set_close_modal(true);" title="Cancelar operaci&oacute;n"><i class="fa fa-remove"></i><span>Cancelar</span></button>
                            </div>
                        </div>
                    </div>
                </div>

            </div>
        </div>
    </div>
</asp:Content>
