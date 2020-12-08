
/* =============================================
--NOMBRE_FUNCIÓN:       _inicializar_vista_layout_puntos_control
--DESCRIPCIÓN:          Establece los métodos principales del modal de adjuntos
--PARÁMETROS:           NA
--CREO:                 Hugo Enrique Ramírez Aguilera
--FECHA_CREO:           27 de Julio de 2020
--MODIFICÓ:
--FECHA_MODIFICÓ:
--CAUSA_MODIFICACIÓN:
=============================================*/
function _inicializar_vista_layout_puntos_control() {
    try {
        //  se inicializan los controles
        _inicializar_controls_file_layout_puntos_control();
        _eventos_layout_puntos_control();
        _load_cmb_filtro_categorias('cmb_categoria_layout');
        _crear_tabla_layout_puntos_control('#tbl_layout_nuevos_punto_control', 'Nuevo');

    } catch (e) {
        //  se muestra el mensaje del error que se presento
        _mostrar_mensaje('Informe técnico' + '[_inicializar_vista_layout_puntos_control]', e);
    }
}

/* =============================================
--NOMBRE_FUNCIÓN:       _load_cmb_filtro_categorias
--DESCRIPCIÓN:          Carga la información de la base de datos dentro del combo
--PARÁMETROS:           cmb: nombre del control al cual se le cargara la información
--CREO:                 Hugo Enrique Ramírez Aguilera
--FECHA_CREO:           27 de Julio de 2020
--MODIFICÓ:
--FECHA_MODIFICÓ:
--CAUSA_MODIFICACIÓN:
=============================================*/
function _load_cmb_filtro_categorias(cmb) {
    try {
        var obj = new Object();//   se le asignaran a las propiedades los filtros de búsqueda

        //  se convierte a la estructura que pueda leer el controlador
        var $data = JSON.stringify({ 'json_object': JSON.stringify(obj) });//   variable para guardar la información que se le pasara al controlador

        //  se consultara y cargara la información
        $('#' + cmb).select2({
            language: "es",
            theme: "classic",
            placeholder: 'SELECCIONE',
            allowClear: true,
            ajax: {
                url: '../Operaciones/controllers/CategoriasController.asmx/Consultar_Categorias_Combo',
                cache: true,
                dataType: 'json',
                type: "POST",
                delay: 250,
                params: {
                    contentType: 'application/json; charset=utf-8'
                },
                quietMillis: 100,
                /* =============================================
                --NOMBRE_FUNCIÓN:       results: function () {
                --DESCRIPCIÓN:          realiza la operación al recibir los resultados
                --PARÁMETROS:           data: estructura que recibe tras realizar la operación
                --CREO:                 Hugo Enrique Ramírez Aguilera
                --FECHA_CREO:           18 de Agosto de 2020
                --MODIFICÓ:
                --FECHA_MODIFICÓ:
                --CAUSA_MODIFICACIÓN:
                =============================================*/
                results: function (data) {
                    return { results: data };
                },
                /* =============================================
                --NOMBRE_FUNCIÓN:       data: function () {
                --DESCRIPCIÓN:          realiza la operación al recibir los resultados
                --PARÁMETROS:           params: parámetros que estará enviando
                --CREO:                 Hugo Enrique Ramírez Aguilera
                --FECHA_CREO:           18 de Agosto de 2020
                --MODIFICÓ:
                --FECHA_MODIFICÓ:
                --CAUSA_MODIFICACIÓN:
                =============================================*/
                data: function (params) {
                    return {
                        q: params.term,
                        page: params.page,
                        evento_id: parseInt($('#txt_evento_id').val()),
                    };
                },
                /* =============================================
                --NOMBRE_FUNCIÓN:       processResults: function () {
                --DESCRIPCIÓN:          Recibe los resultados
                --PARÁMETROS:           data: datos que esta recibiendo
                --                      page: Pagina que muestra
                --CREO:                 Hugo Enrique Ramírez Aguilera
                --FECHA_CREO:           18 de Agosto de 2020
                --MODIFICÓ:
                --FECHA_MODIFICÓ:
                --CAUSA_MODIFICACIÓN:
                =============================================*/
                processResults: function (data, page) {
                    return {
                        results: data
                    };
                },
            }
        });


    } catch (e) {//  variable para atrapar el error
        //  se muestra el mensaje del error que se presento
        _mostrar_mensaje('Informe técnico' + '[_load_cmb_filtro_categorias]', e);
    }
}


