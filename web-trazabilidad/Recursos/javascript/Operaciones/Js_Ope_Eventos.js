var _index = null;
var row_partida = null;


$(document).on('ready', function () {
    _load_vistas();
});



function _load_vistas() {
    _launchComponent('vistas/Eventos/Principal.html', 'Principal');
    _launchComponent('vistas/Eventos/Operacion.html', 'Operacion');
    _launchComponent('vistas/Eventos/Puntos_Control.html', 'PuntoControl');
    _launchComponent('vistas/Eventos/Actividades_Principal.html', 'Actividades_Principal');
    _launchComponent('vistas/Eventos/Actividades_Operacion.html', 'Actividades_Operacion');

    //  jornadas
    _launchComponent('vistas/Eventos/Jornadas_Principal.html', 'JornadasPrincipal');
    _launchComponent('vistas/Eventos/Jornada_Layout.html', 'Jornadas_layout');
    _launchComponent('vistas/Eventos/Jornada_Operaciones.html', 'JornadaOperaciones');
    _launchComponent('vistas/Eventos/Puntos_Control_Layout.html', 'PuntoControl_layout');

    //  categorías
    _launchComponent('vistas/Eventos/Categorias_Principal.html', 'Categorias_Principal');
    _launchComponent('vistas/Eventos/Categorias_Operacion.html', 'Categorias_Operacion');

    // vehículos
    _launchComponent('vistas/Eventos/Vehiculos_Principal.html', 'VehiculosPrincipal');
    _launchComponent('vistas/Eventos/Vehiculos_Operacion.html', 'VehiculosOperacion');
}


