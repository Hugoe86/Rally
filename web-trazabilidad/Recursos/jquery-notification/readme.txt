//Example:
var options = {
    'show': true,
    'title': 'Notificaciones',
    'icon': 'fa fa-bell',
    'iconCancel': 'fa fa-close',
    'textCancel': 'Cancelar',
    'iconSuccess': 'fa fa-check',
    'textSuccess': 'Aceptar',
    'onError': function (response) {
        // return: string message
    },
    'onSuccess': function (response) {
        // return: array

		/*
		array format:
		[
			{
				'Empresa_ID': 0,
				'Sucursal_ID': 0,
				'Usuario_ID': 0,
				'Usuario': '',
				'Email': '',
				'Nombre': ''
			}
		]
		*/
    }
};

$.notification(options);