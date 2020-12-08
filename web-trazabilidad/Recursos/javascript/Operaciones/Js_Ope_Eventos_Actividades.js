
//  *******************************************************************************************************************************
//  *******************************************************************************************************************************
////*********************Inicializar********************///
//  *******************************************************************************************************************************
function _inicializar_vista_actividades_principal() {
    try {
        crear_tabla_actividades();
        //_set_location_toolbar('toolbar');
        //_load_cmb_estatus_actividades('cmb_estatus_filtro');
        _eventos_principal_actividades();
        _mostrar_vista_actividades('Principal');
        _consultar_actividades();

    } catch (e) {
        _mostrar_mensaje('Error Técnico', e);
    }
}
//  -----------------------------------------------------
//  -----------------------------------------------------
function _inicializar_vista_actividades_procesos() {
    try {

        _limpiar_todos_controlers_procesos_actividades();
        _load_cmb_estatus_actividades('cmb_estatus_actividad');

        _eventos_procesos_actividades();
        _inicializar_fechas_operacion_actividades();
    //    //_keyDownInt('txt_año');

    } catch (e) {
        _mostrar_mensaje('Error Técnico', e);
    }
}

function _habilitar_controles_actividades(opc) {
    var estatus = true;
    try {

        estatus = false;
        var _boton_ = $('#btn_guardar_actividad');
        _boton_.show();

        switch (opc) {
            case 'Nuevo':
                $('#btn_guardar_actividad').attr('title', 'Guardar');
                break;

            case 'Modificar':
                $('#btn_guardar_actividad').attr('title', 'Actualizar');
                break;

            case 'ver':
                _boton_.hide();
                break;
        }

    } catch (e) {
        _mostrar_mensaje('error técnico' + ' [_inicializar_fechas_operacion_actividades] ', e);
    }
}

function _eventos_procesos_actividades() {
    try {
        //  ---------------------------------------------------------------------------------
        //  ---------------------------------------------------------------------------------
        $('#btn_guardar_actividad').on('click', function (e) {
            e.preventDefault();

            var title = $('#btn_guardar_actividad').attr('title');
            var valida_datos_requerido = _validadDatos_Nueva_Actividad();
            if (valida_datos_requerido.Estatus) {

                if (title == "Guardar") {
                    _alta_actividad()
                }
                else {
                    _modificar_actividad();
                }
            }
            else {
                _mostrar_mensaje('Información', valida_datos_requerido.Mensaje);
            }

        });
        //  ---------------------------------------------------------------------------------
        //  ---------------------------------------------------------------------------------
        $('#btn_cancelar_actividad').on('click', function (e) {
            e.preventDefault();
            _limpiar_todos_controlers_procesos_actividades();
            _mostrar_vista_actividades('Principal');
        });
        //  ---------------------------------------------------------------------------------
        //  ---------------------------------------------------------------------------------
        
    } catch (e) {
        _mostrar_mensaje('Error Técnico', e);
    }
}

//  *******************************************************************************************************************************
//  *******************************************************************************************************************************
//  fechas
//  *******************************************************************************************************************************
function _inicializar_fechas_operacion_actividades() {
    $('#dtp_txt_fecha_inicio_actividad').datetimepicker({
        defaultDate: new Date(),
        viewMode: 'days',
        locale: 'es',
        format: "DD/MM/YYYY"
    });
    $("#dtp_txt_fecha_inicio_actividad").datetimepicker("useCurrent", true);

    $('#dtp_txt_fecha_fin_actividad').datetimepicker({
        defaultDate: new Date(),
        viewMode: 'days',
        locale: 'es',
        format: "DD/MM/YYYY"
    });
    $("#dtp_txt_fecha_fin_actividad").datetimepicker("useCurrent", true);

}

