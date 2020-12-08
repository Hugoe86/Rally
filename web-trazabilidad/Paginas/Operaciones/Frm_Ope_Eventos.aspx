<%-- --------------------------------------------------------------------------------------------------------------------------------------- --%>
<%-- --------------------------------------------------------------------------------------------------------------------------------------- --%>
<%-- --------------------------------------------------------------------------------------------------------------------------------------- --%>

<%@ Page Language="C#"
    AutoEventWireup="true"
    MasterPageFile="~/Paginas/Paginas_Generales/MasterPage.Master"
    CodeBehind="Frm_Ope_Eventos.aspx.cs"
    Inherits="web_trazabilidad.Paginas.Operaciones.Frm_Ope_Eventos" %>

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

    <link href="../../Recursos/bootstrap-fileinput/fileinput.css" rel="stylesheet" />
    <script src="../../Recursos/bootstrap-fileinput/fileinput.min.js"></script>

    <script src="../../Recursos/javascript/seguridad/Js_Controlador_Sesion.js"></script>

    <script src="../../Recursos/javascript/Operaciones/Js_Ope_Eventos.js"></script>
    <script src="../../Recursos/javascript/Operaciones/Js_Ope_Eventos_Operaciones.js"></script>
    <script src="../../Recursos/javascript/Operaciones/Js_Ope_Eventos_Actividades.js"></script>
    <script src="../../Recursos/javascript/Operaciones/Js_Ope_Eventos_Categorias.js"></script>
    <script src="../../Recursos/javascript/Operaciones/Js_Ope_Eventos_Jornadas.js"></script>
    <script src="../../Recursos/javascript/Operaciones/Js_Evento_Punto_Control.js"></script>
    <script src="../../Recursos/javascript/Operaciones/Js_Eventos_Vehiculos.js"></script>
    <script src="../../Recursos/javascript/Operaciones/Js_Eventos_Jornadas_Layout.js"></script>
    <script src="../../Recursos/javascript/Operaciones/Js_Eventos_Punto_Control_Layout.js"></script>

    <link href="../../Recursos/bootstrap-table/bootstrap-table.min.css" rel="stylesheet" />
    <link href="../../Recursos/estilos/demo_form.css" rel="stylesheet" />
    <link href="../../Recursos/bootstrap-date/bootstrap-datetimepicker.css" rel="stylesheet" />
    <link href="../../Recursos/bootstrap-combo/select2.css" rel="stylesheet" />
    <link href="../../Recursos/icheck/skins/all.css" rel="stylesheet" />
    <script src="../../Recursos/icheck/icheck.min.js"></script>


    <%-- exportar pdf --%>
    <script src="../../Recursos/bootstrap-table-current/extensions/export/jsPDF/jspdf.min.js"></script>
    <script src="../../Recursos/bootstrap-table-current/extensions/export/jsPDF-AutoTable/jspdf.plugin.autotable.js"></script>

    <%-- exportar excel --%>
    <script src="../../Recursos/bootstrap-table-current/extensions/export/bootstrap-table-export.js"></script>
    <script src="../../Recursos/bootstrap-table-current/extensions/export/tableExport.js"></script>

