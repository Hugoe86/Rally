
$(document).on('ready', function () {
    _load_vistas();
});


function _load_vistas() {
    _launchComponent('vistas/Cancelacion_Puntos/Principal.html', 'Principal');
}


function _launchComponent(component, id) {

    $('#' + id).load(component, function () {

        switch (id) {
            case 'Principal':
                _inicializar_vista_principal();
                break;
            case 'Operacion':
                //_inicializar_vista_procesos();
                break;
        }
    });
}


//  *******************************************************************************************************************************
//  *******************************************************************************************************************************
////*********************Inicializar********************///
//  *******************************************************************************************************************************
function _inicializar_vista_principal() {
    try {
        crear_tabla_puntos();
        _set_location_toolbar('toolbar');
        _eventos_principal();

        _mostrar_vista('Principal');
        _load_cmb_eventos('cmb_busqueda_evento');
        _load_cmb_jornada('cmb_busqueda_jornada');

        consultar_eventos_iniciados();

    } catch (e) {
        _mostrar_mensaje('Error Técnico', e);
    }
}


//  -----------------------------------------------------
//  -----------------------------------------------------
function consultar_eventos_iniciados() {

    try {

        $.ajax({
            type: 'POST',
            url: '../../Paginas/Operaciones/controllers/EventosController.asmx/Consultar_Evento_Iniciado',
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            async: false,
            cache: false,
            success: function (datos) {
                if (datos !== null) {
                    datos = JSON.parse(datos.d);

                    if (datos.length > 0) {
                        $('#cmb_busqueda_evento').select2("trigger", "select", {
                            data: { id: datos[0].Evento_Id, text: datos[0].Nombre }
                        });
                    }
                }
            }
        });


    } catch (e) {
        _mostrar_mensaje('Error ', e);
    }
}

function _eventos_procesos() {
    try {
        //  ---------------------------------------------------------------------------------
        //  ---------------------------------------------------------------------------------
        $('#btn_guardar').on('click', function (e) {
            e.preventDefault();
            var title = $('#btn_guardar').attr('title');
            var valida_datos_requerido = _validarDatos_Nuevo();

            if (valida_datos_requerido.Estatus) {
                Modificar();
            }
            else {
                _mostrar_mensaje('Información', valida_datos_requerido.Mensaje);
            }
        });
        //  ---------------------------------------------------------------------------------
        //  ---------------------------------------------------------------------------------
        //$('#btn_cancelar').on('click', function (e) {
        //    e.preventDefault();
        //    _mostrar_vista('Principal');
        //    _limpiar_todos_controles_procesos();
        //});
        //  ---------------------------------------------------------------------------------
        //  ---------------------------------------------------------------------------------
    } catch (e) {
        _mostrar_mensaje('Informe Técnico', e.message);
    }
}
//  *******************************************************************************************************************************
//  *******************************************************************************************************************************
//  fechas
//  *******************************************************************************************************************************
//function _inicializar_fechas_operacion() {
//    $('#dtp_txt_fecha_nacimiento').datetimepicker({
//        defaultDate: new Date(),
//        viewMode: 'days',
//        locale: 'es',
//        format: "DD/MM/YYYY"
//    });
//    $("#dtp_txt_fecha_nacimiento").datetimepicker("useCurrent", true);

//}

