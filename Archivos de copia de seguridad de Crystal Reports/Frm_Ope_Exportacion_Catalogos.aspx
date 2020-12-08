<%@ Page Title="" Language="C#" AutoEventWireup="true"
    MasterPageFile="~/Paginas/Paginas_Generales/MasterPage.Master"
    CodeBehind="Frm_Ope_Exportacion_Catalogos.aspx.cs"
    Inherits="web_trazabilidad.Paginas.Trazabilidad.Frm_Ope_Exportacion_Catalogos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../Recursos/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <script src="../../Recursos/bootstrap/js/bootstrap.min.js"></script>
    <link href="../../Recursos/estilos/center_loader.css" rel="stylesheet" />
    <link href="../../Recursos/estilos/css_master.css" rel="stylesheet" />
    <script src="../../Recursos/javascript/seguridad/Js_Controlador_Sesion.js"></script>

    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/select2/3.4.5/select2.css" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/selectize.js/0.8.5/css/selectize.default.css" />

    <script src="../../Recursos/angular/file-saver/FileSaver.js"></script>
    <script src="../../Recursos/angular/pdf-make/pdfmake.min.js"></script>
    <script src="../../Recursos/angular/pdf-make/vfs_fonts.js"></script>
    <script src="../../Recursos/angular/pdf-make/pdf-helper.js"></script>
    <script src="../../Recursos/angular/xslx/xlsx.core.min.js"></script>
    <script src="../../Recursos/angular/xslx/export-to-excel.js"></script>
    <script src="../../Recursos/angular/csv/csv-helper.js"></script>

    <script src="../../Recursos/angular/app/services/ImportacionCatalogosService.js"></script>
    <script src="../../Recursos/angular/app/services/ExportacionCatalogosService.js"></script>
    <script src="../../Recursos/angular/app/controllers/ExportacionCatalogosController.js"></script>

    <style>
        .select2 > .select2-choice.ui-select-match {
            /* Because of the inclusion of Bootstrap */
            height: 29px;
        }

        .selectize-control > .selectize-dropdown {
            top: 36px;
        }

        /* Some additional styling to demonstrate that append-to-body helps achieve the proper z-index layering. */
        .select-box {
            background: #fff;
            position: relative;
            z-index: 1;
        }

        .alert-info.positioned {
            margin-top: 1em;
            position: relative;
            z-index: 10000; /* The select2 dropdown has a z-index of 9999 */
        }

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

        .panel-header {
            margin: 0px;
            background-color: #428bca;
            color: #ffffff;
            border-radius: 3px 3px 0 0;
            padding: 5px;
        }

        .panel-content {
            margin: 0px;
            border: 1px solid #428bca;
            border-radius: 0 0 3px 3px;
            padding: 5px;
        }

        .invalid-record {
            border-left: 3px solid #ff0000;
        }

        .valid-record {
            border-left: 3px solid #5cb85c;
        }

        .invalid-column {
            border: 2px solid #ff0000 !important;
            color: #ff0000;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid" style="height: 100vh;" ng-controller="ExportacionCatalogosController">
        <div class="page-header">
            <div class="row">
                <div class="col-md-6" style="margin-top: 5px;">
                    <h3 style="font-family: 'Roboto'; font-size: 24px; font-weight: bold; color: #808080;">Exportación de Catálogos</h3>
                </div>
                <div class="col-md-6 pull-right text-right" style="margin-top: 5px;">
                    <button type="button" class="btn btn-danger btn-circle" title="Cancelar"
                        ng-click="CancelExport()">
                        <span class="fa fa-remove"></span>
                    </button>
                </div>
            </div>
        </div>

        <div class="row" style="margin-top: 10px;">
            <div class="col-md-12">
                <div class="form-group">
                    <label class="control-label">Exportar</label>
                    <div class="form-inline">
                        <div class="radio">
                            <label class="radio-inline">
                                <input type="radio" ng-model="DataLayout" ng-value="DataLayoutValues.Layout" />&nbsp;Solo Plantilla
                            </label>
                        </div>
                        <div class="radio" style="margin-left: 5px;">
                            <label class="radio-inline">
                                <input type="radio" ng-model="DataLayout" ng-value="DataLayoutValues.Data" />&nbsp;Plantilla y datos
                            </label>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="row" style="margin-top: 10px;">
            <div class="col-md-12">
                <div class="form-group">
                    <label class="control-label">Guardar como</label>
                    <div class="form-inline">
                        <div class="radio">
                            <label class="radio-inline">
                                <input type="radio" ng-model="TypeFile" ng-value="TypeFileValues.Excel" />&nbsp;Excel
                            </label>
                        </div>
                        <div class="radio" style="margin-left: 5px;">
                            <label class="radio-inline">
                                <input type="radio" ng-model="TypeFile" ng-value="TypeFileValues.Csv" />&nbsp;CSV
                            </label>
                        </div>
                        <div class="radio" style="margin-left: 5px;">
                            <label class="radio-inline">
                                <input type="radio" ng-model="TypeFile" ng-value="TypeFileValues.Pdf" />&nbsp;PDF
                            </label>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="row" style="margin-top: 10px;">
            <div class="col-md-4">
                <div class="form-group">
                    <label class="control-label">Tabla</label>
                    <ui-select ng-model="Exportacion.SelectedTable" theme="selectize" title="Seleccione la tabla"
                        ng-change="TableChange()">
                        <ui-select-match placeholder="Seleccione la tabla">{{$select.selected.TableName}}</ui-select-match>
                        <ui-select-choices repeat="x in TableNames | filter: $select.search">
                            <div ng-bind-html="x.TableName | highlight: $select.search"></div>
                        </ui-select-choices>
                    </ui-select>
                </div>
            </div>
            <div class="col-md-4" style="margin-top: 25px;">
                <button type="button" class="btn btn-primary" title="Obtener Campos"
                    ng-click="GetColumns()"
                    ng-disabled="Exportacion.SelectedTable === undefined">
                    <span class="fa fa-search"></span>
                    Obtener campos
                </button>
            </div>
            <div class="col-md-4 pull-right text-right" style="margin-top: 25px;">
                <button type="button" class="btn btn-success" title="Descargar"
                    ng-click="Download()"
                    ng-disabled="!ValidExport">
                    <span class="fa fa-download"></span>
                    Descargar
                </button>
            </div>
        </div>

        <div class="row" style="margin: 5px 0 5px 0;" ng-show="TableHeaders.length > 0">
            <div class="col-md-12">
                <div class="row header">
                    <div class="col-md-6">
                        <div class="form-inline">
                            <strong>Campos&nbsp;</strong>
                            <input type="text" class="form-control input-sm" ng-model="FilterTableHeader"
                                style="height: 30px !important;" prevent-enter-submit />
                        </div>
                    </div>
                    <div class="col-md-6 pull-right text-right" style="padding-top: 8px;">
                        <div class="form-inline">
                            <strong>Encontrados: <span ng-bind="(TableHeaders | filter:FilterTableHeader).length"></span></strong>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-12" style="padding: 0px !important;">
                <div class="table-responsive">
                    <table class="table table-hover table-striped table-bordered">
                        <thead class="header-table">
                            <tr>
                                <th style="width: 1%; color: #ffffff;">No.</th>
                                <th style="width: 10%; color: #ffffff;">
                                    <div class="form-inline">
                                        <span>Exportar</span>
                                        <div class="checkbox">
                                            <label>
                                                <input type="checkbox"
                                                    ng-model="ExportSelectAll"
                                                    ng-change="ExportSelectAllChange()" />
                                            </label>
                                        </div>
                                    </div>
                                </th>
                                <th style="width: 10%; color: #ffffff;">Llave</th>
                                <th style="width: 50%; color: #ffffff;">Nombre</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr ng-repeat="x in TableHeaders | filter:FilterTableHeader"
                                ng-class="{false: 'invalid-record', true: ''}[ValidateColumn(x)]">
                                <td style="padding-top: 20px !important;">
                                    <span ng-bind="[$index + 1]"></span>
                                </td>
                                <td style="padding-top: 13px !important;">
                                    <div class="checkbox">
                                        <label>
                                            <input type="checkbox" ng-model="x.IsSelected"
                                                ng-change="ExportSelectOneChange()"
                                                ng-disabled="!x.IsNullable && !x.IsIdentity" />
                                        </label>
                                    </div>
                                </td>
                                <td style="padding-top: 20px !important;">
                                    <span ng-bind="x.IsIdentity ? 'Si' : 'No'"></span>
                                </td>
                                <td style="padding-top: 20px !important;">
                                    <div><span ng-bind="x.ColumnName"></span></div>
                                    <div>
                                        <select class="form-control input-sm" ng-show="x.IsReferenceSelected"
                                            ng-model="x.ReferenceSelected"
                                            ng-options="item.ColumnName for item in x.ReferenceCamposTabla"
                                            ng-disabled="!x.IsSelected"
                                            style="width: 40%;"
                                            data-live-search="true">
                                            <option value=""><- Seleccione -></option>
                                        </select>
                                    </div>
                                    <div ng-show="x.IsForeignKey">
                                        <div class="checkbox">
                                            <label>
                                                <input type="checkbox" ng-model="x.IsReferenceSelected"
                                                    ng-change="ReferenceSelectedChange(x)"
                                                    ng-disabled="!x.IsSelected" />
                                                <span>Cambiar por campo de la tabla de referencia
                                                </span>
                                            </label>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
