
$(document).on('ready', function () {
    _load_vistas();
});


function _load_vistas() {
    _launchComponent('vistas/Punto_Control/Principal.html', 'Principal');
}


function _launchComponent(component, id) {

    $('#' + id).load(component, function () {

        switch (id) {
            case 'Principal':
                _inicializar_vista_principal();
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
        //_mostrar_vista('Principal');

        _load_cmb_eventos('cmb_evento_filtro');
        _load_cmb_jornada('cmb_jornada_filtro');
        _load_cmb_puntos('cmb_puntos_filtro');

        consultar_eventos_iniciados();

    } catch (e) {
        _mostrar_mensaje('Error Técnico', e);
    }
}
//  -----------------------------------------------------
//  -----------------------------------------------------

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
                { field: 'Registro_Id', title: '', align: 'center', valign: 'top', visible: false },
                { field: 'No_Vehiculo', title: 'No. Automóvil', align: 'center', valign: 'top', visible: true, sortable: true },
                {
                    field: 'str_Tiempo_Ideal', title: 'Tiempo ideal', align: 'center', valign: 'top', sortable: true, visible: true, formatter: function (value, row, index) {
                        if (row.str_Tiempo_Ideal != null)
                            return new Date('1/1/2018 ' + value).toString('HH:mm:ss');
                    }
                },

                {
                    field: 'str_Tiempo_Ideal', title: 'Todo SEG IDEAL', align: 'center', valign: 'top', sortable: true, visible: true,

                    formatter: function (value, row, index) {

                        if (row.str_Tiempo_Ideal != null)
                        {
                            var horas = 0;
                            var minutos = 0;
                            var segundos = 0;
                            var operacion = 0;

                            var fecha = new Date('1/1/2018 ' + value);

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
                        }
                    }
                },

                //{ field: '', title: 'Todo SEG IDEAL', align: 'center', valign: 'top', visible: true, sortable: true },

                 {
                     field: 'Str_Tiempo_Real', title: 'Horas', align: 'center', valign: 'top', sortable: true, visible: true, formatter: function (value, row, index) {
                         if (row.Str_Tiempo_Real != null) {

                             var hora = new Date('1/1/2018 ' + value).toString('HH');

                             if (hora == "00") {
                                 return "";
                             }
                             else {
                                 return hora;
                             }
                         }
                     }
                 },


                 {
                     field: 'Str_Tiempo_Real', title: 'Minutos', align: 'center', valign: 'top', sortable: true, visible: true, formatter: function (value, row, index) {
                         if (row.Str_Tiempo_Real != null)
                             return new Date('1/1/2018 ' + value).toString('mm');
                     }
                 },

                  {
                      field: 'Str_Tiempo_Real', title: 'Segundos', align: 'center', valign: 'top', sortable: true, visible: true, formatter: function (value, row, index) {
                          if (row.Str_Tiempo_Real != null)
                              return new Date('1/1/2018 ' + value).toString('ss');
                      }
                  },
                {
                    field: 'Str_Tiempo_Real', title: 'Tiempo SEG REAL', align: 'center', valign: 'top', sortable: true, visible: true, formatter: function (value, row, index) {
                        if (row.Str_Tiempo_Real != null) {
                            var horas = 0;
                            var minutos = 0;
                            var segundos = 0;
                            var operacion = 0;

                            var fecha = new Date('1/1/2018 ' + value);

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
                        }
                    }
                },

                { field: 'Puntuacion', title: 'Puntos Malos', align: 'center', valign: 'top', visible: true },
                { field: 'Observaciones', title: 'Obervaciones', align: 'center', valign: 'top', visible: true },
            ]
        });
    } catch (e) {
        _mostrar_mensaje('Informe Técnico' + '[crear_tabla_puntos]', e.message);
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
        //  <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        //  <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        $('#btn_inicio').on('click', function (e) {
            e.preventDefault();
            window.location.href = '../Paginas_Generales/Frm_Apl_Principal.aspx';
        });
        //  <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        //  <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        $('#btn_exportar_pdf').on('click', function (e) {
            e.preventDefault();

            var $table_pagination = $('#tbl_puntos');
            $table_pagination.bootstrapTable('togglePagination');

            $table_pagination.tableExport({
                type: 'pdf',
                worksheetName: 'Etapas',
                fileName: 'Punto de control' + $('#cmb_puntos_filtro :selected').text(),
                jspdf: {
                    orientation: 'l',
                    format: 'a3',
                    margins: { left: 10, right: 10, top: 20, bottom: 20 },
                    autotable: {
                        styles: {
                            fillColor: 'inherit',
                            textColor: 'inherit'
                        },
                        tableWidth: 'auto'
                    }
                }
            });

            $table_pagination.bootstrapTable('tbl_puntos');
        });
        //  <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        //  <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        $('#btn_exportar_excel').on('click', function (e) {
            e.preventDefault();

            var $table_pagination = $('#tbl_puntos');
            $table_pagination.bootstrapTable('togglePagination');
            $table_pagination.tableExport({
                type: 'excel',
                worksheetName: 'Etapas',
                fileName: 'Punto de control' + $('#cmb_puntos_filtro :selected').text(),
            });
            $table_pagination.bootstrapTable('tbl_puntos');

        });
        //  <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        //  <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        $('#btn_busqueda').on('click', function (e) {
            e.preventDefault();
            _ConsultarFiltros();
        });

    } catch (e) {
        _mostrar_mensaje('Error Técnico', e);
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

            filtros.Evento_Id = ($('#cmb_evento_filtro').val() === null ? 0 : parseInt($('#cmb_evento_filtro').val()));
            filtros.Jornada_Id = ($('#cmb_jornada_filtro').val() === null ? 0 : parseInt($('#cmb_jornada_filtro').val()));
            filtros.Punto_Control_Id = ($('#cmb_puntos_filtro').val() === null ? 0 : parseInt($('#cmb_puntos_filtro').val()));
            //filtros.Estatus = $('#cmb_estatus_filtro').val() === null ? "" : ($('#cmb_estatus_filtro').val());

            var $data = JSON.stringify({ 'jsonObject': JSON.stringify(filtros) });

            $.ajax({
                type: 'POST',
                url: 'controllers/Rpt_PuntosControlController.asmx/Consultar_Puntos_Control',
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
        }
        else {
            _mostrar_mensaje('Información', valida_datos_requerido.Mensaje);
        }
    } catch (e) {
        _mostrar_mensaje('Error ', e);
    }
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

            $("#cmb_jornada_filtro").empty().trigger("change");
            $("#cmb_puntos_filtro").empty().trigger("change");

            $('#cmb_jornada_filtro').prop('disabled', false);
            $('#cmb_puntos_filtro').prop('disabled', false);

        });
        //  <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        //  <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        $('#' + cmb).on("select2:unselect", function (evt) {


            $("#cmb_jornada_filtro").empty().trigger("change");
            $("#cmb_puntos_filtro").empty().trigger("change");

            $('#cmb_jornada_filtro').attr('disabled', 'disabled');
            $('#cmb_puntos_filtro').attr('disabled', 'disabled');

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
                        evento_id: $('#cmb_evento_filtro :selected').val()
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

            $('#cmb_puntos_filtro').prop('disabled', false);

        });

        $('#' + cmb).on("select2:unselect", function (evt) {

            $("#cmb_puntos_filtro").empty().trigger("change");

            $('#cmb_puntos_filtro').attr('disabled', 'disabled');
        });

    } catch (e) {
        _mostrar_mensaje('Informe técnico' + '[_load_cmb_vehiculo]', e);
    }
}