function crear_tabla_puntos() {

    try {
        $('#tbl_puntos').bootstrapTable('destroy');
        $('#tbl_puntos').bootstrapTable({
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
                { field: 'Punto_Control_Id', title: 'Punto_Control_Id', align: 'center', valign: 'top', visible: false },
                { field: 'Clave', title: 'Clave', align: 'center', valign: 'top', visible: true, sortable: true },
                { field: 'Ubicacion', title: 'Ubicación', align: 'center', valign: 'top', visible: true, sortable: true },
                { field: 'Responsable', title: 'Responsable', align: 'center', valign: 'top', visible: true, sortable: true },
               
                 {
                     field: 'Estatus', title: 'Estatus', align: 'center', valign: 'top', sortable: true, visible: true,
                     formatter: function (value, row, index) {


                         var opciones = "";
                         opciones += ' <div class="row" style="padding-top:2px;">';

                         opciones += '   <div class="col-md-12">';
                         opciones += '       <div>';

                         if (value == "ACTIVO") {
                             opciones += '           <i class="fa fa-check" style="color:#008000;font-size: 14px;"></i>&nbsp;' + value;
                         }
                         else {
                             opciones += '           <i class="fa fa-window-close" style="color:#FF0000;font-size: 14px;"></i>&nbsp;' + value;
                         }


                         opciones += "       </div>"
                         opciones += "   </div>"
                         opciones += "</div>"
                         return opciones;
                     }
                 },


                 {
                     field: 'Sincronizacion', title: 'Sincronizacion', align: 'center', valign: 'top', sortable: true, visible: true,
                     formatter: function (value, row, index) {


                         var opciones = "";
                         opciones += ' <div class="row" style="padding-top:2px;">';

                         opciones += '   <div class="col-md-12">';
                         opciones += '       <div>';


                         if (value == true) {
                             opciones += '           <i class="fa-check-square" style="color:#008000;font-size: 18px;"></i>';
                         }
                         else {
                             //opciones += '           <i class="fa fa-window-close " style="color:#FF0000;font-size: 18px;"></i>';
                         }


                         opciones += "       </div>"
                         opciones += "   </div>"


                         opciones += "</div>"
                         return opciones;
                     }
                 },


                { field: 'Usuario_Cancelo', title: 'Usuario cancelo', align: 'center', valign: 'top', visible: true, sortable: true },

                { field: 'Comentarios', title: 'Comentarios', align: 'center', valign: 'top', visible: true, sortable: true },
                {
                    field: 'Punto_Control_Id',
                    title: '',
                    align: 'right',
                    valign: 'top',
                    halign: 'center',

                    formatter: function (value, row) {

                        var opciones = '<div style=" text-align: center;">';

                        //if (row.Sincronizacion == false) {
                            if (row.Estatus == "ACTIVO") {
                                opciones += '<div style="display:block"><a class="remove ml10 text-red" id="' + row.Punto_Control_Id + '" href="javascript:void(0)" data-orden=\'' + JSON.stringify(row) + '\' onclick="btn_eliminar_click(this);" title="Cancelar punto"><i class="glyphicon glyphicon-remove"></i>&nbsp;<span style="font-size:11px !important;">Cancelar</span></a></div>';
                            }
                        //}
                        
                        opciones += '</div>';

                        return opciones;
                    }
                }
            ]
        });
    } catch (e) {
        _mostrar_mensaje('Informe Técnico' + '[crear_tabla_puntos]', e.message);
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

function _limpiar_todos_controles_procesos() {

    try {
        $('input[type=text]').each(function () { $(this).val(''); });
        $('input[type=hidden]').each(function () { $(this).val(''); });

    } catch (e) {
        _mostrar_mensaje('Error Técnico', 'limpiar controles. ' + e);
    }
}


function _eventos_principal() {
    try {
        $('#btn_inicio').on('click', function (e) {
            e.preventDefault();
            window.location.href = '../Paginas_Generales/Frm_Apl_Principal.aspx';
        });

        //$('#btn_nuevo').on('click', function (e) {
        //    e.preventDefault();
        //    _habilitar_controles('Nuevo');
        //    _limpiar_todos_controles_procesos();
        //    _mostrar_vista('Operacion');

        //});

        $('#btn_busqueda').on('click', function (e) {
            e.preventDefault();
            _ConsultarFiltros();
        });

    } catch (e) {
        _mostrar_mensaje('Error Técnico', e);
    }
}


//function btn_editar_click(tab) {
//    var row = $(tab).data('orden');

//    _habilitar_controles('Modificar');
//    _limpiar_todos_controles_procesos();

//    $('#txt_participante_id').val(row.Participante_ID);
//    $('#txt_clave').val(row.Clave);
//    $('#txt_nombre').val(row.Nombre);
//    $('#txt_email').val(row.Email);
//    $('#txt_telefono').val(row.Telefono);
//    $('#txt_celular').val(row.Celular);
//    $('#txt_direccion').val(row.Direccion);
//    $('#txt_colonia').val(row.Colonia);
//    $('#txt_nacionalidad').val(row.Nacionalidad);

//    if (row.Estatus != null)
//        $('#cmb_estatus').select2("trigger", "select", {
//            data: { id: row.Estatus, text: row.Estatus }
//        });
//    if (row.Sexo != null)
//        $('#cmb_sexo').select2("trigger", "select", {
//            data: { id: row.Sexo, text: row.Sexo }
//        });

//    if (row.Fecha_Nacimiento != "01/01/0001 00:00:00") {
//        $('#txt_fecha_nacimiento').val(new Date(row.Fecha_Nacimiento).toString('dd/MM/yyyy'));
//    }

//    //  documentos
//    var filtros = null;
//    filtros = new Object();

//    filtros.Participante_ID = $('#txt_participante_id').val();
//    var $data = JSON.stringify({ 'jsonObject': JSON.stringify(filtros) });

//    $.ajax({
//        type: 'POST',
//        url: 'controllers/ParticipantesController.asmx/Consultar_Documentos_Participantes',
//        data: $data,
//        dataType: "json",
//        contentType: "application/json; charset=utf-8",
//        async: false,
//        cache: false,
//        success: function (datos) {
//            if (datos !== null) {
//                datos = JSON.parse(datos.d);
//                $('#tbl_documentos').bootstrapTable('load', datos);
//                //$('#img_documento').attr("src", '../../' + datos[0].Ruta);
//            }
//            else {
//                $('#tbl_documentos').bootstrapTable('load', "[]");
//            }
//        }
//    });

//    _mostrar_vista('Operacion');
//}

function btn_eliminar_click(tab) {
    var row = $(tab).data('orden');

    if (row.Estatus == "ACTIVO") {
        bootbox.prompt({
            title: 'Motivo por el que desea eliminar',
            inputType: 'textarea',
            callback: function (result) {
                if (result) {

                    //  documentos
                    var filtros = null;
                    filtros = new Object();

                    filtros.Punto_Control_Id = row.Punto_Control_Id;
                    filtros.Comentarios = result;
                    var $data = JSON.stringify({ 'jsonObject': JSON.stringify(filtros) });

                    $.ajax({
                        type: 'POST',
                        url: 'controllers/CancelacionPuntosController.asmx/Cancelacion',
                        data: $data,
                        dataType: "json",
                        contentType: "application/json; charset=utf-8",
                        async: false,
                        cache: false,
                        success: function (datos) {
                            if (datos !== null) {
                                datos = JSON.parse(datos.d);

                                _ConsultarFiltros();
                            }
                        }
                    });
                }
            }
        });
    } else {
        _mostrar_mensaje('Cancelar', 'Ya se encuentra cancelado el registro seleccionado');
    }
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
//  -----------------------------------------------------
//  -----------------------------------------------------
function _load_cmb_estatus(cmb) {
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
                    id: 'ACTIVO',
                    text: 'ACTIVO',
                }, {
                    id: 'INACTIVO',
                    text: 'INACTIVO',
                },
            ],
        });

    } catch (e) {
        _mostrar_mensaje('Informe técnico' + '[_load_cmb_estatus]', e);
    }
}

