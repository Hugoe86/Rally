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
    _launchComponent('vistas/Eventos/Principal.html', 'Principal');
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
        crear_tabla_reporte(0, null);
        _set_location_toolbar('toolbar');
        _eventos_principal();


        _load_cmb_eventos('cmb_evento_filtro');
        _load_cmb_categoria('cmb_categoria_filtro');
        _load_cmb_categoria('cmb_categoria_competencia_filtro');


        consultar_eventos_iniciados();

    } catch (e) {
        _mostrar_mensaje('Error Técnico', e);
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

            var $table_pagination = $('#tbl_reporte_evento');
            $table_pagination.bootstrapTable('togglePagination');

            $table_pagination.tableExport({
                type: 'pdf',
                worksheetName: 'Evento',
                fileName: 'Evento',
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

            $table_pagination.bootstrapTable('tbl_reporte_evento');
        });
        //  <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        //  <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        $('#btn_exportar_excel').on('click', function (e) {
            e.preventDefault();

            var $table_pagination = $('#tbl_reporte_evento');
            $table_pagination.bootstrapTable('togglePagination');
            $table_pagination.tableExport({
                type: 'excel',
                worksheetName: 'Evento',
                fileName: 'Evento',
            });
            $table_pagination.bootstrapTable('tbl_reporte_evento');

        });
        //  <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        //  <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        $('#btn_busqueda').on('click', function (e) {
            e.preventDefault();
            consultar_filtros();
        });
        //  <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        //  <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
    } catch (e) {
        _mostrar_mensaje('Error Técnico', e);
    }
}


//  -----------------------------------------------------
//  -----------------------------------------------------

function crear_tabla_reporte(posicion, arr_) {

    try {
        $('#tbl_reporte_evento').bootstrapTable('destroy');

        $('#tbl_reporte_evento').bootstrapTable({
            cache: false,
            striped: true,
            pagination: true,
            data: [],
            pageSize: 100,
            pageList: [10, 25, 50, 100, 200],
            smartDysplay: false,
            search: false,
            showColumns: false,
            showRefresh: false,
            minimumCountColumns: 2,
            columns: [
               generateColumns(posicion, arr_)
            ]
        });


    } catch (e) {
        _mostrar_mensaje('Informe Técnico' + '[crear_tabla_vehiculo]', e.message);
    }
}

//  -----------------------------------------------------
//  -----------------------------------------------------

function generateColumns(posicion, arr_) {
    var i;
    var columnas = [];
    var bol_ = true;

    columnas = [
        { field: 'Automovil_No', title: 'Automóvil No.', align: 'center', valign: 'top', visible: true, sortable: true },
        { field: 'NS', title: 'NS', align: 'center', valign: 'top', visible: true, sortable: true },
        { field: 'Año', title: 'Año', align: 'center', valign: 'top', visible: true, sortable: true },
        { field: 'Marca', title: 'Marca', align: 'center', valign: 'top', visible: true, sortable: true },
        { field: 'Modelo', title: 'Modelo', align: 'center', valign: 'top', visible: true, sortable: true },
        { field: 'Categoria', title: 'Categoria', align: 'center', valign: 'top', visible: true, sortable: true },

        { field: 'Piloto', title: 'Piloto', align: 'center', valign: 'top', visible: true, sortable: true },
        { field: 'Piloto_Nacionalidad', title: 'Nacionalidad', align: 'center', valign: 'top', visible: true, sortable: true },
        { field: 'Copiloto', title: 'Copiloto', align: 'center', valign: 'top', visible: true, sortable: true },
        { field: 'Copiloto_Nacionalidad', title: 'Nacionalidad', align: 'center', valign: 'top', visible: true, sortable: true }
    ];


    if (arr_ != null) {
        for (i = 0; i < arr_.length; i++) {
            columnas.push({ field: 'x' + arr_[i].toString(), title: '[' + arr_[i].toString() + ']', align: 'center', valign: 'top', visible: true, sortable: true });
        }
    }

    columnas.push({
        field: 'Total', title: 'Total', align: 'center', valign: 'top', visible: true, sortable: true,
    });


    return columnas;

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
                url: '../../Paginas/Operaciones/controllers/EventosController.asmx/Consultar_Eventos_Combo',
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


            $("#cmb_categoria_filtro").empty().trigger("change");
            $('#cmb_categoria_filtro').prop('disabled', false);


            $("#cmb_categoria_competencia_filtro").empty().trigger("change");
            $('#cmb_categoria_competencia_filtro').prop('disabled', false);


        });
        //  <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        //  <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        $('#' + cmb).on("select2:unselect", function (evt) {


            $("#cmb_categoria_filtro").empty().trigger("change");
            $('#cmb_categoria_filtro').attr('disabled', 'disabled');


            $("#cmb_categoria_competencia_filtro").empty().trigger("change");
            $('#cmb_categoria_competencia_filtro').attr('disabled', 'disabled');
        });
        //  <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        //  <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
    } catch (e) {
        _mostrar_mensaje('Informe técnico' + '[_load_cmb_eventos]', e);
    }
}