</asp:Content>
<%-- --------------------------------------------------------------------------------------------------------------------------------------- --%>
<%-- --------------------------------------------------------------------------------------------------------------------------------------- --%>
<%-- --------------------------------------------------------------------------------------------------------------------------------------- --%>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container-fluid" style="height: 100vh;">
        <div class="page-header">
            <h3 style="font-family: 'Roboto Light', cursive !important; font-size: 24px; font-weight: bold; color: #808080;">Eventos</h3>
        </div>
        <%-- -------------------------------------------------------------------------------------------------------------------------------------------------- --%>
        <%-- -------------------------------------------------------------------------------------------------------------------------------------------------- --%>
        <div id="Principal"></div>
        <%-- -------------------------------------------------------------------------------------------------------------------------------------------------- --%>
        <%-- -------------------------------------------------------------------------------------------------------------------------------------------------- --%>
        <div id="Operacion"></div>

        <%-- -------------------------------------------------------------------------------------------------------------------------------------------------- --%>
        <%-- -------------------------------------------------------------------------------------------------------------------------------------------------- --%>
        <br />

        <div id="Tabs">
            <!--Tabs-->
            <ul class="nav nav-tabs" role="tablist">
                <li role="presentation" class="active"><a href="#frmActividades" aria-controls="frmActividades" role="tab" data-toggle="tab">Actividades Previas</a></li>
                <li role="presentation"><a href="#frmJornadas" aria-controls="frmJornadas" role="tab" data-toggle="tab">Jornadas</a></li>
                <li role="presentation"><a href="#frmCategorias" aria-controls="frmCategorias" role="tab" data-toggle="tab">Categorías</a></li>
                <li role="presentation"><a href="#frmVehiculos" aria-controls="frmVehiculos" role="tab" data-toggle="tab">Vehiculos</a></li>

            </ul>


            <div class="tab-content" style="border: 1px solid #dedede; border-radius: 3px 3px 3px 3px;">

                <div role="tabpanel" class="tab-pane fade in active" id="frmActividades" style="background-color: white !important;">
                    <div class="container-fluid">
                        <div class="page-header">
                            <h3 style="font-family: 'Roboto Light', cursive !important; font-size: 24px; font-weight: bold; color: #808080;">Actividades previas</h3>
                        </div>
                        <div id="Actividades_Principal"></div>
                        <div id="Actividades_Operacion"></div>
                    </div>
                </div>

                <div role="tabpanel" class="tab-pane fade" id="frmJornadas" style="background-color: white !important;">


                    <div class="container-fluid">
                        <div class="page-header">
                            <h3 style="font-family: 'Roboto Light', cursive !important; font-size: 24px; font-weight: bold; color: #808080;">Jornadas</h3>
                        </div>

                        <!-- ************************************************************************************************************************************ -->
                        <!-- ************************************************************************************************************************************ -->
                        <div id="JornadasPrincipal"></div>


                        <!-- ************************************************************************************************************************************ -->
                        <!-- ************************************************************************************************************************************ -->
                        <div id="Jornadas_layout"></div>
                        <div id="JornadaOperaciones"></div>

                        <!-- ************************************************************************************************************************************ -->
                        <!-- ************************************************************************************************************************************ -->
                        <div id="PuntoControl"></div>
                        <div id="PuntoControl_layout"></div>

                        <!-- ************************************************************************************************************************************ -->
                        <!-- ************************************************************************************************************************************ -->
                    </div>
                </div>

                <div role="tabpanel" class="tab-pane fade" id="frmCategorias" style="background-color: white !important;">
                    <div class="container-fluid">
                        <div class="page-header">
                            <h3 style="font-family: 'Roboto Light', cursive !important; font-size: 24px; font-weight: bold; color: #808080;">Categor&iacute;as</h3>
                        </div>
                        <div id="Categorias_Principal"></div>
                        <div id="Categorias_Operacion"></div>
                    </div>
                </div>

                <div role="tabpanel" class="tab-pane fade" id="frmVehiculos" style="background-color: white !important;">
                       <div class="container-fluid">
                        <div class="page-header">
                            <h3 style="font-family: 'Roboto Light', cursive !important; font-size: 24px; font-weight: bold; color: #808080;">Vehiculos</h3>
                        </div>
                           
                        <div id="VehiculosPrincipal"></div>
                        <div id="VehiculosOperacion"></div>
                    </div>
                </div>
            </div>

        </div>
        <%-- -------------------------------------------------------------------------------------------------------------------------------------------------- --%>
        <%-- -------------------------------------------------------------------------------------------------------------------------------------------------- --%>
    </div>

</asp:Content>

<%-- --------------------------------------------------------------------------------------------------------------------------------------- --%>
<%-- --------------------------------------------------------------------------------------------------------------------------------------- --%>
<%-- --------------------------------------------------------------------------------------------------------------------------------------- --%>
