
//  *******************************************************************************************************************************
//  *******************************************************************************************************************************
////*********************Inicializar********************///
//  *******************************************************************************************************************************
function _inicializar_vista_categorias_principal() {
    try {
        crear_tabla_categorias();
        _eventos_principal_categorias();
        _mostrar_vista_categorias('Principal');
        _consultar_categorias();

    } catch (e) {
        _mostrar_mensaje('Error Técnico', e);
    }
}
//  -----------------------------------------------------
//  -----------------------------------------------------
function _inicializar_vista_categorias_procesos() {
    try {
        _limpiar_todos_controlers_procesos_categorias();
        _load_cmb_estatus_categorias('cmb_estatus_categoria');

        _eventos_procesos_categorias();
        _keyDownInt('txt_año_desde_categoria');
        _keyDownInt('txt_año_hasta_categoria');
        _keyDownInt('txt_folio_inicio_categoria');
        _keyDownInt('txt_folio_fin_categoria');

    } catch (e) {
        _mostrar_mensaje('Error Técnico', e);
    }
}

function _habilitar_controles_categorias(opc) {
    var estatus = true;
    try {

        estatus = false;
        var _boton_ = $('#btn_guardar_categoria');
        _boton_.show();

        switch (opc) {
            case 'Nuevo':
                $('#btn_guardar_categoria').attr('title', 'Guardar');
                break;

            case 'Modificar':
                $('#btn_guardar_categoria').attr('title', 'Actualizar');
                break;

            case 'ver':
                _boton_.hide();
                break;
        }

    } catch (e) {
        _mostrar_mensaje('error técnico' + ' [_inicializar_fechas_operacion_categorias] ', e);
    }
}

function _eventos_procesos_categorias() {
    try {
        //  ---------------------------------------------------------------------------------
        //  ---------------------------------------------------------------------------------
        $('#btn_guardar_categoria').on('click', function (e) {
            e.preventDefault();

            var title = $('#btn_guardar_categoria').attr('title');
            var valida_datos_requerido = _validadDatos_Nueva_Categoria();
            if (valida_datos_requerido.Estatus) {

                if (title == "Guardar") {
                    _alta_categoria()
                }
                else {
                    _modificar_categoria();
                }
            }
            else {
                _mostrar_mensaje('Información', valida_datos_requerido.Mensaje);
            }

        });
        //  ---------------------------------------------------------------------------------
        //  ---------------------------------------------------------------------------------
        $('#btn_cancelar_categoria').on('click', function (e) {
            e.preventDefault();
            _limpiar_todos_controlers_procesos_categorias();
            _mostrar_vista_categorias('Principal');
        });
        //  ---------------------------------------------------------------------------------
        //  ---------------------------------------------------------------------------------

    } catch (e) {
        _mostrar_mensaje('Error Técnico', e);
    }
}

function crear_tabla_categorias() {

    try {
        $('#tbl_categorias').bootstrapTable('destroy');
        $('#tbl_categorias').bootstrapTable({
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
                { field: 'Categoria_Id', title: 'Categoria_Id', align: 'center', valign: 'top', visible: false },
                { field: 'Clave', title: 'Clave', align: 'left', valign: 'top', visible: true, sortable: true },
                { field: 'Nombre', title: 'Nombre Categoría', align: 'left', valign: 'top', visible: true, sortable: true },
                { field: 'Año_Desde', title: 'Año desde', align: 'center', valign: 'top', visible: true, sortable: true },
                { field: 'Año_Hasta', title: 'Año hasta', align: 'center', valign: 'top', visible: true, sortable: true },

                { field: 'Folio_Inicio', title: 'No. inicio', align: 'center', valign: 'top', visible: true, sortable: true },
                { field: 'Folio_Fin', title: 'No. fin', align: 'center', valign: 'top', visible: true, sortable: true },

                {
                    field: 'Categoria_Id',
                    title: '',
                    align: 'right',
                    valign: 'top',
                    halign: 'center',

                    formatter: function (value, row) {

                        var opciones = '<div style=" text-align: center;">';
                        opciones += '<div style="display:block"><a class="remove ml10 text-purple" id="' + row.Categoria_Id + '" href="javascript:void(0)" data-orden=\'' + JSON.stringify(row) + '\' onclick="btn_editar_categoria_click(this);" title="Editar"><i class="glyphicon glyphicon-edit"></i>&nbsp;<span style="font-size:11px !important;">Editar</span></a></div>';
                        opciones += '</div>';

                        return opciones;
                    }
                },

                {
                    field: 'Categoria_Id',
                    title: '',
                    align: 'right',
                    valign: 'top',
                    halign: 'center',

                    formatter: function (value, row) {

                        var opciones = '<div style=" text-align: center;">';
                        opciones += '<div style="display:block"><a class="remove ml10 text-red" id="' + row.Categoria_Id + '" href="javascript:void(0)" data-orden=\'' + JSON.stringify(row) + '\' onclick="btn_eliminar_categoria_click(this);" title="Editar"><i class="glyphicon glyphicon-trash"></i>&nbsp;<span style="font-size:11px !important;">Eliminar</span></a></div>';
                        opciones += '</div>';

                        return opciones;
                    }
                }
            ]
        });
    } catch (e) {
        _mostrar_mensaje('Informe Técnico' + '[crear_tabla_categorias]', e.message);
    }
}

