<%@ Page Title="" Language="C#" MasterPageFile="~/Paginas/Paginas_Generales/MasterPage.Master" AutoEventWireup="true" CodeBehind="Frm_Ope_Produccion_Plasticom.aspx.cs" Inherits="web_trazabilidad.Paginas.Trazabilidad.Frm_Ope_Produccion_Plasticom" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../Recursos/bootstrap/css/bootstrap.css" rel="stylesheet" />
    <%--Hojas de estilo --%>
    <link href="../../Recursos/estilos/demo_form.css" rel="stylesheet" />
    <link href="../../Recursos/bootstrap-combo/select2.css" rel="stylesheet" />
    <link href="../../Recursos/bootstrap-combo/select2-bootstrap.css" rel="stylesheet" />
    <link href="../../Recursos/bootstrap-table/bootstrap-table.css" rel="stylesheet" />
    <link href="../../Recursos/bootstrap-table/extensions/editable/bootstrap-editable.css" rel="stylesheet" />
    <link href="../../Recursos/bootstrap-date/bootstrap-datetimepicker.css" rel="stylesheet" />
    <link href="../../Recursos/EasyUI 1.3.6/themes/default/easyui.css" rel="stylesheet" />
    <link href="../../Recursos/EasyUI%201.3.6/themes/icon.css" rel="stylesheet" />

    <link href="../../Recursos/estilos/isotope_plastic.css" rel="stylesheet" />
    <link href="../../Recursos/estilos/css_orden_trabajo_plastic.css" rel="stylesheet" />
    <link href="../../Recursos/estilos/css_orden_trabajo_produccion_plastic.css" rel="stylesheet" />
    <%--Javascript --%>
    <script src="../../Recursos/bootstrap-combo/select2.js"></script>
    <script src="../../Recursos/bootstrap-combo/es.js"></script>
    <script src="../../Recursos/bootstrap-table/bootstrap-table.min.js"></script>
    <script src="../../Recursos/bootstrap-table/locale/bootstrap-table-es-MX.min.js"></script>
    <script src="../../Recursos/bootstrap-table/extensions/editable/bootstrap-editable.js"></script>
    <script src="../../Recursos/bootstrap-table/extensions/editable/bootstrap-table-editable.min.js"></script>
    <script src="../../Recursos/bootstrap-date/moment.min.js"></script>
    <script src="../../Recursos/bootstrap-date/bootstrap-datetimepicker.min.js"></script>
    <script src="../../Recursos/plugins/jquery.formatCurrency-1.4.0.min.js"></script>
    <script src="../../Recursos/plugins/jquery.formatCurrency.all.js"></script>
    <script src="../../Recursos/plugins/accounting.min.js"></script>
    <script src="../../Recursos/plugins/pinch.js"></script>
    <script src="../../Recursos/plugins/parsley.js"></script>
    <script src="../../Recursos/EasyUI%201.3.6/jquery.easyui.min.js"></script>
    <script src="../../Recursos/javascript/seguridad/Js_Controlador_Sesion.js"></script>
    <script src="../../Recursos/plugins/URI.min.js"></script>
    <script src="../../Recursos/plugins/isotope/isotope.pkgd.min.js"></script>
    <script src="../../Recursos/javascript/trazabilidad/Js_Imprimir_Datos_Etiqueta.js"></script>
    <script src="../../Recursos/javascript/trazabilidad/Js_Ope_Produccion_Plasticom.js"></script>
    <style>
        /* enable absolute positioning */
        .inner-addon {
            position: relative;
        }

            /* style icon */
            .inner-addon .glyphicon {
                position: absolute;
                padding: 10px;
                pointer-events: none;
                font-size: 25px;
            }

        /* align icon */
        .left-addon .glyphicon {
            left: 0px;
        }

        .right-addon .glyphicon {
            right: 0px;
        }

        /* add padding  */
        .left-addon input {
            padding-left: 40px;
        }

        .right-addon input {
            padding-right: 40px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="container-fluid" style="height: 100vh;">
        <%--DIV DE LOS DATOS GENERALES DE LA ORDEN DE TRABAJO--%>
        <div id="div_orden_trabajo" class="panel panel-color panel-info collapsed" style="display:none">
            <div class="panel-heading">
                <h3 class="panel-title">
                    <i style="color: white;" class="glyphicon glyphicon-list-alt"></i>&nbsp;<label id="lbl_titulo_panel"></label>
                </h3>
                <div class="panel-options">
                    <a href="#" id="btn_inicio" title="Salir">
                        <span class="fa fa-home" style="font-size: 16px; color: #fff;"></span>
                    </a>
                    <a href="#" data-toggle="panel">
                        <span class="collapse-icon">-</span>
                        <span class="expand-icon">+</span>
                    </a>
                </div>
            </div>

            <div class="panel-body">
                <div class="row">
                    <div class="col-md-2 etq">
                        <label>No Orden</label>
                    </div>
                    <div class="col-md-2 ctrl">
                        <label id="lbl_no_orden_trabajo"></label>
                    </div>
                    <div class="col-md-2 etq">
                        <label>Prioridad</label>
                    </div>
                    <div class="col-md-2 ctrl">
                        <label id="lbl_prioridad"></label>
                    </div>
                    <div class="col-md-2 etq">
                        <label>Estatus</label>
                    </div>
                    <div class="col-md-2 ctrl ctrl-right">
                        <label id="lbl_estatus"></label>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-2 etq">
                        <label>Cantidad Producir</label>
                    </div>
                    <div class="col-md-2 ctrl">
                        <label id="lbl_cantidad_producir"></label>
                    </div>
                    <div class="col-md-2 etq">
                        <label>Cantidad Producida</label>
                    </div>
                    <div class="col-md-2 ctrl">
                        <label id="lbl_cantidad_producida"></label>
                    </div>
                    <div class="col-md-2 etq">
                        <label>Faltante</label>
                    </div>
                    <div class="col-md-2 ctrl ctrl-right">
                        <label id="lbl_faltante"></label>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-2 etq">
                        <label>Descripci&oacute;n</label>
                    </div>
                    <div class="col-md-10 ctrl ctrl-right">
                        <label id="lbl_descripcion"></label>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-2 etq">
                        <label>Nota</label>
                    </div>
                    <div class="col-md-10 ctrl ctrl-right">
                        <label id="lbl_nota"></label>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-2 etq">
                        <label>Producto</label>
                    </div>
                    <div class="col-md-10 ctrl ctrl-right">
                        <label id="lbl_producto"></label>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-2 etq">
                        <label>Unidad de Medida</label>
                    </div>
                    <div class="col-md-10 ctrl ctrl-right">
                        <label id="lbl_empaque"></label>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-2 etq etq-bottom">
                        <label>Ubicaci&oacute;n</label>
                    </div>
                    <div class="col-md-10 ctrl ctrl-right ctrl-bottom">
                        <label id="lbl_ubicacion"></label>
                    </div>
                </div>
            </div>

        </div>

        <div class="row">
            <div class="col-md-3">
                <div id="contenedor_isotope"></div>
                <%--Grid del Contenedor --%>
            </div>
            <div class="col-md-9" align="left">
                <%--SERIALES --%>
                <div class="panel panel-color panel-info" style="border: solid 0.1px #ccc;">
                    <div class="panel-heading">
                        <h3 class="panel-title">
                            <i style="color: white; font-size: 16px;" class="fa fa-wrench"></i>&nbsp;<label id="lbl_titulo_modal" style="font-size: 16px; font-weight: bold !important;">PANEL DE PRODUCCI&Oacute;N</label>
                        </h3>
                        <div class="panel-options">
                            <a href="#" id="btn_mostrar_contenedores" title="">
                                <span class="fa fa-archive" style="font-size: 16px; color: #fff;"></span>
                            </a>
                            <a href="#" id="btn_generar_no_contenedor" title="">
                                <span class="fa fa-cogs" style="font-size: 16px; color: #fff;"></span>
                            </a>
                            <a href="#" data-toggle="panel">
                                <span class="collapse-icon">-</span>
                                <span class="expand-icon">+</span>
                            </a>
                        </div>
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-sm-10 col-sm-offset-1 etq" align="left">
                                <label id="lbl_no_contenedor_modal" class="fuente_lbl_controles" style="font-size: 16px !important; font-weight: bold !important;"></label>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-sm-10 col-sm-offset-1 etq" align="left">
                                <label class="fuente_lbl_controles" style="font-size: 16px !important; font-weight: bold !important;">
                                    Cantidad por Contenedor:&nbsp;
                                     <label id="lbl_cantidad_contenedor" class="fuente_lbl_controles" style="font-size: 16px !important; font-weight: bold !important;"></label>
                                </label>
                                <%--                            </div>
                            <div class="col-sm-7 ctrl ctrl-right ctrl-bottom" align="left">--%>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-sm-10 col-sm-offset-1 etq etq-bottom etq-barcode" align="left">
                                      <div class="inner-addon left-addon">
                                    <i class="glyphicon glyphicon-barcode"></i>
                                    <input type="text" id="txt_no_serie_producto" maxlength="100" class="form-control " autofocus="autofocus" style="height: 50px !important; font-size: 18px !important; margin: 1px; color:#000;" />
                                </div>
                            </div>
                        </div>

                        <div class="row" style="margin-top: 8px;">
                            <div class="col-sm-10 col-sm-offset-1" align="center" style="padding-right: 0px; padding-left: 0px;">
                                <table id="grid_series" class="table table-responsive"></table>
                            </div>
                        </div>
                        <div style="display: none">
                            <input type="text" id="txt_producto_id" />
                            <input type="text" id="txt_producto_1" />
                            <input type="text" id="txt_no_inventario" />
                            <%--<table id="grid_contenedores_OP" class="table table-responsive"></table>--%>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <%--FIN DE LA SECCION DEL CONTENEDOR --%>
    </div>
    <div style="height: 100vh;">
        <div id="loader" class="loader_plasticom" style="display: none;"></div>
    </div>
</asp:Content>
