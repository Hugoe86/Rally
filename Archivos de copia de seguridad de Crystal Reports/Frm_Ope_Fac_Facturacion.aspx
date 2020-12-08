<%@ Page Title="Facturación" Language="C#" MasterPageFile="~/Paginas/Paginas_Generales/MasterPage.Master" AutoEventWireup="true" CodeBehind="Frm_Ope_Fac_Facturacion.aspx.cs"
    Inherits="web_trazabilidad.Paginas.Facturacion.Frm_Ope_Fac_Facturacion" %>

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
    <script src="../../Recursos/javascript/facturacion/Js_Ope_Fac_Facturacion.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container-fluid" id="div_scroll" style="height: 100vh;">

        <div class="page-header">
            <h3 style="font-family: 'Roboto Light', cursive !important; font-size: 24px; font-weight: bold; color: #808080;">Facturación electrónica</h3>
        </div>
        <div id="toolbar_pro" style="margin-left: 0px; text-align: right">
        </div>

        <div class="panel panel-color panel-info" id="panel1">
            <div class="panel-heading">
                <h3 class="panel-title">
                    <i style="color: white;" class="glyphicon glyphicon-edit"></i>&nbsp;Generales Factura
                </h3>
                <div class="panel-options">
                    <a href="#" style="visibility:hidden" id="btn_regresar_inicio"><i class="fa fa-chevron-left"></i></a>
                    <a href="#" style="visibility:hidden" id="btn_enviar_factura"><i class="fa fa-envelope"></i></a>
                    <a href="#" style="visibility:hidden" id="btn_mostrar_xml"><i class="fa fa-file-code-o"></i></a>
                    <a href="#" style="visibility:hidden" id="btn_mostrar_pdf"><i class="fa fa-file-pdf-o"></i></a>
                    <a href="#" style="visibility: hidden" id="btn_generar_factura"><i class="fa fa-floppy-o"></i></a>
                    <a href="#" style="visibility: visible" id="btn_nueva_factura"><i class="glyphicon glyphicon-plus"></i></a>
                    <a href="#" style="visibility: visible" id="btn_buscar_factura"><i class="fa fa-search"></i></a>
                    <a href="#" style="visibility:hidden" id="btn_cancelar_factura"><i class="fa fa-times"></i></a>
                </div>
            </div>
            <div class="panel-body" id="panel_body">

                <div class="row">
                    <div class="col-md-2">
                        <label class="fuente_lbl_controles">No. Factura</label>
                        <div class="input-group">
                            <input type="number" id="txt_no_factura" name="txt_no_factura" required="required" class="form-control" required />
                            <span class="input-group-addon" style="width: 0px; padding-left: 0px; padding-right: 0px; border: none;"></span>
                            <select id="cmb_serie" name="cmb_serie" class="form-control" style="width: 73px;" required>
                            </select>
                        </div>
                    </div>
                    <div class="col-md-2">
                        <label class="fuente_lbl_controles">Estatus</label>
                        <select id="cmb_estatus" class="form-control" required>
                            <option value="">Seleccione</option>
                            <option value="POR PAGAR">Por pagar</option>
                            <option value="PAGADA">Pagada</option>
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
                        <label class="fuente_lbl_controles">Tipo de Factura</label>
                        <select id="cmb_tipo_factura" class="form-control" required>
                            <option value="">Seleccione</option>
                            <option value="NORMAL">Normal</option>
                            <option value="HONORARIOS">Honorarios</option>
                            <option value="FLETE">Flete</option>
                            <option value="ARRENDAMIENTOS">Arrendamientos</option>
                            <option value="SINIVA">Sin IVA</option>
                        </select>
                    </div>
                    <div class="col-md-2">
                        <label class="fuente_lbl_controles">Moneda</label>
                        <select id="cmb_moneda" class="form-control" required>
                            <option value="PESOS">Pesos</option>
                            <option value="DOLARES">Dólares</option>
                        </select>
                    </div>
                    <div class="col-md-2">
                        <label class="fuente_lbl_controles">Tipo de cambio</label>
                        <input type="text" id="txt_tipo_cambio" class="form-control" placeholder="Tipo cambio" required />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12" style="margin-top: 50px;">
                        <div class="panel-heading">
                            <h3 class="panel-title">
                                <i style="color: white;" class="glyphicon glyphicon-edit"></i>&nbsp;Datos de pago
                            </h3>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-2">
                        <label class="fuente_lbl_controles">Forma de pago</label>
                        <select id="cmb_forma_pago" class="form-control" required>
                        </select>
                    </div>

                    <div class="col-md-2">
                        <label class="fuente_lbl_controles" style="margin-bottom: 8px;">Fecha vencimiento</label>
                        <div class="input-group date" id="dtp_fecha_vencimiento">
                            <input type="text" id="txt_fecha_vencimiento" class="form-control" style="margin: 0px;" placeholder="dd/mm/aaaa" required />
                            <span class="input-group-addon">
                                <span class="glyphicon glyphicon-calendar"></span>
                            </span>
                        </div>
                    </div>

                    <div class="col-md-2">
                        <label class="fuente_lbl_controles">Dias de crédito</label>
                        <input type="text" id="txt_dias_credito" class="form-control" placeholder="Dias credito" required />
                    </div>

                    <div class="col-md-2">
                        <label class="fuente_lbl_controles">Metodo de pago</label>
                        <select id="cmb_metodo_pago" class="form-control" required>
                        </select>
                    </div>
                    <div class="col-md-2">
                        <label class="fuente_lbl_controles">No. cuenta de pago</label>
                        <input type="text" id="txt_no_cuenta_pago" class="form-control" placeholder="No. cuenta de pago" />
                    </div>
                    <div class="col-md-2">
                        <label class="fuente_lbl_controles">Descuento</label>
                        <input type="text" id="txt_descuento" class="form-control" placeholder="Descuento" />
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-12">
                        <label class="fuente_lbl_controles">Condiciones de pago</label>
                        <input type="text" id="txt_condicion_pago" class="form-control" placeholder="Condiciones de pago" required />
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-12" style="margin-top: 50px">
                        <div class="panel-heading">
                            <h3 class="panel-title">
                                <i style="color: white;" class="glyphicon glyphicon-edit"></i>&nbsp;Datos del cliente
                            </h3>
                        </div>
                    </div>
                </div>

                <div class="row" style="margin-top: 5px;">
                </div>

                <div class="row">
                    <div class="col-md-11">
                        <label class="fuente_lbl_controles">Nombre cliente</label>
                        <input type="text" id="txt_nombre_cliente" class="form-control" placeholder="Nombre del cliente" required />
                    </div>
                    <div class="col-md-1" style="text-align: right">
                        <br />
                        <button id="btn_abrir_modal_cliente" type="button" class="btn btn-success" style="border-radius: 6px !important;"><i class="fa fa-search"></i></button>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-2">
                        <label class="fuente_lbl_controles">RFC</label>
                        <input type="text" id="txt_rfc_cliente" class="form-control" placeholder="RFC del cliente" required />
                    </div>
                    <div class="col-md-2">
                        <label class="fuente_lbl_controles">Calle</label>
                        <input type="text" id="txt_calle_cliente" class="form-control" placeholder="Calle del cliente" required />
                    </div>
                    <div class="col-md-2">
                        <label class="fuente_lbl_controles">Número exterior</label>
                        <input type="text" id="txt_numero_exterior_cliente" class="form-control" placeholder="Número exterior del cliente" required />
                    </div>
                    <div class="col-md-2">
                        <label class="fuente_lbl_controles">Número interior</label>
                        <input type="text" id="txt_numero_interior_cliente" class="form-control" placeholder="Número interior del cliente" />
                    </div>
                    <div class="col-md-2">
                        <label class="fuente_lbl_controles">Colonia</label>
                        <input type="text" id="txt_colonia_cliente" class="form-control" placeholder="Colonia del cliente" required />
                    </div>
                    <div class="col-md-2">
                        <label class="fuente_lbl_controles">Codigo postal</label>
                        <input type="text" id="txt_cp_cliente" class="form-control" placeholder="Codigo postal del cliente" required />
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-2">
                        <label class="fuente_lbl_controles">Localidad</label>
                        <input type="text" id="txt_localidad_cliente" class="form-control" placeholder="Localidad del cliente" required />
                    </div>
                    <div class="col-md-2">
                        <label class="fuente_lbl_controles">Ciudad</label>
                        <input type="text" id="txt_ciudad_cliente" class="form-control" placeholder="Ciudad del cliente" required />
                    </div>
                    <div class="col-md-2">
                        <label class="fuente_lbl_controles">Estado</label>
                        <input type="text" id="txt_estado_cliente" class="form-control" placeholder="Estado del cliente" required />
                    </div>
                    <div class="col-md-2">
                        <label class="fuente_lbl_controles">Pais</label>
                        <input type="text" id="txt_pais_cliente" class="form-control" placeholder="Pais del cliente" required />
                    </div>
                    <div class="col-md-4">
                        <input type="hidden" id="txt_cliente_id" class="form-control" placeholder="cliente id" required />
                         <input type="hidden" id="txt_email_cliente" class="form-control" placeholder="email cliente" required />
                        <input type="hidden" id="txt_no_embarque" class="form-control" placeholder="no embarque" required />
                        <input type="hidden" id="txt_no_venta" class="form-control" placeholder="no venta" required />
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-12" style="margin-top: 50px;">
                        <div class="panel-heading">
                            <h3 class="panel-title">
                                <i style="color: white;" class="glyphicon glyphicon-edit"></i>&nbsp;Conceptos de la factura              
                            </h3>
                            <div class="panel-options">
                                <a href="#" style="visibility: hidden" id="btn_conceptos_venta"><i class="fa fa-ticket"></i></a>
                                <a href="#" style="visibility: hidden" id="btn_conceptos_manual"><i class="fa fa-pencil"></i></a>

                            </div>
                        </div>
                    </div>

                </div>

                <%--                <div class="row" style="margin-top: 20px;">
                    <div class="col-md-1">
                        <label class="fuente_lbl_controles">Cantidad</label>
                        <input type="text" id="txt_cantidad_conceptos" class="form-control" style="margin-top:0px !important" placeholder="Cantidad del concepto" required />
                    </div>

                    <div class="col-md-4">
                        <label class="fuente_lbl_controles">Codigo</label>
                        <select id="cmb_producto" style="width:100%"></select>
                    </div>
                    <div class="col-md-2">
                        <label class="fuente_lbl_controles">Impuesto</label>
                        <select id="cmb_impuesto_concepto" class="form-control" required>
                          
                        </select>
                    </div>
                    <div class="col-md-2">
                        <label class="fuente_lbl_controles">Unidad</label>
                        <select id="cmb_unidad_concepto" class="form-control" required>
                        
                        </select>
                    </div>
                    <div class="col-md-2">
                        <label class="fuente_lbl_controles">Precio sin impuesto</label>
                        <input type="text" id="txt_precio_sin_impuesto_concepto" class="form-control" style="margin-top:0px !important" placeholder="Precio sin impuesto" required />
                    </div>
                    <div class="col-md-1">
                        <br />
                        <button type="button" style="visibility: hidden; border-radius: 6px !important;" id="btn_agregar_concepto" class="btn btn-success"><i class="fa fa-plus"></i></button>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-12">
                        <table id="grid_conceptos" class="table table-responsive"></table>
                    </div>
                </div>--%>

                <div class="row">
                    <div class="col-md-12">
                        <table id="tbl_conceptos_factura" class="table table-responsive"></table>
                    </div>
                </div>

                <div class="row" style="margin-top: 20px">
                    <div class="col-md-5" style="margin-top: 50px;">
                        <div class="panel-heading">
                            <h3 class="panel-title">
                                <i style="color: white;" class="glyphicon glyphicon-edit"></i>&nbsp;Información adicional
                            </h3>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <label class="fuente_lbl_controles">Comentarios</label>
                            </div>
                            <div class="col-md-8">
                                <textarea id="txt_comentarios" class="form-control" placeholder="Comentarios"></textarea>
                            </div>
                        </div>
                    </div>

                    <div class="col-md-2">
                    </div>
                    <div class="col-md-5" style="margin-top: 50px;">
                        <div class="panel-heading">
                            <h3 class="panel-title">
                                <i style="color: white;" class="glyphicon glyphicon-edit"></i>&nbsp;Totales
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
                                        <label class="fuente_lbl_controles">Total</label>
                                    </div>
                                    <div class="col-md-9">
                                        <input type="text" id="txt_total" class="form-control" placeholder="Total" required />
                                    </div>
                                </div>
                            </div>
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
        <div style="height: 100px"></div>
    </div>

</asp:Content>
