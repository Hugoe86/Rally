<%@ Page Title="" Language="C#" MasterPageFile="~/Paginas/Paginas_Generales/MasterPage.Master" AutoEventWireup="true" CodeBehind="Frm_Ope_Conteo_Fisico.aspx.cs" Inherits="web_trazabilidad.Paginas.Trazabilidad.Frm_Ope_Conteo_Fisico" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../Recursos/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../../Recursos/estilos/center_loader.css" rel="stylesheet" />
    <link href="../../Recursos/estilos/css_master.css" rel="stylesheet" />
    <link href="../../Recursos/CreativeCSS3/css/style10.css" rel="stylesheet" />

    <script src="../../Recursos/angular/file-saver/FileSaver.js"></script>
    <script src="../../Recursos/angular/pdf-make/pdfmake.min.js"></script>
    <script src="../../Recursos/angular/pdf-make/vfs_fonts.js"></script>
    <script src="../../Recursos/angular/pdf-make/pdf-helper.js"></script>
    <script src="../../Recursos/angular/xslx/xlsx.core.min.js"></script>
    <script src="../../Recursos/angular/xslx/export-to-excel.js"></script>
    <script src="../../Recursos/angular/csv/csv-helper.js"></script>

    <script src="../../Recursos/plugins/center-loader.min.js"></script>
    <script src="../../Recursos/javascript/seguridad/Js_Controlador_Sesion.js"></script>
    <script src="../../Recursos/angular/app/services/ConteoFisicoService.js"></script>
    <script src="../../Recursos/angular/app/controllers/ConteoFisicoController.js"></script>

    <style>
        .header {
            background-color: #3071a9;
            color: #ffffff;
            height: 60px;
            border-radius: 3px 3px 0 0;
            border: 1px solid #3071a9;
            padding-top: 8px;
            border-bottom: 1px solid #ffffff;
        }

        .header-table {
            background-color: #3071a9;
            color: #ffffff !important;
            height: 35px;
            border: 1px solid #3071a9;
            padding-top: 8px;
        }

        .angular-ui-tree-handle {
            background: #f8faff;
            border: 1px solid #dae2ea;
            color: #7c9eb2;
            padding: 10px 10px;
        }

            .angular-ui-tree-handle:hover {
                color: #438eb9;
                background: #f4f6f7;
                border-color: #dce2e8;
            }

        .angular-ui-tree-placeholder {
            background: #f0f9ff;
            border: 2px dashed #bed2db;
            -webkit-box-sizing: border-box;
            -moz-box-sizing: border-box;
            box-sizing: border-box;
        }

        tr.angular-ui-tree-empty {
            height: 100px;
        }

        .group-title {
            background-color: #687074 !important;
            color: #FFF !important;
        }

        /* --- Tree --- */
        .tree-node {
            border: 1px solid #dae2ea;
            background: #f8faff;
            color: #7c9eb2;
            cursor: default;
        }

        .nodrop {
            background-color: #f2dede;
        }

        .tree-node-content {
            margin: 10px;
        }

        .tree-handle {
            padding: 10px;
            background: #428bca;
            color: #FFF;
            margin-right: 10px;
        }

        .angular-ui-tree-handle:hover {
        }

        .angular-ui-tree-placeholder {
            background: #f0f9ff;
            border: 2px dashed #bed2db;
            -webkit-box-sizing: border-box;
            -moz-box-sizing: border-box;
            box-sizing: border-box;
        }

        input.number {
            text-align: right;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid" style="height: 100vh;" ng-controller="ConteoFisicoController">
        <div class="row text-center" ng-show="EsSelectedMenu">
            <div class="col-md-12">
                <ul class="ca-menu">
                    <li ng-click="ExportToExcel(false)">
                        <a href="#">
                            <span class="ca-icon"><i class="fa fa-file-excel-o"></i></span>
                            <div class="ca-content">
                                <h2 class="ca-main">Descargar formato en Excel</h2>
                            </div>
                        </a>
                    </li>
                    <li ng-click="SystemCount(true)">
                        <a href="#">
                            <span class="ca-icon"><i class="fa fa-list-ul"></i></span>
                            <div class="ca-content">
                                <h2 class="ca-main">Conteo en Sistema</h2>
                            </div>
                        </a>
                    </li>
                </ul>
            </div>
        </div>

        <div class="row" ng-show="EsConteoSistema">
            <div class="col-md-12">
                <div class="row">
                    <div class="col-md-6">
                        <h3>Conteo Físico</h3>
                    </div>
                    <div class="col-md-6 pull-right text-right">
                        <div class="form-inline">
                            <button type="button" class="btn btn-success" ng-click="ExportToExcel(true)" ng-show="EstatusHistorico === 'Terminado'">
                                <span class="fa fa-file-excel-o"></span>&nbsp;Exportar
                            </button>
                            <button type="button" class="btn btn-primary" ng-click="Guardar(false)" ng-hide="EstatusHistorico === 'Terminado'">
                                <span class="fa fa-file-text-o"></span>&nbsp;Borrador
                            </button>
                            <button type="button" class="btn btn-success" ng-click="Guardar(true)" ng-hide="EstatusHistorico === 'Terminado'">
                                <span class="fa fa-save"></span>&nbsp;Terminar
                            </button>
                            <button type="button" class="btn btn-danger" ng-click="CloseSystemCount()">
                                <span class="fa fa-remove"></span>&nbsp;Cerrar
                            </button>
                        </div>
                    </div>
                </div>
                <hr style="border-top: 1px solid #dae2ea" />
                <div class="row">
                    <div class="col-md-6">
                        <div class="form-inline">
                            <label class="control-label">Fecha</label>
                            <input type="text" class="form-control input-md"
                                datetimepicker datetimepicker-options="{{DateFormat}}"
                                ng-model="Fecha" readonly style="text-align: center; cursor: pointer !important;"
                                ng-change="FechaHistoricoChange()" />
                        </div>
                    </div>
                    <div class="col-md-6 pull-right text-right">
                        <div class="form-inline">
                            <button type="button" class="btn btn-info" ng-click="ExpandAll()">
                                <span class="fa fa-expand"></span>&nbsp;Expandir
                            </button>
                            <button type="button" class="btn btn-info" ng-click="CollapseAll()">
                                <span class="fa fa-compress"></span>&nbsp;Colapsar
                            </button>
                            <input type="text" class="form-control input-sm" ng-model="FiltroAlmacenes" placeholder="Filtrar" />
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <div ui-tree>
                            <ol ui-tree-nodes ng-model="Almacenes" data-nodrag>
                                <li ng-repeat="almacen in Almacenes | filter:FiltroAlmacenes" ui-tree-node>
                                    <div ui-tree-handle class="tree-node tree-node-content">
                                        <a class="btn btn-success btn-xs" ng-if="almacen.Ubicaciones && almacen.Ubicaciones.length > 0" data-nodrag ng-click="Toggle(this)">
                                            <span class="fa" ng-class="{'fa-chevron-right': collapsed,'fa-chevron-down': !collapsed}"></span>
                                        </a>
                                        <span ng-bind="almacen.NombrePrefijo"></span>
                                    </div>
                                    <ol ui-tree-nodes ng-model="almacen.Ubicaciones" ng-class="{hidden: collapsed}" data-nodrag>
                                        <li ng-repeat="ubicacion in almacen.Ubicaciones | filter:FiltroAlmacenes" ui-tree-node>
                                            <div ui-tree-handle class="tree-node tree-node-content">
                                                <a class="btn btn-success btn-xs" ng-if="ubicacion.Productos && ubicacion.Productos.length > 0" data-nodrag ng-click="Toggle(this)">
                                                    <span class="fa" ng-class="{'fa-chevron-right': collapsed,'fa-chevron-down': !collapsed}"></span>
                                                </a>
                                                <span ng-bind="ubicacion.NombrePrefijo"></span>
                                            </div>
                                            <ol ui-tree-nodes ng-model="ubicacion.Productos" ng-class="{hidden: collapsed}" data-nodrag>
                                                <li ng-repeat="producto in ubicacion.Productos | filter:FiltroAlmacenes" ui-tree-node>
                                                    <div ui-tree-handle class="tree-node tree-node-content">
                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <div class="form-group">
                                                                    <label class="control-label">Código&nbsp;</label>
                                                                    <span ng-bind="producto.Codigo"></span>
                                                                </div>
                                                                <div class="form-group">
                                                                    <label class="control-label">Nombre&nbsp;</label>
                                                                    <span ng-bind="producto.Nombre"></span>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-2">
                                                                <div class="form-group">
                                                                    <label class="control-label">Existencia</label>
                                                                    <input type="text" class="form-control input-sm number"
                                                                        ng-model="producto.Existencia" numeric-only
                                                                        disabled />
                                                                </div>
                                                            </div>
                                                            <div class="col-md-2">
                                                                <form name="frmConteoFisico" role="form" novalidate>
                                                                    <div class="form-group">
                                                                        <label class="control-label">Cantidad Física</label>
                                                                        <input type="text" class="form-control input-sm number"
                                                                            name="txt_cantidad_fisica"
                                                                            ng-model="producto.CantidadFisica" numeric-only
                                                                            ng-disabled="EstatusHistorico === 'Terminado' || producto.Existencia === 0"
                                                                            ng-change="CantidadFisicaChange(producto)"/>
                                                                    </div>
                                                                </form>
                                                            </div>
                                                            <div class="col-md-2">
                                                                <div class="form-group">
                                                                    <label class="control-label">Diferencia</label>
                                                                    <input type="text" class="form-control input-sm number"
                                                                        ng-model="producto.Diferencia" numeric-only
                                                                        disabled />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </li>
                                            </ol>
                                        </li>
                                    </ol>
                                </li>
                            </ol>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