/* =============================================
--NOMBRE_FUNCIÓN:       _limpiar_todos_controles_layout_puntos_control
--DESCRIPCIÓN:          limpia todos los controles que se encuentran dentro del modal
--PARÁMETROS:           NA
--CREO:                 Hugo Enrique Ramírez Aguilera
--FECHA_CREO:           27 de Julio de 2020
--MODIFICÓ:
--FECHA_MODIFICÓ:
--CAUSA_MODIFICACIÓN:
=============================================*/
function _limpiar_todos_controles_layout_puntos_control() {

    try {

        /* =============================================
        --NOMBRE_FUNCIÓN:       input[type=text]
        --DESCRIPCIÓN:          recorre los controles de tipo texto y limpia su valor
        --PARÁMETROS:           NA
        --CREO:                 Hugo Enrique Ramírez Aguilera
        --FECHA_CREO:           27 de Julio de 2020
        --MODIFICÓ:
        --FECHA_MODIFICÓ:
        --CAUSA_MODIFICACIÓN:
        =============================================*/
        $('#modal_layout_puntos_control input[type=text]').each(function () { $(this).val(''); });

        /* =============================================
       --NOMBRE_FUNCIÓN:       input[type=hidden]
       --DESCRIPCIÓN:          recorre los controles de tipo hidden y limpia su valor
       --PARÁMETROS:           NA
       --CREO:                 Hugo Enrique Ramírez Aguilera
       --FECHA_CREO:           27 de Julio de 2020
       --MODIFICÓ:
       --FECHA_MODIFICÓ:
       --CAUSA_MODIFICACIÓN:
       =============================================*/
        $('#modal_layout_puntos_control input[type=hidden]').each(function () { $(this).val(''); });

        //  limpia el valor del combo
        $('#cmb_categoria_layout').empty().trigger("change");


    } catch (e) {//  variable para atrapar el error
        //  se muestra el mensaje del error que se presento
        _mostrar_mensaje('Error Técnico' + ' [_limpiar_todos_controles_layout_puntos_control] ', 'limpiar controles. ' + e);
    }
}


/* =============================================
--NOMBRE_FUNCIÓN:       _limpiar_tablas_puntos_control
--DESCRIPCIÓN:          limpia las tablas
--PARÁMETROS:           NA
--CREO:                 Hugo Enrique Ramírez Aguilera
--FECHA_CREO:           26 Nov 2019
--MODIFICÓ:
--FECHA_MODIFICÓ:
--CAUSA_MODIFICACIÓN:
=============================================*/
function _limpiar_tablas_puntos_control() {
    $('#tbl_layout_nuevos_punto_control').bootstrapTable('load', []);
}

