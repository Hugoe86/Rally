var _index = null;
var row_partida = null;

var ruta_imagen = "";
ruta_imagen = '../../Recursos/img/No_Disponible.Jpg';

$(document).on('ready', function () {
    _load_vistas();
});



function _load_vistas() {
    _launchComponent('vistas/Vehiculos/Principal.html', 'Principal');
    _launchComponent('vistas/Vehiculos/Operacion.html', 'Operacion');
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
        }
    });
}



//  *******************************************************************************************************************************
//  *******************************************************************************************************************************
////*********************Inicializar********************///
//  *******************************************************************************************************************************
function _inicializar_vista_principal() {
    try {
        crear_tabla_vehiculo();
        _set_location_toolbar('toolbar');
        _load_cmb_estatus('cmb_estatus_filtro');
        _eventos_principal();
        _mostrar_vista('Principal');


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
        _load_cmb_estatus('cmb_estatus');


        _eventos_procesos();
        _inicializar_fechas_operacion();
        _keyDownInt('txt_año');


    } catch (e) {
        _mostrar_mensaje('Error Técnico', e);
    }
}
function _limpiar_todos_controles_procesos() {

    try {

        $('input[type=text]').each(function () { $(this).val(''); });
        $('input[type=hidden]').each(function () { $(this).val(''); });

        $('#img_documento').attr('src', ruta_imagen);

        $('#tbl_documentos').bootstrapTable('load', []);
        $('#tbl_documentos_eliminados').bootstrapTable('load', []);

    } catch (e) {
        _mostrar_mensaje('Error Técnico', 'limpiar controles. ' + e);
    }
}