function crear_tabla_actividades() {

    try {
        $('#tbl_actividades').bootstrapTable('destroy');
        $('#tbl_actividades').bootstrapTable({
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
                { field: 'Actividad_Id', title: 'Actividad_Id', align: 'center', valign: 'top', visible: false },
                { field: 'Clave', title: 'Clave', align: 'left', valign: 'top', visible: true, sortable: true },
                { field: 'Nombre', title: 'Nombre Actividad', align: 'left', valign: 'top', visible: true, sortable: true },
                {
                    field: 'Fecha_Inicio', title: 'Fecha Inicio', align: 'center', valign: 'top', visible: true, sortable: true, formatter: function (value, row, index) {
                        if (row.Fecha_Inicio != null)
                            return new Date(value).toString('dd/MM/yyyy');
                    }
                },
                {
                    field: 'Fecha_Fin', title: 'Fecha Fin', align: 'center', valign: 'top', visible: true, sortable: true, formatter: function (value, row, index) {
                        if (row.Fecha_Fin != null)
                            return new Date(value).toString('dd/MM/yyyy');
                    }
                },
                { field: 'Comentarios', title: 'Comentarios', align: 'center', valign: 'top', visible: true, sortable: true },

                {
                    field: 'Actividad_Id',
                    title: '',
                    align: 'right',
                    valign: 'top',
                    halign: 'center',

                    formatter: function (value, row) {

                        var opciones = '<div style=" text-align: center;">';
                        opciones += '<div style="display:block"><a class="remove ml10 text-purple" id="' + row.Actividad_Id + '" href="javascript:void(0)" data-orden=\'' + JSON.stringify(row) + '\' onclick="btn_editar_actividad_click(this);" title="Editar"><i class="glyphicon glyphicon-edit"></i>&nbsp;<span style="font-size:11px !important;">Editar</span></a></div>';
                        opciones += '</div>';

                        return opciones;
                    }
                },

                {
                    field: 'Actividad_Id',
                    title: '',
                    align: 'right',
                    valign: 'top',
                    halign: 'center',

                    formatter: function (value, row) {

                        var opciones = '<div style=" text-align: center;">';
                        opciones += '<div style="display:block"><a class="remove ml10 text-red" id="' + row.Actividad_Id + '" href="javascript:void(0)" data-orden=\'' + JSON.stringify(row) + '\' onclick="btn_eliminar_actividad_click(this);" title="Editar"><i class="glyphicon glyphicon-trash"></i>&nbsp;<span style="font-size:11px !important;">Eliminar</span></a></div>';
                        opciones += '</div>';

                        return opciones;
                    }
                }
            ]
        });
    } catch (e) {
        _mostrar_mensaje('Informe Técnico' + '[crear_tabla_actividades]', e.message);
    }
}

//  -----------------------------------------------------
//  -----------------------------------------------------
function _mostrar_vista_actividades(vista_) {

    switch (vista_) {
        case "Principal":
            $('#Actividades_Operacion').css('display', 'none');
            $('#Actividades_Principal').css('display', 'block');
            break;
        case "Operacion":
            $('#Actividades_Operacion').css('display', 'block');
            $('#Actividades_Principal').css('display', 'none');
            break;
    }
}
//  -----------------------------------------------------
//  -----------------------------------------------------

function _limpiar_todos_controlers_procesos_actividades() {

    try {
        $('#Actividades_Operacion input[type=text]').each(function () { $(this).val(''); });
        $('#Actividades_Operacion input[type=hidden]').each(function () { $(this).val(''); });

    } catch (e) {
        _mostrar_mensaje('Error Técnico', 'limpiar controles. ' + e);
    }
}


function _eventos_principal_actividades() {
    try {

        $('#btn_nuevo_actividad').on('click', function (e) {
            e.preventDefault();
            _habilitar_controles_actividades('Nuevo');
            _limpiar_todos_controlers_procesos_actividades();
            _mostrar_vista_actividades('Operacion');

        });

        //$('#btn_busqueda').on('click', function (e) {
        //    e.preventDefault();
        //    _ConsultarFiltros();
        //});

    } catch (e) {
        _mostrar_mensaje('Error Técnico', e);
    }
}

function btn_editar_actividad_click(tab) {
    var row = $(tab).data('orden');


    if ($('#txt_estatus').val() == "ACTIVO" || $('#txt_estatus').val() == "INACTIVO") {

        _habilitar_controles_actividades('Modificar');
        _limpiar_todos_controlers_procesos_actividades();

        $('#txt_actividad_id').val(row.Actividad_Id);
        $('#txt_clave_actividad').val(row.Clave);
        $('#txt_nombre_actividad').val(row.Nombre);
        $('#txt_comentarios_actividad').val(row.Comentarios);

        if (row.Estatus != null)
            $('#cmb_estatus_actividad').select2("trigger", "select", {
                data: { id: row.Estatus, text: row.Estatus }
            });
        if (row.Fecha_Inicio != "01/01/0001 00:00:00") {
            $('#txt_fecha_inicio_actividad').val(new Date(row.Fecha_Inicio).toString('dd/MM/yyyy'));
        }
        if (row.Fecha_Fin != "01/01/0001 00:00:00") {
            $('#txt_fecha_fin_actividad').val(new Date(row.Fecha_Fin).toString('dd/MM/yyyy'));
        }

        _mostrar_vista_actividades('Operacion');
    }
    else {
        _mostrar_mensaje('Informe Técnico' + '', 'No puede ser editado');
    }
}

