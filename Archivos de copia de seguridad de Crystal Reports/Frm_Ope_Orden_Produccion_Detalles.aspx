<%@ Page Title="" Language="C#" MasterPageFile="~/Paginas/Paginas_Generales/MasterPage.Master" AutoEventWireup="true" CodeBehind="Frm_Ope_Orden_Produccion_Detalles.aspx.cs" Inherits="web_trazabilidad.Paginas.Trazabilidad.Frm_Ope_Orden_Produccion_Detalles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../Recursos/bootstrap/css/bootstrap.css" rel="stylesheet" />
    <%--Hojas de estilo --%>
    <link href="../../Recursos/estilos/demo_form.css" rel="stylesheet" />
    <link href="../../Recursos/bootstrap-combo/select2.css" rel="stylesheet" />
    <link href="../../Recursos/bootstrap-combo/select2-bootstrap.css" rel="stylesheet" />
    <link href="../../Recursos/bootstrap-table/bootstrap-table.css" rel="stylesheet" />
    <link href="../../Recursos/bootstrap-table/extensions/editable/bootstrap-editable.css" rel="stylesheet" />
    <link href="../../Recursos/bootstrap-date/bootstrap-datetimepicker.css" rel="stylesheet" />
    <link href="../../Recursos/EasyUI 1.3.6/themes/default/easyui.css" rel="stylesheet" />
    <link href="../../Recursos/EasyUI%201.3.6/themes/icon.css" rel="stylesheet" />

    <link href="../../Recursos/estilos/isotope.css" rel="stylesheet" />
    <link href="../../Recursos/estilos/css_orden_produccion_detalles.css" rel="stylesheet" />
    <%--Javascript --%>
    <script src="../../Recursos/bootstrap-combo/select2.js"></script>
    <script src="../../Recursos/bootstrap-combo/es.js"></script>
    <script src="../../Recursos/bootstrap-table/bootstrap-table.min.js"></script>
    <script src="../../Recursos/bootstrap-table/locale/bootstrap-table-es-MX.min.js"></script>
    <script src="../../Recursos/bootstrap-table/extensions/editable/bootstrap-editable.js"></script>
    <script src="../../Recursos/bootstrap-table/extensions/editable/bootstrap-table-editable.min.js"></script>
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
    <script src="../../Recursos/javascript/trazabilidad/Js_Ope_Ordenes_Produccion_Detalles.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid" style="height: 100vh;">
        <input type="hidden" id="job" />
        <div id="div_orden_trabajo_detalles">
            <div class="panel-body">
                <div class="row" style="margin-left: 15px !important;">
                    <div class="col-md-10 col-md-offset-1 text-center control-panel">
                        <h3>Panel de Control</h3>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-12">
                        <div class="row" style="margin-left: 15px !important;">
                            <div class="col-md-3 col-md-offset-1 item-ct">
                                <div class="row">
                                    <div class="col-md-12 head-ct" align="center">
                                        Centro de Trabajo
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12 body-ct">
                                        <i class="fa fa-map-marker" style="font-size: 16px;"></i>&nbsp;
                                        <label id="lbl_centro_trabajo_op"></label>
                                    </div>
                                </div>
                                <div class="row" style="display: none">
                                    <div class="col-md-12 footer-ct" align="center">
                                        <select id="cmb_ubicacion_busqueda" class="form-control" style="width: 100% !important;"></select>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-4 item-job">
                                <div class="row">
                                    <div class="col-md-12 head-job" align="center">
                                        Orden de Trabajo
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12 body-job">
                                        No. Parte&nbsp;<label id="lbl_no_orden_trabajo_detalle"></label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12 footer-job" align="center">
                                        <a href="#" id="btn_job" class="ir_job"><i class="fa fa-sign-in"></i>&nbsp;<label id="lbl_titulo"></label></a>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-3 item-status">
                                <div class="row">
                                    <div class="col-md-12 head-status" align="center">
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12 body-status">
                                        <label id="lbl_no_orden_trabajo_estatus"></label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12 footer-status" align="center">
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-10 col-md-offset-1">
                        <div class="row" style="margin-left: 15px !important;">
                            <div class="col-md-2 etq">
                                <label>No Orden</label>
                            </div>
                            <div class="col-md-2 ctrl">
                                <a href="#" class="ir_job">
                                    <i class="fa fa-link"></i>&nbsp;<label id="lbl_no_orden_trabajo"></label>
                                </a>
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

                        <div class="row" style="margin-left: 15px !important;">
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

                        <div class="row" style="margin-left: 15px !important;">
                            <div class="col-md-2 etq">
                                <label>Descripci&oacute;n</label>
                            </div>
                            <div class="col-md-10 ctrl ctrl-right">
                                <label id="lbl_descripcion"></label>
                            </div>
                        </div>

                        <div class="row" style="margin-left: 15px !important;">
                            <div class="col-md-2 etq">
                                <label>Nota</label>
                            </div>
                            <div class="col-md-10 ctrl ctrl-right">
                                <label id="lbl_nota"></label>
                            </div>
                        </div>

                        <div class="row" style="margin-left: 15px !important;">
                            <div class="col-md-2 etq">
                                <label>Producto</label>
                            </div>
                            <div class="col-md-10 ctrl ctrl-right">
                                <label id="lbl_producto"></label>
                            </div>
                        </div>

                        <div class="row" style="margin-left: 15px !important;">
                            <div class="col-md-2 etq">
                                <label>Unidad de Medida</label>
                            </div>
                            <div class="col-md-10 ctrl ctrl-right">
                                <label id="lbl_empaque"></label>
                            </div>
                        </div>

                        <div class="row" style="margin-left: 15px !important;">
                            <div class="col-md-2 etq etq-bottom">
                                <label>Ubicaci&oacute;n</label>
                            </div>
                            <div class="col-md-10 ctrl ctrl-right ctrl-bottom">
                                <label id="lbl_ubicacion"></label>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-12">
                        <div class="row" style="margin-left: 15px !important;">
                            <div class="col-md-3 col-md-offset-1 item-user">
                                <div class="row">
                                    <div class="col-md-12 head-user" align="center">
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12 body-user">
                                        <i class="fa fa-user" style="font-weight: 600; font-size: 16px;"></i>&nbsp;&nbsp;<label id="lbl_orden_trabajo_usuario"></label>

                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12 footer-user" align="center">
                                        <i class="fa fa-clock-o" style="font-weight: 600; font-size: 16px;"></i>&nbsp;&nbsp;<label id="lbl_orden_trabajo_fecha"></label>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-4 item-footer-2">
                                <div class="row">
                                    <div class="col-md-12 head-footer-2" align="center">
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12 body-footer-2">
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12 footer-footer-2" align="center">
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-3 item-footer-3">
                                <div class="row">
                                    <div class="col-md-12 head-footer-3" align="center">
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12 body-footer-3">
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12 footer-footer-3" align="center">
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
