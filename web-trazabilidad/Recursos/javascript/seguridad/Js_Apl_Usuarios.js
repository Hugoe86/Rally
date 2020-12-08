var closeModal = true;
var estatusActivo = '';
var tipo = '';
$(document).on('ready', function () {
    _inicializar_pagina();
});

function _inicializar_pagina() {
    try {
        _filtroEstatus();
        _habilitar_controles('Inicio');
        _limpiar_controles();
        _cargar_tabla();

        _search();
        _modal();
        //$('#chk_es_empleado').prop('checked', true);
        _modal_roles_usuario();
        //_load_cmb_empleados();
        _load_estatus();
        _load_tipo_usuario();
        _load_rol();
        _load_sucursal();
        _eventos_textbox();
        _eventos();
        _enter_keypress_modal();
        _set_location_toolbar();
        _cargar_tbl_usuarios_roles();
    } catch (e) {
        _mostrar_mensaje('Informe Técnico', e);
    }
}

function _estado_inicial() {
    try {
        _habilitar_controles('Inicio');
        _limpiar_controles();
        $('#tbl_usuarios').bootstrapTable('refresh', 'controllers/Usuarios_Controller.asmx/Consultar_Usuarios_Por_Filtros');
        _set_location_toolbar();
    } catch (e) {
        _mostrar_mensaje('Informe Técnico', e);
    }
}

function _habilitar_controles(opcion) {
    var Estatus = false;

    switch (opcion) {
        case "Nuevo":
            Estatus = true;
            $('cmb_estatus').attr({ disabled: Estatus })
            break;
        case "Modificar":
            Estatus = true;
            $('cmb_estatus').attr({ disabled: !Estatus })
            break;
        case "Inicio":
            break;
    }


    $('#txt_usuario').attr({ disabled: !Estatus });
    $('#txt_password').attr({ disabled: !Estatus });
    $('#txt_email').attr({ disabled: !Estatus });
    $('#cmb_tipo_usuario').attr({ disabled: !Estatus })
    $('#txt_nombre_usuario').attr({ disabled: !Estatus });
    //$('#chk_es_empleado').attr({ disabled: !Estatus });
    //$('#cmb_empleado').attr({ disabled: !Estatus });
    //$('#cmb_rol').attr({ disabled: !Estatus });

}

function _limpiar_controles() {
    $('input[type=text]').each(function () { $(this).val(''); });
    $('select').each(function () { $(this).val(estatusActivo); });
    $('#cmb_estatusfiltro').val('');
    $('textarea').each(function () { $(this).val(''); });
    $('#txt_password').val('');
    $('#txt_usuario_id').val('');
    $('#txt_rel_id').val('');
    //$('#cmb_rol').val('');
    $('#cmb_tipo_usuario').val('');
    $('#txt_nombre_usuario').val('');
    _validation_sumary(null);
    _clear_all_class_error();
}

function _eventos() {
    try {
        $('#modal_datos').on('hidden.bs.modal', function () {
            if (!closeModal)
                $(this).modal('show');
        });
        $('#btn_nuevo').click(function (e) {
            _limpiar_controles();
            _habilitar_controles('Nuevo');
            _launch_modal('<i class="fa fa-floppy-o" style="font-size: 25px;"></i>&nbsp;&nbsp;Alta de registro');
        });
        $('.cancelar').each(function (index, element) {
            $(this).on('click', function (e) {
                e.preventDefault();
                _estado_inicial();
            });
        });
        $('#btn_salir').on('click', function (e) { e.preventDefault(); window.location.href = '../Paginas_Generales/Frm_Apl_Principal.aspx'; });
        $('#btn_busqueda').on('click', function (e) {
            e.preventDefault();
            _search();
        });
        $('#modal_datos input[type=text]').each(function (index, element) {
            $(this).on('focus', function () {
                _remove_class_error('#' + $(this).attr('id'));
            });
        });
        $('#btn_exportar').click(function (e) {
            var $table_pagination = $('#tbl_usuarios');
            $table_pagination.bootstrapTable('togglePagination');
            $table_pagination.tableExport({
                type: 'excel',
                worksheetName: 'Usuarios',
                fileName: 'Usuarios'
            });
            $table_pagination.bootstrapTable('togglePagination');
        });
        //$('#chk_es_empleado').change(function () {
        //    if ($(this).is(":checked")) {
        //        $('#contenedor_combo_empleado').show();
        //    }
        //    else {
        //        $('#contenedor_combo_empleado').hide();
        //        $("#cmb_empleado").select2("trigger", "select", {
        //            data: { id: '' }
        //        });
        //    }
        //});
    } catch (e) {
        _mostrar_mensaje('Informe Técnico', e);
    }
}

function _eventos_textbox() {
    $('#txt_usuario').on('blur', function () {
        $(this).val($(this).val().match(/^[0-9a-zA-Z\u0020]+$/) ? $(this).val() : $(this).val().replace(/\W+/g, ''));
    });

    $('#txt_email').on('blur', function () {
        $(this).val(this.value.match(/^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4})$/) ? $(this).val() : _add_class_error('#txt_email'));
    });

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