/* =============================================
--NOMBRE_FUNCIÓN:       _crear_tabla_layout_puntos_control
--DESCRIPCIÓN:          Se crea la estructura de la tabla
--PARÁMETROS:           nombre_variable: contiene el nombre de la tabla incluyendo el caracter #
--                      opcion: indica que columnas se muestran
--CREO:                 Hugo Enrique Ramírez Aguilera
--FECHA_CREO:           27 de Julio de 2020
--MODIFICÓ:
--FECHA_MODIFICÓ:
--CAUSA_MODIFICACIÓN:
=============================================*/
function _crear_tabla_layout_puntos_control(nombre_variable, opcion) {

    var estatus = false;//   se le asignaran a las propiedades de las columans



    try {

        //  se le asignara el titulo al botón de nuevo
        switch (opcion) {

            //  se valida la operacion de nuevo, existente
            case 'Nuevo':
            case 'Ya_Existe':

                //  se le otorga la propiedad
                estatus = false;
                break;

            //  se valida la operacion de error
            case 'Error':

                //  se le otorga la propiedad
                estatus = true;
                break;
        }


        //  destruye la estructura de la tabla
        $(nombre_variable).bootstrapTable('destroy');

        //  crea la estructura de la tabla
        $(nombre_variable).bootstrapTable({
            cache: false,
            striped: true,
            pagination: true,
            data: [],
            pageSize: 5,
            pageList: [5, 10, 25, 50],
            smartDysplay: false,
            search: false,
            showColumns: false,
            showRefresh: false,
            minimumCountColumns: 2,

            columns: [


                { field: 'Mensaje_Error', title: 'Error', align: 'left', valign: 'top', visible: estatus, sortable: true },
                { field: 'Clave', title: 'Clave', align: 'left', valign: 'top', visible: true, sortable: true },
                { field: 'Numero', title: 'Numero', align: 'left', valign: 'top', visible: true, sortable: true },
                { field: 'Fecha', title: 'Fecha', align: 'left', valign: 'top', visible: true, sortable: true },
                { field: 'Hora_Inicio', title: 'Hora inicial', align: 'left', valign: 'top', visible: true, sortable: true },
                { field: 'Hora_Fin', title: 'Hora fin', align: 'left', valign: 'top', visible: true, sortable: true },
                { field: 'Ubicacion', title: 'Ubicación', align: 'left', valign: 'top', visible: true, sortable: true },
                { field: 'Renglon', title: 'Renglón', align: 'left', valign: 'top', visible: true, sortable: true },
                { field: 'Distancia', title: 'Distancia', align: 'left', valign: 'top', visible: true, sortable: true },
                { field: 'Seña', title: 'Seña', align: 'left', valign: 'top', visible: true, sortable: true },
                { field: 'Tiempo_Ideal', title: 'Tiempo ideal', align: 'left', valign: 'top', visible: true, sortable: true },
                { field: 'Responsable', title: 'Responsable', align: 'left', valign: 'top', visible: true, sortable: true },
                { field: 'Responsable_Id_Layout', title: 'Responsable Id', align: 'left', valign: 'top', visible: true, sortable: true },
                
            ]
        });
    } catch (e) {
        //  se muestra el mensaje del error que se presento
        _mostrar_mensaje('Informe Técnico' + '[_crear_tabla_layout_puntos_control]', e.message);
    }
}


/* =============================================
--NOMBRE_FUNCIÓN:       _inicializar_controls_file_layout_puntos_control
--DESCRIPCIÓN:          Establece las propiedades del control de fileinput
--PARÁMETROS:           NA
--CREO:                 Hugo Enrique Ramírez Aguilera
--FECHA_CREO:           27 de Julio de 2020
--MODIFICÓ:
--FECHA_MODIFICÓ:
--CAUSA_MODIFICACIÓN:
=============================================*/
function _inicializar_controls_file_layout_puntos_control() {

    $("#fl_layout_puntos_control").fileinput({

        overwriteInitial: false,
        showClose: true,
        showPreview: false,
        browseLabel: '',
        uploadLabel: '',
        removeLabel: '',
        maxFileSize: 10000,
        browseTitle: 'Seleccionar imagen',
        browseIcon: '<i class="glyphicon glyphicon-folder-open"></i>',
        browseClass: 'btn btn-success',
        showUpload: false,
        removeIcon: '<i class="glyphicon glyphicon-remove"></i>',
        removeTitle: 'Cancelar',
        removeClass: 'btn btn-danger',
        uploadClass: 'btn btn-info',
        msgErrorClass: 'alert alert-block alert-danger',
        allowedFileExtensions: ["xlsx"],
        msgInvalidFileExtension: 'Extensión inválida para el archivo "{name}". Solo los archivos "{extensions}" son compatibles.',
        elErrorContainer: '#bugs',
    });
}




