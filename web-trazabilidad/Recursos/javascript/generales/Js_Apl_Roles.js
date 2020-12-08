$Accion = 'Inicio';
$Accesos = null;
/*-------------------------------------------------------------- Inicio --------------------------------------------------------------*/
/*------------------------------------------------------------------------------
<Descripcion>   funcion de inicio del formulario
<Creo>          Juan Alberto Hernandez Negrete
<Fecha_Creo>    29-Mayo-2017
------------------------------------------------------------------------------*/
$(document).on('ready', function () {
    $('.progressBackgroundFilter').ajaxStart(function () { $(this).show(); }).ajaxStop(function () { $(this).hide(); });
    $('.progressBackgroundFilter').hide();

    Inicializar_Pagina();
    Inicializar_Eventos();
});

/*-------------------------------------------------------------- Métodos --------------------------------------------------------------*/
/*------------------------------------------------------------------------------
<Descripcion>   funcion para inicializar los eventos
<Creo>          Juan Alberto Hernandez Negrete
<Fecha_Creo>    29-Mayo-2017
------------------------------------------------------------------------------*/
function Inicializar_Eventos() {
    try {
        $('#Btn_Salir').on('click', function (e) { e.preventDefault(); window.location.href = '../Generales/Frm_Apl_Principal.aspx'; });
        $('#Btn_Nuevo').on('click', function (e) {
            e.preventDefault();
            Limpiar_Controles();
            Manejo_Controles('Nuevo');
        });
        $('#Btn_Modificar').on('click', function (e) {
            e.preventDefault();
            if ($('#Hf_Rol_ID').val() == '' || $('#Hf_Rol_ID').val() == undefined || $('#Hf_Rol_ID').val() == null)
                Mensaje_Validacion('Favor de seleccionar el registro a modificar.');
            else
                Manejo_Controles('Modificar');
        });
        $('#Btn_Eliminar').on('click', function (e) {
            e.preventDefault();

            if ($('#Hf_Rol_ID').val() == '' || $('#Hf_Rol_ID').val() == undefined || $('#Hf_Rol_ID').val() == null)
                Mensaje_Validacion('Favor de seleccionar el registro a eliminar.');
            else {
                bootbox.confirm({
                    title: 'Eliminar Registro',
                    message: '¿Está seguro de eliminar el registro seleccionado?',
                    callback: function (result) {
                        if (result) {
                            Btn_Eliminar_Click();
                        }
                    }
                });
            }
        });
        $('#Btn_Cancelar').on('click', function (e) { e.preventDefault(); Inicializar_Pagina(); });
        $('#Btn_Guardar').on('click', function (e) {
            e.preventDefault();
            Remove_Class_Error();
            var Validar = Validacion();
            if (Validar.Estatus) {
                if ($('#Hf_Rol_ID').val() != "" && $('#Hf_Rol_ID').val() != undefined && $('#Hf_Rol_ID').val() != null)
                    Btn_Guardar_Actualizacion_Click()
                else
                    Btn_Guardar_Alta_Click();
            }
            else
                Mensaje_Validacion(Validar.Mensaje);
        });

        $('.detail-icon').off('click');

        Evento_Remove_Class_Error(); //creamos los eventos para quitar clase error al foco del control

        //inicalizamos los permisos
        var Pagina = window.location.pathname;
        //Obtener_Permisos_Acceso('Btn_Nuevo', 'Btn_Modificar', 'Btn_Eliminar', '', '', Pagina);//obtenemos los permisos a los botones
    } catch (e) {
        Mostrar_Mensaje("Informa técnico", e);
    }
}