//  -----------------------------------------------------
//  -----------------------------------------------------
function _mostrar_vista_categorias(vista_) {

    switch (vista_) {
        case "Principal":
            $('#Categorias_Operacion').css('display', 'none');
            $('#Categorias_Principal').css('display', 'block');
            break;
        case "Operacion":
            $('#Categorias_Operacion').css('display', 'block');
            $('#Categorias_Principal').css('display', 'none');
            break;
    }
}
//  -----------------------------------------------------
//  -----------------------------------------------------

function _limpiar_todos_controlers_procesos_categorias() {

    try {
        $('#Categorias_Operacion input[type=text]').each(function () { $(this).val(''); });
        $('#Categorias_Operacion input[type=hidden]').each(function () { $(this).val(''); });

    } catch (e) {
        _mostrar_mensaje('Error Técnico', 'limpiar controles. ' + e);
    }
}


function _eventos_principal_categorias() {
    try {

        $('#btn_nuevo_categoria').on('click', function (e) {
            e.preventDefault();
            _habilitar_controles_categorias('Nuevo');
            _limpiar_todos_controlers_procesos_categorias();
            _mostrar_vista_categorias('Operacion');

        });

    } catch (e) {
        _mostrar_mensaje('Error Técnico', e);
    }
}

function btn_editar_categoria_click(tab) {
    var row = $(tab).data('orden');
    if ($('#txt_estatus').val() == "ACTIVO" || $('#txt_estatus').val() == "INACTIVO") {


        _habilitar_controles_categorias('Modificar');
        _limpiar_todos_controlers_procesos_categorias();

        $('#txt_categoria_id').val(row.Categoria_Id);
        $('#txt_clave_categoria').val(row.Clave);
        $('#txt_nombre_categoria').val(row.Nombre);
        $('#txt_año_desde_categoria').val(row.Año_Desde);
        $('#txt_año_hasta_categoria').val(row.Año_Hasta);

        $('#txt_folio_inicio_categoria').val(row.Folio_Inicio);
        $('#txt_folio_fin_categoria').val(row.Folio_Fin);

        if (row.Estatus != null)
            $('#cmb_estatus_categoria').select2("trigger", "select", {
                data: { id: row.Estatus, text: row.Estatus }
            });

        _mostrar_vista_categorias('Operacion');
    }
    else {
        _mostrar_mensaje('Informe Técnico' + '', 'No puede ser editado');
    }
}

