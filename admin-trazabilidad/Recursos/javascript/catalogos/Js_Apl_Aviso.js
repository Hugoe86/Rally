
$(document).ready(function () {
    //_limpiar_controles();
    _inicializador_pagina();
    _eventos();
    _crear_tbl_avisos();
    _search_ordenes_compra();
});

function _inicializador_pagina() {
    $("#cal_vigencia_inicio").kendoCalendar();
    $("#cal_vigencia_termino").kendoCalendar();

    $("#editor").kendoEditor({
        resizable: {
            content: true,
            toolbar: true
        },
        tools: [
        "bold",
        "italic",
        "underline",
        "strikethrough",
        "justifyLeft",
        "justifyCenter",
        "justifyRight",
        "justifyFull",
        "insertUnorderedList",
        "insertOrderedList",
        "indent",
        "outdent",
        "createLink",
        "unlink",
        "insertImage",
        "insertFile",
        "subscript",
        "superscript",
        "createTable",
        "addRowAbove",
        "addRowBelow",
        "addColumnLeft",
        "addColumnRight",
        "deleteRow",
        "deleteColumn",
        "viewHtml",
        "formatting",
        "cleanFormatting",
        "fontName",
        "fontSize",
        "foreColor",
        "backColor",
        "print"
        ]
    });
    function startChange() {
        var startDate = start.value(),
        endDate = end.value();

        if (startDate) {
            startDate = new Date(startDate);
            startDate.setDate(startDate.getDate());
            end.min(startDate);
        } else if (endDate) {
            start.max(new Date(endDate));
        } else {
            endDate = new Date();
            start.max(endDate);
            end.min(endDate);
        }
    }


    function endChange() {
        var endDate = end.value(),
        startDate = start.value();

        if (endDate) {
            endDate = new Date(endDate);
            endDate.setDate(endDate.getDate());
            start.max(endDate);
        } else if (startDate) {
            end.min(new Date(startDate));
        } else {
            endDate = new Date();
            start.max(endDate);
            end.min(endDate);
        }
    }
    var today = kendo.date.today();

    var start = $("#start").kendoDateTimePicker({
        value: today,
        change: startChange,
        parseFormats: ["MM/dd/yyyy"]
    }).data("kendoDateTimePicker");

    var end = $("#end").kendoDateTimePicker({
        value: today,
        change: endChange,
        parseFormats: ["MM/dd/yyyy"]
    }).data("kendoDateTimePicker");

    //start.max(end.value());
    end.min(start.value());
    end.value(start.value());
    _set_location_toolbar();
}

