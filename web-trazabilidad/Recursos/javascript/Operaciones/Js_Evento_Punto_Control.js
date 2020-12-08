

//  -----------------------------------------------------
//  -----------------------------------------------------
function _inicializar_vistapuntoControl() {
    try {
        _eventos_procesos_punto_control();

        _load_cmb_cargo('cmb_cargo_operador_responsable');
        _load_cmb_responsables_operadores();

        crear_tabla_responsable_operador();
        crear_tabla_responsable_operador_eliminar();

        crear_tabla_categorias_tiemposIdeales();
        crear_tabla_categorias_tiemposIdeales_Eliminar();

        _inicializar_horas_operacion_jornadas('dtp_txt_hora_llegada_operador');
        _inicializar_horas_operacion_jornadas('dtp_txt_hora_salida_operador');
        //_inicializar_horas_operacion_jornadas('dtp_tiempo_ideal_punto_control');
        _inicializar_horas_operacion_jornadas('dtp_hora_inicio_punto_control');
        _inicializar_horas_operacion_jornadas('dtp_hora_termino_punto_control');

        //_inicializar_horas_operacion_jornadas('dtp_txt_tiempo_ideal_categoria');

        //_keyDownInt('txt_intervalo_carros_punto_control');
        _keyDownInt('txt_numero_punto_control');
        _load_cmb_categoria_PuntoControl('cmb_categoria_tiempo_ideal');

    } catch (e) {
        _mostrar_mensaje('Error Técnico', e);
    }
}