//function _load_estados() {
//    try {
//        $('#cmb_estados').select2({
//            language: "es",
//            theme: "classic",
//            placeholder: 'SELECCIONE',
//            allowClear: true,
//            ajax: {
//                url: 'controllers/ParticipantesController.asmx/Consultar_Estados',
//                cache: "true",
//                dataType: 'json',
//                type: "POST",
//                delay: 250,
//                cache: true,
//                params: {
//                    contentType: 'application/json; charset=utf-8'
//                },
//                quietMillis: 100,
//                results: function (data) {
//                    return { results: data };
//                },
//                data: function (params) {
//                    return {
//                        q: params.term,
//                        page: params.page
//                    };
//                },
//                processResults: function (data, page) {
//                    return {
//                        results: data
//                    };
//                },
//            }
//        });
//        $('#cmb_estados').on("select2:select", function (evt) {
//            var id = evt.params.data.id;

//            if ($('#cmb_estados :selected').val() !== '' || $('#cmb_estados :selected').val() !== undefined) {
//                var estado = new Object();
//                estado.Estado_ID = parseInt($('#cmb_estados :selected').val());
//                //$('#txt_no_empleado').val('');
//                var $data = JSON.stringify({ 'jsonObject': JSON.stringify(estado) });

//                $.ajax({
//                    type: 'POST',
//                    url: 'controllers/ParticipantesController.asmx/Consultar_Municipios_Filtro',
//                    data: $data,
//                    contentType: "application/json; charset=utf-8",
//                    dataType: "json",
//                    async: false,
//                    cache: false,
//                    success: function (datos) {
//                        if (datos !== null && datos !== undefined && datos.d !== '[]') {
//                            datos_proceso = JSON.parse(datos.d);

