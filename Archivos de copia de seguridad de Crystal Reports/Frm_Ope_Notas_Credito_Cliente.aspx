<%@ Page Title="Notas de crédito" Language="C#" MasterPageFile="~/Paginas/Paginas_Generales/MasterPage.Master" AutoEventWireup="true" CodeBehind="Frm_Ope_Notas_Credito_Cliente.aspx.cs" Inherits="web_trazabilidad.Paginas.Facturacion.Frm_Ope_Nota_Credito" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../Recursos/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../../Recursos/estilos/center_loader.css" rel="stylesheet" />
    <link href="../../Recursos/estilos/demo_form.css" rel="stylesheet" />
    <link href="../../Recursos/bootstrap-combo/select2.css" rel="stylesheet" />
    <link href="../../Recursos/bootstrap-table-current/bootstrap-table.css" rel="stylesheet" />
    <link href="../../Recursos/bootstrap-date/bootstrap-datetimepicker.css" rel="stylesheet" />
    <link href="../../Recursos/bootstrap-table/extensions/editable/bootstrap-editable.css" rel="stylesheet" />

    <script src="../../Recursos/bootstrap-combo/select2.js"></script>
    <script src="../../Recursos/bootstrap-combo/es.js"></script>
    <script src="../../Recursos/bootstrap-date/moment.min.js"></script>
    <script src="../../Recursos/bootstrap-date/bootstrap-datetimepicker.min.js"></script>

    <script src="../../Recursos/bootstrap-table-current/bootstrap-table.js"></script>
    <script src="../../Recursos/bootstrap-table-current/locale/bootstrap-table-es-MX.js"></script>
    <script src="../../Recursos/bootstrap-table/extensions/editable/bootstrap-editable.js"></script>
    <script src="../../Recursos/bootstrap-table-current/extensions/editable/bootstrap-table-editable.min.js"></script>
    <script src="../../Recursos/bootstrap-table-current/extensions/export/bootstrap-table-export.js"></script>
    <script src="../../Recursos/bootstrap-table-current/extensions/export/tableExport.js"></script>
    <script src="../../Recursos/plugins/center-loader.min.js"></script>
    <script src="../../Recursos/plugins/parsley.js"></script>
    <script src="../../Recursos/javascript/seguridad/Js_Controlador_Sesion.js"></script>
    <script src="../../Recursos/bootstrap-box/bootstrap-number-input.js"></script>
    <script src="../../Recursos/jquery-numeric/jquery.numeric.js"></script>
    <script src="../../Recursos/javascript/facturacion/Js_Ope_Notas_Credito_Cliente.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid" id="div_scroll" style="height: 100vh;">

        <div class="page-header">
            <h3 style="font-family: 'Roboto Light', cursive !important; font-size: 24px; font-weight: bold; color: #808080;">Notas de crédito</h3>
        </div>

        <div class="panel panel-color panel-info" id="panel1">
            <div class="panel-heading">
                <h3 class="panel-title">
                    <i style="color: white;" class="fa fa-address-card-o"></i>&nbsp;Datos de la nota de crédito
                </h3>
                <div class="panel-options">
                    <a href="#" style="visibility:hidden" id="btn_inicial_nota"><i class="fa fa-chevron-left"></i></a>
                    <a href="#" style="visibility:hidden" id="btn_enviar_nota_credito"><i class="fa fa-envelope"></i></a>
                    <a href="#" style="visibility:hidden" id="btn_mostrar_xml"><i class="fa fa-file-code-o"></i></a>
                    <a href="#" style="visibility:hidden" id="btn_mostrar_pdf"><i class="fa fa-file-pdf-o"></i></a>
                    <a href="#"><i style="color: white;" class="fa fa-floppy-o" id="btn_nuevo"></i></a>
                    <a href="#"><i class="fa fa-search" id="btn_buscar"></i></a>
                    <a href="#" style="visibility: hidden" id="btn_cancelar_nota"><i class="fa fa-times"></i></a>
                </div>
            </div>
            <div class="panel-body" id="panel_body">

                <div class="row">
                    <div class="col-md-2">
                        <label class="fuente_lbl_controles">No. nota credito</label>
                        <div class="input-group">
                            <input type="number" id="txt_no_nota_credito" name="txt_no_nota_credito" required="required" class="form-control" required/>
                            <span class="input-group-addon" style="width: 0px; padding-left: 0px; padding-right: 0px; border: none;"></span>
                            <select id="cmb_serie" name="cmb_serie" class="form-control" style="width: 73px;" required>
                            </select>
                        </div>
                    </div>
                    <div class="col-md-2">
                        <label class="fuente_lbl_controles">Estatus</label>
                        <select id="cmb_estatus" class="form-control" required>
                            <option value="APLICADA">Aplicada</option>
                        </select>
                    </div>
                    <div class="col-md-2">
                        <label class="fuente_lbl_controles" style="margin-bottom: 8px;">Fecha de emisión</label>
                        <div class="input-group date" id="dtp_fecha_emision">
                            <input type="text" id="txt_fecha_emision" class="form-control" style="margin: 0px;" placeholder="dd/mm/aaaa" required />
                            <span class="input-group-addon">
                                <span class="glyphicon glyphicon-calendar"></span>
                            </span>
                        </div>
                    </div>
                    <div class="col-md-2">
                        <label class="fuente_lbl_controles">Tipo de nota</label>
                        <select id="cmb_tipo_nota" class="form-control" required>
                            <option value="DEVOLUCION">Devolucion</option>
                            <option value="DESCUENTO">Descuento</option>
                            <option value="PRONTO PAGO">Pronto pago</option>
                        </select>
                    </div>
                    <div class="col-md-2">
                        <label class="fuente_lbl_controles">Moneda</label>
                        <select id="cmb_moneda" class="form-control" required>
                            <option value="PESOS">Pesos</option>
                            <option value="DOLARES">Dolares</option>
                        </select>
                    </div>
                    <div class="col-md-2">
                        <label class="fuente_lbl_controles">Tipo de cambio</label>
                        <input type="text" id="txt_tipo_cambio" class="form-control" placeholder="Tipo de cambio" />
                    </div>
                </div>


                <div class="row">
                    <div class="col-md-12" style="margin-top: 50px;">
                        <div class="panel-heading">
                            <h3 class="panel-title">
                                <i style="color: white;" class="fa fa-address-card-o"></i>&nbsp;Datos de cliente
                            </h3>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <label class="fuente_lbl_controles">Cliente</label>
                        <select id="cmb_cliente" class="form-control"></select>
                    </div>
                    <div class="col-md-2">
                        <label class="fuente_lbl_controles">RFC</label>
                        <input type="text" id="txt_rfc" class="form-control" placeholder="RFC" />
                    </div>
                    <div class="col-md-2">
                        <label class="fuente_lbl_controles">No. Exterior</label>
                        <input type="text" id="txt_no_exterior" class="form-control" placeholder="No. Exterior" />
                    </div>
                    <div class="col-md-2">
                        <label class="fuente_lbl_controles">No. Interior</label>
                        <input type="text" id="txt_no_interior" class="form-control" placeholder="No. Interior" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <label class="fuente_lbl_controles">Calle</label>
                        <input type="text" id="txt_calle" class="form-control" placeholder="Calle" />
                    </div>
                    <div class="col-md-3">
                        <label class="fuente_lbl_controles">Colonia</label>
                        <input type="text" id="txt_colonia" class="form-control" placeholder="Colonia" />
                    </div>
                    <div class="col-md-2">
                        <label class="fuente_lbl_controles">Código Postal</label>
                        <input type="text" id="txt_codigo_postal" class="form-control" placeholder="Código Postal" />
                    </div>
                    <div class="col-md-4">
                        <label class="fuente_lbl_controles">Localidad</label>
                        <input type="text" id="txt_localidad" class="form-control" placeholder="Localidad" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <label class="fuente_lbl_controles">Ciudad</label>
                        <input type="text" id="txt_ciudad" class="form-control" placeholder="Ciudad" />
                    </div>
                    <div class="col-md-3">
                        <label class="fuente_lbl_controles">Estado</label>
                        <input type="text" id="txt_estado" class="form-control" placeholder="Estado" />
                    </div>
                    <div class="col-md-6">
                        <label class="fuente_lbl_controles">País</label>
                        <input type="text" id="txt_pais" class="form-control" placeholder="País" />
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-12" style="margin-top: 50px;">
                        <div class="panel-heading">
                            <h3 class="panel-title">
                                <i style="color: white;" class="fa fa-address-card-o"></i>&nbsp;Búsqueda de factura
                            </h3>
                        </div>
                    </div>
                </div>

                <%--inicio panel opcion devolucion--%>
                <div id="div_opcion_devolucion">
                    <div class="row">
                        <div class="col-md-12">
                            <table class="table table-responsive" id="tbl_detalles_factura"></table>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12" style="margin-top: 50px;">
                            <div class="panel-heading">
                                <h3 class="panel-title">
                                    <i style="color: white;" class="fa fa-address-card-o"></i>&nbsp;Partidas de la factura
                                </h3>
                            </div>
                        </div>
                    </div>
                    <div id="toolbar_partidas_factura" style="margin-left: 5px; margin-top: 9px;">
                        <div class="row">
                            <div class="col-md-6" style="margin-top: 9px;">
                                <label class="fuente_lbl_controles">Tipo de devolución</label>
                            </div>
                            <div class="col-md-6">
                                <select id="cmb_devolucion" class="form-control" required>
                                    <option value="UNIDAD">Unidad</option>
                                    <option value="MONTO">Monto</option>
                                </select>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <table class="table table-responsive" data-toolbar="#toolbar_partidas_factura" id="tbl_partidas_factura"></table>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12" style="margin-top: 50px;">
                            <div class="panel-heading">
                                <h3 class="panel-title">
                                    <i style="color: white;" class="fa fa-address-card-o"></i>&nbsp;Partidas a devolver
                                </h3>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <table class="table table-responsive" id="tbl_partidas_devolver"></table>
                        </div>
                    </div>
                </div>
                <%--fin panel opcion devolucion--%>

                <%--fin panel opcion descuento_o_pronto_pago--%>
                <div id="div_opcion_descuento_o_pronto_pago" style="display: none">
                    <div class="row">
                        <div class="col-md-12">
                            <table class="table table-responsive" id="tbl_facturas_checkbox"></table>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12" style="margin-top: 50px;">
                            <div class="panel-heading">
                                <h3 class="panel-title">
                                    <i style="color: white;" class="fa fa-address-card-o"></i>&nbsp;Facturas seleccionadas
                                </h3>
                            </div>
                        </div>
                    </div>
                    <div id="toolbar_fac_seleccionadas" style="margin-left: 5px; margin-top: 9px;">
                        <div class="row">
                            <div class="col-md-4" style="margin-top: 9px;">
                                <label class="fuente_lbl_controles">Descuento %</label>
                            </div>
                            <div class="col-md-8">
                                <input type="text" id="txt_descuento_seleccionadas" class="form-control" placeholder="Descuento %" required />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <table class="table table-responsive" data-toolbar="#toolbar_fac_seleccionadas" id="tbl_facturas_seleccionadas"></table>
                        </div>
                    </div>
                </div>
                <%--fin panel opcion descuento_o_pronto_pago--%>
                <div class="row">
                    <div class="col-md-7">
                        <div class="row">
                            <div class="col-md-12">
                                <label class="fuente_lbl_controles">Comentarios</label>
                                <textarea id="txt_comentarios" name="txt_comentarios" class="form-control input-sm" rows="10" placeholder="Comentarios" data-parsley-required="true" maxlength="250" style="min-height: 50px !important;"></textarea>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-7">
                                <div id="sumary_error_general" class="alert alert-danger text-left" style="width: 300.78px !important; display: none;">
                                    <label id="lbl_msg_error_general" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-5">
                        <div class="row">
                            <div class="col-md-3">
                                <label class="fuente_lbl_controles">Subtotal</label>
                            </div>
                            <div class="col-md-9">
                                <input type="text" id="txt_subtotal" class="form-control" placeholder="Subtotal" required />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3">
                                <label class="fuente_lbl_controles">Subtotal %</label>
                            </div>
                            <div class="col-md-9">
                                <input type="text" id="txt_subtotal_porcentaje" class="form-control" placeholder="Subtotal %" required />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3">
                                <label class="fuente_lbl_controles">Impuesto</label>
                            </div>
                            <div class="col-md-9">
                                <input type="text" id="txt_impuesto" class="form-control" placeholder="Impuesto" required />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3">
                                <label class="fuente_lbl_controles">Total</label>
                            </div>
                            <div class="col-md-9">
                                <input type="text" id="txt_total" class="form-control" placeholder="Total" required />
                            </div>
                        </div>
                    </div>
                </div>

            </div>
        </div>

    </div>
</asp:Content>
