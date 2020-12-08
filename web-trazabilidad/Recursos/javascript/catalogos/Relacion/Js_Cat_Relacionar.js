
$(document).on('ready', function () {
    _load_vistas_relacionar();
});




function _load_vistas_relacionar() {
    _launchComponent_relacionar('vistas/Relacion/Principal_Relacion.html', 'Principal_Relacion');


}


function _launchComponent_relacionar(component, id) {

    $('#' + id).load(component, function () {

        switch (id) {
            case 'Principal_Relacion':
                _inicializar_vista_principal_relacion();
                break;
           
        }
    });
}


//  *******************************************************************************************************************************
//  *******************************************************************************************************************************
////*********************Inicializar********************///
//  *******************************************************************************************************************************
function _inicializar_vista_principal_relacion() {
    try {
        crear_tabla_relaciones();
        _load_cmb_participante();
        _load_cmb_vehiculos_();
        _eventos_relacionar();

    } catch (e) {
        _mostrar_mensaje('Error Técnico', e);
    }
}

function crear_tabla_relaciones() {

    try {
        $('#tbl_relacion_participante_vehiculo').bootstrapTable('destroy');
        $('#tbl_relacion_participante_vehiculo').bootstrapTable({
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
            columns: [
                { field: 'Relacion_Id', title: 'Relacion_Id', align: 'left', valign: 'top', visible: false },
                { field: 'Participante_Id', title: 'Participante_Id', align: 'left', valign: 'top', visible: false, sortable: true },
                { field: 'Participante', title: 'Participante', align: 'left', valign: 'top', visible: false, sortable: true },
                { field: 'Vehiculo_Id', title: 'Vehiculo_Id', align: 'left', valign: 'top', visible: false, sortable: true },
                { field: 'Vehiculo', title: 'Vehiculo (marca - modelo - año - placa)', align: 'left', valign: 'top', visible: true, sortable: true },
              

                  {
                      field: 'Eliminar',
                      title: 'Eliminar',
                      width: 100,
                      align: 'right',
                      valign: 'top',
                      halign: 'center',

                      formatter: function (value, row) {

                          var opciones = '<div style=" text-align: center;">';
                          opciones += '<div style="display:block"><a class="remove ml10 text-red" id="' + row.Relacion_Id + '" href="javascript:void(0)" data-orden=\'' + JSON.stringify(row) + '\' onclick="btn_eliminar_relacion_click(this);" title="Eliminar"><i class="glyphicon glyphicon-remove "></i>&nbsp;<span style="font-size:11px !important;"></span></a></div>';
                          opciones += '</div>';

                          return opciones;
                      }
                  }
            ]
        });
    } catch (e) {
        _mostrar_mensaje('Informe Técnico' + '[crear_tabla_relaciones]', e.message);
    }
}

function _load_cmb_participante() {
    try {
        $('#cmb_participante_relacion').select2({
            language: "es",
            theme: "classic",
            placeholder: 'SELECCIONE',
            allowClear: true,
            ajax: {
                url: 'controllers/Relacionar_Controller.asmx/consultar_participantes_Combo',
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
                        page: params.page
                    };
                },
                processResults: function (data, page) {
                    return {
                        results: data
                    };
                },
            }
        });



        /* =============================================
       --NOMBRE_FUNCIÓN:       $(cmb).on("select2:select", function (evt) {
       --DESCRIPCIÓN:          Habilita la seccion de las cuentas
       --PARÁMETROS:           evt: parametro que se refiere al evento click
       --CREO:                 Hugo Enrique Ramírez Aguilera
       --FECHA_CREO:           24 Octubre de 2019
       --MODIFICÓ:
       --FECHA_MODIFICÓ:
       --CAUSA_MODIFICACIÓN:
       =============================================*/
        $('#cmb_participante_relacion').on("select2:select", function (evt) {

            _consultar_relacion_participante_vehiculos();

            //  limpia el valor del combo
            $('#cmb_vehiculo_relacion').empty().trigger("change");
        });


        /* =============================================
       --NOMBRE_FUNCIÓN:       $(cmb).on("select2:select", function (evt) {
       --DESCRIPCIÓN:          Bloquea el combo de cuentas
       --PARÁMETROS:           evt: parametro que se refiere al evento click
       --CREO:                 Hugo Enrique Ramírez Aguilera
       --FECHA_CREO:           24 Octubre de 2019
       --MODIFICÓ:
       --FECHA_MODIFICÓ:
       --CAUSA_MODIFICACIÓN:
       =============================================*/
        $('#cmb_participante_relacion').on("select2:unselect", function (evt) {

            _limpiar_todos_controles_modal_relacionar();

        });


    } catch (e) {
        _mostrar_mensaje('Technical Report', e);
    }
}