function _launchComponent(component, id) {

    $('#' + id).load(component, function () {

        switch (id) {
            case 'Principal':
                _inicializar_vista_principal();
                break;
            case 'Operacion':
                _inicializar_vista_procesos();
                break;
            case 'Actividades_Principal':
                _inicializar_vista_actividades_principal();
                break;
            case 'Actividades_Operacion':
                _inicializar_vista_actividades_procesos();
                break;
            case "JornadasPrincipal":
                _inicializar_vista_jornadas();
                break;

            case "Jornadas_layout":
                _inicializar_vista_layout_jornadas();
                break;

            case "PuntoControl_layout":
                _inicializar_vista_layout_puntos_control();
                break;


            case "JornadaOperaciones":
                _inicializar_vista_operaciones_jornadas();
                break;
            case "PuntoControl":
                _inicializar_vistapuntoControl();
                break;
            case 'Categorias_Principal':
                _inicializar_vista_categorias_principal();
                break;
            case 'Categorias_Operacion':
                _inicializar_vista_categorias_procesos();
                break;
            case "VehiculosPrincipal":
            	_inicializar_vista_vehiculos();
            	break;
            case "VehiculosOperacion":
                _inicializar_vista_vehiculos_Proceso();
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
        crear_tabla_eventos_principal();
        _set_location_toolbar('toolbar');
        _load_cmb_estatus('cmb_estatus_filtro');
        _eventos_principal();
        _mostrar_vista('Principal');
        _ConsultarFiltros();

    } catch (e) {
        _mostrar_mensaje('Error Técnico', e);
    }
}


//  -----------------------------------------------------
//  -----------------------------------------------------
function _mostrar_vista(vista_) {

    switch (vista_) {
        case "Principal":
            $('#Principal').show();
            $('#Operacion').hide();            
            $('#Tabs').css('display', 'none');

            break;

        case "Operacion":
            $('#Principal').hide();
            $('#Operacion').show();
           
            $("#Tabs").css('display', 'block');

            //  jornadas
            $('#JornadasPrincipal').show();
            $('#JornadaOperaciones').hide();
            $('#PuntoControl').hide();

            //  vehiculos
            $('#VehiculosPrincipal').show();
            $('#VehiculosOperacion').hide();

            
            break;
    }
}
//  -----------------------------------------------------
//  -----------------------------------------------------
function _set_location_toolbar(toolbar) {
    $('#' + toolbar).parent().removeClass("pull-left");
    $('#' + toolbar).parent().addClass("pull-right");

}

//  -----------------------------------------------------
//  -----------------------------------------------------
function _habilitar_controles(opc) {
    var Estatus = true;
    try {

       
        $('#btn_guardar').show();
        $('#btn_cancelar').show();
        $('#btn_regresar_principal').hide();
        $('#btn_agregar_link').show();
        $('#btn_nuevo_actividad').show();
        $('#btn_nuevo_jornada').show();
        $('#btn_nuevo_categoria').show();
        $('#btn_nuevo_vehiculo_participante').show();


        switch (opc) {
            case 'Nuevo':

                $('#txt_clave_evento').prop('disabled', false);
                $('#txt_nombre_evento').prop('disabled', false);
                $('#cmb_estatus').prop('disabled', false);
                $('#txt_fecha_inicio_evento').prop('disabled', false);
                $('#txt_fecha_fin_evento').prop('disabled', false);
                $('#txt_fecha_salida_evento').prop('disabled', false);
                $('#txt_recorrido_completo').prop('disabled', false);
                $('#txt_punto_salida_evento').prop('disabled', false);
                $('#txt_punto_meta_evento').prop('disabled', false);
                $('#txt_hora_salida_evento').prop('disabled', false);
                //$('#txt_intervalo_salida_evento').prop('disabled', false);
                $('#txt_intervalo_evento').prop('disabled', false);
                $('#txt_comentarios_evento').prop('disabled', false);

                //  redes sociales
                $('#txt_nombre_link').prop('disabled', false);
                $('#txt_ruta_link').prop('disabled', false);
              

                $('#btn_guardar').attr('title', 'Guardar');
                $('#Tabs').hide();

                break;


            case 'Modificar':

                $('#txt_clave_evento').prop('disabled', false);
                $('#txt_nombre_evento').prop('disabled', false);
                $('#cmb_estatus').prop('disabled', false);
                $('#txt_fecha_inicio_evento').prop('disabled', false);
                $('#txt_fecha_fin_evento').prop('disabled', false);
                $('#txt_fecha_salida_evento').prop('disabled', false);
                $('#txt_recorrido_completo').prop('disabled', false);
                $('#txt_punto_salida_evento').prop('disabled', false);
                $('#txt_punto_meta_evento').prop('disabled', false);
                $('#txt_hora_salida_evento').prop('disabled', false);
                //$('#txt_intervalo_salida_evento').prop('disabled', false);
                $('#txt_intervalo_evento').prop('disabled', false);
                $('#txt_comentarios_evento').prop('disabled', false);

                //  redes sociales
                $('#txt_nombre_link').prop('disabled', false);
                $('#txt_ruta_link').prop('disabled', false);


                $('#btn_guardar').attr('title', 'Actualizar');
                $('#Tabs').show();

                break;

            case 'Ver':

                _mostrar_vista_jornadas('Principal');

                $('#txt_clave_evento').attr('disabled', 'disabled');
                $('#txt_nombre_evento').attr('disabled', 'disabled');
                $('#cmb_estatus').attr('disabled', 'disabled');
                $('#txt_fecha_inicio_evento').attr('disabled', 'disabled');
                $('#txt_fecha_fin_evento').attr('disabled', 'disabled');
                $('#txt_fecha_salida_evento').attr('disabled', 'disabled');
                $('#txt_recorrido_completo').attr('disabled', 'disabled');
                $('#txt_punto_salida_evento').attr('disabled', 'disabled');
                $('#txt_punto_meta_evento').attr('disabled', 'disabled');
                $('#txt_hora_salida_evento').attr('disabled', 'disabled');
                $('#txt_intervalo_evento').attr('disabled', 'disabled');
                $('#txt_comentarios_evento').attr('disabled', 'disabled');

                //  redes sociales
                $('#txt_nombre_link').attr('disabled', 'disabled');
                $('#txt_ruta_link').attr('disabled', 'disabled');



                $('#btn_guardar').hide();
                $('#btn_cancelar').hide();
                $('#btn_regresar_principal').show();
                $('#btn_agregar_link').hide();
               
                $('#btn_nuevo_actividad').hide();
                $('#btn_nuevo_jornada').hide();
                $('#btn_nuevo_categoria').hide();
                $('#btn_nuevo_vehiculo_participante').hide();
                $('#btn_layout_jornada').hide();
                



                break;
        }


    } catch (e) {
        _mostrar_mensaje('Error Técnico' + ' [_habilitar_controles] ', e);
    }

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
                },
                {
                    id: 'INACTIVO',
                    text: 'INACTIVO',
                },
                 {
                     id: 'INICIADO',
                     text: 'INICIADO',
                 },
                  {
                      id: 'TERMINADO',
                      text: 'TERMINADO',
                  },
            ],
        });

    } catch (e) {
        _mostrar_mensaje('Informe técnico' + '[_load_cmb_estatus]', e);
    }
}
function _load_cmb_estatus_Activo_Inactivo(cmb) {
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
                },
                {
                    id: 'INACTIVO',
                    text: 'INACTIVO',
                },
            ],
        });

    } catch (e) {
        _mostrar_mensaje('Informe técnico' + '[_load_cmb_estatus]', e);
    }
}

