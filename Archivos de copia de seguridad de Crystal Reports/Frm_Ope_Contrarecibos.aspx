<%@ Page Title="" Language="C#" MasterPageFile="~/Paginas/Paginas_Generales/MasterPage.Master" AutoEventWireup="true" CodeBehind="Frm_Ope_Contrarecibos.aspx.cs" Inherits="web_trazabilidad.Paginas.Trazabilidad.Frm_Ope_Contrarecibos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--Hojas de estilo --%>
    <link href="../../Recursos/estilos/demo_form.css" rel="stylesheet" />
    <link href="../../Recursos/EasyUI 1.3.6/themes/default/easyui.css" rel="stylesheet" />
    <link href="../../Recursos/EasyUI%201.3.6/themes/icon.css" rel="stylesheet" />
    <link href="../../Recursos/bootstrap-combo/select2.css" rel="stylesheet" />
    <link href="../../Recursos/bootstrap-combo/select2-bootstrap.css" rel="stylesheet" />
    <link href="../../Recursos/bootstrap-table/bootstrap-table.css" rel="stylesheet" />
    <link href="../../Recursos/bootstrap-table/extensions/editable/bootstrap-editable.css" rel="stylesheet" />
    <link href="../../Recursos/bootstrap-date/bootstrap-datetimepicker.css" rel="stylesheet" />
    <link href="../../Recursos/estilos/css_orden_compra.css" rel="stylesheet" />
    <link href="../../Recursos/estilos/css_contrarecibos.css" rel="stylesheet" />

    <%--Javascript --%>
    <script src="../../Recursos/bootstrap-table-current/bootstrap-table.js"></script>
    <script src="../../Recursos/bootstrap-table-current/locale/bootstrap-table-es-MX.js"></script>
    <script src="../../Recursos/bootstrap-table-current/extensions/export/bootstrap-table-export.js"></script>
    <script src="../../Recursos/bootstrap-table-current/extensions/export/tableExport.js"></script>

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
    <script src="../../Recursos/EasyUI%201.3.6/locale/easyui-lang-es.js"></script>
    <script src="../../Recursos/EasyUI%201.3.6/jquery.edatagrid.js"></script>
    <script src="../../Recursos/EasyUI%201.3.6/datagrid-filter.js"></script>
    <script src="../../Recursos/javascript/seguridad/Js_Controlador_Sesion.js"></script>
    <script src="../../Recursos/javascript/trazabilidad/Js_Ope_Contrarecibos.js"></script>
    <style>
        .panel {
            overflow: initial;
        }

        .panel-body {
            overflow: initial;
            padding-top: 0px !important;
        }

        .panel-heading {
            margin-left: 0px !important;
            margin-right: 0px !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid" style="height: 100vh;">
        <div class="page-header">
            <div class="row">
                <div class="col-sm-12 text-left" style="background-color: white!important;">
                    <h3>Recepci&oacute;n de Material</h3>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <hr />
            </div>
        </div>
        
        <div id="div_busqueda">
        <div class="panel panel-color panel-info" id="panel_1" style="overflow: inherit;">
            <div class="panel-heading">
                <h3 class="panel-title">
                    <i style="color: white;" class="glyphicon glyphicon-filter"></i>&nbsp;Filtros de b&uacute;squeda
                </h3>
                <div class="panel-options">
                    <a id="ctrl_panel" href="#" data-toggle="panel">
                        <span class="collapse-icon">–</span>
                        <span class="expand-icon">+</span>
                    </a>
                </div>
            </div>
            <div class="panel-body" style="overflow: inherit;">
                <div id="div_filtros">
                    <div class="row">
                        <div class="col-md-2">
                            <label class="text-bold text-left text-medium fuente_lbl_controles" title="No Orden Compra">No O. C.</label>
                            <input type="text" id="txt_no_orden_compra" class="form-control" placeholder="No. Orden de Compra" style="margin: 0px;" />
                        </div>
                        <div class="col-md-2">
                            <label class="text-bold text-left text-medium fuente_lbl_controles" title="No Contrarecibo">No C. R.</label>
                            <input type="text" id="txt_no_contrarecibo" class="form-control" placeholder="No. Contrarecibo" style="margin: 0px;" />
                        </div>
                        <div class="col-md-5">
                            <label class="text-bold text-left text-medium fuente_lbl_controles">Proveedor</label>
                            <select id="cmb_proveedores" class="form-control"></select>
                        </div>
                        <div class="col-md-3">
                            <label class="text-bold text-left text-medium fuente_lbl_controles">Estatus</label>
                            <select id="cmb_estatus" class="form-control"></select>
                        </div>
                    </div>

                    <div class="row" style="margin-top: 7px;"></div>

                    <div class="row">
                        <div class="col-md-2">
                            <label class="text-bold text-left text-medium fuente_lbl_controles" title="Fecha Inicio">Fecha Inicio</label>
                            <div class="input-group date" id="dtp_fecha_inicio">
                                <input type="text" id="fi" class="form-control" style="margin: 0px;" placeholder="mm/dd/aaaa" />
                                <span class="input-group-addon">
                                    <span class="glyphicon glyphicon-calendar"></span>
                                </span>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <label class="text-bold text-left text-medium fuente_lbl_controles" title="Fecha Fin">Fecha Fin</label>
                            <div class="input-group date" id="dtp_fecha_termino">
                                <input type="text" id="ff" class="form-control" style="margin: 0px;" placeholder="mm/dd/aaaa" />
                                <span class="input-group-addon">
                                    <span class="glyphicon glyphicon-calendar"></span>
                                </span>
                            </div>
                        </div>
                        <div class="col-md-3 col-md-offset-5">
                            <button id="btn_busqueda" type="button" class="btn btn-info btn-sm" title="" style="width: 100%; overflow:hidden; text-overflow:ellipsis; white-space:nowrap;">
                                <i class="glyphicon glyphicon-search"></i>&nbsp;&nbsp;Buscar &Oacute;rdenes de Compra
                            </button>
                        </div>
                    </div>
                </div>
            </div>
        </div>


        <div class="row">
            <div class="col-md-12">
                <div id="toolbar" style="margin-left: 5px;">
                    <div class="btn-group" role="group" style="margin-left: 5px;">
                        <button id="btn_salir" type="button" class="btn btn-info btn-sm" style="border-bottom-right-radius:0px !important; border-top-right-radius: 0px !important;" title=""><i class="glyphicon glyphicon-home"></i></button>
                        <button id="btn_exportar" type="button" class="btn btn-info btn-sm" style="border-bottom-left-radius:0px !important; height:30px; border-top-left-radius: 0px !important;" title="Exportar Excel"><i class="fa fa-download"></i></button>
                    </div>
                </div>
                <table id="grid_busqueda" data-toolbar="#toolbar" class="table table-responsive"></table>
            </div>
        </div> 

        </div>
        <%--DIV PARA GUARDAR LA INFORMACION --%>
        <div id="div_contrarecibo">

            <div class="row">
                <div class="col-md-1" style="margin-top: 8px;">
                    <label class="text-bold text-left text-medium fuente_lbl_controles">Orden de Compra</label>
                </div>
                <div class="col-md-2">
                    <input type="text" id="txt_no_compra" class="form-control" disabled="disabled" />
                </div>
                <div class="col-md-1" style="margin-top: 8px;">
                    <label class="text-bold text-left text-medium fuente_lbl_controles">Contrarecibo</label>
                </div>
                <div class="col-md-2">
                    <input type="text" id="txt_no_contrarecibo_alta" class="form-control" disabled="disabled" />
                </div>
                <div class="col-md-6 text-right">
                    <button id="btn_guardar" type="button" class="btn btn-info btn-sm" title="">
                        <i class="glyphicon glyphicon-floppy-disk"></i>&nbsp;&nbsp;Guardar
                    </button>
                    <button id="btn_cancelar" type="button" class="btn btn-info btn-sm" title="" style="margin-right: 10px;">
                        <i class="glyphicon glyphicon-remove"></i>&nbsp;&nbsp;Cancelar
                    </button>
                </div>
            </div>


            <div class="row">
                <div class="col-md-12">
                    <table id="grid_ordenes_detalles" class="easyui-datagrid"></table>
                </div>
            </div>

            <div class="row">
                <div class="col-md-12">
                    <table id="grid_detalles" class="easyui-datagrid"></table>
                </div>

            </div>

        </div>

        <div id="div_material_recibido">
            <div class="row">
                <div class="col-md-2">
                    <label class="fuente_lbl_controles">No Orden Compra</label>
                </div>
                <div class="col-md-4">                    
                    <input type="text" id="txt_no_compra_id" disabled="disabled" class="form-control" />
                </div>
                <div class="col-md-2">
                    <label class="fuente_lbl_controles">No Recepción</label>
                </div>
                <div class="col-md-4">
                    <input type="text" id="txt_no_recepcion" disabled="disabled" class="form-control"/>
                </div>
            </div>

            <div id="" style="margin-left: 5px;">
                <div class="btn-group" role="group" style="margin-left: 5px;text-align:right;" >
                    <button id="btn_regresar" type="button" class="btn btn-info btn-sm" title=""><i class="fa fa-reply"></i></button>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <table id="grid_detalles_contrarecibo" class="easyui-datagrid"></table>
                </div>
            </div>
        </div>
    </div>

</asp:Content>


