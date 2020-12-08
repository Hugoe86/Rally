<%@ Page Title="" Language="C#" MasterPageFile="~/Paginas/Paginas_Generales/MasterPage.Master" AutoEventWireup="true" CodeBehind="Frm_Cat_Factura_Series.aspx.cs" Inherits="web_trazabilidad.Paginas.Catalogos.Frm_Cat_Factura_Series" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="../../Recursos/bootstrap-table/bootstrap-table.min.css" rel="stylesheet" />
    <link href="../../Recursos/estilos/center_loader.css" rel="stylesheet" />
    <link href="../../Recursos/estilos/css_master.css" rel="stylesheet" />
    <link href="../../Recursos/bootstrap-combo/select2.css" rel="stylesheet" />
    <link href="../../Recursos/estilos/demo_form.css" rel="stylesheet" />

    <script src="../../Recursos/bootstrap-table-current/bootstrap-table.js"></script>
    <script src="../../Recursos/bootstrap-table-current/locale/bootstrap-table-es-MX.js"></script>
    <script src="../../Recursos/bootstrap-table-current/extensions/export/bootstrap-table-export.js"></script>
    <script src="../../Recursos/bootstrap-table-current/extensions/export/tableExport.js"></script>

    <script src="../../Recursos/plugins/center-loader.min.js"></script>
    <script src="../../Recursos/plugins/parsley.js"></script>
    <script src="../../Recursos/bootstrap-combo/select2.js"></script>
    <script src="../../Recursos/bootstrap-table/bootstrap-table.min.js"></script>
    <script src="../../Recursos/bootstrap-table/locale/bootstrap-table-es-MX.min.js"></script>
    <script src="../../Recursos/javascript/seguridad/Js_Controlador_Sesion.js"></script>
    <script src="../../Recursos/javascript/catalogos/Js_Cat_Facturas_Series.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid" style="height: 100vh;">

        <div class="page-header" align="left">
            <h3 style="font-family: 'Roboto', cursive; font-size: 24px; font-weight: bold; color: #808080;">Cat&aacute;logo de Series</h3>
        </div>

        <div class="panel panel-color panel-info" id="panel1">
            <div class="panel-heading">
                <h3 class="panel-title">
                    <i style="color: white;" class="glyphicon glyphicon-filter"></i>&nbsp;Filtros de b&uacute;squeda
                </h3>
                <div class="panel-options">
                    <a href="#" data-toggle="panel">
                        <span class="collapse-icon">–</span>
                        <span class="expand-icon">+</span>
                    </a>

                </div>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-md-3">
                        <label>Tipo de Serie</label>
                        <select id="cmb_filter_type_serie" class="form-control">
                            <option value="">-- SELECCIONE --</option>
                            <option value="FACTURA">Factura</option>
                            <option value="NOTA CREDITO">Nota de Cr&eacute;dito</option>
                        </select>
                    </div>
                    <div class="col-md-9" align="right">
                        <button type="button" id="btn_search" class="btn btn-secondary btn-icon btn-icon-standalone btn-lg">
                            <i class="fa fa-search"></i>
                            <span>Buscar</span>
                        </button>
                    </div>
                </div>
            </div>
        </div>

        <div id="toolbar" style="margin-left: 5px;">
            <div class="btn-group" role="group" style="margin-left: 5px;">
                <button id="btn_start" type="button" class="btn btn-info btn-sm" title="Inicio"><i class="glyphicon glyphicon-home"></i></button>
                <button id="btn_export" type="button" class="btn btn-info btn-sm" title="Exportar Excel"><i class="fa fa-download"></i></button>
                <button id="btn_new" type="button" class="btn btn-info btn-sm" title="Nuevo"><i class="glyphicon glyphicon-plus"></i></button>
            </div>
        </div>
        <table id="tbl_series" data-toolbar="#toolbar" class="table table-responsive"></table>
    </div>

    <div class="modal fade" id="mdl_series" name="mdl_series" role="dialog" aria-hidden="true" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog">
            <div class="modal-content">

                <div class="modal-header">
                    <button type="button" class="close cancelar" data-dismiss="modal" aria-label="Close" onclick="_set_close_modal(true);"><i class="fa fa-times"></i></button>
                    <h4 class="modal-title" id="myModalLabel">
                        <label id="lbl_title"></label>
                    </h4>
                </div>

                <div class="modal-body">
                    <input type="hidden" id="txt_serie_id" />
                    <div class="row">
                        <div class="col-md-4">
                            <label class="fuente_lbl_controles">(*) Serie</label>
                            <input type="text" id="txt_serie" name="txt_serie" class="form-control input-sm" disabled="disabled" placeholder="(*) Serie" data-parsley-required="true" maxlength="5" required />
                        </div>
                        <div class="col-md-4">
                            <label class="fuente_lbl_controles">(*) Default</label>
                            <select id="cmb_default" name="cmb_default" class="form-control input-sm" disabled="disabled" data-parsley-required="true" required>
                                <option value="">-- SELECCIONE --</option>
                                <option value="NO">No</option>
                                <option value="SI">Si</option>
                            </select>
                        </div>
                        <div class="col-md-4">
                            <label class="fuente_lbl_controles">(*) Estatus</label>
                            <select id="cmb_status" name="cmb_status" class="form-control input-sm" disabled="disabled" data-parsley-required="true" required>
                                <option value="">-- SELECCIONE --</option>
                                <option value="PENDIENTE" selected="selected">Pendiente</option>
                                <option value="ACTIVO">Activo</option>
                                <option value="CANCELADO">Cancelado</option>
                                <option value="TERMINADO">Terminado</option>
                            </select>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-12">
                            <label class="fuente_lbl_controles">(*) Tipo de Serie</label>
                            <select id="cmb_type_serie" name="cmb_type_serie" class="form-control input-sm" disabled="disabled" data-parsley-required="true" required>
                                <option value="">-- SELECCIONE --</option>
                                <option value="FACTURA">Factura</option>
                                <option value="NOTA CREDITO">Nota de Crédito</option>
                            </select>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-12">
                            <label class="fuente_lbl_controles">Descripci&oacute;n</label>
                            <input type="text" id="txt_description" name="txt_description" class="form-control input-sm" disabled="disabled" placeholder="Descripci&oacute;n" maxlength="100" />
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-6">
                            <label class="fuente_lbl_controles">(*) Folio Inicial</label>
                            <input type="text" id="txt_folio_initial" name="txt_folio_initial" class="form-control input-sm" disabled="disabled" placeholder="Folio Inicial" data-parsley-required="true" required />
                        </div>
                        <div class="col-md-6">
                            <label class="fuente_lbl_controles">(*) Folio Final</label>
                            <input type="text" id="txt_folio_final" name="txt_folio_final" class="form-control input-sm" disabled="disabled" placeholder="Folio Final" data-parsley-required="true" required />
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-6">
                            <label class="fuente_lbl_controles">Total de Timbres</label>
                            <input type="text" id="txt_total_series" name="txt_total_series" class="form-control input-sm" disabled="disabled" placeholder="Total de Timbres" />
                        </div>
                        <div class="col-md-6">
                            <label class="fuente_lbl_controles">Cancelaciones</label>
                            <input type="text" id="txt_cancel" name="txt_cancel" class="form-control input-sm" disabled="disabled" placeholder="Cancelaciones" />
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
                                <button type="submit" class="btn btn-info btn-icon btn-icon-standalone btn-xs" id="btn_save_series" title="Guardar"><i class="fa fa-check"></i><span>Aceptar</span></button>
                                <button type="button" class="btn btn-danger btn-icon btn-icon-standalone btn-xs cancelar" data-dismiss="modal" id="btn_cancel_series" aria-label="Close" onclick="_set_close_modal(true);" title="Cancelar operaci&oacute;n"><i class="fa fa-remove"></i><span>Cancelar</span></button>
                            </div>
                        </div>
                    </div>
                </div>

            </div>
        </div>
    </div>

</asp:Content>
