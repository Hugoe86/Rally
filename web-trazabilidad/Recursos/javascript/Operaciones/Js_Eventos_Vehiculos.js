
//  -----------------------------------------------------
//  -----------------------------------------------------
function _inicializar_vista_vehiculos() {
    try {
        _eventos_principal_vehiculos();

        //  vehiculo
        crear_tabla_vehiculos();

    } catch (e) {
        _mostrar_mensaje('Error Técnico', e);
    }
}


//  -----------------------------------------------------
//  -----------------------------------------------------
function _inicializar_vista_vehiculos_Proceso() {
    try {

        _keyDownInt('txt_numero_registro');
        _keyDownInt('txt_numero_participante');
        _load_cmb_revisiones('cmb_revision_mecanica');
        _load_cmb_revisiones('cmb_revision_mecadica_piloto');
        _load_cmb_revisiones('cmb_revision_mecadica_copiloto');

        _load_cmb_vehiculo_asignacion();
        _load_cmb_categoria_vehiculo('cmb_categoria_id');
        _load_cmb_categoria_Participante_vehiculo('cmb_categoria_competencia_id');

        _load_cmb_participante_piloto_vehiculo('cmb_piloto_id');
        _load_cmb_participante_piloto_vehiculo('cmb_copiloto_id');

        _eventos_procesos_vehiculos();

    } catch (e) {
        _mostrar_mensaje('Error Técnico', e);
    }
}


//  -----------------------------------------------------
//  -----------------------------------------------------
function _eventos_principal_vehiculos() {

    $('#btn_nuevo_vehiculo_participante').on('click', function (e) {
        e.preventDefault();

        _mostrar_vista_vehiculos('Operacion');
        _limpiar_todos_controles_proceso_vehiculos();

        _habilitar_controles_vehiculo("Nuevo");
    });

    //  <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
    //  <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
    $('#btn_exportar_excel').on('click', function (e) {
        e.preventDefault();

        var $table_pagination = $('#tbl_vehiculo_participante_principal');
        $table_pagination.bootstrapTable('togglePagination');
        $table_pagination.tableExport({
            type: 'excel',
            worksheetName: 'Etapas',
            fileName: 'Participantes',
        });
        $table_pagination.bootstrapTable('tbl_vehiculo_participante_principal');

    });
}




//  ---------------------------------------------------------------------------------
//  ---------------------------------------------------------------------------------
function _eventos_procesos_vehiculos() {
    try {
        //  ---------------------------------------------------------------------------------
        //  ---------------------------------------------------------------------------------
        $('#btn_guardar_vehiculo').click(function (e) {
            e.preventDefault();


            var title = $('#btn_guardar_vehiculo').attr('title');

            var valida_datos_requerido = _validarDatos_Vehiculo_Nuevo();
            if (valida_datos_requerido.Estatus) {

                if (title == "Guardar") {
                    Alta_Vehiculo_Participante()
                }
                else {
                    Modificar_Vehiculo_Participante();
                }
            }
            else {
                _mostrar_mensaje('Información', valida_datos_requerido.Mensaje);
            }

        });
        //  ---------------------------------------------------------------------------------
        //  ---------------------------------------------------------------------------------
        $('#btn_cancelar_vehiculo').on('click', function (e) {
            e.preventDefault();
            _mostrar_vista_vehiculos('Principal');
            _limpiar_todos_controles_proceso_vehiculos();
        });

        //  ---------------------------------------------------------------------------------
        //  ---------------------------------------------------------------------------------

    } catch (e) {
        _mostrar_mensaje('Error Técnico', e);
    }
}



//  -----------------------------------------------------
//  -----------------------------------------------------
function _mostrar_vista_vehiculos(vista_) {

    switch (vista_) {
        case "Principal":
            $('#VehiculosPrincipal').show();
            $('#VehiculosOperacion').hide();

            break;

        case "Operacion":
            $('#VehiculosPrincipal').hide();
            $('#VehiculosOperacion').show();

            break;

       
    }
}

