$(document).on('ready', function () {
    NProgress.configure({
        template:
            '<div class="nprogress-content"> ' +
            '    <div class="bar" role="bar"> ' +
            '        <div class="peg"> ' +
            '        </div> ' +
            '    </div> ' +
            '    <div class="spinner" role="spinner"> ' +
            '        <div class="spinner-icon"> ' +
            '        </div> ' +
            '        <div class="lazyload"><img src="../../Recursos/img/gears.svg"></img><br/><center><strong style="font-family: Century Gothic; font-size:24px; color: black;">Iniciando sesión<div class="faa-pulse animated">...</div></strong></center></div> ' +
            '    </div> ' +
            '<div> '
    });

    $('#btn_popup').popover({
        html: true,
        trigger: 'focus',
        title:
            '<div class ="row">' +
            '<div class ="col-md-8">' +
            '<span class ="a-popover-header-content" id="a-popover-header-1">Casilla "Mantener la sesión iniciada"</span>' +
            '</div>' +
            '<div class ="col-md-4" align="right">' +
            '<a href="#" data-toggle="popover"><i class ="fa fa-close"></i></a>' +
            '</div>' +
            '</div>'
        ,
        content:
        '<p>Si seleccionas "Mantenerme conectado", se reduce el número de veces que se te pedirá que te identifiques en este dispositivo.</p>'+
        '<p>Para mantener la seguridad de tu cuenta, utiliza esta opción sólo en tus dispositivos personales.</p>'
        ,
        container: ".parent",
        placement: "top"

    });

    $('#anio').text(new moment().format("YYYY"));

    _tooltip('.btn_login_sucursal_salir', 'Ir a login', 'bottomRight');

    jQuery.validator.setDefaults({
        debug: true,
        success: "valid"
    });

    var remember = $.cookie('remember');
    if (remember == 'true') {
        var username = $.cookie('username');
        var password = $.cookie('password');
        // autofill the fields
        $('#username').attr("value", username);
        $('#passwd').attr("value", password);
        $('#remember').click();
        $("#remember").prop("checked", true);
    }

    $('#btn_enviar_email').on('click', function (e) {
        e.preventDefault();
        var value  = $.trim($('#emailInput').val());
        if (value !== '' && value !== null && value !== undefined)
            _send_email();
        else
            $('#emailInput').addClass('inputError');
    });

    //Entrar con sucursal
    $('#btn_entrar_sucursal').on('click', function (e) {
        e.preventDefault();
        var _output = _validation();
        if (_output.Estatus) {
            var $parametros = new Object();
            $parametros.Usuario = $('#username').val();
            $parametros.Password = $('#passwd').val();
            $parametros.Sucursal_ID = $('#cmb_sucursal_inicio').val();
            $parametros.Sucursal = $('#cmb_sucursal_inicio').text();

            var $data = JSON.stringify({ 'jsonObject': JSON.stringify($parametros) });

            show_loading_bar({
                wait: 0,
                delay: 0.5,
                pct: 100,
                finish: function () {
                    $.ajax({
                        url: 'controllers/Autentificacion_Controller.asmx/autentificacion',
                        data: $data,
                        type: 'POST',
                        cache: false,
                        async: false,
                        contentType: 'application/json; charset=UTF-8',
                        dataType: 'json',
                        success: function (resul) {
                            var $resultado = JSON.parse(resul.d);

                            if ($resultado.Estatus === 'success') {
                                window.location.href = '../Paginas_Generales/Frm_Apl_Principal.aspx';
                            } else {
                                hide_loading_bar();
                                toastr.error("Verifica tu usuario o contrase&ntilde;a, por favor, intentalo de nuevo.", "¡Acceso Incorrecto!", opts);
                                $(form).find('#passwd').val('');
                                $(form).find('#passwd').focus();
                                NProgress.done();
                            }
                        }
                    });
                }
            });
        }
    });

    //ocultar login de sucursal
    $('#btn_login_sucursal_salir').on('click', function (e) {
        e.preventDefault();
        _limpiar_controles();
        $('#login-sucursal').hide(1000);
        $('#login-container').show(1000);
    });

    $('#username').on('keyup', function () { $(this).val($(this).val().toLowerCase()); });
    $('#username').on('focus', function () { $(this).select(); });
    $('#passwd').on('focus', function () { $(this).select(); });
    $('#username').focus();
    setTimeout(function () { $(".fade-in-effect").addClass('in'); }, 1);

    $("form#login").validate({
        rules: {
            username: {
                required: true, email: true
            },
            passwd: {
                required: true
            }
        },

        messages: {
            username: {
                required: 'Por favor, ingresa tu e-mail&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;', email: 'Por favor, ingresa una direcci&oacute;n de correo v&aacute;lida&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;'
            },
            passwd: {
                required: 'Por favor, ingresa tu contrase&ntilde;a&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;'
            }
        },

        submitHandler: function (form) {

            var opts = {
                "closeButton": true,
                "debug": false,
                "positionClass": "toast-top-full-width",
                "onclick": null,
                "showDuration": "1000",
                "hideDuration": "1500",
                "timeOut": "7500",
                "extendedTimeOut": "1000",
                "showEasing": "swing",
                "hideEasing": "linear",
                "showMethod": "fadeIn",
                "hideMethod": "fadeOut"
            };

            var $parametros = new Object();
            $parametros.Usuario = $(form).find('#username').val();
            $parametros.Password = $(form).find('#passwd').val();

            var $data = JSON.stringify({ 'jsonObject': JSON.stringify($parametros) });
            NProgress.start();

            show_loading_bar({
                wait: 0,
                delay: 0.5,
                pct: 100,
                finish: function () {
                    $.ajax({
                        url: 'controllers/Autentificacion_Controller.asmx/Pre_autentificacion',
                        data: $data,
                        type: 'POST',
                        cache: false,
                        async: false,
                        contentType: 'application/json; charset=UTF-8',
                        dataType: 'json',
                        success: function (resul) {
                            var $resultado = JSON.parse(resul.d);

                            if ($resultado.Estatus === 'success') {
                                if ($("#remember").is(':checked')) {
                                    var username = $(form).find('#username').val();
                                    var password = $(form).find('#passwd').val();
                                    // set cookies to expire in 14 days
                                    $.cookie('username', username, { expires: 14 });
                                    $.cookie('password', password, { expires: 14 });
                                    $.cookie('remember', true, { expires: 14 });
                                } else {
                                    // reset cookies
                                    $.cookie('username', null);
                                    $.cookie('password', null);
                                    $.cookie('remember', null);
                                }
                                hide_loading_bar();

                                //si se accede directo o no
                                if ($resultado.LoginDirectly === true) {
                                    window.location.href = '../Paginas_Generales/Frm_Apl_Principal.aspx';
                                }
                                else {
                                    NProgress.done();
                                    _consultar_sucursales_inicio();
                                    $('#login-container').hide(1000);
                                    $('#login-sucursal').show(1000);
                                }

                            } else {

                                //mensaje cuando el usuario(rol) no tiene asignada alguna sucursal
                                if ($resultado.Mensaje === "sin_sucursal") {
                                    hide_loading_bar();
                                    NProgress.done();
                                    toastr.error("El usuario no hace referencia a alguna sucursal, por favor, contacte a su administrador.", "¡Acceso Incorrecto!", opts);
                                }
                                else {
                                    NProgress.done();
                                    hide_loading_bar();
                                    toastr.error("Verifica tu usuario o contrase&ntilde;a, por favor, intentalo de nuevo.", "¡Acceso Incorrecto!", opts);
                                    $(form).find('#passwd').val('');
                                    $(form).find('#passwd').focus();
                                }//fin else de mensaje error
                            }//fin else de error
                        }
                    });
                }
            });
        }
    });

    $("form#login .form-group:has(.form-control):first .form-control").focus();

    _ingresar_cookies_cargadas_directamente();
});

