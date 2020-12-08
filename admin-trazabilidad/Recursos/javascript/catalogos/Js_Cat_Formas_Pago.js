var closeModal = true;

///***********LOAD****************///
$(document).on('ready', function () {
    _inicializar_pagina();
});

///**********************Metodos**************//
function _inicializar_pagina() {
    try {
        _modal();
       
        _load_tbl_formas_pago();
        _set_location_toolbar();
        _eventos();
        
       
    } catch (e) {
        _mostrar_mensaje('Informe Técnico', e);
    }
}

function _launch_modal(title_window) {
    _set_title_modal(title_window);
    jQuery('#modal_datos').modal('show', { backdrop: 'static', keyboard: false });
}

function _set_title_modal(Titulo) {
    $("#lbl_titulo").html(Titulo);
}

function _set_close_modal(state) {
    closeModal = state;
    _limpiar_controles();
}

function _mostrar_mensaje(Titulo, Mensaje) {
    bootbox.dialog({
        message: Mensaje,
        title: Titulo,
        locale: 'es',
        closeButton: true,
        buttons: [{
            label: 'Cerrar',
            className: 'btn-default',
            callback: function () { }
        }]
    });
}

function _limpiar_controles() {
    $('input[type=text]').each(function () { $(this).val(''); });
    $('textarea').each(function () { $(this).val(''); });
    $('#txt_forma_pago_id').val('');
    $("#cmb_numero_operacion").val('');
    $("#cmb_bancarizado").val('');
    $("#cmb_rfc_emisor_ordenante").val('');
    $("#cmb_cuenta_ordenante").val('');
    $("#cmb_rfc_emisor_beneficiario").val('');
    $("#cmb_cuenta_beneficiario").val('');
    $("#cmb_nombre_banco").val('');
    _validation_sumary(null);
    //_clear_all_class_error();
}