function _load_cmb_vehiculos_() {
    try {
        $('#cmb_vehiculo_relacion').select2({
            language: "es",
            theme: "classic",
            placeholder: 'SELECCIONE',
            allowClear: true,
            ajax: {
                url: 'controllers/Relacionar_Controller.asmx/consultar_vehiculos_Combo',
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
                        page: params.page
                    };
                },
                processResults: function (data, page) {
                    return {
                        results: data
                    };
                },
            }
        });


    } catch (e) {
        _mostrar_mensaje('Technical Report', e);
    }
}



function _eventos_relacionar() {
    try {
    
        $('#btn_busqueda_relacion').on('click', function (e) {
            e.preventDefault();
            _consultar_relacion_participante_vehiculos();

        });

        $('#btn_agregar_relacion').on('click', function (e) {
            e.preventDefault();

            var valida_datos_requerido = _validar_nueva_relacion();
            if (valida_datos_requerido.Estatus) {

                alta_relacion();
               
            }
            else {
                _mostrar_mensaje('Información', valida_datos_requerido.Mensaje);
            }
        });

    } catch (e) {
        _mostrar_mensaje('Error Técnico', e);
    }
}



//  *******************************************************************************************************************************
//  *******************************************************************************************************************************
///********************validaciones**************///
//  *******************************************************************************************************************************
function _consultar_relacion_participante_vehiculos() {
    var filtros = null;
    try {


        $('#tbl_relacion_participante_vehiculo').bootstrapTable('load', []);

        var valida_datos_requerido = _validar_busqueda();
        if (valida_datos_requerido.Estatus) {

            filtros = new Object();

            filtros.Participante_Id = $('#cmb_participante_relacion').val() === null ? 0 : parseInt($('#cmb_participante_relacion').val());

            var $data = JSON.stringify({ 'jsonObject': JSON.stringify(filtros) });

            $.ajax({
                type: 'POST',
                url: 'controllers/Relacionar_Controller.asmx/consultar_vehiculos_relacionados_filtro',
                data: $data,
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                async: false,
                cache: false,
                success: function (datos) {
                    if (datos !== null) {
                        datos = JSON.parse(datos.d);
                        $('#tbl_relacion_participante_vehiculo').bootstrapTable('load', datos);
                    }
                    else {
                    }
                }
            });
        }
        else {
            _mostrar_mensaje('Información', valida_datos_requerido.Mensaje);
        }

    } catch (e) {
        _mostrar_mensaje('Error ', e);
    }
}

function _validar_nueva_relacion() {

    var _output = new Object();

    try {
        _output.Estatus = true;
        _output.Mensaje = '';
        //  ---------------------------------------------------------------------------------------
        //  ---------------------------------------------------------------------------------------

        if ($('#cmb_participante_relacion').val() == '' || $('#cmb_participante_relacion').val() == undefined) {
            _output.Estatus = false;
            _output.Mensaje += '&nbsp;<i class="fa fa-angle-double-right"></i>&nbsp;El participante.<br />';
        }

        if ($('#cmb_vehiculo_relacion').val() == '' || $('#cmb_vehiculo_relacion').val() == undefined) {
            _output.Estatus = false;
            _output.Mensaje += '&nbsp;<i class="fa fa-angle-double-right"></i>&nbsp;El vehiculo.<br />';
        }

        if (_output.Mensaje != "") {
            _output.Mensaje = "Favor de proporcionar lo siguiente: <br />" + _output.Mensaje;
        }

    } catch (e) {
        _mostrar_mensaje('Informe técnico' + '[_validar_nueva_relacion]', e);
    } finally {
        return _output;
    }
}


