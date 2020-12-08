<%@ Page Title="Notas Credito Proveedor" MasterPageFile="~/Paginas/Paginas_Generales/MasterPage.Master" Language="C#" AutoEventWireup="true" CodeBehind="Frm_Ope_Notas_Credito_Proveedor.aspx.cs" Inherits="web_trazabilidad.Paginas.Facturacion.Frm_Ope_Notas_Credito_Proveedor" %>

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
    <script src="../../Recursos/javascript/facturacion/Js_Ope_Notas_Credito_Proveedor.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid" style="height: 100vh;">

        <div class="page-header">
            <h3 style="font-family: 'Roboto Light', cursive !important; font-size: 24px; font-weight: bold; color: #808080;">Notas de crédito Proveedor</h3>
        </div>

        <div class="panel panel-color panel-info" id="panel1">
            <div class="panel-heading">
                <h3 class="panel-title">
                    <i style="color: white;" class="fa fa-address-card-o"></i>&nbsp;Datos de la factura
                </h3>
                <div class="panel-options">
                    <a href="#" style="visibility: hidden" id="btn_inicial_nota"><i class="fa fa-chevron-left"></i></a>
                    <a href="#" style="visibility: hidden" id="btn_cancelar_nota"><i class="fa fa-times"></i></a>
                    <a href="#"><i style="color: white;" class="fa fa-floppy-o" id="btn_nuevo"></i></a>
                    <a href="#"><i class="fa fa-search" id="btn_buscar"></i></a>
                </div>
            </div>
            <div class="panel-body" id="panel_body">
                <div class="row">
                    <div class="col-md-6">
                        <label class="fuente_lbl_controles">Proveedor</label>
                        <select id="cmb_proveedor" class="form-control"></select>
                    </div>
                    <div class="col-md-4">
                        <label class="fuente_lbl_controles">No. Factura</label>
                        <select id="cmb_factura" class="form-control"></select>
                    </div>
                    <div class="col-md-2">
                        <label class="fuente_lbl_controles" style="margin-bottom: 8px;">Fecha de factura</label>
                        <div class="input-group date" id="dtp_fecha_factura">
                            <input type="text" id="txt_fecha_factura" class="form-control" style="margin: 0px;" placeholder="dd/mm/aaaa" required />
                            <span class="input-group-addon">
                                <span class="glyphicon glyphicon-calendar"></span>
                            </span>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <label class="fuente_lbl_controles">Monto</label>
                        <input type="text" id="txt_monto" class="form-control" placeholder="Monto" />
                    </div>
                    <div class="col-md-3">
                        <label class="fuente_lbl_controles">Abono</label>
                        <input type="text" id="txt_abono" class="form-control" placeholder="Abono" />
                    </div>
                    <div class="col-md-2">
                        <label class="fuente_lbl_controles">Saldo</label>
                        <input type="text" id="txt_saldo" class="form-control" placeholder="Saldo" />
                    </div>
                    <div class="col-md-4">
                        <label class="fuente_lbl_controles">Moneda</label>
                        <input type="text" id="txt_moneda_factura" class="form-control" placeholder="Moneda" />
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-12" style="margin-top: 50px;">
                        <div class="panel-heading">
                            <h3 class="panel-title">
                                <i style="color: white;" class="fa fa-address-card-o"></i>&nbsp;Nota de credito
                            </h3>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-2">
                        <label class="fuente_lbl_controles">Nota credito</label>
                        <input type="text" id="txt_nota_credito_proveedor" class="form-control" placeholder="Nota credito" required/>
                         <input type="hidden" id="txt_no_nota_credito"/>
                    </div>
                    <div class="col-md-2">
                        <label class="fuente_lbl_controles">Cantidad a aplicar</label>
                        <input type="text" id="txt_cantidad" class="form-control" placeholder="Cantidad" required/>
                    </div>
                    <div class="col-md-2">
                        <label class="fuente_lbl_controles" style="margin-bottom: 8px;">Fecha de emisión</label>
                        <div class="input-group date" id="dtp_fecha_credito">
                            <input type="text" id="txt_fecha_credito" class="form-control" style="margin: 0px;" placeholder="dd/mm/aaaa" required />
                            <span class="input-group-addon">
                                <span class="glyphicon glyphicon-calendar"></span>
                            </span>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <label class="fuente_lbl_controles">Moneda</label>
                        <select id="cmb_moneda_credito" class="form-control" required>
                            <option value="PESOS">Pesos</option>
                            <option value="DOLARES">Dolares</option>
                        </select>
                    </div>
                    <div class="col-md-2">
                        <div style="display: none;" id="div_tipo_cambio">
                            <label class="fuente_lbl_controles">Tipo cambio</label>
                            <input type="text" id="txt_tipo_cambio" class="form-control" placeholder="Tipo cambio" required/>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-12">
                        <div id="div_motivo" style="display: none;">
                            <label class="fuente_lbl_controles">Motivo de cancelación</label>
                            <input type="text" id="txt_motivo" class="form-control" placeholder="Motivo"/>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-12">
                        <div id="sumary_error" class="alert alert-danger text-left" style="width: 277.78px !important; height: auto; display: none;">
                            <label id="lbl_msg_error" />
                        </div>

                    </div>
                </div>

            </div>
        </div>

    </div>
</asp:Content>