//  -----------------------------------------------------
//  -----------------------------------------------------


function _load_cmb_categoria(cmb) {
    try {

        $('#' + cmb).select2({
            language: "es",
            theme: "classic",
            placeholder: 'SELECCIONE',
            allowClear: true,
            ajax: {
                url: '../Operaciones/controllers/CategoriasController.asmx/Consultar_Categorias_Combo',
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

            //_Consultar_Llenar_Datos_Vehiculo(_dato_combo.id);

        });

        $('#' + cmb).on("select2:unselect", function (evt) {
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
                       ' <i class="fa fa-circle" style="color:#' + row.detalle_2 + ';font-size: 15px;">' +
                       '</i>' +
                       '&nbsp;' + row.text +
                    '</span>' +
                '</div>' +

            '</div>';

        return $(_salida);
    }

}

//  -----------------------------------------------------
//  -----------------------------------------------------

function _templateSelection(row) {
    var _salida;

    var _salida = '<span style="text-transform:uppercase;">' +
                      ' <i class="fa fa-circle" style="color:#' + row.detalle_2 + ';font-size: 15px;">' +
                      '</i>' +
                      '&nbsp;' + row.text +
                   '</span>';

    return $(_salida);
}


//  -----------------------------------------------------
//  -----------------------------------------------------


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
            filtros.Categoria_Id = ($('#cmb_categoria_filtro').val() === null ? 0 : parseInt($('#cmb_categoria_filtro').val()));
            filtros.Categoria_Participante_Id = ($('#cmb_categoria_competencia_filtro').val() === null ? 0 : parseInt($('#cmb_categoria_competencia_filtro').val()));


            var $data = JSON.stringify({ 'jsonObject': JSON.stringify(filtros) });

            //  se crea la tabla con los valores dinamicos de los puntos de control
            $.ajax({
                type: 'POST',
                url: 'controllers/RptEventosController.asmx/Consultar_Numero_Jornadas',
                data: $data,
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                async: false,
                cache: false,
                success: function (datos2) {
                    if (datos2 !== null) {
                        datos2 = JSON.parse(datos2.d);

                        var columnas_mostrar = 0;

                        var arr_ = [];

                        for (var cont_for = 0; cont_for < datos2.length ; cont_for++) {
                            arr_[cont_for] = datos2[cont_for].Clave;
                        }

                        columnas_mostrar = datos2.length;
                        crear_tabla_reporte(columnas_mostrar, arr_);
                    }
                }
            });


            $.ajax({
                type: 'POST',
                url: 'controllers/RptEventosController.asmx/Consultar_Reporte',
                data: $data,
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                async: false,
                cache: false,
                success: function (datos) {
                    if (datos !== null) {
                        datos = JSON.parse(datos.d);
                        $('#tbl_reporte_evento').bootstrapTable('load', datos);


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

    

        //  ---------------------------------------------------------------------------------------
        //  ---------------------------------------------------------------------------------------
        if (_output.Mensaje != "") {
            _output.Mensaje = "Favor de proporcionar lo siguiente: <br />" + _output.Mensaje;
        }

        //  ---------------------------------------------------------------------------------------
        //  ---------------------------------------------------------------------------------------

    } catch (e) {
        _mostrar_mensaje('Informe técnico' + '[_validar_datos_filtro]', e);
    } finally {
        return _output;
    }
}