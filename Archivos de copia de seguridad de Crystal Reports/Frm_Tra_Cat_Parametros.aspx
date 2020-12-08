<%@ Page Title="" Language="C#" MasterPageFile="~/Paginas/Paginas_Generales/MasterPage.Master" AutoEventWireup="true" CodeBehind="Frm_Tra_Cat_Parametros.aspx.cs" Inherits="web_trazabilidad.Paginas.Catalogos.Frm_Tra_Cat_Parametros" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../Recursos/bootstrap-table/bootstrap-table.min.css" rel="stylesheet" />
    <link href="../../Recursos/estilos/center_loader.css" rel="stylesheet" />
    <link href="../../Recursos/estilos/demo_form.css" rel="stylesheet" />
    <link href="../../Recursos/bootstrap-combo/select2.css" rel="stylesheet" />

    <script src="../../Recursos/bootstrap-combo/select2.js"></script>
    <script src="../../Recursos/bootstrap-combo/es.js"></script>

    <script src="../../Recursos/bootstrap-table-current/bootstrap-table.js"></script>
    <script src="../../Recursos/bootstrap-table-current/locale/bootstrap-table-es-MX.js"></script>
    <script src="../../Recursos/bootstrap-table-current/extensions/export/bootstrap-table-export.js"></script>
    <script src="../../Recursos/bootstrap-table-current/extensions/export/tableExport.js"></script>

    <script src="../../Recursos/bootstrap-table-current/bootstrap-table.js"></script>
    <script src="../../Recursos/bootstrap-table-current/locale/bootstrap-table-es-MX.js"></script>
    <script src="../../Recursos/bootstrap-table/extensions/editable/bootstrap-editable.js"></script>
    <script src="../../Recursos/bootstrap-table-current/extensions/editable/bootstrap-table-editable.js"></script>
    <script src="../../Recursos/jquery-numeric/jquery.numeric.js"></script>
    <script src="../../Recursos/plugins/center-loader.min.js"></script>
    <script src="../../Recursos/plugins/parsley.js"></script>
    <script src="../../Recursos/javascript/seguridad/Js_Controlador_Sesion.js"></script>
    <script src="../../Recursos/javascript/catalogos/Js_Tra_Cat_Parametros.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid" style="height: 100vh;">

        <div class="page-header" align="left">
            <h3 style="font-family: 'Roboto Light', cursive !important; font-size: 24px; font-weight: bold; color: #808080;">Cat&aacute;logo de Par&aacute;metros</h3>
        </div>

        <div class="panel panel-color panel-info" id="panel1">
            <div class="panel-heading">
                <h3 class="panel-title">
                    <i style="color: white;" class="glyphicon glyphicon-edit"></i>&nbsp;Campos
                </h3>
                <div class="panel-options">
                    <a href="#" style="visibility: visible;" id="btn_salir"><i class="glyphicon glyphicon-home"></i></a>
                    <a href="#" style="visibility: visible;" id="btn_nuevo"><i class="fa fa-save"></i></a>
                </div>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-md-3">
                        <label class="fuente_lbl_controles">(*) Prefijo</label>
                        <input type="text" id="txt_prefijo" name="txt_prefijo" class="form-control input-sm" placeholder="(*) Prefijo" data-parsley-required="true" maxlength="5" required />
                    </div>
                    <div class="col-md-3">
                        <label class="fuente_lbl_controles">(*) Tipo de Usuario</label>
                        <input type="text" id="txt_tipo_usuario" name="txt_tipo_usuario" class="form-control input-sm" placeholder="(*) Tipo de Usuario" data-parsley-required="true" maxlength="50" required />
                        <input type="hidden" id="txt_parametro_id" />
                    </div>
                    <div class="col-md-3">
                        <label class="fuente_lbl_controles">(*) URL pantalla producción</label>
                        <select id="cmb_menu" name="cmb_menu" class="form-control input-sm" style="border-radius: inherit" data-parsley-required="false"></select>
                    </div>
                    <div class="col-md-3">
                        <label class="fuente_lbl_controles">Generar Contenedor</label><select id="cmb_generar_contenedor" name="cmb_generar_contenedor" class="form-control input-sm" style="border-radius: inherit" data-parsley-required="false"></select>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <label class="fuente_lbl_controles">Pedir Confirmación</label>
                        <select id="cmb_confirmacion" name="cmb_confirmacion" class="form-control input-sm" style="border-radius: inherit" data-parsley-required="false"></select>
                    </div>
                    <div class="col-md-3">
                        <label class="fuente_lbl_controles">Mostrar Panel</label>
                        <select id="cmb_panel" name="cmb_panel" class="form-control input-sm" style="border-radius: inherit" data-parsley-required="false"></select>
                    </div>

                    <div class="col-md-3">
                        <label class="fuente_lbl_controles">Generar No Lote</label><select id="cmb_generar_no_lote" name="cmb_generar_no_lote" class="form-control input-sm" style="width: 100% !important; border-radius: inherit" data-parsley-required="false"></select>
                    </div>
                    <div class="col-md-3">
                        <label class="fuente_lbl_controles">¿Más de 1 Orden producción en 1 ubicación?</label><select id="cmb_tener_ordprod_ubicacion" name="cmb_tener_ordprod_ubicacion" class="form-control input-sm" data-parsley-required="false"><option value="">SELECCIONE</option>
                            <option value="SI">SI</option>
                            <option value="NO">NO</option>
                        </select>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <label class="fuente_lbl_controles">Almacén Entrada</label><select id="cmb_almacen" name="cmb_almacen" class="form-control input-sm" style="width: 100% !important" data-parsley-required="false"></select>
                    </div>
                    <div class="col-md-3">
                        <label class="fuente_lbl_controles">Almacén Cuarentera</label><select id="cmb_almacen_cuarentena" name="cmb_almacen_cuarentena" class="form-control input-sm" style="width: 100% !important" data-parsley-required="false"></select>
                    </div>
                    <div class="col-md-3">
                        <label class="fuente_lbl_controles">Almacén Embarque</label><select id="cmb_almacen_embarque" name="cmb_almacen_embarque" class="form-control input-sm" style="width: 100% !important;" data-parsley-required="false"></select>
                    </div>
                    <div class="col-md-3">
                        <label class="fuente_lbl_controles">Rol de Validación de Calidad</label><select id="cmb_rol_calidad" name="cmb_rol_calidad" class="form-control input-sm" style="width: 100% !important;" data-parsley-required="false"></select>
                    </div>
                </div>
                <div class="row" style="margin-top: 3px;">
                    <div class="col-md-3">
                        <label class="fuente_lbl_controles">Ubicación Entrada</label>
                        <select id="cmb_ubicacion" name="cmb_ubicacion" class="form-control input-sm" style="width: 100% !important" data-parsley-required="false"></select>
                    </div>
                    <div class="col-md-3">
                        <label class="fuente_lbl_controles">Ubicación Cuarentera</label><select id="cmb_ubicacion_cuarentena" name="cmb_ubicacion_cuarentena" class="form-control input-sm" style="width: 100% !important" data-parsley-required="false"></select>
                    </div>
                    <div class="col-md-3">
                        <label class="fuente_lbl_controles">Ubicación Embarque</label><select id="cmb_ubicacion_embarque" name="cmb_ubicacion_embarque" class="form-control input-sm" style="width: 100% !important;" data-parsley-required="false"></select>
                    </div>
                    <div class="col-md-3">
                        <label class="fuente_lbl_controles">Unidad de medida serie</label><select id="cmb_um_id_serie" name="cmb_um_id_serie" class="form-control input-sm" style="width: 100% !important;" data-parsley-required="false"></select>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <label class="fuente_lbl_controles">Path write file Cashdro</label><input type="text" id="txt_path_write_file_cashdro" name="txt_path_write_file_cashdro" class="form-control input-sm" data-parsley-required="false" />
                    </div>
                    <div class="col-md-6">
                        <label class="fuente_lbl_controles">Path read file Cashdro</label><input type="text" id="txt_path_read_file_cashdro" name="txt_path_read_file_cashdro" class="form-control input-sm" data-parsley-required="false" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <label class="fuente_lbl_controles">Email</label>
                        <input type="text" id="txt_email" class="form-control" placeholder="Email" />
                    </div>
                    <div class="col-md-3">
                        <label class="fuente_lbl_controles">Contraseña</label>
                        <input type="password" id="txt_contrasena" class="form-control" placeholder="Contraseña" />
                    </div>
                    <div class="col-md-3">
                        <label class="fuente_lbl_controles">Puerto</label>
                        <input type="text" id="txt_puerto" class="form-control" placeholder="Puerto" />
                    </div>
                    <div class="col-md-3">
                        <label class="fuente_lbl_controles">Host</label>
                        <input type="text" id="txt_host" class="form-control" placeholder="Host" />
                    </div>
                </div>
                <div class="row" style="margin-top: 7px;">
                </div>
                <div class="row">
                    <div class="col-md-3">
                        &nbsp;&nbsp;&nbsp;<input type="checkbox" name="chck_usedefaultcredentials" id="chck_usedefaultcredentials" />&nbsp;<span style="font-family: \'Roboto Regular\'; color: #000000;">¿Usar credenciales default?</span>
                    </div>
                    <div class="col-md-3">
                        &nbsp;&nbsp;&nbsp;<input type="checkbox" name="chck_enablessl" id="chck_enablessl" />&nbsp;<span style="font-family: \'Roboto Regular\'; color: #000000;">¿Habilitar SSL?</span>
                    </div>
                    <div class="col-md-3">
                        &nbsp;&nbsp;&nbsp;<input type="checkbox" name="chck_habilitar_contenedor_no_piezas" id="chck_habilitar_contenedor_no_piezas" />&nbsp;<span style="font-family: \'Roboto Regular\'; color: #000000;">¿Habilitar Contenedor y No. piezas</span>
                    </div>
                    <div class="col-md-3">
                        &nbsp;&nbsp;&nbsp;<input type="checkbox" name="chck_modificar_tipo_impuesto_oc" id="chck_modificar_tipo_impuesto_oc" />&nbsp;<span style="font-family: \'Roboto Regular\'; color: #000000;">¿Modificar tipo de impuesto OC?</span>
                    </div>
                </div>
                <div class="row" style="margin-top: 7px;">
                </div>
                <div class="row" style="margin-top: 3px;">
                    <div class="col-md-3">
                        &nbsp;&nbsp;&nbsp;<input type="checkbox" name="permitir_produccion" id="permitir_neg_produccion" />&nbsp;<span style="font-family: \'Roboto Regular\'; color: #000000;">¿Permitir negativos en producción?</span>
                    </div>
                    <div class="col-md-3">&nbsp;&nbsp;&nbsp;<input type="checkbox" name="visible" id="visible" onclick="consultar();" />&nbsp;<span style="font-family: \'Roboto Regular\'; color: #000000;">¿Tiene almacen embarque?</span>     </div>
                    <div class="col-md-3">&nbsp;&nbsp;&nbsp;<input type="checkbox" name="permitir_embarques" id="permitir_neg_embarques" />&nbsp;<span style="font-family: \'Roboto Regular\'; color: #000000;">¿Permitir negativos en embarques?</span>     </div>
                    <div class="col-md-3">
                        &nbsp;&nbsp;&nbsp;<input type="checkbox" name="chck_modificar_precio_oc" id="chck_modificar_precio_oc" />&nbsp;<span style="font-family: \'Roboto Regular\'; color: #000000;">¿Modificar precio OC?</span>
                    </div>
                </div>
                <div class="row" style="margin-top: 7px;">
                </div>
                <div class="row" style="margin-top: 3px;">
                    <div class="col-md-3">
                        <label class="fuente_lbl_controles">Almacén producción</label>
                        <select id="cmb_almacen_produccion_id" style="width: 100% !important"></select>
                    </div>
                    <div class="col-md-3">
                        <label class="fuente_lbl_controles">Categoria Bebida</label>
                        <select id="cmb_categoria_bebida_id" style="width: 100% !important"></select>
                    </div>
                    <div class="col-md-3">
                        <label class="fuente_lbl_controles">Categoria Combo</label>
                        <select id="cmb_categoria_combo_id" style="width: 100% !important"></select>
                    </div>
                    <div class="col-md-3">
                        <label class="fuente_lbl_controles">Cantidad de Decimales</label><input type="text" id="txt_cantidad_decimales" name="txt_cantidad_decimales" placeholder="Cantidad de Decimales" class="form-control input-sm" data-parsley-required="false" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <label class="fuente_lbl_controles">Ubicación producción</label>
                        <select id="cmb_ubicacion_produccion_id" style="width: 100% !important"></select>
                    </div>
                    <div class="col-md-3">
                        <label class="fuente_lbl_controles">Título del reporte</label><input type="text" id="txt_titulo_reporte" name="txt_titulo_reporte" placeholder="Título del reporte" class="form-control input-sm" data-parsley-required="false" />
                    </div>
                    <div class="col-md-3">
                        <label class="fuente_lbl_controles">Subtítulo del reporte</label><input type="text" id="txt_subtitulo_reporte" name="txt_subtitulo_reporte" placeholder="Subtítulo del reporte" class="form-control input-sm" data-parsley-required="false" />
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



</asp:Content>