//  -----------------------------------------------------
//  -----------------------------------------------------
function _limpiar_todos_controles_proceso_vehiculos() {

    try {

        $('#frmVehiculos input[type=text]').each(function () { $(this).val(''); });
        $('#frmVehiculos input[type=hidden]').each(function () { $(this).val(''); });



        $('#cmb_categoria_id').empty().trigger('change');
        $('#cmb_categoria_competencia_id').empty().trigger('change');
        $('#cmb_piloto_id').empty().trigger('change');
        $('#cmb_copiloto_id').empty().trigger('change');

        $('#cmb_vehiculo_id').empty().trigger('change');
        
        //$('#cmb_revision_mecanica').text('');
        //$('#cmb_revision_mecadica_piloto').text('');
        //$('#cmb_revision_mecadica_copiloto').text('');

    } catch (e) {
        _mostrar_mensaje('Error Técnico', 'limpiar controles. ' + e);
    }
}

//  -----------------------------------------------------
//  -----------------------------------------------------

function _habilitar_controles_vehiculo(opc) {
    var Estatus = true;
    try {

        Estatus = false;

        switch (opc) {
            case 'Nuevo':
                $('#btn_guardar_vehiculo').attr('title', 'Guardar');
                break;


            case 'Modificar':
                $('#btn_guardar_vehiculo').attr('title', 'Actualizar');
                break;

        }


    } catch (e) {
        _mostrar_mensaje('Error Técnico' + ' [_habilitar_controles] ', e);
    }

}

//  -----------------------------------------------------
//  -----------------------------------------------------


