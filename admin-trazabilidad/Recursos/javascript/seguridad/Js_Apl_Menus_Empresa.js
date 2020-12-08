$(document).on('ready', function () {
    _inicializar_pagina();
});

function _inicializar_pagina() {
    _consultar_empresas();
    _crear_estructura_tbl_menus();
    _search_menus();
    _eventos();
}

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

function _consultar_empresas() {
    try {
        $('#cmb_empresa').select2({
            language: "es",
            theme: "classic",
            placeholder: 'Selecciona una empresa',
            allowClear: true,
            ajax: {
                url: 'controllers/Menus_Empresa_Controller.asmx/Consultar_Empresas',
                cache: "true",
                dataType: 'json',
                type: "POST",
                delay: 250,
                cache: true,
                params: { contentType: 'application/json; charset=utf-8' },
                quietMillis: 100,
                results: function (data) { return { results: data }; },
                data: function (params) {
                    return {
                        q: params.term,
                        page: params.page
                    };
                },
                processResults: function (data, page) {
                    return {
                        results: data
                    };
                },
            }
        });
        $('#cmb_empresa').on("select2:select", function (evt) {
            $('#tbl_menus').bootstrapTable('uncheckAll');
            $('#tbl_menus').bootstrapTable('showColumn', 'Check_Select');
            _search_menus();
        });
        $('#cmb_empresa').on("select2:unselect", function (evt) {
            $('#tbl_menus').bootstrapTable('hideColumn', 'Check_Select');
            //$('#tbl_menus').bootstrapTable('uncheckAll');
            _search_menus();
        });
    } catch (e) {
        _mostrar_mensaje('Informe Técnico', e);
    }
}

function _consultar_menus_empresa(id) {
    var filtros = null;
    try {

        filtros = new Object();
        filtros.Empresa_ID = parseInt(id);

        var $data = JSON.stringify({ 'jsonObject': JSON.stringify(filtros) });
        $.ajax({
            url: 'controllers/Menus_Empresa_Controller.asmx/Consultar_Menus_Empresas',
            data: $data,
            method: 'POST',
            cache: false,
            async: true,
            contentType: 'application/json; charset=UTF-8',
            dataType: 'json',
            success: function (datos) {
                //_crear_estructura_tbl_menus();
                if (datos !== null) {
                    $('#tbl_menus').bootstrapTable('load', JSON.parse(datos.d));
                    var resultado = JSON.parse(datos.d);
                    if (datos != null) {
                        $.each(resultado, function (index, value) {
                            if (value.Menu_Empresa_ID != null && value.Menu_Empresa_ID != '') {
                                _update_tbl_menus(index, 'A');
                                $('#tbl_menus').bootstrapTable('check', index);
                            } else {
                                _update_tbl_menus(index, '');
                            }
                        });
                    }
                }
            }
        });

    } catch (e) {

    }
}

function _guardar() {
    var menu_empresa = null;
    menu_empresa = new Object();
    var tbl_menu = $('#tbl_menus').bootstrapTable('getData');
    menu_empresa.datos = JSON.stringify(tbl_menu);
    menu_empresa.Empresa_ID = parseInt($('#cmb_empresa').val());
    var $data = JSON.stringify({ 'jsonObject': JSON.stringify(menu_empresa) });

    //alert(JSON.stringify(tbl_menu));

    $.ajax({
        type: 'POST',
        url: 'controllers/Menus_Empresa_Controller.asmx/Guardar',
        data: $data,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        cache: false,
        success: function ($result) {
            var Resultado = JSON.parse($result.d);
            if (Resultado != null && Resultado != undefined && Resultado != '') {
                if (Resultado.Estatus == 'success') {
                    _limpiar_controles();
                    _mostrar_mensaje(Resultado.Titulo, Resultado.Mensaje);
                    _search_menus();
                    $('#tbl_menus').bootstrapTable('hideColumn', 'Check_Select');
                } else if (Resultado.Estatus == 'error') {
                    _mostrar_mensaje(Resultado.Titulo, Resultado.Mensaje);
                }
            } else {
                _mostrar_mensaje(Resultado.Titulo, Resultado.Mensaje);
                _limpiar_controles();
                _search_menus();
            }
        }     
    });
    
}