//                            $("#cmb_municipios").select2("trigger", "select", {
//                                data: { id: datos_proceso[0].Municipio_Localidad_ID, text: datos_proceso[0].Nombre }
//                            });
//                        }
//                    }
//                });
//            }
//        });
//    } catch (e) {
//        _mostrar_mensaje('Technical Report', e);
//    }
//}

//function _load_municipios() {
//    try {
//        $('#cmb_municipios').select2({
//            language: "es",
//            theme: "classic",
//            placeholder: 'SELECCIONE',
//            allowClear: true,
//            tags: true
//        });
//    } catch (e) {
//        _mostrar_mensaje('Technical Report', e);
//    }
//}

//  *******************************************************************************************************************************
//  *******************************************************************************************************************************
///********************CONSULTAS**************///
//  *******************************************************************************************************************************
function _ConsultarFiltros() {
    var filtros = null;
    try {

        var valida_datos_requerido = _validar_datos_filtro();

        if (valida_datos_requerido.Estatus) {



            filtros = new Object();

            filtros.Evento_Id = ($('#cmb_busqueda_evento').val() === null ? 0 : parseInt($('#cmb_busqueda_evento').val()));
            filtros.Jornada_Id = ($("#cmb_busqueda_jornada").val() === null ? 0 : parseInt($('#cmb_busqueda_jornada').val()));

            var $data = JSON.stringify({ 'jsonObject': JSON.stringify(filtros) });

            $.ajax({
                type: 'POST',
                url: 'controllers/CancelacionPuntosController.asmx/Consultar_Puntos_Filtros',
                data: $data,
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                async: false,
                cache: false,
                success: function (datos) {
                    if (datos !== null) {
                        datos = JSON.parse(datos.d);
                        $('#tbl_puntos').bootstrapTable('load', datos);
                    }
                }
            });
        } else {
            _mostrar_mensaje('Información', valida_datos_requerido.Mensaje);
        }
    } catch (e) {
        _mostrar_mensaje('Error ', e);
    }
}

