var _index = null;
var row_partida = null;


//  -----------------------------------------------------
//  -----------------------------------------------------
$(document).on('ready', function () {
    _load_vistas();
});



//  -----------------------------------------------------
//  -----------------------------------------------------
function _load_vistas() {
    _launchComponent('vistas/Actualizacion_Tiempos/Principal.html', 'Principal');
}


//  -----------------------------------------------------
//  -----------------------------------------------------
function _launchComponent(component, id) {

    $('#' + id).load(component, function () {

        switch (id) {
            case 'Principal':
                _inicializar_vista_principal();
                break;
        }
    });
}

//  -----------------------------------------------------
//  -----------------------------------------------------

//  *******************************************************************************************************************************
//  *******************************************************************************************************************************
////*********************Inicializar********************///
//  *******************************************************************************************************************************
function _inicializar_vista_principal() {
    try {
        crear_tabla_tiempos();
        _set_location_toolbar('toolbar');
        _eventos_principal();

        _load_cmb_eventos('cmb_evento_filtro');
        _load_cmb_jornada('cmb_jornada_filtro');
        _load_cmb_vehiculo('cmb_vehiculo_filtro');

        _load_cmb_participante_piloto_vehiculo('cmb_piloto_id');
        _load_cmb_participante_piloto_vehiculo('cmb_copiloto_id');

        _load_cmb_vehiculo_Informacion();
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
                        $('#cmb_evento_filtro').select2("trigger", "select", {
                            data: { id: datos[0].Evento_Id, text: datos[0].Nombre }
                        });

                        _load_cmb_categoria_vehiculo('cmb_categoria_id');
                        _load_cmb_categoria_vehiculo('cmb_categoria_competencia_id');
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
function _set_location_toolbar(toolbar) {
    $('#' + toolbar).parent().removeClass("pull-left");
    $('#' + toolbar).parent().addClass("pull-right");

}
//  -----------------------------------------------------
//  -----------------------------------------------------

function _load_cmb_categoria_vehiculo(cmb) {
    try {
        var obj = new Object();
        obj.Evento_Id = parseInt($('#cmb_evento_filtro').val());
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
                        evento_id: $('#cmb_evento_filtro').val(),
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

function _load_cmb_participante_piloto_vehiculo(cmb) {
    try {
        var obj = new Object();
        obj.Evento_Id = parseInt($('#cmb_evento_filtro').val());
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
                        evento_id: $('#cmb_evento_filtro').val(),
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

//  -----------------------------------------------------
//  -----------------------------------------------------
function _eventos_principal() {
    try {
        $('#btn_inicio').on('click', function (e) {
            e.preventDefault();
            window.location.href = '../Paginas_Generales/Frm_Apl_Principal.aspx';
        });

      
        $('#btn_busqueda').on('click', function (e) {
            e.preventDefault();
            consultar_filtros();
        });

    } catch (e) {
        _mostrar_mensaje('Error Técnico', e);
    }
}


//  -----------------------------------------------------
//  -----------------------------------------------------

function crear_tabla_tiempos() {

    try {
        $('#tbl_tiempos').bootstrapTable('destroy');

        $('#tbl_tiempos').bootstrapTable({
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

                { field: 'Registro_Id', title: 'Registro_Id', align: 'center', valign: 'top', visible: false, sortable: true },
                //{ field: 'Evento_Id', title: 'Evento_Id', align: 'center', valign: 'top', visible: true, sortable: true },
                //{ field: 'Jornada_Id', title: 'Jornada_Id', align: 'left', valign: 'top', sortable: true },
                //{ field: 'Punto_Control_Id', title: 'Punto_Control_Id', align: 'left', valign: 'top', sortable: true },
                //{ field: 'Vehiculo_Participante_Id', title: 'Vehiculo_Participante_Id', align: 'left', valign: 'top', sortable: true },


                { field: 'Clave_Punto_Control', title: 'Clave punto de control', align: 'left', valign: 'top', sortable: true },
                { field: 'Punto_Control', title: 'Punto de control', align: 'left', valign: 'top', sortable: true },
                
                {
                    field: 'Str_Tiempo_Real', title: 'Hora', align: 'left', valign: 'top', sortable: true, visible: true,

                    formatter: function (value, row, index) {
                        if (row.Str_Tiempo_Real != null)
                            return new Date('1/1/2018 ' + value).toString('HH:mm:ss');
                    },

                    editable: {
                        title: 'Ingresar hora',
                        type: 'text',
                        inputclass: 'form-control',
                        clear: true,
                        validate: function (value, row) {
                            if ($.trim(value) != '') {
                            }
                            else {
                                return 'Se esperaba un valor';
                            }
                        }
                    }
                },

                { field: 'Usuario_Modifica_Registro', title: 'Usuario modifico', align: 'left', valign: 'top', sortable: true },
                { field: 'Motivo_Cambio', title: 'Motivo del cambio', align: 'left', valign: 'top', sortable: true },

                {
                    field: 'Registro_Id',
                    title: '',
                    align: 'right',
                    valign: 'top',
                    halign: 'center',

                    formatter: function (value, row) {

                        var opciones = '<div style=" text-align: center;">';

                        opciones += '<div style="display:block"><a class="remove ml10 text-purple" id="' + row.Documento_Id + '" href="javascript:void(0)" data-orden=\'' + JSON.stringify(row) + '\' onclick="btn_actualizar_click(this);" title="Actualizar"><i class="glyphicon glyphicon-refresh"></i>&nbsp;<span style="font-size:11px !important;">Actualizar</span></a></div>';


                        opciones += '</div>';

                        return opciones;
                    }
                },

            ]
        });

        $('#tbl_tiempos').on('editable-save.bs.table', function (e, field, row, $el) {

            var _detalles = $('#tbl_tiempos').bootstrapTable('getData');

            $.each(_detalles, function (index, field) {

                if (field.Registro_Id === row.Registro_Id) {

                    //Actualizar los registro seleccionados para inhabitarlos 
                    $('#tbl_tiempos').bootstrapTable('updateRow', {
                        index: index,
                        row: {
                            Str_Tiempo_Real: row.Str_Tiempo_Real,

                        }
                    });
                }
            });
        });
           
    } catch (e) {
        _mostrar_mensaje('Informe Técnico' + '[crear_tabla_vehiculo]', e.message);
    }
}

//  -----------------------------------------------------
//  -----------------------------------------------------

function btn_actualizar_click(tab) {

    try{
        var row = $(tab).data('orden');


        bootbox.prompt({
            title: 'Motivo por el cual cambiara el tiempo',
            inputType: 'textarea',
            callback: function (result) {
                if (result) {

                    //  documentos
                    var filtros = null;
                    filtros = new Object();

                    filtros.Registro_Id = row.Registro_Id;
                    filtros.Tiempo_Real = row.Str_Tiempo_Real;
                    filtros.Motivo_Cambio = result;


                    var $data = JSON.stringify({ 'jsonObject': JSON.stringify(filtros) });

                    $.ajax({
                        type: 'POST',
                        url: 'controllers/ActualizacionTiemposController.asmx/Actalizar_Hora_Registro',
                        data: $data,
                        dataType: "json",
                        contentType: "application/json; charset=utf-8",
                        async: false,
                        cache: false,
                        success: function (datos) {
                            if (datos !== null) {
                                datos = JSON.parse(datos.d);

                                consultar_filtros();
                            }
                            else {

                            }
                        }
                    });

                }

            }
        });



    }
    catch(e){
        _mostrar_mensaje('Informe técnico' + 'Error_ [btn_actualizar_click]', e);
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


            $('#cmb_jornada_filtro').prop('disabled', false);
            $('#cmb_vehiculo_filtro').prop('disabled', false);

            _load_cmb_categoria_vehiculo('cmb_categoria_id');
            _load_cmb_categoria_vehiculo('cmb_categoria_competencia_id');

        });
        //  <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        //  <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        $('#' + cmb).on("select2:unselect", function (evt) {


            $("#cmb_jornada_filtro").empty().trigger("change");
            $("#cmb_vehiculo_filtro").empty().trigger("change");

            $('#cmb_jornada_filtro').attr('disabled', 'disabled');
            $('#cmb_vehiculo_filtro').attr('disabled', 'disabled');

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

        });

        $('#' + cmb).on("select2:unselect", function (evt) {
        });

    } catch (e) {
        _mostrar_mensaje('Informe técnico' + '[_load_cmb_vehiculo]', e);
    }
}

//  -----------------------------------------------------
//  -----------------------------------------------------

//  -----------------------------------------------------
//  -----------------------------------------------------
function _load_cmb_vehiculo_Informacion() {
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

            _id_combo = $('#cmb_vehiculo_filtro :selected').val();


            _Consultar_Llenar_Datos_Vehiculo(_dato_combo.id, _id_combo);

        });

        $('#cmb_vehiculo_id').on("select2:unselect", function (evt) {
        });

    } catch (e) {
        _mostrar_mensaje('Informe técnico' + '[_load_cmb_vehiculo]', e);
    }
}