///******************************MODAL********************************///
function _modal() {
    var tags = '';
    try {
        tags += '<div class="modal fade" id="modal_datos" name="modal_datos" role="dialog" aria-hidden="true" data-backdrop="static" data-keyboard="false">';
        tags += '<div class="modal-dialog">';
        tags += '<div class="modal-content">';

        tags += '<div class="modal-header">';
        tags += '   <button type="button" class="close cancelar" data-dismiss="modal" aria-label="Close" onclick="_set_close_modal(true);"><i class="fa fa-times"></i></button>';
        tags += '   <h4 class="modal-title" id="myModalLabel">';
        tags += '       <label id="lbl_titulo"></label>';
        tags += '   </h4>';
        tags += '</div>';

        tags += '<div class="modal-body">';

        tags += '   <div class="row">' +
                '       <div class="col-md-12">' +
                '           <label class="fuente_lbl_controles">(*) Nombre forma de pago</label>' +
                '           <input type="text" id="txt_nombre" name="txt_nombre" class="form-control input-sm" placeholder="(*) Forma de pago" data-parsley-required="true" maxlength="100" required /> ' +
                '           <input type="hidden" id="txt_forma_pago_id"/>' +
                '       </div>' +
                '   </div>' +
                '   <div class="row">' +
                '       <div class="col-md-6">' +
                '           <label class="fuente_lbl_controles">Clave SAT</label>' +
                '           <input type="text" id="txt_clave_sat" name="txt_clave_sat" class="form-control input-sm" placeholder="Clave SAT" data-parsley-required="true" maxlength="2" required /> ' +
                '       </div>' +
                ' <div class="col-md-6">'+
                '      <label class="fuente_lbl_controles"> Bancarizado</label>'+
                '      <select id="cmb_bancarizado" name="cmb_bancarizado" class="form-control input-sm" style="width:100% !important" data-parsley-required="true" required>'+
                '        <option value=""><<--SELECCIONE-->></option>' +
                '        <option value="SI">SI</option>' +
                '        <option value="NO">NO</option>' +
                '        <option value="OPCIONAL">OPCIONAL</option>' +
                '</select>' +
                '   </div>'+
                ' </div>'+
                '<div class="row">' +
                ' <div class="col-md-6">' +
                '      <label class="fuente_lbl_controles"> Número Operación</label>' +
                '      <select id="cmb_numero_operacion" name="cmb_numero_operacion" class="form-control input-sm" style="width:100% !important"  data-parsley-required="true" required>'+
                '        <option value=""><<--SELECCIONE-->></option>' +
                '        <option value="SI">SI</option>' +
                '        <option value="NO">NO</option>' +
                '        <option value="OPCIONAL">OPCIONAL</option>' +
                '       </select>' +
                '   </div>' +
                ' <div class="col-md-6">' +
                '      <label class="fuente_lbl_controles"> RFC Emisor Cuenta Ordenante</label>' +
                '      <select id="cmb_rfc_emisor_ordenante" name="cmb_rcf_emisor_ordenante" class="form-control input-sm" style="width:100% !important"  data-parsley-required="true" required>' +
                '        <option value=""><<--SELECCIONE-->></option>' +
                '        <option value="SI">SI</option>' +
                '        <option value="NO">NO</option>' +
                '        <option value="OPCIONAL">OPCIONAL</option>' +
                '</select>' +
                '   </div>' +
                '</div>'+
                 '<div class="row">' +
                ' <div class="col-md-6">' +
                '      <label class="fuente_lbl_controles"> Cuenta Ordenante</label>' +
                '      <select id="cmb_cuenta_ordenante" name="cmb_cuenta_ordenante" class="form-control input-sm" style="width:100% !important"  data-parsley-required="true" required>' +
                 '        <option value=""><<--SELECCIONE-->></option>' +
                '        <option value="SI">SI</option>' +
                '        <option value="NO">NO</option>' +
                '        <option value="OPCIONAL">OPCIONAL</option>' +
                '</select>' +
                '   </div>' +
                ' <div class="col-md-6">' +
                '      <label class="fuente_lbl_controles"> Patrón Cuenta Ordenante</label>' +
                '           <input type="text" id="txt_patron_cuenta_ordenante" name="txt_patron_cuenta_ordenante" class="form-control input-sm" placeholder=" Patrón Cuenta Ordenante" data-parsley-required="true" maxlength="500" required /> ' +
                '   </div>' +
                '</div>'+
                '<div class="row">' +
                ' <div class="col-md-6">' +
                '      <label class="fuente_lbl_controles"> RFC Emisor Cuenta Beneficiario</label>' +
                '      <select id="cmb_rfc_emisor_beneficiario" name="cmb_rfc_emisor_beneficiario" class="form-control input-sm" style="width:100% !important"  data-parsley-required="true" required>' +
                 '        <option value=""><<--SELECCIONE-->></option>' +
                '        <option value="SI">SI</option>' +
                '        <option value="NO">NO</option>' +
                '        <option value="OPCIONAL">OPCIONAL</option>' +
                '</select>' +
                '   </div>' +
                ' <div class="col-md-6">' +
                '      <label class="fuente_lbl_controles"> Cuenta Beneficiario</label>' +
                '      <select id="cmb_cuenta_beneficiario" name="cmb_cuenta_beneficiario" class="form-control input-sm" style="width:100% !important" data-parsley-required="true" required>' +
                 '        <option value=""><<--SELECCIONE-->></option>' +
                '        <option value="SI">SI</option>' +
                '        <option value="NO">NO</option>' +
                '        <option value="OPCIONAL">OPCIONAL</option>' +
                '</select>' +
                '   </div>' +
                '</div>' +
                '<div class="row">' +
                ' <div class="col-md-6">' +
                '      <label class="fuente_lbl_controles"> Patrón Cuenta Beneficiario</label>' +
                '           <input type="text" id="txt_patron_cuenta_beneficiario" name="txt_patron_cuenta_beneficiario" class="form-control input-sm" placeholder=" Patrón Cuenta Beneficiario" data-parsley-required="true" maxlength="500" required /> ' +
                '   </div>' +
                  ' <div class="col-md-6">' +
                '      <label class="fuente_lbl_controles"> Nombre Banco Emisor Cuenta Ordenante Caso Extranjero</label>' +
                '      <select id="cmb_nombre_banco" name="cmb_nombre_banco" class="form-control input-sm" style="width:100% !important" data-parsley-required="true" required>' +
                '        <option value=""><<--SELECCIONE-->></option>' +
                '        <option value="SI">SI</option>' +
                '        <option value="NO">NO</option>' +
                '        <option value="OPCIONAL">OPCIONAL</option>' +
                '</select>' +
                '   </div>' +
                '</div>'+
                '<div class="row">' +
                '   <div class="col-md-6">'+
                '<label class="text-bold text-left text-medium fuente_lbl_controles" title="Fecha Inicio Vigencia" style="margin-bottom: 5px !important;"> Fecha Inicio Vigencia</label>'+
                '<div class="input-group date" id="dtp_fecha_inicio">'+
                '<input type="text" id="f_inicio" class="form-control" style="margin: 0px;" placeholder="dd/mm/aaaa" />'+
                '<span class="input-group-addon">'+
                ' <span class="glyphicon glyphicon-calendar"></span>'+
                ' </span>'+
                '</div>'+  
                '</div>' +
                '<div class="col-md-6">' +
                '<label class="text-bold text-left text-medium fuente_lbl_controles" title="Fecha Fin Vigencia" style="margin-bottom: 5px !important;"> Fecha Fin Vigencia</label>' +
                '<div class="input-group date" id="dtp_fecha_fin">' +
                 '<input type="text" id="f_fin" class="form-control" style="margin: 0px;" placeholder="dd/mm/aaaa" />' +
                 '<span class="input-group-addon">' +
                    ' <span class="glyphicon glyphicon-calendar"></span>' +
                ' </span>' +
                '</div>' +
                '</div>' 
                
        tags += '</div>';

        tags += '<div class="modal-footer">';
        tags += '   <div class="row">';
        tags += '       <div class="col-md-7">';
        tags += '           <div id="sumary_error" class="alert alert-danger text-left" style="width: 277.78px !important; display:none;">';
        tags += '               <label id="lbl_msg_error"/>';
        tags += '           </div>';
        tags += '       </div>';

        tags += '       <div class="col-md-5">';
        tags += '           <div class="form-inline">';
        tags += '               <button type="submit" class="btn btn-info btn-icon btn-icon-standalone btn-xs" id="btn_guardar_datos" title="Guardar"><i class="fa fa-check"></i><span>Aceptar</span></button>';
        tags += '               <button type="button" class="btn btn-danger btn-icon btn-icon-standalone btn-xs cancelar" data-dismiss="modal" id="btn_cancelar" aria-label="Close" onclick="_set_close_modal(true);" title="Cancelar operaci&oacute;n"><i class="fa fa-remove"></i><span>Cancelar</span></button>';
        tags += '           </div>';
        tags += '       </div>';

        tags += '   </div>';
        tags += '</div>';

        tags += '</div>';
        tags += '</div>';
        tags += '</div>';

        $(tags).appendTo('body');

        $('#btn_guardar_datos').bind('click', function (e) {
            e.preventDefault();

            if ($('#txt_forma_pago_id').val() != null && $('#txt_forma_pago_id').val() != undefined && $('#txt_forma_pago_id').val() != '') {
                var _output = _validation('editar');
                if (_output.Estatus) {
                    if (_modificar_forma_pago()) {
                        _set_close_modal(true);
                        jQuery('#modal_datos').modal('hide');
                    }
                } else {
                    _set_close_modal(false);
                }
            } else {
                var _output = _validation('alta');
                if (_output.Estatus) {
                    if (_alta_forma_pago()) {
                        _set_close_modal(true);
                        jQuery('#modal_datos').modal('hide');
                    }
                } else {
                    _set_close_modal(false);
                }
            }
        });
    } catch (e) {
        _mostrar_mensaje('Informe técnico', e);
    }
}