//  -----------------------------------------------------
//  -----------------------------------------------------
function _eventos_principal() {
    try {
        $('#btn_inicio').on('click', function (e) {
            e.preventDefault();
            window.location.href = '../Paginas_Generales/Frm_Apl_Principal.aspx';
        });

        $('#btn_nuevo').on('click', function (e) {
            e.preventDefault();


            $("#cmb_estatus").empty().trigger("change");
            _load_cmb_estatus("cmb_estatus");
            _mostrar_vista('Operacion');
            _habilitar_controles('Nuevo');
            _limpiar_todos_controles_procesos();
            $('#tbl_redes_sociales').bootstrapTable('load', []);
            $('#tbl_redes_sociales_eliminar').bootstrapTable('load', []);

            //  se carga el combo como "ACTIVO" y se BLOQUEA
            $('#cmb_estatus').select2("trigger", "select", {
                data: { id: "ACTIVO", text: "ACTIVO"}
            });

            $('#cmb_estatus').prop('disabled', true);

            $('#txt_intervalo_evento').val('30');
            
        });

        $('#btn_busqueda').on('click', function (e) {
            e.preventDefault();
            _ConsultarFiltros();
        });

    } catch (e) {
        _mostrar_mensaje('Error Técnico', e);
    }
}



//  -----------------------------------------------------
//  -----------------------------------------------------
function crear_tabla_eventos_principal() {

    try {
        $('#tbl_eventos_principal').bootstrapTable('destroy');

        $('#tbl_eventos_principal').bootstrapTable({
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

                { field: 'Evento_Id', title: 'Evento_Id', align: 'center', valign: 'top', visible: false, sortable: true },
                { field: 'Clave', title: 'Clave', align: 'center', valign: 'top', visible: true, sortable: true },
                { field: 'Nombre', title: 'Nombre evento', align: 'center', valign: 'top', visible: true, sortable: true },

                 {
                     field: 'Fecha_Inicio', title: 'Fecha inicio', align: 'left', valign: 'top', sortable: true, visible: true, formatter: function (value, row, index) {
                         if (row.Fecha_Inicio != null)
                             return new Date(value).toString('dd/MMMM/yyyy');
                     }
                 },
                 {
                     field: 'Fecha_Fin', title: 'Fecha fin', align: 'left', valign: 'top', sortable: true, visible: true, formatter: function (value, row, index) {
                         if (row.Fecha_Fin != null)
                             return new Date(value).toString('dd/MMMM/yyyy');
                     }
                 },

                { field: 'Punto_Salida', title: 'Punto de salida', align: 'center', valign: 'top', visible: true, sortable: true },
                { field: 'Punto_Meta', title: 'Punto de la meta', align: 'center', valign: 'top', visible: true, sortable: true },
                { field: 'Estatus', title: 'Estatus', align: 'center', valign: 'top', visible: true, sortable: true },



                {
                    field: 'Fecha_Salida', title: 'Fecha salida', align: 'left', valign: 'top', sortable: true, visible: false, formatter: function (value, row, index) {
                        if (row.Fecha_Salida != null)
                            return new Date(value).toString('dd/MMMM/yyyy');
                    }
                },


                { field: 'Recorrido', title: 'Recorrido', align: 'center', valign: 'top', visible: false, sortable: true },



                {
                    field: 'Str_Hora_Salida', title: 'Hora salida', align: 'left', valign: 'top', sortable: true, visible: false, formatter: function (value, row, index) {
                        if (row.Str_Hora_Salida != null)
                            return new Date('1/1/2018 ' + value).toString('HH:mm');
                    }
                },
                {
                    field: 'Str_Intervalo_Salida', title: 'Intervalo salida', align: 'left', valign: 'top', sortable: true, visible: false, formatter: function (value, row, index) {
                        if (row.Str_Intervalo_Salida != null)
                            return new Date('1/1/2018 ' + value).toString('HH:mm');
                    }
                },


                { field: 'Comentarios', title: 'Comentarios', align: 'center', valign: 'top', visible: false, sortable: true },


                {
                    field: 'Evento_Id',
                    title: 'Iniciar',
                    align: 'right',
                    valign: 'top',
                    halign: 'center',

                    formatter: function (value, row) {


                        if (row.Estatus == 'ACTIVO') {
                            var opciones = '<div style=" text-align: center;">';

                            opciones += '<div style="display:block"><a class="remove ml10 text-blue" id="' + row.Evento_Id + '" href="javascript:void(0)" data-orden=\'' + JSON.stringify(row) + '\' onclick="btn_iniciar_click(this);" title="Inicio"><i class="glyphicon glyphicon-play"></i>&nbsp;<span style="font-size:11px !important;"></span></a></div>';


                            opciones += '</div>';

                            return opciones;
                        }
                        else {
                            return '';
                        }

                     
                    }
                },


                    {
                        field: 'Evento_Id',
                        title: 'Pausar',
                        align: 'right',
                        valign: 'top',
                        halign: 'center',

                        formatter: function (value, row) {


                            if (row.Estatus == 'INICIADO') {
                                var opciones = '<div style=" text-align: center;">';

                                opciones += '<div style="display:block"><a class="remove ml10 text-purple" id="' + row.Evento_Id + '" href="javascript:void(0)" data-orden=\'' + JSON.stringify(row) + '\' onclick="btn_pausar_click(this);" title="Pausar"><i class="glyphicon glyphicon-pause"></i>&nbsp;<span style="font-size:11px !important;"></span></a></div>';


                                opciones += '</div>';

                                return opciones;
                            }
                            else {
                                return '';
                            }


                        }
                    },

                {
                    field: 'Evento_Id',
                    title: 'Detener',
                    align: 'right',
                    valign: 'top',
                    halign: 'center',

                    formatter: function (value, row) {

                        if (row.Estatus == 'INICIADO') {
                            var opciones = '<div style=" text-align: center;">';

                            opciones += '<div style="display:block"><a class="remove ml10 text-red" id="' + row.Evento_Id + '" href="javascript:void(0)" data-orden=\'' + JSON.stringify(row) + '\' onclick="btn_detener_click(this);" title="Detener"><i class="glyphicon glyphicon-stop"></i>&nbsp;<span style="font-size:11px !important;"></span></a></div>';


                            opciones += '</div>';

                            return opciones;
                        }
                        else {
                            return '';
                        }

                    }
                },

                {
                    field: 'Evento_Id',
                    title: 'Editar',
                    align: 'right',
                    valign: 'top',
                    halign: 'center',

                    formatter: function (value, row) {

                        if (row.Estatus == 'ACTIVO' || row.Estatus == 'INACTIVO') {
                            var opciones = '<div style=" text-align: center;">';

                            opciones += '<div style="display:block">';

                            opciones += '<a class="remove ml10 text-purple" id="' + row.Evento_Id + '" href="javascript:void(0)" data-orden=\'' + JSON.stringify(row) + '\' onclick="btn_editar_click(this);" title="Editar"><i class="glyphicon glyphicon-edit"></i>&nbsp;<span style="font-size:11px !important;">Editar</span></a>';

                            opciones += '</div>';

                            opciones += '</div>';

                            return opciones;
                        }
                        else {
                            return '';
                        }
                    }
                },


                 {
                     field: 'Evento_Id',
                     title: 'Eliminar',
                     align: 'right',
                     valign: 'top',
                     halign: 'center',

                     formatter: function (value, row) {
                         if (row.Estatus == 'ACTIVO') {
                             var opciones = '<div style=" text-align: center;">';

                             opciones += '<div style="display:block">';

                             opciones += '<a class="remove ml10 text-red" id="' + row.Evento_Id + '" href="javascript:void(0)" data-orden=\'' + JSON.stringify(row) + '\' onclick="btn_eliminar_click(this);" title="Eliminar"><i class="glyphicon glyphicon-trash"></i>&nbsp;<span style="font-size:11px !important;">Eliminar</span></a>';

                             opciones += '</div>';

                             opciones += '</div>';

                             return opciones;
                         }
                         else {
                             return '';
                         }
                     }
                 },

                  {
                      field: 'Evento_Id',
                      title: 'Ver',
                      align: 'right',
                      valign: 'top',
                      halign: 'center',

                      formatter: function (value, row) {
                          if (row.Estatus == 'INICIADO' || row.Estatus == 'TERMINADO') {
                              var opciones = '<div style=" text-align: center;">';

                              opciones += '<div style="display:block">';

                              opciones += '<a class="remove ml10 text-purple" id="' + row.Evento_Id + '" href="javascript:void(0)" data-orden=\'' + JSON.stringify(row) + '\' onclick="btn_ver_click(this);" title="Ver"><i class="glyphicon glyphicon-eye-open"></i>&nbsp;<span style="font-size:11px !important;">Ver</span></a>';

                              opciones += '</div>';

                              opciones += '</div>';

                              return opciones;
                          }
                          else {
                              return '';
                          }
                      }
                  },
            ]
        });
    } catch (e) {
        _mostrar_mensaje('Informe Técnico' + '[crear_tabla_vehiculo]', e.message);
    }
}



