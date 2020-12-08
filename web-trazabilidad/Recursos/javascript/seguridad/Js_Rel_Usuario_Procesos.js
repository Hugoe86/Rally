$(document).on('ready', function () {
    _inicializar_pagina();
});

function _inicializar_pagina() {
    _load_users();
    _cargar_tabla();
    _eventos();

}

function _cargar_tabla() {

    try {
        $('#tbl_procesos').bootstrapTable('destroy');
        $('#tbl_procesos').bootstrapTable({
            idField: 'Producto_ID',
            method: 'POST',
            async: false,
            cache: false,
            striped: true,
            pagination: false,
            pageSize: 10,
            pageList: [10, 25, 50, 100, 200],
            smartDysplay: false,
            search: false,
            showColumns: false,
            showRefresh: false,
            minimumCountColumns: 2,
            clickToSelect: true,
            editable: true,
            checkboxHeader: false,
            uniqueId: 'Proceso_ID',
            sortName: 'Proceso',
            columns: [
                { field: 'Select', checkbox: true, clickToSelect: true },
                { field: 'Nombre', title: 'Nombre', align: 'left', valign: 'top' },
                { field: 'Proceso', title: 'Proceso', align: 'left', valign: 'top'},
                { field: 'Comentarios', title: 'Comentarios', align: 'left', valign: 'top' }
            ]
        });

    } catch (e) {
        _mostrar_mensaje('Informe Técnico', e);
    }
}

function _search_procesos(Usuario) {
    var filtros = null;
    try {

        filtros = new Object();
        filtros.Usuario_ID = parseInt(Usuario);

        var $data = JSON.stringify({ 'jsonObject': JSON.stringify(filtros) });

        jQuery.ajax({
            type: 'POST',
            url: 'controllers/RelUsuarioProceso_Controller.asmx/Consultar_Procesos',
            data: $data,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: true,
            cache: false,
            success: function (datos) {
                if (datos !== null) {
                    $('#tbl_procesos').bootstrapTable('load', JSON.parse(datos.d));

                }
            }
        });
    } catch (e) {

    }

}

function _load_users() {

    try {
        $('#Cmb_Usuario').select2({
            language: "es",
            theme: "classic",
            placeholder: 'Selecciona el usuario',
            allowClear: true,
            ajax: {
                url: 'controllers/RelUsuarioProceso_Controller.asmx/Consultar_Usuarios',
                cache: "true",
                dataType: 'json',
                type: "POST",
                delay: 250,
                cache: true,
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
                        //sucursal: $('#cmb_sucursal').val(),
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
        });
        $('#Cmb_Usuario').on("select2:select", function (evt) {
            //tipo = evt.params.data.detalle_1;

            _search_procesos(evt.params.data.id)
        });
        $('#Cmb_Usuario').on("select2:unselect", function (evt) {
            $('#tbl_procesos').bootstrapTable('load', []);
        });
    } catch (e) {
        _mostrar_mensaje('Informe Técnico', e);
    }
}

function _formato(row) {
    if (!row.id) {
        return row.text;
    } else if (row.id == row.text) {
        return row.text;
    } else {
        var _salida = '<div class="row">' +
            '<div class="col-md-9">' +
            '<span style="text-transform:uppercase;">' +
            '<i class="fa fa-angle-double-right"></i>&nbsp;' + row.text +
            '<br />' +
            '<b style="font-size: x-small;"><i class="fa fa-envelope-o"></i> &nbsp;' + row.detalle_1 + '</b>' +
            '</span>' +
            '</div>' +
            '</div>';

        return $(_salida);
    }
}

function _templateSelection(row) {
    if (!row.id) { return row.text; }
    else if (row.id == row.text) return row.text;

    var _salida = '<span style="text-transform:uppercase;">' +
        '<i class="fa fa-user-o"></i>&nbsp;' + row.text +
        '&nbsp;<i class = "fa  fa-long-arrow-right "></i> &nbsp; <i class="fa fa-envelope-o"></i>&nbsp;' + row.detalle_1 +
        '</span>';

    return $(_salida);
}

function _altaEliminar() {
    var Obj = new Object();
    try {
        Obj.Usuario_ID = $('#Cmb_Usuario').val();
        Obj.datos = JSON.stringify($('#tbl_procesos').bootstrapTable('getData'));
        var $data = JSON.stringify({ 'jsonObject': JSON.stringify(Obj) });

        $.ajax({
            type: 'POST',
            url: 'controllers/RelUsuarioProceso_Controller.asmx/AltaEliminar',
            data: $data,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            cache: false,
            success: function (result) {
                var Resultado = JSON.parse(result.d);
                if (Resultado != null && Resultado != undefined && Resultado != '') {
                    if (Resultado.Estatus == 'success') {
                        _limpiar_controles();
                        _mostrar_mensaje(Resultado.Titulo, Resultado.Mensaje);
                        isComplete = true;
                    } else if (Resultado.Estatus == 'error') {
                        _mostrar_mensaje(Resultado.Titulo, Resultado.Mensaje);
                    }
                } else {
                    _mostrar_mensaje(Resultado.Titulo, Resultado.Mensaje);
                }
            }
        });
    } catch (e) {

    }

}

function _validar() {
    var _output = new Object();
    _output.Estatus = true;
    _output.Mensaje = '';
    try {
        if (!$('#Cmb_Usuario').parsley().isValid()) {
          
            _output.Estatus = false;
            _output.Mensaje += '&nbsp;<i class="fa fa-angle-double-right"></i>&nbsp;El Usuario es un dato requerido.<br />';
        }
    } catch (e) {

    }

    return _output; 
}

function _eventos() {
    try {
        $('#btn_nuevo').on('click', function (e) {
            e.preventDefault();
            var _val = _validar(); 
            if (_val.Estatus) {
                _altaEliminar();
            } else {
                _mostrar_mensaje('Validacion', _val.Mensaje);
            }
        });

        $('#btn_salir').on('click', function (e) {
            e.preventDefault(); window.location.href = '../Paginas_Generales/Frm_Apl_Principal.aspx';
        });
    } catch (e) {

    }
}

function _limpiar_controles() {
    
    $('#tbl_procesos').bootstrapTable('load', []);
    $('#Cmb_Usuario').empty().trigger('change');
}