function _set_location_toolbar() {
    $('#toolbar').parent().removeClass("pull-left");
    $('#toolbar').parent().addClass("pull-right");
}

///*********************************Eventos****************************//
function _eventos() {
    try {
        $('#btn_nuevo').click(function (e) {
         _launch_modal('<i class="fa fa-floppy-o" style="font-size: 25px;"></i>&nbsp;&nbsp;Alta de registro');
        });

        $('#btn_salir').on('click', function (e) {
            e.preventDefault();
            window.location.href = '../Paginas_Generales/Frm_Apl_Principal.aspx';
        });

        $('#btn_busqueda').on('click', function (e) {
            e.preventDefault();
            _search_formas_pago();
        });

        $('#dtp_fecha_inicio').datetimepicker({
            defaultDate: new Date(),
            viewMode: 'days',
            locale: 'es',
            format: "DD/MM/YYYY"
        });
        $("#dtp_fecha_inicio").datetimepicker("useCurrent", true);

        $('#dtp_fecha_fin').datetimepicker({
            defaultDate: new Date(),
            viewMode: 'days',
            locale: 'es',
            format: "DD/MM/YYYY"
        });
        $("#dtp_fecha_fin").datetimepicker("useCurrent", true);

    } catch (e) {
        _mostrar_mensaje('Informe Técnico', e);
    }
}