/* =============================================
--NOMBRE_FUNCIÓN:       _eventos_layout_puntos_control
--DESCRIPCIÓN:          Crea los eventos del modal que seran utilizados
--PARÁMETROS:           NA
--CREO:                 Hugo Enrique Ramírez Aguilera
--FECHA_CREO:           27 de Julio de 2020
--MODIFICÓ:
--FECHA_MODIFICÓ:
--CAUSA_MODIFICACIÓN:
=============================================*/
function _eventos_layout_puntos_control() {


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
            '        <div class="lazyload"><img src="../../Recursos/img/gears.svg"></img><br/><center><strong style="font-family: Century Gothic; font-size:24px; color: black;">Procesando archivo excel<div class="faa-pulse animated">...</div></strong></center></div> ' +
            '    </div> ' +
            '<div> '
    });




    /* =============================================
     --NOMBRE_FUNCIÓN:       btn_leer_layout_punto_control
     --DESCRIPCIÓN:          Evento con el que se cargar el archivo
     --PARÁMETROS:           e: parametro que se refiere al evento click
     --CREO:                 Hugo Enrique Ramírez Aguilera
     --FECHA_CREO:           27 de Julio de 2020
     --MODIFICÓ:
     --FECHA_MODIFICÓ:
     --CAUSA_MODIFICACIÓN:
     =============================================*/
    $('#btn_leer_layout_punto_control').on('click', function (e) {
        e.preventDefault();

        //  se limpian las tablas
        _limpiar_tablas_puntos_control();

        //  guarda el archivo en el servidor
        var guardar = _guardar_archivo_layout_puntos_control();

    });

    /* =============================================
   --NOMBRE_FUNCIÓN:       btn_subir_layout_punto_control
   --DESCRIPCIÓN:          Evento con el que se cargar el archivo
   --PARÁMETROS:           e: parametro que se refiere al evento click
   --CREO:                 Hugo Enrique Ramírez Aguilera
   --FECHA_CREO:           27 de Julio de 2020
   --MODIFICÓ:
   --FECHA_MODIFICÓ:
   --CAUSA_MODIFICACIÓN:
   =============================================*/
    $('#btn_subir_layout_punto_control').on('click', function (e) {
        e.preventDefault();

        var total_row_nuevos = $('#tbl_layout_nuevos_punto_control').bootstrapTable('getOptions').totalRows;//  variable que indicar el numero de rows de una tabla
        var valida_datos_requerido = _validar_datos_layout_puntos_control();// almacenara el resultado de la validación de los datos

        //  validamos si la información es correcta
        if (valida_datos_requerido.Estatus) {

            if (total_row_nuevos > 0) {
                alta_layout_puntos_control();
            }
            else {
                //  se muestra el mensaje
                _mostrar_mensaje("Validación", "No tiene registros para subir al sistema");
            }
        }
        //  mostramos el mensaje del error
        else {
            //  se muestra el mensaje del error que se presento
            _mostrar_mensaje('Información' + '', valida_datos_requerido.Mensaje);
        }

    });

    /* =============================================
      --NOMBRE_FUNCIÓN:       btn_descargar_plantilla_layout_punto_control
      --DESCRIPCIÓN:          Evento con el que se descarga la plantilla del layout
      --PARÁMETROS:           e: parametro que se refiere al evento click
      --CREO:                 Hugo Enrique Ramírez Aguilera
      --FECHA_CREO:           27 de Julio de 2020
      --MODIFICÓ:
      --FECHA_MODIFICÓ:
      --CAUSA_MODIFICACIÓN:
      =============================================*/
    $('#btn_descargar_plantilla_layout_punto_control').on('click', function (e) {
        e.preventDefault();

        descargar_plantilla_puntos_control();


    });



    /* =============================================
   --NOMBRE_FUNCIÓN:       btn_cancelar_layout_punto_control
   --DESCRIPCIÓN:          Evento con el que se cargar el archivo
   --PARÁMETROS:           e: parametro que se refiere al evento click
   --CREO:                 Hugo Enrique Ramírez Aguilera
   --FECHA_CREO:           27 de Julio de 2020
   --MODIFICÓ:
   --FECHA_MODIFICÓ:
   --CAUSA_MODIFICACIÓN:
   =============================================*/
    $('#btn_cancelar_layout_punto_control').on('click', function (e) {
        e.preventDefault();

        //  se limpian las tablas
        _limpiar_tablas_puntos_control();

        //  se cierra el modal
        _mostrar_vista_jornadas("Principal");
    });

}




