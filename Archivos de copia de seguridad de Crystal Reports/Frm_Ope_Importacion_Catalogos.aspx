<%@ Page Title="" Language="C#" AutoEventWireup="true"
    MasterPageFile="~/Paginas/Paginas_Generales/MasterPage.Master"
    CodeBehind="Frm_Ope_Importacion_Catalogos.aspx.cs"
    Inherits="web_trazabilidad.Paginas.Trazabilidad.Frm_Ope_Importacion_Catalogos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../Recursos/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <script src="../../Recursos/bootstrap/js/bootstrap.min.js"></script>
    <link href="../../Recursos/estilos/center_loader.css" rel="stylesheet" />
    <link href="../../Recursos/estilos/css_master.css" rel="stylesheet" />
    <script src="../../Recursos/javascript/seguridad/Js_Controlador_Sesion.js"></script>

    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/select2/3.4.5/select2.css" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/selectize.js/0.8.5/css/selectize.default.css" />

    <script src="../../Recursos/angular/xslx/xlsx.core.min.js"></script>
    <script src="../../Recursos/angular/pdf-make/pdf-helper.js"></script>

    <script src="../../Recursos/angular/app/services/ImportacionCatalogosService.js"></script>
    <script src="../../Recursos/angular/app/controllers/ImportacionCatalogosController.js"></script>

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
    <div class="container-fluid ng-cloak" style="height: 100vh;" ng-controller="ImportacionCatalogosController" ng-cloak>
        <div class="page-header">
            <div class="row">
                <div class="col-md-6" style="margin-top: 5px;">
                    <h3 style="font-family: 'Roboto'; font-size: 24px; font-weight: bold; color: #808080;">Importación de catálogos</h3>
                </div>
                <div class="col-md-6 pull-right text-right" style="margin-top: 5px;">
                    <button type="button" class="btn btn-danger btn-circle" title="Cancelar"
                        ng-show="IsArchivo"
                        ng-click="CancelarImportacion()">
                        <span class="fa fa-remove"></span>
                    </button>
                </div>
            </div>
        </div>

        <div class="row">
            <div class="col-md-12">
                <div class="btn-group" role="group" aria-label="...">
                    <button type="button" class="btn btn-primary"
                        data-toggle="collapse" data-target="#collapseCagaDatos"
                        aria-expanded="true" aria-controls="collapseCagaDatos"
                        ng-class="{'active': Wizzard === WizzardProcess.CargaDatos}">
                        Carga de datos
                    </button>
                    <button type="button" class="btn btn-primary"
                        data-toggle="collapse" data-target="#collapseMapearBaseDatos"
                        aria-expanded="false" aria-controls="collapseMapearBaseDatos"
                        ng-disabled="RegistrosImportar.length === 0"
                        ng-class="{'active': Wizzard === WizzardProcess.MapearBaseDatos}">
                        Mapear con base de datos
                    </button>
                    <button type="button" class="btn btn-primary"
                        data-toggle="collapse" data-target="#collapseRegistrosGuardar"
                        aria-expanded="false" aria-controls="collapseRegistrosGuardar"
                        ng-disabled="RegistrosGuardar.length === 0"
                        ng-class="{'active': Wizzard === WizzardProcess.RegistrosGuardar}">
                        Registros a guardar
                    </button>
                    <button type="button" class="btn btn-primary"
                        data-toggle="collapse" data-target="#collapseRegistrosGuardados"
                        aria-expanded="false" aria-controls="collapseRegistrosGuardados"
                        ng-disabled="RegistrosGuardados.length === 0"
                        ng-class="{'active': Wizzard === WizzardProcess.RegistrosGuardados}">
                        Registros guardados
                    </button>
                </div>
            </div>
        </div>

        <div class="row" style="margin-top: 10px;">
            <div class="col-md-12">
                <div class="collapse" id="collapseCagaDatos"
                    ng-class="{'in': Wizzard === WizzardProcess.CargaDatos}">
                    <div id="CargaDatos">
                        <div class="row" style="margin-bottom: 8px;">
                            <div class="col-md-12">
                                <div class="row panel-header">
                                    <div class="col-md-6" style="padding-top: 5px;">
                                        <strong><span>Carga de datos</span></strong>
                                    </div>
                                    <div class="col-md-6 pull-right text-right">
                                    </div>
                                </div>
                                <div class="row panel-content">
                                    <div class="col-md-12">
                                        <div class="row" ng-show="!IsLoadArchivo">
                                            <div class="col-md-8">
                                                <button type="button" class="btn btn-primary"
                                                    ngf-select
                                                    ng-model="Archivo"
                                                    ngf-pattern="'.xlsx,.xls,.csv'"
                                                    ngf-accept="'.xlsx,.xls,.csv'"
                                                    ngf-max-size="20MB"
                                                    title="Seleccione archivo"
                                                    ng-change="SeleccioneArchivoChange()"
                                                    ng-disabled="IsArchivo">
                                                    <span class="fa fa-file-o"></span>
                                                    <span>&nbsp;Seleccione archivo</span>
                                                </button>
                                                <button type="button" class="btn btn-primary"
                                                    ng-disabled="!IsArchivo"
                                                    ng-click="CargarArchivo()">
                                                    <span class="fa fa-circle-o-notch"></span>
                                                    <span>&nbsp;Cargar archivo</span>
                                                </button>
                                            </div>
                                            <div class="col-md-4 pull-right text-right">
                                                <button type="button" class="btn btn-primary"
                                                    ng-click="GenerarPlantilla()">
                                                    <span class="fa fa-table"></span>
                                                    <span>&nbsp;Generar Plantilla</span>
                                                </button>
                                            </div>
                                            <div class="col-md-12" style="margin-top: 10px;">
                                                <p>
                                                    <strong><i>Archivos aceptados</i></strong>
                                                </p>
                                                <p>
                                                    <span class="fa fa-file-excel-o" style="font-size: 14pt; color: green;"></span>
                                                    <span>Excel</span>
                                                </p>
                                                <p>
                                                    <span class="fa fa-file-text-o" style="font-size: 14pt; color: dodgerblue;"></span>
                                                    <span>CSV</span>
                                                    <a href="https://support.office.com/es-es/article/Importar-o-exportar-archivos-de-texto-txt-o-csv-5250ac4c-663c-47ce-937b-339e391393ba?ui=es-ES&rs=es-ES&ad=ES#bmexport"
                                                        target="_blank">
                                                        <span class="fa fa-info-circle" style="color: #3071a9; cursor: pointer" title="Información"></span>
                                                    </a>
                                                </p>
                                            </div>
                                        </div>

                                        <div class="row" ng-show="IsExcel" style="margin-top: 10px;">
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>Hoja</label>
                                                    <ui-select ng-model="HojaExcel.Hoja" theme="selectize" title="Seleccione la hoja"
                                                        ng-change="HojaChange()">
                                                        <ui-select-match placeholder="Seleccione la hoja">{{$select.selected.Nombre}}</ui-select-match>
                                                        <ui-select-choices repeat="x in HojasExcel | filter: $select.search">
                                                            <div ng-bind-html="x.Nombre | highlight: $select.search"></div>
                                                        </ui-select-choices>
                                                    </ui-select>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row" style="margin: 5px 0 5px 0;" ng-show="RegistrosImportar.length > 0">
                                            <div class="col-md-12">
                                                <div class="row header">
                                                    <div class="col-md-6">
                                                        <div class="form-inline">
                                                            <strong>Registros a importar&nbsp;</strong>
                                                            <input type="text" class="form-control input-sm" ng-model="FiltroRegistrosImportar"
                                                                style="height: 30px !important;" prevent-enter-submit />
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6 pull-right text-right" style="padding-top: 8px;">
                                                        <div class="form-inline">
                                                            <strong>Encontrados: <span ng-bind="(RegistrosImportar | filter:FiltroRegistrosImportar).length"></span></strong>
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
                                                                <th style="width: 10%; color: #ffffff;" ng-repeat="x in EncabezadosImportar">
                                                                    <span ng-bind="x"></span>
                                                                </th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <tr ng-repeat="x in RegistrosImportar | filter:FiltroRegistrosImportar"
                                                                ng-show="IsExcel">
                                                                <td style="width: 1%;">
                                                                    <span ng-bind="[$index + 1]"></span>
                                                                </td>
                                                                <td style="width: 10%;" ng-repeat="y in EncabezadosImportar">
                                                                    <span ng-bind="x[y]"></span>
                                                                </td>
                                                            </tr>
                                                            <tr ng-repeat="x in RegistrosImportar | filter:FiltroRegistrosImportar"
                                                                ng-show="!IsExcel">
                                                                <td style="width: 1%;">
                                                                    <span ng-bind="[$index + 1]"></span>
                                                                </td>
                                                                <td style="width: 10%;" ng-repeat="y in x">
                                                                    <span ng-bind="y"></span>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="collapse" id="collapseMapearBaseDatos"
                    ng-class="{'in': Wizzard === WizzardProcess.MapearBaseDatos}">
                    <div id="MapearBaseDatos">
                        <div class="row" style="margin-bottom: 15px;" ng-show="EncabezadosImportar.length > 0">
                            <div class="col-md-12">
                                <div class="row panel-header">
                                    <div class="col-md-6" style="padding-top: 5px;">
                                        <strong><span>Mapear con base de datos</span></strong>
                                    </div>
                                    <div class="col-md-6 pull-right text-right">
                                    </div>
                                </div>
                                <div class="row panel-content">
                                    <div class="col-md-12">
                                        <div class="row">
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>Tabla</label>
                                                    <ui-select ng-model="Importacion.SelectedTable" theme="selectize" title="Seleccione la tabla"
                                                        ng-change="TablaChange()">
                                                        <ui-select-match placeholder="Seleccione la tabla">{{$select.selected.TableName}}</ui-select-match>
                                                        <ui-select-choices repeat="x in Tablas | filter: $select.search">
                                                            <div ng-bind-html="x.TableName | highlight: $select.search"></div>
                                                        </ui-select-choices>
                                                    </ui-select>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <button type="button" class="btn btn-primary" title="Buscar campos"
                                                        ng-click="BuscarCampos()"
                                                        ng-disabled="Importacion.SelectedTable === undefined"
                                                        style="margin-top: 25px;">
                                                        <span class="fa fa-search"></span>
                                                        <span>&nbsp;Buscar campos</span>
                                                    </button>
                                                </div>
                                            </div>
                                            <div class="col-md-4 pull-right text-right">
                                                <div class="form-group">
                                                    <button type="button" class="btn btn-success" title="Mapear"
                                                        ng-click="MapearAction()"
                                                        ng-disabled="!MapearValido"
                                                        style="margin-top: 25px;">
                                                        <span class="fa fa-table"></span>
                                                        <span>&nbsp;Mapear</span>
                                                    </button>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row" ng-show="!MapearValido && CamposTabla.length > 0">
                                            <div class="col-md-12">
                                                <div class="invalid-record" style="margin-bottom: 5px;">&nbsp;Campo inválido</div>
                                                <div class="well well-sm">
                                                    <p>Campo no mapeado correctamente.</p>
                                                    <p>Asigne un campo de la lista de Mapear al campo de Nombre que le corresponde.</p>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row" style="margin: 5px 0 5px 0;" ng-show="CamposTabla.length > 0">
                                            <div class="col-md-12">
                                                <div class="row header">
                                                    <div class="col-md-4">
                                                        <div class="form-inline">
                                                            <strong>Campos&nbsp;</strong>
                                                            <input type="text" class="form-control input-sm" ng-model="FiltroCampoTabla"
                                                                style="height: 30px !important;" prevent-enter-submit />
                                                        </div>
                                                    </div>
                                                    <div class="col-md-4 text-center">
                                                        <h4><span ng-bind="SelectedTableName"></span></h4>
                                                    </div>
                                                    <div class="col-md-4 pull-right text-right" style="padding-top: 8px;">
                                                        <div class="form-inline">
                                                            <strong>Encontrados: <span ng-bind="(CamposTabla | filter:FiltroCampoTabla).length"></span></strong>
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
                                                                        <span>Importar</span>
                                                                        <div class="checkbox">
                                                                            <label>
                                                                                <input type="checkbox"
                                                                                    ng-model="SelectAll"
                                                                                    ng-change="SelectAllChange()" />
                                                                            </label>
                                                                        </div>
                                                                    </div>
                                                                </th>
                                                                <th style="width: 10%; color: #ffffff;">Llave</th>
                                                                <th style="width: 10%; color: #ffffff;">
                                                                    <div class="form-inline">
                                                                        <span>Único</span>
                                                                        <div class="checkbox">
                                                                            <label>
                                                                                <input type="checkbox"
                                                                                    ng-model="UniqueAll"
                                                                                    ng-change="UniqueAllChange()"
                                                                                    ng-disabled="!SelectAll" />
                                                                            </label>
                                                                        </div>
                                                                    </div>
                                                                </th>
                                                                <th style="width: 50%; color: #ffffff;">Nombre</th>
                                                                <th style="width: 20%; color: #ffffff;">Mapear</th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <tr ng-repeat="x in CamposTabla | filter:FiltroCampoTabla"
                                                                ng-class="{false: 'invalid-record', true: ''}[ValidarRegistroAMapear(x)]">
                                                                <td style="padding-top: 20px !important;">
                                                                    <span ng-bind="[$index + 1]"></span>
                                                                </td>
                                                                <td style="padding-top: 13px !important;">
                                                                    <div class="checkbox">
                                                                        <label>
                                                                            <input type="checkbox" ng-model="x.IsSelected"
                                                                                ng-change="SelectOneChange()"
                                                                                ng-disabled="!x.IsNullable" />
                                                                        </label>
                                                                        <span class="fa fa-info-circle" data-toggle="tooltip"
                                                                            data-placement="top"
                                                                            title="Este campo es obligatorio"
                                                                            style="cursor: pointer; color: #3071a9;"
                                                                            ng-show="!x.IsNullable"></span>
                                                                    </div>
                                                                </td>
                                                                <td style="padding-top: 20px !important;">
                                                                    <span ng-bind="x.IsIdentity ? 'Si' : 'No'"></span>
                                                                    <span class="fa fa-info-circle" data-toggle="tooltip"
                                                                        data-placement="top"
                                                                        title="Si agrega este campo se modificará el registro en base de datos solo si existe, sino existe este campo no se guardará de ninguna manera, sino lo agrega se insertará normalmente."
                                                                        style="cursor: pointer; color: #3071a9;"
                                                                        ng-show="x.IsIdentity"></span>
                                                                </td>
                                                                <td style="padding-top: 13px !important;">
                                                                    <div class="checkbox">
                                                                        <label>
                                                                            <input type="checkbox" ng-model="x.IsUnique"
                                                                                ng-disabled="!x.IsSelected || x.IsIdentity"
                                                                                ng-change="UniqueOneChange()" />
                                                                        </label>
                                                                    </div>
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
                                                                <td style="padding-top: 5px !important;">
                                                                    <select class="form-control input-sm"
                                                                        ng-disabled="!x.IsSelected"
                                                                        ng-model="x.Encabezado"
                                                                        style="width: 95%;"
                                                                        data-live-search="true">
                                                                        <option value=""><- Seleccione -></option>
                                                                        <option ng-repeat="e in EncabezadosImportar" value="{{e}}"
                                                                            >{{e}}
                                                                        </option>
                                                                    </select>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="collapse" id="collapseRegistrosGuardar"
                    ng-class="{'in': Wizzard === WizzardProcess.RegistrosGuardar}">
                    <div id="RegistrosGuardar">
                        <div class="row" style="margin-bottom: 15px;" ng-show="RegistrosGuardar.length > 0">
                            <div class="col-md-12">
                                <div class="row panel-header">
                                    <div class="col-md-6" style="padding-top: 5px;">
                                        <strong><span>Registros a guardar</span></strong>
                                    </div>
                                    <div class="col-md-6 pull-right text-right">
                                    </div>
                                </div>
                                <div class="row panel-content">
                                    <div class="col-md-12">
                                        <div class="row">
                                            <div class="col-md-12 pull-right text-right">
                                                <div class="form-inline">
                                                    <button type="button" class="btn btn-primary" title="Validar"
                                                        ng-click="ValidarAction()">
                                                        <span class="fa fa-check"></span>
                                                        <span>&nbsp;Validar</span>
                                                    </button>
                                                    <button type="button" class="btn btn-success" title="Procesar"
                                                        ng-click="ProcesarAction()"
                                                        ng-disabled="!IsValidAction">
                                                        <span class="fa fa-spinner"></span>
                                                        <span>&nbsp;Procesar</span>
                                                    </button>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row" ng-show="IsNotValidRegister && RegistrosGuardar.length > 0">
                                            <div class="col-md-12">
                                                <div class="form-inline">
                                                    <div class="invalid-record" style="margin-bottom: 5px;">&nbsp;Registro inválido</div>
                                                    <div class="well well-sm">
                                                        <p>Se han encontrado registros con errores de acuerdo a las configuraciones de Mapeo con base de datos.</p>
                                                        <p>
                                                            Los registros con <span class="invalid-record" style="margin-bottom: 5px;">&nbsp;</span> serán descartados a guardarse en la base de datos.
                                                        </p>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row" style="margin: 5px 0 5px 0;">
                                            <div class="col-md-12">
                                                <div class="row header">
                                                    <div class="col-md-6">
                                                        <div class="form-inline">
                                                            <strong>Registros a guardar&nbsp;</strong>
                                                            <input type="text" class="form-control input-sm" ng-model="FiltroRegistroGuardar"
                                                                style="height: 30px !important;" prevent-enter-submit />
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6 pull-right text-right" style="padding-top: 8px;">
                                                        <div class="form-inline">
                                                            <strong>Encontrados: <span ng-bind="(RegistrosGuardar | filter:FiltroRegistroGuardar).length"></span></strong>
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
                                                                <th style="width: 3%; color: #ffffff;">Acción</th>
                                                                <th style="width: 10%; color: #ffffff;" ng-repeat="x in RegistrosGuardar[0].Row.Columns">
                                                                    <span ng-bind="x.ColumnName"></span>
                                                                    <span ng-if="x.IsReferenceSelected">(<span ng-bind="x.ReferenceSelected.ColumnName"></span>)
                                                                    </span>
                                                                    <span class="fa fa-info-circle" data-toggle="tooltip"
                                                                            data-placement="top"
                                                                            title="Si el campo no existe en base de datos y se está tomando como 'Editar' no se guardará de ninguna forma."
                                                                            style="cursor: pointer; color: yellow;"
                                                                            ng-show="x.IsIdentity"></span>
                                                                </th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <tr ng-repeat="x in RegistrosGuardar | filter:FiltroRegistroGuardar"
                                                                ng-class="{false: 'invalid-record', true: ''}[x.Row.IsValid]">
                                                                <td style="width: 1%;">
                                                                    <span ng-bind="[$index + 1]"></span>
                                                                </td>
                                                                <td style="width: 3%">
                                                                    <span ng-bind="x.Row.IsNewRegister ? 'Nuevo' : 'Editar'"></span>
                                                                </td>
                                                                <td style="width: 10%;" ng-repeat="y in x.Row.Columns"
                                                                    ng-class="{false: 'invalid-column', true: ''}[y.IsValid]">
                                                                    <div class="form-inline">
                                                                        <span ng-bind="y.ColumnValue"></span>&nbsp;
                                                                        <span class="fa fa-info-circle" data-toggle="tooltip"
                                                                            data-placement="top" title="{{y.InvalidMessage}}"
                                                                            ng-show="!y.IsValid"
                                                                            style="cursor: pointer;"></span>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="collapse" id="collapseRegistrosGuardados"
                    ng-class="{'in': Wizzard === WizzardProcess.RegistrosGuardados}">
                    <div id="RegistrosGuardados">
                        <div class="row" style="margin-bottom: 15px;" ng-show="RegistrosGuardados.length > 0">
                            <div class="col-md-12">
                                <div class="row panel-header">
                                    <div class="col-md-6" style="padding-top: 5px;">
                                        <strong><span>Registros guardados</span></strong>
                                    </div>
                                    <div class="col-md-6 pull-right text-right">
                                    </div>
                                </div>
                                <div class="row panel-content">
                                    <div class="col-md-12">
                                        <div class="row" ng-show="NoSavedCount > 0">
                                            <div class="col-md-12">
                                                <div class="form-inline">
                                                    <div class="invalid-record" style="margin-bottom: 5px;">&nbsp;Registro no guardado</div>
                                                    <div class="well well-sm">
                                                        <p>No se ha podido guardar correctamente el registro.</p>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row" style="margin: 5px 0 5px 0;">
                                            <div class="col-md-12">
                                                <div class="row header">
                                                    <div class="col-md-6">
                                                        <div class="form-inline">
                                                            <strong>Registros guardados&nbsp;</strong>
                                                            <input type="text" class="form-control input-sm" ng-model="FiltroRegistroGuardado"
                                                                style="height: 30px !important;" prevent-enter-submit />
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6 pull-right text-right" style="padding-top: 8px;">
                                                        <div class="form-inline">
                                                            <strong>Encontrados: <span ng-bind="(RegistrosGuardados | filter:FiltroRegistroGuardado).length"></span></strong>
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
                                                                <th style="width: 10%; color: #ffffff;" ng-repeat="x in RegistrosGuardados[0].Row.Columns">
                                                                    <span ng-bind="x.ColumnName"></span>
                                                                    <span ng-if="x.IsReferenceSelected">(<span ng-bind="x.ReferenceSelected.ColumnName"></span>)
                                                                    </span>
                                                                </th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <tr ng-repeat="x in RegistrosGuardados | filter:FiltroRegistroGuardado"
                                                                ng-class="{false: 'invalid-record', true: ''}[x.Row.IsSaved]">
                                                                <td style="width: 1%;">
                                                                    <span ng-bind="[$index + 1]"></span>
                                                                </td>
                                                                <td style="width: 10%;" ng-repeat="y in x.Row.Columns"
                                                                    ng-class="{false: 'invalid-column', true: ''}[y.IsValid]">
                                                                    <div class="form-inline">
                                                                        <span ng-bind="y.ColumnValue"></span>&nbsp;
                                                                        <span class="fa fa-info-circle" data-toggle="tooltip"
                                                                            data-placement="top" title="{{y.Message}}"
                                                                            ng-show="!y.IsValid"
                                                                            style="cursor: pointer;"></span>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