//  *******************************************************************************************************************************
//  *******************************************************************************************************************************
///********************CONSULTAS**************///
//  *******************************************************************************************************************************
function _ConsultarFiltros() {
    var filtros = null;
    try {
        filtros = new Object();

        filtros.Clave = $("#txt_clave_busqueda").val();
        filtros.Nombre = $('#txt_nombre_busqueda').val();
        filtros.Estatus = $('#cmb_estatus_filtro').val() === null ? "" : ($('#cmb_estatus_filtro').val());

        var $data = JSON.stringify({ 'jsonObject': JSON.stringify(filtros) });

        $.ajax({
            type: 'POST',
            url: 'controllers/EventosController.asmx/Consultar_Evento_Filtro',
            data: $data,
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            async: false,
            cache: false,
            success: function (datos) {
                if (datos !== null) {
                    datos = JSON.parse(datos.d);
                    $('#tbl_eventos_principal').bootstrapTable('load', datos);
                }
            }
        });
    } catch (e) {
        _mostrar_mensaje('Error ', e);
    }
}


//  -----------------------------------------------------
//  -----------------------------------------------------
function btn_iniciar_click(tab) {
    var row = $(tab).data('orden');


    bootbox.confirm({
        title: 'Iniciar evento',
        message: 'Esta seguro de INICIAR el evento?',
        callback: function (result) {
            if (result) {

                //  documentos
                var filtros = null;
                filtros = new Object();

                filtros.Evento_Id = row.Evento_Id;
                var $data = JSON.stringify({ 'jsonObject': JSON.stringify(filtros) });

                $.ajax({
                    type: 'POST',
                    url: 'controllers/EventosController.asmx/Iniciar_Evento',
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
                        else {

                        }
                    }
                });

            }

        }
    });
}


