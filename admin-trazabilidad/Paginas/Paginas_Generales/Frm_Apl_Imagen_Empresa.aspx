<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Paginas/Paginas_Generales/MasterPage.Master" CodeBehind="Frm_Apl_Imagen_Empresa.aspx.cs" Inherits="admin_trazabilidad.Paginas.Paginas_Generales.Frm_Apl_Imagen_Empresa" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../Recursos/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../../Recursos/estilos/center_loader.css" rel="stylesheet" />
    <link href="../../Recursos/estilos/css_master.css" rel="stylesheet" />

    <script src="../../Recursos/plugins/center-loader.min.js"></script>
    <script src="../../Recursos/angular/app/services/EmpresaService.js"></script>
    <script src="../../Recursos/angular/app/controllers/EmpresaController.js"></script>
    <style>
        .header-grid {
            background-color: #3071a9;
            color: #ffffff;
            height: 35px;
            border-radius: 3px 3px 0 0;
            border: 1px solid #3071a9;
        }

        .element-grid {
            border-radius: 3px;
            border: 1px solid #3071a9;
        }

        .title-element-grid {
            background-color: #eeeeee;
            color: #000000;
            height: 30px;
        }

        .img-responsive.img-center {
            margin: 0 auto;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid" style="height: 100vh;" ng-controller="EmpresaController">
        <div class="page-header">
            <div class="row">
                <div class="col-md-6" style="margin-top: 5px;">
                    <h3 style="font-family: 'Roboto'; font-size: 24px; font-weight: bold; color: #808080;">Imagen de Empresa</h3>
                </div>
                <div class="col-md-6 pull-right text-right">
                    <div class="form-inline">
                        <input type="text" class="form-control" placeholder="Buscar..." ng-model="EmpresaNombre"
                            ng-change="BuscarEmpresasChange()" />
                        <%--<button type="button" class="btn btn-info" title="Buscar">
                            <span class="fa fa-search"></span><span>&nbsp;Buscar</span>
                        </button>--%>
                    </div>
                </div>
            </div>
        </div>

        <div class="row" ng-if="Empresas.length > 0">
            <div class="col-md-12">
                <div class="row header-grid" style="padding-top: 8px;">
                    <div class="col-md-6">
                        <strong>Empresas</strong>
                    </div>
                    <div class="col-md-6 pull-right text-right">
                        <strong>Encontrados: <span ng-bind="Empresas.length"></span></strong>
                    </div>
                </div>

                <div class="row" style="margin-top: 5px;">
                    <div class="col-md-3" ng-repeat="empresa in Empresas">
                        <div class="row" style="margin-bottom: 5px; padding-right: 5px;">
                            <div class="col-md-12 element-grid">
                                <div class="row title-element-grid">
                                    <div class="col-md-12" style="padding-top: 5px;">
                                        <span ng-bind="empresa.Nombre"></span>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12" style="padding: 8px;">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <img class="img-responsive img-rounded img-center"
                                                    ngf-thumbnail="empresa.ImagenObj || '../../Recursos/img/Image_128x128.png'"
                                                    style="min-width: 128px; min-height: 128px; height: 128px;" />
                                            </div>
                                        </div>
                                        <div class="row" style="margin-top: 5px;">
                                            <div class="col-md-12 pull-right text-right">
                                                <button type="button" class="btn btn-primary" ngf-select ng-model="empresa.ImagenObj"
                                                    name="File{{empresa.Empresa_ID}}" ngf-pattern="'image/*'"
                                                    ngf-accept="'image/*'" ngf-max-size="20MB" ngf-min-height="100"
                                                    ngf-resize="{width: 100, height: 100}" title="Seleccionar imagen"
                                                    ng-click="SeleccionarImagenEmpresa($index)">
                                                    <span class="fa fa-file-image-o"></span>
                                                </button>
                                                <button type="button" class="btn btn-danger" title="Cancelar"
                                                    ng-if="empresa.Nueva_Imagen"
                                                    ng-click="CancelarImagenEmpresa($index)">
                                                    <span class="fa fa-remove"></span>
                                                </button>
                                                <button type="button" class="btn btn-success" title="Guardar"
                                                    ng-if="empresa.Nueva_Imagen"
                                                    ng-click="GuardarImagenEmpresa($index)">
                                                    <span class="fa fa-save"></span>
                                                </button>
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

        <div class="row" ng-if="Empresas.length === 0">
            <div class="col-md-12">
                <strong>No se han encontrado empresas con esa descripción</strong>
            </div>
        </div>
    </div>
</asp:Content>
