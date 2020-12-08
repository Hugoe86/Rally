$(document).on('ready', function () {
    _load_vistas();
});

function _inicializar_pagina() {
    try {
        NProgress.configure({
            template:
                '<div class="nprogress-content"> ' +
                '    <div class="bar" role="bar"> ' +
                '        <div class="peg"> ' +
                '        </div> ' +
                '    </div> ' +
                '    <div class="spinner" role="spinner"> ' +
                '        <div class="spinner-icon"> ' +
                '        </div> ' +
                '        <div class="lazyload"><img src="../../Recursos/img/gears.svg"></img><br/><center><strong style="font-family: Century Gothic; font-size:24px; color: black;">Procesando<div class="faa-pulse animated">...</div></strong></center></div> ' +
                '    </div> ' +
                '<div> '
        });

        _set_location_toolbar();
    } catch (e) {
        _mostrar_mensaje('Informe Técnico', e);
    }
}

function _cargar_tabla(_tipo) {

    try {
        $('#tbl_notificaciones').bootstrapTable('destroy');
        $('#tbl_notificaciones').bootstrapTable({
            cache: false,
            striped: true,
            pagination: false,
            data: [],
            pageSize: 10,
            pageList: [10, 25, 50, 100, 200],
            smartDysplay: false,
            search: true,
            showColumns: false,
            showRefresh: false,
            minimumCountColumns: 2,
            clickToSelect: true,
            contextMenu: _tipo,
            onContextMenuItem: function (row, $el) {
                if ($el.data("item") == "sin_leer") {
                    _cambiar_notificacion_sin_leer(row.Notificacion_ID);
                }
                else if ($el.data("item") == "leido") {
                    _cambiar_notificacion_leer(row.Notificacion_ID);
                }
            },
            columns: [
                { field: 'Tipo_Notificacion_ID', title: '', align: 'center', valign: 'bottom', sortable: true, visible: false },
                { field: 'Estatus', title: '', align: 'center', valign: 'bottom', sortable: true, visible: false },
                { field: 'Notificacion_ID', title: 'Clave', align: 'center', valign: 'bottom', sortable: true, width: 50 },
                { field: 'Tipo_Notificacion', title: 'Modulo', align: 'left', valign: '', sortable: true, width: 150 },
                {
                    field: 'Fecha',
                    title: 'Fecha',
                    align: 'left',
                    valign: '',
                    width: 100,
                    sortable: true,
                    visible: true,
                    formatter: function (value, row) {
                        var _valor = '<i class="fa fa-calendar" aria-hidden="true"></i> ' + row.Fecha;

                        return _valor;
                    }
                },
                {
                    field: 'Mensaje',
                    title: 'Mensaje',
                    align: 'left',
                    align: 'center',
                    valign: 'bottom',
                    formatter: function (value, row) {
                        var _valor = '<div class="truncate"> ' + row.Mensaje + '</div>';

                        return _valor;
                    }
                },
                {
                    field: 'Url',
                    title: '',
                    align: 'center',
                    valign: 'bottom',
                    width: 100,
                    clickToSelect: false,
                    formatter: function (value, row) {
                        return row.Estatus === "NO_VISTO" ? ('<div> ' +
                            '<a class="remove ml10 edit" id="' + row.Notificacion_ID + '" href="javascript:void(0)" data-notificacion=\'' + JSON.stringify(row) + '\' onclick="btn_leer_click(this);" title="Ir a pagina"><i class="fa fa-link"></i></a>&nbsp;&nbsp;&nbsp;&nbsp;Ir a pagina' + '</div>') : "";
                    }
                }
            ]
        });
    } catch (e) {
        _mostrar_mensaje('Informe Técnico', e);
    }
}

function _search() {
    var filtros = null;
    try {
        show_loading_bar({
            pct: 78,
            wait: .5,
            delay: .5,
            finish: function (pct) {
                filtros = new Object();
                filtros.Tipo_Notificacion_ID = $('#cmb_tipo_notificacion').val();
                filtros.Estatus = $('#cmb_estatus').val();
                var $data = JSON.stringify(filtros);

                $.ajax({
                    type: 'POST',
                    url: UrlApp + "/api/Mostrar_Notificaciones/Consultar_Notificaciones_Por_Filtros",
                    data: $data,
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    cache: false,
                    success: function (datos) {
                        if (datos !== null) {
                            $('#tbl_notificaciones').bootstrapTable('load', JSON.parse(datos));
                            hide_loading_bar();
                        }
                    }
                });
            }
        });
    } catch (e) {

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
            className: 'btn-info',
            callback: function () { }
        }]
    });
}

function _launchComponent(component, id) {

    $('#' + id).load(component, function () {

        switch (id) {

            case 'Operaciones_Vista':
                _inicializar_pagina();
                _cargar_combos();
                _crear_cargar_tabla_tipo_estatus();
                _eventos_componente1();
                break;

            default:

        }
    });
}