function btn_eliminar_categoria_click(tab) {
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

                    filtros.Categoria_Id = row.Categoria_Id;
                    var $data = JSON.stringify({ 'jsonObject': JSON.stringify(filtros) });

                    $.ajax({
                        type: 'POST',
                        url: 'controllers/CategoriasController.asmx/Cancelacion',
                        data: $data,
                        dataType: "json",
                        contentType: "application/json; charset=utf-8",
                        async: false,
                        cache: false,
                        success: function (datos) {
                            if (datos !== null) {
                                datos = JSON.parse(datos.d);

                                _consultar_categorias();
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
function _load_cmb_estatus_categorias(cmb) {
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
        _mostrar_mensaje('Informe técnico' + '[_load_cmb_estatus_categoriaes]', e);
    }
}


//  *******************************************************************************************************************************
//  *******************************************************************************************************************************
///********************CONSULTAS**************///
//  *******************************************************************************************************************************
function _consultar_categorias() {
    var filtros = null;
    try {
        filtros = new Object();

        //filtros.Estatus = $('#cmb_estatus_filtro').val() === null ? "" : ($('#cmb_estatus_filtro').val());
        filtros.Evento_Id = parseInt($("#txt_evento_id").val());

        var $data = JSON.stringify({ 'jsonObject': JSON.stringify(filtros) });

        $.ajax({
            type: 'POST',
            url: 'controllers/CategoriasController.asmx/Consultar_Categorias',
            data: $data,
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            async: false,
            cache: false,
            success: function (datos) {
                if (datos !== null) {
                    datos = JSON.parse(datos.d);
                    $('#tbl_categorias').bootstrapTable('load', datos);
                }
            }
        });
    } catch (e) {
        _mostrar_mensaje('Error ', e);
    }
}


///*******************Validaciones*************************///
function _validadDatos_Nueva_Categoria() {
    var _output = new Object();

    try {
        _output.Estatus = true;
        _output.Mensaje = '';
        //  ---------------------------------------------------------------------------------------
        //  ---------------------------------------------------------------------------------------
        //  datos de la categoria
        if ($('#txt_clave_categoria').val() == '' || $('#txt_clave_categoria').val() == undefined) {
            _output.Estatus = false;
            _output.Mensaje += '&nbsp;<i class="fa fa-angle-double-right"></i>&nbsp;La clave.<br />';
        }
        if ($('#txt_nombre_categoria').val() == '' || $('#txt_nombre_categoria').val() == undefined) {
            _output.Estatus = false;
            _output.Mensaje += '&nbsp;<i class="fa fa-angle-double-right"></i>&nbsp;El nombre.<br />';
        }
        if ($('#txt_año_desde_categoria').val() == '' || $('#txt_año_desde_categoria').val() == undefined) {
            _output.Estatus = false;
            _output.Mensaje += '&nbsp;<i class="fa fa-angle-double-right"></i>&nbsp;El año desde.<br />';
        }
        if ($('#txt_año_hasta_categoria').val() == '' || $('#txt_año_hasta_categoria').val() == undefined) {
            _output.Estatus = false;
            _output.Mensaje += '&nbsp;<i class="fa fa-angle-double-right"></i>&nbsp;El año hasta.<br />';
        }
        if ($('#cmb_estatus_categoria').val() == '' || $('#cmb_estatus_categoria').val() == undefined) {
            _output.Estatus = false;
            _output.Mensaje += '&nbsp;<i class="fa fa-angle-double-right"></i>&nbsp;El estatus.<br />';
        }
        if ($('#txt_folio_inicio_categoria').val() == '' || $('#txt_folio_inicio_categoria').val() == undefined) {
            _output.Estatus = false;
            _output.Mensaje += '&nbsp;<i class="fa fa-angle-double-right"></i>&nbsp;El No. de inicio.<br />';
        }
        if ($('#txt_folio_fin_categoria').val() == '' || $('#txt_folio_fin_categoria').val() == undefined) {
            _output.Estatus = false;
            _output.Mensaje += '&nbsp;<i class="fa fa-angle-double-right"></i>&nbsp;El No. de fin.<br />';
        }

        if (_output.Mensaje != "") {
            _output.Mensaje = "Favor de proporcionar lo siguiente: <br />" + _output.Mensaje;
        }

    } catch (e) {
        _mostrar_mensaje('Informe técnico' + '[_validadDatos_Nueva_Categoria]', e);
    } finally {
        return _output;
    }
}


/////********************BD***************///
function _alta_categoria() {
    var obj = new Object();
    var clave = "";

    try {
        obj.Evento_Id = parseInt($('#txt_evento_id').val());
        obj.Clave = $('#txt_clave_categoria').val();
        obj.Nombre = $('#txt_nombre_categoria').val();
        obj.Año_Desde = parseInt($('#txt_año_desde_categoria').val());
        obj.Año_Hasta = parseInt($('#txt_año_hasta_categoria').val());
        obj.Folio_Inicio = parseInt($('#txt_folio_inicio_categoria').val());
        obj.Folio_Fin = parseInt($('#txt_folio_fin_categoria').val());
        obj.Estatus = $('#cmb_estatus_categoria').val();

        var $data = JSON.stringify({ 'jsonObject': JSON.stringify(obj) });

        $.ajax({
            type: 'POST',
            url: 'controllers/CategoriasController.asmx/Alta',
            data: $data,
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            async: false,
            cache: false,
            success: function (datos) {
                if (datos != null) {
                    var result = JSON.parse(datos.d);
                    if (result.Estatus == 'success') {
                        _mostrar_vista_categorias('Principal');
                        _mostrar_mensaje(result.Titulo, result.Mensaje);
                        _consultar_categorias();
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


function _modificar_categoria() {
    var obj = new Object();

    try {
        obj.Evento_Id = parseInt($('#txt_evento_id').val());
        obj.Categoria_Id = parseInt($('#txt_categoria_id').val());
        obj.Clave = $('#txt_clave_categoria').val();
        obj.Nombre = $('#txt_nombre_categoria').val();
        obj.Año_Desde = parseInt($('#txt_año_desde_categoria').val());
        obj.Año_Hasta = parseInt($('#txt_año_hasta_categoria').val());
        obj.Folio_Inicio = parseInt($('#txt_folio_inicio_categoria').val());
        obj.Folio_Fin = parseInt($('#txt_folio_fin_categoria').val());
        obj.Estatus = $('#cmb_estatus_categoria').val();

        var $data = JSON.stringify({ 'jsonObject': JSON.stringify(obj) });

        $.ajax({
            type: 'POST',
            url: 'controllers/CategoriasController.asmx/Modificar',
            data: $data,
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            async: false,
            cache: false,
            success: function (datos) {
                if (datos != null) {
                    var result = JSON.parse(datos.d);
                    if (result.Estatus == 'success') {
                        _mostrar_vista_categorias('Principal');
                        _mostrar_mensaje(result.Titulo, result.Mensaje);
                        _consultar_categorias();
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