function btn_editar_click(metodo_pago) {
    var row = $(metodo_pago).data('forma_pago');

    $('#txt_forma_pago_id').val(row.Forma_ID);
    $('#txt_nombre').val(row.Nombre);
    $('#txt_clave_sat').val(row.Clave_Sat);
 
    $('#cmb_bancarizado').val(row.Bancarizado)
        
    $('#cmb_numero_operacion').val(row.Numero_Operacion);
  
    $('#cmb_rfc_emisor_ordenante').val(row.RFC_Emisor_Cuenta_Ordenante);
  
    $("#cmb_cuenta_ordenante").val(row.Cuenta_Ordenante);
  
    $("#cmb_rfc_emisor_beneficiario").val(row.RFC_Emisor_Cuenta_Beneficiario );
 
   
    $("#cmb_cuenta_beneficiario").val(row.Cuenta_Benenficiario);
    $("#cmb_nombre_banco").val(row.Nombre_Banco_Emisor_Cuenta_Ordenante_Caso_Extranjero)
   
    $('#f_inicio').val(parseDate(row.Fecha_Inicio_Vigencia1));
    $('#f_fin').val(parseDate(row.Fecha_Fin_Vigencia1));
    $('#txt_patron_cuenta_beneficiario').val(row.Patron_Cuenta_Beneficiaria);
    $('#txt_patron_cuenta_ordenante').val(row.Patron_Cuenta_Ordenante);


    //_habilitar_controles('Modificar');
    _launch_modal('<i class="glyphicon glyphicon-edit" style="font-size: 25px;"></i>&nbsp;&nbsp;Actualizar registro');
}

function btn_eliminar_click(metodo_pago) {
    var row = $(metodo_pago).data('forma_pago');

    bootbox.confirm({
        title: 'Eliminar Registro',
        message: '¿Está seguro de eliminar el registro seleccionado?',
        callback: function (result) {
            if (result) {
                _eliminar_metodo_pago(row.Forma_ID);
            }
        
        }
    });
}

