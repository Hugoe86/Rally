<%@ Page Title="Facturas de proveedor" Language="C#"
    MasterPageFile="~/Paginas/Paginas_Generales/MasterPage.Master"
    AutoEventWireup="true"
    CodeBehind="Frm_Ope_Facturas_Proveedor.aspx.cs"
    Inherits="web_trazabilidad.Paginas.Facturacion.Frm_Ope_Facturas_Proveedor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../Recursos/bootstrap-table/bootstrap-table.min.css" rel="stylesheet" />
    <link href="../../Recursos/estilos/center_loader.css" rel="stylesheet" />
    <link href="../../Recursos/estilos/demo_form.css" rel="stylesheet" />
    <link href="../../Recursos/bootstrap-combo/select2.css" rel="stylesheet" />
    <link href="../../Recursos/bootstrap-date/bootstrap-datetimepicker.css" rel="stylesheet" />

    <script src="../../Recursos/bootstrap-combo/select2.js"></script>
    <script src="../../Recursos/bootstrap-combo/es.js"></script>
    <script src="../../Recursos/bootstrap-date/moment.min.js"></script>
    <script src="../../Recursos/bootstrap-date/bootstrap-datetimepicker.min.js"></script>

    <script src="../../Recursos/bootstrap-table-current/bootstrap-table.js"></script>
    <script src="../../Recursos/bootstrap-table-current/locale/bootstrap-table-es-MX.js"></script>
    <script src="../../Recursos/bootstrap-table-current/extensions/export/bootstrap-table-export.js"></script>
    <script src="../../Recursos/bootstrap-table-current/extensions/export/tableExport.js"></script>
    <script src="../../Recursos/plugins/center-loader.min.js"></script>
    <script src="../../Recursos/plugins/parsley.js"></script>
    <script src="../../Recursos/bootstrap-table/bootstrap-table.min.js"></script>
    <script src="../../Recursos/bootstrap-table/locale/bootstrap-table-es-MX.min.js"></script>
    <script src="../../Recursos/javascript/seguridad/Js_Controlador_Sesion.js"></script>
    <script src="../../Recursos/jquery-numeric/jquery.numeric.js"></script>
    <script src="../../Recursos/javascript/facturacion/Js_Ope_Factura_Proveedores.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid" id="div_scroll" style="height: 100vh;">

        <div class="page-header">
            <h3 style="font-family: 'Roboto Light', cursive !important; font-size: 24px; font-weight: bold; color: #808080;">Factura de Proveedores</h3>
        </div>

        <div class="panel panel-color panel-info" id="panel1">
            <div class="panel-heading">
                <h3 class="panel-title">
                    <i style="color: white;" class="glyphicon glyphicon-filter"></i>&nbsp;Generales Factura
                </h3>
                <div class="panel-options">
                    <a href="#" style="visibility:hidden" id="btn_inicial_factura"><i class="fa fa-chevron-left"></i></a>
                    <a href="#" style="visibility:hidden" id="btn_cancelar_factura"><i class="fa fa-times"></i></a>
                    <a href="#"><i style="color: white;" class="fa fa-floppy-o" id="btn_nuevo"></i></a>
                    <a href="#"><i class="fa fa-search" id="btn_buscar"></i></a>
                </div>
            </div>
            <div class="panel-body">

                <div class="row">
                    <div class="col-md-8">
                        <label class="fuente_lbl_controles">Proveedor</label>
                        <select id="cmb_proveedor" class="form-control"></select>
                    </div>
                    <div class="col-md-2">
                        <label class="fuente_lbl_controles">No. Factura</label>
                        <input type="text" id="txt_no_factura_proveedor" class="form-control" placeholder="No. factura" required />
                        <input type="hidden" id="txt_no_factura" />
                    </div>
                    <div class="col-md-2">
                        <label class="fuente_lbl_controles">Estatus</label>
                        <select id="cmb_estatus" class="form-control" required>
                            <option value="">Seleccione</option>
                            <option value="POR PAGAR">Por pagar</option>
                            <option value="PAGADA">Pagada</option>
                            <option value="CANCELADA">Cancelada</option>
                        </select>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-3">
                        <label class="fuente_lbl_controles">Tipo de Factura</label>
                        <select id="cmb_tipo_factura" class="form-control" required>
                            <option value="">Seleccione</option>
                            <option value="NORMAL">Normal</option>
                            <option value="HONORARIOS">Honorarios</option>
                            <option value="FLETE">Flete</option>
                            <option value="ARRENDAMIENTO">Arrendamiento</option>
                            <option value="SIN IVA">Sin IVA</option>
                        </select>
                    </div>
                    <div class="col-md-3">
                        <label class="fuente_lbl_controles">Tipo de movimiento</label>
                        <select id="cmb_tipo_movimiento" class="form-control" required>
                        </select>
                    </div>
                    <div class="col-md-2">
                        <label class="fuente_lbl_controles">Moneda</label>
                        <select id="cmb_moneda" class="form-control" required>
                            <option value="">Seleccione</option>
                            <option value="PESOS">Pesos</option>
                            <option value="DOLARES">Dolares</option>
                        </select>
                    </div>
                    <div class="col-md-2">
                        <label class="fuente_lbl_controles" style="margin-bottom: 8px;">Fecha Factura</label>
                        <div class="input-group date" id="dtp_fecha_factura">
                            <input type="text" id="ff" class="form-control" style="margin: 0px;" placeholder="dd/mm/aaaa" required />
                            <span class="input-group-addon">
                                <span class="glyphicon glyphicon-calendar"></span>
                            </span>
                        </div>
                    </div>
                    <div class="col-md-2">
                        <label class="fuente_lbl_controles" style="margin-bottom: 8px;">Fecha Recepción</label>
                        <div class="input-group date" id="dtp_fecha_recepcion">
                            <input type="text" id="fr" class="form-control" style="margin: 0px;" placeholder="dd/mm/aaaa" required />
                            <span class="input-group-addon">
                                <span class="glyphicon glyphicon-calendar"></span>
                            </span>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-5" style="margin-top: 50px;">
                        <div class="panel-heading">
                            <h3 class="panel-title">
                                <i style="color: white;" class="glyphicon glyphicon-filter"></i>&nbsp;Generales Factura
                            </h3>
                        </div>

                        <div class="row">
                            <div class="col-md-12">
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
                                        <label class="fuente_lbl_controles">IVA</label>
                                    </div>
                                    <div class="col-md-9">
                                        <input type="text" id="txt_iva" class="form-control" placeholder="IVA" required />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-3">
                                        <label class="fuente_lbl_controles">Retención IVA</label>
                                    </div>
                                    <div class="col-md-9">
                                        <input type="text" id="txt_retencion_iva" class="form-control" placeholder="Retención IVA" required />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-3">
                                        <label class="fuente_lbl_controles">Retención ISR</label>
                                    </div>
                                    <div class="col-md-9">
                                        <input type="text" id="txt_retencion_isr" class="form-control" placeholder="Retención ISR" required />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-3">
                                        <label class="fuente_lbl_controles">Retención fletes</label>
                                    </div>
                                    <div class="col-md-9">
                                        <input type="text" id="txt_retencion_flete" class="form-control" placeholder="Retención fletes" required />
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
                                <div class="row">
                                    <div class="col-md-3">
                                        <label class="fuente_lbl_controles">Saldo</label>
                                    </div>
                                    <div class="col-md-9">
                                        <input type="text" id="txt_saldo" class="form-control" placeholder="Saldo" required />
                                    </div>
                                </div>
                                <div class="row" style="display:none;">
                                    <div class="col-md-12">
                                        <input type="text" id="txt_iva_db" class="form-control" placeholder="iva"/>
                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>

                    <div class="col-md-2">
                    </div>

                    <div class="col-md-5" style="margin-top: 50px;">
                        <div class="panel-heading">
                            <h3 class="panel-title">
                                <i style="color: white;" class="glyphicon glyphicon-filter"></i>&nbsp;Condiciones de pago
                            </h3>
                        </div>

                        <div class="row">
                            <div class="col-md-4">
                                <label class="fuente_lbl_controles">Forma de pago</label>
                            </div>
                            <div class="col-md-8">
                                <select id="cmb_forma_de_pago" class="form-control" required>
                                </select>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-4">
                                <label class="fuente_lbl_controles" style="margin-bottom: 8px;">Fecha de pago</label>
                            </div>
                            <div class="col-md-8">
                                <div class="input-group date" id="dtp_fecha_de_pago">
                                    <input type="text" id="fp" class="form-control" style="margin: 0px;" placeholder="dd/mm/aaaa" required />
                                    <span class="input-group-addon">
                                        <span class="glyphicon glyphicon-calendar"></span>
                                    </span>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-4">
                                <label class="fuente_lbl_controles">Comentarios</label>
                            </div>
                            <div class="col-md-8">
                                <textarea id="txt_comentarios" class="form-control" placeholder="Comentarios"></textarea>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-7">
                                <div id="sumary_error" class="alert alert-danger text-left" style="width: 277.78px !important; display: none;">
                                    <label id="lbl_msg_error" />
                                </div>

                            </div>
                        </div>

                    </div>
                </div>

            </div>
        </div>

    </div>
</asp:Content>