function _send_email() {
    var Usuario = null;

    show_loading_bar({
        pct: 78,
        wait: .5,
        delay: .5,
        finish: function (pct) {

            Usuario = new Object();
            Usuario.Email = $('#emailInput').val();

            var $_data = JSON.stringify({ 'jsonObject': JSON.stringify(Usuario) });

            $.ajax({
                type: 'POST',
                url: 'controllers/Autentificacion_Controller.asmx/recuperar_password',
                data: $_data,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                cache: false,
                success: function (result) {
                    var $_datos = JSON.parse(result.d);

                    if ($_datos !== null && $_datos !== undefined) {
                        if ($_datos.Estatus === 'success') {
                            hide_loading_bar();
                            $('#emailInput').val('');
                            _mostrar_mensaje($_datos.Titulo, $_datos.Mensaje);
                        } else {
                            _mostrar_mensaje('Información', 'Problemas al realizar el cambio de password.');
                        }
                    } else {
                        _mostrar_mensaje('Información', 'No fue posible enviar el correo electrónico, avise a su administrador del sistema.');
                    }
                }
            });
        }
    });
}

function _mostrar_mensaje(Titulo, Mensaje) {
    bootbox.dialog({
        message: Mensaje,
        title: Titulo,
        locale: 'es',
        closeButton: true,
        buttons: [{
            label: 'Cerrar',
            className: 'btn-primary',
            callback: function () { }
        }]
    });
}