///*****************************Grid*********************///
function _load_tbl_formas_pago() {

    try {
        $('#tbl_forma_pago').bootstrapTable('destroy');
        $('#tbl_forma_pago').bootstrapTable({
            cache: false,
            striped: true,
            pagination: true,
            pageSize: 10,
            pageList: [10, 25, 50, 100, 200],
 
            search: true,
            showColumns: false,
            showRefresh: false,
            minimumCountColumns: 2,
            columns: [
                { field: 'Forma_ID', title: '', align: 'center', valign: 'bottom', sortable: false, visible: false },
                { field: 'Nombre', title: 'Nombre', align: 'left', valign: 'bottom', sortable: false },
                { field: 'Clave_Sat', title: 'Clave SAT', align: 'left', valign: 'bottom', sortable: false },
                { field: 'Numero_Operacion', title: '', align: 'left', valign: 'bottom', sortable: false, visible: false },
                { field: 'Bancarizado', title: '', align: 'left', valign: 'bottom', sortable: false, visible: false },
                { field: 'RFC_Emisor_Cuenta_Ordenante', title: '', align: 'left', valign: 'bottom', sortable: false, visible: false },
                { field: 'Cuenta_Ordenante', title: '', align: 'left', valign: 'bottom', sortable: false, visible: false },
                { field: 'RFC_Emisor_Cuenta_Beneficiario', title: '', align: 'left', valign: 'bottom', sortable: false, visible: false },
                {
                    field: 'Forma_ID',
                    title: '',
                    align: 'center',
                    valign: 'bottom',
                    width: 60,
                    formatter: function (value, row) {
                        return '<div> ' +
                            '<a class="remove ml10 edit" id="' + row.Metodo_Pago_ID + '" href="javascript:void(0)" data-forma_pago=\'' + JSON.stringify(row) + '\' onclick="btn_editar_click(this);" title="Editar"><i class="glyphicon glyphicon-edit"></i></button>' +
                            '&nbsp;&nbsp;<a class="remove ml10 delete" id="' + row.Metodo_Pago_ID + '" href="javascript:void(0)" data-forma_pago=\'' + JSON.stringify(row) + '\' onclick="btn_eliminar_click(this);" title="Eliminar"><i class="glyphicon glyphicon-trash"></i></a>' +
                            '</div>';
                    }
                }
            ]
        });
    } catch (e) {
        _mostrar_mensaje('Informe Técnico', e);
    }
}

///*********************BD**************///
function _search_formas_pago() {
    var filtros = null;
    try {
        show_loading_bar({
            pct: 78,
            wait: .5,
            delay: .5,
            finish: function (pct) {
                var $_nombre = $('#txt_busqueda_por_nombre').val();

                filtros = new Object();
                filtros.Nombre = ($_nombre === undefined || $_nombre === '') ? '' : $('#txt_busqueda_por_nombre').val();
                
                var $data = JSON.stringify({ 'jsonObject': JSON.stringify(filtros) });

                jQuery.ajax({
                    type: 'POST',
                    url: 'controller/Formas_Pago_Controller.asmx/Consultar_Formas_Pago',
                    data: $data,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: true,
                    cache: false,
                    success: function (datos) {
                        if (datos !== null) {
                            $('#tbl_forma_pago').bootstrapTable('load', JSON.parse(datos.d));
                            hide_loading_bar();
                        }
                    }
                });
            }
        });
    } catch (e) {

    }
}

