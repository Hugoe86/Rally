<%@ Page Title="Principal" Language="C#" MasterPageFile="~/Paginas/Paginas_Generales/MasterPage.Master" AutoEventWireup="true"
    CodeBehind="Frm_Apl_Principal.aspx.cs" Inherits="web_trazabilidad.Paginas.Paginas_Generales.Frm_Apl_Principal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../Recursos/bootstrap-fileinput/fileinput.css" rel="stylesheet" />
    <script src="../../Recursos/bootstrap-fileinput/fileinput.js"></script>
    <script src="../../Recursos/javascript/seguridad/Js_Controlador_Sesion.js"></script>
    <script>
        $(document).on('ready', function () {
            //_inicializar_controls_file();

            //$('#mostrar_modal').on('click', function (e) {
            //    e.preventDefault();
            //    _mostra_modal();
            //});
        });
        function _inicializar_controls_file() {
            $("#fl_upload").fileinput({
                uploadUrl: 'http://localhost:61655/api/Producto/GuardarImagenProducto',
                uploadAsync: true,
                uploadExtraData: { Producto_ID: '1' },
                overwriteInitial: true,
                maxFileSize: 1500,
                showUpload: true,
                showClose: true,
                showPreview: true,
                showZoom: false,
                browseLabel: '',
                removeLabel: '',
                browseTitle: 'Seleccionar Imagen',
                browseIcon: '<i class="glyphicon glyphicon-folder-open"></i>',
                browseClass: 'btn btn-success',
                removeIcon: '<i class="glyphicon glyphicon-remove"></i>',
                removeTitle: 'Cancel or reset changes',
                removeClass: 'btn btn-danger',
                msgErrorClass: 'alert alert-block alert-danger',
                allowedFileExtensions: ["jpg", "png", "gif"]
            });
        }

        //function _mostra_modal() {

        //    var options = {
        //        'show': true,
        //        'title': 'Notificaciones',
        //        'icon': 'fa fa-bell',
        //        'iconCancel': 'fa fa-close',
        //        'textCancel': 'Cancelar',
        //        'iconSuccess': 'fa fa-check',
        //        'textSuccess': 'Aceptar',
        //        'onError': function (response) {
        //            // return: string message
        //        },
        //        'onSuccess': function (response) {
        //            // return: array
        //            alert(JSON.stringify(response));
        //        }
        //    };

        //    $.notification(options);
        //}
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container" style="height: 100vh;">
        <%--<input type="file" id="fl_upload" name="fl_upload[]" multiple="multiple" />--%>
        <%--<button id="mostrar_modal"></button>--%>
    </div>
</asp:Content>