//  -----------------------------------------------------
//  -----------------------------------------------------

function _load_cmb_puntos(cmb) {
    try {

        $('#' + cmb).select2({
            language: "es",
            theme: "classic",
            placeholder: 'SELECCIONE',
            allowClear: true,
            ajax: {
                url: '../Operaciones/controllers/EventosPtsCtrlController.asmx/Consultar_Puntos_Combo',
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
                        jornada_id: $('#cmb_jornada_filtro :selected').val()
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

            _consultar_datos_punto_control(_dato_combo.id);

        });

        $('#' + cmb).on("select2:unselect", function (evt) {

            $("#txt_responsable_filtro").val('');
            $("#txt_ubicacion_filtro").val('');
            $("#txt_tiempo_ideal_filtro").val('');
            $("#txt_tiempo_carros_filtro").val('');

        });

    } catch (e) {
        _mostrar_mensaje('Informe técnico' + '[_load_cmb_puntos]', e);
    }
}

//  -----------------------------------------------------
//  -----------------------------------------------------


///*******************Validaciones*************************///
function _validar_datos_filtro() {
    var _output = new Object();

    try {

        _output.Estatus = true;
        _output.Mensaje = '';

        //  ---------------------------------------------------------------------------------------
        //  ---------------------------------------------------------------------------------------
        //  filtros       

        if ($('#cmb_evento_filtro').val() == '' || $('#cmb_evento_filtro').val() == undefined) {
            _output.Estatus = false;
            _output.Mensaje += '&nbsp;<i class="fa fa-angle-double-right"></i>&nbsp;El evento.<br />';
        }

        if ($('#cmb_jornada_filtro').val() == '' || $('#cmb_jornada_filtro').val() == undefined) {
            _output.Estatus = false;
            _output.Mensaje += '&nbsp;<i class="fa fa-angle-double-right"></i>&nbsp;La jornada.<br />';
        }

        if ($('#cmb_puntos_filtro').val() == '' || $('#cmb_puntos_filtro').val() == undefined) {
            _output.Estatus = false;
            _output.Mensaje += '&nbsp;<i class="fa fa-angle-double-right"></i>&nbsp;El punto de control.<br />';
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
                        $('#cmb_evento_filtro').select2("trigger", "select", {
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

//  -----------------------------------------------------
//  -----------------------------------------------------



function _consultar_datos_punto_control(id) {
    var filtros = null;
    try {
        filtros = new Object();


        filtros.Punto_Control_Id = parseInt(id);


        var $data = JSON.stringify({ 'jsonObject': JSON.stringify(filtros) });

        $.ajax({
            type: 'POST',
            url: 'controllers/Rpt_PuntosControlController.asmx/Consultar_Datos_Puntos_Control',
            data: $data,
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            async: false,
            cache: false,
            success: function (datos) {
                if (datos !== null) {
                    datos = JSON.parse(datos.d);

                    if (datos.length > 0) {
                        $('#txt_responsable_filtro').val(datos[0].Responsable);
                        $('#txt_ubicacion_filtro').val(datos[0].Ubicacion);
                        $('#txt_tiempo_ideal_filtro').val(datos[0].Tiempo_Ideal);
                        $('#txt_tiempo_carros_filtro').val(datos[0].Intervalo);
                    }
                    else {

                    }

                }
            }
        });
    } catch (e) {
        _mostrar_mensaje('Error ', e);
    }
}

//  -----------------------------------------------------
//  -----------------------------------------------------

