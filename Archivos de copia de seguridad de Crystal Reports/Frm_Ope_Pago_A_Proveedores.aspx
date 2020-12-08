<%@ Page Title="Pago a proveedor" Language="C#" MasterPageFile="~/Paginas/Paginas_Generales/MasterPage.Master" AutoEventWireup="true" CodeBehind="Frm_Ope_Pago_A_Proveedores.aspx.cs" Inherits="web_trazabilidad.Paginas.Facturacion.Frm_Ope_Pago_A_Proveedores" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../Recursos/bootstrap-table/bootstrap-table.min.css" rel="stylesheet" />
    <link href="../../Recursos/estilos/center_loader.css" rel="stylesheet" />
    <link href="../../Recursos/estilos/demo_form.css" rel="stylesheet" />
    <link href="../../Recursos/bootstrap-combo/select2.css" rel="stylesheet" />


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
    <script src="../../Recursos/javascript/seguridad/Js_Controlador_Sesion.js"></script>
    <script src="../../Recursos/jquery-numeric/jquery.numeric.js"></script>
    <script src="../../Recursos/javascript/facturacion/Js_Ope_Pago_A_Proveedores.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid" style="height: 100vh;">

        <div class="page-header">
            <h3 style="font-family: 'Roboto Light', cursive !important; font-size: 24px; font-weight: bold; color: #808080;">Pago a proveedor</h3>
        </div>

        <div class="panel panel-color panel-info" id="panel1">
            <div class="panel-heading">
                <h3 class="panel-title">
                    <i style="color: white;" class="fa fa-address-card-o"></i>&nbsp;Datos de pago
                </h3>
                <div class="panel-options">
                  <a href="#"><i style="color: white;" class="fa fa-floppy-o" id="btn_nuevo"></i></a>
                </div>
            </div>
            <div class="panel-body" id="panel_body">

                <div class="row">
                    <div class="col-md-8">
                        <label class="fuente_lbl_controles">Proveedor</label>
                        <select id="cmb_proveedor" class="form-control"></select>
                    </div>
                    <div class="col-md-4">
                        <label class="fuente_lbl_controles">Banco a pagar</label>
                        <select id="cmb_banco" class="form-control" required>
                        </select>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-8">
                        <label class="fuente_lbl_controles">Concepto</label>
                        <input type="text" id="txt_concepto" class="form-control" placeholder="Concepto" required />
                    </div>
                    <div class="col-md-4">
                        <label class="fuente_lbl_controles">Forma de pago</label>
                        <select id="cmb_forma_de_pago" class="form-control" required>
                        </select>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <label class="fuente_lbl_controles">Pago</label>
                        <input type="text" id="txt_pago" class="form-control" placeholder="pago" required />
                    </div>
                    <div class="col-md-3">
                        <label class="fuente_lbl_controles">Total a pagar</label>
                        <input type="text" id="txt_total" class="form-control" placeholder="total a pagar" required />
                    </div>
                    <div class="col-md-2">
                        <label class="fuente_lbl_controles" style="margin-bottom: 8px;">Fecha</label>
                        <div class="input-group date" id="dtp_fecha">
                            <input type="text" id="txt_fecha" class="form-control" style="margin: 0px;" placeholder="dd/mm/aaaa" required />
                            <span class="input-group-addon">
                                <span class="glyphicon glyphicon-calendar"></span>
                            </span>
                        </div>
                    </div>
                    <div class="col-md-2">
                        <div style="display: none;" id="div_referencia">
                            <label class="fuente_lbl_controles">Referencia</label>
                            <input type="text" id="txt_referencia" class="form-control" placeholder="Referencia" maxlength="10" required />
                        </div>
                        <div style="display: none;" id="div_no_cheque">
                            <label class="fuente_lbl_controles">No. de cheque</label>
                            <input type="text" id="txt_no_cheque" class="form-control" placeholder="No. de cheque" maxlength="10" required />

                        </div>
                    </div>
                    <div class="col-md-2">
                        <div style="display: none;" id="div_tipo_cambio">
                            <label class="fuente_lbl_controles">Tipo de cambio</label>
                            <input type="text" id="txt_tipo_cambio" class="form-control" placeholder="Tipo de cambio" required/>
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

        <div id="toolbar" style="margin-left: 5px; margin-top:8px" >
            <div class="btn-group" role="group" style="margin-left: 5px;">
                <button id="btn_salir" type="button" class="btn btn-info btn-sm" title="Salir"><i class="glyphicon glyphicon-home"></i></button>
            </div>
        </div>
        <table id="tbl_pagos_proveedor" data-toolbar="#toolbar" class="table table-responsive"></table>
    </div>
</asp:Content>
