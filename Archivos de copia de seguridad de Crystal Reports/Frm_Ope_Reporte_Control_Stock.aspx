<%@ Page Title="" Language="C#" MasterPageFile="~/Paginas/Paginas_Generales/MasterPage.Master" AutoEventWireup="true" CodeBehind="Frm_Ope_Reporte_Control_Stock.aspx.cs" Inherits="web_trazabilidad.Paginas.Catalogos.Frm_Ope_Reporte_Control_Stock" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../Recursos/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../../Recursos/estilos/center_loader.css" rel="stylesheet" />
    <link href="../../Recursos/estilos/css_master.css" rel="stylesheet" />

    <script src="../../Recursos/plugins/center-loader.min.js"></script>
    <script src="../../Recursos/javascript/seguridad/Js_Controlador_Sesion.js"></script>

    <script src="../../Recursos/angular/app/services/ControlStockService.js"></script>
    <script src="../../Recursos/angular/app/controllers/ReporteControlStockController.js"></script>

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
            color: #ffffff;
            height: 35px;
            border: 1px solid #3071a9;
            padding-top: 8px;
        }

        [contenteditable]:hover {
            cursor: text;
            border-radius: 3px;
            background-color: #dedede;
            padding: 3px;
        }

        [contenteditable]:focus {
            border: 1px solid #3071a9;
            border-radius: 3px;
            background-color: #ffffff;
            padding: 3px;
        }

        .element-table {
            border-radius: 3px;
            border: 1px solid #3071a9;
        }

        .title-element-table {
            background-color: #eeeeee;
            color: #000000;
            height: 30px;
            border-radius: 3px 3px 0 0;
        }

        .body-element-table {
            padding: 5px;
        }

        .footer-element-table {
            background-color: #dedede;
            color: #000000;
            height: 20px;
            border-radius: 0 0 3px 3px;
        }

        #filas {
            padding: 0px !important;
            margin: 0px !important;
        }

        #filas > div {
            padding: 0px !important;
            margin: 0px !important;
            margin-top: 0px !important;
            font-family: Tahoma !important;
            font-size: 12px !important;
        }

        .radio, .checkbox {
            position: relative;
            top: 4px;
            margin-top: 0px;
            margin-bottom: 0px;
        }

        .list-group-item {
            border-top: dotted 0.1px #95B8E7;
             border-left: dotted 0.1px #95B8E7;
              border-right: dotted 0.1px #95B8E7;
              border-bottom: none;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid" style="height: 100vh;" ng-controller="ReporteControlStockController">
        <div class="page-header">
            <div class="row">
                <div class="col-md-6" style="margin-top: 5px;">
                    <h3 style="font-family: 'Roboto'; font-size: 24px; font-weight: bold; color: #808080;">Reporte Control Stock</h3>
                </div>
            </div>
        </div>

        <div class="row">
            <div class="col-md-4">
                <div class="form-group">
                    <label class="control-label">(*) Almacén</label>
                    <select name="cmb_almacen" class="form-control input-sm" required
                        ng-model="ControlStock.AlmacenObj"
                        ng-options="almacen as almacen.Nombre for almacen in Almacenes track by almacen.Almacen_ID"
                        ng-change="AlmacenChange()">
                        <option value=''><- Seleccione -></option>
                    </select>
                </div>
            </div>
            <div class="col-md-4">
                <div class="form-group">
                    <div class="form-group">
                        <label class="control-label">(*) Ubicación</label>
                        <select name="cmb_ubicacion" class="form-control input-sm" required
                            ng-model="ControlStock.UbicacionObj"
                            ng-disabled="!IsSelectedAlmacen"
                            ng-options="ubicacion as ubicacion.Nombre for ubicacion in Ubicaciones track by ubicacion.Ubicacion_ID"
                            ng-change="UbicacionChange()">
                            <option value=''><- Seleccione -></option>
                        </select>
                    </div>
                </div>
            </div>
            <div class="col-md-4 pull-right text-right">
                <button type="button" class="btn btn-primary" style="margin-top: 30px;"
                    ng-click="ObtenerProductos()"
                    ng-disabled="!IsSelectedAlmacen">
                    <span class="fa fa-search"></span>&nbsp;<span>Buscar productos</span>
                </button>
            </div>
        </div>

        <div class="row" style="margin: 5px 0 5px 0;" ng-show="Productos.length > 0">
            <div class="col-md-12">
                <div class="row header">
                    <div class="col-md-6">
                        <div class="form-inline">
                            <strong>Productos</strong>&nbsp;
                            <input type="text" class="form-control input-sm" ng-model="FiltroProducto"
                                style="height: 30px !important;" prevent-enter-submit />
                        </div>
                    </div>
                    <div class="col-md-6 pull-right text-right" style="padding-top: 8px;">
                        <div class="form-inline">
                            <strong>Encontrados: <span ng-bind="(Productos | filter:FiltroProducto).length"></span></strong>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-12">
                <div class="row header-table">
                    <div class="col-md-1">
                        <span>No.</span>
                    </div>
                    <div class="col-md-3">
                        <span>Nombre</span>
                    </div>
                    <div class="col-md-2">
                        <span>Código</span>
                    </div>
                    <div class="col-md-1">
                        <span>Máximo</span>
                    </div>
                    <div class="col-md-1">
                        <span>Mínimo</span>
                    </div>
                    <div class="col-md-2">
                        <span>Punto reorden</span>
                    </div>
                    <div class="col-md-1">
                        <span>Existencia</span>
                    </div>
                    <div class="col-md-1"
                        ng-hide="ControlStock.UbicacionObj !== undefined && ControlStock.UbicacionObj !== null && ControlStock.UbicacionObj !== ''">
                        <span>Ubicación</span>
                    </div>
                </div>
            </div>
        </div>

        <div class="row" ng-show="Productos.length > 0">
            <div class="col-md-12">
                <div class="list-group">
                    <a href="javascript:void(0)" class="list-group-item" ng-repeat="x in Productos | filter:FiltroProducto" id="filas">
                        <div class="row">
                            <div class="col-md-1" style="margin-top: 2px;">
                                <span ng-bind="[$index + 1]"></span>
                            </div>
                            <div class="col-md-3" style="margin-top: 2px;">
                                <span ng-bind="x.Nombre"></span>
                            </div>
                            <div class="col-md-2" style="margin-top: 2px;">
                                <span ng-bind="x.Codigo"></span>
                            </div>
                            <div class="col-md-1" style="margin-top: 2px;">
                                <span ng-bind="x.Maximo"></span>
                            </div>
                            <div class="col-md-1" style="margin-top: 2px;">
                                <span ng-bind="x.Minimo"></span>
                            </div>
                            <div class="col-md-2" style="margin-top: 2px;">
                                <span ng-bind="x.PuntoReorden"></span>
                            </div>
                            <div class="col-md-1" style="margin-top: 2px;">
                                <span ng-bind="x.Existencia"></span>
                            </div>
                            <div class="col-md-1" style="margin-top: 2px;"
                                ng-hide="ControlStock.UbicacionObj !== undefined && ControlStock.UbicacionObj !== null && ControlStock.UbicacionObj !== ''">
                                <button type="button"  style="border: none; background-color: transparent; margin: 0px; padding: 0px;"
                                    ng-click="UbicacionAction(x)">
                                    <span class="fa fa-search" style="font-size: 12px; color: red; position:relative;  top: -3px;"></span>
                                </button>
                            </div>
                        </div>
                    </a>
                </div>
            </div>
        </div>

        <div class="row" ng-show="Productos.length === 0">
            <div class="col-md-12">
                <span>No se han encontrado productos.</span>
            </div>
        </div>

        <transaccion-modal></transaccion-modal>
    </div>
</asp:Content>