function _load_vistas() {
    _launchComponent('Vistas_Auxiliares/Mostrar_Notificaciones/Operaciones_Vista.html', 'Operaciones_Vista');
}

function _set_location_toolbar() {
    $('#toolbar').parent().removeClass("pull-left");
    $('#toolbar').parent().addClass("pull-right");
}

function _mostrar_mensaje_validacion(_mensaje, _tipo) {
    Command: toastr[_tipo](_mensaje, "Aviso")
}

function _eventos_componente1() {
    try {
        $('#cmb_tipo_notificacion').on('change', _crear_cargar_tabla_tipo_estatus);
        $('#cmb_estatus').on('change', _crear_cargar_tabla_tipo_estatus);
        $("#tbl_notificaciones").on("click-cell.bs.table", function (field, value, row, $el) {
            if (value === "Mensaje") {
                _mostrar_mensaje("Vista completa del mensaje", row);
            }
        });

    } catch (e) {
        _mostrar_mensaje('Informe Técnico', e);
    }
}

function _cargar_combos() {
    _cargar_tipos_notificaciones();
    _cargar_estatus();
}

function _cargar_tipos_notificaciones() {
    var filtros = null;
    try {
        jQuery.ajax({
            type: 'POST',
            url: UrlApp + "/api/Mostrar_Notificaciones/Consultar_Tipos_Notificacion",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: true,
            cache: false,
            success: function (datos) {
                if (datos !== null) {
                    var datos_combo = $.parseJSON(datos);
                    var combo = $('#cmb_tipo_notificacion');
                    $('option', combo).remove();

                    var options = '<option value=""><-SELECCIONE></option>';
                    options += '<option value="-1">[PER] Personificado</option>';
                    for (var Indice_Tipo_Notificaciones = 0; Indice_Tipo_Notificaciones < datos_combo.length; Indice_Tipo_Notificaciones++) {
                        options += '<option value="' + datos_combo[Indice_Tipo_Notificaciones].Tipo_Notificacion_ID + '">' + datos_combo[Indice_Tipo_Notificaciones].Nombre + '</option>';
                    }
                    combo.append(options);
                }
            }
        });
    } catch (e) {

    }
}

function _cargar_estatus() {
    var select_estatus = $('#cmb_estatus');
    $('option', select_estatus).remove();
    var options_estatus = '<option value="">SELECCIONE</option>';
    options_estatus += '<option value="LEIDO">LEIDO</option>';
    options_estatus += '<option value="NO_LEIDO">NO LEIDO</option>';
    select_estatus.append(options_estatus);

    $('#cmb_estatus').val("NO_LEIDO");
}

function _crear_cargar_tabla_tipo_estatus() {
    if ($('#cmb_estatus').val() === "LEIDO") {
        _cargar_tabla('#context-menu');
        _search();
    }
    else {
        _cargar_tabla("#context-menu-visto");
        _search();
    }
}

function btn_leer_click(notificacion) {
    var row = $(notificacion).data('notificacion');
    _seleccion_notificacion(row.Notificacion_ID, row.Url)
}

function _seleccion_notificacion(_notificacion_id, url_cambio) {
    var filtros = null;
    try {

        filtros = new Object();
        filtros.Notificacion_ID = String(_notificacion_id);

        var $data = JSON.stringify(filtros);

        NProgress.start();

        $.ajax({
            type: 'POST',
            url: UrlApp + "/api/Mostrar_Notificaciones/Generar_Lectura_Notificacion",
            data: $data,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            cache: false,
            success: function (result) {
                var Resultado = JSON.parse(result);
                if (Resultado != null && Resultado != undefined && Resultado != '') {
                    if (Resultado.Estatus == 'success') {
                        _crear_cargar_tabla_tipo_estatus();
                        _consultar_notificaciones_usuario_reload();
                        window.location.href = url_cambio;
                        NProgress.done();
                    } else if (Resultado.Estatus == 'error') {
                        NProgress.done();
                        alert("Error");
                    }
                } else {
                    NProgress.done();
                    alert("Error");
                }
            }
        });

    } catch (e) {
        _mostrar_mensaje('Informe Tecnico', e);
    }
}

function _cambiar_notificacion_sin_leer(_notificacion_id) {
    var filtros = null;
    try {

        filtros = new Object();
        filtros.Notificacion_ID = String(_notificacion_id);

        var $data = JSON.stringify(filtros);

        NProgress.start();

        $.ajax({
            type: 'POST',
            url: UrlApp + "/api/Mostrar_Notificaciones/Sin_Leer_Notificacion",
            data: $data,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            cache: false,
            success: function (result) {
                var Resultado = JSON.parse(result);
                if (Resultado != null && Resultado != undefined && Resultado != '') {
                    if (Resultado.Estatus == 'success') {
                        _crear_cargar_tabla_tipo_estatus();
                        _consultar_notificaciones_usuario_reload();
                        NProgress.done();
                    } else if (Resultado.Estatus == 'error') {
                        NProgress.done();
                        alert("Error");
                    }
                } else {
                    NProgress.done();
                    alert("Error");
                }
            }
        });

    } catch (e) {
        _mostrar_mensaje('Informe Tecnico', e);
    }
}

