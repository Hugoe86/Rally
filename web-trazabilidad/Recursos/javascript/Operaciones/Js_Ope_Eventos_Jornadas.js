

//  -----------------------------------------------------
//  -----------------------------------------------------
function _inicializar_vista_jornadas() {
    try {
        _eventos_principal_jornada();

        //  jornadas
        crear_tabla_Jornadas();

        _load_cmb_tipo_jornada('cmb_tipo_jornada');

    } catch (e) {
        _mostrar_mensaje('Error Técnico', e);
    }
}

//  -----------------------------------------------------
//  -----------------------------------------------------
function _inicializar_vista_operaciones_jornadas() {
    try {

        _eventos_procesos_jornada();
        _limpiar_todos_controles_procesos_jornada();

        _load_cmb_tipo_jornada('cmb_tipo_jornada');

        _inicializar_fechas_operacion_jornadas('dtp_txt_fecha_inicio_jornada');


    } catch (e) {
        _mostrar_mensaje('Error Técnico', e);
    }
}

//  -----------------------------------------------------
//  -----------------------------------------------------
function _eventos_principal_jornada() {

    $('#btn_nuevo_jornada').on('click', function (e) {
        e.preventDefault();

        _mostrar_vista_jornadas('Operacion');
        _limpiar_todos_controles_procesos_jornada();
        _Consultar_Clave_Jornada_Automatica();
        _habilitar_controles_jornadas("Nuevo");
    });

    /* =============================================
       --NOMBRE_FUNCIÓN:       btn_layout_jornada
       --DESCRIPCIÓN:          Evento con el que se muestra el panel de layout de jornada
       --PARÁMETROS:           e: parámetro que se refiere al evento click
       --CREO:                 Hugo Enrique Ramírez Aguilera
       --FECHA_CREO:           27 de Julio de 2020
       --MODIFICÓ:
       --FECHA_MODIFICÓ:
       --CAUSA_MODIFICACIÓN:
       =============================================*/
    $('#btn_layout_jornada').on('click', function (e) {
       
        e.preventDefault();

        //  se limpian los controles
        _limpiar_todos_controles_layout();

        //  se ejecuta el modal
        //_launch_modal_layout('<i class="fa fa-list-alt" style="font-size: 25px; color: #0e62c7;"></i>&nbsp;&nbsp;Carga masiva');
        _mostrar_vista_jornadas("Layout_Jornadas");

    });
}


//  -----------------------------------------------------
//  -----------------------------------------------------
function _mostrar_vista_jornadas(vista_) {

    switch (vista_) {
        case "Principal":
            $('#JornadasPrincipal').show();

            $('#JornadaOperaciones').hide();
            $('#PuntoControl').hide();
            $('#Jornadas_layout').hide();
            $('#PuntoControl_layout').hide();

            break;

        case "Operacion":

            $('#JornadaOperaciones').show();

            $('#JornadasPrincipal').hide();
            $('#PuntoControl').hide();
            $('#Jornadas_layout').hide();
            $('#PuntoControl_layout').hide();

            break;

        case "Punto":

            $('#PuntoControl').show();

            $('#JornadasPrincipal').hide();
            $('#JornadaOperaciones').hide();
            $('#Jornadas_layout').hide();
            $('#PuntoControl_layout').hide();
            
            break;

        //  valores para el layout de jornadas
        case "Layout_Jornadas":

            $('#Jornadas_layout').show();

            $('#JornadasPrincipal').hide();
            $('#JornadaOperaciones').hide();
            $('#PuntoControl').hide();
            $('#PuntoControl_layout').hide();
            
            break;

        //  valores para el layout de jornadas
        case "Layout_Puntos_Control":

            $('#PuntoControl_layout').show();

            $('#JornadasPrincipal').hide();
            $('#JornadaOperaciones').hide();
            $('#PuntoControl').hide();
            $('#Jornadas_layout').hide();

            break;
    }
}