function _habilitar_controles(opc) {
    var Estatus = true;
    try {

        Estatus = false;
        var _boton_ = $('#btn_guardar');
        _boton_.show();

        switch (opc) {
            case 'Nuevo':
                $('#btn_guardar').attr('title', 'Guardar');


                break;


            case 'Modificar':
                $('#btn_guardar').attr('title', 'Actualizar');

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
        $('#btn_guardar').on('click', function (e) {
            e.preventDefault();


            var title = $('#btn_guardar').attr('title');
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
        $('#btn_cancelar').on('click', function (e) {
            e.preventDefault();
            _mostrar_vista('Principal');
            _limpiar_todos_controles_procesos();
        });
        //  ---------------------------------------------------------------------------------
        //  ---------------------------------------------------------------------------------
        $('#btn_agregar_documento').on('click', function (e) {
            e.preventDefault();

            var vehiculo_id = "";

            var valida_datos_requerido = _validar_agregar_documento();

            if (valida_datos_requerido.Estatus) {

                var files = $('#txt_ruta_imagen').get(0).files;
                var nombre_Archivo = files[0].name;

                var resultado = subir_archivo("#txt_ruta_imagen", "rally/Vehiculos", "Temporales");


                if (resultado === "error") {
                    _mostrar_mensaje('Informe Tecnico', "No se pudo cargar el archivo del certificado");
                }
                else {



                    if ($('#txt_vehiculo_id').val() == '' || $('#txt_vehiculo_id').val() == undefined) {
                        vehiculo_id = 0;
                    }
                    else {
                        vehiculo_id = parseInt($('#txt_vehiculo_id').val());
                    }

                    $('#tbl_documentos').bootstrapTable('insertRow', {
                        index: 0,
                        row: {
                            Documento_Id: 0,
                            Vehiculo_Id: vehiculo_id,
                            Nombre_Documento: $('#txt_nombre_documento').val(),
                            Nombre: nombre_Archivo,
                            Ruta: 'Vehiculos/Temporales/' + nombre_Archivo,
                            Estatus: 'ACTIVO',
                        }
                    });
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
    $('#dtp_txt_vigencia_inicial').datetimepicker({
        defaultDate: new Date(),
        viewMode: 'days',
        locale: 'es',
        format: "DD/MM/YYYY"
    });
    $("#dtp_txt_vigencia_inicial").datetimepicker("useCurrent", true);

    $('#dtp_txt_vigencia_final').datetimepicker({
        defaultDate: new Date(),
        viewMode: 'days',
        locale: 'es',
        format: "DD/MM/YYYY"
    });
    $("#dtp_txt_vigencia_final").datetimepicker("useCurrent", true);


}
function crear_tabla_vehiculo() {

    try {
        $('#tbl_vehiculos').bootstrapTable('destroy');

        $('#tbl_vehiculos').bootstrapTable({
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

                { field: 'Marca', title: 'Marca', align: 'center', valign: 'top', visible: true, sortable: true },
                { field: 'Modelo', title: 'Modelo', align: 'center', valign: 'top', visible: true, sortable: true },
                { field: 'Año', title: 'Año', align: 'center', valign: 'top', sortable: true },

                 {
                     field: 'Color_Fondo_Hex_Rgb', title: '', align: 'center', valign: 'top', sortable: true, visible: true,
                     formatter: function (value, row, index) {


                         var opciones = "";
                         opciones += ' <div class="row" style="padding-top:2px;">';

                         opciones += '   <div class="col-md-12">';
                         opciones += '       <div>';
                         opciones += '           <i class="fa fa-mobile" style="color:#' + value + ';font-size: 25px;"></i>';
                         opciones += "       </div>"
                         opciones += "   </div>"

                        
                         opciones += "</div>"
                         return opciones;
                     }
                 },
                  {
                      field: 'Color_Hex_Rgb', title: '', align: 'center', valign: 'top', sortable: true, visible: true,
                      formatter: function (value, row, index) {


                          var opciones = "";
                          opciones += ' <div class="row" style="padding-top:2px;">';

                          opciones += '   <div class="col-md-12">';
                          opciones += '       <div>';
                          opciones += '           <i class="fa fa-car" style="color:#' + value + ';font-size: 15px;"></i>';
                          opciones += "       </div>"
                          opciones += "   </div>"


                          opciones += "</div>"
                          return opciones;
                      }
                  },
                {
                    field: 'Color_Hex_Rgb', title: '', align: 'center', valign: 'top', sortable: true, visible: true,
                    formatter: function (value, row, index) {


                        var opciones = "";
                      
                        opciones += '<div style="background-color:#' + row.Color_Fondo_Hex_Rgb  + ';">';
                        opciones += '   <i class="fa fa-circle" style="color:#' + value + ';font-size: 15px;"></i>';
                        opciones += "</div>"
                      
                        return opciones;
                    }
                },

                { field: 'Placas', title: 'Placa', align: 'center', valign: 'top', sortable: true, visible: true },
                 //{ field: 'Estatus', title: 'Estatus', align: 'left', valign: 'top' },


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


                { field: 'Vehiculo_Id', title: 'Vehiculo_Id', align: 'center', valign: 'top', sortable: true, visible: false },
                { field: 'NS', title: 'NS', align: 'left', valign: 'top', visible: false },

                { field: 'Notas', title: 'Notas', align: 'center', valign: 'top', sortable: true, visible: false },
                { field: 'Compañia', title: 'Compañia', align: 'center', valign: 'top', sortable: true, visible: false },
                { field: 'Numero_Poliza', title: 'No. Poliza', align: 'center', valign: 'top', sortable: true, visible: false },

                {
                    field: 'Vigencia_Inicial', title: 'Vigencia inicial', align: 'left', valign: 'top', sortable: true, visible: false, formatter: function (value, row, index) {
                        if (row.Vigencia_Inicial != null)
                            return new Date(value).toString('dd/MMMM/yyyy');
                    }
                },
                 {
                     field: 'Vigencia_Final', title: 'Vigencia final', align: 'left', valign: 'top', sortable: true, visible: false, formatter: function (value, row, index) {
                         if (row.Vigencia_Final != null)
                             return new Date(value).toString('dd/MMMM/yyyy');
                     }
                 },

                  {
                      field: 'Vehiculo_Id',
                      title: '',
                      align: 'right',
                      valign: 'top',
                      halign: 'center',

                      formatter: function (value, row) {

                          var opciones = '<div style=" text-align: center;">';

                          opciones += '<div style="display:block"><a class="remove ml10 text-purple" id="' + row.Documento_Id + '" href="javascript:void(0)" data-orden=\'' + JSON.stringify(row) + '\' onclick="btn_editar_click(this);" title="Editar"><i class="glyphicon glyphicon-edit"></i>&nbsp;<span style="font-size:11px !important;">Editar</span></a></div>';


                          opciones += '</div>';

                          return opciones;
                      }
                  },

                {
                    field: 'Vehiculo_Id',
                    title: '',
                    align: 'right',
                    valign: 'top',
                    halign: 'center',

                    formatter: function (value, row) {

                        var opciones = '<div style=" text-align: center;">';

                        opciones += '<div style="display:block"><a class="remove ml10 text-red" id="' + row.Documento_Id + '" href="javascript:void(0)" data-orden=\'' + JSON.stringify(row) + '\' onclick="btn_eliminar_click(this);" title="Editar"><i class="glyphicon glyphicon-trash"></i>&nbsp;<span style="font-size:11px !important;">Eliminar</span></a></div>';


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


function crear_tabla_documentos() {

    try {
        $('#tbl_documentos').bootstrapTable('destroy');

        $('#tbl_documentos').bootstrapTable({
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
                { field: 'Documento_Id', title: 'Vehiculo_ID', align: 'center', valign: 'top', visible: false, sortable: true },
                { field: 'Vehiculo_Id', title: 'Año', align: 'left', valign: 'top', visible: false, sortable: true },
                { field: 'Nombre', title: 'Nombre', align: 'center', valign: 'top', visible: false, sortable: true },
                { field: 'Nombre_Documento', title: 'Nombre', align: 'center', valign: 'top', visible: true, sortable: true },
                { field: 'Ruta', title: 'Ruta', align: 'center', valign: 'top', visible: false, sortable: true },
                { field: 'Estatus', title: 'Estatus', align: 'center', valign: 'top', visible: true, sortable: true },



                 {
                     field: 'Documento_Id',
                     title: '',
                     align: 'right',
                     valign: 'top',
                     halign: 'center',

                     formatter: function (value, row) {

                         var opciones = '<div style=" text-align: center;">';

                         opciones += '<div style="display:block"><a class="remove ml10 text-danger" id="' + row.Documento_Id + '" href="javascript:void(0)" data-orden=\'' + JSON.stringify(row) + '\' onclick="btn_eliminar_documento(this);" title="Editar"><i class="glyphicon glyphicon-remove"></i>&nbsp;<span style="font-size:11px !important;">Eliminar</span></a></div>';


                         opciones += '</div>';

                         return opciones;
                     }
                 },

                  {
                      field: 'Documento_Id',
                      title: '',
                      align: 'right',
                      valign: 'top',
                      halign: 'center',

                      formatter: function (value, row) {

                          var opciones = '<div style=" text-align: center;">';

                          opciones += '<div style="display:block"><a class="remove ml10 text-purple" id="' + row.Documento_Id + '" href="javascript:void(0)" data-orden=\'' + JSON.stringify(row) + '\' onclick="btn_ver_documento(this);" title="Editar"><i class="glyphicon glyphicon-eye-open"></i>&nbsp;<span style="font-size:11px !important;">Ver</span></a></div>';


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
        $('#tbl_documentos_eliminados').bootstrapTable('destroy');

        $('#tbl_documentos_eliminados').bootstrapTable({
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
                { field: 'Documento_Id', title: 'Vehiculo_ID', align: 'center', valign: 'top', visible: true, sortable: true },

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



function _eventos_principal() {
    try {
        $('#btn_inicio').on('click', function (e) {
            e.preventDefault();
            window.location.href = '../Paginas_Generales/Frm_Apl_Principal.aspx';
        });

        $('#btn_nuevo').on('click', function (e) {
            e.preventDefault();


            _habilitar_controles('Nuevo');
            _limpiar_todos_controles_procesos();
            _mostrar_vista('Operacion');


        });

        $('#btn_busqueda').on('click', function (e) {
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

function btn_editar_click(tab) {


    _habilitar_controles('Modificar');
    _limpiar_todos_controles_procesos();


    var row = $(tab).data('orden');
    $('#txt_vehiculo_id').val(row.Vehiculo_Id);
    $('#txt_ns').val(row.NS);
    $('#txt_año').val(row.Año);
    $('#txt_marca').val(row.Marca);
    $('#txt_notas').val(row.Notas);

    if (row.Estatus != null)
        $('#cmb_estatus').select2("trigger", "select", {
            data: { id: row.Estatus, text: row.Estatus }
        });

    $('#txt_modelo').val(row.Modelo);
    $('#txt_placas').val(row.Placas);
    //$('#txt_tarjeta_circulacion').val(row.Tarjeta_Circulacion);

    $('#txt_color').val('#' + row.Color_Hex_Rgb);
    $('#txt_color_fondo').val('#' + row.Color_Fondo_Hex_Rgb);

    $('#txt_compañia').val(row.Compañia);
    $('#txt_no_poliza').val(row.Numero_Poliza);

    if (row.Vigencia_Inicial != "01/01/0001 00:00:00") {
        $('#txt_vigencia_inicial').val(new Date(row.Vigencia_Inicial).toString('dd/MM/yyyy'));
    }

    if (row.Vigencia_Final != "01/01/0001 00:00:00") {
        $('#txt_vigencia_final').val(new Date(row.Vigencia_Final).toString('dd/MM/yyyy'));
    }


    //  documentos
    var filtros = null;
    filtros = new Object();

    filtros.Vehiculo_Id = $('#txt_vehiculo_id').val();
    var $data = JSON.stringify({ 'jsonObject': JSON.stringify(filtros) });

    $.ajax({
        type: 'POST',
        url: 'controllers/VehiculosController.asmx/Consultar_Documentos_Vehiculos',
        data: $data,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        async: false,
        cache: false,
        success: function (datos) {
            if (datos !== null) {
                datos = JSON.parse(datos.d);
                $('#tbl_documentos').bootstrapTable('load', datos);

                if (datos.length >= 1) {
                    $("#img_documento").attr("src", '../../' + datos[0].Ruta);
                } else {
                }
            }
            else {
                $('#tbl_documentos').bootstrapTable('load', []);
            }
        }
    });


    _mostrar_vista('Operacion');
}

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

                filtros.Vehiculo_Id = row.Vehiculo_Id;
                var $data = JSON.stringify({ 'jsonObject': JSON.stringify(filtros) });

                $.ajax({
                    type: 'POST',
                    url: 'controllers/VehiculosController.asmx/Cancelacion',
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

function btn_eliminar_documento(tab) {
    var row = $(tab).data('orden');

    if (row.Documento_Id == 0) {
        $('#tbl_documentos').bootstrapTable('remove', {
            field: 'Documento_Id',
            values: [row.Documento_Id],
            field: 'Nombre',
            values: [row.Nombre],
            field: 'Ruta',
            values: [row.Ruta],
        });
    }
    else {
        $('#tbl_documentos').bootstrapTable('remove', {
            field: 'Documento_Id',
            values: [row.Documento_Id],
        });

        $('#tbl_documentos_eliminados').bootstrapTable('insertRow', {
            index: 0,
            row: {
                Documento_Id: row.Documento_Id,
            }
        });
    }

}

function btn_ver_documento(tab) {
    var row = $(tab).data('orden');

    //$('#div_preliminar').css('display', 'block');
    $("#img_documento").attr("src", '../../' + row.Ruta);

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
        filtros = new Object();
        filtros.Vehiculo_Id = 0;
        filtros.NS = $("#txt_ns_busqueda").val();
        filtros.Año = parseInt($('#txt_año_busqueda').val() === "" ? 0 : ($('#txt_año_busqueda').val()));
        filtros.Marca = $("#txt_marca_busqueda").val();
        filtros.Estatus = $('#cmb_estatus_filtro').val() === null ? "" : ($('#cmb_estatus_filtro').val());
        filtros.Modelo = $("#txt_modelo_busqueda").val();

        filtros.Placas = $("#txt_placas_busqueda").val();


        var $data = JSON.stringify({ 'jsonObject': JSON.stringify(filtros) });

        $.ajax({
            type: 'POST',
            url: 'controllers/VehiculosController.asmx/Consultar_Vehiculos_Filtro',
            data: $data,
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            async: false,
            cache: false,
            success: function (datos) {
                if (datos !== null) {
                    datos = JSON.parse(datos.d);
                    $('#tbl_vehiculos').bootstrapTable('load', datos);
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
        //  datos del vehiculo
        //if ($('#txt_ns').val() == '' || $('#txt_ns').val() == undefined) {
        //    _output.Estatus = false;
        //    _output.Mensaje += '&nbsp;<i class="fa fa-angle-double-right"></i>&nbsp;El vehiculo (NS).<br />';
        //}


        if ($('#txt_año').val() == '' || $('#txt_año').val() == undefined) {
            _output.Estatus = false;
            _output.Mensaje += '&nbsp;<i class="fa fa-angle-double-right"></i>&nbsp;El año.<br />';
        }

        if ($('#txt_marca').val() == '' || $('#txt_marca').val() == undefined) {
            _output.Estatus = false;
            _output.Mensaje += '&nbsp;<i class="fa fa-angle-double-right"></i>&nbsp;La marca.<br />';
        }

        if ($('#txt_modelo').val() == '' || $('#txt_modelo').val() == undefined) {
            _output.Estatus = false;
            _output.Mensaje += '&nbsp;<i class="fa fa-angle-double-right"></i>&nbsp;El modelo.<br />';
        }

        //if ($('#txt_placas').val() == '' || $('#txt_placas').val() == undefined) {
        //    _output.Estatus = false;
        //    _output.Mensaje += '&nbsp;<i class="fa fa-angle-double-right"></i>&nbsp;La placa.<br />';
        //}


        if ($('#cmb_estatus').val() == '' || $('#cmb_estatus').val() == undefined) {
            _output.Estatus = false;
            _output.Mensaje += '&nbsp;<i class="fa fa-angle-double-right"></i>&nbsp;El estatus.<br />';
        }

        //if ($('#txt_tarjeta_circulacion').val() == '' || $('#txt_tarjeta_circulacion').val() == undefined) {
        //    _output.Estatus = false;
        //    _output.Mensaje += '&nbsp;<i class="fa fa-angle-double-right"></i>&nbsp;La placa.<br />';
        //}

        if ($('#txt_color').val() == '' || $('#txt_color').val() == undefined) {
            _output.Estatus = false;
            _output.Mensaje += '&nbsp;<i class="fa fa-angle-double-right"></i>&nbsp;El color del automovil.<br />';
        }
        if ($('#txt_color_fondo').val() == '' || $('#txt_color_fondo').val() == undefined) {
            _output.Estatus = false;
            _output.Mensaje += '&nbsp;<i class="fa fa-angle-double-right"></i>&nbsp;El color de fondo.<br />';
        }
        ////  ---------------------------------------------------------------------------------------
        ////  ---------------------------------------------------------------------------------------
        ////  datos del seguro

        //if ($('#txt_compañia').val() == '' || $('#txt_compañia').val() == undefined) {
        //    _output.Estatus = false;
        //    _output.Mensaje += '&nbsp;<i class="fa fa-angle-double-right"></i>&nbsp;La compañia del seguro.<br />';
        //}

        //if ($('#txt_no_poliza').val() == '' || $('#txt_no_poliza').val() == undefined) {
        //    _output.Estatus = false;
        //    _output.Mensaje += '&nbsp;<i class="fa fa-angle-double-right"></i>&nbsp;El numero de poliza.<br />';
        //}

        //if ($('#txt_vigencia_inicial').val() == '' || $('#txt_vigencia_inicial').val() == undefined) {
        //    _output.Estatus = false;
        //    _output.Mensaje += '&nbsp;<i class="fa fa-angle-double-right"></i>&nbsp;La vigencia inicial.<br />';
        //}

        //if ($('#txt_vigencia_final').val() == '' || $('#txt_vigencia_final').val() == undefined) {
        //    _output.Estatus = false;
        //    _output.Mensaje += '&nbsp;<i class="fa fa-angle-double-right"></i>&nbsp;La vigencia final.<br />';
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
        if ($('#txt_ruta_imagen').val() == '' || $('#txt_ruta_imagen').val() == undefined) {
            _output.Estatus = false;
            _output.Mensaje += '&nbsp;<i class="fa fa-angle-double-right"></i>&nbsp;El documento.<br />';
        }

        if ($('#txt_nombre_documento').val() == '' || $('#txt_nombre_documento').val() == undefined) {
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

    try {

        var color;

        color = $("#txt_color").val();
        color = color.replace('#', '');

        var color_fondo;

        color_fondo = $("#txt_color_fondo").val();
        color_fondo = color_fondo.replace('#', '');

        obj.NS = $('#txt_ns').val();
        obj.Año = parseInt($('#txt_año').val());
        obj.Marca = $('#txt_marca').val();
        obj.Estatus = $('#cmb_estatus').val();
        obj.Modelo = $('#txt_modelo').val();
        obj.Placas = $('#txt_placas').val();
        obj.Notas = $('#txt_notas').val();


        obj.Compañia = $('#txt_compañia').val();
        obj.Numero_Poliza = $('#txt_no_poliza').val();


        if ($('#txt_vigencia_inicial').val() !== '' || $('#txt_vigencia_inicial').val() == undefined) {
            obj.Vigencia_Inicial = parseDate($('#txt_vigencia_inicial').val());
        }
        if ($('#txt_vigencia_final').val() !== '' || $('#txt_vigencia_final').val() == undefined) {
            obj.Vigencia_Final = parseDate($('#txt_vigencia_final').val());
        }

        obj.Color_Hex_Rgb = color;
        obj.Color_Fondo_Hex_Rgb = color_fondo;
        obj.tbl_documentos = JSON.stringify($('#tbl_documentos').bootstrapTable('getData'));


        var $data = JSON.stringify({ 'jsonObject': JSON.stringify(obj) });

        $.ajax({
            type: 'POST',
            url: 'controllers/VehiculosController.asmx/Alta',
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

        var color;

        color = $("#txt_color").val();
        color = color.replace('#', '');

        var color_fondo;

        color_fondo = $("#txt_color_fondo").val();
        color_fondo = color_fondo.replace('#', '');

        obj.Vehiculo_Id = parseInt($('#txt_vehiculo_id').val());
        obj.NS = $('#txt_ns').val();
        obj.Año = parseInt($('#txt_año').val());
        obj.Marca = $('#txt_marca').val();
        obj.Estatus = $('#cmb_estatus').val();
        obj.Modelo = $('#txt_modelo').val();
        obj.Placas = $('#txt_placas').val();
        obj.Notas = $('#txt_notas').val();
       

        obj.Compañia = $('#txt_compañia').val();
        obj.Numero_Poliza = $('#txt_no_poliza').val();

        if ($('#txt_vigencia_inicial').val() !== '' || $('#txt_vigencia_inicial').val() == undefined) {
            obj.Vigencia_Inicial = parseDate($('#txt_vigencia_inicial').val());
        }
        if ($('#txt_vigencia_final').val() !== '' || $('#txt_vigencia_final').val() == undefined) {
            obj.Vigencia_Final = parseDate($('#txt_vigencia_final').val());
        }

        obj.Color_Hex_Rgb = color;
        obj.Color_Fondo_Hex_Rgb = color_fondo;
        obj.tbl_documentos = JSON.stringify($('#tbl_documentos').bootstrapTable('getData'));
        obj.tbl_documentos_eliminados = JSON.stringify($('#tbl_documentos_eliminados').bootstrapTable('getData'));


        var $data = JSON.stringify({ 'jsonObject': JSON.stringify(obj) });




        $.ajax({
            type: 'POST',
            url: 'controllers/VehiculosController.asmx/Modificar',
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