//  -----------------------------------------------------
//  -----------------------------------------------------
function btn_pausar_click(tab) {
    var row = $(tab).data('orden');


    bootbox.confirm({
        title: 'Pausar evento',
        message: 'Esta seguro de PAUSAR el evento?',
        callback: function (result) {
            if (result) {

                //  documentos
                var filtros = null;
                filtros = new Object();

                filtros.Evento_Id = row.Evento_Id;
                var $data = JSON.stringify({ 'jsonObject': JSON.stringify(filtros) });

                $.ajax({
                    type: 'POST',
                    url: 'controllers/EventosController.asmx/Pausar_Evento',
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
                        else {

                        }
                    }
                });

            }

        }
    });
}

//  -----------------------------------------------------
//  -----------------------------------------------------

function btn_detener_click(tab) {

    var row = $(tab).data('orden');


    bootbox.confirm({
        title: 'Terminar evento',
        message: 'Esta seguro de TERMINAR el evento?',
        callback: function (result) {
            if (result) {

                //  documentos
                var filtros = null;
                filtros = new Object();

                filtros.Evento_Id = row.Evento_Id;
                var $data = JSON.stringify({ 'jsonObject': JSON.stringify(filtros) });

                $.ajax({
                    type: 'POST',
                    url: 'controllers/EventosController.asmx/Terminar_Evento',
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
                        else {

                        }
                    }
                });

            }

        }
    });
}

