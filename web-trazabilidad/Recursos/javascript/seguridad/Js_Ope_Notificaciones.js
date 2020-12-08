$(document).on('ready', function () {
    _eventos_notificaciones();
    _contador_tiempo();
});

function _eventos_notificaciones() {
    $('#Ref_Notif').on('click', function (e) {
        e.preventDefault();
        /*_notificaciones_revision();*/
    });
}

function _contador_tiempo() {
    _consultar_notificaciones_usuario_reload();
    setTimeout(function () {

        _contador_tiempo();
    }, 80000);
}

function _consultar_notificaciones_usuario() {
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
        //    url: '../Paginas_Generales/controllers/Notificaciones_Controller.asmx/Consultar_Notificaciones',
        //    //data: $data,
        //    contentType: "application/json; charset=utf-8",
        //    dataType: "json",
        //    async: false,
        //    cache: false,
        //    success: function (datos) {
        //        if (datos !== null) {
        //            _create_structure_notifications(JSON.parse(datos.d));
        //        }
        //    }
        //});
    } catch (e) {

    }

}

function _create_structure_notifications(datos) {
    var tags = '';
    try {
        tags += '<ul class="dropdown-menu-list list-unstyled " style="overflow-y:scroll ">';
        var count = 0;


        if (datos.length > 0) {
            $.each(datos, function (index, value) {
                tags += '<li class="active notification-success">'
                tags += '   <a href="#' + '">';
                tags += '       <i class="' + (value.Icono == null ? 'fa-bell-o' : value.Icono) + '"></i>';

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
        tags += '</ul>';
        var Tabla = $('#dwn_lst_notif');
        $('ul', Tabla).remove();
        Tabla.append(tags);
        $('#notif_count').html(count);
    } catch (e) {

    }
}

function _create_notification(users, mensaje, icon, url) {
    var filtros = null;
    try {

        filtros = new Object();
        filtros.Users = users;
        filtros.Mensaje = mensaje;
        filtros.Icon = icon;
        filtros.Url = url;

        var $data = JSON.stringify({ 'jsonObject': JSON.stringify(filtros) });

        //$.ajax({
        //    type: 'POST',
        //    url: '../Paginas_Generales/controllers/Notificaciones_Controller.asmx/Crear_Notificacion',
        //    data: $data,
        //    contentType: "application/json; charset=utf-8",
        //    dataType: "json",
        //    async: false,
        //    cache: false,
        //    success: function (datos) {
        //        if (datos !== null) {
        //            //_create_structure_notifications(JSON.parse(datos.d));
        //        }
        //    }
        //});
    } catch (e) {

    }
}

function _notificaciones_revision() {

    var filtros = null;
    try {

        //filtros = new Object();
        //filtros.Users = users;
        //filtros.Mensaje = mensaje;
        //filtros.Icon = icon;
        //filtros.Url = url;

        //var $data = JSON.stringify({ 'jsonObject': JSON.stringify(filtros) });

        //$.ajax({
        //    type: 'POST',
        //    url: '../Paginas_Generales/controllers/Notificaciones_Controller.asmx/Modificar_Notificacion',
        //    //data: $data,
        //    contentType: "application/json; charset=utf-8",
        //    dataType: "json",
        //    async: false,
        //    cache: false,
        //    success: function (datos) {
        //        if (datos !== null) {
        //            //_create_structure_notifications(JSON.parse(datos.d));
        //        }
        //    }
        //});
    } catch (e) {

    }
}

/*Consulta de Notificaciones RELOAD*/

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

function _seleccion_notificacion(_notificacion_id, url_cambio) {
    var filtros = null;
    try {

        filtros = new Object();
        filtros.Notificacion_ID = String(_notificacion_id);

        var $data = JSON.stringify({ 'jsonObject': JSON.stringify(filtros) });

        //$.ajax({
        //    type: 'POST',
        //    url: '../Paginas_Generales/controllers/Notificaciones_Controller.asmx/Generar_Lectura_Notificacion',
        //    data: $data,
        //    contentType: "application/json; charset=utf-8",
        //    dataType: "json",
        //    async: false,
        //    cache: false,
        //    success: function (result) {
        //        var Resultado = JSON.parse(result.d);
        //        if (Resultado != null && Resultado != undefined && Resultado != '') {
        //            if (Resultado.Estatus == 'success') {
        //                window.location.href = url_cambio;
        //            } else if (Resultado.Estatus == 'error') {
        //                alert("Error");
        //            }
        //        } else {
        //            alert("Error");
        //        }
        //    }
        //});
    } catch (e) {

    }
}

function _mostrar_todas_notificaciones() {
    window.location.href = "../Paginas_Generales/Frm_Apl_Mostrar_Notificaciones.aspx";
}