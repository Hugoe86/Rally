$(document).on('ready', function () {
    _inicializar_pagina();
});

function _inicializar_pagina() {

    _Consultar();
    _eventos();
    _eventos_textbox();
}

function _alta_parametro() {
    var parametro = null;
    var isComplete = false;

    try {

        parametro = new Object();
        parametro.Email = $('#txt_email').val();
        parametro.Contrasena = $('#txt_contrasena').val();
        parametro.Puerto = $('#txt_puerto').val() == '' ? -1 : parseInt($('#txt_puerto').val());
        parametro.Host = $('#txt_host').val();
        parametro.UseDefaultCredentials = $('#chck_usedefaultcredentials').prop('checked');
        parametro.EnableSsl = $('#chck_enablessl').prop('checked');
        parametro.Url_Jira_Service = $('#txt_url_jira_service').val();
        parametro.Usuario_Jira = $('#txt_usuario_jira').val();
        parametro.Password_Jira = $('#txt_password_jira').val();
        parametro.Name_Jira_Project = $('#txt_name_jira_project').val();

        var $data = JSON.stringify({ 'jsonObject': JSON.stringify(parametro) });

        $.ajax({
            type: 'POST',
            url: 'controllers/Parametros_Controller.asmx/Alta_Parametro',
            data: $data,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            cache: false,
            success: function (result) {
                var Resultado = JSON.parse(result.d);
                if (Resultado != null && Resultado != undefined && Resultado != '') {
                    if (Resultado.Estatus == 'success') {
                        _mostrar_mensaje('Información', '<i class="fa fa-check" style="font-size: 25px;"></i>&nbsp;&nbsp;' + 'Cambios guardados con éxito.');
                        _Consultar();
                        isComplete = true;
                    } else if (Resultado.Estatus == 'error') {
                        //_validation_sumary(Resultado);
                    }
                } else {
                    //_validation_sumary(Resultado);
                }
            }
        });
    } catch (e) {
        _mostrar_mensaje('Informe Tecnico', e);
    }
    return isComplete;
}


function _modificar_parametros() {
    var parametro = null;
    var isComplete = false;

    try {
        parametro = new Object();
        parametro.Parametro_ID = parseInt($('#txt_parametro_id').val());
        parametro.Email = $('#txt_email').val();
        parametro.Contrasena = $('#txt_contrasena').val();
        parametro.Puerto = $('#txt_puerto').val() == '' ? -1 : parseInt($('#txt_puerto').val());
        parametro.Host = $('#txt_host').val();
        parametro.UseDefaultCredentials = $('#chck_usedefaultcredentials').prop('checked');
        parametro.EnableSsl = $('#chck_enablessl').prop('checked');
        parametro.Url_Jira_Service = $('#txt_url_jira_service').val();
        parametro.Usuario_Jira = $('#txt_usuario_jira').val();
        parametro.Password_Jira = $('#txt_password_jira').val();
        parametro.Name_Jira_Project = $('#txt_name_jira_project').val();

        var $data = JSON.stringify({ 'jsonObject': JSON.stringify(parametro) });

        $.ajax({
            type: 'POST',
            url: 'controllers/Parametros_Controller.asmx/Actualizar',
            data: $data,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            cache: false,
            success: function (result) {
                var Resultado = JSON.parse(result.d);
                if (Resultado != null && Resultado != undefined && Resultado != '') {
                    if (Resultado.Estatus == 'success') {
                        _mostrar_mensaje('Información', '<i class="fa fa-check" style="font-size: 25px;"></i>&nbsp;&nbsp;' + 'Cambios guardados con éxito.');
                        _Consultar();
                        isComplete = true;
                    } else if (Resultado.Estatus == 'error') {
                        //_validation_sumary(Resultado);
                    }
                } else {
                    // _validation_sumary(Resultado);
                }
            }
        });

    } catch (e) {
        _mostrar_mensaje('Informe Tecnico', e);
    }
    return isComplete;
}



function _Consultar() {
    var filtros = null;
    try {
        show_loading_bar({
            pct: 78,
            wait: .5,
            delay: .5,
            finish: function (pct) {
                jQuery.ajax({
                    type: 'POST',
                    url: 'controllers/Parametros_Controller.asmx/Consultar_Parametro',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: true,
                    cache: false,
                    success: function (datos) {
                        if (datos !== null) {
                            var resultado = $.parseJSON(datos.d);
                            $('#txt_parametro_id').val(resultado.Parametro_ID);
                            $('#txt_email').val( resultado.Email);
                            $('#txt_contrasena').val(resultado.Contrasena);
                            $('#txt_puerto').val(resultado.Puerto);
                            $('#txt_host').val(resultado.Host);
                                $('#chck_usedefaultcredentials').prop('checked', resultado.UseDefaultCredentials);
                                $('#chck_enablessl').prop('checked', resultado.EnableSsl);
                                $('#txt_url_jira_service').val(resultado.Url_Jira_Service);
                                $('#txt_usuario_jira').val(resultado.Usuario_Jira);
                                $('#txt_password_jira').val(resultado.Password_Jira);
                                $('#txt_name_jira_project').val(resultado.Name_Jira_Project);
                        }
                        hide_loading_bar();
                    }
                });
            }
        });
    } catch (e) {
        _mostrar_mensaje('Informe Técnico', e);
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
            className: 'btn-default',
            callback: function () { }
        }]
    });
}

function _eventos() {
    try {
        $('#btn_guardar').click(function (e) {
            if ($('#txt_parametro_id').val() == null || $('#txt_parametro_id').val() == "" || $('#txt_parametro_id').val() == undefined)
            {
                _alta_parametro();
            }
            else
            {
                _modificar_parametros();
            }
        });

        $('#btn_salir').on('click', function (e) { e.preventDefault(); window.location.href = '../Paginas_Generales/Frm_Apl_Principal.aspx'; });
    } catch (e) {
        _mostrar_mensaje('Informe Técnico', e);
    }
}

function _eventos_textbox() {

    $('#txt_puerto').on('blur', function () {
        //$(this).val($(this).val().match(/^[0-9]+$/) ? $(this).val() : '');
        $(this).val($(this).val().match(/^[0-9]+$/) ? $(this).val() : $(this).val().replace(/[^0-9]/g, ''));
    });
}