//  -----------------------------------------------------
//  -----------------------------------------------------
function btn_editar_click(tab) {

    $('#tbl_redes_sociales').bootstrapTable('load', []);
    $('#tbl_redes_sociales_eliminar').bootstrapTable('load', []);

    var row = $(tab).data('orden');

    _habilitar_controles('Modificar');
    _limpiar_todos_controles_procesos();

    
    $('#txt_estatus').val(row.Estatus);
    $('#txt_evento_id').val(row.Evento_Id);
    $('#txt_clave_evento').val(row.Clave);

    $('#txt_nombre_evento').val(row.Nombre);


    if (row.Estatus != null) {

         if (row.Estatus == "INICIADO" || row.Estatus == "TERMINADO") {
             $('#cmb_estatus').prop('disabled', true);

             $("#cmb_estatus").empty().trigger("change");
             _load_cmb_estatus("cmb_estatus");
        }
        else {
             $('#cmb_estatus').prop('disabled', false);

             $("#cmb_estatus").empty().trigger("change");
             _load_cmb_estatus_Activo_Inactivo("cmb_estatus");
         }


         $('#cmb_estatus').select2("trigger", "select", {
            data: { id: row.Estatus, text: row.Estatus }
        });

       
    }



    if (row.Fecha_Inicio != "01/01/0001 00:00:00") {
        $('#txt_fecha_inicio_evento').val(new Date(row.Fecha_Inicio).toString('dd/MM/yyyy'));
    }


    if (row.Fecha_Fin != "01/01/0001 00:00:00") {
        $('#txt_fecha_fin_evento').val(new Date(row.Fecha_Fin).toString('dd/MM/yyyy'));
    }



    if (row.Fecha_Salida != "01/01/0001 00:00:00") {
        $('#txt_fecha_salida_evento').val(new Date(row.Fecha_Salida).toString('dd/MM/yyyy'));
    }


    $('#txt_recorrido_completo').val(row.Recorrido);
    $('#txt_punto_salida_evento').val(row.Punto_Salida);
    $('#txt_punto_meta_evento').val(row.Punto_Meta);
    $('#txt_hora_salida_evento').val(row.Str_Hora_Salida.substring(0,5));
    //$('#txt_intervalo_salida_evento').val(row.Str_Intervalo_Salida.substring(0, 5));
  
    $('#txt_comentarios_evento').val(row.Comentarios);

    var segundos;
    segundos = calcular_total_segundos(row.Str_Intervalo);
    $('#txt_intervalo_evento').val(segundos);

   
    //------------------------------------------------------------------------------------------------------
    //------------------------------------------------------------------------------------------------------
    //  links
    var filtros = null;
    filtros = new Object();
    
    filtros.Evento_Id = row.Evento_Id;
    var $data = JSON.stringify({ 'jsonObject': JSON.stringify(filtros) });

    $.ajax({
        type: 'POST',
        url: 'controllers/EventosController.asmx/Consultar_Link_Evento',
        data: $data,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        async: false,
        cache: false,
        success: function (datos) {
            if (datos !== null) {
                datos = JSON.parse(datos.d);
                $('#tbl_redes_sociales').bootstrapTable('load', datos);

            }
            else {
                $('#tbl_redes_sociales').bootstrapTable('load', []);
            }
        }
    });
    //------------------------------------------------------------------------------------------------------
    //------------------------------------------------------------------------------------------------------
    _mostrar_vista_jornadas('Principal');

    _Consultar_Jornadas();//    js_ope_eventos_jornadas
    _consultar_actividades();
    _Consultar_vehiculos();
    _consultar_categorias();

    _mostrar_vista('Operacion');
}