function _cargar_tabla() {

    try {
        $('#tbl_usuarios').bootstrapTable('destroy');
        $('#tbl_usuarios').bootstrapTable({
            cache: false,
            width: 900,
            height: 400,
            striped: true,
            pagination: true,
            pageSize: 10,
            pageList: [10, 25, 50, 100, 200],
            smartDysplay: false,
            search: true,
            showColumns: false,
            showRefresh: false,
            minimumCountColumns: 2,
            clickToSelect: true,
            columns: [
                { field: 'Usuario_ID', title: '', width: 0, align: 'center', valign: 'bottom', sortable: true, visible: false },
                { field: 'Usuario', title: 'Usuario', width: 100, align: 'left', valign: 'bottom', sortable: true, clickToSelect: false },
                { field: 'Nombre', title: 'Nombre usuario', width: 100, align: 'left', valign: 'bottom', sortable: true, clickToSelect: false },
                { field: 'Password', title: 'Password', width: 100, align: 'left', valign: 'bottom', sortable: true, visible: false },
                { field: 'Rol_ID', title: 'Rol_ID', width: 100, align: 'letf', valign: 'bottom', sortable: true, visible: false },
                { field: 'Email', title: 'Email', width: 200, align: 'left', valign: 'bottom', sortable: true, clickToSelect: false },
                { field: 'Empresa_ID', title: '', width: 200, align: 'left', valign: 'bottom', sortable: true, visible: false },
                { field: 'Estatus_ID', title: '', width: 200, align: 'left', valign: 'bottom', sortable: true, visible: false },
                { field: 'Tipo_Usuario_ID', title: '', width: 200, align: 'left', valign: 'bottom', sortable: true, visible: false },
                { field: 'Rel_Usuarios_Rol_ID', title: '', width: 200, align: 'left', valign: 'bottom', sortable: true, visible: false },
                {
                    field: 'Usuario_ID',
                    title: '',
                    align: 'center',
                    valign: 'bottom',
                    width: 60,
                    clickToSelect: false,
                    formatter: function (value, row) {
                        return '<div> ' +
                                '<a class="remove ml10 edit" id="' + row.Usuario_ID + '" href="javascript:void(0)" data-usuario=\'' + JSON.stringify(row) + '\' onclick="btn_editar_click(this);" title="Editar"><i class="glyphicon glyphicon-edit"></i></button>' +
                                '&nbsp;&nbsp;<a class="remove ml10 delete" id="' + row.Usuario_ID + '" href="javascript:void(0)" data-usuario=\'' + JSON.stringify(row) + '\' onclick="btn_eliminar_click(this);" title="Eliminar"><i class="glyphicon glyphicon-trash"></i></a>' +
                                '&nbsp;&nbsp;<a class="remove ml10 edit" id="' + row.Usuario_ID + '" href="javascript:void(0)" data-usuario=\'' + JSON.stringify(row) + '\' onclick="btn_add_rol_click(this);" title="Agregar Roles"><i class="glyphicon glyphicon-plus"></i></a>' +
                               '</div>';
                    }
                }
            ]
        });
    } catch (e) {
        _mostrar_mensaje('Informe Técnico', e);
    }
}

function _cargar_tbl_usuarios_roles() {

    try {
        $('#tbl_usuarios_roles').bootstrapTable('destroy');
        $('#tbl_usuarios_roles').bootstrapTable({
            cache: false,

            striped: true,
            pagination: true,
            pageSize: 10,
            pageList: [10, 25, 50, 100, 200],
            search: true,
            showColumns: false,
            showRefresh: false,
            minimumCountColumns: 1,
            columns: [
                { field: 'Usuario_ID', title: '', align: 'center', valign: 'bottom', sortable: true, visible: false },
                { field: 'Sucursal', title: 'Sucursal', align: 'left', valign: 'bottom', sortable: true },
                { field: 'Usuario', title: 'Usuario', align: 'left', valign: 'bottom', sortable: true },
                { field: 'Rol', title: 'Rol', align: 'letf', valign: 'bottom', sortable: true },
                { field: 'Tipo', title: 'Tipo', align: 'left', valign: 'bottom', sortable: true },
                {
                    field: 'Usuario_ID',
                    title: '',
                    align: 'center',
                    valign: 'bottom',
                    width: 60,
                    clickToSelect: false,
                    formatter: function (value, row) {
                        return '<div> ' +
                                '&nbsp;&nbsp;<a class="remove ml10 delete" id="' + row.Usuario_ID + '" href="javascript:void(0)" data-usuario=\'' + JSON.stringify(row) + '\' onclick="btn_eliminar_rol_click(this);" title="Eliminar"><i class="glyphicon glyphicon-trash"></i></a>' +
                               '</div>';
                    }
                }
            ]
        });
    } catch (e) {
        _mostrar_mensaje('Informe Técnico', e);
    }
}