function _load_cmb_vehiculo(cmb) {
    try {

        $('#' + cmb).select2({
            language: "es",
            theme: "classic",
            placeholder: 'SELECCIONE',
            allowClear: true,
            ajax: {
                url: '../Operaciones/controllers/ActualizacionTiemposController.asmx/Consultar_participantes_Vehiculo_Combo',
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
                        evento_id: $('#cmb_evento_filtro :selected').val(),
                    };
                },
                processResults: function (data, page) {
                    return {
                        results: data
                    };
                },
              
            } , 
            templateResult: _formato,
            templateSelection: _templateSelection,
            escapeMarkup: function (m) { return m; },
        });

        $('#' + cmb).on("select2:select", function (evt) {
            var _dato_combo = evt.params.data;
            var _id_combo = evt.params.data.detalle_1;

            $('#cmb_vehiculo_id').select2("trigger", "select", {
                data: { id: _id_combo, text: _dato_combo.detalle_3 }
            });

            $('#txt_numero_participante').val(_dato_combo.detalle_7);

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
function _Consultar_Llenar_Datos_Vehiculo(id, vehiculo_participante_id) {
    var filtros = null;
    try {
        filtros = new Object();


        filtros.Vehiculo_Id = parseInt(id);
        filtros.Vehiculo_Participante_Id = parseInt(vehiculo_participante_id);


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

        $.ajax({
            type: 'POST',
            url: '../Operaciones/controllers/Eventos_VehiculosController.asmx/Consultar_Categorias_Vehiculo_Participante',
            data: $data,
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            async: false,
            cache: false,
            success: function (datos) {
                if (datos !== null) {
                    datos = JSON.parse(datos.d);

                    $('#cmb_categoria_id').select2("trigger", "select", {
                        data: { id: datos[0].Categoria_Id, text: datos[0].text }
                    });


                }
            }
        });

        $.ajax({
            type: 'POST',
            url: '../Operaciones/controllers/Eventos_VehiculosController.asmx/Consultar_Categorias_Competencia_Vehiculo_Participante',
            data: $data,
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            async: false,
            cache: false,
            success: function (datos) {
                if (datos !== null) {
                    datos = JSON.parse(datos.d);

                    $('#cmb_categoria_competencia_id').select2("trigger", "select", {
                        data: { id: datos[0].Categoria_Id, text: datos[0].text }
                    });


                }
            }
        });


        //------------------------------------------------------------------------------------------------------
        //------------------------------------------------------------------------------------------------------
        //  piloto
       
          
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
                            data: { id: datos[0].Participante_Piloto_Id, text: datos[0].text }
                        });

                        //------------------------------------------------------------------------------------------------------
                        //------------------------------------------------------------------------------------------------------


                    }
                    else {
                    }
                }
            });

       



        //------------------------------------------------------------------------------------------------------
        //------------------------------------------------------------------------------------------------------
        //  copiloto
       
          

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
                            data: { id: datos[0].Participante_Copiloto_Id, text: datos[0].text }
                        });

                      
                        //------------------------------------------------------------------------------------------------------
                        //------------------------------------------------------------------------------------------------------


                    }
                    else {
                    }
                }
            });

        
        //------------------------------------------------------------------------------------------------------
        //------------------------------------------------------------------------------------------------------


    } catch (e) {
        _mostrar_mensaje('Error ', e);
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
                       ' <i class="fa fa-circle" style="color:#' + row.detalle_2 + ';font-size: 15px;">' +
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




//  *******************************************************************************************************************************
//  *******************************************************************************************************************************
///********************CONSULTAS**************///
//  *******************************************************************************************************************************
function consultar_filtros() {
    var filtros = null;
    try {


        

        var valida_datos_requerido = _validar_datos_filtro();

        if (valida_datos_requerido.Estatus) {

            filtros = new Object();

            filtros.Evento_Id = ($('#cmb_evento_filtro').val() === null ? 0 : parseInt($('#cmb_evento_filtro').val()));
            filtros.Jornada_Id = ($('#cmb_jornada_filtro').val() === null ? 0 : parseInt($('#cmb_jornada_filtro').val()));
            filtros.Vehiculo_Participante_Id = ($('#cmb_vehiculo_filtro').val() === null ? 0 : parseInt($('#cmb_vehiculo_filtro').val()));


            var $data = JSON.stringify({ 'jsonObject': JSON.stringify(filtros) });

            $.ajax({
                type: 'POST',
                url: 'controllers/ActualizacionTiemposController.asmx/Consultar_Tiempos',
                data: $data,
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                async: false,
                cache: false,
                success: function (datos) {
                    if (datos !== null) {
                        datos = JSON.parse(datos.d);
                        $('#tbl_tiempos').bootstrapTable('load', datos);
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

        if ($('#cmb_vehiculo_filtro').val() == '' || $('#cmb_vehiculo_filtro').val() == undefined) {
            _output.Estatus = false;
            _output.Mensaje += '&nbsp;<i class="fa fa-angle-double-right"></i>&nbsp;El vehiculo.<br />';
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