//  -----------------------------------------------------
//  -----------------------------------------------------
function crear_tabla_Jornadas() {

    try {
        $('#tbl_jornadas_principal').bootstrapTable('destroy');

        $('#tbl_jornadas_principal').bootstrapTable({
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

            detailView: true,
            onExpandRow: function (index, row, $detail) {
                expandTable($detail, row);
            },

            columns: [

                { field: 'Jornada_Id', title: 'Jornada_Id', align: 'center', valign: 'top', visible: false, sortable: true },
                { field: 'Evento_Id', title: 'Evento_Id', align: 'center', valign: 'top', visible: false, sortable: true },

                { field: 'Clave', title: 'Clave', align: 'center', valign: 'top', visible: true, sortable: true },
                { field: 'Nombre', title: 'Nombre de la jornada', align: 'center', valign: 'top', visible: true, sortable: true },
                { field: 'Punto_Inicial', title: 'Punto inicial', align: 'center', valign: 'top', visible: true, sortable: true },
                { field: 'Punto_Final', title: 'Punto final', align: 'center', valign: 'top', visible: true, sortable: true },

              

                { field: 'Comentarios', title: 'Comentarios', align: 'center', valign: 'top', visible: true, sortable: true },

                { field: 'Estatus', title: 'Estatus', align: 'center', valign: 'top', visible: false, sortable: true },
                { field: 'Tipo', title: 'Tipo', align: 'center', valign: 'top', visible: false, sortable: true },
                {
                    field: 'Fecha_Inicio', title: 'Fecha inicio', align: 'left', valign: 'top', sortable: true, visible: false, formatter: function (value, row, index) {
                        if (row.Fecha_Inicio != null)
                            return new Date(value).toString('dd/MMMM/yyyy');
                    }
                },

                 {
                     field: 'Nuevo',
                     title: 'Nuevo',
                     align: 'right',
                     valign: 'top',
                     halign: 'center',

                     formatter: function (value, row) {

                         var opciones = '<div style=" text-align: center;">';

                         opciones += '<div style="display:block">';

                         opciones += '<a class="remove ml10 text-purple" id="' + row.Jornada_Id + '" href="javascript:void(0)" data-orden=\'' + JSON.stringify(row) + '\' onclick="btn_nuevo_punto_control_click(this);" title="Nuevo"><i class="glyphicon glyphicon-plus"></i>&nbsp;<span style="font-size:11px !important;"></span></a>';


                         opciones += '</div>';

                         opciones += '</div>';

                         return opciones;
                     }
                 },

                {
                    field: 'Editar',
                    title: 'Editar',
                    align: 'right',
                    valign: 'top',
                    halign: 'center',

                    formatter: function (value, row) {

                        var opciones = '<div style=" text-align: center;">';

                        opciones += '<div style="display:block">';

                        opciones += '<a class="remove ml10 text-purple" id="' + row.Jornada_Id + '" href="javascript:void(0)" data-orden=\'' + JSON.stringify(row) + '\' onclick="btn_editar_jornada_click(this);" title="Editar"><i class="glyphicon glyphicon-edit"></i>&nbsp;<span style="font-size:11px !important;"></span></a>';


                        opciones += '</div>';

                        opciones += '</div>';

                        return opciones;
                    }
                },

                {
                    field: 'Eliminar',
                    title: 'Eliminar',
                    align: 'right',
                    valign: 'top',
                    halign: 'center',

                    formatter: function (value, row) {

                        var opciones = '<div style=" text-align: center;">';

                        opciones += '<div style="display:block">';

                        opciones += '<a class="remove ml10 text-red" id="' + row.Evento_Id + '" href="javascript:void(0)" data-orden=\'' + JSON.stringify(row) + '\' onclick="btn_eliminar_jornada_click(this);" title="Eliminar"><i class="glyphicon glyphicon-trash"></i>&nbsp;<span style="font-size:11px !important;"></span></a>';


                        opciones += '</div>';

                        opciones += '</div>';

                        return opciones;
                    }
                },

                {
                    field: 'Layout_Punto_Control',
                    title: 'Layout',
                    width: 80,
                    align: 'right',
                    valign: 'top',
                    halign: 'center',

                    /* =============================================
                    --NOMBRE_FUNCIÓN:        formatter: function (value, row) {
                    --DESCRIPCIÓN:          Evento con el que se da estilo a la celda
                    --PARÁMETROS:           value: es el valor de la celda
                    --                      row: estructura del renglón de la tabla
                    --CREO:                 Hugo Enrique Ramírez Aguilera
                    --FECHA_CREO:           17 Agosto 2020
                    --MODIFICÓ:
                    --FECHA_MODIFICÓ:
                    --CAUSA_MODIFICACIÓN:
                    =============================================*/
                    formatter: function (value, row) {
                        var opciones;//   variable para formar la estructura del botón

                        opciones = '<div style=" text-align: center;">';
                        opciones += '<div style="display:block"><a class="remove ml10 text-warning" id="' +
                            row.Beneficiario_Id + '" href="javascript:void(0)" data-orden=\'' + JSON.stringify(row) +
                            '\' onclick="btn_layout_punto_control_click(this);" title="Layout"><i class="fa fa-book"></i>&nbsp;<span style="font-size:11px !important;"></span></a></div>';
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

function expandTable($detail, row) {
    _crear_tbl_puntos_control($detail.html('<table class="table table-responsive header-subtable"></table>').find('table'), row);
}


//  -----------------------------------------------------
//  -----------------------------------------------------

function expandTable_responsables($detail, row) {
    _crear_tbl_responsable($detail.html('<table class="table table-responsive header-subtable2"></table>').find('table'), row);
}

//  -----------------------------------------------------
//  -----------------------------------------------------
function _crear_tbl_puntos_control($el, rows) {

    var filtro = new Object();
    var Datos = [];

    try {

        filtro.Jornada_Id = rows.Jornada_Id;
        $data = JSON.stringify({ 'jsonObject': JSON.stringify(filtro) });

        $.ajax({
            type: 'POST',
            url: 'controllers/EventosPtsCtrlController.asmx/Consultar_Puntos_Control',
            data: $data,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            cache: false,
            success: function (result) {
                Datos = JSON.parse(result.d);
            }
        });
        $el.bootstrapTable('destroy');
        $el.bootstrapTable({
            cache: false,
            striped: true,
            pagination: false,
            smartDisplay: false,
            search: false,
            showColumns: false,
            showRefresh: false,
            showHeader: true,
            minimumCountColumns: 2,

            detailView: true,
            onExpandRow: function (index, row, $detail) {
                expandTable_responsables($detail, row);
            },


            columns: [

                { field: 'Punto_Control_Id', title: 'Punto_Control_Id', align: 'center', valign: 'top', visible: false, sortable: true },
                { field: 'Jornada_Id', title: 'Jornada_Id', align: 'center', valign: 'top', visible: false, sortable: true },
                { field: 'Evento_Id', title: 'Evento_Id', align: 'center', valign: 'top', visible: false, sortable: true },

                { field: 'Renglon', title: 'Renglon', align: 'center', valign: 'top', visible: false, sortable: true },
                { field: 'Distancia', title: 'Distancia', align: 'center', valign: 'top', visible: false, sortable: true },
                { field: 'Seña', title: 'Seña', align: 'center', valign: 'top', visible: false, sortable: true },

                { field: 'Clave', title: 'Clave', align: 'center', valign: 'top', visible: true, sortable: true },
                { field: 'Numero', title: 'Numero', align: 'center', valign: 'top', visible: true, sortable: true },

                 {
                     field: 'Fecha', title: 'Fecha inicio', align: 'left', valign: 'top', sortable: true, visible: true, formatter: function (value, row, index) {
                         if (row.Fecha != null)
                             return new Date(value).toString('dd/MMMM/yyyy');
                     }
                 },
                {
                    field: 'Str_Hora_Inicio', title: 'Hora de inicio', align: 'left', valign: 'top', sortable: true, visible: true, formatter: function (value, row, index) {
                        if (row.Str_Hora_Inicio != null)
                            return new Date('1/1/2018 ' + value).toString('HH:mm');
                    }
                },

                {
                    field: 'Str_Hora_Fin', title: 'Hora de termino', align: 'left', valign: 'top', sortable: true, visible: true, formatter: function (value, row, index) {
                        if (row.Str_Hora_Fin != null)
                            return new Date('1/1/2018 ' + value).toString('HH:mm');
                    }
                },
                //{
                //    field: 'Str_Tiempo_Ideal', title: 'Tiempo ideal', align: 'left', valign: 'top', sortable: true, visible: true, formatter: function (value, row, index) {
                //        if (row.Str_Tiempo_Ideal != null)
                //            return new Date('1/1/2018 ' + value).toString('HH:mm');
                //    }
                //},


                 //{
                 //    field: 'Str_Intervalo', title: 'Intervalo', align: 'left', valign: 'top', sortable: true, visible: true, formatter: function (value, row, index) {
                 //        if (row.Str_Intervalo != null) {
                 //            var segundos;

                 //            segundos = calcular_total_segundos(value);

                 //            return segundos;
                 //        }
                 //    }

                 //},

                { field: 'Estatus', title: 'Estatus', align: 'center', valign: 'top', visible: false, sortable: true },

                 {
                     field: 'Punto_Control_Id',
                     title: '',
                     align: 'right',
                     valign: 'top',
                     halign: 'center',

                     formatter: function (value, row) {

                         var opciones = '<div style=" text-align: center;">';

                         opciones += '<div style="display:block">';

                         opciones += '<a class="remove ml10 text-blue" id="' + row.Punto_Control_Id + '" href="javascript:void(0)" data-orden=\'' + JSON.stringify(row) + '\' onclick="btn_editar_punto_click(this);" title="Editar"><i class="glyphicon glyphicon-edit"></i>&nbsp;<span style="font-size:11px !important;">Editar</span></a>';


                         opciones += '</div>';

                         opciones += '</div>';

                         return opciones;
                     }
                 },

                {
                    field: 'Punto_Control_Id',
                    title: '',
                    align: 'right',
                    valign: 'top',
                    halign: 'center',

                    formatter: function (value, row) {

                        var opciones = '<div style=" text-align: center;">';

                        opciones += '<div style="display:block">';

                        opciones += '<a class="remove ml10 text-red" id="' + row.Punto_Control_Id + '" href="javascript:void(0)" data-orden=\'' + JSON.stringify(row) + '\' onclick="btn_eliminar_punto_click(this);" title="Eliminar"><i class="glyphicon glyphicon-trash"></i>&nbsp;<span style="font-size:11px !important;">Eliminar</span></a>';


                        opciones += '</div>';

                        opciones += '</div>';

                        return opciones;
                    }
                },

             

            ], data: Datos,
        });

    } catch (e) {
        _mostrar_mensaje('Informe Técnico', e);
    }
}


//  -----------------------------------------------------
//  -----------------------------------------------------
function _crear_tbl_responsable($el, rows) {

    var filtro = new Object();
    var Datos = [];

    try {

        filtro.Punto_Control_Id = rows.Punto_Control_Id;
        $data = JSON.stringify({ 'jsonObject': JSON.stringify(filtro) });

        $.ajax({
            type: 'POST',
            url: 'controllers/EventosPtsCtrlController.asmx/Consultar_Responables_Punto_Control',
            data: $data,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            cache: false,
            success: function (result) {
                Datos = JSON.parse(result.d);
            }
        });

       

        $el.bootstrapTable('destroy');
        $el.bootstrapTable({
            cache: false,
            striped: true,
            pagination: false,
            smartDisplay: false,
            search: false,
            showColumns: false,
            showRefresh: false,
            showHeader: true,
            minimumCountColumns: 2,


            columns: [

                { field: 'Relacion_Operador_Id', title: 'Relacion_Operador_Id', align: 'center', valign: 'top', visible: false, sortable: true },
                { field: 'Responsable_Id', title: 'Responsable_Id', align: 'center', valign: 'top', visible: false, sortable: true },
                { field: 'Punto_Control_Id', title: 'Punto_Control_Id', align: 'center', valign: 'top', visible: false, sortable: true },
                { field: 'Evento_Id', title: 'Evento_Id', align: 'center', valign: 'top', visible: false, sortable: true },
                { field: 'Estatus', title: 'Estatus', align: 'center', valign: 'top', visible: false, sortable: true },

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
                

            ], data: Datos,
        });

    } catch (e) {
        _mostrar_mensaje('Informe Técnico', e);
    }
}


//  -----------------------------------------------------
//  -----------------------------------------------------
function _limpiar_todos_controles_procesos_jornada() {

    try {

        $('#frmJornadas input[type=text]').each(function () { $(this).val(''); });
        $('#frmJornadas input[type=hidden]').each(function () { $(this).val(''); });

    } catch (e) {
        _mostrar_mensaje('Error Técnico', 'limpiar controles. ' + e);
    }
}


//  -----------------------------------------------------
//  -----------------------------------------------------

function _Consultar_Jornadas() {
    var filtros = null;
    try {
        filtros = new Object();

        filtros.Evento_Id = parseInt($("#txt_evento_id").val());
        var $data = JSON.stringify({ 'jsonObject': JSON.stringify(filtros) });

        $.ajax({
            type: 'POST',
            url: 'controllers/EventosJornadasController.asmx/Consultar_Jornadas_Evento',
            data: $data,
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            async: false,
            cache: false,
            success: function (datos) {
                if (datos !== null) {
                    datos = JSON.parse(datos.d);
                    $('#tbl_jornadas_principal').bootstrapTable('load', datos);
                }
            }
        });
    } catch (e) {
        _mostrar_mensaje('Error ', e);
    }
}

//  -----------------------------------------------------
//  -----------------------------------------------------

function _Consultar_Clave_Jornada_Automatica() {
    var filtros = null;
    try {
        filtros = new Object();

        filtros.Evento_Id = parseInt($("#txt_evento_id").val());
        var $data = JSON.stringify({ 'jsonObject': JSON.stringify(filtros) });

        $.ajax({
            type: 'POST',
            url: 'controllers/EventosJornadasController.asmx/Consultar_Clave_Automatica_Jornada',
            data: $data,
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            async: false,
            cache: false,
            success: function (datos) {
                if (datos !== null) {
                    datos = JSON.parse(datos.d);
                    $("#txt_clave_jornada").val(datos.length + 1);
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
function _load_cmb_tipo_jornada(cmb) {
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
                    id: 'TRANSITO',
                    text: 'TRANSITO',
                },
                {
                    id: 'REGULARIDAD',
                    text: 'REGULARIDAD',
                },
               {
                   id: 'OTRA',
                   text: 'OTRA',
               },
            ],
        });

    } catch (e) {
        _mostrar_mensaje('Informe técnico' + '[_load_cmb_estatus]', e);
    }
}


//  -----------------------------------------------------
//  -----------------------------------------------------
function _inicializar_fechas_operacion_jornadas(txt) {
    //------------------------------------------------------------------
    //------------------------------------------------------------------
    $('#' + txt).datetimepicker({
        defaultDate: new Date(),
        viewMode: 'days',
        locale: 'es',
        format: "DD/MM/YYYY"
    });
    $("#" + txt).datetimepicker("useCurrent", true);
    //------------------------------------------------------------------
    //------------------------------------------------------------------
}
//  -----------------------------------------------------
//  -----------------------------------------------------
function _inicializar_horas_operacion_jornadas(txt) {
    //------------------------------------------------------------------
    $('#' + txt).datetimepicker({
        defaultDate: new Date(),
        locale: 'es',
        format: "LT"
    });
    $("#" + txt).datetimepicker("useCurrent", true);
    //------------------------------------------------------------------

}



//  -----------------------------------------------------
//  -----------------------------------------------------

function _habilitar_controles_jornadas(opc) {
    var Estatus = true;
    try {

        Estatus = false;
     
        switch (opc) {
            case 'Nuevo':
                $('#btn_guardar_jornada').attr('title', 'Guardar');
                break;


            case 'Modificar':
                $('#btn_guardar_jornada').attr('title', 'Actualizar');
                break;

        }


    } catch (e) {
        _mostrar_mensaje('Error Técnico' + ' [_habilitar_controles] ', e);
    }

}

//  -----------------------------------------------------
//  -----------------------------------------------------




///*******************Validaciones*************************///
//  -----------------------------------------------------
//  -----------------------------------------------------
function _validarDatos_Jornada_Nuevo() {
    var _output = new Object();

    try {
        _output.Estatus = true;
        _output.Mensaje = '';
        //  ---------------------------------------------------------------------------------------
        //  ---------------------------------------------------------------------------------------
        //  datos del evento
        if ($('#txt_clave_jornada').val() == '' || $('#txt_clave_jornada').val() == undefined) {
            _output.Estatus = false;
            _output.Mensaje += '&nbsp;<i class="fa fa-angle-double-right"></i>&nbsp;La clave de la jornada.<br />';
        }


        if ($('#txt_nombre_jornada').val() == '' || $('#txt_nombre_jornada').val() == undefined) {
            _output.Estatus = false;
            _output.Mensaje += '&nbsp;<i class="fa fa-angle-double-right"></i>&nbsp;El nombre de la jornada.<br />';
        }


        if ($('#cmb_tipo_jornada').val() == '' || $('#cmb_tipo_jornada').val() == undefined) {
            _output.Estatus = false;
            _output.Mensaje += '&nbsp;<i class="fa fa-angle-double-right"></i>&nbsp;El tipo de la jornada.<br />';
        }

        if ($('#txt_punto_inicio_jornada').val() == '' || $('#txt_punto_inicio_jornada').val() == undefined) {
            _output.Estatus = false;
            _output.Mensaje += '&nbsp;<i class="fa fa-angle-double-right"></i>&nbsp;La punto inicial de la jornada.<br />';
        }

        if ($('#txt_punto_fin_jornada').val() == '' || $('#txt_punto_fin_jornada').val() == undefined) {
            _output.Estatus = false;
            _output.Mensaje += '&nbsp;<i class="fa fa-angle-double-right"></i>&nbsp;El punto final de la jornada.<br />';
        }

        if ($('#txt_fecha_inicio_jornada').val() == '' || $('#txt_fecha_inicio_jornada').val() == undefined) {
            _output.Estatus = false;
            _output.Mensaje += '&nbsp;<i class="fa fa-angle-double-right"></i>&nbsp;La fecha de inicio de la jornada.<br />';
        }


        if ($('#txt_comentarios_jornada').val() == '' || $('#txt_comentarios_jornada').val() == undefined) {
            _output.Estatus = false;
            _output.Mensaje += '&nbsp;<i class="fa fa-angle-double-right"></i>&nbsp;Los comentarios de la jornada.<br />';
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

///********************BD***************///
function Alta_Jornada() {
    var obj = new Object();

    try {

        obj.Evento_Id = parseInt($('#txt_evento_id').val());
        obj.Clave = $('#txt_clave_jornada').val();
        obj.Nombre = $('#txt_nombre_jornada').val();
        obj.Tipo = ($('#cmb_tipo_jornada').val());
        obj.Punto_Inicial = $('#txt_punto_inicio_jornada').val();
        obj.Punto_Final = $('#txt_punto_fin_jornada').val();
        obj.Fecha_Inicio = parseDate($('#txt_fecha_inicio_jornada').val());
        obj.Comentarios = $('#txt_comentarios_jornada').val();

        var $data = JSON.stringify({ 'jsonObject': JSON.stringify(obj) });

        $.ajax({
            type: 'POST',
            url: 'controllers/EventosJornadasController.asmx/Alta',
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
function Modificar_Jornada() {
    var obj = new Object();

    try {
        
        obj.Jornada_Id = parseInt($('#txt_jornada_id').val());
        obj.Evento_Id = parseInt($('#txt_evento_id').val());
        obj.Clave = $('#txt_clave_jornada').val();
        obj.Nombre = $('#txt_nombre_jornada').val();
        obj.Tipo = ($('#cmb_tipo_jornada').val());
        obj.Punto_Inicial = $('#txt_punto_inicio_jornada').val();
        obj.Punto_Final = $('#txt_punto_fin_jornada').val();
        obj.Fecha_Inicio = parseDate($('#txt_fecha_inicio_jornada').val());
        obj.Comentarios = $('#txt_comentarios_jornada').val();

        var $data = JSON.stringify({ 'jsonObject': JSON.stringify(obj) });

        $.ajax({
            type: 'POST',
            url: 'controllers/EventosJornadasController.asmx/Modificar',
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


//  ---------------------------------------------------------------------------------
//  ---------------------------------------------------------------------------------
function _eventos_procesos_jornada() {
    try {
        //  ---------------------------------------------------------------------------------
        //  ---------------------------------------------------------------------------------
        $('#btn_guardar_jornada').click(function (e) {
            e.preventDefault();


            var title = $('#btn_guardar_jornada').attr('title');

            var valida_datos_requerido = _validarDatos_Jornada_Nuevo();
            if (valida_datos_requerido.Estatus) {

                if (title == "Guardar") {
                    Alta_Jornada()
                }
                else {
                    Modificar_Jornada();
                }
            }
            else {
                _mostrar_mensaje('Información', valida_datos_requerido.Mensaje);
            }

        });
        //  ---------------------------------------------------------------------------------
        //  ---------------------------------------------------------------------------------
        $('#btn_cancelar_jornada').on('click', function (e) {
            e.preventDefault();
            _mostrar_vista_jornadas('Principal');
            _limpiar_todos_controles_procesos_jornada();
        });

        //  ---------------------------------------------------------------------------------
        //  ---------------------------------------------------------------------------------

    } catch (e) {
        _mostrar_mensaje('Error Técnico', e);
    }
}


//  -----------------------------------------------------
//  -----------------------------------------------------

function btn_eliminar_jornada_click(tab) {

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

                    filtros.Jornada_Id = row.Jornada_Id;
                    var $data = JSON.stringify({ 'jsonObject': JSON.stringify(filtros) });

                    $.ajax({
                        type: 'POST',
                        url: 'controllers/EventosJornadasController.asmx/Cancelacion',
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

function btn_editar_jornada_click(tab) {

    var row = $(tab).data('orden');

    if ($('#txt_estatus').val() == "ACTIVO" || $('#txt_estatus').val() == "INACTIVO") {

        _limpiar_todos_controles_procesos_jornada();

        $('#txt_jornada_id').val(row.Jornada_Id);
        $('#txt_clave_jornada').val(row.Clave);
        $('#txt_nombre_jornada').val(row.Nombre);
        $('#txt_punto_inicio_jornada').val(row.Punto_Inicial);
        $('#txt_punto_fin_jornada').val(row.Punto_Final);
        $('#txt_comentarios_jornada').val(row.Comentarios);

        if (row.Fecha_Inicio != "01/01/0001 00:00:00") {
            $('#txt_fecha_inicio_jornada').val(new Date(row.Fecha_Inicio).toString('dd/MM/yyyy'));

        }
        if (row.Tipo != null)
            $('#cmb_tipo_jornada').select2("trigger", "select", {
                data: { id: row.Tipo, text: row.Tipo }
            });

        _habilitar_controles_jornadas("Modificar");
        _mostrar_vista_jornadas('Operacion');

    }
    else {
        _mostrar_mensaje('Informe Técnico' + '', 'No puede ser editado');
    }
}



/* =============================================
--NOMBRE_FUNCIÓN:       btn_layout_punto_control_click
--DESCRIPCIÓN:          Evento con el que se muestra el panel de layout de jornada
--PARÁMETROS:           e: parámetro que se refiere al evento click
--CREO:                 Hugo Enrique Ramírez Aguilera
--FECHA_CREO:           27 de Julio de 2020
--MODIFICÓ:
--FECHA_MODIFICÓ:
--CAUSA_MODIFICACIÓN:
=============================================*/
function btn_layout_punto_control_click(tab) {

    var row = $(tab).data('orden');
    if ($('#txt_estatus').val() == "ACTIVO" || $('#txt_estatus').val() == "INACTIVO") {

        //  se limpian los controles
        _limpiar_tablas_puntos_control();
        _limpiar_todos_controles_layout_puntos_control();

        $('#txt_jornada_id_layout_puntos_control').val(row.Jornada_Id);
        $('#txt_jornada_nombre_puntos_control').val(row.Nombre);


        //  se ejecuta el modal
        _mostrar_vista_jornadas("Layout_Puntos_Control");
    }
    else {
        _mostrar_mensaje('Informe Técnico' + '', 'No puede ser editado');
    }
}