function _alta_usuarios() {
    var usuarios = null;
    var rol = null;
    var isComplete = false;

    try {

        usuarios = new Object();

        usuarios.Usuario = $('#txt_usuario').val();
        usuarios.Estatus_ID = parseInt($('#cmb_estatus').val());
        usuarios.Tipo_Usuario_ID = parseInt($('#cmb_tipo_usuario').val());
        usuarios.Password = $('#txt_password').val();
        usuarios.Email = $('#txt_email').val();
        usuarios.Nombre = $('#txt_nombre_usuario').val();
        //usuarios.Empleado_ID = $('#cmb_empleado').val();
        //usuarios.Rol_ID = parseInt ($('#cmb_rol').val());

        var $data = JSON.stringify({ 'jsonObject': JSON.stringify(usuarios) });

        $.ajax({
            type: 'POST',
            url: 'controllers/Usuarios_Controller.asmx/Alta',
            data: $data,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            cache: false,
            success: function (result) {
                var Resultado = JSON.parse(result.d);
                if (Resultado != null && Resultado != undefined && Resultado != '') {
                    if (Resultado.Estatus == 'success') {
                        _search();
                        isComplete = true;
                    } else if (Resultado.Estatus == 'error') {
                        _validation_sumary(Resultado);
                    }
                } else {
                    _validation_sumary(Resultado);
                }
            }
        });
    } catch (e) {
        _mostrar_mensaje('Informe Tecnico', e);
    }
    return isComplete;
}

function _modificar_usuarios() {
    var usuarios = null;

    var isComplete = false;

    try {
        usuarios = new Object();
        usuarios.Usuario_ID = parseInt($('#txt_usuario_id').val());
        usuarios.Rel_Usuarios_Rol_ID = parseInt($('#txt_rel_id').val());
        usuarios.Usuario = $('#txt_usuario').val();
        usuarios.Password = $('#txt_password').val();
        usuarios.Email = $('#txt_email').val();
        usuarios.Estatus_ID = parseInt($('#cmb_estatus').val());
        usuarios.Tipo_Usuario_ID = parseInt($('#cmb_tipo_usuario').val());
        usuarios.Nombre = $('#txt_nombre_usuario').val();
        //usuarios.Empleado_ID = $('#cmb_empleado').val();
        //usuarios.Rol_ID = parseInt($('#cmb_rol').val());


        var $data = JSON.stringify({ 'jsonObject': JSON.stringify(usuarios) });

        $.ajax({
            type: 'POST',
            url: 'controllers/Usuarios_Controller.asmx/Actualizar',
            data: $data,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            cache: false,
            success: function (result) {
                var Resultado = JSON.parse(result.d);
                if (Resultado != null && Resultado != undefined && Resultado != '') {
                    if (Resultado.Estatus == 'success') {
                        _search();
                        isComplete = true;
                    } else if (Resultado.Estatus == 'error') {
                        _validation_sumary(Resultado);
                    }
                } else {
                    _validation_sumary(Resultado);
                }
            }
        });

    } catch (e) {
        _mostrar_mensaje('Informe Tecnico', e);
    }
    return isComplete;
}

function _eliminar_usuarios(usuario_id) {
    var usuarios = null;


    try {
        usuarios = new Object();
        usuarios.Usuario_ID = parseInt(usuario_id);
        usuarios.Rel_Usuarios_Rol_ID = parseInt(usuario_id);

        var $data = JSON.stringify({ 'jsonObject': JSON.stringify(usuarios) });


        $.ajax({
            type: 'POST',
            url: 'controllers/Usuarios_Controller.asmx/Eliminar',
            data: $data,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            cache: false,
            success: function (result) {
                var Resultado = JSON.parse(result.d);
                if (Resultado != null && Resultado != undefined && Resultado != '') {
                    if (Resultado.Estatus == 'success') {
                        _search();
                    } else if (Resultado.Estatus == 'error') {
                        _mostrar_mensaje(Resultado.Titulo, Resultado.Mensaje);
                    }
                } else { _mostrar_mensaje(Resultado.Titulo, Resultado.Mensaje); }
            }
        });

    } catch (e) {
        _mostrar_mensaje('Informe Técnico', e);
    }
}

function btn_eliminar_rol_click(Usuario) {
    var usuarios = null;

    try {
        var row = $(Usuario).data('usuario');
        usuarios = new Object();

        usuarios.Rel_Usuario_Rol_ID = parseInt(row.Rel_Usuario_Rol_ID);

        var $data = JSON.stringify({ 'jsonObject': JSON.stringify(usuarios) });

        $.ajax({
            type: 'POST',
            url: 'controllers/Usuarios_Controller.asmx/Eliminar_Relacion_Usuario_Rol',
            data: $data,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            cache: false,
            success: function (result) {
                var Resultado = JSON.parse(result.d);
                if (Resultado != null && Resultado != undefined && Resultado != '') {
                    if (Resultado.Estatus == 'success') {
                        _consultar_roles_usuarios();
                    } else if (Resultado.Estatus == 'error') {
                        _mostrar_mensaje(Resultado.Titulo, Resultado.Mensaje);
                    }
                } else { _mostrar_mensaje(Resultado.Titulo, Resultado.Mensaje); }
            }
        });

    } catch (e) {
        _mostrar_mensaje('Informe Técnico', e);
    }
}