/* =============================================
--NOMBRE_FUNCIÓN:       alta_layout_puntos_control
--DESCRIPCIÓN:          se registra el alta
--PARÁMETROS:           NA
--CREO:                 Hugo Enrique Ramírez Aguilera
--FECHA_CREO:           27 de Julio de 2020
--MODIFICÓ:
--FECHA_MODIFICÓ:
--CAUSA_MODIFICACIÓN:
=============================================*/
function alta_layout_puntos_control() {
    var obj = new Object();//   estructura en donde se cargaran la información del formulario

    try {

        //  se carga la información 
        obj.Evento_Id = parseInt($('#txt_evento_id').val());
        obj.Jornada_Id = parseInt($('#txt_jornada_id_layout_puntos_control').val());
        obj.Categoria_Id_Layout = parseInt($('#cmb_categoria_layout :selected').val());
        obj.Tabla_Layout = JSON.stringify($('#tbl_layout_nuevos_punto_control').bootstrapTable('getData'));

        //  se convierte a la estructura que pueda leer el controlador
        var $data = JSON.stringify({ 'json_object': JSON.stringify(obj) });//   variable para guardar la informacion que se le pasara al controlador

        //  se inicia la barra de progreso
        NProgress.start();

        //  se muestra la barra de progreso
        show_loading_bar({
            wait: 0,
            delay: 0.5,
            pct: 100,

            /* =============================================
            --NOMBRE_FUNCIÓN:       success: function () {
            --DESCRIPCIÓN:          se realiza si la operacion se realizo
            --PARÁMETROS:           datos: estructura que recibe tras realizar la operacion
            --CREO:                 Hugo Enrique Ramírez Aguilera
            --FECHA_CREO:           09 de Septiembre de 2020
            --MODIFICÓ:
            --FECHA_MODIFICÓ:
            --CAUSA_MODIFICACIÓN:
            =============================================*/
            finish: function () {

                //  se realiza la petición
                $.ajax({
                    type: 'POST',
                    url: 'controllers/EventosPtsCtrlController.asmx/Alta_Layout',
                    data: $data,
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    cache: false,

                    /* =============================================
                    --NOMBRE_FUNCIÓN:       success: function () {
                    --DESCRIPCIÓN:          recorre la estructura de la tabla
                    --PARÁMETROS:           datos: estructura que recibe tras realizar la operacion
                    --CREO:                 Hugo Enrique Ramírez Aguilera
                    --FECHA_CREO:           27 de Julio de 2020
                    --MODIFICÓ:
                    --FECHA_MODIFICÓ:
                    --CAUSA_MODIFICACIÓN:
                    =============================================*/
                    success: function (datos) {

                        //  validamos si tiene alguna información la variable
                        if (datos != null) {

                            var result = JSON.parse(datos.d);// almacena la información recibida

                            //  validamos si la operación fue exitosa, de no ser así se mostrara el error
                            if (result.Estatus == 'success') {

                                //  se detiene la barra de progreso
                                NProgress.done();

                                //  se muestra el mensaje
                                _mostrar_mensaje(result.Titulo, result.Mensaje);

                                //  se limpia el modal
                                _limpiar_todos_controles_layout_puntos_control();
                                _limpiar_tablas_puntos_control();

                                //  se cierra el modal
                                _mostrar_vista_jornadas("Principal");

                                //  se consultan la informacion
                                _Consultar_Jornadas();

                            }
                            //  si se genera un error, se notifica al usuario
                            else {

                                //  se detiene la barra de progreso
                                NProgress.done();

                                //  se muestra el mensaje del error que se presento
                                _mostrar_mensaje(result.Titulo, result.Mensaje);
                            }
                        }
                    }
                });
            }
        });


    } catch (e) {//  variable para atrapar el error

        //  se detiene la barra de progreso
        NProgress.done();

        //  se muestra el mensaje del error que se presento
        _mostrar_mensaje('Informe técnico' + '[alta_layout_puntos_control]', e);
    }

}


