$(document).on('ready', function () {
    _load_vistas();
});

function _inicializar_pagina() {
    try {
        _cargar_tabla();
        _search();
        _set_location_toolbar();
    } catch (e) {
        _mostrar_mensaje('Informe Técnico', e);
    }
}

function _cargar_tabla() {

    try {
        $('#tbl_usuarios_tipos_notificaciones').bootstrapTable('destroy');
        $('#tbl_usuarios_tipos_notificaciones').bootstrapTable({
            cache: false,
            striped: true,
            pagination: true,
            data: [],
            pageSize: 10,
            pageList: [10, 25, 50, 100, 200],
            smartDysplay: false,
            search: true,
            showColumns: false,
            showRefresh: false,
            minimumCountColumns: 2,
            clickToSelect: true,
            columns: [
                { field: 'Tipo_Notificaciones_Usuario_ID', title: '', width: 0, align: 'center', valign: 'bottom', sortable: true, visible: false },
                { field: 'Usuario_ID', title: '', width: 0, align: 'center', valign: 'bottom', sortable: true, visible: false },
                { field: 'Tipo_Notificacion_ID', title: '', width: 0, align: 'center', valign: 'bottom', sortable: true, visible: false },
                { field: 'Usuario', title: 'Usuario', title: 'Nombre', align: 'left', valign: '', sortable: true },
                { field: 'Tipo_Notificacion', title: 'Tipo Notificacion', align: 'left', valign: '', sortable: true },
                {
                    field: 'Enviar_Correo',
                    title: 'Enviar Correo',
                    align: 'left',
                    valign: '',
                    width: 60,
                    sortable: true,
                    visible: true,
                    formatter: function (value, row) {
                        var _valor = row.Enviar_Correo === true ? '<center><i class="fa fa-envelope" aria-hidden="true"></i></center>' : "";

                        return _valor;
                    }
                },
                { field: 'Correo', title: 'Correo', align: 'left', valign: '', sortable: true },
                {
                    field: 'Tipo_Notificaciones_Usuario_ID',
                    title: '',
                    align: 'center',
                    valign: 'bottom',
                    width: 60,
                    clickToSelect: false,
                    formatter: function (value, row) {
                        return '<div> ' +
                            '<a class="remove ml10 edit" id="' + row.Tipo_Notificaciones_Usuario_ID + '" href="javascript:void(0)" data-notificacion=\'' + JSON.stringify(row) + '\' onclick="btn_editar_click(this);" title="Editar"><i class="glyphicon glyphicon-edit"></i></button>' +
                            '&nbsp;&nbsp;<a class="remove ml10 delete" id="' + row.Tipo_Notificaciones_Usuario_ID + '" href="javascript:void(0)" data-notificacion=\'' + JSON.stringify(row) + '\' onclick="btn_eliminar_click(this);" title="Eliminar"><i class="glyphicon glyphicon-trash"></i></a>' +
                            '</div>';
                    }
                }
            ]
        });
    } catch (e) {
        _mostrar_mensaje('Informe Técnico', e);
    }
}

function _search() {
    var filtros = null;
    try {
        show_loading_bar({
            pct: 78,
            wait: .5,
            delay: .5,
            finish: function (pct) {
                filtros = new Object();
                filtros.Usuario = $('#txt_busqueda_por_nombre').val() === '' ? '' : $('#txt_busqueda_por_nombre').val();
                var $data = JSON.stringify(filtros);

                $.ajax({
                    type: 'POST',
                    url: UrlApp + "/api/Usuarios_Tipos_Notificaciones/Consultar_Usuarios_Tipos_Notificaciones_Por_Filtros",
                    data: $data,
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    cache: false,
                    success: function (datos) {
                        if (datos !== null) {
                            $('#tbl_usuarios_tipos_notificaciones').bootstrapTable('load', JSON.parse(datos));
                            hide_loading_bar();
                        }
                    }
                });
            }
        });
    } catch (e) {

    }
}