//  -----------------------------------------------------
//  -----------------------------------------------------
function _load_cmb_categoria_PuntoControl(cmb) {
    try {
        var obj = new Object();
        obj.Evento_Id = parseInt($('#txt_evento_id').val());
        var $data = JSON.stringify({ 'jsonObject': JSON.stringify(obj) });


        $('#' + cmb).select2({
            language: "es",
            theme: "classic",
            placeholder: 'SELECCIONE',
            allowClear: true,
            ajax: {
                url: '../Operaciones/controllers/Eventos_VehiculosController.asmx/Consultar_Categoria_Vehiculo_Combo',
                cache: true,
                dataType: 'json',
                type: "POST",
                delay: 250,
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
                        evento_id: $('#txt_evento_id').val(),
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
        _mostrar_mensaje('Informe técnico' + '[_load_cmb_vehiculo]', e);
    }
}

//  -----------------------------------------------------
//  -----------------------------------------------------
function _load_cmb_cargo(cmb) {
    try {
        $('#' + cmb).select2({
            language: "es",
            theme: "classic",
            placeholder: 'SELECCIONE',
            allowClear: true,
            data: [
                {
                    id: '',
                    text: '',
                }, {
                    id: 'RESPONSABLE',
                    text: 'RESPONSABLE',
                },
                {
                    id: 'EQUIPO',
                    text: 'EQUIPO',
                },
            ],
        });

    } catch (e) {
        _mostrar_mensaje('Informe técnico' + '[_load_cmb_estatus]', e);
    }
}


//  -----------------------------------------------------
//  -----------------------------------------------------


function btn_nuevo_punto_control_click(tab) {

    $('#tbl_operadores_responsables').bootstrapTable('load', []);
    $('#tbl_operadores_responsables_eliminar').bootstrapTable('load', []);


    $('#tbl_categoria_tiempo_ideal').bootstrapTable('load', []);
    $('#tbl_categoria_tiempo_ideal_eliminar').bootstrapTable('load', []);



    var row = $(tab).data('orden');

    if ($('#txt_estatus').val() == "ACTIVO" || $('#txt_estatus').val() == "INACTIVO") {

        _limpiar_todos_controles_puntos_control();

        $('#txt_jornada_punto_control_id').val(row.Jornada_Id);
        $('#txt_clave_jornada_pnt').val(row.Clave);
        $('#txt_nombre_jornada_pnt').val(row.Nombre);

        $('#txt_fecha_punto_control').val(new Date(row.Fecha_Inicio).toString('dd/MM/yyyy'));

        _Consultar_Clave_Punto_Control_Automatica();

        _mostrar_vista_jornadas('Punto');
        _habilitar_controles_punto_control('Nuevo');
    }
    else {
        _mostrar_mensaje('Informe Técnico' + '', 'No puede ser editado');
    }
}

//  -----------------------------------------------------
//  -----------------------------------------------------


function _limpiar_todos_controles_puntos_control() {

    try {

        $('#PuntoControl input[type=text]').each(function () { $(this).val(''); });
        $('#PuntoControl input[type=hidden]').each(function () { $(this).val(''); });



        $('#cmb_operador_responsable').empty().trigger('change');

    } catch (e) {
        _mostrar_mensaje('Error Técnico', 'limpiar controles. ' + e);
    }
}

//  -----------------------------------------------------
//  -----------------------------------------------------


//  ---------------------------------------------------------------------------------
//  ---------------------------------------------------------------------------------
function _eventos_procesos_punto_control() {
    try {
        //  ---------------------------------------------------------------------------------
        //  ---------------------------------------------------------------------------------
        $('#btn_guardar_punto_control').click(function (e) {
            e.preventDefault();


            var title = $('#btn_guardar_punto_control').attr('title');

            var valida_datos_requerido = _validarDatos_Punto_Control_Nuevo();
            if (valida_datos_requerido.Estatus) {

                if (title == "Guardar") {
                    Alta_Jornada_Punto_Control()
                }
                else {
                    Modificar_Jornada_Punto_Control();
                }
            }
            else {
                _mostrar_mensaje('Información', valida_datos_requerido.Mensaje);
            }

        });
        //  ---------------------------------------------------------------------------------
        //  ---------------------------------------------------------------------------------
        $('#btn_cancelar_punto_Control').on('click', function (e) {
            e.preventDefault();
            _mostrar_vista_jornadas('Principal');
            _limpiar_todos_controles_puntos_control();
        });

        //  ---------------------------------------------------------------------------------
        //  ---------------------------------------------------------------------------------
        $('#btn_agregar_operador_responsable').on('click', function (e) {
            e.preventDefault();

            var valida_datos_requerido = _validarDatos_Responsable_Operador();

            if (valida_datos_requerido.Estatus) {

                $('#tbl_operadores_responsables').bootstrapTable('insertRow', {
                    index: 0,
                    row: {
                        Relacion_Operador_Id: 0,
                        Responsable_Id: $('#cmb_operador_responsable').val(),
                        Responsable: $('#cmb_operador_responsable').text(),
                        Punto_Control_Id: 0,
                        Evento_Id: 0,
                        Estatus: 'ACTIVO',
                        Cargo: $('#cmb_cargo_operador_responsable').val(),

                        Hora_Llegada: $('#txt_hora_llegada_operador').val(),
                        Hora_Salida: $('#txt_hora_salida_operador').val(),

                        Str_Hora_Llegada: $('#txt_hora_llegada_operador').val(),
                        Str_Hora_Salida: $('#txt_hora_salida_operador').val(),
                    }
                });

                $('#txt_hora_llegada_operador').val('');
                $('#txt_hora_salida_operador').val('');
                $('#cmb_cargo_operador_responsable').val('');
                $('#cmb_operador_responsable').val('');

            }
            else {

                _mostrar_mensaje('Información', valida_datos_requerido.Mensaje);

            }
        });


        //  ---------------------------------------------------------------------------------
        //  ---------------------------------------------------------------------------------
        $('#btn_agregar_tiempo_ideal_Categ').on('click', function (e) {
            e.preventDefault();

            var valida_datos_requerido = _validarDatos_Tiempo_Ideal_Categoria();

            if (valida_datos_requerido.Estatus) {

                $('#tbl_categoria_tiempo_ideal').bootstrapTable('insertRow', {
                    index: 0,
                    row: {
                        Relacion_Id: 0,
                        Punto_Control_Id: 0,
                        Categoria_Id: $('#cmb_categoria_tiempo_ideal').val(),
                        Categoria: $('#cmb_categoria_tiempo_ideal').text(),
                        Tiempo_Ideal: $('#txt_tiempo_ideal_categoria').val(),
                        Str_Tiempo_Ideal: $('#txt_tiempo_ideal_categoria').val(),
                    }
                });

                $('#txt_tiempo_ideal_categoria').val('');
                $('#cmb_categoria_tiempo_ideal').val('');

            }
            else {

                _mostrar_mensaje('Información', valida_datos_requerido.Mensaje);

            }
        });

        //  ---------------------------------------------------------------------------------
        //  ---------------------------------------------------------------------------------
    } catch (e) {
        _mostrar_mensaje('Error Técnico', e);
    }
}



///*******************Validaciones*************************///
//  -----------------------------------------------------
//  -----------------------------------------------------
function _validarDatos_Punto_Control_Nuevo() {
    var _output = new Object();

    try {
        _output.Estatus = true;
        _output.Mensaje = '';
        //  ---------------------------------------------------------------------------------------
        //  ---------------------------------------------------------------------------------------
        //  datos del evento
        if ($('#txt_clave_punto_control').val() == '' || $('#txt_clave_punto_control').val() == undefined) {
            _output.Estatus = false;
            _output.Mensaje += '&nbsp;<i class="fa fa-angle-double-right"></i>&nbsp;La clave del punto de control.<br />';
        }


        if ($('#txt_numero_punto_control').val() == '' || $('#txt_numero_punto_control').val() == undefined) {
            _output.Estatus = false;
            _output.Mensaje += '&nbsp;<i class="fa fa-angle-double-right"></i>&nbsp;El numero del punto de control.<br />';
        }

        if ($('#txt_fecha_punto_control').val() == '' || $('#txt_fecha_punto_control').val() == undefined) {
            _output.Estatus = false;
            _output.Mensaje += '&nbsp;<i class="fa fa-angle-double-right"></i>&nbsp;La fecha del punto de control.<br />';
        }

        if ($('#txt_ubicacion_punto_control').val() == '' || $('#txt_ubicacion_punto_control').val() == undefined) {
            _output.Estatus = false;
            _output.Mensaje += '&nbsp;<i class="fa fa-angle-double-right"></i>&nbsp;La ubicacion del punto de control.<br />';
        }

        if ($('#txt_renglon_punto_control').val() == '' || $('#txt_renglon_punto_control').val() == undefined) {
            _output.Estatus = false;
            _output.Mensaje += '&nbsp;<i class="fa fa-angle-double-right"></i>&nbsp;El renglon del punto de control.<br />';
        }

        if ($('#txt_distancia_punto_control').val() == '' || $('#txt_distancia_punto_control').val() == undefined) {
            _output.Estatus = false;
            _output.Mensaje += '&nbsp;<i class="fa fa-angle-double-right"></i>&nbsp;La distancia del punto de control.<br />';
        }

        if ($('#txt_senia_punto_control').val() == '' || $('#txt_senia_punto_control').val() == undefined) {
            _output.Estatus = false;
            _output.Mensaje += '&nbsp;<i class="fa fa-angle-double-right"></i>&nbsp;La seña del punto de control.<br />';
        }

        //if ($('#txt_tiempo_ideal_punto_control').val() == '' || $('#txt_tiempo_ideal_punto_control').val() == undefined) {
        //    _output.Estatus = false;
        //    _output.Mensaje += '&nbsp;<i class="fa fa-angle-double-right"></i>&nbsp;El tiempo ideal del punto de control.<br />';
        //}

        if ($('#txt_hora_inicio_punto_control').val() == '' || $('#txt_hora_inicio_punto_control').val() == undefined) {
            _output.Estatus = false;
            _output.Mensaje += '&nbsp;<i class="fa fa-angle-double-right"></i>&nbsp;La hora inicio del punto de control.<br />';
        }

        if ($('#txt_hora_termino_punto_control').val() == '' || $('#txt_hora_termino_punto_control').val() == undefined) {
            _output.Estatus = false;
            _output.Mensaje += '&nbsp;<i class="fa fa-angle-double-right"></i>&nbsp;La hora de termino del punto de control.<br />';
        }

        //if ($('#txt_intervalo_carros_punto_control').val() == '' || $('#txt_intervalo_carros_punto_control').val() == undefined) {
        //    _output.Estatus = false;
        //    _output.Mensaje += '&nbsp;<i class="fa fa-angle-double-right"></i>&nbsp;El intervalo entre los carros en el punto de control.<br />';
        //}
        //  ---------------------------------------------------------------------------------------
        //  ---------------------------------------------------------------------------------------
        if (_output.Mensaje != "") {
            _output.Mensaje = "Favor de proporcionar lo siguiente: <br />" + _output.Mensaje;
        }

        //  ---------------------------------------------------------------------------------------
        //  ---------------------------------------------------------------------------------------

    } catch (e) {
        _mostrar_mensaje('Informe técnico' + '[_validarDatos_Nuevo]', e);
    } finally {
        return _output;
    }
}

//  -----------------------------------------------------
//  -----------------------------------------------------
function _validarDatos_Responsable_Operador() {
    var _output = new Object();

    try {
        _output.Estatus = true;
        _output.Mensaje = '';
        //  ---------------------------------------------------------------------------------------
        //  ---------------------------------------------------------------------------------------
        //  datos del evento
        if ($('#txt_hora_llegada_operador').val() == '' || $('#txt_hora_llegada_operador').val() == undefined) {
            _output.Estatus = false;
            _output.Mensaje += '&nbsp;<i class="fa fa-angle-double-right"></i>&nbsp;La hora de llegada.<br />';
        }


        if ($('#txt_hora_salida_operador').val() == '' || $('#txt_hora_salida_operador').val() == undefined) {
            _output.Estatus = false;
            _output.Mensaje += '&nbsp;<i class="fa fa-angle-double-right"></i>&nbsp;La hora de salida.<br />';
        }

        if ($('#txt_fecha_punto_control').val() == '' || $('#txt_fecha_punto_control').val() == undefined) {
            _output.Estatus = false;
            _output.Mensaje += '&nbsp;<i class="fa fa-angle-double-right"></i>&nbsp;La fecha del punto de control.<br />';
        }

        if ($('#cmb_cargo_operador_responsable').val() == '' || $('#cmb_cargo_operador_responsable').val() == undefined) {
            _output.Estatus = false;
            _output.Mensaje += '&nbsp;<i class="fa fa-angle-double-right"></i>&nbsp;El cargo del operador.<br />';
        }

        if ($('#cmb_operador_responsable').val() == '' || $('#cmb_operador_responsable').val() == undefined) {
            _output.Estatus = false;
            _output.Mensaje += '&nbsp;<i class="fa fa-angle-double-right"></i>&nbsp;El operador.<br />';
        }

        //  ---------------------------------------------------------------------------------------
        //  ---------------------------------------------------------------------------------------
        if (_output.Mensaje != "") {
            _output.Mensaje = "Favor de proporcionar lo siguiente: <br />" + _output.Mensaje;
        }

        //  ---------------------------------------------------------------------------------------
        //  ---------------------------------------------------------------------------------------

    } catch (e) {
        _mostrar_mensaje('Informe técnico' + '[_validarDatos_Responsable_Operador]', e);
    } finally {
        return _output;
    }
}

//  -----------------------------------------------------
//  -----------------------------------------------------
function _validarDatos_Tiempo_Ideal_Categoria() {
    var _output = new Object();

    try {
        _output.Estatus = true;
        _output.Mensaje = '';
        //  ---------------------------------------------------------------------------------------
        //  ---------------------------------------------------------------------------------------
        //  datos del evento
        if ($('#txt_tiempo_ideal_categoria').val() == '' || $('#txt_tiempo_ideal_categoria').val() == undefined) {
            _output.Estatus = false;
            _output.Mensaje += '&nbsp;<i class="fa fa-angle-double-right"></i>&nbsp;El tiempo ideal.<br />';
        }



        if ($('#cmb_categoria_tiempo_ideal').val() == '' || $('#cmb_categoria_tiempo_ideal').val() == undefined) {
            _output.Estatus = false;
            _output.Mensaje += '&nbsp;<i class="fa fa-angle-double-right"></i>&nbsp;La categoria.<br />';
        }

       

        //  ---------------------------------------------------------------------------------------
        //  ---------------------------------------------------------------------------------------
        if (_output.Mensaje != "") {
            _output.Mensaje = "Favor de proporcionar lo siguiente: <br />" + _output.Mensaje;
        }

        //  ---------------------------------------------------------------------------------------
        //  ---------------------------------------------------------------------------------------

    } catch (e) {
        _mostrar_mensaje('Informe técnico' + '[_validarDatos_Responsable_Operador]', e);
    } finally {
        return _output;
    }
}
//  -----------------------------------------------------
//  -----------------------------------------------------

///********************BD***************///
function Alta_Jornada_Punto_Control() {
    var obj = new Object();

    try {

        //var intervalo;

        //  se calcula el tiempo del intervalo basandose en los segundos ingresados
        //intervalo =  Calcular_Hora($('#txt_intervalo_carros_punto_control').val());


        obj.Jornada_Id = parseInt($('#txt_jornada_punto_control_id').val());
        obj.Evento_Id = parseInt($('#txt_evento_id').val());
        obj.Clave = $('#txt_clave_punto_control').val();
        obj.Numero = parseInt($('#txt_numero_punto_control').val());
        obj.Fecha = parseDate($('#txt_fecha_punto_control').val());
        obj.Ubicacion = $('#txt_ubicacion_punto_control').val();
        //obj.Tiempo_Ideal = $('#txt_tiempo_ideal_punto_control').val();

        obj.Renglon = $('#txt_renglon_punto_control').val();
        obj.Distancia = $('#txt_distancia_punto_control').val();
        obj.Seña = $('#txt_senia_punto_control').val();

        obj.Hora_Inicio = $('#txt_hora_inicio_punto_control').val();
        obj.Hora_Fin = $('#txt_hora_termino_punto_control').val();

        //obj.Intervalo = intervalo;

        obj.tbl_operadores = JSON.stringify($('#tbl_operadores_responsables').bootstrapTable('getData'));
        obj.tbl_categoria_tiempoIdeal = JSON.stringify($('#tbl_categoria_tiempo_ideal').bootstrapTable('getData'));


        var $data = JSON.stringify({ 'jsonObject': JSON.stringify(obj) });

        $.ajax({
            type: 'POST',
            url: 'controllers/EventosPtsCtrlController.asmx/Alta',
            data: $data,
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            async: false,
            cache: false,
            success: function (datos) {
                if (datos != null) {
                    var result = JSON.parse(datos.d);
                    if (result.Estatus == 'success') {

                        _mostrar_vista_jornadas('Principal');
                        _limpiar_todos_controles_puntos_control();
                        _mostrar_mensaje(result.Titulo, result.Mensaje);
                        _Consultar_Jornadas();

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


//  -----------------------------------------------------
//  -----------------------------------------------------
function Modificar_Jornada_Punto_Control() {
    var obj = new Object();

    try {
        //var intervalo;

        //  se calcula el tiempo del intervalo basandose en los segundos ingresados
        //intervalo = Calcular_Hora($('#txt_intervalo_carros_punto_control').val());

        obj.Punto_Control_Id = parseInt($('#txt_punto_control_id').val());
        obj.Jornada_Id = parseInt($('#txt_jornada_punto_control_id').val());
        obj.Evento_Id = parseInt($('#txt_evento_id').val());
        obj.Clave = $('#txt_clave_punto_control').val();
        obj.Numero = parseInt($('#txt_numero_punto_control').val());
        obj.Fecha = parseDate($('#txt_fecha_punto_control').val());
        obj.Ubicacion = $('#txt_ubicacion_punto_control').val();
        //obj.Tiempo_Ideal = $('#txt_tiempo_ideal_punto_control').val();

        obj.Renglon = $('#txt_renglon_punto_control').val();
        obj.Distancia = $('#txt_distancia_punto_control').val();
        obj.Seña = $('#txt_senia_punto_control').val();

        obj.Hora_Inicio = $('#txt_hora_inicio_punto_control').val();
        obj.Hora_Fin = $('#txt_hora_termino_punto_control').val();

        //obj.Intervalo = intervalo;

        obj.tbl_operadores = JSON.stringify($('#tbl_operadores_responsables').bootstrapTable('getData'));
        obj.tbl_operadores_eliminados = JSON.stringify($('#tbl_operadores_responsables_eliminar').bootstrapTable('getData'));

        obj.tbl_categoria_tiempoIdeal = JSON.stringify($('#tbl_categoria_tiempo_ideal').bootstrapTable('getData'));
        obj.tbl_categoria_tiempoIdeal_Eliminar = JSON.stringify($('#tbl_categoria_tiempo_ideal_eliminar').bootstrapTable('getData'));

        
        var $data = JSON.stringify({ 'jsonObject': JSON.stringify(obj) });

        $.ajax({
            type: 'POST',
            url: 'controllers/EventosPtsCtrlController.asmx/Modificar',
            data: $data,
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            async: false,
            cache: false,
            success: function (datos) {
                if (datos != null) {
                    var result = JSON.parse(datos.d);
                    if (result.Estatus == 'success') {
                        _mostrar_vista_jornadas('Principal');
                        _limpiar_todos_controles_procesos_jornada();
                        _mostrar_mensaje(result.Titulo, result.Mensaje);
                        _Consultar_Jornadas();
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


//  -----------------------------------------------------
//  -----------------------------------------------------

function _Consultar_Clave_Punto_Control_Automatica() {
    var filtros = null;
    try {
        filtros = new Object();

        filtros.Evento_Id = parseInt($("#txt_evento_id").val());
        filtros.Jornada_Id = parseInt($("#txt_jornada_punto_control_id").val());
        var $data = JSON.stringify({ 'jsonObject': JSON.stringify(filtros) });

        $.ajax({
            type: 'POST',
            url: 'controllers/EventosPtsCtrlController.asmx/Consultar_Clave_Automatica_Punto_Control',
            data: $data,
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            async: false,
            cache: false,
            success: function (datos) {
                if (datos !== null) {
                    datos = JSON.parse(datos.d);
                    $("#txt_clave_punto_control").val(datos.length + 1);
                }
                else {

                }
            }
        });
    } catch (e) {
        _mostrar_mensaje('Error ', e);
    }
}


//  -----------------------------------------------------
//  -----------------------------------------------------
function _habilitar_controles_punto_control(opc) {
    var Estatus = true;
    try {

        Estatus = false;
        var _boton_ = $('#btn_guardar_punto_control');
        _boton_.show();

        switch (opc) {
            case 'Nuevo':
                $('#btn_guardar_punto_control').attr('title', 'Guardar');

                break;


            case 'Modificar':
                $('#btn_guardar_punto_control').attr('title', 'Actualizar');

                break;

        
        }


    } catch (e) {
        _mostrar_mensaje('Error Técnico' + ' [_habilitar_controles] ', e);
    }

}



function btn_editar_punto_click(tab) {

    $('#tbl_operadores_responsables').bootstrapTable('load', []);
    $('#tbl_operadores_responsables_eliminar').bootstrapTable('load', []);


    if ($('#txt_estatus').val() == "ACTIVO" || $('#txt_estatus').val() == "INACTIVO") {


        var row = $(tab).data('orden');
        _limpiar_todos_controles_puntos_control();

        $('#txt_punto_control_id').val(row.Punto_Control_Id);
        $('#txt_jornada_punto_control_id').val(row.Jornada_Id);
        $('#txt_clave_punto_control').val(row.Clave);
        $('#txt_numero_punto_control').val(row.Numero);

        $('#txt_renglon_punto_control').val(row.Renglon);
        $('#txt_distancia_punto_control').val(row.Distancia);
        $('#txt_senia_punto_control').val(row.Seña);


        if (row.Fecha != "01/01/0001 00:00:00") {
            $('#txt_fecha_punto_control').val(new Date(row.Fecha).toString('dd/MM/yyyy'));

        }

        $('#txt_ubicacion_punto_control').val(row.Ubicacion);
        //$('#txt_tiempo_ideal_punto_control').val(row.Str_Tiempo_Ideal.substring(0, 5));

        $('#txt_hora_inicio_punto_control').val(row.Str_Hora_Inicio.substring(0, 5));
        $('#txt_hora_termino_punto_control').val(row.Str_Hora_Fin.substring(0, 5));

        //var segundos;
        //segundos = calcular_total_segundos(row.Str_Intervalo);
        //$('#txt_intervalo_carros_punto_control').val(segundos);

        //------------------------------------------------------------------------------------------------------
        //------------------------------------------------------------------------------------------------------
        //  links
        var filtros = null;
        filtros = new Object();

        filtros.Punto_Control_Id = row.Punto_Control_Id;
        var $data = JSON.stringify({ 'jsonObject': JSON.stringify(filtros) });

        $.ajax({
            type: 'POST',
            url: 'controllers/EventosPtsCtrlController.asmx/Consultar_Responables_Punto_Control',
            data: $data,
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            async: false,
            cache: false,
            success: function (datos) {
                if (datos !== null) {
                    datos = JSON.parse(datos.d);
                    $('#tbl_operadores_responsables').bootstrapTable('load', datos);

                }
                else {
                    $('#tbl_operadores_responsables').bootstrapTable('load', []);
                }
            }
        });


        $.ajax({
            type: 'POST',
            url: 'controllers/EventosPtsCtrlController.asmx/Consultar_TiempoIdeal_Categorias_Punto_Control',
            data: $data,
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            async: false,
            cache: false,
            success: function (datos) {
                if (datos !== null) {
                    datos = JSON.parse(datos.d);
                    $('#tbl_categoria_tiempo_ideal').bootstrapTable('load', datos);

                }
                else {
                    $('#tbl_categoria_tiempo_ideal').bootstrapTable('load', []);
                }
            }
        });

        _habilitar_controles_punto_control("Modificar");
        _mostrar_vista_jornadas('Punto');
    }
    else {
        _mostrar_mensaje('Informe Técnico' + '', 'No puede ser editado');
    }
}




//  -----------------------------------------------------
//  -----------------------------------------------------

function btn_eliminar_punto_click(tab) {

    var row = $(tab).data('orden');
    if ($('#txt_estatus').val() == "ACTIVO" || $('#txt_estatus').val() == "INACTIVO") {


        bootbox.confirm({
            title: 'Eliminar Registro',
            message: 'Esta seguro de Eliminar el registro seleccionado?',
            callback: function (result) {
                if (result) {

                    //  documentos
                    var filtros = null;
                    filtros = new Object();

                    filtros.Punto_Control_Id = row.Punto_Control_Id;
                    var $data = JSON.stringify({ 'jsonObject': JSON.stringify(filtros) });

                    $.ajax({
                        type: 'POST',
                        url: 'controllers/EventosPtsCtrlController.asmx/Cancelacion',
                        data: $data,
                        dataType: "json",
                        contentType: "application/json; charset=utf-8",
                        async: false,
                        cache: false,
                        success: function (datos) {
                            if (datos !== null) {
                                datos = JSON.parse(datos.d);

                                _Consultar_Jornadas();
                            }
                            else {

                            }
                        }
                    });

                }

            }
        });

    }
    else {
        _mostrar_mensaje('Informe Técnico' + '', 'No puede ser editado');
    }
}



//  -----------------------------------------------------
//  -----------------------------------------------------
function crear_tabla_responsable_operador() {

    try {
        $('#tbl_operadores_responsables').bootstrapTable('destroy');

        $('#tbl_operadores_responsables').bootstrapTable({
            cache: false,
            striped: true,
            pagination: true,
            data: [],
            pageSize: 10,
            pageList: [10, 25, 50, 100, 200],
            smartDysplay: false,
            search: false,
            showColumns: false,
            showRefresh: false,
            minimumCountColumns: 2,

            columns: [


                { field: 'Relacion_Operador_Id', title: 'Relacion_Operador_Id', align: 'center', valign: 'top', visible: false, sortable: true },

                { field: 'Responsable_Id', title: 'Responsable_Id', align: 'center', valign: 'top', visible: false, sortable: true },
                { field: 'Punto_Control_Id', title: 'Punto_Control_Id', align: 'center', valign: 'top', visible: false, sortable: true },
                { field: 'Evento_Id', title: 'Evento_Id', align: 'center', valign: 'top', visible: false, sortable: true },
                { field: 'Estatus', title: 'Estatus', align: 'center', valign: 'top', visible: false, sortable: true },
                { field: 'Hora_Llegada', title: 'Hora_Llegada', align: 'center', valign: 'top', visible: false, sortable: true },
                { field: 'Hora_Salida', title: 'Hora_Salida', align: 'center', valign: 'top', visible: false, sortable: true },


                { field: 'Responsable', title: 'Operador', align: 'center', valign: 'top', visible: true, sortable: true },
                { field: 'Cargo', title: 'Cargo', align: 'center', valign: 'top', visible: true, sortable: true },
                
                 {
                     field: 'Str_Hora_Llegada', title: 'Hora de llegada', align: 'left', valign: 'top', sortable: true, visible: true, formatter: function (value, row, index) {
                         if (row.Str_Hora_Llegada != null)
                             return new Date('1/1/2018 ' + value).toString('HH:mm');
                     }
                 },

                   {
                       field: 'Str_Hora_Salida', title: 'Hora de salida', align: 'left', valign: 'top', sortable: true, visible: true, formatter: function (value, row, index) {
                           if (row.Str_Hora_Salida != null)
                               return new Date('1/1/2018 ' + value).toString('HH:mm');
                       }
                   },

                {
                    field: 'Relacion_Operador_Id',
                    title: '',
                    align: 'right',
                    valign: 'top',
                    halign: 'center',

                    formatter: function (value, row) {

                        var opciones = '<div style=" text-align: center;">';

                        opciones += '<div style="display:block">';

                        opciones += '<a class="remove ml10 text-red" id="' + row.Evento_Id + '" href="javascript:void(0)" data-orden=\'' + JSON.stringify(row) + '\' onclick="btn_eliminar_relacion_operador_click(this);" title="Eliminar"><i class="glyphicon glyphicon-trash"></i>&nbsp;<span style="font-size:11px !important;">Eliminar</span></a>';


                        opciones += '</div>';

                        opciones += '</div>';

                        return opciones;
                    }
                },
            ]
        });
    } catch (e) {
        _mostrar_mensaje('Informe Técnico' + '[crear_tabla_vehiculo]', e.message);
    }
}

//  -----------------------------------------------------
//  -----------------------------------------------------

function crear_tabla_categorias_tiemposIdeales() {

    try {
        $('#tbl_categoria_tiempo_ideal').bootstrapTable('destroy');

        $('#tbl_categoria_tiempo_ideal').bootstrapTable({
            cache: false,
            striped: true,
            pagination: true,
            data: [],
            pageSize: 10,
            pageList: [10, 25, 50, 100, 200],
            smartDysplay: false,
            search: false,
            showColumns: false,
            showRefresh: false,
            minimumCountColumns: 2,

            columns: [


                { field: 'Relacion_Id', title: 'Relacion_Id', align: 'center', valign: 'top', visible: false, sortable: true },

                { field: 'Punto_Control_Id', title: 'Punto_Control_Id', align: 'center', valign: 'top', visible: false, sortable: true },
                { field: 'Categoria_Id', title: 'Categoria_Id', align: 'center', valign: 'top', visible: false, sortable: true },

                { field: 'Categoria', title: 'Categoria', align: 'center', valign: 'top', visible: true, sortable: true },

                { field: 'Tiempo_Ideal', title: 'Tiempo ideal', align: 'center', valign: 'top', visible: false, sortable: true },


                 {
                     field: 'Str_Tiempo_Ideal', title: 'Tiempo ideal', align: 'left', valign: 'top', sortable: true, visible: true, formatter: function (value, row, index) {
                         if (row.Str_Tiempo_Ideal != null)
                             return new Date('1/1/2018 ' + value).toString('HH:mm:ss');
                     }
                 },

                  

                {
                    field: 'Relacion_Id',
                    title: '',
                    align: 'right',
                    valign: 'top',
                    halign: 'center',

                    formatter: function (value, row) {

                        var opciones = '<div style=" text-align: center;">';

                        opciones += '<div style="display:block">';

                        opciones += '<a class="remove ml10 text-red" id="' + row.Relacion_Id + '" href="javascript:void(0)" data-orden=\'' + JSON.stringify(row) + '\' onclick="btn_eliminar_relacion_categoria_tiempoIdeal_click(this);" title="Eliminar"><i class="glyphicon glyphicon-trash"></i>&nbsp;<span style="font-size:11px !important;">Eliminar</span></a>';


                        opciones += '</div>';

                        opciones += '</div>';

                        return opciones;
                    }
                },
            ]
        });
    } catch (e) {
        _mostrar_mensaje('Informe Técnico' + '[crear_tabla_vehiculo]', e.message);
    }
}



//  -----------------------------------------------------
//  -----------------------------------------------------
function btn_eliminar_relacion_categoria_tiempoIdeal_click(tab) {
    var row = $(tab).data('orden');

   
    if (row.Relacion_Id == 0) {
        $('#tbl_categoria_tiempo_ideal').bootstrapTable('remove', {
            field: 'Relacion_Id',
            values: [row.Relacion_Id],
            field: 'Punto_Control_Id',
            values: [row.Punto_Control_Id],
            field: 'Categoria_Id',
            values: [row.Categoria_Id],
        });
    }
    else {
        $('#tbl_categoria_tiempo_ideal').bootstrapTable('remove', {
            field: 'Relacion_Id',
            values: [row.Relacion_Id],
        });

        $('#tbl_categoria_tiempo_ideal_eliminar').bootstrapTable('insertRow', {
            index: 0,
            row: {
                Relacion_Id: row.Relacion_Id,
            }
        });
        
    }
}


function crear_tabla_categorias_tiemposIdeales_Eliminar() {

    try {
        $('#tbl_categoria_tiempo_ideal_eliminar').bootstrapTable('destroy');

        $('#tbl_categoria_tiempo_ideal_eliminar').bootstrapTable({
            cache: false,
            striped: true,
            pagination: true,
            data: [],
            pageSize: 10,
            pageList: [10, 25, 50, 100, 200],
            smartDysplay: false,
            search: false,
            showColumns: false,
            showRefresh: false,
            minimumCountColumns: 2,

            columns: [


                { field: 'Relacion_Id', title: 'Relacion_Id', align: 'center', valign: 'top', visible: true, sortable: true },
            ]
        });
    } catch (e) {
        _mostrar_mensaje('Informe Técnico' + '[crear_tabla_vehiculo]', e.message);
    }
}
//  -----------------------------------------------------
//  -----------------------------------------------------
function crear_tabla_responsable_operador_eliminar() {

    try {
        $('#tbl_operadores_responsables_eliminar').bootstrapTable('destroy');

        $('#tbl_operadores_responsables_eliminar').bootstrapTable({
            cache: false,
            striped: true,
            pagination: true,
            data: [],
            pageSize: 10,
            pageList: [10, 25, 50, 100, 200],
            smartDysplay: false,
            search: false,
            showColumns: false,
            showRefresh: false,
            minimumCountColumns: 2,


            columns: [


                { field: 'Relacion_Operador_Id', title: 'Relacion_Operador_Id', align: 'center', valign: 'top', visible: true, sortable: true },

            ]
        });
    } catch (e) {
        _mostrar_mensaje('Informe Técnico' + '[crear_tabla_vehiculo]', e.message);
    }
}


//  -----------------------------------------------------
//  -----------------------------------------------------

function btn_eliminar_relacion_operador_click(tab) {

    var row = $(tab).data('orden');

    bootbox.confirm({
        title: 'Eliminar Registro',
        message: 'Esta seguro de Eliminar el registro seleccionado?',
        callback: function (result) {
            if (result) {

             
            }

        }
    });


}


//  -----------------------------------------------------
//  -----------------------------------------------------
///***************************Combos********************///
function _load_cmb_responsables_operadores() {
    try {
        $('#cmb_operador_responsable').select2({
            language: "es",
            theme: "classic",
            placeholder: 'SELECCIONE',
            allowClear: true,
            ajax: {
                url: '../Catalogos/controllers/ResponsablesController.asmx/Consultar_Responsables_Combo',
                cache: true,
                dataType: 'json',
                type: "POST",
                delay: 250,
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
                    };
                },
                processResults: function (data, page) {
                    return {
                        results: data
                    };
                },
            }
        });

        $('#cmb_operador_responsable').on("select2:select", function (evt) {
        });

        $('#cmb_operador_responsable').on("select2:unselect", function (evt) {
        });

    } catch (e) {
        mostrar_mensaje('Informe técnico', e);
    }
}


//  -----------------------------------------------------
//  -----------------------------------------------------

function btn_eliminar_relacion_operador_click(tab) {
    var row = $(tab).data('orden');

    if (row.Relacion_Operador_Id == 0) {
        $('#tbl_operadores_responsables').bootstrapTable('remove', {
            field: 'Relacion_Operador_Id',
            values: [row.Relacion_Operador_Id],

            field: 'Responsable_Id',
            values: [row.Responsable_Id],

            field: 'Cargo',
            values: [row.Cargo],

            field: 'Str_Hora_Llegada',
            values: [row.Str_Hora_Llegada],

            field: 'Str_Hora_Salida',
            values: [row.Str_Hora_Salida],
        });
    }
    else {
        $('#tbl_operadores_responsables').bootstrapTable('remove', {
            field: 'Relacion_Operador_Id',
            values: [row.Relacion_Operador_Id],
        });

        $('#tbl_operadores_responsables_eliminar').bootstrapTable('insertRow', {
            index: 0,
            row: {
                Relacion_Operador_Id: row.Relacion_Operador_Id,
            }
        });
    }

}

//  -----------------------------------------------------
//  -----------------------------------------------------

function Calcular_Hora(time) {
    try {
        var hours = Math.floor( time / 3600 );  
        var minutes = Math.floor( (time % 3600) / 60 );
        var seconds = time % 60;
        var result;

        //Anteponiendo un 0 a los minutos si son menos de 10 
        hours = hours < 10 ? '0' + hours : hours;

        //Anteponiendo un 0 a los minutos si son menos de 10 
        minutes = minutes < 10 ? '0' + minutes : minutes;
 
        //Anteponiendo un 0 a los segundos si son menos de 10 
        seconds = seconds < 10 ? '0' + seconds : seconds;

        result = hours + ":" + minutes + ":" + seconds;  // 2:41:30


        return result;
    } catch (e) {
        mostrar_mensaje('Informe técnico', e);
    }
}


        
//  -----------------------------------------------------
//  -----------------------------------------------------

function calcular_total_segundos(time) {
    try {

        var horas = 0;
        var minutos = 0;
        var segundos = 0;
        var operacion = 0;

        var fecha = new Date('1/1/2018 ' + time);

        //  horas
        horas = fecha.getHours();
        operacion = horas * 60 * 60;

        //  minutos
        minutos = fecha.getMinutes();
        operacion = operacion + (minutos * 60);

        //  segundos
        segundos = fecha.getSeconds();
        operacion = operacion + segundos;


        return operacion;

    } catch (e) {
        mostrar_mensaje('Informe técnico', e);
    }
}