function _limpiar_controles() {
    $('#cmb_empresa').empty().trigger("change");
}

function _update_tbl_menus(index, info) {

    $('#tbl_menus').bootstrapTable('updateRow', {
        index: index,
        row: {
            info: info
        }
    });
}

function _check_uncheck(row, check) {
    var menu_id = row.Menu_ID;
    var tbl = $('#tbl_menus').bootstrapTable('getData');

    if (check == 'c') {
        if (row.Parent_ID == 0) {
            $.each(tbl, function (index, value) {
                if (value.Parent_ID == row.Menu_ID) {
                    $('#tbl_menus').bootstrapTable('check', index);
                }
            });
        }
    }
    else {
        if (row.Parent_ID == 0) {
            $.each(tbl, function (index, value) {
                if (value.Parent_ID == row.Menu_ID) {
                    $('#tbl_menus').bootstrapTable('uncheck', index);
                }
            });
        }
    }
}

function _checkAll_uncheckAll(row, check) {
    var tbl = $('#tbl_menus').bootstrapTable('getData');

    if (check == 'c') {
        $.each(tbl, function (index, value) {
            if (value.info == '') {
                _update_tbl_menus(index, 'N');
            } else if (value.info == 'E' && value.Menu_Empresa_ID != null && value.Menu_Empresa_ID != '') {
                _update_tbl_menus(index, 'A');
            }
        });
    } else {
        $.each(tbl, function (index, value) {
            if (value.info == 'A') {
                _update_tbl_menus(index, 'E');
            } else if (value.info == 'N') {
                _update_tbl_menus(index, '');
            }
        });
    }
}

///*****************************Eventos**********************

function _eventos() {

    try{
        $('#btn_aplicar').on('click', function (e) {
            e.preventDefault();

            if ($('#cmb_empresa').val() != null && $('#cmb_empresa').val() != '')
                _guardar();
            else
                _mostrar_mensaje('Validación', 'Selecciona una empresa');
        });
        $('#tbl_menus').on('check.bs.table', function (e, row) {
            _check_uncheck(row, 'c');
        });
        $('#tbl_menus').on('uncheck.bs.table', function (e, row) {
            _check_uncheck(row, 'u');
        });
       

    } catch (e) {

    }

}

function _btn_mostrar(renglon) {
    try {
        var row = $(renglon).data('cr');

        var divs = $(".Childs" + row.Menu_ID);
        //var img = document.getElementById('img' + obj);


        divs.each(function (index, elem) {
            var div = $(elem);

            if (div.is(":visible")) {
                div.css('display', "none");
                $('.Parent' + row.Menu_ID + ' td div i').removeClass('fa fa-minus');
                $('.Parent' + row.Menu_ID + ' td div i').addClass('fa fa-plus');

            } else {
                div.css('display', "");
                $('.Parent' + row.Menu_ID + ' td div i').removeClass('fa fa-plus');
                $('.Parent' + row.Menu_ID + ' td div i').addClass('fa fa-minus');
            }
        });

    } catch (e) {
        _mostrar_mensaje('Error Técnico', e);
    }
}

///*****************************GRID*************************
function _crear_estructura_tbl_menus() {
    try {
        $('#tbl_menus').bootstrapTable('destroy');
        $('#tbl_menus').bootstrapTable({
            cache: false,
            height: 500,
            striped: true,
            pagination: false,
            pageSize: 10,
            pageList: [10, 25, 50, 100, 200],
            smartDysplay: true,
            showColumns: false,
            showRefresh: false,
            checkboxHeader: true,
            clickToSelect: false,
            selectItemName: 'RowSelect',
            rowStyle: "rowStyle",
            uniqueId: 'Menu_ID',
            columns: [
                {
                    field: 'plus', width: 5, formatter: function (value, row) {
                        //Declaracion de variables
                        if (row.Parent_ID == 0) {
                            resultado = '<div> ' +
                                ' <a id=btn_mostrar_' + row.Menu_ID + '" class="edit" href="javascript:void(0)" data-cr=\'' + JSON.stringify(row) + '\' onclick="_btn_mostrar(this);" title="">' +
                                    '<i class="fa fa-plus" style="font-size: 14px;">' +
                                '</a>' +
                                '</div>';
                        } else {
                            resultado = '<div>&nbsp;</div>';
                        }
                        //Entregar resultado formateado
                        return resultado;
                    }
                },
                {
                    field: 'chk', width: 5, visible: true, checkbox:true
                },
                { field: 'info', visible: false },
                { field: 'Menu_Empresa_ID', visible: false },
                { field: 'Menu_ID', title: '', width: 0, align: 'center', valign: 'bottom', sortable: true, visible: false },
                { field: 'Menu_Descripcion', title: 'Nombre', align: 'left', valign: 'bottom', sortable: true }
            ]
        });
    } catch (e) {
        _mostrar_mensaje('Informe Técnico', e);
    }
}