/* =============================================
--NOMBRE_FUNCIÓN:       alta_layout_puntos_control
--DESCRIPCIÓN:          se registra el alta
--PARÁMETROS:           NA
--CREO:                 Hugo Enrique Ramírez Aguilera
--FECHA_CREO:           27 de Julio de 2020
--MODIFICÓ:
--FECHA_MODIFICÓ:
--CAUSA_MODIFICACIÓN:
=============================================*/
function descargar_plantilla_puntos_control() {
    var obj = new Object();//   estructura en donde se cargaran la información del formulario

    try {


        //_exportar_excel();
        jQuery.ajax({
            type: 'post',
            url: 'controllers/EventosPtsCtrlController.asmx/Descargar_Plantilla_Layout',
            dataType: "text",
            async: true,
            cache: false,

            /* =============================================
            --NOMBRE_FUNCIÓN:       success
            --DESCRIPCIÓN:          recupera el resultado devuleto por el controlador
            --PARÁMETROS:           data: almacena la respuesta en formato json devuelta por el controlador
            --CREO:                 Juan Carlos Gómez Rangel
            --FECHA_CREO:           7 de Agosto de 2020
            --MODIFICÓ:
            --FECHA_MODIFICÓ:
            --CAUSA_MODIFICACIÓN:
            =============================================*/
            success: function (data) {

                //extraemos el resultado de la respuesta xml del controlador
                data = data.substr(76, (data.length - 85));
                //variable para almacenar la respuesta del controlador en formato json
                var datos = JSON.parse(data);

                //se evalua si el resultado obtenido de la busqueda es diferente a nulo y si la operacion se ejecutó correctamente
                if (datos !== null && datos.Estatus == "success") {

                    //se descarga el archivo
                    window.location = "../Ayudante/Frm_Ayudante_Descarga_Excel.aspx?Url="
                        + datos.Ruta_Archivo_Excel + "&Nombre=" + datos.Nombre_Excel;


                    //se evalua si el resultado obtenido de la busqueda es diferente a nulo y si la operacion se ejecutó correctamente
                } else {

                    //se muestra el mensaje de error
                    _mostrar_mensaje("Error", datos.Mensaje);
                }
            },
            error: function (error) {//variable que almacena la información del error ajax

                //se muestra el mensaje de error
                _mostrar_mensaje("Error", "falló respuesta del controlador" + error);
            }
        });


    } catch (e) {//  variable para atrapar el error



        //  se muestra el mensaje del error que se presento
        _mostrar_mensaje('Informe técnico' + '[alta_layout_puntos_control]', e);
    }

}

/* =============================================
--NOMBRE_FUNCIÓN:       _guardar_archivo_layout_puntos_control
--DESCRIPCIÓN:          Guarda el archivo dentro del servidor
--PARÁMETROS:           NA
--CREO:                 Hugo Enrique Ramírez Aguilera
--FECHA_CREO:           27 de Julio de 2020
--MODIFICÓ:
--FECHA_MODIFICÓ:
--CAUSA_MODIFICACIÓN:
=============================================*/
function _guardar_archivo_layout_puntos_control() {

    var archivos = $("#fl_layout_puntos_control").get(0).files;//  variable con la que se obtiene el documento
    var data = new FormData();//    variabla para saber los bits del documento
    var ruta = '';//    variable para saber la ruta en donde se guardara el archivo
    var salida = new Object();//  variable para almacenar el resultado de la operacion
    var archivo_final;//    variable para el archivo final

    salida.Estatus = false;

    try {

        //  validamos que exista el documento
        if (archivos.length > 0) {

            var nombre = archivos[0].name;//    variable para obtener el nombre del documento
            var ruta_importacion = "Reportes/Importaciones";//  variable para obtener la ruta en donde se guardara el documento


            data.append("file", archivos[0]);
            data.append("nombre", nombre);
            data.append("url_", ruta_importacion);
            data.append("tipo", archivos[0].type);

            ruta = '../../../' + ruta_importacion + '/' + archivos[0].name;

            var guardar = _guardar_archivo_directorio_layout_puntos_control(data);//  variable para guardar el valor resultante de la operacion

            //  validamos que la operacion sea exitos
            if (guardar.Estatus === "success") {
                _leer_archivo_layout_puntos_control_masivo(archivos[0], ruta, archivos[0].type);
            }
        }
    } catch (e) {

        salida.Estatus = false;

        //  se muestra el mensaje del error que se presento
        _mostrar_mensaje('Informe Técnico' + '[_guardar_archivo_layout_puntos_control]', e.message);
    }

    return salida;
}


