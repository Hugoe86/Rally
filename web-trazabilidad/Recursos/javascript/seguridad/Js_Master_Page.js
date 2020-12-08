jQuery(document).on('ready', function () {
    _init_master();
  
});

//--------------------------------------
//  Funcion: _init_events_master
//  Descripción: Función que inicializa la página.
//  
//  Parámetros: Na
//  Usuario Creo: Juan Alberto Hernández Negrete
//  Fecha Creo: 01 Julio 2016 11:12 p.m.
//  Usuario Modifico:
//  Fecha Modifico:  
//--------------------------------------
function _init_master() {
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

        _init_events_master();
        _init_clock_config();   
        _init_btn_busqueda_menus_config();
        _config_lang();
        _events();
        _consultar_avisos();
        filterMenus();
        $(document).on('pjax:start', function () {
            NProgress.start();
        });
        $(document).on('pjax:end', function () { NProgress.done(); });

        $('#S_ID').select2({
            language: "es",
            theme: "classic",
            allowClear: true,
            width: '235px',
            //templateSelection: _formato_seleccion,
            escapeMarkup: function (m) { return m; }
        });
    } catch (e) {
        _mostrar_mensaje('Informe técnico', e);
    }
}

function _formato_seleccion($row) {
    return '<div class="item_selected">' + $row.Nombre_Mostrar + '</div>';
}
//--------------------------------------
//  Funcion: _init_events_master
//  Descripción: Función que inicializa los eventos de la página.
//  
//  Parámetros: Na
//  Usuario Creo: Juan Alberto Hernández Negrete
//  Fecha Creo: 01 Julio 2016 11:12 p.m.
//  Usuario Modifico:
//  Fecha Modifico:  
//--------------------------------------
function _init_events_master() {
    try {
        $('#btn_salir_sistema').click(function (e) {
            e.preventDefault();
            $.ajax({
                url: '../../Paginas/Paginas_Generales/controllers/Autentificacion_Controller.asmx/cerrar_sesion',
                type: 'POST',
                cache: false,
                async: false,
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                success: function (resul) {
                    var $resultado = JSON.parse(resul.d);

                    if ($resultado.Estatus === 'logout') {
                        $.cookie('username', null);
                        $.cookie('password', null);
                        $.cookie('remember', null);
                        window.location.href = "../Paginas_Generales/Frm_Apl_Login.html";
                    }
                }
            });
        });

        $('#btn_salir_sistema_mobile').click(function (e) {
            e.preventDefault();
            $.ajax({
                url: '../../Paginas/Paginas_Generales/controllers/Autentificacion_Controller.asmx/cerrar_sesion',
                type: 'POST',
                cache: false,
                async: false,
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                success: function (resul) {
                    var $resultado = JSON.parse(resul.d);

                    if ($resultado.Estatus === 'logout') {
                        $.cookie('username', null);
                        $.cookie('password', null);
                        $.cookie('remember', null);
                        window.location.href = "../Paginas_Generales/Frm_Apl_Login.html";
                    }
                }
            });
        });

        $('#btn_busqueda_menu').on('click', function (e) {
            e.preventDefault();
            $('.search-form').addClass("focused");
        });

        $('#busqueda_menus').on('click', function (e) {
            e.preventDefault();
            $('#ctrl_panel_menu').click();
        });
    } catch (e) {
        _mostrar_mensaje('Informe técnico', e);
    }
}

//--------------------------------------
//  Funcion: _mostrar_mensaje
//  Descripción: Función que generara muestra los mensajes que recibe como parámetro
//  
//  Parámetros: Titulo.- Titulo de la ventana.
//              Mensaje.- Informaciónque será mostrada al usuario.
//  Usuario Creo: Juan Alberto Hernández Negrete
//  Fecha Creo: 01 Julio 2016 11:12 p.m.
//  Usuario Modifico:
//  Fecha Modifico:  
//--------------------------------------
function _consultar_avisos() {
    //$.ajax({
    //    url: '../../Paginas/Trazabilidad/controllers/Avisos_Controller.asmx/Consultar_Avisos',
    //    type: 'POST',
    //    cache: false,
    //    async: false,
    //    dataType: 'json',
    //    contentType: 'application/json; charset=utf-8',
    //    success: function (resul) {
    //        var $resultado = JSON.parse(resul.d);

    //        if ($resultado.Estatus === 'success') {
    //            $('#div_master_mensaje_aviso').css('display', 'block').append(htmlEscape($resultado.Mensaje));
    //            //$('#lb_master_mensaje_aviso').html($resultado.Mensaje);

    //        }
    //    }
    //});
}

function htmlEscape(value) {
    if (value) {
        return $('<div/>').html(value).text();
    } else {
        return '';
    }
}

