<%@ Page Language="C#" Title="Parámetros Facturación" MasterPageFile="~/Paginas/Paginas_Generales/MasterPage.Master" AutoEventWireup="true" CodeBehind="Frm_Apl_Parametros_Facturacion.aspx.cs" Inherits="web_trazabilidad.Paginas.Facturacion.Frm_Apl_Parametros_Facturacion" %>

<%@ Import Namespace="web_trazabilidad.Models.Ayudante" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../Recursos/estilos/center_loader.css" rel="stylesheet" />
    <link href="../../Recursos/estilos/demo_form.css" rel="stylesheet" />

    <script src="../../Recursos/plugins/center-loader.min.js"></script>
    <script src="../../Recursos/plugins/parsley.js"></script>
    <script src="../../Recursos/javascript/seguridad/Js_Controlador_Sesion.js"></script>
    <script src="../../Recursos/javascript/facturacion/Js_Apl_Parametros_Facturas.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid" style="height: 100vh;">

        <div class="page-header" align="left">
            <h3 style="font-family: 'Roboto Light', cursive !important; font-size: 24px; font-weight: bold; color: #808080;">Cat&aacute;logo de Par&aacute;metros Facturaci&oacute;n</h3>
        </div>

        <div class="panel panel-color panel-info" id="panel1">
            <div class="panel-heading">
                <div class="panel-options">
                    <a href="#" style="visibility: visible" id="btn_salir"><i class="glyphicon glyphicon-home"></i></a>
                    <a href="#" style="visibility: visible" id="btn_guardar"><i class="fa fa-floppy-o"></i></a>
                </div>
            </div>
            <div class="panel-body">
                <nav class="navbar navbar-default">
                    <div class="container-fluid">
                        <ul class="nav navbar-nav">
                            <li class="active" id="btn_parametros_sat"><a href="#">Parámetros SAT</a></li>
                            <li id="btn_parametros_facturas"><a href="#">Parámetros Facturas</a></li>
                            <li id="btn_imagen_factura"><a href="#">Imagen Factura</a></li>
                        </ul>
                    </div>
                </nav>
                <div id="panel_parametros_sat" style="display: block;">
                    <%--inicio panel parametros sat--%>
                    <div class="row">
                        <div class="col-md-6">
                            <label class="fuente_lbl_controles">*Certificado</label>
                            <input type="file" id="txt_ruta_certificado" class="form-control" />
                        </div>
                        <div class="col-md-6">
                            <label class="fuente_lbl_controles">El certificado cargado es</label>
                            <input type="text" id="txt_nombre_certificado" class="form-control" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <label class="fuente_lbl_controles">*Llave privada</label>
                            <input type="file" id="txt_ruta_llave_privada" class="form-control" />
                        </div>
                        <div class="col-md-6">
                            <label class="fuente_lbl_controles">La llave cargada es</label>
                            <input type="text" id="txt_nombre_llave_privada" class="form-control" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3">
                            <label class="fuente_lbl_controles">Password llave</label>
                            <input type="password" id="txt_password_llave" class="form-control" placeholder="Password llave" />
                        </div>
                        <div class="col-md-2">
                            <label class="fuente_lbl_controles">Ver password</label>
                            <input type="checkbox" id="ver_password" class="form-control" />
                        </div>
                        <div class="col-md-7">
                            <input type="hidden" id="txt_parametro_id" />
                            <input type="hidden" value="<%=Cls_Sesiones.Nombre_Carpeta %>" id="txt_nombre_carpeta" class="form-control" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <label class="fuente_lbl_controles">Vigencia inico certificado</label>
                            <input type="text" id="txt_inicio_certificado" class="form-control" placeholder="Vigencia inico certificado" />
                        </div>
                        <div class="col-md-6">
                            <label class="fuente_lbl_controles">Vigencia fin certificado</label>
                            <input type="text" id="txt_fin_certificado" class="form-control" placeholder="Se calcula automáticamente al guardar el certificado" />
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-6">
                            <label class="fuente_lbl_controles">*Ruta PDFs</label>
                            <input type="text" id="txt_ruta_pdf" class="form-control" placeholder="La ruta PDF se calculará automáticamente al guardar el registro" />
                        </div>
                        <div class="col-md-6">
                            <label class="fuente_lbl_controles">Ruta XMLs</label>
                            <input type="text" id="txt_ruta_xml" class="form-control" placeholder="La ruta XML se calculará automáticamente al guardar el registro" />
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-6">
                            <label class="fuente_lbl_controles">Aviso expira certificado</label>
                            <input type="text" id="txt_aviso_certificado" class="form-control" placeholder="Aviso expira certificado" />
                        </div>
                        <div class="col-md-6">
                            <label class="fuente_lbl_controles">Aviso expira folios</label>
                            <input type="text" id="txt_aviso_folios" class="form-control" placeholder="Aviso expira folios" />
                        </div>
                    </div>

                </div>
                <%--fin panel parametros sat--%>

                <div id="panel_parametros_facturas" style="display: none">
                    <div class="row">
                        <div class="col-md-3">
                            <label class="fuente_lbl_controles">Retención cedular</label>
                            <input type="text" id="txt_retencion_cedular" class="form-control" placeholder="Retención cedular" />
                        </div>
                        <div class="col-md-3">
                            <label class="fuente_lbl_controles">Retención ISR</label>
                            <input type="text" id="txt_retencion_isr" class="form-control" placeholder="Retención ISR" />
                        </div>
                        <div class="col-md-3">
                            <label class="fuente_lbl_controles">Retención IVA</label>
                            <input type="text" id="txt_retencion_iva" class="form-control" placeholder="Retención IVA" />
                        </div>
                        <div class="col-md-3">
                            <label class="fuente_lbl_controles">Retención flete</label>
                            <input type="text" id="txt_retencion_flete" class="form-control" placeholder="Retención flete" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <label class="fuente_lbl_controles">Régimen fiscal</label>
                            <input type="text" id="txt_regimen_fiscal" class="form-control" placeholder="Régimen fiscal" />
                        </div>
                        <div class="col-md-3">
                            <label class="fuente_lbl_controles">Versión</label>
                            <input type="text" id="txt_version" class="form-control" placeholder="Versión" />
                        </div>
                        <div class="col-md-3">
                            <label class="fuente_lbl_controles">Email origen de correos</label>
                            <input type="text" id="txt_email_origen" class="form-control" placeholder="Email origen de correos" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <label class="fuente_lbl_controles">Código del usuario</label>
                            <input type="text" id="txt_codigo_usuario" class="form-control" placeholder="Código del usuario" />
                        </div>
                        <div class="col-md-6">
                            <label class="fuente_lbl_controles">Código usuario proveedor</label>
                            <input type="text" id="txt_codigo_usuario_proveedor" class="form-control" placeholder="Código usuario proveedor" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3">
                            <label class="fuente_lbl_controles">ID Sucursal</label>
                            <input type="text" id="txt_id_sucursal" class="form-control" placeholder="ID Sucursal" />
                        </div>
                        <div class="col-md-3">
                            <label class="fuente_lbl_controles">Ambiente timbrado</label>
                            <select id="cmb_ambiente_timbrado" class="form-control" required>
                                <option value="PRUEBA">PRUEBA</option>
                                <option value="PRODUCCION">PRODUCCIÓN</option>
                            </select>
                        </div>
                        <div class="col-md-3">
                            <label class="fuente_lbl_controles">¿Enviar correo automático?</label>
                            <input type="checkbox" id="chck_correo_automatico" class="form-control" />
                        </div>
                        <div class="col-md-3">
                            <label class="fuente_lbl_controles">Porcentaje efectivo</label>
                            <input type="text" id="txt_porcentaje_efectivo" class="form-control" />
                        </div>
                    </div>
                </div>

                <div id="panel_imagen" style="display: none">
                    <div class="row">
                        <div class="col-md-12">
                            <label class="fuente_lbl_controles">Imagen facturación</label>
                            <input type="file" id="txt_ruta_imagen" class="form-control" />
                        </div>
                    </div>
                    <div class="row" style="text-align: center;">
                        <img src="no found" class="img-rounded" id="img_facturacion" alt="Ninguna imagen cargada" width="304" height="236" />
                    </div>
                </div>

            </div>
        </div>

    </div>
</asp:Content>