function _modal() {
    var tags = '';
    try {
        tags += '<div class="modal fade" id="modal_datos" name="modal_datos" role="dialog" aria-hidden="true" data-backdrop="static" data-keyboard="false">';
        tags += '<div class="modal-dialog modal-lg">';
        tags += '<div class="modal-content">';

        tags += '<div class="modal-header">';
        tags += '<button type="button" class="close cancelar" data-dismiss="modal" aria-label="Close" onclick="_set_close_modal(true);"><i class="fa fa-times"></i></button>';
        tags += '<h4 class="modal-title" id="myModalLabel">';
        tags += '<label id="lbl_titulo"></label>';
        tags += '</h4>';
        tags += '</div>';

        tags += '<div class="modal-body">';

        //tags += '<div class="row">' +
        //        '    <div class="col-md-6" style="margin-top: 10px !important;">' +
        //        '        &nbsp;&nbsp;&nbsp;<input type="checkbox" name="chk_es_empleado" id="chk_es_empleado" class="cbr cbr-primary">&nbsp;<span style="font-family: \'Roboto Regular\'">¿Es empleado?</span>' +
        //        '    </div>' +
        //        '    <div class="col-md-6">' +
        //        '              <div id="contenedor_combo_empleado">' +
        //        '                       <label class="fuente_lbl_controles">(*) Empleado</label>' +
        //        '                       <select id="cmb_empleado" style="width: 100% !important;" name="cmb_empleado" class="form-control input-sm" disabled="disabled" data-parsley-required="true" required ></select> ' +
        //        '              </div>' +
        //        '     </div>' +
        //        '</div>';

        tags += '<div class="row">' +
        '       <div class="col-md-6" >' +
        '           <label class="fuente_lbl_controles">(*) Usuario</label>' +
        '           <input type="text" id="txt_usuario" name="txt_usuario" class="form-control input-sm" disabled="disabled" placeholder="Usuario" data-parsley-required="true" maxlength="20" required /> ' +
        '           <input type="hidden" id="txt_usuario_id"/>' +
        '           <input type="hidden" id="txt_rel_id"/>' +
        '       </div>' +
        '       <div class="col-md-6" >' +
        '           <label class="fuente_lbl_controles">(*) Nombre del Usuario </label>' +
        '           <input type="text" id="txt_nombre_usuario" name="txt_nombre_usuario" class="form-control input-sm" disabled="disabled" placeholder="Nombre del Usuario" data-parsley-required="true" maxlength="100" required /> ' +
        '       </div>' +

        '   </div>' +

        '<div class="row">' +
        ' <div class="col-md-6">' +
        '            <label class="fuente_lbl_controles">(*) Email</label>' +
        '        <input type="text" id="txt_email" name="txt_email" class="form-control input-sm" disabled="disabled" placeholder="Email" data-parsley-required="true" maxlength="100" required /> ' +
        '    </div>' +
        '       <div class="col-md-6">' +
        '           <label class="fuente_lbl_controles">(*) Contraseña</label>' +
        '           <input type="password" id="txt_password" name="txt_password" class="form-control input-sm" disabled="disabled" placeholder="Contraseña" data-parsley-required="true" maxlength="100" required /> ' +
        '       </div>' +
        '</div>' +

        '<div class="row">' +
        '    <div class="col-sm-6">' +
        '      <label class="fuente_lbl_controles">(*) Tipo de usuario</label>' +
        '    <select id="cmb_tipo_usuario" name="cmb_tipo_usuario" class="form-control input-sm" disabled="disabled" data-parsley-required="true" required ></select> ' +
        '    </div>' +
        '    <div class="col-sm-6">' +
        '      <label class="fuente_lbl_controles">(*) Estatus</label>' +
        '       <select id="cmb_estatus" name="cmb_estatus" class="form-control input-sm" disabled="disabled" data-parsley-required="true" required ></select> ' +
        '    </div>' +
        //'    <div class="col-sm-6">' +
        //'       <label class="fuente_lbl_controles">(*) Rol</label>' +
        //'       <select id="cmb_rol" name="cmb_rol" class="form-control input-sm" disabled="disabled" data-parsley-required="true" required ></select> ' +
        //'    </div>' +
        '</div>';

        tags += '</div>';

        tags += '<div class="modal-footer">';
        tags += '<div class="row">';

        tags += '<div class="col-md-7">';
        tags += '<div id="sumary_error" class="alert alert-danger text-left" style="width: 277.78px !important; display:none;">';
        tags += '<label id="lbl_msg_error"/>';
        tags += '</div>';
        tags += '</div>';

        tags += '<div class="col-md-5">';
        tags += '<div class="form-inline">';
        tags += '<button type="submit" class="btn btn-info btn-icon btn-icon-standalone btn-xs" id="btn_guardar_datos" title="Guardar"><i class="fa fa-check"></i><span>Aceptar</span></button>';
        tags += '<button type="button" class="btn btn-danger btn-icon btn-icon-standalone btn-xs cancelar" data-dismiss="modal" id="btn_cancelar" aria-label="Close" onclick="_set_close_modal(true);" title="Cancelar operaci&oacute;n"><i class="fa fa-remove"></i><span>Cancelar</span></button>';
        tags += '</div>';
        tags += '</div>';

        tags += '</div>';
        tags += '</div>';

        tags += '</div>';
        tags += '</div>';
        tags += '</div>';

        $(tags).appendTo('body');

        $('#btn_guardar_datos').bind('click', function (e) {
            e.preventDefault();

            if ($('#txt_usuario_id').val() != null && $('#txt_usuario_id').val() != undefined && $('#txt_usuario_id').val() != '') {
                var _output = _validation('editar');
                if (_output.Estatus) {
                    if (_modificar_usuarios()) {
                        _estado_inicial();
                        _set_close_modal(true);
                        jQuery('#modal_datos').modal('hide');
                    }
                } else {
                    _set_close_modal(false);
                }
            } else {
                var _output = _validation('alta');
                if (_output.Estatus) {
                    if (_alta_usuarios()) {
                        _estado_inicial();
                        _set_close_modal(true);
                        jQuery('#modal_datos').modal('hide');
                    }
                } else {
                    _set_close_modal(false);
                }
            }
        });
    } catch (e) {
        _mostrar_mensaje('Informe técnico', e);
    }
}

