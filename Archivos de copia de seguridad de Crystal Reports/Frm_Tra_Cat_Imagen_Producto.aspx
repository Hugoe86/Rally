<%@ Page Title="" Language="C#" MasterPageFile="~/Paginas/Paginas_Generales/MasterPage.Master" AutoEventWireup="true" CodeBehind="Frm_Tra_Cat_Imagen_Producto.aspx.cs" Inherits="web_trazabilidad.Paginas.Catalogos.Frm_Tra_Cat_Imagen_Producto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../Recursos/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../../Recursos/estilos/center_loader.css" rel="stylesheet" />
    <link href="../../Recursos/estilos/css_master.css" rel="stylesheet" />

    <script src="../../Recursos/plugins/center-loader.min.js"></script>
    <script src="../../Recursos/javascript/seguridad/Js_Controlador_Sesion.js"></script>
    <script src="../../Recursos/angular/app/services/ProductoService.js"></script>
    <script src="../../Recursos/angular/app/controllers/ImagenProductoController.js"></script>

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
    <div class="container-fluid" style="height: 100vh;" ng-controller="ImagenProductoController">
        <div class="page-header">
            <div class="row">
                <div class="col-md-6" style="margin-top: 5px;">
                    <h3 style="font-family: 'Roboto'; font-size: 24px; font-weight: bold; color: #808080;">Imagen de Producto</h3>
                </div>
                <div class="col-md-6 pull-right text-right">
                    <div class="form-inline">
                        <input type="text" class="form-control" placeholder="Buscar..." ng-model="ProductoFiltro"
                            ng-change="BuscarProductosChange()" />
                    </div>
                </div>
            </div>
        </div>

        <div class="row" ng-if="Productos.length > 0">
            <div class="col-md-12">
                <div class="row header-grid" style="padding-top: 8px;">
                    <div class="col-md-6">
                        <strong>Productos</strong>
                    </div>
                    <div class="col-md-6 pull-right text-right">
                        <strong>Encontrados: <span ng-bind="Productos.length"></span></strong>
                    </div>
                </div>

                <div class="row" style="margin-top: 8px; margin-bottom: 5px;">
                    <div class="col-md-12 pull-right text-right">
                        <span>Ordenar por:&nbsp;</span>
                        <div class="btn-group" opt-kind ok-key="sortBy">
                            <button type='button' class='btn btn-primary active' ok-sel="[producto-id]" ok-type="string">Id</button>
                            <button type='button' class='btn btn-primary' ok-sel="[producto-nombre]" ok-type="string">Nombre</button>
                            <button type='button' class='btn btn-primary' ok-sel="[producto-codigo]" ok-type="string">Código</button>
                        </div>
                    </div>
                </div>

                <div class="row ng-cloak" style="margin-top: 5px;" isotope-container ng-cloak>
                    <div class="col-md-3 " ng-repeat="producto in Productos" isotope-item>
                        <div class="row" style="margin-bottom: 5px; padding-right: 5px;" producto-id="{{producto.Producto_ID}}">
                            <div class="col-md-12 element-grid">
                                <div class="row title-element-grid">
                                    <div class="col-md-12" style="padding-top: 5px;">
                                        <div class="form-group">
                                            <p producto-nombre="{{producto.Nombre}}"><span ng-bind="producto.Nombre"></span></p>
                                            <p producto-codigo="{{producto.Codigo}}"><span ng-bind="producto.Codigo"></span></p>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12" style="padding: 8px;">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <img class="img-responsive img-rounded img-center"
                                                    ngf-thumbnail="producto.ImagenObj || '../../Recursos/img/Image_128x128.png'"
                                                    style="min-width: 128px; min-height: 128px; height: 128px;" />
                                            </div>
                                        </div>
                                        <div class="row" style="margin-top: 5px;">
                                            <div class="col-md-12 pull-right text-right">
                                                <button type="button" class="btn btn-primary" ngf-select ng-model="producto.ImagenObj"
                                                    name="File{{producto.Empresa_ID}}" ngf-pattern="'image/*'"
                                                    ngf-accept="'image/*'" ngf-max-size="20MB" ngf-min-height="100"
                                                    ngf-resize="{width: 100, height: 100}" title="Seleccionar imagen"
                                                    ng-click="SeleccionarImagenProducto($index)">
                                                    <span class="fa fa-file-image-o"></span>
                                                </button>
                                                <button type="button" class="btn btn-danger" title="Cancelar"
                                                    ng-if="producto.Nueva_Imagen"
                                                    ng-click="CancelarImagenProducto($index)">
                                                    <span class="fa fa-remove"></span>
                                                </button>
                                                <button type="button" class="btn btn-success" title="Guardar"
                                                    ng-if="producto.Nueva_Imagen"
                                                    ng-click="GuardarImagenProducto($index)">
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

        <div class="row" ng-if="Productos.length === 0">
            <div class="col-md-12">
                <strong>No se han encontrado productos con ese filtro</strong>
            </div>
        </div>
    </div>
</asp:Content>