/*------------------------------------------------------------------------------
<Descripcion>   funcion para inicializar la pagina
<Creo>          Juan Alberto Hernandez Negrete
<Fecha_Creo>    29-Mayo-2017
------------------------------------------------------------------------------*/
function Inicializar_Pagina() {
    try {
        NProgress.start();

        Limpiar_Controles();
        Consultar_Menus();
        Manejo_Controles('Inicio');
        Consultar_Informacion();

        NProgress.done();
    } catch (e) {
        Mostrar_Mensaje("Informa técnico", e);
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
function Mostrar_Mensaje(Titulo, Mensaje) {
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

/*------------------------------------------------------------------------------
<Descripcion>   funcion para el control de los botones
<Creo>          Juan Alberto Hernandez Negrete
<Fecha_Creo>    29-Mayo-2017
------------------------------------------------------------------------------*/
function Manejo_Controles(Accion) {
    var Estatus = false;
    try {
        switch (Accion) {
            case "Inicio":
                Estatus = false;
                $Accion = 'Inicio';
                $('#Btn_Salir').css({ display: 'inline' });
                $('#Btn_Nuevo').css({ display: 'inline' });
                $('#Btn_Modificar').css({ display: 'inline' });
                $('#Btn_Eliminar').css({ display: 'inline' });
                $('#Btn_Guardar').css({ display: 'none' });
                $('#Btn_Cancelar').css({ display: 'none' });
                $('[href="#Tab_Rol"]').tab('show');
                break;
            case "Nuevo":
                Estatus = true;
                $Accion = 'Nuevo';
                $('#Btn_Salir').css({ display: 'none' });
                $('#Btn_Nuevo').css({ display: 'none' });
                $('#Btn_Modificar').css({ display: 'none' });
                $('#Btn_Eliminar').css({ display: 'none' });
                $('#Btn_Guardar').css({ display: 'inline' });
                $('#Btn_Cancelar').css({ display: 'inline' });
                $('[href="#Tab_Accesos"]').tab('show');
            case "Modificar":
                Estatus = true;
                $Accion = 'Modificar';
                $('#Btn_Salir').css({ display: 'none' });
                $('#Btn_Nuevo').css({ display: 'none' });
                $('#Btn_Modificar').css({ display: 'none' });
                $('#Btn_Eliminar').css({ display: 'none' });
                $('#Btn_Guardar').css({ display: 'inline' });
                $('#Btn_Cancelar').css({ display: 'inline' });
                $('[href="#Tab_Accesos"]').tab('show');
                break;
        }

        $('#Txt_Nombre').attr({ disabled: !Estatus });
        $('#Cmb_Estatus').attr({ disabled: !Estatus });
        $('#Txt_Descripcion').attr({ disabled: !Estatus });

        if (Estatus) {
            $("#Div_Tabla").hide();
        }
        else {
            $("#Div_Tabla").show();
        }

        Habilitar_Checkbox();
    } catch (e) {
        Mostrar_Mensaje("Informa técnico", e);
    }
}

/*------------------------------------------------------------------------------
<Descripcion>   funcion para limpiar los controles de la pagina
<Creo>          Juan Alberto Hernandez Negrete
<Fecha_Creo>    29-Mayo-2017
------------------------------------------------------------------------------*/
function Limpiar_Controles() {
    $('#Div_Contenido input[type=text]').each(function () { $(this).val(''); });
    $('#Div_Contenido input[type=hidden]').each(function () { $(this).val(''); });
    $('#Div_Contenido select').each(function () { $(this).val(''); });
    Mensaje_Validacion('');
    Remove_Class_Error();
    //$('#Li_Rol').addClass('active');
    //$('#Li_Accesos').removeClass('active');
    $('input[type=checkbox]').each(function () {
        $(this)[0].checked = false;
    });
}

/*------------------------------------------------------------------------------
<Descripcion>   funcion para agregar el estilo de error
<Creo>          Juan Alberto Hernandez Negrete
<Fecha_Creo>    29-Mayo-2017
------------------------------------------------------------------------------*/
function Add_Class_Error(Selector) {
    $(Selector).addClass('alert-danger');
}

/*------------------------------------------------------------------------------
<Descripcion>   funcion para quitar el estilo de error
<Creo>          Juan Alberto Hernandez Negrete
<Fecha_Creo>    29-Mayo-2017
------------------------------------------------------------------------------*/
function Evento_Remove_Class_Error() {
    $('#Div_Contenido input[type=text]').each(function (index, element) {
        $(this).on('focus', function () {
            $('#' + $(this).attr('id')).removeClass('alert-danger');
        });
    });
    $('#Div_Contenido select').each(function (index, element) {
        $(this).on('focus', function () {
            $('#' + $(this).attr('id')).removeClass('alert-danger');
        });
    });
}

/*------------------------------------------------------------------------------
<Descripcion>   funcion para quitar el estilo de error
<Creo>          Juan Alberto Hernandez Negrete
<Fecha_Creo>    29-Mayo-2017
------------------------------------------------------------------------------*/
function Remove_Class_Error() {
    $('#Div_Contenido input[type=text]').each(function (index, element) {
        $('#' + $(this).attr('id')).removeClass('alert-danger');
    });
    $('#Div_Contenido select').each(function (index, element) {
        $('#' + $(this).attr('id')).removeClass('alert-danger');
    });
}

/*------------------------------------------------------------------------------
<Descripcion>   funcion de para mostrar los mensajes de la validacion
<Creo>          Juan Alberto Hernandez Negrete
<Fecha_Creo>    29-Mayo-2017
------------------------------------------------------------------------------*/
function Mensaje_Validacion(Mensaje) {
    var Header_Message = '<i class="fa fa-exclamation-triangle fa-2x"></i><span>Observaciones</span><br />';

    if (Mensaje == null || Mensaje == '' || Mensaje == undefined) {
        $('#Lbl_Msg_Error').html('');
        $('#Sumary_Error').css('display', 'none');
    } else {
        $('#Lbl_Msg_Error').html(Header_Message + Mensaje);
        $('#Sumary_Error').css('display', 'block');
    }
}

/*------------------------------------------------------------------------------
<Descripcion>   funcion de para obtener la informacion del catalogo
<Creo>          Juan Alberto Hernandez Negrete
<Fecha_Creo>    29-Mayo-2017
------------------------------------------------------------------------------*/
function Consultar_Informacion() {
    var Registros = "{}";
    var Respuesta_ = null;
    try {

        $.ajax({
            url: 'controllers/Ctrl_Apl_Roles.asmx/Consultar_Registros',
            data: "{'Parametros':'" + Parametros() + "'}",
            method: 'POST',
            cache: false,
            async: false,
            contentType: "application/json; charset=utf-8",
            dataType: 'json',
            success: function (Respuesta) {
                if (Respuesta.d != undefined && Respuesta.d != null) {
                    var Respuesta_ = eval("(" + Respuesta.d + ")");
                    if (Respuesta_.Estatus) {
                        Registros = Respuesta_.Registros;
                        Llenar_Grid(Registros);
                    }
                }
            }
        });
    } catch (e) {
        Mostrar_Mensaje('Informe Técnico', e);
    }
}

/*------------------------------------------------------------------------------
<Descripcion>   funcion de para llenar el grid
<Creo>          Juan Alberto Hernandez Negrete
<Fecha_Creo>    29-Mayo-2017
------------------------------------------------------------------------------*/
function Llenar_Grid(Registros) {
    var Height = $(window).height() / 2.2;

    try {
        $('#Tbl_Registros').bootstrapTable('destroy');
        $('#Tbl_Registros').bootstrapTable({
            data: JSON.parse(Registros),
            method: 'POST',
            height: Height,
            striped: true,
            showColumns: false,
            showRefresh: false,
            minimumCountColumns: 2,
            clickToSelect: false,
            search: true,
            columns: [
                { field: 'Nombre', title: 'Nombre', align: 'left', valign: 'center', sortable: true },
                { field: 'Estatus', title: 'Estatus', align: 'left', valign: 'center', sortable: true },
                { field: 'Descripcion', title: 'Descripción', align: 'left', valign: 'center', sortable: true },
                { field: 'Rol_ID', visible: false }
            ],
            onClickRow: function (row) {
                Limpiar_Controles();
                $('#Hf_Rol_ID').val(row.Rol_ID);
                $('#Txt_Nombre').val(row.Nombre);
                $('#Txt_Descripcion').val(row.Descripcion);
                $('#Cmb_Estatus').val(row.Estatus);

                Consultar_Accesos();
                Accesos_Menu();

            }
        });

        // sometimes footer render error.
        setTimeout(function () {
            $('#Tbl_Registros').bootstrapTable('resetView');
        }, 200);

        $(window).resize(function () {
            var Height = $(window).height() / 2.2;
            $('#Tbl_Registros').bootstrapTable('resetView', {
                height: Height
            });
        });
    } catch (e) {
        _mostrar_mensaje('Informe Técnico', e);
    }
}

/*------------------------------------------------------------------------------
<Descripcion>   funcion de para obtener la informacion de los menus
<Creo>          Juan Alberto Hernandez Negrete
<Fecha_Creo>    30-Mayo-2017
------------------------------------------------------------------------------*/
function Consultar_Menus() {
    var Registros = "{}";
    var Respuesta_ = null;
    try {

        $.ajax({
            url: 'controllers/Ctrl_Apl_Roles.asmx/Consultar_Menus',
            method: 'POST',
            cache: false,
            async: false,
            contentType: "application/json; charset=utf-8",
            dataType: 'json',
            success: function (Respuesta) {
                if (Respuesta.d != undefined && Respuesta.d != null) {
                    var Respuesta_ = eval("(" + Respuesta.d + ")");
                    if (Respuesta_.Estatus) {
                        Registros = Respuesta_.Registros;
                        Llenar_Grid_Menus(Registros);
                    }
                }
            }
        });
    } catch (e) {
        Mostrar_Mensaje('Informe Técnico', e);
    }
}

/*------------------------------------------------------------------------------
<Descripcion>   funcion de para llenar el grid de los menus
<Creo>          Juan Alberto Hernandez Negrete
<Fecha_Creo>    30-Mayo-2017
------------------------------------------------------------------------------*/
function Llenar_Grid_Menus(Registros) {
    var Height = $(window).height() / 2.2;

    try {
        $('#Tbl_Accesos').bootstrapTable('destroy');
        $('#Tbl_Accesos').bootstrapTable({
            data: JSON.parse(Registros),
            method: 'POST',
            height: Height,
            striped: true,
            showColumns: false,
            showRefresh: false,
            minimumCountColumns: 2,
            clickToSelect: true,
            search: false,
            detailView: true,
            detailFormatter: "Detalles_Grid_Menus",
            checkboxHeader:false,
            columns: [
                { field: 'Nombre_Mostrar', title: 'Menú', align: 'left', valign: 'center', sortable: true },
                //{ field: 'Sistema', title: 'Sistema', align: 'left', valign: 'center', sortable: true },
                {
                    field: '_Menu_ID', width:'30', title: 'Habilitar', align: 'center', valign: 'center', sortable: false, formatter: function (value, row, index) {
                        return '<input type="checkbox" id="P' + value + '" class="' + value + '" disabled="disabled"  onclick="Check_Click(\'P' + value + '\',' + value + ', \'' + row.Sistema + '\')" />';
                    }
                },
                { field: 'Submenus', visible: false },
                { field: 'Menu_ID', visible: false }
            ],
            onExpandRow: function (index, row, $detail) {
                Habilitar_Checkbox();
                //Accesos_Menu();
            },
            onPostBody: function () {
                Habilitar_Checkbox();
                Accesos_Menu();
            }
        });

        // sometimes footer render error.
        setTimeout(function () {
            $('#Tbl_Accesos').bootstrapTable('resetView');
            _set_location_toolbar();
        }, 200);

        $(window).resize(function () {
            var Height = $(window).height() / 2.2;
            $('#Tbl_Accesos').bootstrapTable('resetView', {
                height: Height
            });
        });

        $('#Tbl_Accesos').bootstrapTable('expandAllRows');
    } catch (e) {
        _mostrar_mensaje('Informe Técnico', e);
    }
}

/*------------------------------------------------------------------------------
<Descripcion>   funcion del detalle del grid de menus
<Creo>          Juan Alberto Hernandez Negrete
<Fecha_Creo>    30-Mayo-2017
------------------------------------------------------------------------------*/
function Detalles_Grid_Menus(index, row) {
    var html = [];
    var det = '';

    try {
        Datos = JSON.parse(row.Submenus);

        if (Datos != null) {
            var largo = 0;
            largo = $(window).width() / 3;
            largo = largo * 2.5;
            //creamos el encabezado
            html.push('<table id="accesos_' + row.Menu_ID + '" class="table table-responsive">');
            html.push('<tr>');
            html.push('<td class="Head_Detalle" style="width: 400px;"><b>Nombre</b></td>');
            html.push('<td class="Head_Detalle"><b>Habilitar</b></td>');
            html.push('<td class="Head_Detalle"><b>Alta</b></td>');
            html.push('<td class="Head_Detalle"><b>Cambio</b></td>');
            html.push('<td class="Head_Detalle"><b>Eliminar</b></td>');
            html.push('<td class="Head_Detalle"><b>Consultar</b></td>');

            html.push('</tr>');

            $.each(Datos, function (key, value) {
                html.push('<tr>');
                html.push('<td class="Row_Detalle">' + value.Nombre_Mostrar + '</td>');
                html.push('<td class="Row_Detalle"><input type="checkbox" id="H' + value.Menu_ID + '" class="' + value.Parent_ID + '" onclick="Check_Click(\'H' + value.Menu_ID + '\',' + value.Parent_ID + ', \'' + row.Sistema + '\')" /></td>');
                html.push('<td class="Row_Detalle"><input type="checkbox" id="A' + value.Menu_ID + '" class="' + value.Parent_ID + '" onclick="Check_Click(\'A' + value.Menu_ID + '\',' + value.Parent_ID + ', \'' + row.Sistema + '\')" /></td>');
                html.push('<td class="Row_Detalle"><input type="checkbox" id="M' + value.Menu_ID + '" class="' + value.Parent_ID + '" onclick="Check_Click(\'M' + value.Menu_ID + '\',' + value.Parent_ID + ', \'' + row.Sistema + '\')" /></td>');
                html.push('<td class="Row_Detalle"><input type="checkbox" id="E' + value.Menu_ID + '" class="' + value.Parent_ID + '" onclick="Check_Click(\'E' + value.Menu_ID + '\',' + value.Parent_ID + ', \'' + row.Sistema + '\')" /></td>');
                html.push('<td class="Row_Detalle"><input type="checkbox" id="N' + value.Menu_ID + '" class="' + value.Parent_ID + '" onclick="Check_Click(\'N' + value.Menu_ID + '\',' + value.Parent_ID + ', \'' + row.Sistema + '\')" /></td>');

                html.push('</tr>');
            });

            html.push('</table>');
        }

    } catch (e) {
        Mostrar_Mensaje("Informe técnico", e);
    }
    return html.join('');
}

/*------------------------------------------------------------------------------
<Descripcion>   funcion de para habilitar o no los checkbox
<Creo>          Juan Alberto Hernandez Negrete
<Fecha_Creo>    30-Mayo-2017
------------------------------------------------------------------------------*/
function Habilitar_Checkbox() {
    if ($Accion == 'Inicio') {
        $('input[type=checkbox]').each(function () {
            $(this)[0].disabled = true;
        });
    }
    else if ($Accion == 'Nuevo' || $Accion == 'Modificar') {
        $('input[type=checkbox]').each(function () {
            $(this)[0].disabled = false;
        });
    }
}

/*------------------------------------------------------------------------------
<Descripcion>   funcion de para habilitar o no los checkbox
<Creo>          Juan Alberto Hernandez Negrete
<Fecha_Creo>    30-Mayo-2017
------------------------------------------------------------------------------*/
function Accesos_Menu() {
    if ($Accesos != null && $Accesos != undefined)
    {
        $.each($Accesos, function (key, Accesos_) {
            if (Accesos_.Habilitado == 'S') {
                $("#P" + Accesos_.Menu_ID).attr("checked", true);
                $("#H" + Accesos_.Menu_ID).attr("checked", true);
            }

            if (Accesos_.Alta == 'S')
                $("#A" + Accesos_.Menu_ID).attr("checked", true);

            if (Accesos_.Cambio == 'S')
                $("#M" + Accesos_.Menu_ID).attr("checked", true);

            if (Accesos_.Eliminar == 'S')
                $("#E" + Accesos_.Menu_ID).attr("checked", true);

            if (Accesos_.Cancelar == 'S')
                $("#X" + Accesos_.Menu_ID).attr("checked", true);

            if (Accesos_.Cerrar == 'S')
                $("#C" + Accesos_.Menu_ID).attr("checked", true);

            if (Accesos_.Reimprimir == 'S')
                $("#R" + Accesos_.Menu_ID).attr("checked", true);

            if (Accesos_.Consultar == 'S')
                $("#N" + Accesos_.Menu_ID).attr("checked", true);
        });
    }
}

/*------------------------------------------------------------------------------
<Descripcion>   funcion de para obtener los accesos del rol
<Creo>          Juan Alberto Hernandez Negrete
<Fecha_Creo>    30-Mayo-2017
------------------------------------------------------------------------------*/
function Consultar_Accesos() {
    var Obj_Param = null;
    var Respuesta_ = null;

    try {
        if ($('#Hf_Rol_ID').val() != null && $('#Hf_Rol_ID').val() != undefined && $('#Hf_Rol_ID').val() != "") {
            Obj_Param = new Object();
            Obj_Param.Rol_ID = parseInt($('#Hf_Rol_ID').val(), 10);

            $.ajax({
                url: 'controllers/Ctrl_Apl_Roles.asmx/Consultar_Accesos',
                data: "{'Parametros':'" + JSON.stringify(Obj_Param) + "'}",
                method: 'POST',
                cache: false,
                async: false,
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                success: function (Respuesta) {
                    if (Respuesta.d != undefined && Respuesta.d != null) {
                        var Respuesta_ = eval("(" + Respuesta.d + ")");
                        if (Respuesta_.Estatus) {
                            $Accesos = $.parseJSON(Respuesta_.Registros);
                        }
                    }
                }
            });
        }
    } catch (e) {
        Mostrar_Mensaje('Informe Técnico', e);
    }
}

/*------------------------------------------------------------------------------
<Descripcion>   funcion de para validar la información
<Creo>          Juan Alberto Hernandez Negrete
<Fecha_Creo>    29-Mayo-2017
------------------------------------------------------------------------------*/
function Validacion() {
    var Obj_ = new Object();
    try {
        Obj_.Estatus = true;
        Obj_.Mensaje = '';

        if ($('#Txt_Nombre').val() == '' || $('#Txt_Nombre').val() == undefined || $('#Txt_Nombre').val() == null) {
            Add_Class_Error('#Txt_Nombre');
            Obj_.Estatus = false;
            Obj_.Mensaje += '&nbsp;<i class="fa fa-angle-double-right"></i>&nbsp;El nombre es un dato requerido.<br />';
        }

        if ($('#Cmb_Estatus').val() == '' || $('#Cmb_Estatus').val() == undefined || $('#Cmb_Estatus').val() == null) {
            Add_Class_Error('#Cmb_Estatus');
            Obj_.Estatus = false;
            Obj_.Mensaje += '&nbsp;<i class="fa fa-angle-double-right"></i>&nbsp;El estatus es un dato requerido.<br />';
        }
    } catch (e) {
        Mostrar_Mensaje('Informe técnico', e);
    }
    return Obj_;
}

/*------------------------------------------------------------------------------
<Descripcion>   funcion de para obtener los parametros
<Creo>          Juan Alberto Hernandez Negrete
<Fecha_Creo>    29-Mayo-2017
------------------------------------------------------------------------------*/
function Parametros() {
    var Obj_Param = null;
    try {
        Obj_Param = new Object();
        Obj_Param.Rol_ID = parseInt($('#Hf_Rol_ID').val(), 10);
        Obj_Param.Nombre = $('#Txt_Nombre').val();
        Obj_Param.Descripcion = $('#Txt_Descripcion').val();
        Obj_Param.Estatus = $('#Cmb_Estatus').val();
    } catch (e) {
        Mostrar_Mensaje('Informe técnico', e);
    }
    return JSON.stringify(Obj_Param);
}

/*------------------------------------------------------------------------------
<Descripcion>   funcion de para obtener los accesos del rol
<Creo>          Juan Alberto Hernandez Negrete
<Fecha_Creo>    30-Mayo-2017
------------------------------------------------------------------------------*/
function Obtener_Accessos_() {
    var Obj_Param = null;
    var Arr = new Array();
    var Inicial = '';
    var Menu_ID = '';
    var Cons = 0;

    try {
        $('input[type=checkbox]').each(function () {
            if ($('#' + $(this)[0].id)[0] !== null && $('#' + $(this)[0].id)[0] !== undefined) {
                if ($('#' + $(this)[0].id)[0].checked) {
                    Obj_Param = new Object();
                    Inicial = $(this)[0].id.substring(0, 1);
                    Menu_ID = $(this)[0].id.substring(1);
                    Obj_Param.Menu_ID = parseInt(Menu_ID, 10);

                    if (Inicial == "H")
                        Obj_Param.Habilitado = 'S';
                    else if (Inicial == "P")
                        Obj_Param.Habilitado = 'S';
                    else if (Inicial == "A")
                        Obj_Param.Alta = 'S';
                    else if (Inicial == "M")
                        Obj_Param.Cambio = 'S';
                    else if (Inicial == "E")
                        Obj_Param.Eliminar = 'S';
                    else if (Inicial == "X")
                        Obj_Param.Cancelar = 'S';
                    else if (Inicial == "C")
                        Obj_Param.Cerrar = 'S';
                    else if (Inicial == "R")
                        Obj_Param.Reimprimir = 'S';
                    else if (Inicial == "N")
                        Obj_Param.Consultar = 'S';

                    Arr[Cons] = Obj_Param;
                    Cons++;
                }
            }
            //else
            //{
            //    if (Inicial == "H")
            //        Obj_Param.Habilitado = 'N';
            //    else if (Inicial == "P")
            //        Obj_Param.Habilitado = 'N';
            //    else if (Inicial == "A")
            //        Obj_Param.Alta = 'N';
            //    else if (Inicial == "M")
            //        Obj_Param.Cambio = 'N';
            //    else if (Inicial == "E")
            //        Obj_Param.Eliminar = 'N';
            //    else if (Inicial == "X")
            //        Obj_Param.Cancelar = 'N';
            //    else if (Inicial == "C")
            //        Obj_Param.Cerrar = 'N';
            //    else if (Inicial == "R")
            //        Obj_Param.Reimprimir = 'N';
            //    else if (Inicial == "N")
            //        Obj_Param.Consultar = 'N';
            //}
        });
    } catch (e) {
        Mostrar_Mensaje('Informe técnico', e);
    }
    return JSON.stringify(Arr);
}

/*-------------------------------------------------------------- Eventos --------------------------------------------------------------*/
/*------------------------------------------------------------------------------
<Descripcion>   funcion de para el alta del catalogo
<Creo>          Juan Alberto Hernandez Negrete
<Fecha_Creo>    29-Mayo-2017
------------------------------------------------------------------------------*/
function Btn_Guardar_Alta_Click() {
    var Respuesta_ = null;

    try {
        NProgress.start();

        $.ajax({
            url: 'controllers/Ctrl_Apl_Roles.asmx/Alta',
            data: "{'Parametros':'" + Parametros() + "', 'Accesos':'" + Obtener_Accessos_() + "'}",
            type: 'POST',
            async: true,
            cache: false,
            dataType: 'json',
            contentType: 'application/json; charset=UTF-8',
            success: function (Resultado) {
                Respuesta_ = eval("(" + Resultado.d + ")");
                if (Respuesta_.Estatus) {
                    Inicializar_Pagina();
                    NProgress.done();
                    Mostrar_Mensaje("Correcto", Respuesta_.Mensaje);
                }
                else
                    Mostrar_Mensaje("Advertencia", Respuesta_.Mensaje);
            }
        });

    } catch (e) {
        Mostrar_Mensaje('Informe técnico', e);
    }
}

/*------------------------------------------------------------------------------
<Descripcion>   funcion de para la actualizacion del registro del catalogo
<Creo>          Juan Alberto Hernandez Negrete
<Fecha_Creo>    29-Mayo-2017
------------------------------------------------------------------------------*/
function Btn_Guardar_Actualizacion_Click() {
    var Respuesta_ = null;

    try {
        NProgress.start();

        $.ajax({
            url: 'controllers/Ctrl_Apl_Roles.asmx/Actualizar',
            data: "{'Parametros':'" + Parametros() + "', 'Accesos':'" + Obtener_Accessos_() + "'}",
            type: 'POST',
            async: true,
            cache: false,
            dataType: 'json',
            contentType: 'application/json; charset=UTF-8',
            success: function (Resultado) {
                Respuesta_ = eval("(" + Resultado.d + ")");
                if (Respuesta_.Estatus) {
                    $('[href="#Tab_Rol"]').tab('show');
                    Inicializar_Pagina();
                    NProgress.done();
                    Mostrar_Mensaje("Correcto", Respuesta_.Mensaje);
                }
                else
                    Mostrar_Mensaje("Advertencia", Respuesta_.Mensaje);
            }
        });

    } catch (e) {
        Mostrar_Mensaje('Informe técnico', e);
    }
}

/*------------------------------------------------------------------------------
<Descripcion>   funcion de para eliminar un registro
<Creo>          Juan Alberto Hernandez Negrete
<Fecha_Creo>    29-Mayo-2017
------------------------------------------------------------------------------*/
function Btn_Eliminar_Click() {
    var Respuesta_ = null;

    try {
        NProgress.start();

        $.ajax({
            url: 'controllers/Ctrl_Apl_Roles.asmx/Eliminar',
            data: "{'Parametros':'" + Parametros() + "'}",
            type: 'POST',
            async: true,
            cache: false,
            dataType: 'json',
            contentType: 'application/json; charset=UTF-8',
            success: function (Resultado) {
                Respuesta_ = eval("(" + Resultado.d + ")");
                if (Respuesta_.Estatus) {
                    Inicializar_Pagina();
                    NProgress.done();
                    Mostrar_Mensaje("Correcto", Respuesta_.Mensaje);
                }
                else
                    Mostrar_Mensaje("Advertencia", Respuesta_.Mensaje);
            }
        });

    } catch (e) {
        Mostrar_Mensaje('Informe técnico', e);
    }
}

/*------------------------------------------------------------------------------
<Descripcion>   funcion del evento de los checkbox
<Creo>          Juan Alberto Hernandez Negrete
<Fecha_Creo>    30-Mayo-2017
------------------------------------------------------------------------------*/
function Check_Click(Id, Parent, Sistema) {
    var Inicial = Id.substring(0, 1);
    var Menu_ID = Id.substring(1);
    try {

        if ($('#' + Id)[0].checked) {
            if (Inicial == "H") {
                $("#A" + Menu_ID).attr("checked", true);
                $("#M" + Menu_ID).attr("checked", true);
                $("#E" + Menu_ID).attr("checked", true);
                $("#N" + Menu_ID).attr("checked", true);
                if (Sistema == 'Escritorio') {
                    $("#X" + Menu_ID).attr("checked", true);
                    $("#C" + Menu_ID).attr("checked", true);
                    $("#R" + Menu_ID).attr("checked", true);
                }
            }
            else if (Inicial == "P") {
                $("." + Menu_ID).attr("checked", true);
            }
            else {
                $("#H" + Menu_ID).attr("checked", true);
            }
            $("#P" + Parent).attr("checked", true);
        }
        else {//acciones si el check es deseleccionado
            if (Inicial == "H") {
                $("#A" + Menu_ID).attr("checked", false);
                $("#M" + Menu_ID).attr("checked", false);
                $("#E" + Menu_ID).attr("checked", false);
                $("#N" + Menu_ID).attr("checked", false);
                if (Sistema == 'Escritorio') {
                    $("#X" + Menu_ID).attr("checked", false);
                    $("#C" + Menu_ID).attr("checked", false);
                    $("#R" + Menu_ID).attr("checked", false);
                }
            }
            else if (Inicial == "P") {
                $("." + Menu_ID).attr("checked", false);
            }
            else {
                $("#H" + Menu_ID).attr("checked", false);
                if ($('#A' + Menu_ID)[0].checked || $('#M' + Menu_ID)[0].checked || $('#E' + Menu_ID)[0].checked || $('#N' + Menu_ID)[0].checked)
                    $("#H" + Menu_ID).attr("checked", true);

                if (Sistema == 'Escritorio') {
                    if ($('#X' + Menu_ID)[0].checked || $('#C' + Menu_ID)[0].checked || $('#R' + Menu_ID)[0].checked)
                        $("#H" + Menu_ID).attr("checked", true);
                }
            }

            $("#P" + Parent).attr("checked", false);
            $("." + Parent).each(function () {
                if ($(this)[0].checked) {
                    $("#P" + Parent).attr("checked", true);
                    return
                }
            });
        }
    } catch (e) {
        Mostrar_Mensaje("Informe técnico", e);
    }
}

function _set_location_toolbar() {
    $('#toolbar').parent().removeClass("pull-left");
    $('#toolbar').parent().addClass("pull-right");
}