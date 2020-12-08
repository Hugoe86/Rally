
//  -----------------------------------------------------
//  -----------------------------------------------------
function _inicializar_vista_procesos() {
    try {

        
        crear_tabla_redes_sociales();
        crear_tabla_redes_sociales_eliminadas();

        _limpiar_todos_controles_procesos();
        _load_cmb_estatus('cmb_estatus');


        _eventos_procesos();
        _inicializar_fechas_operacion();
        _inicializar_horas_operacion();
        _keyDownInt('txt_recorrido_completo');

        _keyDownInt('txt_intervalo_evento');

    } catch (e) {
        _mostrar_mensaje('Error Técnico', e);
    }
}



//  *******************************************************************************************************************************
//  *******************************************************************************************************************************
//  fechas
//  *******************************************************************************************************************************
function _inicializar_fechas_operacion() {
    //------------------------------------------------------------------
    $('#dtp_txt_fecha_inicio_evento').datetimepicker({
        defaultDate: new Date(),
        viewMode: 'days',
        locale: 'es',
        format: "DD/MM/YYYY"
    });
    $("#dtp_txt_fecha_inicio_evento").datetimepicker("useCurrent", true);

    //------------------------------------------------------------------
    $('#dtp_txt_fecha_fin_evento').datetimepicker({
        defaultDate: new Date(),
        viewMode: 'days',
        locale: 'es',
        format: "DD/MM/YYYY"
    });
    $("#dtp_txt_fecha_fin_evento").datetimepicker("useCurrent", true);
    //------------------------------------------------------------------
    //------------------------------------------------------------------
    $('#dtp_txt_fecha_salida_evento').datetimepicker({
        defaultDate: new Date(),
        viewMode: 'days',
        locale: 'es',
        format: "DD/MM/YYYY"
    });
    $("#dtp_txt_fecha_salida_evento").datetimepicker("useCurrent", true);

    //------------------------------------------------------------------
    //------------------------------------------------------------------
}

//  -----------------------------------------------------
//  -----------------------------------------------------
function _inicializar_horas_operacion() {
    //------------------------------------------------------------------
    $('#dtp_txt_hora_salida_evento').datetimepicker({
        defaultDate: new Date(),
        locale: 'es',
        format: "LT"
    });
    $("#dtp_txt_hora_salida_evento").datetimepicker("useCurrent", true);

    //------------------------------------------------------------------
    //$('#dtp_txt_intervalo_salida_evento').datetimepicker({
    //    defaultDate: new Date(),
    //    locale: 'es',
    //    format: "LT"
    //});


  

    $("#dtp_txt_fecha_fin_evento").datetimepicker("useCurrent", true);
    
    //------------------------------------------------------------------

}

//  -----------------------------------------------------
//  -----------------------------------------------------
function _limpiar_todos_controles_procesos() {

    try {

        $('input[type=text]').each(function () { $(this).val(''); });
        $('input[type=hidden]').each(function () { $(this).val(''); });

    } catch (e) {
        _mostrar_mensaje('Error Técnico', 'limpiar controles. ' + e);
    }
}


//  ---------------------------------------------------------------------------------
//  ---------------------------------------------------------------------------------
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
        $('#btn_regresar_principal').on('click', function (e) {
            e.preventDefault();
            _mostrar_vista('Principal');
            _limpiar_todos_controles_procesos();
        });
        //  ---------------------------------------------------------------------------------
        //  ---------------------------------------------------------------------------------
        $('#btn_agregar_link').on('click', function (e) {
            e.preventDefault();

            var valida_datos_requerido = _validar_agregar_link();

            if (valida_datos_requerido.Estatus) {

                $('#tbl_redes_sociales').bootstrapTable('insertRow', {
                    index: 0,
                    row: {
                        Link_ID: 0,
                        Evento_Id: 0,
                        Nombre: $('#txt_nombre_link').val(),
                        Link: $('#txt_ruta_link').val(),
                        Estatus: 'ACTIVO',
                    }
                });

                $('#txt_nombre_link').val('');
                $('#txt_ruta_link').val('');

            }
            else {

                _mostrar_mensaje('Información', valida_datos_requerido.Mensaje);

            }
        });
        //  ---------------------------------------------------------------------------------
        //  ---------------------------------------------------------------------------------

    } catch (e) {
        _mostrar_mensaje('Error Técnico', e);
    }
}