function btn_eliminar_actividad_click(tab) {
    var row = $(tab).data('orden');

    if ($('#txt_estatus').val() == "ACTIVO" || $('#txt_estatus').val() == "INACTIVO") {

        bootbox.confirm({
            title: 'INACTIVAR Registro',
            message: 'Esta seguro de INACTIVAR el registro seleccionado?',
            callback: function (result) {
                if (result) {

                    //  documentos
                    var filtros = null;
                    filtros = new Object();

                    filtros.Actividad_Id = row.Actividad_Id;
                    var $data = JSON.stringify({ 'jsonObject': JSON.stringify(filtros) });

                    $.ajax({
                        type: 'POST',
                        url: 'controllers/ActividadesController.asmx/Cancelacion',
                        data: $data,
                        dataType: "json",
                        contentType: "application/json; charset=utf-8",
                        async: false,
                        cache: false,
                        success: function (datos) {
                            if (datos !== null) {
                                datos = JSON.parse(datos.d);

                                _consultar_actividades();
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


//function _keyDownInt(id) {
//    $('#' + id).on('keydown', function (e) {

//        //alert("entro int");//_remove_class_error('#' + $(this).attr('id'));

//        // Allow: backspace, delete, tab, escape, enter
//        if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110]) !== -1 ||
//            // Allow: Ctrl+A, Command+A
//            (e.keyCode === 65 && (e.ctrlKey === true || e.metaKey === true)) ||
//            // Allow: home, end, left, right, down, up
//            (e.keyCode >= 35 && e.keyCode <= 40)) {
//            // let it happen, don't do anything
//            return;
//        }
//        // Ensure that it is a number and stop the keypress
//        if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
//            e.preventDefault();
//        }
//    });
//}
//  -----------------------------------------------------
//  -----------------------------------------------------
function _load_cmb_estatus_actividades(cmb) {
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
        _mostrar_mensaje('Informe técnico' + '[_load_cmb_estatus_actividades]', e);
    }
}


//  *******************************************************************************************************************************
//  *******************************************************************************************************************************
///********************CONSULTAS**************///
//  *******************************************************************************************************************************
function _consultar_actividades() {
    var filtros = null;
    try {
        filtros = new Object();

        //filtros.Estatus = $('#cmb_estatus_filtro').val() === null ? "" : ($('#cmb_estatus_filtro').val());
        filtros.Evento_Id = parseInt($("#txt_evento_id").val());

        var $data = JSON.stringify({ 'jsonObject': JSON.stringify(filtros) });

        $.ajax({
            type: 'POST',
            url: 'controllers/ActividadesController.asmx/Consultar_Actividades',
            data: $data,
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            async: false,
            cache: false,
            success: function (datos) {
                if (datos !== null) {
                    datos = JSON.parse(datos.d);
                    $('#tbl_actividades').bootstrapTable('load', datos);
                }
            }
        });
    } catch (e) {
        _mostrar_mensaje('Error ', e);
    }
}


///*******************Validaciones*************************///
function _validadDatos_Nueva_Actividad() {
    var _output = new Object();

    try {
        _output.Estatus = true;
        _output.Mensaje = '';
        //  ---------------------------------------------------------------------------------------
        //  ---------------------------------------------------------------------------------------
        //  datos de la actividad
        if ($('#txt_clave_actividad').val() == '' || $('#txt_clave_actividad').val() == undefined) {
            _output.Estatus = false;
            _output.Mensaje += '&nbsp;<i class="fa fa-angle-double-right"></i>&nbsp;La clave.<br />';
        }
        if ($('#txt_nombre_actividad').val() == '' || $('#txt_nombre_actividad').val() == undefined) {
            _output.Estatus = false;
            _output.Mensaje += '&nbsp;<i class="fa fa-angle-double-right"></i>&nbsp;El nombre.<br />';
        }
        if ($('#txt_fecha_inicio_actividad').val() == '' || $('#txt_fecha_inicio_actividad').val() == undefined) {
            _output.Estatus = false;
            _output.Mensaje += '&nbsp;<i class="fa fa-angle-double-right"></i>&nbsp;La fecha de inicio.<br />';
        }
        if ($('#txt_fecha_fin_actividad').val() == '' || $('#txt_fecha_fin_actividad').val() == undefined) {
            _output.Estatus = false;
            _output.Mensaje += '&nbsp;<i class="fa fa-angle-double-right"></i>&nbsp;La fecha de fin.<br />';
        }
        if ($('#cmb_estatus_actividad').val() == '' || $('#cmb_estatus_actividad').val() == undefined) {
            _output.Estatus = false;
            _output.Mensaje += '&nbsp;<i class="fa fa-angle-double-right"></i>&nbsp;El estatus.<br />';
        }

        if (_output.Mensaje != "") {
            _output.Mensaje = "Favor de proporcionar lo siguiente: <br />" + _output.Mensaje;
        }

    } catch (e) {
        _mostrar_mensaje('Informe técnico' + '[_validadDatos_Nueva_Actividad]', e);
    } finally {
        return _output;
    }
}


/////********************BD***************///
function _alta_actividad() {
    var obj = new Object();
    var clave = "";

    try {
        obj.Evento_Id = parseInt($('#txt_evento_id').val());
        obj.Clave = $('#txt_clave_actividad').val();
        obj.Nombre = $('#txt_nombre_actividad').val();
        obj.Fecha_Inicio = parseDate($('#txt_fecha_inicio_actividad').val());
        obj.Fecha_Fin = parseDate($('#txt_fecha_fin_actividad').val());
        obj.Estatus = $('#cmb_estatus_actividad').val();
        obj.Comentarios = $('#txt_comentarios_actividad').val();

        var $data = JSON.stringify({ 'jsonObject': JSON.stringify(obj) });

        $.ajax({
            type: 'POST',
            url: 'controllers/ActividadesController.asmx/Alta',
            data: $data,
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            async: false,
            cache: false,
            success: function (datos) {
                if (datos != null) {
                    var result = JSON.parse(datos.d);
                    if (result.Estatus == 'success') {
                        _mostrar_vista_actividades('Principal');
                        _mostrar_mensaje(result.Titulo, result.Mensaje);
                        _consultar_actividades();
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


function _modificar_actividad() {
    var obj = new Object();

    try {
        obj.Evento_Id = parseInt($('#txt_evento_id').val());
        obj.Actividad_Id = parseInt($('#txt_actividad_id').val());
        obj.Clave = $('#txt_clave_actividad').val();
        obj.Nombre = $('#txt_nombre_actividad').val();
        obj.Fecha_Inicio = parseDate($('#txt_fecha_inicio_actividad').val());
        obj.Fecha_Fin = parseDate($('#txt_fecha_fin_actividad').val());
        obj.Estatus = $('#cmb_estatus_actividad').val();
        obj.Comentarios = $('#txt_comentarios_actividad').val();

        var $data = JSON.stringify({ 'jsonObject': JSON.stringify(obj) });

        $.ajax({
            type: 'POST',
            url: 'controllers/ActividadesController.asmx/Modificar',
            data: $data,
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            async: false,
            cache: false,
            success: function (datos) {
                if (datos != null) {
                    var result = JSON.parse(datos.d);
                    if (result.Estatus == 'success') {
                        _mostrar_vista_actividades('Principal');
                        _mostrar_mensaje(result.Titulo, result.Mensaje);
                        _consultar_actividades();
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

//function obtenerClave() {
//    var arr = ($('#txt_nombre').val()).split(" ", 3);
//    var dateString = $('#txt_fecha_nacimiento').val();
//    var dateTime = dateString.split(" ");
//    var dateOnly = dateTime[0];
//    var dates = dateOnly.split("/");
//    var temp = dates[1] + "/" + dates[0] + "/" + dates[2];
//    var clave = "";

//    for (var i = 0; i < arr.length; i++) {
//        clave += arr[i].charAt(0);
//    }
//    clave += dates[2].toString().substr(-2, 2) + dates[1].toString() + dates[0].toString();

//    return clave;
//}