function _modal_roles_usuario() {
    var tags = '';
    try {
        tags += '<div class="modal fade" id="mdl_rol_usuario" name="mdl_rol_usuario" role="dialog" aria-hidden="true" data-backdrop="static" data-keyboard="false">';
        tags += '<div class="modal-dialog modal-lg">';
        tags += '<div class="modal-content">';

        tags += '<div class="modal-header">';
        tags += '<button type="button" class="close cancelar" data-dismiss="modal" aria-label="Close" onclick="_set_close_mdl_roles_usuario(true);"><i class="fa fa-times text-red"></i></button>';
        tags += '<h3 class="modal-title" id="myModalLabels">';
        tags += '<label id="lbl_titulo_rol_usuario"></label>';
        tags += '</h3>';
        tags += '</div>';

        tags += '<div class="modal-body">';

        tags += '<div class="row" id="div_roles">' +
                '   <div class="col-sm-6">' +
                '       <label class="fuente_lbl_controles">Sucursal</label>' +
                '       <select id="cmb_sucursal" name="cmb_sucursal" style="width:100%"  data-parsley-required="true"></select> ' +
                '   </div>' +
                '   <div class="col-sm-6">' +
                '       <label class="fuente_lbl_controles">Roles</label>' +
                '       <input type="hidden" id="txt_usuario_id"/>' +
                '       <select id="cmb_roles" name="cmb_roles" style="width:100%"  data-parsley-required="true"></select> ' +
                '   </div>' +
                '</div>';
        tags += '<hr />';

        tags += '<div class="row">';
        tags += '   <div class="col-sm-12">';
        tags += '       <div id="toolbarpr" style="margin-left: 5px;">';
        tags += '           <div class="btn-group" role="group" style="margin-left: 5px;">';
        tags += '               <button type="submit" class="btn btn-blue btn-icon btn-icon-standalone" id="btn_agregar_rol_usuario" title="Agregar">' +
                '                   <i class="fa fa-plus"></i>' +
                '                   <span>Agregar</span>' +
                '               </button>';
        tags += '           </div>';
        tags += '       </div>';
        tags += '       <table id="tbl_usuarios_roles" data-toolbar="#toolbarpr" class="table table-responsive"></table>';
        tags += '   </div>';
        tags += '</div>';

        tags += '</div>';

        tags += '<div class="modal-footer">';

        tags += '<div class="row">';
        tags += '   <div class="col-md-7">';
        tags += '       <div id="sumary_error_rol_usuario" class="alert alert-danger text-left" style="width: 277.78px !important; display:none;">';
        tags += '           <label id="lbl_msg_error_rol_usuario"/>';
        tags += '       </div>';
        tags += '   </div>';
        tags += '   <div class="col-md-5">';
        tags += '   </div>';
        tags += '</div>';

        tags += '</div>';

        tags += '</div>';
        tags += '</div>';
        tags += '</div>';

        $(tags).appendTo('body');

        $('#btn_agregar_rol_usuario').bind('click', function (e) {
            e.preventDefault();
            var s = _validation_insert_rol();
            if (s.Estatus) {
                if (_alta_rol_usuario()) {
                    //_estado_inicial_rol_sucursal();
                    //_set_close_modal_roles_usuario(false);
                    //jQuery('#mdl_rol_usuario').modal('hide');
                }
            } else {
                _mostrar_mensaje("Validacion", s.Mensaje);
            }
        });
    } catch (e) {
        _mostrar_mensaje_rol_sucursal('Informe técnico', e);
    }
}

function _set_close_modal(state) {
    closeModal = state;
}

function btn_editar_click(usuario) {
    var row = $(usuario).data('usuario');

    $('#txt_usuario_id').val(row.Usuario_ID);
    $('#txt_usuario').val(row.Usuario);
    $('#txt_password').val(row.Password);
    $('#txt_email').val(row.Email);
    $('#cmb_estatus').val(row.Estatus_ID);
    $('#cmb_tipo_usuario').val(row.Tipo_Usuario_ID);
    $('#txt_nombre_usuario').val(row.Nombre);
    //$('#cmb_rol').val(row.Rol_ID);
    $('#txt_rel_id').val(row.Rel_Usuarios_Rol_ID);


    //if (row.Empleado_ID !== "" && row.Empleado_ID !== null && row.Empleado_ID !== undefined) {
    //    $('#chk_es_empleado').prop('checked', true);
    //    $("#cmb_empleado").select2("trigger", "select", {
    //        data: { id: row.Empleado_ID, text: row.Nombre }
    //    });
    //}
    //else {
    //    $('#chk_es_empleado').prop('checked', false);
    //}

    _habilitar_controles('Modificar');
    _launch_modal('<i class="glyphicon glyphicon-edit" style="font-size: 25px;"></i>&nbsp;&nbsp;Actualizar registro');
}

