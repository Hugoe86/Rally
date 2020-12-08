<%@ Page Title="" Language="C#" MasterPageFile="~/Paginas/Paginas_Generales/MasterPage.Master" AutoEventWireup="true" CodeBehind="Frm_Ope_Orden_Trabajo_Produccion.aspx.cs" Inherits="web_trazabilidad.Paginas.Trazabilidad.Frm_Ope_Orden_Trabajo_Produccion" %>

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

    <link href="../../Recursos/estilos/isotope.css" rel="stylesheet" />
    <link href="../../Recursos/estilos/css_orden_trabajo.css" rel="stylesheet" />
    <link href="../../Recursos/estilos/css_orden_trabajo_produccion.css" rel="stylesheet" />
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
    <script src="../../Recursos/javascript/trazabilidad/Js_Ope_Orden_Trabajo_Produccion.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid" style="height: 100vh;">
        <%--DIV DE LOS DATOS GENERALES DE LA ORDEN DE TRABAJO--%>
        <div id="div_orden_trabajo" class="panel panel-color panel-info collapsed">
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

        <div id="contenedor_isotope"></div>

        <br />

        <%--SECCION DEL CONTENEDOR --%>
        <%--  <div id="toolbar" style="margin-left: 5px; text-align: right">
            <div class="btn-group" role="group" style="margin-left: 5px;">
                <button id="btn_inicio" type="button" class="btn btn-primary btn-sm" style="border-radius: 0px !important; border-top-left-radius: 6px !important; border-bottom-left-radius: 6px !important;" title="Inicio"><i class="glyphicon glyphicon-home"></i></button>
                <button id="btn_generar_no_contenedor" type="button" class="btn btn-primary btn-sm" style="border-radius: 0px !important;" title="Crear Contenedor"><i class="glyphicon glyphicon-cog"></i></button>
                <button id="btn_guardar" type="button" class="btn btn-primary btn-sm" style="border-radius: 0px !important; border-top-right-radius: 6px !important; border-bottom-right-radius: 6px !important;" title="Agregar Serial"><i class="glyphicon glyphicon-floppy-disk"></i></button>
            </div>
        </div><br />--%>

        <div class="row">
            <div class="col-md-4">
                <%--Grid del Contenedor --%>
                <table id="grid_contenedores_OP" class="table table-responsive"></table>
            </div>
            <div class="col-md-8" align="left">
                <%--SERIALES --%>
                <div class="panel panel-color panel-info" style="border: solid 0.1px #ccc;">
                    <div class="panel-heading">
                        <h3 class="panel-title">
                            <i style="color: white; font-size: 16px;" class="fa fa-wrench"></i>&nbsp;<label id="lbl_titulo_modal"></label>
                        </h3>
                        <div class="panel-options">
                            <a href="#" id="btn_generar_no_contenedor" title="Crear nuevo contenedor">
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
                                <label id="lbl_no_contenedor_modal" class="fuente_lbl_controles"></label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-3 col-sm-offset-1 etq" align="left">
                                <label class="fuente_lbl_controles">Cantidad Producida</label>
                            </div>
                            <div class="col-sm-7 ctrl ctrl-right ctrl-bottom" align="left">
                                <label id="lbl_cant_producida" class="fuente_lbl_controles"></label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-3 col-sm-offset-1 etq" align="left">
                                <label class="fuente_lbl_controles">Cantidad &times; Contenedor</label>
                            </div>
                            <div class="col-sm-7 ctrl ctrl-right ctrl-bottom" align="left">
                                <label id="lbl_cantidad_contenedor" class="fuente_lbl_controles"></label>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-sm-3 col-sm-offset-1 etq etq-bottom" align="left">
                                <label class="fuente_lbl_controles" id="lbl_stock"></label>
                            </div>
                            <div class="col-sm-5 ctrl ctrl-right" align="left">
                                <input type="text" id="txt_no_serie_producto" maxlength="100" class="form-control" style="height: 25px !important; margin: 2px; font-size: 10px;" />
                            </div>
                            <div class="col-sm-2 ctrl ctrl-right ctrl-bottom" align="right" valign="top">
                                <button id="btn_asignar_serial" type="button" class="btn btn-primary btn-sm" style="border-radius: 0px !important; border-top-left-radius: 6px !important; border-top-right-radius: 6px !important; border-bottom-left-radius: 6px !important; border-bottom-right-radius: 6px !important;">
                                    Agregar</button>
                            </div>
                        </div>

                        <div class="row" style="margin-top: 8px;">
                            <div class="col-sm-10 col-sm-offset-1" align="center" style="padding-right: 0px; padding-left: 0px;">
                                <table id="grid_series" class="table table-responsive"></table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <%--FIN DE LA SECCION DEL CONTENEDOR --%>
    </div>
</asp:Content>
