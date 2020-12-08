var _index = null;
var row_partida = null;


$(document).on('ready', function () {
    _load_vistas();
});



function _load_vistas() {
    _launchComponent('vistas/Parametros_Eventos/Operacion.html', 'Operacion');
}


function _launchComponent(component, id) {

    $('#' + id).load(component, function () {

        switch (id) {
            case 'Operacion':
                _inicializar_vista_procesos();
                break;
        }
    });
}



//  *******************************************************************************************************************************
//  *******************************************************************************************************************************
////*********************Inicializar********************///
//  *******************************************************************************************************************************
//function _inicializar_vista_principal() {
//    try {
//        crear_tabla_vehiculo();
//        _set_location_toolbar('toolbar');
//        _load_cmb_estatus('cmb_estatus_filtro');
//        _eventos_principal();
//        _mostrar_vista('Principal');


//    } catch (e) {
//        _mostrar_mensaje('Error Técnico', e);
//    }
//}
//  -----------------------------------------------------
//  -----------------------------------------------------
function _inicializar_vista_procesos() {
    try {

        _mostrar_vista('Operacion');
        _set_location_toolbar('toolbar');
        _keyDownInt('txt_puntos_penalizacion');
        _limpiar_todos_controles_procesos();
        _eventos_procesos();
        _Consultar();

    } catch (e) {
        _mostrar_mensaje('Error Técnico', e);
    }
}
//  -----------------------------------------------------
//  -----------------------------------------------------
function _limpiar_todos_controles_procesos() {

    try {

        $('input[type=text]').each(function () { $(this).val(''); });
        $('input[type=hidden]').each(function () { $(this).val(''); });

    } catch (e) {
        _mostrar_mensaje('Error Técnico', 'limpiar controles. ' + e);
    }
}
//  ---------------------------------------------------------------------------------
//  ---------------------------------------------------------------------------------
function _eventos_procesos() {
    try {
        //  ---------------------------------------------------------------------------------
        //  ---------------------------------------------------------------------------------
        $('#btn_guardar').on('click', function (e) {
            e.preventDefault();

            var _validacion = _validar_datos_requeridos();

            if (_validacion.Estatus) {
                alta()
            }
            else {
                _mostrar_mensaje('Información', _validacion.Mensaje);
            }
        });
        //  ---------------------------------------------------------------------------------
        //  ---------------------------------------------------------------------------------

    } catch (e) {
        _mostrar_mensaje('Error Técnico', e);
    }
}



function _validar_datos_requeridos() {
    var _output = new Object();

    try {
        _output.Estatus = true;
        _output.Mensaje = '';
        //  ---------------------------------------------------------------------------------------
        //  ---------------------------------------------------------------------------------------
        //  puntos de penalizacion
        if ($('#txt_puntos_penalizacion').val() == '' || $('#txt_puntos_penalizacion').val() == undefined) {
            _output.Estatus = false;
            _output.Mensaje += '&nbsp;<i class="fa fa-angle-double-right"></i>&nbsp;Los puntos de penalizacion.<br />';
        }


    } catch (e) {
        _mostrar_mensaje('Informe técnico' + '[_validar_datos_requeridos]', e);
    } finally {
        return _output;
    }
}


//  -----------------------------------------------------
//  -----------------------------------------------------
function _mostrar_vista(vista_) {

    switch (vista_) {
        case "Principal":
            $('#Operacion').hide();
            $('#Principal').show();
            break;
        case "Operacion":
            $('#Operacion').show();
            $('#Principal').hide();
            break;
    }
}
//  -----------------------------------------------------
//  -----------------------------------------------------
function _set_location_toolbar(toolbar) {
    $('#' + toolbar).parent().removeClass("pull-left");
    $('#' + toolbar).parent().addClass("pull-right");

}





function _keyDownInt(id) {
    $('#' + id).on('keydown', function (e) {

        //alert("entro int");//_remove_class_error('#' + $(this).attr('id'));

        // Allow: backspace, delete, tab, escape, enter
        if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110]) !== -1 ||
            // Allow: Ctrl+A, Command+A
            (e.keyCode === 65 && (e.ctrlKey === true || e.metaKey === true)) ||
            // Allow: home, end, left, right, down, up
            (e.keyCode >= 35 && e.keyCode <= 40)) {
            // let it happen, don't do anything
            return;
        }
        // Ensure that it is a number and stop the keypress
        if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
            e.preventDefault();
        }
    });
}

//  *******************************************************************************************************************************
//  *******************************************************************************************************************************
///********************CONSULTAS**************///
//  *******************************************************************************************************************************
function _Consultar() {
    var filtros = null;
    try {
        filtros = new Object();
        filtros.Estatus = 'ACTIVO';


        var $data = JSON.stringify({ 'jsonObject': JSON.stringify(filtros) });

        $.ajax({
            type: 'POST',
            url: 'controllers/Parametros_Eventos_Controller.asmx/Consultar_Parametros',
            data: $data,
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            async: false,
            cache: false,
            success: function (datos) {
                if (datos !== null) {
                    datos = JSON.parse(datos.d);

                    _limpiar_todos_controles_procesos();

                    if (datos.length > 0) {
                        $('#txt_parametro_id').val(datos[0].Parametro_ID);
                        $('#txt_puntos_penalizacion').val(datos[0].Puntos_Penalizacion);
                    }
                    else {
                        $('#txt_parametro_id').val(0);
                    }
                }
                else {
                    $('#txt_parametro_id').val(0);
                }
            }
        });
    } catch (e) {
        _mostrar_mensaje('Error ', e);
    }
}




///********************BD***************///
function alta() {
    var obj = new Object();

    try {

        obj.Parametro_ID = parseInt($('#txt_parametro_id').val());
        obj.Puntos_Penalizacion = parseInt($('#txt_puntos_penalizacion').val());
        obj.Estatus = 'ACTIVO'
        var $data = JSON.stringify({ 'jsonObject': JSON.stringify(obj) });

        $.ajax({
            type: 'POST',
            url: 'controllers/Parametros_Eventos_Controller.asmx/Alta',
            data: $data,
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            async: false,
            cache: false,
            success: function (datos) {
                if (datos != null) {
                    var result = JSON.parse(datos.d);
                    if (result.Estatus == 'success') {
                      
                        _mostrar_mensaje(result.Titulo, result.Mensaje);
                        _Consultar();

                    } else {
                        _mostrar_mensaje(result.Titulo, result.Mensaje);
                    }
                }
            }
        });

    } catch (e) {
        alert(e.message)
    }

}

function parseDate(dateString) {
    //Intercambia el dia y el mes de los formatos de fecha( DD/MM/YYYY o MM/DD/YYYY )
    var dateTime = dateString.split(" ");
    var dateOnly = dateTime[0];
    var dates = dateOnly.split("/");
    var temp = dates[1] + "/" + dates[0] + "/" + dates[2];
    return temp;
}