function _habilitar_controles(opcion) {
    $('#div_consulta_mensajes').css('display', 'none');
    $('#div_crear_mensaje').css('display', 'none');

    switch (opcion) {
        case 'Inicial':
            $('#div_consulta_mensajes').css('display', 'block');
            break;
        case 'Nuevo':
            $('#div_crear_mensaje').css('display', 'block');
            break;
    }
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

function _eventos() {
    try {
        $('#btn_guardar').on('click', function (e) {
            e.preventDefault();
            var validacion = _validar_controles();
            if (validacion.Estatus) {
                if ($('#txt_aviso_id').val() == "") {
                    _guardar_aviso();
                } else {
                    _actualizar_aviso();
                }
            } else {
                _mostrar_mensaje("Validación", validacion.Mensaje);
            }
        });
        $('#preview').on('click', function (e) {
            e.preventDefault();
            $('#div_preview').empty();
            $('#div_preview').append(htmlEscape($('#editor').val()));
        });
        $('#btn_nuevo').on('click', function (e) {
            e.preventDefault();
            _habilitar_controles('Nuevo');
            _limpiar_controles();
        });
        $('#btn_cancelar').on('click', function (e) {
            e.preventDefault();
            _habilitar_controles('Inicial');
            _limpiar_controles();
        });
    } catch (e) {

    }
}

function htmlEscape(value) {
    if (value) {
        return $('<div/>').html(value).text();
    } else {
        return '';
    }
}

function _set_location_toolbar() {
    $('#toolbar').parent().removeClass("pull-left");
    $('#toolbar').parent().addClass("pull-right");
}

function _guardar_aviso() {
    var $aviso = null;

    try {
        $aviso = new Object();

        $aviso.Mensaje = $('#editor').val();
        //$aviso.Fecha_Inicio_Vigencia = $("#cal_vigencia_inicio").data("kendoCalendar").value();
        //$aviso.Fecha_Fin_Vigencia = $("#cal_vigencia_termino").data("kendoCalendar").value();
        $aviso.Fecha_Inicio_Vigencia = $("#start").data("kendoDateTimePicker").value();
        $aviso.Fecha_Fin_Vigencia = $("#end").data("kendoDateTimePicker").value();
        var $data = JSON.stringify({ 'jsonObject': JSON.stringify($aviso) });

        $.ajax({
            type: 'POST',
            url: 'controller/Avisos_Controller.asmx/Guardar_Aviso',
            data: $data,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            cache: false,
            success: function ($result) {
                var Resultado = JSON.parse($result.d);

                if (Resultado != null && Resultado != undefined && Resultado != '') {
                    if (Resultado.Estatus == 'success') {
                        _mostrar_mensaje(Resultado.Titulo, Resultado.Mensaje);
                        _limpiar_controles();
                        _habilitar_controles('Inicial');
                        _search_ordenes_compra();
                    } else if (Resultado.Estatus == 'error') {
                        _mostrar_mensaje(Resultado.Titulo, Resultado.Mensaje);
                    }
                }
            }
        });
    } catch (e) {
        _mostrar_mensaje('Información técnica', e);
    }
}

function _actualizar_aviso() {
    var $aviso = null;

    try {
        $aviso = new Object();
        $aviso.Aviso_ID = parseInt($('#txt_aviso_id').val());
        $aviso.Mensaje = $('#editor').val();
        $aviso.Fecha_Inicio_Vigencia = $("#start").data("kendoDateTimePicker").value();
        $aviso.Fecha_Fin_Vigencia = $("#end").data("kendoDateTimePicker").value();
        var $data = JSON.stringify({ 'jsonObject': JSON.stringify($aviso) });

        $.ajax({
            type: 'POST',
            url: 'controller/Avisos_Controller.asmx/Actualizar_Aviso',
            data: $data,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            cache: false,
            success: function ($result) {
                var Resultado = JSON.parse($result.d);
                if (Resultado != null && Resultado != undefined && Resultado != '') {
                    if (Resultado.Estatus == 'success') {
                        _mostrar_mensaje(Resultado.Titulo, Resultado.Mensaje);
                        _limpiar_controles();
                        _habilitar_controles('Inicial');
                        _search_ordenes_compra();
                    } else if (Resultado.Estatus == 'error') {
                        _mostrar_mensaje(Resultado.Titulo, Resultado.Mensaje);
                    }
                }
            }
        });
    } catch (e) {
        _mostrar_mensaje('Información técnica', e);
    }
}

function _limpiar_controles() {
    var editor = $("#editor").data("kendoEditor");
    editor.value('');
    $('#start').val('');
    $('#end').val('');
    $('#div_preview').empty();
    $('#txt_aviso_id').val('');
}

function _validar_controles() {
    var _output = new Object();

    try {
        _output.Estatus = true;
        _output.Mensaje = '';

        if ($.trim($('#start').val()) === '') {
            _output.Estatus = false;
            _output.Mensaje += '&nbsp;<i class="fa fa-angle-double-right"></i>&nbsp;Ingrese la Fecha de Vigencia de Inicio.<br />';
        }
        if ($.trim($('#end').val()) === '') {
            _output.Estatus = false;
            _output.Mensaje += '&nbsp;<i class="fa fa-angle-double-right"></i>&nbsp;Ingrese la Fecha de Vigencia de Termino.<br />';
        }
        if ($.trim($('#editor').val()) === '') {
            _output.Estatus = false;
            _output.Mensaje += '&nbsp;<i class="fa fa-angle-double-right"></i>&nbsp;Ingrese el Mensaje.<br />';
        }

        if (_output.Mensaje != "")
            _output.Mensaje = "Favor de proporcionar lo siguiente: <br />" + _output.Mensaje;
    } catch (e) {
        _mostrar_mensaje('Informe técnico', e);
    } finally {
        return _output;
    }
}

function _crear_tbl_avisos() {
    try {
        $('#tbl_avisos').bootstrapTable('destroy');
        $('#tbl_avisos').bootstrapTable({
            cache: false,
            striped: true,
            pagination: true,
            pageSize: 10,
            pageList: [10, 25, 50, 100, 200],
            smartDysplay: false,
            search: true,
            showColumns: false,
            showRefresh: false,
            minimumCountColumns: 2,
            clickToSelect: false,
            filterControl: true,
            columns: [
                {
                    field: 'Editar', title: '', align: 'center', width: 5, clickToSelect: false,
                    formatter: function (value, row) {
                        return '<div>' +
                                    '<a class="remove ml10 edit orden_compra_editar" id="' + row.No_Orden_Compra + '" href="javascript:void(0)" data-aviso=\'' + row.Aviso_ID + '\' onclick="btn_editar_click(this);"><i class="glyphicon glyphicon-edit"></i></a>' +
                                '</div>';
                    }
                }, {
                    field: 'Eliminar', title: '', align: 'center', width: 5, clickToSelect: false,
                    formatter: function (value, row) {
                        return '<div>' +
                                    '<a class="remove ml10 edit" id="' + row.No_Orden_Compra + '" href="javascript:void(0)" data-aviso=\'' + row.Aviso_ID + '\' onclick="btn_eliminar_click(this);"><i class="glyphicon glyphicon-remove"></i></a>' +
                                '</div>';
                    }
                },
                {
                    field: 'Mensaje', title: 'Mensaje', align: 'center', clickToSelect: false,
                    formatter: function (value, row) {
                        return '<div>' +
                                    htmlEscape(value) +
                                '</div>';
                    }
                },
                {
                    field: 'Fecha_Inicio_Vigencia', title: 'Fecha Vigencia Inicio', align: 'center', valign: '', clickToSelect: false,filterControl:'select'
                },
                {
                    field: 'Fecha_Fin_Vigencia', title: 'Fecha Vigencia Termino ', align: 'center', valign: '', clickToSelect: false, filterControl:'select'
                }
            ]
        });
    } catch (e) {
        _mostrar_mensaje('Informe Técnico', e);
    }

}

function _search_ordenes_compra() {
    var filtros = null;
    try {
        jQuery.ajax({
            type: 'POST',
            url: 'controller/Avisos_Controller.asmx/Consultar_Avisos_Vigentes',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            cache: false,
            success: function (datos) {
                if (datos !== null) {
                    $('#tbl_avisos').bootstrapTable('load', JSON.parse(datos.d));
                }
            }
        });

    } catch (e) {

    }
}

function btn_editar_click(Mensaje) {
    var Aviso_ID = $(Mensaje).data('aviso');

    $aviso = new Object();

    $aviso.Aviso_ID = Aviso_ID;
    var $data = JSON.stringify({ 'jsonObject': JSON.stringify($aviso) });

    $.ajax({
        type: 'POST',
        url: 'controller/Avisos_Controller.asmx/Consultar_Aviso',
        data: $data,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        cache: false,
        success: function ($result) {
            var Resultado = JSON.parse($result.d);

            if (Resultado != null && Resultado != undefined && Resultado != '') {
                var row = Resultado;
                _limpiar_controles();
                _habilitar_controles('Nuevo');
                var editor = $("#editor").data("kendoEditor");
                editor.value(htmlEscape(row.Mensaje));
                $('#txt_aviso_id').val(row.Aviso_ID);
                $("#start").data("kendoDateTimePicker").value(row.Fecha_Inicio_Vigencia);
                $("#end").data("kendoDateTimePicker").value(row.Fecha_Fin_Vigencia);
                $('#div_preview').empty();
                $('#preview').click();
            }
        }
    });


}

function btn_eliminar_click(Mensaje) {
    var Aviso_ID = $(Mensaje).data('aviso');

    $aviso = new Object();

    $aviso.Aviso_ID = Aviso_ID;
    var $data = JSON.stringify({ 'jsonObject': JSON.stringify($aviso) });
    bootbox.dialog({
        message: '¿Desea eliminar el aviso?',
        title: 'Eliminar Aviso',
        locale: 'es',
        closeButton: true,
        buttons: [{
            label: 'Eliminar',
            className: 'btn-default',
            callback: function () {
                $.ajax({
                    type: 'POST',
                    url: 'controller/Avisos_Controller.asmx/Eliminar_Aviso',
                    data: $data,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,
                    cache: false,
                    success: function ($result) {

                        var Resultado = JSON.parse($result.d);

                        if (Resultado != null && Resultado != undefined && Resultado != '') {
                            if (Resultado.Estatus == 'success') {
                                _mostrar_mensaje(Resultado.Titulo, Resultado.Mensaje);
                                _search_ordenes_compra();
                            } else if (Resultado.Estatus == 'error') {
                                _mostrar_mensaje(Resultado.Titulo, Resultado.Mensaje);
                            }
                        }
                    }
                });
            }
        }, {
            label: 'Cerrar',
            className: 'btn-default',
            callback: function () {

            }
        }]
    });
}