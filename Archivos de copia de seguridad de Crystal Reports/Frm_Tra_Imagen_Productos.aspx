<%@ Page Title="" Language="C#" MasterPageFile="~/Paginas/Paginas_Generales/MasterPage.Master" AutoEventWireup="true" CodeBehind="Frm_Tra_Imagen_Productos.aspx.cs" Inherits="web_trazabilidad.Paginas.Catalogos.Frm_Imagen_Producto" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../Recursos/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../../Recursos/estilos/demo_form.css" rel="stylesheet" />
    <link href="../../Recursos/bootstrap-combo/select2.css" rel="stylesheet" />
    <link href="../../Recursos/estilos/center_loader.css" rel="stylesheet" />
    <link href="../../Recursos/estilos/demo_form.css" rel="stylesheet" />
    <link href="../../Recursos/bootstrap-fileinput/fileinput.css" rel="stylesheet" />
    <link href="../../Recursos/bootstrap-fileinput/fileinput.min.css" rel="stylesheet" />
    <script src="../../Recursos/bootstrap-fileinput/fileinput.js"></script>
    <script src="../../Recursos/bootstrap-fileinput/fileinput.min.js"></script>
    <script src="../../Recursos/bootstrap-combo/select2.js"></script>
    <script src="../../Recursos/bootstrap-combo/es.js"></script>
    <script src="../../Recursos/plugins/parsley.js"></script>
    <script src="../../Recursos/plugins/parsley.js"></script>
    <script src="../../Recursos/javascript/seguridad/Js_Controlador_Sesion.js"></script>
    <script src="../../Recursos/bootstrap-box/bootstrap-number-input.js"></script>
    <script src="../../Recursos/javascript/seguridad/Js_Controlador_Sesion.js"></script>
    <script src="../../Recursos/javascript/catalogos/Js_Imagen_Producto.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="container" style="height:100vh;" align="left">
         <div class="panel panel-default" style="max-width:50%;">
                <div class="panel-heading">
                <h3 class="panel-title"><i class="fa fa-key"></i>&nbsp;Seleccionar la imagen de Productos</h3>
                <div class="panel-options">
                    <a href="" data-toggle="panel">
                        <span class="collapse-icon">–</span>
                        <span class="expand-icon">+</span>
                    </a>
                    <a href="" data-toggle="remove">×
                    </a>
                </div>
            </div>

         <div class="panel-body">
             <div class="row">
                 <div class="col-md-7">
                     <select id="cmb_producto" style="width: 100% !important"></select>
                 </div>
             </div>
             <br />
             <div class="row">
                 <div class="col-md-7">
                     <label class="fuente_lbl_controles">Seleciona Imagen:</label>
                     <input id="fl_imagen" type="file" class="file-loading"  data-show-upload="false" data-show-caption="false" style="margin-bottom: 0px !important; border-bottom: 0px !important"/>
                 </div>
             </div>
             <br />
             <div class="row">
                    <div class="col-md-12">
                        <button id="btn_guardar" class="btn btn-lg btn-info btn-icon btn-icon-standalone btn-icon-standalone-right" type="button" onclick="_guardar_imagen">
                            <i class="fa fa-refresh" aria-hidden="true"></i>
                            <span>Aceptar</span>
                        </button>
                        <button id="btn_cancelar" class="btn btn-lg btn-primary btn-icon btn-icon-standalone btn-icon-standalone-right" type="button" onclick="_guardar_datos" onclick="_close_page">
                            <i class="fa fa-close" aria-hidden="true"></i>
                            <span>Cancelar</span>
                        </button>
                    </div>
                </div>
         </div>
       </div>        
     </div>
</asp:Content>
