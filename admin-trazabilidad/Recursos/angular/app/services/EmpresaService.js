TrazabilidadModule.factory('EmpresaService', ['$http', '$q',
function ($http, $q) {
    return {
        ObtenerEmpresas: function (entity) {
            var deferred = $q.defer();

            $http({
                url: UrlApp + '/Paginas/Paginas_Generales/controllers/Imagen_Empresa_Controller.asmx/Obtener_Empresas',
                method: 'POST',
                data: entity,
            }).success(deferred.resolve).error(deferred.reject);

            return deferred.promise;
        },
    };
}]);