function _mostrar_mensaje(Titulo, Mensaje) {
    bootbox.dialog({
        message: Mensaje,
        title: Titulo,
        locale: 'es',
        closeButton: true,
        buttons: [{
            label: 'Cerrar',
            className: 'btn-info',
            callback: function () { }
        }]
    });
}

function _launchComponent(component, id) {

    $('#' + id).load(component, function () {

        switch (id) {

            case 'Controles_Generales':
                _inicializar_pagina();
                _mostrar_vista("Controles Generales");
                _eventos_componente1();
                break;

            case 'Operaciones_Vista':
                _cargar_combos();
                _eventos_componente2();
                break;

            default:

        }
    });
}

function _load_vistas() {
    _launchComponent('Vistas_Auxiliares/Usuarios_Tipos_Notificaciones/Controles_Generales.html', 'Controles_Generales');
    _launchComponent('Vistas_Auxiliares/Usuarios_Tipos_Notificaciones/Operaciones_Vista.html', 'Operaciones_Vista');
}

function _mostrar_vista(vista_) {

    if (vista_ === "Operaciones Vista") {
        $('#Controles_Generales').hide();
        $('#Operaciones_Vista').show();
    }
    else if (vista_ === "Controles Generales") {
        $('#Controles_Generales').show();
        $('#Operaciones_Vista').hide();
    }
    else {

    }
}

function _set_location_toolbar() {
    $('#toolbar').parent().removeClass("pull-left");
    $('#toolbar').parent().addClass("pull-right");
}

function _eventos_componente1() {
    try {
        $('#btn_nuevo').bind('click', function (e) {
            _limpiar_controles();
            _habilitar_controles('Nuevo');
            //_mostrar_vista('Operaciones_Vista');
        });
        $('#btn_busqueda').bind('click', function (e) {
            e.preventDefault();
            _search();
        });
    } catch (e) {
        _mostrar_mensaje('Informe Técnico', e);
    }
}

function _eventos_componente2() {
    try {

        $('#btn_cancelar').bind('click', function (e) {
            _mostrar_vista("Controles Generales");
            _limpiar_controles();
            _habilitar_controles("Inicio");
        });
        $('#btn_guardar_datos').on('click', function (e) {
            e.preventDefault();

            if ($('#txt_tipo_notificaciones_usuario_id').val() != null && $('#txt_tipo_notificaciones_usuario_id').val() != undefined && $('#txt_tipo_notificaciones_usuario_id').val() != '') {
                var _output = _validar_datos();

                if (_output.Estatus) {
                    if (_actualizar()) {
                        $('#btn_cancelar').click();
                    }
                }
                else {
                    _mostrar_mensaje('Información', _output.Mensaje);
                }
            }
            else {
                var _output = _validar_datos();

                if (_output.Estatus) {
                    if (_alta()) {
                        $('#btn_cancelar').click();
                    }
                }
                else {
                    _mostrar_mensaje('Información', _output.Mensaje);
                }
            }
        });
        $('#txt_nombre').on('blur', function () {
            $(this).val($(this).val().match(/^[0-9a-zA-ZáéíóúÁÉÍÓÚ\s]+$/) ? $(this).val() : $(this).val().replace(/\W+/g, ' '));
        });
        $('#txt_descripcion').on('blur', function () {
            $(this).val($(this).val().match(/^[0-9a-zA-ZáéíóúÁÉÍÓÚ\s]+$/) ? $(this).val() : $(this).val().replace(/\W+/g, ' '));
        });
        $('#chk_correo').change(function () {
            if ($(this).is(":checked")) {
                $('#txt_correo').attr({ disabled: false });
                $('#txt_correo').val('');
                _cargar_email_usuario();
            }
            else {
                $('#txt_correo').attr({ disabled: true });
                $('#txt_correo').val('');
            }
        });

        _configuracion_toastr();

    } catch (e) {
        _mostrar_mensaje('Informe Técnico', e);
    }
}