//  ---------------------------------------------------------------------------------
//  ---------------------------------------------------------------------------------
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





//  -----------------------------------------------------
//  -----------------------------------------------------
function crear_tabla_redes_sociales() {

    try {
        $('#tbl_redes_sociales').bootstrapTable('destroy');

        $('#tbl_redes_sociales').bootstrapTable({
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

                { field: 'Link_ID', title: 'Link_ID', align: 'center', valign: 'top', visible: false, sortable: true },
                { field: 'Evento_Id', title: 'Evento_Id', align: 'center', valign: 'top', visible: false, sortable: true },
                { field: 'Nombre', title: 'Nombre', align: 'center', valign: 'top', visible: true, sortable: true },
                { field: 'Link', title: 'Link', align: 'center', valign: 'top', visible: true, sortable: true },
                { field: 'Estatus', title: 'Estatus', align: 'center', valign: 'top', visible: false, sortable: true },
                
                {
                    field: 'Link_ID',
                    title: '',
                    align: 'right',
                    valign: 'top',
                    halign: 'center',

                    formatter: function (value, row) {

                        var opciones = '<div style=" text-align: center;">';

                        opciones += '<div style="display:block">';

                         opciones += '<a class="remove ml10 text-red" id="' + row.Evento_Id + '" href="javascript:void(0)" data-orden=\'' + JSON.stringify(row) + '\' onclick="btn_eliminar_rede_social_click(this);" title="Editar"><i class="glyphicon glyphicon-trash"></i>&nbsp;<span style="font-size:11px !important;">Eliminar</span></a>';


                        opciones += '</div>';

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


function btn_eliminar_rede_social_click(tab) {
    var row = $(tab).data('orden');

    if ($('#txt_estatus').val() == "ACTIVO" || $('#txt_estatus').val() == "INACTIVO") {

        if (row.Documento_Id == 0) {
            $('#tbl_redes_sociales').bootstrapTable('remove', {
                field: 'Link_ID',
                values: [row.Link_ID],
                field: 'Nombre',
                values: [row.Nombre],
                field: 'Link',
                values: [row.Link],
            });
        }
        else {
            $('#tbl_redes_sociales').bootstrapTable('remove', {
                field: 'Link_ID',
                values: [row.Link_ID],
            });

            $('#tbl_redes_sociales_eliminar').bootstrapTable('insertRow', {
                index: 0,
                row: {
                    Link_ID: row.Link_ID,
                }
            });
        }
    }
    else {
        _mostrar_mensaje('Informe Técnico' + '', 'No puede ser editado');
    }



}

//  -----------------------------------------------------
//  -----------------------------------------------------
function crear_tabla_redes_sociales_eliminadas() {

    try {
        $('#tbl_redes_sociales_eliminar').bootstrapTable('destroy');

        $('#tbl_redes_sociales_eliminar').bootstrapTable({
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

                { field: 'Link_ID', title: 'Link_ID', align: 'center', valign: 'top', visible: true, sortable: true },
              
            ]
        });
    } catch (e) {
        _mostrar_mensaje('Informe Técnico' + '[crear_tabla_vehiculo]', e.message);
    }
}







///*******************Validaciones*************************///
function _validarDatos_Nuevo() {
    var _output = new Object();

    try {
        _output.Estatus = true;
        _output.Mensaje = '';
        //  ---------------------------------------------------------------------------------------
        //  ---------------------------------------------------------------------------------------
        //  datos del evento
        if ($('#txt_clave_evento').val() == '' || $('#txt_clave_evento').val() == undefined) {
            _output.Estatus = false;
            _output.Mensaje += '&nbsp;<i class="fa fa-angle-double-right"></i>&nbsp;La clave del evento.<br />';
        }


        if ($('#txt_nombre_evento').val() == '' || $('#txt_nombre_evento').val() == undefined) {
            _output.Estatus = false;
            _output.Mensaje += '&nbsp;<i class="fa fa-angle-double-right"></i>&nbsp;El nombre del evento.<br />';
        }


        if ($('#cmb_estatus').val() == '' || $('#cmb_estatus').val() == undefined) {
            _output.Estatus = false;
            _output.Mensaje += '&nbsp;<i class="fa fa-angle-double-right"></i>&nbsp;El estatus.<br />';
        }

        if ($('#txt_fecha_inicio_evento').val() == '' || $('#txt_fecha_inicio_evento').val() == undefined) {
            _output.Estatus = false;
            _output.Mensaje += '&nbsp;<i class="fa fa-angle-double-right"></i>&nbsp;La fecha de inicio del evento.<br />';
        }

        if ($('#txt_fecha_fin_evento').val() == '' || $('#txt_fecha_fin_evento').val() == undefined) {
            _output.Estatus = false;
            _output.Mensaje += '&nbsp;<i class="fa fa-angle-double-right"></i>&nbsp;La fecha fin del evento.<br />';
        }

        if ($('#txt_fecha_salida_evento').val() == '' || $('#txt_fecha_salida_evento').val() == undefined) {
            _output.Estatus = false;
            _output.Mensaje += '&nbsp;<i class="fa fa-angle-double-right"></i>&nbsp;La fecha de salida del evento.<br />';
        }


        //if ($('#txt_recorrido_completo').val() == '' || $('#txt_recorrido_completo').val() == undefined) {
        //    _output.Estatus = false;
        //    _output.Mensaje += '&nbsp;<i class="fa fa-angle-double-right"></i>&nbsp;El recorrido completo(Km).<br />';
        //}

        if ($('#txt_punto_salida_evento').val() == '' || $('#txt_punto_salida_evento').val() == undefined) {
            _output.Estatus = false;
            _output.Mensaje += '&nbsp;<i class="fa fa-angle-double-right"></i>&nbsp;El punto de salida.<br />';
        }

        if ($('#txt_punto_meta_evento').val() == '' || $('#txt_punto_meta_evento').val() == undefined) {
            _output.Estatus = false;
            _output.Mensaje += '&nbsp;<i class="fa fa-angle-double-right"></i>&nbsp;El punto de meta.<br />';
        }

        if ($('#txt_hora_salida_evento').val() == '' || $('#txt_hora_salida_evento').val() == undefined) {
            _output.Estatus = false;
            _output.Mensaje += '&nbsp;<i class="fa fa-angle-double-right"></i>&nbsp;La hora de salida.<br />';
        }



        if ($('#txt_intervalo_evento').val() == '' || $('#txt_intervalo_evento').val() == undefined) {
            _output.Estatus = false;
            _output.Mensaje += '&nbsp;<i class="fa fa-angle-double-right"></i>&nbsp;El intervalo.<br />';
        }
        else {
            var intervalo = 0;

            intervalo = parseInt($('#txt_intervalo_evento').val());

            if (intervalo == 0) {
                _output.Estatus = false;
                _output.Mensaje += '&nbsp;<i class="fa fa-angle-double-right"></i>&nbsp;El intervalo no puede ser cero.<br />';
            }
        }

        if ($('#txt_comentarios_evento').val() == '' || $('#txt_comentarios_evento').val() == undefined) {
            _output.Estatus = false;
            _output.Mensaje += '&nbsp;<i class="fa fa-angle-double-right"></i>&nbsp;Los comentarios.<br />';
        }

       

        //  ---------------------------------------------------------------------------------------
        //  ---------------------------------------------------------------------------------------
        if (_output.Mensaje != "") {
            _output.Mensaje = "Favor de proporcionar lo siguiente: <br />" + _output.Mensaje;
        }

        //  ---------------------------------------------------------------------------------------
        //  ---------------------------------------------------------------------------------------

    } catch (e) {
        _mostrar_mensaje('Informe técnico' + '[_validarDatos_Nuevo]', e);
    } finally {
        return _output;
    }
}



//  -----------------------------------------------------
//  -----------------------------------------------------
function parseDate(dateString) {
    //Intercambia el dia y el mes de los formatos de fecha( DD/MM/YYYY o MM/DD/YYYY )
    var dateTime = dateString.split(" ");
    var dateOnly = dateTime[0];
    var dates = dateOnly.split("/");
    var temp = dates[1] + "/" + dates[0] + "/" + dates[2];
    return temp;
}


///********************BD***************///
function Alta() {
    var obj = new Object();

    try {
        var intervalo;

        //  se calcula el tiempo del intervalo basandose en los segundos ingresados
        intervalo = Calcular_Hora($('#txt_intervalo_evento').val());



        obj.Clave = $('#txt_clave_evento').val();
        obj.Nombre = $('#txt_nombre_evento').val();
        obj.Estatus = $('#cmb_estatus').val();
        obj.Fecha_Inicio = parseDate($('#txt_fecha_inicio_evento').val());
        obj.Fecha_Fin = parseDate($('#txt_fecha_fin_evento').val());
        obj.Fecha_Salida = parseDate($('#txt_fecha_salida_evento').val());

        obj.Recorrido = parseFloat($('#txt_recorrido_completo').val());
        obj.Punto_Salida = $('#txt_punto_salida_evento').val();
        obj.Punto_Meta = $('#txt_punto_meta_evento').val();
        obj.Hora_Salida = $('#txt_hora_salida_evento').val();
        //obj.Intervalo_Salida = $('#txt_intervalo_salida_evento').val();
        obj.Intervalo = intervalo;
        obj.Comentarios = $('#txt_comentarios_evento').val();
      

        obj.tbl_redes_sociales = JSON.stringify($('#tbl_redes_sociales').bootstrapTable('getData'));


        var $data = JSON.stringify({ 'jsonObject': JSON.stringify(obj) });

        $.ajax({
            type: 'POST',
            url: 'controllers/EventosController.asmx/Alta',
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


//  -----------------------------------------------------
//  -----------------------------------------------------
function Modificar() {
    var obj = new Object();

    try {
        var intervalo;

        //  se calcula el tiempo del intervalo basandose en los segundos ingresados
        intervalo = Calcular_Hora($('#txt_intervalo_evento').val());


        obj.Evento_Id = parseInt($('#txt_evento_id').val());
        obj.Clave = $('#txt_clave_evento').val();
        obj.Nombre = $('#txt_nombre_evento').val();
        obj.Estatus = $('#cmb_estatus').val();
        obj.Fecha_Inicio = parseDate($('#txt_fecha_inicio_evento').val());
        obj.Fecha_Fin = parseDate($('#txt_fecha_fin_evento').val());
        obj.Fecha_Salida = parseDate($('#txt_fecha_salida_evento').val());

        obj.Recorrido = parseFloat($('#txt_recorrido_completo').val());
        obj.Punto_Salida = $('#txt_punto_salida_evento').val();
        obj.Punto_Meta = $('#txt_punto_meta_evento').val();
        obj.Hora_Salida = $('#txt_hora_salida_evento').val();
        //obj.Intervalo_Salida = $('#txt_intervalo_salida_evento').val();
        obj.Intervalo = intervalo;
        obj.Comentarios = $('#txt_comentarios_evento').val();


        obj.tbl_redes_sociales = JSON.stringify($('#tbl_redes_sociales').bootstrapTable('getData'));
        obj.tbl_redes_sociales_eliminados = JSON.stringify($('#tbl_redes_sociales_eliminar').bootstrapTable('getData'));

        var $data = JSON.stringify({ 'jsonObject': JSON.stringify(obj) });




        $.ajax({
            type: 'POST',
            url: 'controllers/EventosController.asmx/Modificar',
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





function _validar_agregar_link() {
    var _output = new Object();

    try {
        _output.Estatus = true;
        _output.Mensaje = '';
        //  ---------------------------------------------------------------------------------------
        //  ---------------------------------------------------------------------------------------
        //  documento
        if ($('#txt_nombre_link').val() == '' || $('#txt_nombre_link').val() == undefined) {
            _output.Estatus = false;
            _output.Mensaje += '&nbsp;<i class="fa fa-angle-double-right"></i>&nbsp;El nombre.<br />';
        }

        if ($('#txt_ruta_link').val() == '' || $('#txt_ruta_link').val() == undefined) {
            _output.Estatus = false;
            _output.Mensaje += '&nbsp;<i class="fa fa-angle-double-right"></i>&nbsp;El link.<br />';
        }

        if (_output.Mensaje != "") {
            _output.Mensaje = "Favor de proporcionar lo siguiente: <br />" + _output.Mensaje;
        }


    } catch (e) {
        _mostrar_mensaje('Informe técnico' + '[_validar_agregar_documento]', e);
    } finally {
        return _output;
    }
}

//  -----------------------------------------------------
//  -----------------------------------------------------