var _index = null;
var row_partida = null;

$(document).on('ready', function () {
    _load_vistas();
});


//  *******************************************************************************************************************************
//  *******************************************************************************************************************************
////*********************Inicializar********************///
//  *******************************************************************************************************************************
function _load_vistas() {
    try {
        _launchComponent('vistas/Orden_Vehiculos/Principal.html', 'Principal');

    } catch (e) {
        _mostrar_mensaje('Error Técnico', e);
    }
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



//  *******************************************************************************************************************************
//  *******************************************************************************************************************************
////*********************Inicializar********************///
//  *******************************************************************************************************************************
function _inicializar_vista_principal() {
    try {

        crear_tabla();
        _eventos_principal();
        _load_cmb_eventos('cmb_evento_filtro');
        _load_cmb_categoria_vehiculo('cmb_categoria_filtro');
        
        $('#txt_numero_inicio').val(1);
        $('#txt_numero_fin').val(200);


    } catch (e) {
        _mostrar_mensaje('Error Técnico', e);
    }
}


//  -----------------------------------------------------
//  -----------------------------------------------------

function crear_tabla() {

    try {
        $('#tbl_datos_reorden').bootstrapTable('destroy');

        $('#tbl_datos_reorden').bootstrapTable({
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
                { field: 'id', title: 'id', width: 150, align: 'center', valign: 'top', visible: true, sortable: true },
                { field: 'no', title: 'no', width: 150, align: 'center', valign: 'top', visible: true, sortable: true },
            ]
        });

    

    } catch (e) {
        _mostrar_mensaje('Informe Técnico' + '[crear_tabla_vehiculo]', e.message);
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
            //_consultar_folio_inicio_fin_participante();
            _consultar_vehiculos();
        });

        $('#btn_guardar').on('click', function (e) {
            e.preventDefault();
           


            bootbox.confirm({
                title: 'Actualizar datos',
                message: 'Esta seguro de actualizar el orden de los vehiculos?',
                callback: function (result) {
                    if (result) {

                      
                      
                        var filtros = null;
                        filtros = new Object();

                        filtros.tbl_datos_reorden = JSON.stringify($('#tbl_datos_reorden').bootstrapTable('getData'));
                        
                        var $data = JSON.stringify({ 'jsonObject': JSON.stringify(filtros) });

                        $.ajax({
                            type: 'POST',
                            url: 'controllers/Orden_VehiculosController.asmx/Actualizar_Orden',
                            data: $data,
                            dataType: "json",
                            contentType: "application/json; charset=utf-8",
                            async: false,
                            cache: false,
                            success: function ($result) {

                                var Resultado = JSON.parse($result.d);

                                if (Resultado != null && Resultado != undefined && Resultado != '') {
                                    if (Resultado.Estatus == 'success') {

                                        $('#tbl_datos_reorden').bootstrapTable('load', JSON.parse('[]'));
                                        _mostrar_mensaje(Resultado.Titulo, Resultado.Mensaje);

                                    }
                                }
                                else {

                                }
                            }
                        });

                    }

                }
            });

        });

    } catch (e) {
        _mostrar_mensaje('Error Técnico', e);
    }
}