function _cargar_email_usuario() {
    var _valor_Seleccionado = $('#cmb_usuario').val();
    if (_valor_Seleccionado != "") {
        var filtros = null;
        try {

            filtros = new Object();
            filtros.Usuario_ID = $('#cmb_usuario').val();

            var $data = JSON.stringify(filtros);

            jQuery.ajax({
                type: 'POST',
                url: UrlApp + "/api/Usuarios_Tipos_Notificaciones/Consultar_Correo_Usuario",
                data: $data,
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                async: false,
                cache: false,
                success: function (datos) {
                    if (datos !== null) {
                        var datos_combo = JSON.parse(datos);

                        for (var Indice_Usuario = 0; Indice_Usuario < datos_combo.length; Indice_Usuario++) {
                            $('#txt_correo').val(datos_combo[Indice_Usuario].Email);
                        }

                    }
                }
            });

        } catch (e) {

        }
    }
    else {
        $('#txt_fecha_inicio').val("");
        $('#txt_fecha_termino').val("");
    }
}

function _limpiar_controles() {
    $('input[type=text]').each(function () { $(this).val(''); });
    $('select').each(function () { $(this).val(''); });
    $('textarea').each(function () { $(this).val(''); });
    $('#txt_tipo_notificaciones_usuario_id').val('');
    $('input[type=checkbox]').each(function () { $(this).prop('checked', false).change() });
}

function _habilitar_controles(opcion) {
    var Estatus = false;

    switch (opcion) {
        case "Nuevo":
            _mostrar_vista("Operaciones Vista");
            Estatus = true;
            break;
        case "Modificar":
            _mostrar_vista("Operaciones Vista");
            Estatus = true;
            break;
        case "Inicio":
            break;
    }
    $('#cmb_usuario').attr({ disabled: !Estatus });
    $('#cmb_tipo_notificacion').attr({ disabled: !Estatus });
    $('#chk_correo').attr({ disabled: !Estatus });
    $('#cmb_estatus').attr({ disabled: !Estatus });
}

function _mostrar_mensaje_validacion(_mensaje, _tipo) {
    Command: toastr[_tipo](_mensaje, "Aviso")
}

function _cargar_combos() {
    _cargar_usuarios();
    _cargar_tipos_notificaciones();
    _cargar_estatus();
}

function _cargar_usuarios() {
    var filtros = null;
    try {
        jQuery.ajax({
            type: 'POST',
            url: UrlApp + "/api/Usuarios_Tipos_Notificaciones/Consultar_Usuarios",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: true,
            cache: false,
            success: function (datos) {
                if (datos !== null) {
                    var datos_combo = $.parseJSON(datos);
                    var combo = $('#cmb_usuario');
                    $('option', combo).remove();

                    var options = '<option value=""><-SELECCIONE></option>';
                    for (var Indice_Usuario = 0; Indice_Usuario < datos_combo.length; Indice_Usuario++) {
                        options += '<option value="' + datos_combo[Indice_Usuario].Usuario_ID + '">' + datos_combo[Indice_Usuario].Usuario + '</option>';
                    }
                    combo.append(options);
                }
            }
        });
    } catch (e) {

    }
}

function _cargar_tipos_notificaciones() {
    var filtros = null;
    try {
        jQuery.ajax({
            type: 'POST',
            url: UrlApp + "/api/Usuarios_Tipos_Notificaciones/Consultar_Tipos_Notificacion",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: true,
            cache: false,
            success: function (datos) {
                if (datos !== null) {
                    var datos_combo = $.parseJSON(datos);
                    var combo = $('#cmb_tipo_notificacion');
                    $('option', combo).remove();

                    var options = '<option value=""><-SELECCIONE></option>';
                    for (var Indice_Tipo_Notificaciones = 0; Indice_Tipo_Notificaciones < datos_combo.length; Indice_Tipo_Notificaciones++) {
                        options += '<option value="' + datos_combo[Indice_Tipo_Notificaciones].Tipo_Notificacion_ID + '">' + datos_combo[Indice_Tipo_Notificaciones].Nombre + '</option>';
                    }
                    combo.append(options);
                }
            }
        });
    } catch (e) {

    }
}