//  -----------------------------------------------------
//  -----------------------------------------------------
function crear_tabla_vehiculos() {

    try {
        $('#tbl_vehiculo_participante_principal').bootstrapTable('destroy');

        $('#tbl_vehiculo_participante_principal').bootstrapTable({
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

                { field: 'Vehiculo_Participante_Id', title: 'Vehiculo_Participante_Id', align: 'center', valign: 'top', visible: false, sortable: true },
                { field: 'Vehiculo_Id', title: 'Vehiculo_Id', align: 'center', valign: 'top', visible: false , sortable: true },
                { field: 'Evento_Id', title: 'Evento_Id', align: 'center', valign: 'top', visible: false, sortable: true },
                { field: 'Categoria_Id', title: 'Categoria_Id', align: 'center', valign: 'top', visible: false, sortable: true },
                { field: 'Categoria_Participante_Id', title: 'Categoria_Participante_Id', align: 'center', valign: 'top', visible: false, sortable: true },
                { field: 'vehiculo_cmb', title: 'vehiculo_cmb', align: 'center', valign: 'top', visible: false, sortable: true },
                { field: 'categoria_id_cmb', title: 'categoria_id_cmb', align: 'center', valign: 'top', visible: false, sortable: true },
                { field: 'categoria_participante_cmb', title: 'categoria_participante_cmb', align: 'center', valign: 'top', visible: false, sortable: true },
                { field: 'Participante_Piloto_Id_Cmb', title: 'Participante_Piloto_Id_Cmb', align: 'center', valign: 'top', visible: false, sortable: true },
                { field: 'Participante_Copiloto_Id_Cmb', title: 'Participante_Copiloto_Id_Cmb', align: 'center', valign: 'top', visible: false, sortable: true },
                { field: 'Revision_Mecanica', title: 'Revision_Mecanica', align: 'center', valign: 'top', visible: false, sortable: true },

             

                { field: 'Comentario', title: 'Comentarios', align: 'center', valign: 'top', visible: false, sortable: true },
                
                { field: 'Numero_Registro', title: 'Numero registro', align: 'center', valign: 'top', visible: false, sortable: true },
                { field: 'Numero_Participante', title: 'Numero', align: 'center', valign: 'top', visible: true, sortable: true },
                {
                    field: 'Estatus', title: 'Estatus', align: 'center', valign: 'top', visible: true, sortable: true,
                    formatter: function (value, row, index) {


                        var opciones = "";
                        opciones += ' <div class="row" style="padding-top:2px;">';

                        opciones += '   <div class="col-md-12">';
                        opciones += '       <div>';

                        if (value == "ACTIVO") {
                            opciones += '           <i class="fa fa-check" style="color:#008000;font-size: 14px;"></i>&nbsp;';
                        }
                        else {
                            opciones += '           <i class="fa fa-window-close" style="color:#FF0000;font-size: 14px;"></i>&nbsp;';
                        }


                        opciones += "       </div>"
                        opciones += "   </div>"
                        opciones += "</div>"
                        return opciones;
                    }
                },
                { field: 'Marca', title: 'Marca', align: 'center', valign: 'top', visible: true, sortable: true },
                { field: 'Modelo', title: 'Modelo', align: 'center', valign: 'top', visible: true, sortable: true },
                { field: 'Año', title: 'Año', align: 'center', valign: 'top', visible: true, sortable: true },

                {
                    field: 'Color_Hex_Rgb', title: '', align: 'center', valign: 'top', sortable: true, visible: true,
                    formatter: function (value, row, index) {


                        var opciones = "";

                        opciones += '<div style="background-color:#' + row.Color_Fondo_Hex_Rgb + ';">';
                        opciones += '   <i class="fa fa-circle" style="color:#' + value + ';font-size: 15px;"></i>';
                        opciones += "</div>"

                        return opciones;
                    }
                },

                { field: 'Placas', title: 'Placas', align: 'center', valign: 'top', visible: true, sortable: true },
                { field: 'Categoria', title: 'Categoria', align: 'center', valign: 'top', visible: true, sortable: true },

                { field: 'Piloto', title: 'Piloto', align: 'center', valign: 'top', visible: true, sortable: true },

               

                {
                   field: 'Vehiculo_Participante_Id',
                    title: 'Editar',
                   align: 'right',
                   valign: 'top',
                   halign: 'center',

                   formatter: function (value, row) {

                       var opciones = '<div style=" text-align: center;">';

                       opciones += '<div style="display:block">';

                       opciones += '<a class="remove ml10 text-purple" id="' + row.Vehiculo_Participante_Id + '" href="javascript:void(0)" data-orden=\'' + JSON.stringify(row) + '\' onclick="btn_editar_vehiculo_click(this);" title="Editar"><i class="glyphicon glyphicon-edit"></i>&nbsp;<span style="font-size:11px !important;"></span></a>';


                       opciones += '</div>';

                       opciones += '</div>';

                       return opciones;
                   }
                },

                {
                    field: 'Vehiculo_Participante_Id',
                    title: 'Eliminar',
                    align: 'right',
                    valign: 'top',
                    halign: 'center',

                    formatter: function (value, row) {

                        var opciones = "";//   variable para formar la estructura del boton

                        //  validamos que el estatus sea de captura
                        if (row.Estatus == "ACTIVO") {


                            opciones = '<div style=" text-align: center;">';

                            opciones += '<div style="display:block">';

                            opciones += '<a class="remove ml10 text-red" id="' + row.Vehiculo_Participante_Id + '" href="javascript:void(0)" data-orden=\'' + JSON.stringify(row) + '\' onclick="btn_eliminar_vehiculo_click(this);" title="Eliminar"><i class="glyphicon glyphicon-trash"></i>&nbsp;<span style="font-size:11px !important;"></span></a>';


                            opciones += '</div>';

                            opciones += '</div>';
                        }

                        return opciones;
                    }
                },


                {
                    field: 'Vehiculo_Participante_Id',
                    title: 'Reactivar',
                    align: 'right',
                    valign: 'top',
                    halign: 'center',

                    formatter: function (value, row) {

                        var opciones = "";//   variable para formar la estructura del boton

                        //  validamos que el estatus sea de captura
                        if (row.Estatus == "INACTIVO") {

                            opciones = '<div style=" text-align: center;">';

                            opciones += '<div style="display:block;">';

                            opciones += '<a class="remove ml10 text-warning" id="' +
                                row.Vehiculo_Participante_Id + '" href="javascript:void(0)" data-orden=\'' +
                                JSON.stringify(row) + '\' onclick="btn_reactivar_vehiculo_click(this);" title="Reactivar"><i class="glyphicon glyphicon-ok-sign"></i>&nbsp;<span style="style="font-size:15px !important;"></span></a>';


                            opciones += '</div>';

                            opciones += '</div>';
                        }



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


function btn_eliminar_vehiculo_click(tab) {
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

                    filtros.Vehiculo_Participante_Id = row.Vehiculo_Participante_Id;
                    var $data = JSON.stringify({ 'jsonObject': JSON.stringify(filtros) });

                    $.ajax({
                        type: 'POST',
                        url: 'controllers/Eventos_VehiculosController.asmx/Cancelacion',
                        data: $data,
                        dataType: "json",
                        contentType: "application/json; charset=utf-8",
                        async: false,
                        cache: false,
                        success: function (datos) {
                            if (datos !== null) {
                                datos = JSON.parse(datos.d);

                                _Consultar_vehiculos();
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



function btn_reactivar_vehiculo_click(tab) {
    var row = $(tab).data('orden');

    if ($('#txt_estatus').val() == "ACTIVO" || $('#txt_estatus').val() == "INACTIVO") {

        bootbox.confirm({
            title: 'REACTIVACION de registro',
            message: 'Esta seguro de REACTIVAR el registro seleccionado?',
            callback: function (result) {
                if (result) {

                    //  documentos
                    var filtros = null;
                    filtros = new Object();

                    filtros.Vehiculo_Participante_Id = row.Vehiculo_Participante_Id;
                    var $data = JSON.stringify({ 'jsonObject': JSON.stringify(filtros) });

                    $.ajax({
                        type: 'POST',
                        url: 'controllers/Eventos_VehiculosController.asmx/Reactivacion',
                        data: $data,
                        dataType: "json",
                        contentType: "application/json; charset=utf-8",
                        async: false,
                        cache: false,
                        success: function (datos) {
                            if (datos !== null) {
                                datos = JSON.parse(datos.d);

                                _Consultar_vehiculos();
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


function btn_editar_vehiculo_click(tab) {

   

    var row = $(tab).data('orden');

    if ($('#txt_estatus').val() == "ACTIVO" || $('#txt_estatus').val() == "INACTIVO") {


        _limpiar_todos_controles_proceso_vehiculos();

        $('#txt_vehiculo_participante_id').val(row.Vehiculo_Participante_Id);


        if (row.Vehiculo_Id != null) {
            $('#cmb_vehiculo_id').select2("trigger", "select", {
                data: { id: row.Vehiculo_Id, text: row.vehiculo_cmb }
            });

            $('#cmb_vehiculo_id').prop('disabled', false);
        }

        if (row.Categoria_Id != null) {
            $('#cmb_categoria_id').select2("trigger", "select", {
                data: { id: row.Categoria_Id, text: row.categoria_id_cmb }
            });
        }

        if (row.Categoria_Participante_Id != null) {
            $('#cmb_categoria_competencia_id').select2("trigger", "select", {
                data: { id: row.Categoria_Participante_Id, text: row.categoria_participante_cmb }
            });
        }



        $('#txt_comentarios_vehiculo').val(row.Comentario);


        //------------------------------------------------------------------------------------------------------
        //------------------------------------------------------------------------------------------------------

        if (row.Revision_Mecanica == true) {
            $('#cmb_revision_mecanica').select2("trigger", "select", {
                data: { id: 'SI', text: 'SI' }
            });
        }
        else {
            $('#cmb_revision_mecanica').select2("trigger", "select", {
                data: { id: 'NO', text: 'NO' }
            });
        }
        //------------------------------------------------------------------------------------------------------
        //------------------------------------------------------------------------------------------------------
        //  piloto
        if (row.Participante_Piloto_Id_Cmb != null) {

            var filtros = null;
            filtros = new Object();

            filtros.Vehiculo_Participante_Id = row.Vehiculo_Participante_Id;
            var $data = JSON.stringify({ 'jsonObject': JSON.stringify(filtros) });

            $.ajax({
                type: 'POST',
                url: 'controllers/Eventos_VehiculosController.asmx/Consultar_Participantes_Piloto_Evento',
                data: $data,
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                async: false,
                cache: false,
                success: function (datos) {
                    if (datos !== null) {
                        datos = JSON.parse(datos.d);

                        $('#cmb_piloto_id').select2("trigger", "select", {
                            data: { id: datos[0].Participante_Piloto_Id, text: row.Participante_Piloto_Id_Cmb }
                        });

                        //------------------------------------------------------------------------------------------------------
                        //------------------------------------------------------------------------------------------------------

                        if (datos[0].Revision_Medica_Piloto == true) {
                            $('#cmb_revision_mecadica_piloto').select2("trigger", "select", {
                                data: { id: 'SI', text: 'SI' }
                            });
                        }
                        else {
                            $('#cmb_revision_mecadica_piloto').select2("trigger", "select", {
                                data: { id: 'NO', text: 'NO' }
                            });
                        }
                        //------------------------------------------------------------------------------------------------------
                        //------------------------------------------------------------------------------------------------------


                        $('#txt_comentarios_piloto').val(datos[0].Comentario_Piloto);
                        //------------------------------------------------------------------------------------------------------
                        //------------------------------------------------------------------------------------------------------


                    }
                    else {
                    }
                }
            });

        }



        //------------------------------------------------------------------------------------------------------
        //------------------------------------------------------------------------------------------------------
        //  copiloto
        if (row.Participante_Copiloto_Id_Cmb != null) {

            var filtros = null;
            filtros = new Object();

            filtros.Vehiculo_Participante_Id = row.Vehiculo_Participante_Id;
            var $data = JSON.stringify({ 'jsonObject': JSON.stringify(filtros) });

            $.ajax({
                type: 'POST',
                url: 'controllers/Eventos_VehiculosController.asmx/Consultar_Participantes_Copiloto_Evento',
                data: $data,
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                async: false,
                cache: false,
                success: function (datos) {
                    if (datos !== null) {
                        datos = JSON.parse(datos.d);

                        $('#cmb_copiloto_id').select2("trigger", "select", {
                            data: { id: datos[0].Participante_Copiloto_Id, text: row.Participante_Copiloto_Id_Cmb }
                        });

                        //------------------------------------------------------------------------------------------------------
                        //------------------------------------------------------------------------------------------------------

                        if (datos[0].Revision_Medica_Piloto == true) {
                            $('#cmb_revision_mecadica_copiloto').select2("trigger", "select", {
                                data: { id: 'SI', text: 'SI' }
                            });
                        }
                        else {
                            $('#cmb_revision_mecadica_copiloto').select2("trigger", "select", {
                                data: { id: 'NO', text: 'NO' }
                            });
                        }
                        //------------------------------------------------------------------------------------------------------
                        //------------------------------------------------------------------------------------------------------


                        $('#txt_comentarios_copiloto').val(datos[0].Comentario_Copiloto);
                        //------------------------------------------------------------------------------------------------------
                        //------------------------------------------------------------------------------------------------------


                    }
                    else {
                    }
                }
            });

        }
        //------------------------------------------------------------------------------------------------------
        //------------------------------------------------------------------------------------------------------


        $('#txt_numero_registro').val(row.Numero_Registro);
        $('#txt_numero_participante').val(row.Numero_Participante);

        _mostrar_vista_vehiculos('Operacion');
        _habilitar_controles_vehiculo("Modificar");


    }
    else {
        _mostrar_mensaje('Informe Técnico' + '', 'No puede ser editado');
    }
}

//  -----------------------------------------------------
//  -----------------------------------------------------

function _Consultar_vehiculos() {
    var filtros = null;
    try {
        filtros = new Object();

        filtros.Evento_Id = parseInt($("#txt_evento_id").val());
        var $data = JSON.stringify({ 'jsonObject': JSON.stringify(filtros) });

        $.ajax({
            type: 'POST',
            url: 'controllers/Eventos_VehiculosController.asmx/Consultar_Vehiculos_Evento',
            data: $data,
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            async: false,
            cache: false,
            success: function (datos) {
                if (datos !== null) {
                    datos = JSON.parse(datos.d);
                    $('#tbl_vehiculo_participante_principal').bootstrapTable('load', datos);
                }
                else {
                    $('#tbl_vehiculo_participante_principal').bootstrapTable('load', []);
                }
            }
        });
    } catch (e) {
        _mostrar_mensaje('Error ', e);
    }
}




///*******************Validaciones*************************///
//  -----------------------------------------------------
//  -----------------------------------------------------
function _validarDatos_Vehiculo_Nuevo() {
    var _output = new Object();

    try {
        _output.Estatus = true;
        _output.Mensaje = '';
        //  ---------------------------------------------------------------------------------------
        //  ---------------------------------------------------------------------------------------
        //  datos del vehiculo

        if ($('#cmb_vehiculo_id').val() == '' || $('#cmb_vehiculo_id').val() == undefined) {
            _output.Estatus = false;
            _output.Mensaje += '&nbsp;<i class="fa fa-angle-double-right"></i>&nbsp;El vehiculo.<br />';
        }

        if ($('#cmb_categoria_id').val() == '' || $('#cmb_categoria_id').val() == undefined) {
            _output.Estatus = false;
            _output.Mensaje += '&nbsp;<i class="fa fa-angle-double-right"></i>&nbspLa categoria del carro.<br />';
        }

        if ($('#cmb_categoria_competencia_id').val() == '' || $('#cmb_categoria_competencia_id').val() == undefined) {
            _output.Estatus = false;
            _output.Mensaje += '&nbsp;<i class="fa fa-angle-double-right"></i>&nbspLa categoria del carro en que competira en el evento.<br />';
        }

        //  ---------------------------------------------------------------------------------------
        //  ---------------------------------------------------------------------------------------
        //  datos del piloto

        if ($('#cmb_piloto_id').val() == '' || $('#cmb_piloto_id').val() == undefined) {
            _output.Estatus = false;
            _output.Mensaje += '&nbsp;<i class="fa fa-angle-double-right"></i>&nbspEl piloto.<br />';
        }

        //  ---------------------------------------------------------------------------------------
        //  ---------------------------------------------------------------------------------------
        //  datos del copiloto

        //if ($('#cmb_copiloto_id').val() == '' || $('#cmb_copiloto_id').val() == undefined) {
        //    _output.Estatus = false;
        //    _output.Mensaje += '&nbsp;<i class="fa fa-angle-double-right"></i>&nbspEl piloto.<br />';
        //}

        //  ---------------------------------------------------------------------------------------
        //  ---------------------------------------------------------------------------------------
        if (_output.Mensaje != "") {
            _output.Mensaje = "Favor de proporcionar lo siguiente: <br />" + _output.Mensaje;
        }

        //  ---------------------------------------------------------------------------------------
        //  ---------------------------------------------------------------------------------------

    } catch (e) {
        _mostrar_mensaje('Informe técnico' + '[_validarDatos_Vehiculo_Nuevo]', e);
    } finally {
        return _output;
    }
}


//  -----------------------------------------------------
//  -----------------------------------------------------
function _load_cmb_vehiculo_asignacion() {
    try {
       
        $('#cmb_vehiculo_id').select2({
            language: "es",
            theme: "classic",
            placeholder: 'SELECCIONE',
            allowClear: true,
            ajax: {
                url: '../Operaciones/controllers/Eventos_VehiculosController.asmx/Consultar_Vehiculo_Combo',
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
            },
            templateResult: _formato,
            templateSelection: _templateSelection,
            escapeMarkup: function (m) { return m; },



        });

        $('#cmb_vehiculo_id').on("select2:select", function (evt) {
            var _dato_combo = evt.params.data;
            var _id_combo = evt.params.data.detalle_1;

            _Consultar_Llenar_Datos_Vehiculo(_dato_combo.id);

        });

        $('#cmb_vehiculo_id').on("select2:unselect", function (evt) {
        });

    } catch (e) {
        _mostrar_mensaje('Informe técnico' + '[_load_cmb_vehiculo]', e);
    }
}

//  -----------------------------------------------------
//  -----------------------------------------------------

function _formato(row) {

    if (!row.detalle_1) {
        return row.text;
    }
    else {
        var _salida = '<div class="row">' +
                '<div class="col-md-12">' +

                    '<span style="text-transform:uppercase;">' +
                        '<i class="fa fa-circle" style="color:#' + row.detalle_2 + ';font-size: 15px;">' +
                        '</i>' +
                        '&nbsp;' + row.text +
                    '</span>' +
                '</div>' +

            '</div>';

        return $(_salida);
    }

}


function _templateSelection(row) {
    var _salida;


    var _salida = '<span style="text-transform:uppercase;">' +
                      ' <i class="fa fa-circle" style="color:#' + row.detalle_2 + ';font-size: 15px;">' +
                      '</i>' +
                      '&nbsp;' + row.text +
                   '</span>';

    return $(_salida);
}


function _load_cmb_categoria_vehiculo(cmb) {
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


function _load_cmb_categoria_Participante_vehiculo(cmb) {
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

            $('#' + cmb).on("select2:select", function (evt) {
                var _dato_combo = evt.params.data;
                var _id_combo = evt.params.data.detalle_1;

                var filtros = null;
                filtros = new Object();
                filtros.Evento_Id = parseInt($("#txt_evento_id").val());
                filtros.Categoria_Id = parseInt(_dato_combo.id);

                var $data = JSON.stringify({ 'jsonObject': JSON.stringify(filtros) });

                if ($('#txt_numero_registro').val() == "") {
                    $.ajax({
                        type: 'POST',
                        url: 'controllers/Eventos_VehiculosController.asmx/Obtener_Clave_Participante',
                        data: $data,
                        dataType: "json",
                        contentType: "application/json; charset=utf-8",
                        async: false,
                        cache: false,
                        success: function (datos) {
                            if (datos !== null) {
                                datos = JSON.parse(datos.d);
                                if (datos.length > 0) {
                                    $("#txt_numero_registro").val(datos[0].Numero_Participante + 1);
                                }
                                else {
                                    $("#txt_numero_registro").val(1);
                                }
                            }
                            else {

                            }
                        }
                    });

               

                }
                else {

                }


                $.ajax({
                    type: 'POST',
                    url: 'controllers/Eventos_VehiculosController.asmx/Obtener_Clave_Participante_Por_Categoria',
                    data: $data,
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    cache: false,
                    success: function (datos) {
                        if (datos !== null) {
                            datos = JSON.parse(datos.d);
                            $("#txt_numero_participante").val(datos[0].Numero_Participante);
                        }
                        else {

                        }
                    }
                });


            });

            $('#' + cmb).on("select2:unselect", function (evt) {

                
                $("#txt_numero_participante").val('');
            });
        

    } catch (e) {
        _mostrar_mensaje('Informe técnico' + '[_load_cmb_vehiculo]', e);
    }
}


function _load_cmb_participante_piloto_vehiculo(cmb) {
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
                url: '../Operaciones/controllers/Eventos_VehiculosController.asmx/Consultar_participantes_Vehiculo_Combo',
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

        $('#' + cmb).on("select2:select", function (evt) {
            var _dato_combo = evt.params.data;
            var _id_combo = evt.params.data.detalle_1;

        });

        $('#' + cmb).on("select2:unselect", function (evt) {
        });

    } catch (e) {
        _mostrar_mensaje('Informe técnico' + '[_load_cmb_vehiculo]', e);
    }
}


//  *******************************************************************************************************************************
//  *******************************************************************************************************************************
///********************CONSULTAS**************///
//  *******************************************************************************************************************************
function _Consultar_Llenar_Datos_Vehiculo(id) {
    var filtros = null;
    try {
        filtros = new Object();

       
        filtros.Vehiculo_Id = parseInt(id);


        var $data = JSON.stringify({ 'jsonObject': JSON.stringify(filtros) });

        $.ajax({
            type: 'POST',
            url: '../Catalogos/controllers/VehiculosController.asmx/Consultar_Vehiculos_Filtro',
            data: $data,
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            async: false,
            cache: false,
            success: function (datos) {
                if (datos !== null) {
                    datos = JSON.parse(datos.d);
                  

                    $('#txt_color').val('#' + datos[0].Color_Hex_Rgb);
                    $('#txt_marca').val(datos[0].Marca);
                    $('#txt_modelo').val(datos[0].Modelo);
                    $('#txt_placas').val(datos[0].Placas);
                    $('#txt_año').val(datos[0].Año);


                }
            }
        });
    } catch (e) {
        _mostrar_mensaje('Error ', e);
    }
}




//  -----------------------------------------------------
//  -----------------------------------------------------
function _load_cmb_revisiones(cmb) {
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
                    id: 'SI',
                    text: 'SI',
                },
                {
                    id: 'NO',
                    text: 'NO',
                },
                 
            ],
        });

    } catch (e) {
        _mostrar_mensaje('Informe técnico' + '[_load_cmb_estatus]', e);
    }
}




///********************BD***************///
function Alta_Vehiculo_Participante() {
    var obj = new Object();

    try {

        //  vehiculo
        obj.Vehiculo_Id = parseInt($('#cmb_vehiculo_id').val());
        obj.Evento_Id = parseInt($('#txt_evento_id').val());
        obj.Categoria_Id = $('#cmb_categoria_id').val();
        obj.Categoria_Participante_Id = $('#cmb_categoria_competencia_id').val();
        obj.Revision_Mecanica = ($('#cmb_revision_mecanica').val() === "" ? false : $('#cmb_revision_mecanica').val() === 'SI' ? true: false);
        obj.Comentario = $('#txt_comentarios_vehiculo').val();
        obj.Numero_Registro = parseInt($('#txt_numero_registro').val());
        obj.Numero_Participante = parseInt($('#txt_numero_participante').val());

        //  piloto
        obj.Participante_Piloto_Id = parseInt($('#cmb_piloto_id').val());
        obj.Revision_Medica_Piloto = ($('#cmb_revision_mecadica_piloto').val() === "" ? false : $('#cmb_revision_mecadica_piloto').val() === "SI" ? true : false);
        obj.Comentario_Piloto = $('#txt_comentarios_piloto').val();


        //  copiloto
        obj.Participante_Copiloto_Id = $('#cmb_copiloto_id').val() === "" ? false : parseInt($('#cmb_copiloto_id').val());
        obj.Revision_Medica_Copiloto = ($('#cmb_revision_mecadica_copiloto').val() === "" ? false : $('#cmb_revision_mecadica_copiloto').val() === "SI" ? true : false);
        obj.Comentario_Copiloto = $('#txt_comentarios_copiloto').val();



        var $data = JSON.stringify({ 'jsonObject': JSON.stringify(obj) });

        $.ajax({
            type: 'POST',
            url: 'controllers/Eventos_VehiculosController.asmx/Alta',
            data: $data,
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            async: false,
            cache: false,
            success: function (datos) {
                if (datos != null) {
                    var result = JSON.parse(datos.d);
                    if (result.Estatus == 'success') {

                        _mostrar_vista_vehiculos('Principal');
                        _limpiar_todos_controles_proceso_vehiculos();
                        _mostrar_mensaje(result.Titulo, result.Mensaje);
                        _Consultar_vehiculos();

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



///********************BD***************///
function Modificar_Vehiculo_Participante() {
    var obj = new Object();

    try {

        //  vehiculo
        obj.Vehiculo_Participante_Id = parseInt($('#txt_vehiculo_participante_id').val());
        obj.Vehiculo_Id = parseInt($('#cmb_vehiculo_id').val());
        obj.Evento_Id = parseInt($('#txt_evento_id').val());
        obj.Categoria_Id = $('#cmb_categoria_id').val();
        obj.Categoria_Participante_Id = $('#cmb_categoria_competencia_id').val();
        obj.Revision_Mecanica = ($('#cmb_revision_mecanica').val() === "" ? false : $('#cmb_revision_mecanica').val() === 'SI' ? true : false);
        obj.Comentario = $('#txt_comentarios_vehiculo').val();
        obj.Numero_Registro = parseInt($('#txt_numero_registro').val());
        obj.Numero_Participante = parseInt($('#txt_numero_participante').val());

        //  piloto
        obj.Participante_Piloto_Id = parseInt($('#cmb_piloto_id').val());
        obj.Revision_Medica_Piloto = ($('#cmb_revision_mecadica_piloto').val() === "" ? false : $('#cmb_revision_mecadica_piloto').val() === "SI" ? true : false);
        obj.Comentario_Piloto = $('#txt_comentarios_piloto').val();


        //  copiloto
        obj.Participante_Copiloto_Id = $('#cmb_copiloto_id').val() === "" ? false : parseInt($('#cmb_copiloto_id').val());
        obj.Revision_Medica_Copiloto = ($('#cmb_revision_mecadica_copiloto').val() === "" ? false : $('#cmb_revision_mecadica_copiloto').val() === "SI" ? true : false);
        obj.Comentario_Copiloto = $('#txt_comentarios_copiloto').val();



        var $data = JSON.stringify({ 'jsonObject': JSON.stringify(obj) });

        $.ajax({
            type: 'POST',
            url: 'controllers/Eventos_VehiculosController.asmx/Modificar',
            data: $data,
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            async: false,
            cache: false,
            success: function (datos) {
                if (datos != null) {
                    var result = JSON.parse(datos.d);
                    if (result.Estatus == 'success') {

                        _mostrar_vista_vehiculos('Principal');
                        _limpiar_todos_controles_proceso_vehiculos();
                        _mostrar_mensaje(result.Titulo, result.Mensaje);
                        _Consultar_vehiculos();

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