//  -----------------------------------------------------
//  -----------------------------------------------------
function _mostrar_mensaje(Titulo, Mensaje) {
    bootbox.dialog({
        message: Mensaje,
        title: Titulo,
        locale: 'es',
        closeButton: true,
        buttons: [{
            label: 'Cerrar',
            className: 'btn-default',
            callback: function () { }
        }]
    });
}
//  -----------------------------------------------------
//  -----------------------------------------------------
function _consultar_vehiculos(cmb) {
    var html = "";
    var filtros = new Object();

    try {
      
        filtros.Evento_Id = ($('#cmb_evento_filtro').val() === null ? 0 : parseInt($('#cmb_evento_filtro').val()));
        filtros.Categoria_Id = ($('#cmb_categoria_filtro').val() === null ? 0 : parseInt($('#cmb_categoria_filtro').val()));
        
        var $data = JSON.stringify({ 'jsonObject': JSON.stringify(filtros) });

        $.ajax({
            type: 'POST',
            url: 'controllers/Orden_VehiculosController.asmx/Consultar_Vehiculos',
            data: $data,
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            async: false,
            cache: false,
            success: function (datos) {
                if (datos !== null) {
                    html += JSON.parse(datos.d).html;
                }
            }
        });
     

    } catch (e) {
        _mostrar_mensaje('Informe técnico' + '[_consultar_vehiculos]', e);
    }

    $("#contenedor_izquierdo").html(html);
    //_isotope();


    var izquierdo = document.getElementById('izquierdo');
    //*************************************************************************************************************************************************
    //*************************************************************************************************************************************************
    //  Metodo para el reorden de la tabla
    //*************************************************************************************************************************************************
    //*************************************************************************************************************************************************
    new Sortable(izquierdo, {
        animation: 150,
        group: 'vehiculos',
        ghostClass: 'blue-background-class',

        onSort: function (e) {
            
            var contenedor = document.getElementById('izquierdo');

            var contador = 0;
            var contador_fin = 0;

            contador = parseInt($('#txt_numero_inicio').val());
            contador_fin = parseInt($('#txt_numero_fin').val());

            //  busca los controles con la class = 'No_Participante'
            $('.No_Participante').each(function () {

                var control = $(this);

                if (contador <= contador_fin) {

                    $(this).text(contador);
                    contador++;
                }
                else {

                    $(this).text(0);
                }
            });



            $('#tbl_datos_reorden').bootstrapTable('load', JSON.parse('[]'));


            var arreglo = [];// nota: [id_vehiculo_participante][no_participante]
            var contadorx = 0;
            var contadory = 0;
            var indice = 0;

            contadorx = 0;



            $('.id').each(function () {
                var id;
                var no;

                contadory = 0;

                $('.No_Participante').each(function () {
                    if (contadorx == contadory) {
                        no = $(this).text();
                    }
                    contadory++;
                });

                var control_id = $(this);
                id = $(this).text();


                $('#tbl_datos_reorden').bootstrapTable('insertRow', {
                    index: indice,
                    row: {
                        id: parseInt(id),
                        no: parseInt(no),
                    }
                });


                contadorx++;
                indice++;
            });


        },
    });
    //*************************************************************************************************************************************************
    //*************************************************************************************************************************************************
    //*************************************************************************************************************************************************
    //*************************************************************************************************************************************************
}

//  -----------------------------------------------------
//  -----------------------------------------------------
function _consultar_folio_inicio_fin_participante(cmb) {
    var html = "";
    var filtros = new Object();

    try {

        filtros.Evento_Id = ($('#cmb_evento_filtro').val() === null ? 0 : parseInt($('#cmb_evento_filtro').val()));
        filtros.Categoria_Id = ($('#cmb_categoria_filtro').val() === null ? 0 : parseInt($('#cmb_categoria_filtro').val()));

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

                    $('#txt_numero_inicio').val(datos[0].Folio_Inicio);
                    $('#txt_numero_fin').val(datos[0].Folio_Fin);

                }
            }
        });


    } catch (e) {
        _mostrar_mensaje('Informe técnico' + '[_consultar_folio_inicio_fin_participante]', e);
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

            $('#txt_evento_id').val(_dato_combo.id);
            $('#cmb_categoria_filtro').prop('disabled', false);

        });
        //  <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        //  <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        $('#' + cmb).on("select2:unselect", function (evt) {

            $('#txt_evento_id').val('');
            $("#cmb_categoria_filtro").empty().trigger("change");
            $('#cmb_categoria_filtro').attr('disabled', 'disabled');

        });
        //  <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        //  <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
    } catch (e) {
        _mostrar_mensaje('Informe técnico' + '[_load_cmb_eventos]', e);
    }
}

//  -----------------------------------------------------
//  -----------------------------------------------------
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
        _mostrar_mensaje('Informe técnico' + '[_load_cmb_categoria_vehiculo]', e);
    }
}



//  -----------------------------------------------------
//  -----------------------------------------------------

function _isotope() {
    var $grid = $('.grid').isotope({
        itemSelector: '.element-item',
        layoutMode: 'fitRows',
        getSortData: {
            name: '.name',
            weight: function (itemElem) {
                var weight = $(itemElem).find('.weight').text();
                return parseFloat(weight.replace(/[\(\)]/g, ''));
            }
        }
    });

    // filter functions
    var filterFns = {
        // show if number is greater than 50
        numberGreaterThan50: function () {
            var number = $(this).find('.number').text();
            return parseInt(number, 10) > 50;
        },
        // show if name ends with -ium
        ium: function () {
            var name = $(this).find('.name').text();
            return name.match(/ium$/);
        }
    };
}


//  -----------------------------------------------------
//  -----------------------------------------------------