function _alta_forma_pago() {
    var Forma_Pago = null;
    var isComplete = false;

    try {

        Forma_Pago = new Object();
        Forma_Pago.Nombre = $('#txt_nombre').val();
        Forma_Pago.Clave_Sat = ($('#txt_clave_sat').val() != null && $('#txt_clave_sat') != undefined && $('#txt_clave_sat') != '') ? $('#txt_clave_sat').val() : null;
        Forma_Pago.Bancarizado = $('#cmb_bancarizado').val();
        Forma_Pago.Numero_Operacion = $('#cmb_numero_operacion').val();
        Forma_Pago.RFC_Emisor_Cuenta_Ordenante = $("#cmb_rfc_emisor_ordenante").val();
        Forma_Pago.Cuenta_Ordenante = $("#cmb_cuenta_ordenante").val();
        Forma_Pago.RFC_Emisor_Cuenta_Beneficiario = $("#cmb_rfc_emisor_beneficiario").val();
        Forma_Pago.Cuenta_Benenficiario = $("#cmb_cuenta_beneficiario").val();
        Forma_Pago.Nombre_Banco_Emisor_Cuenta_Ordenante_Caso_Extranjero = $("#cmb_nombre_banco").val();

        if ($('#f_inicio').val() == '' || $('#f_inicio').val() == undefined || $('#f_inicio').val() == null)
            Forma_Pago.Fecha_Inicio_Vigencia = "";
        else {
            Forma_Pago.Fecha_Inicio_Vigencia = parseDate( $('#f_inicio').val());
        }
        if ($('#f_fin').val() == '' || $('#f_fin').val() == undefined || $('#f_fin').val() == null)
            Forma_Pago.Fecha_Fin_Vigencia = "";
        else {
            Forma_Pago.Fecha_Fin_Vigencia =parseDate($('#f_fin').val());
        }
        Forma_Pago.Patron_Cuenta_Beneficiaria= $('#txt_patron_cuenta_beneficiario').val();
        Forma_Pago.Patron_Cuenta_Ordenante = $('#txt_patron_cuenta_ordenante').val();

        var $data = JSON.stringify({ 'jsonObject': JSON.stringify(Forma_Pago) });

        $.ajax({
            type: 'POST',
            url: 'controller/Formas_Pago_Controller.asmx/Alta',
            data: $data,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            cache: false,
            success: function (result) {
                var Resultado = JSON.parse(result.d);
                if (Resultado != null && Resultado != undefined && Resultado != '') {
                    if (Resultado.Estatus == 'success') {
                        _search_formas_pago();
                        _limpiar_controles();
                        isComplete = true;
                    } else if (Resultado.Estatus == 'error') {
                        _validation_sumary(Resultado);
                    }
                } else {
                    _validation_sumary(Resultado);
                }
            }
        });
    } catch (e) {
        _mostrar_mensaje('Informe Tecnico', e);
    }
    return isComplete;
}

function _modificar_forma_pago() {
    var Metodo_Pago = null;
    var isComplete = false;

    try {
        Metodo_Pago = new Object();
        Metodo_Pago.Forma_ID = parseInt($('#txt_forma_pago_id').val());
        Metodo_Pago.Nombre = $('#txt_nombre').val();
        Metodo_Pago.Clave_Sat = ($('#txt_clave_sat').val() != null && $('#txt_clave_sat') != undefined && $('#txt_clave_sat') != '') ? $('#txt_clave_sat').val() : null;
        Metodo_Pago.Bancarizado = $('#cmb_bancarizado').val();
        Metodo_Pago.Numero_Operacion = $('#cmb_numero_operacion').val();
        Metodo_Pago.RFC_Emisor_Cuenta_Ordenante = $("#cmb_rfc_emisor_ordenante").val();
        Metodo_Pago.Cuenta_Ordenante = $("#cmb_cuenta_ordenante").val();
        Metodo_Pago.RFC_Emisor_Cuenta_Beneficiario = $("#cmb_rfc_emisor_beneficiario").val();
        Metodo_Pago.Cuenta_Benenficiario = $("#cmb_cuenta_beneficiario").val();
        Metodo_Pago.Nombre_Banco_Emisor_Cuenta_Ordenante_Caso_Extranjero = $("#cmb_nombre_banco").val();

        if ($('#f_inicio').val() == '' || $('#f_inicio').val() == undefined || $('#f_inicio').val() == null)
            Metodo_Pago.Fecha_Inicio_Vigencia = "";
        else {
            Metodo_Pago.Fecha_Inicio_Vigencia = parseDate($('#f_inicio').val());
        }
        if ($('#f_fin').val() == '' || $('#f_fin').val() == undefined || $('#f_fin').val() == null)
            Metodo_Pago.Fecha_Fin_Vigencia = "";
        else {
            Metodo_Pago.Fecha_Fin_Vigencia = parseDate($('#f_fin').val());
        }
        Metodo_Pago.Patron_Cuenta_Beneficiaria = $('#txt_patron_cuenta_beneficiario').val();
        Metodo_Pago.Patron_Cuenta_Ordenante = $('#txt_patron_cuenta_ordenante').val();

        var $data = JSON.stringify({ 'jsonObject': JSON.stringify(Metodo_Pago) });

        $.ajax({
            type: 'POST',
            url: 'controller/Formas_Pago_Controller.asmx/Actualizar',
            data: $data,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            cache: false,
            success: function (result) {
                var Resultado = JSON.parse(result.d);
                if (Resultado != null && Resultado != undefined && Resultado != '') {
                    if (Resultado.Estatus == 'success') {
                        _search_formas_pago();
                        _limpiar_controles();
                        isComplete = true;
                    } else if (Resultado.Estatus == 'error') {
                        _validation_sumary(Resultado);
                    }
                } else {
                    _validation_sumary(Resultado);
                }
            }
        });

    } catch (e) {
        _mostrar_mensaje('Informe Tecnico', e);
    }
    return isComplete;
}