//  -----------------------------------------------------
//  -----------------------------------------------------
function btn_ver_click(tab) {

    $('#tbl_redes_sociales').bootstrapTable('load', []);
    $('#tbl_redes_sociales_eliminar').bootstrapTable('load', []);

    var row = $(tab).data('orden');

    _habilitar_controles('Ver');
    _limpiar_todos_controles_procesos();


    $('#txt_estatus').val(row.Estatus);
    $('#txt_evento_id').val(row.Evento_Id);
    $('#txt_clave_evento').val(row.Clave);

    $('#txt_nombre_evento').val(row.Nombre);



    $("#cmb_estatus").empty().trigger("change");
    _load_cmb_estatus("cmb_estatus");

    if (row.Estatus != null)
        $('#cmb_estatus').select2("trigger", "select", {
            data: { id: row.Estatus, text: row.Estatus }
        });



    if (row.Fecha_Inicio != "01/01/0001 00:00:00") {
        $('#txt_fecha_inicio_evento').val(new Date(row.Fecha_Inicio).toString('dd/MM/yyyy'));
    }


    if (row.Fecha_Fin != "01/01/0001 00:00:00") {
        $('#txt_fecha_fin_evento').val(new Date(row.Fecha_Fin).toString('dd/MM/yyyy'));
    }



    if (row.Fecha_Salida != "01/01/0001 00:00:00") {
        $('#txt_fecha_salida_evento').val(new Date(row.Fecha_Salida).toString('dd/MM/yyyy'));
    }


    $('#txt_recorrido_completo').val(row.Recorrido);
    $('#txt_punto_salida_evento').val(row.Punto_Salida);
    $('#txt_punto_meta_evento').val(row.Punto_Meta);
    $('#txt_hora_salida_evento').val(row.Str_Hora_Salida.substring(0, 5));

    var intervalo_en_segundos = calcular_total_segundos(row.Str_Intervalo);

    $('#txt_intervalo_evento').val(intervalo_en_segundos);
    $('#txt_comentarios_evento').val(row.Comentarios);

    //------------------------------------------------------------------------------------------------------
    //------------------------------------------------------------------------------------------------------
    //  links
    var filtros = null;
    filtros = new Object();

    filtros.Evento_Id = row.Evento_Id;
    var $data = JSON.stringify({ 'jsonObject': JSON.stringify(filtros) });

    $.ajax({
        type: 'POST',
        url: 'controllers/EventosController.asmx/Consultar_Link_Evento',
        data: $data,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        async: false,
        cache: false,
        success: function (datos) {
            if (datos !== null) {
                datos = JSON.parse(datos.d);
                $('#tbl_redes_sociales').bootstrapTable('load', datos);

            }
            else {
                $('#tbl_redes_sociales').bootstrapTable('load', []);
            }
        }
    });
    //------------------------------------------------------------------------------------------------------
    //------------------------------------------------------------------------------------------------------
    _Consultar_Jornadas();//    js_ope_eventos_jornadas
    _consultar_actividades();
    _Consultar_vehiculos();
    _consultar_categorias();

    _mostrar_vista('Operacion');
}





//  -----------------------------------------------------
//  -----------------------------------------------------
function btn_eliminar_click(tab) {


    var row = $(tab).data('orden');


    bootbox.confirm({
        title: 'INACTIVAR Registro',
        message: 'Esta seguro de INACTIVAR el registro seleccionado?',
        callback: function (result) {
            if (result) {

                //  documentos
                var filtros = null;
                filtros = new Object();

                filtros.Evento_Id = row.Evento_Id;
                var $data = JSON.stringify({ 'jsonObject': JSON.stringify(filtros) });

                $.ajax({
                    type: 'POST',
                    url: 'controllers/EventosController.asmx/Cancelacion',
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
                        else {

                        }
                    }
                });

            }

        }
    });


}


//  -----------------------------------------------------
//  -----------------------------------------------------

/* =============================================
--NOMBRE_FUNCIÓN:       _remove_class_error
--DESCRIPCIÓN:          Se quita el estilo de error de un control 
--PARÁMETROS:           selector: nombre del control al cual se le estará quitando el estilo de error
--CREO:                 Hugo Enrique Ramírez Aguilera
--FECHA_CREO:           27 de Julio de 2020
--MODIFICÓ:
--FECHA_MODIFICÓ:
--CAUSA_MODIFICACIÓN:
=============================================*/
function _remove_class_error(selector) {
    //  se remueve el estilo al control
    $('#' + selector).removeClass('alert-danger');
}

/* =============================================
--NOMBRE_FUNCIÓN:       _add_class_error
--DESCRIPCIÓN:          Se agrego un estilo a un control, el cual se utiliza para las funciones de validación
--PARÁMETROS:           selector: nombre del control al cual se le estará aplicando el estilo de error
--CREO:                 Hugo Enrique Ramírez Aguilera
--FECHA_CREO:           27 de Julio de 2020
--MODIFICÓ:
--FECHA_MODIFICÓ:
--CAUSA_MODIFICACIÓN:
=============================================*/
function _add_class_error(selector) {
    //  se le asigna el estilo al control
    $('#' + selector).addClass('alert-danger');
}