function btn_eliminar_click(usuario) {
    var row = $(usuario).data('usuario');

    bootbox.confirm({
        title: 'Eliminar Registro',
        message: '¿Está seguro de eliminar el registro seleccionado?',
        callback: function (result) {
            if (result) {
                _eliminar_usuarios(row.Usuario_ID);
            }
            _estado_inicial();
        }
    });
}

function btn_add_rol_click(usuario) {
    try {
        var row = $(usuario).data('usuario');
        $('#txt_usuario_id').val(row.Usuario_ID);
        _consultar_roles_usuarios();
        jQuery('#mdl_rol_usuario').modal('show', { backdrop: 'static', keyboard: false });
    } catch (e) {
        _mostrar_mensaje("Error Técnico", "Error " + e);
    }
}

function _set_location_toolbar() {
    $('#toolbar').parent().removeClass("pull-left");
    $('#toolbar').parent().addClass("pull-right");
}

function _set_title_modal(Titulo) {
    $("#lbl_titulo").html(Titulo);
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

                filtros.Usuario = $('#txt_busqueda_por_usuario').val() === '' ? '' : $('#txt_busqueda_por_usuario').val();
                filtros.Estatus_ID = $('#cmb_estatusfiltro').val() === '' ? 0 : parseInt($('#cmb_estatusfiltro').val());


                var $data = JSON.stringify({ 'jsonObject': JSON.stringify(filtros) });

                jQuery.ajax({
                    type: 'POST',
                    url: 'controllers/Usuarios_Controller.asmx/Consultar_Usuarios_Por_Filtros',
                    data: $data,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: true,
                    cache: false,
                    success: function (datos) {
                        if (datos !== null) {
                            $('#tbl_usuarios').bootstrapTable('load', JSON.parse(datos.d));
                            hide_loading_bar();
                        }
                    }
                });
            }
        });
    } catch (e) {

    }
}

function _validation(opcion) {
    var _output = new Object();

    _output.Estatus = true;
    _output.Mensaje = '';

    if (!$('#txt_usuario').parsley().isValid()) {
        _add_class_error('#txt_usuario');
        _output.Estatus = false;
        _output.Mensaje += '&nbsp;<i class="fa fa-angle-double-right"></i>&nbsp;El usuario es un dato requerido.<br />';
    } else {
        var _Resultado = (opcion === 'alta') ?
            _validate_fields($('#txt_usuario').val(), null, 'usuario') :
            _validate_fields($('#txt_usuario').val(), $('#txt_usuario_id').val(), 'usuario');

        if (_Resultado.Estatus === 'error') {
            _add_class_error('#txt_usuario');
            _output.Estatus = false;
            _output.Mensaje += '&nbsp;<i class="fa fa-angle-double-right"></i>&nbsp;' + _Resultado.Mensaje + '<br />';
        }
    }
    if (!$('#txt_nombre_usuario').parsley().isValid()) {
        _add_class_error('#txt_nombre_usuario');
        _output.Estatus = false;
        _output.Mensaje += '&nbsp;<i class="fa fa-angle-double-right"></i>&nbsp;El Nombre es un dato requerido.<br />';
    }
    if (!$('#txt_password').parsley().isValid()) {
        _add_class_error('#txt_password');
        _output.Estatus = false;
        _output.Mensaje += '&nbsp;<i class="fa fa-angle-double-right"></i>&nbsp;La contraseña es un dato requerido.<br />';
    }

    if (!$('#txt_email').parsley().isValid()) {
        _add_class_error('#txt_email');
        _output.Estatus = false;
        _output.Mensaje += '&nbsp;<i class="fa fa-angle-double-right"></i>&nbsp;El email es un dato requerido.<br />';
    } else {
        var _Resultado = (opcion === 'alta') ?
            _validate_fields($('#txt_email').val(), null, 'email') :
            _validate_fields($('#txt_email').val(), $('#txt_usuario_id').val(), 'email');

        if (_Resultado.Estatus === 'error') {
            _add_class_error('#txt_email');
            _output.Estatus = false;
            _output.Mensaje += '&nbsp;<i class="fa fa-angle-double-right"></i>&nbsp;' + _Resultado.Mensaje + '<br />';
        }
    }

    if (!$('#cmb_estatus').parsley().isValid()) {
        _add_class_error('#cmb_estatus');
        _output.Estatus = false;
        _output.Mensaje += '&nbsp;<i class="fa fa-angle-double-right"></i>&nbsp;El estatus es un dato requerido.<br />';
    }

    //if ($('#chk_es_empleado').is(":checked")) {
    //    if (!$('#cmb_empleado').parsley().isValid()) {
    //        _add_class_error('#cmb_empleado');
    //        _output.Estatus = false;
    //        _output.Mensaje += '&nbsp;<i class="fa fa-angle-double-right"></i>&nbsp;El empleado es un dato requerido.<br />';
    //    }
    //}

    if (!$('#cmb_tipo_usuario').parsley().isValid()) {
        _add_class_error('#cmb_tipo_usuario');
        _output.Estatus = false;
        _output.Mensaje += '&nbsp;<i class="fa fa-angle-double-right"></i>&nbsp;El tipo de usuario es un dato requerido.<br />';
    }


    if (!_output.Estatus) _validation_sumary(_output);

    return _output;
}