function _cargar_estatus() {
    var select_estatus = $('#cmb_estatus');
    $('option', select_estatus).remove();
    var options_estatus = '<option value="">SELECCIONE</option>';
    options_estatus += '<option value="ACTIVO">ACTIVO</option>';
    options_estatus += '<option value="INACTIVO">INACTIVO</option>';
    select_estatus.append(options_estatus);
}

function _configuracion_toastr() {
    toastr.options = {
        "closeButton": true,
        "debug": false,
        "newestOnTop": true,
        "progressBar": false,
        "positionClass": "toast-bottom-right",
        "preventDuplicates": true,
        "onclick": null,
        "showDuration": "200",
        "hideDuration": "700",
        "timeOut": "5000",
        "extendedTimeOut": "1000",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut"
    }
}

function _validar_datos() {
    var _output = new Object();

    try {
        _output.Estatus = true;
        _output.Mensaje = '';

        if ($('#cmb_usuario').val() == '' || $('#cmb_usuario').val() == undefined || $('#cmb_usuario').val() == null) {
            _output.Estatus = false;
            _output.Mensaje += '&nbsp;-&nbsp;El usuario es un dato requerido.<br />';
        }

        if ($('#cmb_tipo_notificacion').val() == '' || $('#cmb_tipo_notificacion').val() == undefined || $('#cmb_tipo_notificacion').val() == null) {
            _output.Estatus = false;
            _output.Mensaje += '&nbsp;-&nbsp;El tipo de notificacion es un dato requerido.<br />';
        }

        if ($('#cmb_estatus').val() == '' || $('#cmb_estatus').val() == undefined || $('#cmb_estatus').val() == null) {
            _output.Estatus = false;
            _output.Mensaje += '&nbsp;-&nbsp;El estatus es un dato requerido.<br />';
        }

        if ($('#chk_correo').is(':checked')) {
            if ($('#txt_correo').val() == '' || $('#txt_correo').val() == undefined || $('#txt_correo').val() == null) {
                _output.Estatus = false;
                _output.Mensaje += '&nbsp;-&nbsp;El correo es un dato requerido.<br />';
            }
        }

        if (_output.Mensaje != "")
            _output.Mensaje = "Favor de proporcionar lo siguiente: <br />" + _output.Mensaje;

    } catch (e) {
        _mostrar_mensaje('Informe técnico', e);
    } finally {
        return _output;
    }
}

function _alta() {
    var Usuarios_Tipos_Notificaciones = null;
    var isComplete = false;

    try {

        Usuarios_Tipos_Notificaciones = new Object();
        Usuarios_Tipos_Notificaciones.Tipo_Notificaciones_Usuario_ID = $('#txt_tipo_notificaciones_usuario_id').val();
        Usuarios_Tipos_Notificaciones.Usuario_ID = $('#cmb_usuario').val();
        Usuarios_Tipos_Notificaciones.Tipo_Notificacion_ID = $('#cmb_tipo_notificacion').val();
        Usuarios_Tipos_Notificaciones.Enviar_Correo = ($('#chk_correo').is(":checked")) ? true : false;
        Usuarios_Tipos_Notificaciones.Correo = $('#txt_correo').val();
        Usuarios_Tipos_Notificaciones.Estatus = $('#cmb_estatus').val();

        var $data = JSON.stringify(Usuarios_Tipos_Notificaciones);

        $.ajax({
            type: 'POST',
            url: UrlApp + "/api/Usuarios_Tipos_Notificaciones/Alta_Usuarios_Tipos_Notificaciones",
            data: $data,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            cache: false,
            success: function (result) {
                var Resultado = JSON.parse(result);
                if (Resultado != null && Resultado != undefined && Resultado != '') {
                    if (Resultado.Estatus == 'success') {
                        _mostrar_mensaje_validacion(Resultado.Mensaje, "info");
                        _search();
                        isComplete = true;
                    } else if (Resultado.Estatus == 'error') {
                        _mostrar_mensaje('Información', Resultado.Mensaje);
                    }
                } else {
                    _mostrar_mensaje('Información', Resultado.Mensaje);
                }
            }
        });
    } catch (e) {
        _mostrar_mensaje('Informe Tecnico', e);
    }
    return isComplete;
}