function _eliminar_metodo_pago(Metodo_Pago_ID) {
    var Metodo_Pago = null;

    try {
        Metodo_Pago = new Object();
        Metodo_Pago.Forma_ID = parseInt(Metodo_Pago_ID);

        var $data = JSON.stringify({ 'jsonObject': JSON.stringify(Metodo_Pago) });

        $.ajax({
            type: 'POST',
            url: 'controller/Formas_Pago_Controller.asmx/Eliminar',
            data: $data,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            cache: false,
            success: function (result) {
                var Resultado = JSON.parse(result.d);
                if (Resultado != null && Resultado != undefined && Resultado != '') {
                    if (Resultado.Estatus == 'success') {
                        _search_formas_pago();
                    } else if (Resultado.Estatus == 'error') {
                        _mostrar_mensaje(Resultado.Titulo, Resultado.Mensaje);
                    }
                } else { _mostrar_mensaje(Resultado.Titulo, Resultado.Mensaje); }
            }
        });

    } catch (e) {
        _mostrar_mensaje('Informe Técnico', e);
    }
}

///*******************Validacion******************///
function _validation(opcion) {
    var _output = new Object();

    _output.Estatus = true;
    _output.Mensaje = '';

    if (!$('#txt_nombre').parsley().isValid()) {
        _add_class_error('#txt_nombre');
        _output.Estatus = false;
        _output.Mensaje += '&nbsp;<i class="fa fa-angle-double-right"></i>&nbsp;El nombre es un dato requerido.<br />';
    }
    if (!$('#txt_clave_sat').parsley().isValid()) {
        _add_class_error('#txt_nombre');
        _output.Estatus = false;
        _output.Mensaje += '&nbsp;<i class="fa fa-angle-double-right"></i>&nbsp;El nombre es un dato requerido.<br />';
    }

    if (_output.Estatus === 'error') {
        //_add_class_error('#txt_nombre');
        _output.Estatus = false;
        _output.Mensaje += '&nbsp;<i class="fa fa-angle-double-right"></i>&nbsp;' + _output.Mensaje + '<br />';
    }

    if (!_output.Estatus) _validation_sumary(_output);

    return _output;
}

function _validation_sumary(validation) {
    var header_message = '<i class="fa fa-exclamation-triangle fa-2x"></i><span>Observaciones</span><br />';

    if (validation == null) {
        $('#lbl_msg_error').html('');
        $('#sumary_error').css('display', 'none');
    } else {
        $('#lbl_msg_error').html(header_message + validation.Mensaje);
        $('#sumary_error').css('display', 'block');
    }
}

function _cargar_cmb(cmb) {
    try {
        $(cmb).select2({
            language: "es",
            theme: "classic",
            placeholder: 'SELECCIONE',
            allowClear: true,
            data: [
                {
                    id: '',
                    text: ''
                },
                {
                    id: 'SI',
                    text:'SI'
                },
                {
                    id: 'NO',
                    text:'NO'
                },
                {
                    id: 'OPCIONAL',
                    text:'OPCIONAL'
                }
            ]
        });
    } catch (e) {
        _mostrar_mensaje('Informe Técnico', e);
    }
}

function parseDate(dateString) {
    if (dateString != null) {
        var dateTime = dateString.split(" ");
        var dateOnly = dateTime[0];
        var dates = dateOnly.split("/");
        var temp = dates[1] + "/" + dates[0] + "/" + dates[2];
        return temp;

    }
}