//--------------------------------------
//  Funcion: _mostrar_mensaje
//  Descripción: Función que generara muestra los mensajes que recibe como parámetro
//  
//  Parámetros: Titulo.- Titulo de la ventana.
//              Mensaje.- Informaciónque será mostrada al usuario.
//  Usuario Creo: Juan Alberto Hernández Negrete
//  Fecha Creo: 01 Julio 2016 11:12 p.m.
//  Usuario Modifico:
//  Fecha Modifico:  
//--------------------------------------
function _mostrar_mensaje(Titulo, Mensaje) {
    var box = bootbox.dialog({
        message: Mensaje,
        title: Titulo,
        locale: 'es',
        closeButton: true,
        buttons: [{
            label: 'Cerrar',
            className: 'btn btn-success',
            callback: function () { }
        }]
    });

    box.bind('shown.bs.modal', function () {
        box.find(".btn-success:first").focus();
    });
}

function _init_clock_config() {
    var options = {};
    $('.jclock').jclock(options);

    $('.jclock').qtip({
        content: '<i class="fontello-icon-pin" style="color:#000 ;"></i>&nbsp;' + new Date().toString('dddd, dd MMMM yyyy HH:mm:ss tt'),
        position: {
            corner: {
                target: 'topLeft',
                tooltip: 'bottomRight'
            }
        },
        show: {
            when: { event: 'mouseover' },
            ready: false
        },
        hide: { event: 'mouseout' },
        style: {
            border: {
                width: 5,
                radius: 10,
                color: '#91b23d'
            },
            padding: 10,
            textAlign: 'center',
            tip: true,
            background: '#fff',
            color: '#777',
            width: 350
        }
    });
}

function _init_btn_busqueda_menus_config() {
    $('#btn_busqueda_menu').qtip({
        content: '<i class="fontello-icon-search-1" style="color:#000 ;"></i>&nbsp; Búsqueda de menus...',
        position: {
            corner: {
                target: 'bottomMiddle',
                tooltip: 'topMiddle'
            }
        },
        show: {
            when: { event: 'mouseover' },
            ready: false
        },
        hide: { event: 'mouseout' },
        style: {
            border: {
                width: 5,
                radius: 10,
                color: '#55acee'
            },
            padding: 10,
            textAlign: 'center',
            tip: true,
            background: '#fff',
            color: '#777',
            width: 200
        }
    });
}

function _tooltip(_selector, _title, _tooltipAlign) {
    $(_selector).qtip({
        content: _title,
        position: {
            corner: {
                target: 'topMiddle',
                tooltip: _tooltipAlign
            }
        },
        show: {
            when: { event: 'mouseover' },
            ready: false
        },
        hide: { event: 'mouseout' },
        style: {
            border: {
                width: 5,
                radius: 7
            },
            padding: 5,
            textAlign: 'center',
            tip: {
                corner: true,
                method: "polygon",
                border: 1,
                height: 20,
                width: 9
            },
            background: '#F5F6CE',
            color: '#2d2d30',
            width: 200,
            'font-size': 'small',
            'font-family': 'Calibri',
            'font-weight': 'Bold',
            tip: true,
            name: 'blue'
        }
    });
}

function _config_lang() {
    var language = window.navigator.language || window.navigator.userLanguage;
    var idiomaActual = language.toLowerCase().replace(/-.*/, '');

    $(document).find('#_inicio').prop('lang', idiomaActual);
    $(document).find('#_inicio').attr('data-lang-token', 'inicio');

    window.lang = new Lang(idiomaActual);

    lang.init({
        defaultLang: idiomaActual
    });
}

function _events() {
    $('#option_en').on('click', function () {
        window.lang.dynamic('en', '../../Recursos/jquery-lang/langpack/en.json');
        window.lang.loadPack('en');
        window.lang.change('en');
    });

    $('#option_es').on('click', function () {
        window.lang.dynamic('es', '../../Recursos/jquery-lang/langpack/es.json');
        window.lang.loadPack('es');
        window.lang.change('es');
    });

    $('#option_zh').on('click', function () {
        window.lang.dynamic('zh', '../../Recursos/jquery-lang/langpack/zh.json');
        window.lang.loadPack('zh');
        window.lang.change('zh');
    });

    $('#S_ID').on('change', function () {
        var url = $(this).val(); // get selected value
        if (url) { // require a URL
            window.location = url; // redirect
        }
        return false;
    });

    $('#S_ID').on('select2:open', function (e) {
        $(document).on('keyup', '.select2-search__field', function (e) {
            e.preventDefault();
            var filter = $(this).val().toLowerCase();

            var count = 0;
            $('#main-menu').find("li").each(function (index, li) {

                if (filter == "") {
                    li.style["display"] = "list-item";
                } else if (!li.textContent.toLowerCase().match(filter) && !$(li).hasClass('li_filter_menu')) {
                    li.style["display"] = "none";
                } else {
                    li.style["display"] = "list-item";
                }
            });
        });
    });

    $('#S_ID').on('select2:unselect', function (e) {
        $('#main-menu').find("li").each(function (index, li) {
            li.style["display"] = "list-item";
        });
    });

    $("#btn_cambiar_contraseña").on('click', function (e) {
        //e.preventDefault();
        _limp_cntls_cmb_pass();
        $('#mdal_cambio_contrasena').modal('show', { backdrop: 'static', keyboard: false });
       
    });
 
}