function _add_class_error(selector) {
    $(selector).addClass('alert-danger');
}

function _remove_class_error(selector) {
    $(selector).removeClass('alert-danger');
}

function _clear_all_class_error() {
    $('#modal_datos input[type=text]').each(function (index, element) {
        _remove_class_error('#' + $(this).attr('id'));
    });

    $('#modal_datos select').each(function (index, element) {
        _remove_class_error('#' + $(this).attr('id'));
    });

    $('#modal_datos input[type=password]').each(function (index, element) {
        _remove_class_error('#' + $(this).attr('id'));
    });
}

function _validation_sumary(validation) {
    var header_message = '<i class="fa fa-exclamation-triangle fa-2x"></i><span>Observaciones</span><br />';

    if (validation == null) {
        $('#lbl_msg_error').html('');
        $('#sumary_error').css('display', 'none');
    } else {
        $('#lbl_msg_error').html(header_message + validation.Mensaje);
        $('#sumary_error').css('display', 'block');
    }
}

function _launch_modal(title_window) {
    _set_title_modal(title_window);
    jQuery('#modal_datos').modal('show', { backdrop: 'static', keyboard: false });
    $('#txt_usuario').focus();

}

function _enter_keypress_modal() {
    var $btn = $('[id$=btn_guardar_datos]').get(0);
    $(window).keypress(function (e) {
        if (e.which === 13 && e.target.type !== 'textarea') {
            if ($btn != undefined && $btn != null) {
                if ($btn.type === 'submit')
                    $btn.click();
                else
                    eval($btn.href);
                return false;
            }
        }
    });
}

function _validate_fields(value, id, field) {
    var usuario = null;
    var Resultado = null;

    try {
        usuario = new Object();
        if (id !== null)
            usuario.Usuario_ID = parseInt(id);

        switch (field) {
            case 'usuario':
                usuario.Usuario = value;
                break;
            case 'email':
                usuario.Email = value;
                break;
            default:
        }

        var $data = JSON.stringify({ 'jsonObject': JSON.stringify(usuario) });

        $.ajax({
            type: 'POST',
            url: 'controllers/Usuarios_Controller.asmx/Consultar_Usuarios_Por_Nombre',
            data: $data,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            cache: false,
            success: function (result) {
                if (result !== null)
                    Resultado = JSON.parse(result.d);
            }
        });
    } catch (e) {
        Resultado = new Object();
        Resultado.Estatus = 'error';
        Resultado.Mensaje = 'No fue posible realizar la validación del ' + field + ' en la base de datos.';
        _mostrar_mensaje('Informe Técnico', e);
    }
    return Resultado;
}

function _load_estatus() {
    var filtros = null;
    try {
        jQuery.ajax({
            type: 'POST',
            url: 'controllers/Usuarios_Controller.asmx/ConsultarEstatus',
            //data: $data,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: true,
            cache: false,
            success: function (datos) {
                if (datos !== null) {
                    var datos_combo = $.parseJSON(datos.d);
                    var select = $('#cmb_estatus');
                    $('option', select).remove();
                    var options = '';
                    for (var Indice_Estatus = 0; Indice_Estatus < datos_combo.length; Indice_Estatus++) {
                        options += '<option value="' + datos_combo[Indice_Estatus].Estatus_ID + '">' + datos_combo[Indice_Estatus].Estatus.toUpperCase() + '</option>';
                        if (datos_combo[Indice_Estatus].Estatus.toUpperCase() == 'ACTIVO') {
                            estatusActivo = datos_combo[Indice_Estatus].Estatus_ID;
                        }
                    }
                    select.append(options);
                }
            }
        });
    } catch (e) {

    }
}

function _filtroEstatus() {
    var filtros = null;
    try {
        jQuery.ajax({
            type: 'POST',
            url: 'controllers/Usuarios_Controller.asmx/ConsultarFiltroEstatus',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: true,
            cache: false,
            success: function (datos) {
                if (datos !== null) {
                    var datos_combo = $.parseJSON(datos.d);
                    var select = $('#cmb_estatusfiltro');
                    $('option', select).remove();
                    var options = '<option value=""><-TODOS-></option>';
                    for (var Indice_estatus = 0; Indice_estatus < datos_combo.length; Indice_estatus++) {
                        options += '<option value="' + datos_combo[Indice_estatus].Estatus_ID + '">' + datos_combo[Indice_estatus].Estatus + '</option>';
                    }
                    select.append(options);
                }
            }
        });
    } catch (e) {

    }
}

function _load_tipo_usuario() {
    var filtros = null;
    try {
        jQuery.ajax({
            type: 'POST',
            url: 'controllers/Usuarios_Controller.asmx/ConsultarTipoUsuario',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: true,
            cache: false,
            success: function (datos) {
                if (datos !== null) {
                    var datos_combo = $.parseJSON(datos.d);
                    var select = $('#cmb_tipo_usuario');
                    $('option', select).remove();
                    var options = '<option value=""><-SELECCIONE-></option>';
                    for (var Indice_tipo = 0; Indice_tipo < datos_combo.length; Indice_tipo++) {
                        options += '<option value="' + datos_combo[Indice_tipo].Tipo_Usuario_ID + '">' + datos_combo[Indice_tipo].Nombre + '</option>';
                    }
                    select.append(options);
                }
            }
        });
    } catch (e) {

    }
}

