<%@ Page Title="" Language="C#" MasterPageFile="~/Paginas/Paginas_Generales/MasterPage.Master" AutoEventWireup="true" CodeBehind="Frm_Ope_Control_Stock.aspx.cs" Inherits="web_trazabilidad.Paginas.Catalogos.Frm_Ope_Control_Stock" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../Recursos/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../../Recursos/estilos/center_loader.css" rel="stylesheet" />
    <link href="../../Recursos/estilos/css_master.css" rel="stylesheet" />

    <script src="../../Recursos/plugins/center-loader.min.js"></script>
    <script src="../../Recursos/javascript/seguridad/Js_Controlador_Sesion.js"></script>

    <script src="../../Recursos/angular/app/services/ControlStockService.js"></script>
    <script src="../../Recursos/angular/app/controllers/ControlStockController.js"></script>

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

        .punto-reorden-editable:hover {
            color: #3071a9;
            font-size: 13pt;
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
    <div class="container-fluid" style="height: 100vh;" ng-controller="ControlStockController">
        <div class="page-header">
            <div class="row">
                <div class="col-md-6" style="margin-top: 5px;">
                    <h3 style="font-family: Tahoma; font-size: 24px; font-weight: bold; color: #808080;">M&aacute;ximos y M&iacute;nimos</h3>
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
                        <label class="control-label">Ubicación</label>
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
                            <strong>Encontrados: <span ng-bind="(Productos | filter:FiltroProducto).length"></span>&nbsp;|&nbsp;</strong>
                            <button type="button" class="btn btn-success" title="Aplicar" style="margin-top: -5px;"
                                ng-click="AplicarPuntoReordenProducto()">
                                <span class="fa fa-check"></span>&nbsp;
                                <span>Aplicar</span>
                            </button>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-12">
                <div class="row header-table">
                    <div class="col-md-1">
                        <span>No.</span>
                    </div>
                    <div class="col-md-2">
                        <span>Tiene punto reorden</span>
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
                </div>
            </div>
        </div>

        <div class="row" style="margin-top: 8px;" ng-show="Productos.length > 0">
            <div class="col-md-12">
                <div class="list-group">
                    <a href="javascript:void(0)" class="list-group-item" ng-repeat="x in Productos | filter:FiltroProducto" id="filas">
                        <div class="row">
                            <div class="col-md-1" style="margin-top: 4px;">
                                <span ng-bind="[$index + 1]"></span>
                            </div>
                            <div class="col-md-2">
                                <div class="checkbox">
                                    <label>
                                        <input type="checkbox" ng-model="x.TienePuntoReorden"
                                            ng-disabled="x.NoPuntoReorden !== null && x.NoPuntoReorden > 0 && x.TienePuntoReorden" />
                                        <button type="button"  title="Desaplicar el punto de reorden" style="border: none; background-color: transparent; margin: 0px; padding: 0px;"
                                            ng-if="x.NoPuntoReorden !== null && x.NoPuntoReorden > 0 && x.TienePuntoReorden && !x.TieneUbicaciones"
                                            ng-click="EliminarPuntoReorden(x)">
                                            <span class="fa fa-remove" style="font-size: 12px; color: red; position:relative;  top: -3px;"></span>
                                        </button>
                                    </label>
                                </div>
                            </div>
                            <div class="col-md-3" style="margin-top: 4px;">
                                <span ng-bind="x.Nombre"></span>
                            </div>
                            <div class="col-md-2" style="margin-top: 4px;">
                                <span ng-bind="x.Codigo"></span>
                            </div>
                            <div class="col-md-1" style="margin-top: 4px;">
                                <%--<div contenteditable ng-model="x.Maximo" numeric-only></div>--%>
                                <span ng-bind="x.Maximo" class="punto-reorden-editable" title="Editar" 
                                    style="cursor: pointer;"
                                    ng-click="PuntoReordenModal(x)"></span>
                            </div>
                            <div class="col-md-1" style="margin-top: 4px;">
                                <%--<div contenteditable ng-model="x.Minimo" numeric-only></div>--%>
                                <span ng-bind="x.Minimo" class="punto-reorden-editable" title="Editar" 
                                    style="cursor: pointer;"
                                    ng-click="PuntoReordenModal(x)"></span>
                            </div>
                            <div class="col-md-2" style="margin-top: 4px;">
                                <%--<div contenteditable ng-model="x.PuntoReorden" numeric-only></div>--%>
                                <span ng-bind="x.PuntoReorden" class="punto-reorden-editable" title="Editar" 
                                    style="cursor: pointer;"
                                    ng-click="PuntoReordenModal(x)"></span>
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

        <punto-reorden-modal></punto-reorden-modal>
    </div>
</asp:Content>