function filterMenus() {
    $("#menuFilter").on("keyup", function (e) {
        e.preventDefault();
        var filter = this.value.toLowerCase();

        var count = 0;
        $('#main-menu').find("li").each(function (index, li) {

            if (filter == "") {
                li.style["display"] = "list-item";
            } else if (!li.textContent.toLowerCase().match(filter)) {
                li.style["display"] = "none";
            } else {
                li.style["display"] = "list-item";
            }
        });

    });
}

//--------------------------------------
//  Funcion: _validacionPermisoUsuarios
//  Descripción: Función que valida que los usuarios logueado que recibe como parámetro
//  Parámetros: Nombre_Proceso.- Nombre del proceso 
//  Usuario Creo: Jose Maldonado Mendez
//  Fecha Creo: 01 Julio 2016 11:12 p.m.
//  Usuario Modifico:
//  Fecha Modifico:  
//--------------------------------------
function _validacionPermisoUsuarios(Nombre_Proceso) {
    var filtros = null;
    var resultado = new Object;
    try {
        filtros = new Object();

        filtros.Proceso = Nombre_Proceso;
        var $data = JSON.stringify({ 'jsonObject': JSON.stringify(filtros) });

        $.ajax({
            type: 'POST',
            url: '../../Paginas/Paginas_Generales/controllers/Autentificacion_Controller.asmx/Consultar_Permisos_Procesos',
            data: $data,
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            async: false,
            cache: false,
            success: function (datos) {
                if (datos !== null) {
                    resultado = JSON.parse(datos.d);
                } else {
                    resultado = null;
                }
            }
        });
    } catch (e) {

    }

    return resultado;
}

function formatNumber(num) {
    if (!num || num == 'NaN') return '-';
    if (num == 'Infinity') return '&#x221e;';
    num = num.toString().replace(/\$|\,/g, '');
    if (isNaN(num))
        num = "0";
    sign = (num == (num = Math.abs(num)));
    num = Math.floor(num * 100 + 0.50000000001);
    cents = num % 100;
    num = Math.floor(num / 100).toString();
    if (cents < 10)
        cents = "0" + cents;
    for (var i = 0; i < Math.floor((num.length - (1 + i)) / 3); i++)
        num = num.substring(0, num.length - (4 * i + 3)) + ',' + num.substring(num.length - (4 * i + 3));
    return (((sign) ? '' : '-') + num + '.' + cents);
}

//--------------------------------------
//  Funcion: _crearNuevaNotificacion
//  Descripción: Función que valida que los usuarios logueado que recibe como parámetro
//  Parámetros: Nombre_Proceso.- Nombre del proceso 
//  Usuario Creo: Jose Maldonado Mendez
//  Fecha Creo: 01 Julio 2016 11:12 p.m.
//  Usuario Modifico:
//  Fecha Modifico:  
//--------------------------------------
function _crearNuevaNotificacion(Json_List, Url_Notificacion, Icono, Tipo_Notificacion_ID, Mensaje) {
    var filtros = null;
    var resultado = false;
    try {
        filtros = new Object();

        filtros.List_Usuario = Json_List;
        filtros.Url_Notificacion = Url_Notificacion;
        filtros.Icono = Icono;
        filtros.Tipo_Notificacion_ID = Tipo_Notificacion_ID;
        filtros.Mensaje = Mensaje;

        var $data = JSON.stringify({ 'jsonObject': JSON.stringify(filtros) });

        $.ajax({
            type: 'POST',
            url: '../../Paginas/Paginas_Generales/controllers/Autentificacion_Controller.asmx/Crear_Notificacion',
            data: $data,
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            async: false,
            cache: false,
            success: function (datos) {
                resultado = true;
            }
        });
    } catch (e) {

    }

    return resultado;
}

function _mostrar_modal_notificaciones(Url_Notificacion, Icono, Tipo_Notificacion_ID, Mensaje) {
    lst = '';
    var options = {
        'show': true,
        'title': 'Notificaciones',
        'icon': 'fa fa-bell',
        'iconCancel': 'fa fa-close',
        'textCancel': 'Cancelar',
        'iconSuccess': 'fa fa-check',
        'textSuccess': 'Aceptar',
        'onError': function (response) {
            // return: string message
        },
        'onSuccess': function (response) {
            // return: array
            lst = JSON.stringify(response);
            _crearNuevaNotificacion(lst, Url_Notificacion, Icono, Tipo_Notificacion_ID, Mensaje);
        }
    };

    $.notification(options);
}