function _load_rol() {

    try {
        $('#cmb_roles').select2({
            language: "es",
            theme: "classic",
            placeholder: 'Selecciona el rol',
            allowClear: true,
            ajax: {
                url: 'controllers/Usuarios_Controller.asmx/ConsultarRol',
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
                        sucursal: $('#cmb_sucursal').val(),
                    };
                },
                processResults: function (data, page) {
                    return {
                        results: data
                    };
                },
            }
        });
        $('#cmb_roles').on("select2:select", function (evt) {
            tipo = evt.params.data.detalle_1;
        });
    } catch (e) {
        _mostrar_mensaje('Informe Técnico', e);
    }
}

function _load_sucursal() {

    try {
        $('#cmb_sucursal').select2({
            language: "es",
            theme: "classic",
            placeholder: 'Selecciona la sucursal',
            allowClear: true,
            ajax: {
                url: 'controllers/Usuarios_Controller.asmx/Consultar_Sucursales',
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

    } catch (e) {
        _mostrar_mensaje('Informe Técnico', e);
    }
}

function _consultar_roles_usuarios() {
    var filtros = null;
    try {

        filtros = new Object();

        filtros.Usuario_ID = $('#txt_usuario_id').val() === '' ? 0 : parseInt($('#txt_usuario_id').val());

        var $data = JSON.stringify({ 'jsonObject': JSON.stringify(filtros) });

        jQuery.ajax({
            type: 'POST',
            url: 'controllers/Usuarios_Controller.asmx/Consultar_Roles_Usuarios',
            data: $data,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: true,
            cache: false,
            success: function (datos) {
                if (datos !== null) {
                    $('#tbl_usuarios_roles').bootstrapTable('load', JSON.parse(datos.d));
                }
            }
        });

    } catch (e) {

    }
}

//function _load_cmb_empleados() {
//    try {
//        $('#cmb_empleado').select2({
//            language: "es",
//            theme: "classic",
//            placeholder: 'SELECCIONE',
//            allowClear: true,
//            ajax: {
//                url: 'controllers/Usuarios_Controller.asmx/Consultar_Empleados',
//                cache: true,
//                dataType: 'json',
//                type: "POST",
//                delay: 250,
//                params: {
//                    contentType: 'application/json; charset=utf-8'
//                },
//                quietMillis: 100,
//                results: function (data) {
//                    return { results: data };
//                },
//                data: function (params) {
//                    return {
//                        q: params.term,
//                        page: params.page,
//                        id_usuario: $('#txt_usuario_id').val()
//                    };
//                },
//                processResults: function (data, page) {
//                    return {
//                        results: data
//                    };
//                },
//            }
//        });

//        $('#cmb_empleado').on("select2:select", function (evt) {
//            $('#txt_nombre_usuario').val(evt.params.data.text);
//        });

//        $('#cmb_empleado').on("select2:unselect", function (evt) {
//            $('#txt_nombre_usuario').val("");
//        });

//    } catch (e) {
//        mostrar_mensaje('Informe técnico', e);
//    }
//}

function _alta_rol_usuario() {
    var Producto_Proveedor = null;
    var isComplete = false;

    try {

        Producto_Proveedor = new Object();
        Producto_Proveedor.Usuario_ID = parseInt($('#txt_usuario_id').val());
        Producto_Proveedor.Rol_ID = parseInt($('#cmb_roles').val());
        Producto_Proveedor.Sucursal_ID = parseInt($('#cmb_sucursal').val());
        var $data = JSON.stringify({ 'jsonObject': JSON.stringify(Producto_Proveedor) });

        $.ajax({
            type: 'POST',
            url: 'controllers/Usuarios_Controller.asmx/Alta_Rol_Usuario',
            data: $data,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            cache: false,
            success: function (result) {
                var Resultado = JSON.parse(result.d);
                if (Resultado != null && Resultado != undefined && Resultado != '') {
                    if (Resultado.Estatus == 'success') {
                        _consultar_roles_usuarios();
                        isComplete = true;
                    } else if (Resultado.Estatus == 'error') {
                        //_validation_rol_sucursal_sumary(Resultado);
                    }
                } else {
                    //_validation_rol_sucursal_sumary(Resultado);
                }
            }
        });
    } catch (e) {
        _mostrar_mensaje_rol_sucursal('Informe Tecnico', e);
    }
    return isComplete;
}

function _validation_insert_rol() {
    var _output = new Object();

    _output.Estatus = true;
    _output.Mensaje = '';
    try {

        var tbl = $('#tbl_usuarios_roles').bootstrapTable('getData');

        $.each(tbl, function (index, value) {
            if (value.Sucursal_ID == $('#cmb_sucursal').val()) {
                if (value.Tipo == tipo) {
                    _output.Estatus = false;
                    _output.Mensaje += 'No se puede agregar el rol';
                }

            }
        });
    } catch (e) {

    }

    return _output;
}