//cargar combo de sucursales
function _consultar_sucursales_inicio() {

    var $parametros = new Object();
    $parametros.Usuario = $('#username').val();

    var $data = JSON.stringify($parametros);

    try {
        $('#cmb_sucursal_inicio').select2({
            language: "es",
            theme: "classic",
            placeholder: 'Selecciona la sucursal',
            allowClear: true,
            ajax: {
                url: 'controllers/Autentificacion_Controller.asmx/Consultar_Sucursales_Inicio',
                cache: "true",
                dataType: 'json',
                type: "POST",
                delay: 250,
                cache: true,
                params: {
                    contentType: 'application/json; charset=utf-8'
                },
                quietMillis: 100,
                results: function (data) {
                    return { results: data };
                },
                data: function (params) {
                    return {
                        q: params.term,
                        page: params.page,
                        datos: $data
                    };
                },
                processResults: function (data, page) {
                    return {
                        results: data
                    };
                },
            },
            templateResult: _formato,
            templateSelection: _templateSelection
        });
    } catch (e) {
        _mostrar_mensaje('Informe Técnico', e);
    }
}



//validacion combo sucursal
function _validation() {
    var _output = new Object();

    _output.Estatus = true;
    _output.Mensaje = '';
    if ($('#cmb_sucursal_inicio').val() === '' || $('#cmb_sucursal_inicio').val() == undefined) {
        _add_class_error('#cmb_sucursal_inicio');
        _output.Estatus = false;
        _output.Mensaje += '&nbsp;<i class="fa fa-angle-double-right"></i>&nbsp;La sucursal es un dato requerido para iniciar sesión<br />';
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


function _add_class_error(selector) {
    $(selector).addClass('alert-danger');
}

function _remove_class_error(selector) {
    $(selector).removeClass('alert-danger');
}

function _clear_all_class_error() {
    $('#login-sucursal input[type=text]').each(function (index, element) {
        _remove_class_error('#' + $(this).attr('id'));
    });

    $('#login-sucursal select').each(function (index, element) {
        _remove_class_error('#' + $(this).attr('id'));
    });
}


function _limpiar_controles() {
    $('#cmb_sucursal_inicio').empty().trigger("change");
    _validation_sumary(null);
    _clear_all_class_error();
}


function _tooltip(_selector, _title, _tooltipAlign) {
    $(_selector).qtip({
        content: _title,
        position: {
            corner: {
                target: 'topMiddle',
                tooltip: _tooltipAlign
            }
        },
        show: {
            when: { event: 'mouseover' },
            ready: false
        },
        hide: { event: 'mouseout' },
        style: {
            border: {
                width: 5,
                radius: 7
            },
            padding: 5,
            textAlign: 'center',
            tip: {
                corner: true,
                method: "polygon",
                border: 1,
                height: 20,
                width: 9
            },
            background: '#fff',
            color: '#2d2d30',
            width: 200,
            'font-size': 'small',
            'font-family': 'Calibri',
            'font-weight': 'Bold',
            tip: true,
            name: 'dark'
        }
    });
}

function _formato(row) {
    if (!row.id) { return row.text; }
    else if (row.id == row.text) return row.text;

    var _salida = '<span style="text-transform:uppercase;">' +
        '<i class="fa fa-angle-double-right"></i>&nbsp;' + row.text +
        '</span>';

    return $(_salida);
}

function _templateSelection(row) {
    if (!row.id) { return row.text; }
    else if (row.id == row.text) return row.text;

    var _salida = '<span style="text-transform:uppercase;">' +
        '<i class="fa fa-angle-double-right"></i>&nbsp;' + row.text +
        '</span>';

    return $(_salida);
}

//*Método que se encarga de ingresar al sistema con las cookie previamente cargadas*//
function _ingresar_cookies_cargadas_directamente() {
    if ($.cookie('username') != null && $.cookie('password') != null) {
        $("#remember").prop("checked", true);
        var $parametros = new Object();
        $parametros.Usuario = $.cookie('username');
        $parametros.Password = $.cookie('password');

        var $data = JSON.stringify({ 'jsonObject': JSON.stringify($parametros) });

        $.ajax({
            url: 'controllers/Autentificacion_Controller.asmx/Pre_autentificacion',
            data: $data,
            type: 'POST',
            cache: false,
            async: false,
            contentType: 'application/json; charset=UTF-8',
            dataType: 'json',
            success: function (resul) {
                var $resultado = JSON.parse(resul.d);

                if ($resultado.Estatus === 'success') {
                    //si se accede directo o no
                    if ($resultado.LoginDirectly === true) {
                        window.location.href = '../Paginas_Generales/Frm_Apl_Principal.aspx';
                    }
                    else {
                        _consultar_sucursales_inicio();
                        $('#login-container').hide(1000);
                        $('#login-sucursal').show(1000);
                    }

                }//fin else de error
            }
        });
    }
}