<%@ Page Title="" Language="C#" MasterPageFile="~/Paginas/Paginas_Generales/MasterPage.Master" AutoEventWireup="true" CodeBehind="Frm_Apl_Avisos.aspx.cs" Inherits="admin_trazabilidad.Paginas.Catalogos.Frm_Apl_Avisos1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
      <link href="../../Recursos/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../../Recursos/bootstrap-table/bootstrap-table.css" rel="stylesheet" />
    <link href="../../Recursos/estilos/demo_form.css" rel="stylesheet" />
    <link href="../../Recursos/estilos/center_loader.css" rel="stylesheet" />
    <link href="../../Recursos/estilos/css_master.css" rel="stylesheet" />
    <link href="../../Recursos/kendo/css/kendo.common.min.css" rel="stylesheet" />
    <link href="../../Recursos/kendo/css/kendo.default.min.css" rel="stylesheet" />
    <link href="../../Recursos/kendo/css/kendo.default.mobile.min.css" rel="stylesheet" />

    <script src="../../Recursos/bootstrap/js/bootstrap.min.js"></script>
    <script src="../../Recursos/bootstrap-table/bootstrap-table.js"></script>
    <script src="../../Recursos/bootstrap-table/locale/bootstrap-table-es-MX.js"></script>
    <script src="../../Recursos/kendo/js/kendo.all.min.js"></script>
    <script src="../../Recursos/kendo/js/cultures/kendo.culture.es-MX.min.js"></script>
    <script src="../../Recursos/javascript/seguridad/Js_Controlador_Sesion.js"></script>
    <script src="../../Recursos/javascript/catalogos/Js_Apl_Aviso.js"></script>

    <style>
        .table thead tr th {
            background-color: #fff !important;
            color: black !important;
            padding: 3px 3px;
        }
        .fixed-table-container thead th .th-inner {
            font-family: Tahoma !important;
            font-size: 12px !important;
            font-weight:600 !important;
        }
        .Fecha_Fin_Vigencia,  .Fecha_Inicio_Vigencia {
            width: 95% !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid" style="height: 100vh;">
        <div id="div_consulta_mensajes">
            <div class="row">
                <div class="col-sm-12 text-left" style="background-color: white!important;">
                    <h3>Catálogo de Avisos</h3>
                </div>
            </div>
            <hr />
            <div class="row">
                <div class="col-md-12">
                    <div id="toolbar" style="margin-left: 5px; text-align: right">
                        <div class="btn-group" role="group" style="margin-left: 5px;">
                            <button id="btn_inicio" type="button" class="btn btn-info btn-sm" style="border-radius: 0px !important; border-top-left-radius: 6px !important; border-bottom-left-radius: 6px !important;"><i class="glyphicon glyphicon-home"></i></button>
                            <button id="btn_nuevo" type="button" class="btn btn-info btn-sm" style="border-radius: 0px !important; border-top-right-radius: 6px !important; border-bottom-right-radius: 6px !important;"><i class="glyphicon glyphicon-plus"></i></button>
                        </div>
                    </div>
                    <table id="tbl_avisos" data-toolbar="#toolbar" class="table table-responsive"></table>
                </div>
            </div>
        </div>
        <div id="div_crear_mensaje" style="display: none">
            <div class="row">
                <div class="col-sm-8 text-left" style="background-color: white!important;">
                    <h3>Avisos</h3>
                </div>
                <div class="col-sm-4" style="text-align: right">
                    <button id="btn_cancelar" type="button" class="btn btn-danger"><i class="fa fa-reply"></i>&nbsp;Regresar</button>
                    <button id="btn_guardar" type="button" class="btn btn-primary" title="">
                        <i class="glyphicon glyphicon-floppy-disk"></i>&nbsp;&nbsp;Guardar
                    </button>
                    <button id="preview" type="button" class="btn btn-success"><i class="fa fa-refresh"></i>&nbsp;Run</button>
                </div>
            </div>
            <div class="row">
                <div class="col-md-6">
                    <span class="fuente_lbl_controles">*Fecha de Vigencia Inicio</span>
                    <div class="demo-section k-content">
                        <input id="start" style="width: 100%;" />
                        <input type="hidden" id="txt_aviso_id" />
                    </div>
                </div>
                <div class="col-md-6">
                    <span class="fuente_lbl_controles">*Fecha de Vigencia Termino</span>
                    <div class="demo-section k-content">
                        <input id="end" style="width: 100%;" />

                    </div>
                </div>
            </div>
            <div class="row">
                <%--<div class="col-md-6">
                <span class="fuente_lbl_controles"></span>
                <textarea id="editor2" rows="10" cols="20" class="form-control" style="height: 440px !important; resize: none;">
                </textarea>
            </div>--%>
                <div class="col-md-12">
                    <span class="fuente_lbl_controles">* Mensaje</span>
                    <textarea id="editor" rows="10" cols="0" class="form-control" style="height: 200px !important">
                </textarea>
                    <%--<textarea ng-bind-html="html" style="width: 100%; height: 5em"></textarea>--%>
                </div>
            </div>
            <div class="row" style="margin-top: 5px;">
                <div class="col-md-12">
                    <div class="panel panel-color panel-info" id="panel_1">
                        <div class="panel-heading">
                            <h3 class="panel-title">
                                <i style="color: white;" class="fa fa-co"></i>&nbsp;Vista Previa
                            </h3>
                            <div class="panel-options">
                                <a id="ctrl_panel" href="#" data-toggle="panel">
                                    <span class="collapse-icon">–</span>
                                    <span class="expand-icon">+</span>
                                </a>
                            </div>
                        </div>
                        <div class="panel-body">
                            <div id="div_preview">
                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </div>
</asp:Content>