/* =============================================
--NOMBRE_FUNCIÓN:       _guardar_archivo_directorio_layout_puntos_control
--DESCRIPCIÓN:          Guarda el archivo dentro de una carpeta temporal
--PARÁMETROS:           data: información del archivo a subir
--CREO:                 Hugo Enrique Ramírez Aguilera
--FECHA_CREO:           27 de Julio de 2020
--MODIFICÓ:
--FECHA_MODIFICÓ:
--CAUSA_MODIFICACIÓN:
=============================================*/
function _guardar_archivo_directorio_layout_puntos_control(data) {
    var estatus = false;//  variable para establecer el estatus de la accion realizada
    var resultado = '';//   variable para indicar el resultado obtenido
    try {
        $.ajax({
            type: "POST",
            url: "../../FileUploadHandler.ashx",
            contentType: false,
            processData: false,
            data: data,
            async: false,
            success: function (result) {
                resultado = JSON.parse(result);

                //  validamos que tenga informacion
                if (result) {
                    estatus = true;
                }
            }
        });
    } catch (e) {
        estatus = false;
        //  se muestra el mensaje del error que se presento
        _mostrar_mensaje('Informe Técnico' + '[_guardar_archivo_directorio_layout_puntos_control]', e.message);
    }
    return resultado;
}


/* =============================================
--NOMBRE_FUNCIÓN:       _leer_archivo_layout_puntos_control_masivo
--DESCRIPCIÓN:          Guarda el archivo en la base de datos
--PARÁMETROS:           nombre: nombre del archivo que se guardara
--                      url: ruta en donde se almacenara el archivo
--                      tipo: tipo del archivo que se estara subiendo
--CREO:                 Hugo Enrique Ramírez Aguilera
--FECHA_CREO:           27 de Julio de 2020
--MODIFICÓ:
--FECHA_MODIFICÓ:
--CAUSA_MODIFICACIÓN:
=============================================*/
function _leer_archivo_layout_puntos_control_masivo(nombre, url, tipo) {
    var obj = new Object();//   estructura en donde se cargaran la información del formulario


    try {

        //  se carga la información 
        obj.Nombre = nombre.name;
        obj.Tipo = tipo;
        obj.Ruta = url;

        //  se convierte a la estructura que pueda leer el controlador
        var $data = JSON.stringify({ 'json_object': JSON.stringify(obj) });//   variable para guardar la informacion que se le pasara al controlador


        //  se inicia la barra de progreso
        NProgress.start();

        //  se muestra la barra de progreso
        show_loading_bar({
            wait: 0,
            delay: 0.5,
            pct: 100,

            /* =============================================
            --NOMBRE_FUNCIÓN:       success: function () {
            --DESCRIPCIÓN:          se realiza si la operacion se realizo
            --PARÁMETROS:           datos: estructura que recibe tras realizar la operacion
            --CREO:                 Hugo Enrique Ramírez Aguilera
            --FECHA_CREO:           09 de Septiembre de 2020
            --MODIFICÓ:
            --FECHA_MODIFICÓ:
            --CAUSA_MODIFICACIÓN:
            =============================================*/
            finish: function () {
                //  ejecutamos la peticion
                $.ajax({
                    type: 'POST',
                    url: 'controllers/EventosPtsCtrlController.asmx/Leer_Archivo_Layout_Puntos_Control',
                    data: $data,
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    cache: false,
                    success: function (datos) {

                        //  validamos si tiene alguna información la variable
                        if (datos != null) {

                            var result = JSON.parse(datos.d);// almacena la información recibida

                            //  validamos si la operación fue exitosa, de no ser así se mostrara el error
                            if (result.Estatus == true) {

                                //  se detiene la barra de progreso
                                NProgress.done();

                                var result = JSON.parse(datos.d);// almacena la información recibida

                                //  se valida que tenga información la variable recibida
                                var nuevos = (result.Nuevos == undefined || result.Nuevos == null) ? '[]' : result.Nuevos;

                                //  se carga la información
                                $('#tbl_layout_nuevos_punto_control').bootstrapTable('load', JSON.parse(nuevos));

                            }
                            //  se muestra el error que se presento
                            else {

                                //  se detiene la barra de progreso
                                NProgress.done();

                                _mostrar_mensaje(result.Titulo, result.Mensaje);
                            }
                        }
                    }
                });
            }
        });


    } catch (e) {

        //  se detiene la barra de progreso
        NProgress.done();

        //  se muestra el mensaje del error que se presento
        _mostrar_mensaje('Informe Técnico' + '[_leer_archivo_layout_puntos_control_masivo]', e.message);
    }

}