function _search_menus() {
    var filtros = null;
    try {

        filtros = new Object();
        filtros.Empresa_ID = ($('#cmb_empresa').val() === null || $('#cmb_empresa').val() === '') ? 0 : parseInt($('#cmb_empresa').val());

        var $data = JSON.stringify({ 'jsonObject': JSON.stringify(filtros) });
        $.ajax({
            url: 'controllers/Menus_Empresa_Controller.asmx/Consultar_Menus',
            data: $data,
            method: 'POST',
            cache: false,
            async: true,
            contentType: 'application/json; charset=UTF-8',
            dataType: 'json',
            success: function (datos) {
                if (datos !== null) {
                    $('#tbl_menus').bootstrapTable('load', JSON.parse(datos.d));
                    var resultado = JSON.parse(datos.d);
                    if ($('#cmb_empresa').val() != null || $('#cmb_empresa').val() != '') {
                        $.each(resultado, function (index, value) {
                            
                            if (value.Menu_Empresa_ID != null && value.Menu_Empresa_ID != '') {
                                if (value.Parent_ID == 0) {
                                    $('#tbl_menus').bootstrapTable('check', index);
                                }
                                else {
                                    if (value.Menu_Empresa_ID == 0) {
                                        $('#tbl_menus').bootstrapTable('uncheck', index);
                                    }
                                }
                            }
                            //else {
                            //    $('#tbl_menus').bootstrapTable('uncheck', index);
                            //}

                        });
                        $.each(resultado, function (index, value) {

                            if (value.Menu_Empresa_ID == 0) {
                                if (value.Parent_ID != 0) {
                                    $('#tbl_menus').bootstrapTable('uncheck', index);
                                }
                            }
                            //else {
                            //    $('#tbl_menus').bootstrapTable('uncheck', index);
                            //}

                        });
                    }
                    _mostrar_ocultar('t', '')
                }
            }
        });

    } catch (e) {

    }
}

function stateFormatter(value, row, index) {
    if (row.Parent_ID == 0) {
        if (row.Menu_Empresa_ID != 0) {
            resultado = '<input id="Parent" class="Parent" name="RowSelect" type="checkbox" checked>';
        } else {
            resultado = '<input id="Parent" class="Parent" name="RowSelect" type="checkbox">';
        }
    } else {
        resultado = '<input id="child" class="child" name="RowSelect" type="checkbox">';
    }
    //Entregar resultado formateado
    return resultado;
}

function _mostrar_ocultar(opcion, parent_id) {

    var resultado = $('#tbl_menus').bootstrapTable('getData');

    $.each(resultado, function (indexs, value) {

        if (opcion != 't') {
            if (value.Parent_ID === parent_id) {
                var a = parseInt(indexs);
                if (opcion == 'h') {
                    $('#tbl_menus').bootstrapTable('hideRow', { index: a });
                } else {
                    $('#tbl_menus').bootstrapTable('showRow', { index: a});
                }
            }
        } else {
            if (value.Parent_ID != '0') {
                $("[class*=Childs]").each(function (index, elem) {
                    var div = $(elem);
                    div.css('display', "none");
                });
            }
        }
    });

}

function rowStyle(row, index) {
    var classes = ['Parent', 'Childs'];
    if (row.Parent_ID == '0') {
        return {
            classes: classes[0] + row.Menu_ID
        };
    } else {
        return {
            classes: classes[1] + row.Parent_ID
        };
    }
    return {};
}