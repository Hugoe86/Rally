var bloqueado = "";
var _modal_open = "";

$(document).on('ready', function () {
    //_bloquear_pantallas();
    //_bloquear_desbloquear_pantalla();

    setTimeout(_verificar_bloqueo, 1);
    $("#contenedor").hide();
    $.sessionTimeout({
        keepAliveUrl: '../../KeepSessionAlive.ashx',
        keepAlive: true,
        keepAliveInterval: 600000,//(1 hora)
        warnAfter: 1080000,//Aviso a los 18 minutos de que la sesión esta por terminar (milliseconds ).
        redirAfter: 3600000,//Aviso a los 20 minutos que la sesión ha caducado (milliseconds ).
        onWarn: function () {
            _bloquear_pantallas();
            _bloquear_desbloquear_pantalla();
        },
        onRedir: function () {
            $.ajax({
                url: '../../Paginas/Paginas_Generales/controllers/Autentificacion_Controller.asmx/cerrar_sesion',
                type: 'POST',
                cache: false,
                async: false,
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                success: function (data) {
                    bootbox.dialog({
                        message: 'Tu sesión ha expirado',
                        title: '<i class="fa fa-exclamation-triangle"></i>&nbsp;Información',
                        locale: 'es',
                        closeButton: false,
                        buttons: [{
                            label: 'Cerrar',
                            className: 'btn-default',
                            callback: function () { window.location.href = "../Paginas_Generales/Frm_Apl_Login.html"; }
                        }]
                    });
                }
            });
        }
    });
});

function _mostrar_mensaje(Titulo, Mensaje) {
    bootbox.dialog({
        message: Mensaje,
        title: Titulo,
        locale: 'es',
        closeButton: false,
        buttons: [{
            label: 'Cerrar',
            className: 'btn-default',
            callback: function () { }
        }]
    });
}

//****FUNCION PARA BLOQUEAR PANTALLAS SI EXISTE UN BLOQUEO PREVIO****//
function _bloquear_pantallas() {
    $.ajax({
        type: 'POST',
        url: '../Paginas_Generales/controllers/Autentificacion_Controller.asmx/bloqueo_pantalla',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        cache: false,
        success: function (result) {
            var $_datos = JSON.parse(result.d);

            if ($_datos !== null && $_datos !== undefined) {
                if ($_datos.Estatus === 'success') {
                    
                } else {
                    _mostrar_mensaje('Información', 'Problemas al realizar el bloqueo pantalla.');
                }
            } else {
                _mostrar_mensaje('Información', 'Problemas al realizar el bloqueo pantalla.');
            }
        }
    });
}

//****FUNCION PARA BLOQUEAR-DESBLOQUEAR PANTALLAS SI EXISTE UN BLOQUEO PREVIO****//
function _bloquear_desbloquear_pantalla() {
    setTimeout(function () { $(".fade-in-effect").addClass('in'); }, 1);
    $('#passwd').val('');
    if (bloqueado === "SI") {
        _modal_open = ($('#modal_datos').is(':visible')) ? "visible" : "";
        $('#modal_datos').css('display', 'none');
        $('.modal-backdrop.in').css('display', 'none');
        $('#access').focus();

        $("#form1").hide();
        $("#contenedor").show();
        $("body").addClass("page-body lockscreen-page");
    }
    else {
        _modal_open = ($('#modal_datos').is(':visible')) ? "visible" : "";
        $('#modal_datos').css('display', 'none');
        $('.modal-backdrop.in').css('display', 'none');
        $('#access').focus();

        $("#form1").hide();
        $("#contenedor").show();
        $("body").addClass("page-body lockscreen-page");
    }
    // Clicking on thumbnail will focus on password field
    $(".user-thumb a").on('click', function (ev) {
        ev.preventDefault();
        $("#passwd").focus();
    });


    // Form validation and AJAX request
    $(".lockcreen-form").validate({
        rules: {
            passwd: {
                required: true
            }
        },

        messages: {
            passwd: {
                required: 'Please enter your password.'
            }
        },

        submitHandler: function (form) {
            show_loading_bar(70); // Fill progress bar to 70% (just a given value)
            var $parametros = new Object();
            $parametros.Usuario = $('#username').val();
            $parametros.Password = $('#passwd').val();
            $parametros.Sucursal_ID = $('#cmb_sucursal_inicio').val();

            opts = {
                "closeButton": true,
                "debug": false,
                "positionClass": "toast-top-full-width",
                "onclick": null,
                "showDuration": "300",
                "hideDuration": "1000",
                "timeOut": "5000",
                "extendedTimeOut": "1000",
                "showEasing": "swing",
                "hideEasing": "linear",
                "showMethod": "fadeIn",
                "hideMethod": "fadeOut"
            };


            var $data = JSON.stringify({ 'jsonObject': JSON.stringify($parametros) });

            $.ajax({
                url: '../Paginas_Generales/controllers/Autentificacion_Controller.asmx/autentificar_bloqueo',
                data: $data,
                type: 'POST',
                cache: false,
                async: false,
                contentType: 'application/json; charset=UTF-8',
                dataType: 'json',
                success: function (resp) {
                    show_loading_bar({
                        delay: .5,
                        pct: 100,
                        finish: function () {
                            var $resultado = JSON.parse(resp.d);
                            if ($resultado.Mensaje == "Desbloqueo") {

                                if (_modal_open != "")
                                {
                                    $('#modal_datos').css('display', 'block');
                                    $('.modal-backdrop.in').css('display', 'block');
                                    _modal_open = "";
                                }

                                $("#contenedor").hide();
                                $("body").removeClass("page-body lockscreen-page");
                                $("#form1").show();
                                var audioElement = document.createElement('audio');
                                audioElement.setAttribute('src', '../../Recursos/sound/android_unlock.mp3');
                                audioElement.setAttribute('autoplay', 'autoplay');
                                audioElement.play();
                            }

                            else {
                                toastr.error("Password incorrecto", '<i class="fa fa-exclamation-triangle" aria-hidden="true"></i>' + " Acceso Incorrecto", opts);
                                $('#passwd').select();
                            }
                        }
                    });
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    console.log(textStatus, errorThrown);
                }
            });
        }
    });

    // Set Form focus
    $("form#lockscreen .form-group:has(.form-control):first .form-control").focus();
}

//****FUNCION PARA BLOQUEAR PANTALLAS SI EXISTE UN BLOQUEO PREVIO****//
function _verificar_bloqueo() {
    $.ajax({
        type: 'POST',
        url: '../Paginas_Generales/controllers/Autentificacion_Controller.asmx/verificar_bloqueo_sistema',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        cache: false,
        success: function (result) {
            var $_datos = JSON.parse(result.d);

            if ($_datos !== null && $_datos !== undefined) {
                if ($_datos.Estatus === 'success') {
                    if ($_datos.Mensaje === 'SI') {
                        _bloquear_pantallas();
                        _bloquear_desbloquear_pantalla();
                        bloqueado = "SI";
                    }
                } else {
                    _mostrar_mensaje('Información', 'Problemas bloqueo pantalla.');
                }
            } else {
                _mostrar_mensaje('Información', 'Problemas bloqueo pantalla.');
            }
        }
    });
}