var _index = null;
var row_partida = null;

var ruta_imagen = "";
ruta_imagen = '../../Recursos/img/No_Disponible.Jpg';

$(document).on('ready', function () {
    _load_vistas();
});


function _load_vistas() {
    _launchComponent('vistas/Relacion/Principal_Participante.html', 'Principal_Participante');
    _launchComponent('vistas/Relacion/Operacion_Participante.html', 'Operacion_Participante');
 

}


function _launchComponent(component, id) {

    $('#' + id).load(component, function () {

        switch (id) {
            case 'Principal_Participante':
                _inicializar_vista_principal();
                break;
            case 'Operacion_Participante':
                _inicializar_vista_procesos();
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
        crear_tabla_participantes();
        _set_location_toolbar('toolbar');
        _load_cmb_estatus('cmb_estatus_participante_filtro');
        _eventos_principal();
        _mostrar_vista('Principal');
        _ConsultarFiltros();

    } catch (e) {
        _mostrar_mensaje('Error Técnico', e);
    }
}
//  -----------------------------------------------------
//  -----------------------------------------------------
function _inicializar_vista_procesos() {
    try {
        crear_tabla_documentos();
        crear_tabla_documentos_eliminados();
        _eventos_operacion();

        _limpiar_todos_controles_procesos();
        _load_cmb_estatus('cmb_estatus_participante');
        _load_cmb_sexo('cmb_sexo_participante');

        _eventos_procesos();
        _inicializar_fechas_operacion();
        //_keyDownInt('txt_año');

    } catch (e) {
        _mostrar_mensaje('Error Técnico', e);
    }
}

function _habilitar_controles(opc) {
    var Estatus = true;
    try {

        Estatus = false;
        var _boton_ = $('#btn_guardar_participante');
        _boton_.show();

        switch (opc) {
            case 'Nuevo':
                $('#btn_guardar_participante').attr('title', 'Guardar');
                break;

            case 'Modificar':
                $('#btn_guardar_participante').attr('title', 'Actualizar');
                break;

            case 'Ver':
                _boton_.hide();
                break;
        }

    } catch (e) {
        _mostrar_mensaje('Error Técnico' + ' [_habilitar_controles] ', e);
    }

}

function _eventos_procesos() {
    try {
        //  ---------------------------------------------------------------------------------
        //  ---------------------------------------------------------------------------------
        $('#btn_guardar_participante').on('click', function (e) {
            e.preventDefault();


            var title = $('#btn_guardar_participante').attr('title');
            var valida_datos_requerido = _validarDatos_Nuevo();
            if (valida_datos_requerido.Estatus) {

                if (title == "Guardar") {
                    alta()
                }
                else {
                    Modificar();
                }
            }
            else {
                _mostrar_mensaje('Información', valida_datos_requerido.Mensaje);
            }

        });
        //  ---------------------------------------------------------------------------------
        //  ---------------------------------------------------------------------------------
        $('#btn_cancelar_participante').on('click', function (e) {
            e.preventDefault();
            _mostrar_vista('Principal');
            _limpiar_todos_controles_procesos();
        });
        //  ---------------------------------------------------------------------------------
        //  ---------------------------------------------------------------------------------
        $('#btn_agregar_documento_participante').on('click', function (e) {
            e.preventDefault();

            var participante_id = "";

            var valida_datos_requerido = _validar_agregar_documento();

            if (valida_datos_requerido.Estatus) {

                var files = $('#txt_ruta_imagen_participante').get(0).files;
                var nombre_Archivo = files[0].name;

                var resultado = subir_archivo("#txt_ruta_imagen_participante", "/rally/Participantes", "Temporales");

                if (resultado === "error") {
                    _mostrar_mensaje('Informe Tecnico', "No se pudo cargar el archivo del certificado");
                }
                else {

                    if ($('#txt_participante_id').val() == '' || $('#txt_participante_id').val() == undefined) {
                        participante_id = 0;
                    }
                    else {
                        participante_id = parseInt($('#txt_participante_id').val());
                    }

                    $('#tbl_documentos_participante').bootstrapTable('insertRow', {
                        index: 0,
                        row: {
                            Adjunto_ID: 0,
                            Participante_ID: participante_id,
                            Nombre_Documento: $('#txt_nombre_documento_participante').val(),
                            Nombre: nombre_Archivo,
                            Ruta: 'Participantes/Temporales/' + nombre_Archivo,
                            Estatus: 'ACTIVO',
                        }
                    });

                    $('#txt_nombre_documento_participante').val('');
                }
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

//  *******************************************************************************************************************************
//  *******************************************************************************************************************************
//  fechas
//  *******************************************************************************************************************************
function _inicializar_fechas_operacion() {
    $('#dtp_txt_fecha_nacimiento_participante').datetimepicker({
        defaultDate: new Date(),
        viewMode: 'days',
        locale: 'es',
        format: "DD/MM/YYYY"
    });
    $("#dtp_txt_fecha_nacimiento_participante").datetimepicker("useCurrent", true);

}

function crear_tabla_participantes() {

    try {
        $('#tbl_participantes').bootstrapTable('destroy');
        $('#tbl_participantes').bootstrapTable({
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
                { field: 'Participante_ID', title: 'Participante_Id', align: 'center', valign: 'top', visible: false },
                { field: 'Clave', title: 'Clave', align: 'left', valign: 'top', visible: true, sortable: true },
                { field: 'Nombre', title: 'Nombre Participante', align: 'left', valign: 'top', visible: true, sortable: true },
                { field: 'Email', title: 'Correo Electónico', align: 'center', valign: 'top', visible: true, sortable: true },
                { field: 'Telefono', title: 'Teléfono', align: 'center', valign: 'top', visible: true, sortable: true },
                { field: 'Direccion', title: 'Dirección', align: 'center', valign: 'top', visible: true },
                { field: 'Notas', title: 'Notas', align: 'center', valign: 'top', visible: false },
                //{ field: 'Estado', title: 'Estado', align: 'center', valign: 'top', visible: true },
                //{ field: 'Municipio', title: 'Municipio', align: 'center', valign: 'top', visible: true },
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
                          else if (value == "BLOQUEADO") {
                              opciones += '           <i class="fa-user-times " style="color:#FF4500;font-size: 14px;"></i>&nbsp;' + value;
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
                    field: 'Editar',
                    title: 'Editar',
                    align: 'right',
                    valign: 'top',
                    halign: 'center',

                    formatter: function (value, row) {

                        var opciones = '<div style=" text-align: center;">';
                        opciones += '<div style="display:block"><a class="remove ml10 text-purple" id="' + row.Adjunto_ID + '" href="javascript:void(0)" data-orden=\'' + JSON.stringify(row) + '\' onclick="btn_editar_participante_click(this);" title="Editar"><i class="glyphicon glyphicon-edit"></i>&nbsp;<span style="font-size:11px !important;"></span></a></div>';
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
                        opciones += '<div style="display:block"><a class="remove ml10 text-red" id="' + row.Documento_Id + '" href="javascript:void(0)" data-orden=\'' + JSON.stringify(row) +
                                        '\' onclick="btn_eliminar_participante_click(this);" title="Eliminar"><i class="glyphicon glyphicon-trash"></i>&nbsp;<span style="font-size:11px !important;"></span></a></div>';
                        opciones += '</div>';

                        return opciones;
                    }
                },

                  {
                      field: 'Bloquear',
                      title: 'Bloquear',
                      align: 'right',
                      valign: 'top',
                      halign: 'center',

                      formatter: function (value, row) {

                          var opciones = '<div style=" text-align: center;">';
                          opciones += '<div style="display:block"><a class="remove ml10 text-red" id="' + row.Documento_Id + '" href="javascript:void(0)" data-orden=\'' + JSON.stringify(row) +
                                        '\' onclick="btn_bloquear_participante_click(this);" title="Bloquear"><i class="fa-user-times "></i>&nbsp;<span style="font-size:11px !important;"></span></a></div>';
                          opciones += '</div>';

                          return opciones;
                      }
                  }
            ]
        });
    } catch (e) {
        _mostrar_mensaje('Informe Técnico' + '[crear_tabla_participantes]', e.message);
    }
}


function crear_tabla_documentos() {
    try {
        $('#tbl_documentos_participante').bootstrapTable('destroy');
        $('#tbl_documentos_participante').bootstrapTable({
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
                { field: 'Adjunto_ID', title: 'Adjunto_ID', align: 'center', valign: 'top', visible: false },
                { field: 'Participante_ID', title: 'Participante_ID', align: 'left', valign: 'top', visible: false },
                { field: 'Nombre', title: 'Nombre', align: 'center', valign: 'top', visible: false },
                { field: 'Nombre_Documento', title: 'Nombre', align: 'center', valign: 'top', visible: true, sortable: true },
                { field: 'Ruta', title: 'Ruta', align: 'center', valign: 'top', visible: false },
                { field: 'Estatus', title: 'Estatus', align: 'center', valign: 'top', visible: true, sortable: true },

                {
                    field: 'Adjunto_ID',
                    title: '',
                    align: 'right',
                    valign: 'top',
                    halign: 'center',

                    formatter: function (value, row) {

                        var opciones = '<div style=" text-align: center;">';
                        opciones += '<div style="display:block"><a class="remove ml10 text-danger" id="' + row.Adjunto_ID + '" href="javascript:void(0)" data-orden=\'' + JSON.stringify(row) + '\' onclick="btn_eliminar_documento(this);" title="Editar"><i class="glyphicon glyphicon-remove"></i>&nbsp;<span style="font-size:11px !important;">Eliminar</span></a></div>';
                        opciones += '</div>';

                        return opciones;
                    }
                },
                {
                    field: 'Adjunto_ID',
                    title: '',
                    align: 'right',
                    valign: 'top',
                    halign: 'center',

                    formatter: function (value, row) {

                        var opciones = '<div style=" text-align: center;">';
                        opciones += '<div style="display:block"><a class="remove ml10 text-purple" id="' + row.Adjunto_ID + '" href="javascript:void(0)" data-orden=\'' + JSON.stringify(row) + '\' onclick="btn_ver_documento(this);" title="Ver"><i class="glyphicon glyphicon-eye-open"></i>&nbsp;<span style="font-size:11px !important;">Ver</span></a></div>';
                        opciones += '</div>';

                        return opciones;
                    }
                },
            ]
        });
    } catch (e) {
        _mostrar_mensaje('Informe Técnico' + '[crear_tabla_documentos]', e);
    }
}


function crear_tabla_documentos_eliminados() {

    try {
        $('#tbl_documentos_eliminados_participante').bootstrapTable('destroy');
        $('#tbl_documentos_eliminados_participante').bootstrapTable({
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
                { field: 'Adjunto_ID', title: 'Participante_ID', align: 'center', valign: 'top', visible: true },
            ]
        });
    } catch (e) {
        _mostrar_mensaje('Informe Técnico' + '[crear_tabla_documentos]', e);
    }
}


//  -----------------------------------------------------
//  -----------------------------------------------------
function _mostrar_vista(vista_) {

    switch (vista_) {
        case "Principal":
            $('#Operacion_Participante').hide();
            $('#Principal_Participante').show();
            break;
        case "Operacion":
            $('#Operacion_Participante').show();
            $('#Principal_Participante').hide();
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
        $('#div_operacion_participante input[type=text]').each(function () { $(this).val(''); });
        $('#div_operacion_participante input[type=hidden]').each(function () { $(this).val(''); });

        $('#img_documento_participante').attr('src', ruta_imagen);

        $('#tbl_documentos_participante').bootstrapTable('load', []);
        $('#tbl_documentos_eliminados_participante').bootstrapTable('load', []);

    } catch (e) {
        _mostrar_mensaje('Error Técnico', 'limpiar controles. ' + e);
    }
}


function _eventos_principal() {
    try {
        //$('#btn_inicio').on('click', function (e) {
        //    e.preventDefault();
        //    window.location.href = '../Paginas_Generales/Frm_Apl_Principal.aspx';
        //});

        $('#btn_nuevo_participante').on('click', function (e) {
            e.preventDefault();
            _habilitar_controles('Nuevo');
            _limpiar_todos_controles_procesos();
            _mostrar_vista('Operacion');

        });

        $('#btn_busqueda_participante').on('click', function (e) {
            e.preventDefault();
            _ConsultarFiltros();
        });

    } catch (e) {
        _mostrar_mensaje('Error Técnico', e);
    }
}


function _eventos_operacion() {
    try {

    } catch (e) {
        _mostrar_mensaje('Error Técnico', e);
    }
}

function btn_editar_participante_click(tab) {
    var row = $(tab).data('orden');

    _habilitar_controles('Modificar');
    _limpiar_todos_controles_procesos();

    $('#txt_participante_id').val(row.Participante_ID);
    $('#txt_clave_participante').val(row.Clave);
    $('#txt_nombre_participante').val(row.Nombre);
    $('#txt_email_participante').val(row.Email);
    $('#txt_telefono_participante').val(row.Telefono);
    $('#txt_celular_participante').val(row.Celular);
    $('#txt_direccion_participante').val(row.Direccion);
    $('#txt_colonia_participante').val(row.Colonia);
    $('#txt_nacionalidad_participante').val(row.Nacionalidad);
    $('#txt_notas_participante').val(row.Notas);

    if (row.Estatus != null)
        $('#cmb_estatus_participante').select2("trigger", "select", {
            data: { id: row.Estatus, text: row.Estatus }
        });
    if (row.Sexo != null)
        $('#cmb_sexo_participante').select2("trigger", "select", {
            data: { id: row.Sexo, text: row.Sexo }
        });

    if (row.Fecha_Nacimiento != "01/01/0001 00:00:00") {
        $('#txt_fecha_nacimiento_participante').val(new Date(row.Fecha_Nacimiento).toString('dd/MM/yyyy'));
    }

    ////  documentos
    //var filtros = null;
    //filtros = new Object();

    //filtros.Participante_ID = $('#txt_participante_id').val();
    //var $data = JSON.stringify({ 'jsonObject': JSON.stringify(filtros) });

    //$.ajax({
    //    type: 'POST',
    //    url: 'controllers/ParticipantesController.asmx/Consultar_Documentos_Participantes',
    //    data: $data,
    //    dataType: "json",
    //    contentType: "application/json; charset=utf-8",
    //    async: false,
    //    cache: false,
    //    success: function (datos) {
    //        if (datos !== null) {
    //            datos = JSON.parse(datos.d);
    //            $('#tbl_documentos_participante').bootstrapTable('load', datos);

    //            if (datos.length >= 1) {
    //                $('#img_documento_participante').attr("src", '../../' + datos[0].Ruta);
    //            }
    //            else {
    //            }
    //        }
    //        else {
    //            $('#tbl_documentos_participante').bootstrapTable('load', "[]");
    //        }
    //    }
    //});

    _mostrar_vista('Operacion');
}

function btn_eliminar_participante_click(tab) {
    var row = $(tab).data('orden');

    bootbox.confirm({
        title: 'INACTIVAR Registro',
        message: 'Esta seguro de INACTIVAR el registro seleccionado?',
        callback: function (result) {
            if (result) {

                //  documentos
                var filtros = null;
                filtros = new Object();

                filtros.Participante_ID = row.Participante_ID;
                var $data = JSON.stringify({ 'jsonObject': JSON.stringify(filtros) });

                $.ajax({
                    type: 'POST',
                    url: 'controllers/ParticipantesController.asmx/Cancelacion',
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
}



function btn_bloquear_participante_click(tab) {
    var row = $(tab).data('orden');

    bootbox.prompt({
        title: 'Motivo por el que desea bloquear al participante',
        inputType: 'textarea',
        callback: function (result) {
            if (result) {

                var filtros = null;
                filtros = new Object();

                filtros.Participante_ID = row.Participante_ID;
                filtros.Notas = result;
                var $data = JSON.stringify({ 'jsonObject': JSON.stringify(filtros) });

                $.ajax({
                    type: 'POST',
                    url: 'controllers/ParticipantesController.asmx/Bloquear',
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

}

function btn_eliminar_documento(tab) {
    var row = $(tab).data('orden');

    if (row.Adjunto_ID == 0) {
        $('#tbl_documentos_participante').bootstrapTable('remove', {
            field: 'Adjunto_ID',
            values: [row.Adjunto_ID],
            field: 'Nombre',
            values: [row.Nombre],
            field: 'Ruta',
            values: [row.Ruta],
        });
    }
    else {
        $('#tbl_documentos_participante').bootstrapTable('remove', {
            field: 'Adjunto_ID',
            values: [row.Adjunto_ID],
        });

        $('#tbl_documentos_eliminados_participante').bootstrapTable('insertRow', {
            index: 0,
            row: {
                Adjunto_ID: row.Adjunto_ID,
            }
        });
    }

}

function btn_ver_documento(tab) {
    var row = $(tab).data('orden');

    $("#img_documento_participante").attr("src", '../../' + row.Ruta);

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
                }, {
                    id: 'BLOQUEADO',
                    text: 'BLOQUEADO',
                },
            ],
        });

    } catch (e) {
        _mostrar_mensaje('Informe técnico' + '[_load_cmb_estatus]', e);
    }
}

function _load_cmb_sexo(cmb) {
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
                    id: 'HOMBRE',
                    text: 'HOMBRE',
                }, {
                    id: 'MUJER',
                    text: 'MUJER',
                },
            ],
        });

    } catch (e) {
        _mostrar_mensaje('Informe técnico' + '[_load_cmb_sexo]', e);
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
        filtros = new Object();

        filtros.Estatus = $('#cmb_estatus_participante_filtro').val() === null ? "" : ($('#cmb_estatus_participante_filtro').val());
        filtros.Nombre = $("#txt_nombre_participante_busqueda").val();
        filtros.Email = $("#txt_email_participante_busqueda").val();

        var $data = JSON.stringify({ 'jsonObject': JSON.stringify(filtros) });

        $.ajax({
            type: 'POST',
            url: 'controllers/ParticipantesController.asmx/Consultar_Participantes_Filtro',
            data: $data,
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            async: false,
            cache: false,
            success: function (datos) {
                if (datos !== null) {
                    datos = JSON.parse(datos.d);
                    $('#tbl_participantes').bootstrapTable('load', datos);
                }
            }
        });
    } catch (e) {
        _mostrar_mensaje('Error ', e);
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
        if ($('#txt_nombre_participante').val() == '' || $('#txt_nombre_participante').val() == undefined) {
            _output.Estatus = false;
            _output.Mensaje += '&nbsp;<i class="fa fa-angle-double-right"></i>&nbsp;El nombre.<br />';
        }
        if ($('#txt_email_participante').val() == '' || $('#txt_email_participante').val() == undefined) {
            _output.Estatus = false;
            _output.Mensaje += '&nbsp;<i class="fa fa-angle-double-right"></i>&nbsp;El correo.<br />';
        }
        if ($('#txt_telefono_participante').val() == '' || $('#txt_telefono_participante').val() == undefined) {
            _output.Estatus = false;
            _output.Mensaje += '&nbsp;<i class="fa fa-angle-double-right"></i>&nbsp;El teléfono.<br />';
        }
        if ($('#cmb_estatus_participante').val() == '' || $('#cmb_estatus_participante').val() == undefined) {
            _output.Estatus = false;
            _output.Mensaje += '&nbsp;<i class="fa fa-angle-double-right"></i>&nbsp;El estatus.<br />';
        }
        if ($('#cmb_sexo_participante').val() == '' || $('#cmb_sexo_participante').val() == undefined) {
            _output.Estatus = false;
            _output.Mensaje += '&nbsp;<i class="fa fa-angle-double-right"></i>&nbsp;El sexo.<br />';
        }
        //if ($('#txt_fecha_nacimiento_participante').val() == '' || $('#txt_fecha_nacimiento_participante').val() == undefined) {
        //    _output.Estatus = false;
        //    _output.Mensaje += '&nbsp;<i class="fa fa-angle-double-right"></i>&nbsp;La fecha de nacimiento.<br />';
        //}

        if (_output.Mensaje != "") {
            _output.Mensaje = "Favor de proporcionar lo siguiente: <br />" + _output.Mensaje;
        }


    } catch (e) {
        _mostrar_mensaje('Informe técnico' + '[_validarDatos_Nuevo]', e);
    } finally {
        return _output;
    }
}

function _validar_agregar_documento() {
    var _output = new Object();

    try {
        _output.Estatus = true;
        _output.Mensaje = '';
        //  ---------------------------------------------------------------------------------------
        //  ---------------------------------------------------------------------------------------
        //  documento
        if ($('#txt_ruta_imagen_participante').val() == '' || $('#txt_ruta_imagen_participante').val() == undefined) {
            _output.Estatus = false;
            _output.Mensaje += '&nbsp;<i class="fa fa-angle-double-right"></i>&nbsp;El documento.<br />';
        }

        if ($('#txt_nombre_documento_participante').val() == '' || $('#txt_nombre_documento_participante').val() == undefined) {
            _output.Estatus = false;
            _output.Mensaje += '&nbsp;<i class="fa fa-angle-double-right"></i>&nbsp;El nombre del documento.<br />';
        }

        if (_output.Mensaje != "") {
            _output.Mensaje = "Favor de proporcionar lo siguiente: <br />" + _output.Mensaje;
        }

    } catch (e) {
        _mostrar_mensaje('Informe técnico' + '[_validar_agregar_documento]', e);
    } finally {
        return _output;
    }
}


///********************BD***************///
function alta() {
    var obj = new Object();
    var clave = "";

    try {
        obj.Clave = obtenerClave();
        obj.Nombre = $('#txt_nombre_participante').val();
        obj.Email = $('#txt_email_participante').val();
        obj.Telefono = $('#txt_telefono_participante').val();
        obj.Celular = $('#txt_celular_participante').val();
        obj.Notas = $('#txt_notas_participante').val();

        if ($('#txt_fecha_nacimiento_participante').val() !== '' || $('#txt_fecha_nacimiento_participante').val() == undefined) {
            obj.Fecha_Nacimiento = parseDate($('#txt_fecha_nacimiento_participante').val());
        }


        obj.Sexo = $('#cmb_sexo_participante').val();
        obj.Estatus = $('#cmb_estatus_participante').val();
        obj.Direccion = $('#txt_direccion_participante').val();
        obj.Colonia = $('#txt_colonia_participante').val();
        obj.Nacionalidad = $('#txt_nacionalidad_participante').val();
        obj.tbl_documentos_participante = JSON.stringify($('#tbl_documentos_participante').bootstrapTable('getData'));

        var $data = JSON.stringify({ 'jsonObject': JSON.stringify(obj) });

        $.ajax({
            type: 'POST',
            url: 'controllers/ParticipantesController.asmx/Alta',
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
                        _ConsultarFiltros();
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


function Modificar() {
    var obj = new Object();

    try {

        obj.Participante_ID = parseInt($('#txt_participante_id').val());
        obj.Clave = obtenerClave();
        obj.Nombre = $('#txt_nombre_participante').val();
        obj.Email = $('#txt_email_participante').val();
        obj.Notas = $('#txt_notas_participante').val();
        obj.Estatus = $('#cmb_estatus_participante').val();
        obj.Telefono = $('#txt_telefono_participante').val();
        obj.Celular = $('#txt_celular_participante').val();
        obj.Colonia = $('#txt_colonia_participante').val();
        obj.Nacionalidad = $('#txt_nacionalidad_participante').val();
        obj.Direccion = $('#txt_direccion_participante').val();
        obj.tbl_documentos_participante = JSON.stringify($('#tbl_documentos_participante').bootstrapTable('getData'));
        obj.tbl_documentos_eliminados_participante = JSON.stringify($('#tbl_documentos_eliminados_participante').bootstrapTable('getData'));

        var $data = JSON.stringify({ 'jsonObject': JSON.stringify(obj) });

        $.ajax({
            type: 'POST',
            url: 'controllers/ParticipantesController.asmx/Modificar',
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
                        _ConsultarFiltros();
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

function obtenerClave() {
    var arr = ($('#txt_nombre_participante').val()).split(" ", 3);
    var dateString = new Date();

    var FormatoDate = dateString.getUTCFullYear().toString() +
                        dateString.getUTCMonth().toString() +
                        dateString.getUTCDate().toString() +
                        dateString.getUTCMinutes().toString() +
                        dateString.getUTCSeconds().toString() +
                        dateString.getUTCMilliseconds().toString();

    //var dateTime = FormatoDate.split(" ");
    //var dateOnly = dateTime[0];
    //var dates = dateOnly.split("/");
    //var temp = dates[1] + "/" + dates[0] + "/" + dates[2];
    var clave = "";

    for (var i = 0; i < arr.length; i++) {
        clave += arr[i].charAt(0);
    }
    //clave += dates[2].toString().substr(-2, 2) + dates[1].toString() + dates[0].toString();
    clave += FormatoDate;

    return clave;
}