/* =============================================
--NOMBRE_FUNCIÓN:       _validarDatos_nuevo
--DESCRIPCIÓN:          Se valida la información requerida para realizar una acción
--PARÁMETROS:           NA
--CREO:                 Hugo Enrique Ramírez Aguilera
--FECHA_CREO:           27 de Julio de 2020
--MODIFICÓ:
--FECHA_MODIFICÓ:
--CAUSA_MODIFICACIÓN:
=============================================*/
function _validar_datos_layout_puntos_control() {
    var _output = new Object();//   se le asignaran a las propiedades los filtros de búsqueda

    try {
        //  se limpian los estilos de los controles
        _clear_all_class_error_layout_puntos_control();

        //  se inicializan las propiedades
        _output.Estatus = true;
        _output.Mensaje = '';

      
        //  ---------------------------------------------------------------------------------------
        //  ---------------------------------------------------------------------------------------
        //  filtro para el estatus
        if ($('#cmb_categoria_layout :selected').val() === undefined || $('#cmb_categoria_layout :selected').val() === '') {
            //  agregamos el estilo al control que no cumple
            _add_class_error("cmb_categoria_layout");

            //  se indica el estatus como no cumplido
            _output.Estatus = false;

            //  se asigna el mensaje que se mostrara
            _output.Mensaje += '&nbsp;<i class="fa fa-angle-double-right"></i>&nbsp;La categoría.<br />';
        }

        //  ---------------------------------------------------------------------------------------
        //  ---------------------------------------------------------------------------------------
        //  validamos si no cumple alguno de los elementos
        if (_output.Mensaje != "") {
            //  asignamos el titulo del mensaje
            _output.Mensaje = "Favor de proporcionar lo siguiente: <br />" + _output.Mensaje;
        }

        //  ---------------------------------------------------------------------------------------
        //  ---------------------------------------------------------------------------------------

    } catch (e) {//  variable para atrapar el error
        //  se muestra el mensaje del error que se presento
        _mostrar_mensaje('Informe técnico' + '[_validarDatos_nuevo]', e);
    } finally {
        return _output;//   se regresa la variable
    }
}

/* =============================================
--NOMBRE_FUNCIÓN:       _clear_all_class_error
--DESCRIPCIÓN:          Quita los estilos de error de todos los controles del modal
--PARÁMETROS:           NA
--CREO:                 Hugo Enrique Ramírez Aguilera
--FECHA_CREO:           27 de Julio de 2020
--MODIFICÓ:
--FECHA_MODIFICÓ:
--CAUSA_MODIFICACIÓN:
=============================================*/
function _clear_all_class_error_layout_puntos_control() {

    /* =============================================
    --NOMBRE_FUNCIÓN:       modal_beneficiarios
    --DESCRIPCIÓN:          remueve todos los estilos de error de los controles
    --PARÁMETROS:           NA
    --CREO:                 Hugo Enrique Ramírez Aguilera
    --FECHA_CREO:           27 de Julio de 2020
    --MODIFICÓ:
    --FECHA_MODIFICÓ:
    --CAUSA_MODIFICACIÓN:
    =============================================*/
    $('#modal_layout_puntos_control input[type=text]').each(function (index, element) {
        _remove_class_error($(this).attr('id'));
    });
}
