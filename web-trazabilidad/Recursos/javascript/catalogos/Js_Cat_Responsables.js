
$(document).on('ready', function () {
    _load_vistas();
});



function _load_vistas() {
    _launchComponent('vistas/Responsables/Principal.html', 'Principal');
    _launchComponent('vistas/Responsables/Operacion.html', 'Operacion');
}


function _launchComponent(component, id) {

    $('#' + id).load(component, function () {

        switch (id) {
            case 'Principal':
                _inicializar_vista_principal();
                break;
            case 'Operacion':
                _inicializar_vista_procesos();
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
        crear_tabla_responsables();
        _set_location_toolbar('toolbar');
        _load_cmb_estatus('cmb_estatus_filtro');
        _eventos_principal();
        _mostrar_vista('Principal');


    } catch (e) {
        _mostrar_mensaje('Error Técnico', e);
    }
}
//  -----------------------------------------------------
//  -----------------------------------------------------
function _inicializar_vista_procesos() {
    try {

        _eventos_procesos();
        _limpiar_todos_controles_procesos();
        _load_cmb_estatus('cmb_estatus');

        _keyDownInt('txt_cp');

    } catch (e) {
        _mostrar_mensaje('Error Técnico', e);
    }
}


function _limpiar_todos_controles_procesos() {

    try {

        $('input[type=text]').each(function () { $(this).val(''); });
        $('input[type=hidden]').each(function () { $(this).val(''); });


    } catch (e) {
        _mostrar_mensaje('Error Técnico', 'limpiar controles. ' + e);
    }
}


function _habilitar_controles(opc) {
    var Estatus = true;
    try {

        Estatus = false;
        var _boton_ = $('#btn_guardar');
        _boton_.show();

        switch (opc) {
            case 'Nuevo':
                $('#btn_guardar').attr('title', 'Guardar');

                $('#txt_clave').attr({ 'disabled': Estatus });
                $('#txt_nombre').attr({ 'disabled': Estatus });
                $('#txt_email').attr({ 'disabled': Estatus });

                break;


            case 'Modificar':
                $('#btn_guardar').attr('title', 'Actualizar');

                Estatus = true;

                $('#txt_clave').attr({ 'disabled': Estatus });
                //$('#txt_nombre').attr({ 'disabled': Estatus });
                //$('#txt_email').attr({ 'disabled': Estatus });
                break;

            case 'Ver':
                _boton_.hide();

                break;
        }


    } catch (e) {
        _mostrar_mensaje('Error Técnico' + ' [_habilitar_controles] ', e);
    }

}

function crear_tabla_responsables() {

    try {
        $('#tbl_responsables').bootstrapTable('destroy');

        $('#tbl_responsables').bootstrapTable({
            cache: false,
            striped: true,
            pagination: true,
            data: [],
            pageSize: 10,
            pageList: [10, 25, 50, 100, 200],
            smartDysplay: false,
            search: true,
            showColumns: false,
            showRefresh: false,
            minimumCountColumns: 2,
            columns: [
                
                { field: 'Responsable_Id', title: 'Responsable_Id', align: 'center', valign: 'top', visible: false, sortable: true },
                { field: 'Clave', title: 'Clave', align: 'left', valign: 'top', sortable: true },
                { field: 'Nombre', title: 'Nombre', align: 'center', valign: 'top', visible: true, sortable: true },
                { field: 'Telefono', title: 'Telefono', align: 'center', valign: 'top', visible: true, sortable: true },
                { field: 'Email', title: 'Correo electronico', align: 'center', valign: 'top', visible: true, sortable: true },
                { field: 'Direccion', title: 'Direccion', align: 'center', valign: 'top', visible: true, sortable: true },
                //{ field: 'Estatus', title: 'Estatus', align: 'center', valign: 'top', visible: true, sortable: true },
                 {
                     field: 'Estatus', title: 'Estatus', align: 'center', valign: 'top', sortable: true, visible: true,
                     formatter: function (value, row, index) {


                         var opciones = "";
                         opciones += ' <div class="row" style="padding-top:2px;">';

                         opciones += '   <div class="col-md-12">';
                         opciones += '       <div>';

                         if (value == "ACTIVO") {
                             opciones += '           <i class="fa fa-check" style="color:#008000;font-size: 14px;"></i>&nbsp;' + value;
                         }
                         else {
                             opciones += '           <i class="fa fa-window-close" style="color:#FF0000;font-size: 14px;"></i>&nbsp;' + value;
                         }


                         opciones += "       </div>"
                         opciones += "   </div>"
                         opciones += "</div>"
                         return opciones;
                     }
                 },

                { field: 'Password', title: 'Password', align: 'center', valign: 'top', visible: false, sortable: true },
              

                { field: 'Celular', title: 'Celular', align: 'center', valign: 'top', visible: false, sortable: true },
                { field: 'Colonia', title: 'Colonia', align: 'center', valign: 'top', visible: false, sortable: true },
                { field: 'CP', title: 'CP', align: 'center', valign: 'top', visible: false, sortable: true },
                { field: 'Ciudad', title: 'Ciudad', align: 'center', valign: 'top', visible: false, sortable: true },
                { field: 'Estado', title: 'Estado', align: 'center', valign: 'top', visible: false, sortable: true },


                  {
                      field: 'Responsable_Id',
                      title: '',
                      align: 'right',
                      valign: 'top',
                      halign: 'center',

                      formatter: function (value, row) {

                          var opciones = '<div style=" text-align: center;">';

                          opciones += '<div style="display:block"><a class="remove ml10 text-purple" id="' + row.Documento_Id + '" href="javascript:void(0)" data-orden=\'' + JSON.stringify(row) + '\' onclick="btn_editar_click(this);" title="Editar"><i class="glyphicon glyphicon-edit"></i>&nbsp;<span style="font-size:11px !important;">Editar</span></a></div>';


                          opciones += '</div>';

                          return opciones;
                      }
                  },
                   {
                       field: 'Responsable_Id',
                       title: '',
                       align: 'right',
                       valign: 'top',
                       halign: 'center',

                       formatter: function (value, row) {

                           var opciones = '<div style=" text-align: center;">';

                           opciones += '<div style="display:block"><a class="remove ml10 text-red" id="' + row.Documento_Id + '" href="javascript:void(0)" data-orden=\'' + JSON.stringify(row) + '\' onclick="btn_eliminar_click(this);" title="Editar"><i class="glyphicon glyphicon-trash"></i>&nbsp;<span style="font-size:11px !important;">Eliminar</span></a></div>';


                           opciones += '</div>';

                           return opciones;
                       }
                   },
            ]
        });
    } catch (e) {
        _mostrar_mensaje('Informe Técnico' + '[crear_tabla_vehiculo]', e.message);
    }
}

//  -----------------------------------------------------
//  -----------------------------------------------------


function btn_eliminar_click(tab) {


    var row = $(tab).data('orden');

    bootbox.confirm({
        title: 'INACTIVAR Registro',
        message: 'Esta seguro de INACTIVAR el registro seleccionado?',
        callback: function (result) {
            if (result) {

                //  documentos
                var filtros = null;
                filtros = new Object();

                filtros.Responsable_Id = row.Responsable_Id;
                var $data = JSON.stringify({ 'jsonObject': JSON.stringify(filtros) });

                $.ajax({
                    type: 'POST',
                    url: 'controllers/ResponsablesController.asmx/Cancelacion',
                    data: $data,
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    cache: false,
                    success: function (datos) {
                        if (datos !== null) {
                            datos = JSON.parse(datos.d);

                            _ConsultarFiltros();
                        }
                        else {

                        }
                    }
                });

            }

        }
    });


}

//  -----------------------------------------------------
//  -----------------------------------------------------

function btn_editar_click(tab) {


    var row = $(tab).data('orden');

    _habilitar_controles('Modificar');
    _limpiar_todos_controles_procesos();


    $('#txt_responsable_id').val(row.Responsable_Id);

    $('#txt_clave').val(row.Clave);
    $('#txt_nombre').val(row.Nombre);


    if(row.Estatus != null)
    $('#cmb_estatus').select2("trigger", "select", {
        data: { id: row.Estatus, text: row.Estatus }
    });
    
    $('#txt_email').val(row.Email);
    $('#txt_telefono').val(row.Telefono);
    $('#txt_celular').val(row.Celular);


    $('#txt_direccion').val(row.Direccion);
    $('#txt_colonia').val(row.Colonia);
    $('#txt_cp').val(row.CP);
    $('#txt_ciudad').val(row.Ciudad);
    $('#txt_estado').val(row.Estado);


    if (row.Password != null) {

        //  documentos
        var filtros = null;
        filtros = new Object();

        filtros.Responsable_Id = $('#txt_responsable_id').val();
        var $data = JSON.stringify({ 'jsonObject': JSON.stringify(filtros) });

        $.ajax({
            type: 'POST',
            url: 'controllers/ResponsablesController.asmx/Consultar_Password',
            data: $data,
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            async: false,
            cache: false,
            success: function (datos) {
                if (datos !== null) {
                    datos = JSON.parse(datos.d);

                    $('#txt_password').val(datos[0].Password);
                }
            }
        });
    }


    _mostrar_vista('Operacion');
}


//  -----------------------------------------------------
//  -----------------------------------------------------
function _set_location_toolbar(toolbar) {
    $('#' + toolbar).parent().removeClass("pull-left");
    $('#' + toolbar).parent().addClass("pull-right");

}

//  -----------------------------------------------------
//  -----------------------------------------------------
function _load_cmb_estatus(cmb) {
    try {
        $('#' + cmb).select2({
            language: "es",
            theme: "classic",
            placeholder: 'SELECCIONE',
            allowClear: true,
            data: [
                {
                    id: '',
                    text: '',
                }, {
                    id: 'ACTIVO',
                    text: 'ACTIVO',
                }, {
                    id: 'INACTIVO',
                    text: 'INACTIVO',
                },
            ],
        });

    } catch (e) {
        _mostrar_mensaje('Informe técnico' + '[_load_cmb_estatus]', e);
    }
}


function _eventos_procesos() {
    try {
        //  ---------------------------------------------------------------------------------
        //  ---------------------------------------------------------------------------------
        $('#btn_guardar').on('click', function (e) {
            e.preventDefault();


            var title = $('#btn_guardar').attr('title');
            var valida_datos_requerido = _validarDatos_Nuevo();
            if (valida_datos_requerido.Estatus) {

                if (title == "Guardar") {
                    Alta()
                }
                else {
                    Modificar();
                }
            }
            else {
                _mostrar_mensaje('Información', valida_datos_requerido.Mensaje);
            }

        });
        //  ---------------------------------------------------------------------------------
        //  ---------------------------------------------------------------------------------
        $('#btn_cancelar').on('click', function (e) {
            e.preventDefault();
            _mostrar_vista('Principal');
            _limpiar_todos_controles_procesos();
        });
       
        //  ---------------------------------------------------------------------------------
        //  ---------------------------------------------------------------------------------

    } catch (e) {
        _mostrar_mensaje('Error Técnico', e);
    }
}


function _eventos_principal() {
    try {
        $('#btn_inicio').on('click', function (e) {
            e.preventDefault();
            window.location.href = '../Paginas_Generales/Frm_Apl_Principal.aspx';
        });

        $('#btn_nuevo').on('click', function (e) {
            e.preventDefault();


            _habilitar_controles('Nuevo');
            _limpiar_todos_controles_procesos();
            _mostrar_vista('Operacion');


        });

        $('#btn_busqueda').on('click', function (e) {
            e.preventDefault();
            _ConsultarFiltros();
        });

    } catch (e) {
        _mostrar_mensaje('Error Técnico', e);
    }
}



//  *******************************************************************************************************************************
//  *******************************************************************************************************************************
///********************CONSULTAS**************///
//  *******************************************************************************************************************************
function _ConsultarFiltros() {
    var filtros = null;
    try {
        filtros = new Object();

        filtros.Nombre = $("#txt_nombre_busqueda").val();
        filtros.Estatus = $('#cmb_estatus_filtro').val() === null ? "" : ($('#cmb_estatus_filtro').val());
        filtros.Clave = $("#txt_clave_busqueda").val();
        filtros.Email = $("#txt_email_busqueda").val();


        var $data = JSON.stringify({ 'jsonObject': JSON.stringify(filtros) });

        $.ajax({
            type: 'POST',
            url: 'controllers/ResponsablesController.asmx/Consultar_Responsables_Filtro',
            data: $data,
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            async: false,
            cache: false,
            success: function (datos) {
                if (datos !== null) {
                    datos = JSON.parse(datos.d);
                    $('#tbl_responsables').bootstrapTable('load', datos);
                }
            }
        });
    } catch (e) {
        _mostrar_mensaje('Error ', e);
    }
}


//  -----------------------------------------------------
//  -----------------------------------------------------
function _mostrar_vista(vista_) {

    switch (vista_) {
        case "Principal":
            $('#Operacion').hide();
            $('#Principal').show();
            break;
        case "Operacion":
            $('#Operacion').show();
            $('#Principal').hide();
            break;
    }
}

function _validarDatos_Nuevo() {
    var _output = new Object();

    try {
        _output.Estatus = true;
        _output.Mensaje = '';
        //  ---------------------------------------------------------------------------------------
        //  ---------------------------------------------------------------------------------------
        //  datos del vehiculo
        if ($('#txt_clave').val() == '' || $('#txt_clave').val() == undefined) {
            _output.Estatus = false;
            _output.Mensaje += '&nbsp;<i class="fa fa-angle-double-right"></i>&nbsp;La clave (NS).<br />';
        }

        if ($('#txt_nombre').val() == '' || $('#txt_nombre').val() == undefined) {
            _output.Estatus = false;
            _output.Mensaje += '&nbsp;<i class="fa fa-angle-double-right"></i>&nbsp;El nombre.<br />';
        }

        if ($('#cmb_estatus').val() == '' || $('#cmb_estatus').val() == undefined) {
            _output.Estatus = false;
            _output.Mensaje += '&nbsp;<i class="fa fa-angle-double-right"></i>&nbsp;El estatus.<br />';
        }


        if ($('#txt_email').val() == '' || $('#txt_email').val() == undefined) {
            _output.Estatus = false;
            _output.Mensaje += '&nbsp;<i class="fa fa-angle-double-right"></i>&nbsp;El email.<br />';
        }

        if ($('#txt_password').val() == '' || $('#txt_password').val() == undefined) {
            _output.Estatus = false;
            _output.Mensaje += '&nbsp;<i class="fa fa-angle-double-right"></i>&nbsp;El password.<br />';
        }

        //if ($('#txt_direccion').val() == '' || $('#txt_direccion').val() == undefined) {
        //    _output.Estatus = false;
        //    _output.Mensaje += '&nbsp;<i class="fa fa-angle-double-right"></i>&nbsp;La direccion.<br />';
        //}

        //if ($('#txt_colonia').val() == '' || $('#txt_colonia').val() == undefined) {
        //    _output.Estatus = false;
        //    _output.Mensaje += '&nbsp;<i class="fa fa-angle-double-right"></i>&nbsp;La colonia.<br />';
        //}

        //if ($('#txt_cp').val() == '' || $('#txt_cp').val() == undefined) {
        //    _output.Estatus = false;
        //    _output.Mensaje += '&nbsp;<i class="fa fa-angle-double-right"></i>&nbsp;El codigo postal.<br />';
        //}

        //if ($('#txt_ciudad').val() == '' || $('#txt_ciudad').val() == undefined) {
        //    _output.Estatus = false;
        //    _output.Mensaje += '&nbsp;<i class="fa fa-angle-double-right"></i>&nbsp;La ciudad.<br />';
        //}

        //if ($('#txt_estado').val() == '' || $('#txt_estado').val() == undefined) {
        //    _output.Estatus = false;
        //    _output.Mensaje += '&nbsp;<i class="fa fa-angle-double-right"></i>&nbsp;El estado.<br />';
        //}

        //if ($('#txt_telefono').val() == '' || $('#txt_telefono').val() == undefined) {
        //    _output.Estatus = false;
        //    _output.Mensaje += '&nbsp;<i class="fa fa-angle-double-right"></i>&nbsp;El telefono.<br />';
        //}

        if (_output.Mensaje != "") {
            _output.Mensaje = "Favor de proporcionar lo siguiente: <br />" + _output.Mensaje;
        }


    } catch (e) {
        _mostrar_mensaje('Informe técnico' + '[_validarDatos_Nuevo]', e);
    } finally {
        return _output;
    }
}


///********************BD***************///
function Alta() {
    var obj = new Object();

    try {

        obj.Clave = $('#txt_clave').val();
        obj.Nombre = ($('#txt_nombre').val());
        obj.Estatus = $('#cmb_estatus').val();
        obj.Email = $('#txt_email').val();
        obj.Password = $('#txt_password').val();
        obj.Telefono = $('#txt_telefono').val();
        obj.Celular = $('#txt_celular').val();
        obj.Direccion = $('#txt_direccion').val();
        obj.Colonia = $('#txt_colonia').val();
        obj.CP = $('#txt_cp').val();
        obj.Ciudad = ($('#txt_ciudad').val());
        obj.Estado = ($('#txt_estado').val());


        var $data = JSON.stringify({ 'jsonObject': JSON.stringify(obj) });

        $.ajax({
            type: 'POST',
            url: 'controllers/ResponsablesController.asmx/Alta',
            data: $data,
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            async: false,
            cache: false,
            success: function (datos) {
                if (datos != null) {
                    var result = JSON.parse(datos.d);
                    if (result.Estatus == 'success') {
                        _mostrar_vista('Principal');
                        _mostrar_mensaje(result.Titulo, result.Mensaje);
                        _ConsultarFiltros();
                    } else {
                        _mostrar_mensaje(result.Titulo, result.Mensaje);
                    }
                }
            }
        });

    } catch (e) {
        alert(e.message)
    }

}


function Modificar() {
    var obj = new Object();

    try {

        obj.Responsable_Id = parseInt( $('#txt_responsable_id').val());
        obj.Clave = $('#txt_clave').val();
        obj.Nombre = ($('#txt_nombre').val());
        obj.Estatus = $('#cmb_estatus').val();
        obj.Email = $('#txt_email').val();
        obj.Password = $('#txt_password').val();
        obj.Telefono = $('#txt_telefono').val();
        obj.Celular = $('#txt_celular').val();
        obj.Direccion = $('#txt_direccion').val();
        obj.Colonia = $('#txt_colonia').val();
        obj.CP = $('#txt_cp').val();
        obj.Ciudad = ($('#txt_ciudad').val());
        obj.Estado = ($('#txt_estado').val());
        var $data = JSON.stringify({ 'jsonObject': JSON.stringify(obj) });




        $.ajax({
            type: 'POST',
            url: 'controllers/ResponsablesController.asmx/Modificar',
            data: $data,
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            async: false,
            cache: false,
            success: function (datos) {
                if (datos != null) {
                    var result = JSON.parse(datos.d);
                    if (result.Estatus == 'success') {
                        _mostrar_vista('Principal');
                        _mostrar_mensaje(result.Titulo, result.Mensaje);
                        _ConsultarFiltros();
                    } else {
                        _mostrar_mensaje(result.Titulo, result.Mensaje);
                    }
                }
            }
        });

    } catch (e) {
        alert(e.message)
    }

}


function _keyDownInt(id) {
    $('#' + id).on('keydown', function (e) {

        //alert("entro int");//_remove_class_error('#' + $(this).attr('id'));

        // Allow: backspace, delete, tab, escape, enter
        if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110]) !== -1 ||
            // Allow: Ctrl+A, Command+A
            (e.keyCode === 65 && (e.ctrlKey === true || e.metaKey === true)) ||
            // Allow: home, end, left, right, down, up
            (e.keyCode >= 35 && e.keyCode <= 40)) {
            // let it happen, don't do anything
            return;
        }
        // Ensure that it is a number and stop the keypress
        if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
            e.preventDefault();
        }
    });
}