///*******************Validaciones*************************///
function _validar_datos_filtro() {
    var _output = new Object();

    try {

        _output.Estatus = true;
        _output.Mensaje = '';

        //  ---------------------------------------------------------------------------------------
        //  ---------------------------------------------------------------------------------------
        //  filtros       

        if ($('#cmb_busqueda_evento').val() == '' || $('#cmb_busqueda_evento').val() == undefined) {
            _output.Estatus = false;
            _output.Mensaje += '&nbsp;<i class="fa fa-angle-double-right"></i>&nbsp;El evento.<br />';
        }

        if ($('#cmb_busqueda_jornada').val() == '' || $('#cmb_busqueda_jornada').val() == undefined) {
            _output.Estatus = false;
            _output.Mensaje += '&nbsp;<i class="fa fa-angle-double-right"></i>&nbsp;La jornada.<br />';
        }

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



///*******************Validaciones*************************///
function _validarDatos_Nuevo() {
    var _output = new Object();

    try {
        _output.Estatus = true;
        _output.Mensaje = '';
        //  ---------------------------------------------------------------------------------------
        //  ---------------------------------------------------------------------------------------
        //  datos del participante
        if ($('#txt_comentarios').val() == '' || $('#txt_comentarios').val() == undefined) {
            _output.Estatus = false;
            _output.Mensaje += '&nbsp;<i class="fa fa-angle-double-right"></i>&nbsp;El comentario.<br />';
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
//function alta() {
//    var obj = new Object();
//    var clave = "";

//    try {
//        obj.Clave = obtenerClave();
//        obj.Nombre = $('#txt_nombre').val();
//        obj.Email = $('#txt_email').val();
//        obj.Telefono = $('#txt_telefono').val();
//        obj.Celular = $('#txt_celular').val();
//        obj.Fecha_Nacimiento = parseDate($('#txt_fecha_nacimiento').val());
//        obj.Sexo = $('#cmb_sexo').val();
//        obj.Estatus = $('#cmb_estatus').val();
//        obj.Direccion = $('#txt_direccion').val();
//        obj.Colonia = $('#txt_colonia').val();
//        obj.Nacionalidad = $('#txt_nacionalidad').val();
//        obj.tbl_documentos = JSON.stringify($('#tbl_documentos').bootstrapTable('getData'));

//        var $data = JSON.stringify({ 'jsonObject': JSON.stringify(obj) });

//        $.ajax({
//            type: 'POST',
//            url: 'controllers/ParticipantesController.asmx/Alta',
//            data: $data,
//            dataType: "json",
//            contentType: "application/json; charset=utf-8",
//            async: false,
//            cache: false,
//            success: function (datos) {
//                if (datos != null) {
//                    var result = JSON.parse(datos.d);
//                    if (result.Estatus == 'success') {
//                        _mostrar_vista('Principal');
//                        _mostrar_mensaje(result.Titulo, result.Mensaje);
//                        _ConsultarFiltros();
//                    } else {
//                        _mostrar_mensaje(result.Titulo, result.Mensaje);
//                    }
//                }
//            }
//        });

//    } catch (e) {
//        alert(e.message)
//    }

//}

function parseDate(dateString) {
    //Intercambia el dia y el mes de los formatos de fecha( DD/MM/YYYY o MM/DD/YYYY )
    var dateTime = dateString.split(" ");
    var dateOnly = dateTime[0];
    var dates = dateOnly.split("/");
    var temp = dates[1] + "/" + dates[0] + "/" + dates[2];
    return temp;
}

//  -----------------------------------------------------
//  -----------------------------------------------------
function _load_cmb_eventos(cmb) {
    try {

        //  <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        //  <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        $('#' + cmb).select2({
            language: "es",
            theme: "classic",
            placeholder: 'SELECCIONE',
            allowClear: true,
            ajax: {
                url: '../Operaciones/controllers/EventosController.asmx/Consultar_Eventos_Combo',
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
        //  <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        //  <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        $('#' + cmb).on("select2:select", function (evt) {
            var _dato_combo = evt.params.data;
            var _id_combo = evt.params.data.detalle_1;

            $("#cmb_busqueda_jornada").empty().trigger("change");
            $('#cmb_busqueda_jornada').prop('disabled', false);

        });
        //  <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        //  <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        $('#' + cmb).on("select2:unselect", function (evt) {

            $("#cmb_busqueda_jornada").empty().trigger("change");

            $('#cmb_busqueda_jornada').attr('disabled', 'disabled');

        });
        //  <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        //  <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
    } catch (e) {
        _mostrar_mensaje('Informe técnico' + '[_load_cmb_eventos]', e);
    }
}


//  -----------------------------------------------------
//  -----------------------------------------------------
function _load_cmb_jornada(cmb) {
    try {

        $('#' + cmb).select2({
            language: "es",
            theme: "classic",
            placeholder: 'SELECCIONE',
            allowClear: true,
            ajax: {
                url: '../Operaciones/controllers/EventosJornadasController.asmx/Consultar_Jornadas_Combo',
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
                        evento_id: $('#cmb_busqueda_evento :selected').val()
                    };
                },
                processResults: function (data, page) {
                    return {
                        results: data
                    };
                },
            }
        });

        $('#' + cmb).on("select2:select", function (evt) {
            var _dato_combo = evt.params.data;
            var _id_combo = evt.params.data.detalle_1;

            //_Consultar_Llenar_Datos_Vehiculo(_dato_combo.id);

        });

        $('#' + cmb).on("select2:unselect", function (evt) {
        });

    } catch (e) {
        _mostrar_mensaje('Informe técnico' + '[_load_cmb_jornada]', e);
    }
}
