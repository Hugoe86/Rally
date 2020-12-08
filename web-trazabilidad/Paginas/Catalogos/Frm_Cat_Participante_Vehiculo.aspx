
<%-- --------------------------------------------------------------------------------------------------------------------------------------- --%>
<%-- --------------------------------------------------------------------------------------------------------------------------------------- --%>
<%-- --------------------------------------------------------------------------------------------------------------------------------------- --%>
<%@ Page Language="C#" 
    AutoEventWireup="true"     
    MasterPageFile="~/Paginas/Paginas_Generales/MasterPage.Master"
    CodeBehind="Frm_Cat_Participante_Vehiculo.aspx.cs" 
    Inherits="web_trazabilidad.Paginas.Catalogos.Frm_Cat_Participante_Vehiculo" %>

<%-- --------------------------------------------------------------------------------------------------------------------------------------- --%>
<%-- --------------------------------------------------------------------------------------------------------------------------------------- --%>
<%-- --------------------------------------------------------------------------------------------------------------------------------------- --%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../Recursos/bootstrap-table/bootstrap-table.min.css" rel="stylesheet" />
    <link href="../../Recursos/bootstrap-table/extensions/editable/bootstrap-editable.css" rel="stylesheet" />
    <link href="../../Recursos/estilos/center_loader.css" rel="stylesheet" />
    <link href="../../Recursos/estilos/demo_form.css" rel="stylesheet" />
    <link href="../../Recursos/plugins/toastr/toastr.css" rel="stylesheet" />
    <link href="../../Recursos/bootstrap-date/bootstrap-datetimepicker.css" rel="stylesheet" />
    <link href="../../Recursos/bootstrap-combo/select2.css" rel="stylesheet" />
    <script src="../../Recursos/lightbox/ekko-lightbox.min.js"></script>
    <script src="../../Recursos/jquery-validate/jquery.validate.min.js"></script>
    <script src="../../Recursos/xenon/js/xenon-widgets.js"></script>
    <script src="../../Recursos/xenon/js/xenon-custom.js"></script>
    <script src="../../Recursos/bootstrap-table-current/bootstrap-table.js"></script>
    <script src="../../Recursos/bootstrap-table-current/locale/bootstrap-table-es-MX.js"></script>
    <script src="../../Recursos/bootstrap-table/extensions/editable/bootstrap-editable.js"></script>
    <script src="../../Recursos/bootstrap-table/extensions/editable/bootstrap-table-editable.min.js"></script>
    <script src="../../Recursos/bootstrap-table-current/extensions/editable/bootstrap-table-editable.js"></script>
    <script src="../../Recursos/bootstrap-combo/select2.js"></script>
    <script src="../../Recursos/bootstrap-combo/es.js"></script>
    <script src="../../Recursos/plugins/center-loader.min.js"></script>
    <script src="../../Recursos/plugins/parsley.js"></script>
    <script src="../../Recursos/jquery-numeric/jquery.numeric.js"></script>
    <script src="../../Recursos/jquery-numeric/accounting.min.js"></script>
    <script src="../../Recursos/jquery-numeric/jquery.formatCurrency-1.4.0.min.js"></script>
    <script src="../../Recursos/bootstrap-date/bootstrap-datetimepicker.min.js"></script>
    <script src="../../Recursos/javascript/seguridad/Js_Controlador_Sesion.js"></script>

    
    <script src="../../Recursos/javascript/catalogos/Relacion/Js_Cat_Relacion_Participantes.js"></script>
    <script src="../../Recursos/javascript/catalogos/Relacion/Js_Cat_Relacion_Vehiculo.js"></script>
    <script src="../../Recursos/javascript/catalogos/Relacion/Js_Cat_Relacionar.js"></script>
    <script src="../../Recursos/javascript/generales/Js_Apl_Documentos.js"></script>



</asp:Content>
<%-- --------------------------------------------------------------------------------------------------------------------------------------- --%>
<%-- --------------------------------------------------------------------------------------------------------------------------------------- --%>
<%-- --------------------------------------------------------------------------------------------------------------------------------------- --%>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container-fluid" style="height:40vh;">
        <div class="page-header">
            <h3 style="font-family: 'Roboto Light', cursive !important; font-size: 24px; font-weight: bold; color: #808080;">Participantes - Vehículos - Relación</h3>
        </div>
       
          <div class="row">
            <div class="col-md-12 text-left">
                
                <hr style="padding: 0px !important; margin: 5px 0px 5px 0px;">
            </div>
            <div class="col-md-12">

                <ul class="nav nav-tabs" role="tablist">
                    <li class="active" id="Li_Participante">
                        <a href="#Tab_Participante" data-toggle="tab">
                            <span class="visible-xs"><i class="fa fa-user-circle-o"></i></span>
                            <span class="hidden-xs"> <i class="fa fa-user-circle" style="color: #B22222;"></i> Participantes</span>
                        </a>
                    </li>
                    <li id="Li_Vehiculos">
                        <a href="#Tab_Vehiculo" data-toggle="tab">
                            <span class="visible-xs"><i class="fa fa-car"></i></span>
                           <span class="hidden-xs"> <i class="fa fa-car" style="color: green;"></i> Vehículos</span>
                        </a>
                    </li>

                     <li id="Li_Relacion">
                        <a href="#Tab_Relacion" data-toggle="tab">
                            <span class="visible-xs"><i class="fa fa-tags"></i></span>
                           <span class="hidden-xs"> <i class="fa fa-tags" style="color: #DAA520;"></i> Relacionar</span>
                        </a>
                    </li>
                </ul>


                <div class="tab-content">
                    <!--******************************************************************************************************************************-->
                    <div class="tab-pane active" id="Tab_Participante">

                        <div>
                            <i class="fa fa-user-circle" style="font-size:24px; color: #B22222;">&nbsp;Participantes</i>
                        </div>
                           
                        <!--******************************************************************************************************************************-->
                        <br />

                        <!--******************************************************************************************************************************-->
                        <div id="Principal_Participante"></div>
                        <div id="Operacion_Participante"></div>
                    </div>

                    <!--******************************************************************************************************************************-->
                    <div class="tab-pane" id="Tab_Vehiculo">

                        <div>
                            <i class="fa fa-car" style="font-size: 24px; color: green;">&nbsp;Vehículos</i>
                        </div>

                           
                        <!--******************************************************************************************************************************-->
                        <br />

                        <!--******************************************************************************************************************************-->

                        <div id="Principal_Vehiculos"></div>
                        <div id="Operacion_Vehiculos"></div>
                    </div>
                    <!--******************************************************************************************************************************-->
                    <div class="tab-pane" id="Tab_Relacion">

                        <!--******************************************************************************************************************************-->

                        <div>
                            <i class="fa fa-cogs" style="font-size: 24px; color: #DAA520;">&nbsp;Relacionar participante-Vehículo</i>
                        </div>
                        
                        <!--******************************************************************************************************************************-->
                        <br />

                        <!--******************************************************************************************************************************-->
                        <div id="Principal_Relacion"></div>
                        <!--******************************************************************************************************************************-->
                    </div>
                </div>

            </div>
        </div>





    </div>

</asp:Content>

<%-- --------------------------------------------------------------------------------------------------------------------------------------- --%>
<%-- --------------------------------------------------------------------------------------------------------------------------------------- --%>
<%-- --------------------------------------------------------------------------------------------------------------------------------------- --%>




