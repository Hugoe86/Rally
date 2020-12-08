TrazabilidadModule.controller('EmpresaController', ['$scope', '$sce', 'EmpresaService', 'Upload',
function ($scope, $sce, EmpresaService, Upload) {
    //#region Variables

    $scope.Empresa = {};
    $scope.Empresas = [];
    $scope.EmpresaNombre = '';

    //#endregion

    //#region Functions

    $scope.InitController = function () {
        $scope.ObtenerEmpresas();
    };

    $scope.ObtenerEmpresas = function () {
        var EmpresaObj = new Object();
        EmpresaObj.Nombre = $scope.EmpresaNombre;

        var Entity = JSON.stringify({ 'jsonObject': JSON.stringify(EmpresaObj) });

        EmpresaService.ObtenerEmpresas(Entity).then(function (result) {
            $scope.Empresas = JSON.parse(result.d);

            for (var i = 0; i < $scope.Empresas.length; i++) {
                $scope.Empresas[i].Nueva_Imagen = false;

                if ($scope.Empresas[i].Ruta_Imagen === null) {
                    $scope.Empresas[i].ImagenObj = null;
                } else {
                    var image = $scope.ConvertBase64ToImage($scope.Empresas[i].Ruta_Imagen);
                    $scope.Empresas[i].ImagenObj = image.src;
                }
            }
        }, function (result) {

        });
    };

    $scope.ConvertByteArrayToBase64 = function base64ArrayBuffer(arrayBuffer) {
        var base64 = ''
        var encodings = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/'

        var bytes = new Uint8Array(arrayBuffer)
        var byteLength = bytes.byteLength
        var byteRemainder = byteLength % 3
        var mainLength = byteLength - byteRemainder

        var a, b, c, d
        var chunk

        // Main loop deals with bytes in chunks of 3
        for (var i = 0; i < mainLength; i = i + 3) {
            // Combine the three bytes into a single integer
            chunk = (bytes[i] << 16) | (bytes[i + 1] << 8) | bytes[i + 2]

            // Use bitmasks to extract 6-bit segments from the triplet
            a = (chunk & 16515072) >> 18 // 16515072 = (2^6 - 1) << 18
            b = (chunk & 258048) >> 12 // 258048   = (2^6 - 1) << 12
            c = (chunk & 4032) >> 6 // 4032     = (2^6 - 1) << 6
            d = chunk & 63               // 63       = 2^6 - 1

            // Convert the raw binary segments to the appropriate ASCII encoding
            base64 += encodings[a] + encodings[b] + encodings[c] + encodings[d]
        }

        // Deal with the remaining bytes and padding
        if (byteRemainder == 1) {
            chunk = bytes[mainLength]

            a = (chunk & 252) >> 2 // 252 = (2^6 - 1) << 2

            // Set the 4 least significant bits to zero
            b = (chunk & 3) << 4 // 3   = 2^2 - 1

            base64 += encodings[a] + encodings[b] + '=='
        } else if (byteRemainder == 2) {
            chunk = (bytes[mainLength] << 8) | bytes[mainLength + 1]

            a = (chunk & 64512) >> 10 // 64512 = (2^6 - 1) << 10
            b = (chunk & 1008) >> 4 // 1008  = (2^6 - 1) << 4

            // Set the 2 least significant bits to zero
            c = (chunk & 15) << 2 // 15    = 2^4 - 1

            base64 += encodings[a] + encodings[b] + encodings[c] + '='
        }

        return base64
    };

    $scope.ConvertBase64ToImage = function (base64) {
        var image = new Image();
        image.src = 'data:image/png;base64,' + $scope.ConvertByteArrayToBase64(base64);

        return image;
    };

    //#endregion

    //#region Actions

    $scope.SeleccionarImagenEmpresa = function (index) {
        $scope.Empresas[index].Nueva_Imagen = true;
    };

    $scope.CancelarImagenEmpresa = function (index) {
        $scope.Empresas[index].Nueva_Imagen = false;

        if ($scope.Empresas[index].Ruta_Imagen === null) {
            $scope.Empresas[index].ImagenObj = null;
        } else {
            var image = $scope.ConvertBase64ToImage($scope.Empresas[index].Ruta_Imagen);
            $scope.Empresas[index].ImagenObj = image.src;
        }
    };

    $scope.GuardarImagenEmpresa = function (index) {
        var Empresa = $scope.Empresas[index];

        Upload.upload({
            url: UrlApp + '/api/ImagenEmpresa/GuardarImagenEmpresa',
            data: {
                Empresa_ID: Empresa.Empresa_ID,
                File: Empresa.ImagenObj,
            },
        }).then(function (result) { //success
            if (result.data) {
                $scope.Empresas[index].Nueva_Imagen = false;
                $scope.Empresas[index].Ruta_Imagen = $scope.Empresas[index].ImagenObj;
            }
        }, function (result) { //error

        }, function (result) { //progress

        });
    };

    $scope.BuscarEmpresasChange = function () {
        $scope.ObtenerEmpresas();
    };

    //#endregion

    //#region Angular Ready

    angular.element(document).ready(function () {
        $scope.InitController();
    });

    //#endregion
}]);