function _cambiar_notificacion_leer(_notificacion_id) {
    var filtros = null;
    try {

        filtros = new Object();
        filtros.Notificacion_ID = String(_notificacion_id);

        var $data = JSON.stringify(filtros);

        NProgress.start();

        $.ajax({
            type: 'POST',
            url: UrlApp + "/api/Mostrar_Notificaciones/Generar_Lectura_Notificacion",
            data: $data,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            cache: false,
            success: function (result) {
                var Resultado = JSON.parse(result);
                if (Resultado != null && Resultado != undefined && Resultado != '') {
                    if (Resultado.Estatus == 'success') {
                        _crear_cargar_tabla_tipo_estatus();
                        _consultar_notificaciones_usuario_reload();
                        NProgress.done();
                    } else if (Resultado.Estatus == 'error') {
                        NProgress.done();
                        alert("Error");
                    }
                } else {
                    NProgress.done();
                    alert("Error");
                }
            }
        });

    } catch (e) {
        _mostrar_mensaje('Informe Tecnico', e);
    }
}

function _consultar_notificaciones_usuario_reload() {
    var filtros = null;
    try {

        filtros = new Object();
        //filtros.Clave = $('#Txt_Clave_Filtro').val();
        //filtros.Proyecto = $('#Txt_Proyecto_Filtro').val();
        //filtros.Estatus = $('#Cmb_Estatus_Filtro').val();
        //if ($('#Cmb_Corporativo_Filtro :selected').val() !== undefined && $('#Cmb_Corporativo_Filtro').val() !== '')
        //    filtros.Corporativo_ID = parseInt($('#Cmb_Corporativo_Filtro').val());
        //filtros.Prioridad = $('#Cmb_Prioridad_Filtro').val();

        //var $data = JSON.stringify({ 'jsonObject': JSON.stringify(filtros) });

        //$.ajax({
        //    type: 'POST',
        //    url: '../Paginas_Generales/controllers/Notificaciones_Controller.asmx/Consultar_Notificaciones_Usuario',
        //    //data: $data,
        //    contentType: "application/json; charset=utf-8",
        //    dataType: "json",
        //    async: false,
        //    cache: false,
        //    success: function (datos) {
        //        if (datos !== null) {
        //            _create_structure_notifications_reload(JSON.parse(datos.d));
        //        }
        //    }
        //});
    } catch (e) {

    }

}

function _create_structure_notifications_reload(datos) {
    var tags = '';
    try {
        tags += '<ul class="dropdown-menu-list list-unstyled " style="overflow-y:scroll ">';
        var count = 0;


        if (datos.length > 0) {
            $.each(datos, function (index, value) {
                tags += '<li class="active notification-success" onclick="_seleccion_notificacion(' + value.Notificacion_ID + ',' + '\'' + value.Url + '\'' + ');">'
                tags += '   <a href="#' + '">';
                tags += '       <i class="' + (value.Icono == null ? 'fa-exclamation' : value.Icono) + '"></i>';

                tags += '       <span class="line" style=" overflow: hidden; padding-left:20px; text-overflow: ellipsis; white-space: nowrap;">';
                if (value.Estatus === "NO_VISTO") {
                    count += 1;
                    tags += '           <strong>' + value.Mensaje + '</strong>';
                } else {
                    tags += value.Mensaje;
                }
                tags += '       </span>';

                tags += '       <span class="line small time">';
                tags += new Date(value.Fecha).toString('dd/MMM/yy HH:mm').toUpperCase();
                tags += '       </span>';
                tags += '   </a>';
                tags += '</li>';
            });

        } else {
            tags += '<li class="active notification-success">'
            tags += '   <a href= "#' + '">';
            tags += '       <i class="fa-exclamation"></i>';

            tags += '       <span class="line">';
            tags += '           <strong>No Tiene Notificaciones</strong>';
            tags += '       </span>';

            tags += '       <span class="line small time">';
            tags += '       </span>';
            tags += '   </a>';
            tags += '</li>';
        }
        tags += '<li class="external" onclick="_mostrar_todas_notificaciones()">';
        tags += '    <a href="#">';
        tags += '          <span>Ver todas las notificaciones</span';
        tags += '           <i class="fa-link-ext"></i>';
        tags += '    </a>';
        tags += '</li>';
        var Tabla = $('#dwn_lst_notif');
        $('ul', Tabla).remove();
        Tabla.append(tags);
        $('#notif_count').html(count);
    } catch (e) {

    }
}