///*******************Validaciones*************************///
function _validar_busqueda() {
    var _output = new Object();

    try {
        _output.Estatus = true;
        _output.Mensaje = '';
        //  ---------------------------------------------------------------------------------------
        //  ---------------------------------------------------------------------------------------
        
        if ($('#cmb_participante_relacion').val() == '' || $('#cmb_participante_relacion').val() == undefined) {
            _output.Estatus = false;
            _output.Mensaje += '&nbsp;<i class="fa fa-angle-double-right"></i>&nbsp;El participante.<br />';
        }
        


        if (_output.Mensaje != "") {
            _output.Mensaje = "Favor de proporcionar lo siguiente: <br />" + _output.Mensaje;
        }


    } catch (e) {
        _mostrar_mensaje('Informe técnico' + '[_validarDatos_Nuevo]', e);
    } finally {
        return _output;
    }
}




///********************BD***************///
function alta_relacion() {
    var obj = new Object();
   

    try {

        obj.Participante_Id = parseInt($('#cmb_participante_relacion').val());
        obj.Vehiculo_Id = parseInt($('#cmb_vehiculo_relacion').val());

        

        var $data = JSON.stringify({ 'jsonObject': JSON.stringify(obj) });

        $.ajax({
            type: 'POST',
            url: 'controllers/Relacionar_Controller.asmx/alta_relacion',
            data: $data,
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            async: false,
            cache: false,
            success: function (datos) {
                if (datos != null) {
                    var result = JSON.parse(datos.d);
                    if (result.Estatus == 'success') {
                        _mostrar_vista('Principal');
                        _mostrar_mensaje(result.Titulo, result.Mensaje);
                        _consultar_relacion_participante_vehiculos();
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


function btn_eliminar_relacion_click(tab) {
    var row = $(tab).data('orden');

    bootbox.confirm({
        title: 'Eliminar Registro',
        message: 'Esta seguro de Eliminar la relación del vehiculo?',
        callback: function (result) {
            if (result) {

                //  documentos
                var filtros = null;
                filtros = new Object();

                filtros.Relacion_Id = row.Relacion_Id;
                var $data = JSON.stringify({ 'jsonObject': JSON.stringify(filtros) });

                $.ajax({
                    type: 'POST',
                    url: 'controllers/Relacionar_Controller.asmx/Eliminar',
                    data: $data,
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    cache: false,
                    success: function (datos) {
                        if (datos !== null) {
                            datos = JSON.parse(datos.d);

                            _consultar_relacion_participante_vehiculos();
                        }
                    }
                });
            }
        }
    });
}



/* =============================================
--NOMBRE_FUNCIÓN:       _limpiar_todos_controles_modal_relacionar
--DESCRIPCIÓN:          limpia todos los controles que se encuentran dentro del modal
--PARÁMETROS:           NA
--CREO:                 Hugo Enrique Ramírez Aguilera
--FECHA_CREO:           27 de Julio de 2020
--MODIFICÓ:
--FECHA_MODIFICÓ:
--CAUSA_MODIFICACIÓN:
=============================================*/
function _limpiar_todos_controles_modal_relacionar() {

    try {

        /* =============================================
        --NOMBRE_FUNCIÓN:       input[type=text]
        --DESCRIPCIÓN:          recorre los controles de tipo texto y limpia su valor
        --PARÁMETROS:           NA
        --CREO:                 Hugo Enrique Ramírez Aguilera
        --FECHA_CREO:           27 de Julio de 2020
        --MODIFICÓ:
        --FECHA_MODIFICÓ:
        --CAUSA_MODIFICACIÓN:
        =============================================*/
        $('#div_relacionar input[type=text]').each(function () { $(this).val(''); });

        /* =============================================
       --NOMBRE_FUNCIÓN:       input[type=hidden]
       --DESCRIPCIÓN:          recorre los controles de tipo hidden y limpia su valor
       --PARÁMETROS:           NA
       --CREO:                 Hugo Enrique Ramírez Aguilera
       --FECHA_CREO:           27 de Julio de 2020
       --MODIFICÓ:
       --FECHA_MODIFICÓ:
       --CAUSA_MODIFICACIÓN:
       =============================================*/
        $('#div_relacionar input[type=hidden]').each(function () { $(this).val(''); });


        //  limpia el valor del combo
        $('#cmb_vehiculo_relacion').empty().trigger("change");


        //  se limpian las tablas
        $('#tbl_relacion_participante_vehiculo').bootstrapTable('load', []);


    } catch (e) {//  variable para atrapar el error

        //  se muestra el mensaje del error que se presento
        _mostrar_mensaje('Error Técnico' + ' [_limpiar_todos_controles_modal_relacionar] ', 'limpiar controles. ' + e);
    }
}