function _actualizar() {
    var Usuarios_Tipos_Notificaciones = null;
    var isComplete = false;

    try {

        Usuarios_Tipos_Notificaciones = new Object();
        Usuarios_Tipos_Notificaciones.Tipo_Notificaciones_Usuario_ID = $('#txt_tipo_notificaciones_usuario_id').val();
        Usuarios_Tipos_Notificaciones.Usuario_ID = $('#cmb_usuario').val();
        Usuarios_Tipos_Notificaciones.Tipo_Notificacion_ID = $('#cmb_tipo_notificacion').val();
        Usuarios_Tipos_Notificaciones.Enviar_Correo = ($('#chk_correo').is(":checked")) ? true : false;
        Usuarios_Tipos_Notificaciones.Correo = $('#txt_correo').val();
        Usuarios_Tipos_Notificaciones.Estatus = $('#cmb_estatus').val();

        var $data = JSON.stringify(Usuarios_Tipos_Notificaciones);

        $.ajax({
            type: 'POST',
            url: UrlApp + "/api/Usuarios_Tipos_Notificaciones/Actualizar_Usuarios_Tipos_Notificacioness",
            data: $data,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            cache: false,
            success: function (result) {
                var Resultado = JSON.parse(result);
                if (Resultado != null && Resultado != undefined && Resultado != '') {
                    if (Resultado.Estatus == 'success') {
                        _mostrar_mensaje_validacion(Resultado.Mensaje, "info");
                        _search();
                        isComplete = true;
                    } else if (Resultado.Estatus == 'error') {
                        _mostrar_mensaje('Información', Resultado.Mensaje);
                    }
                } else {
                    _mostrar_mensaje('Información', Resultado.Mensaje);
                }
            }
        });

    } catch (e) {
        _mostrar_mensaje('Informe Tecnico', e);
    }
    return isComplete;
}

function _eliminar(Tipo_Notificaciones_Usuario_ID) {
    var Usuarios_Tipos_Notificaciones = null;
    var isComplete = false;

    try {

        Usuarios_Tipos_Notificaciones = new Object();
        Usuarios_Tipos_Notificaciones.Tipo_Notificaciones_Usuario_ID = Tipo_Notificaciones_Usuario_ID;

        var $data = JSON.stringify(Usuarios_Tipos_Notificaciones);

        $.ajax({
            type: 'POST',
            url: UrlApp + "/api/Usuarios_Tipos_Notificaciones/Eliminar_Usuarios_Tipos_Notificacioness",
            data: $data,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            cache: false,
            success: function (result) {
                var Resultado = JSON.parse(result);
                if (Resultado != null && Resultado != undefined && Resultado != '') {
                    if (Resultado.Estatus == 'success') {
                        _mostrar_mensaje_validacion(Resultado.Mensaje, "info");
                        _search();
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

function btn_editar_click(notificacion) {
    var row = $(notificacion).data('notificacion');

    $('#txt_tipo_notificaciones_usuario_id').val(row.Tipo_Notificaciones_Usuario_ID);
    $('#cmb_usuario').val(row.Usuario_ID);
    $('#cmb_tipo_notificacion').val(row.Tipo_Notificacion_ID);
    $('#chk_correo').prop('checked', row.Enviar_Correo).change();
    $('#txt_correo').val(row.Correo);
    $('#cmb_estatus').val(row.Estatus);

    _habilitar_controles('Modificar');
}

function btn_eliminar_click(notificacion) {
    var row = $(notificacion).data('notificacion');

    bootbox.confirm({
        title: 'Eliminar Registro',
        message: '¿Está seguro de eliminar el registro seleccionado?',
        callback: function (result) {
            if (result) {
                _eliminar(row.Tipo_Notificaciones_Usuario_ID);
            }
        }
    });
}