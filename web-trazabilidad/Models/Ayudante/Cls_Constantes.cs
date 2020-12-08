using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace admin_trazabilidad.Models.Ayudante
{
    public class Cls_Constantes
    {
        public static string Str_Conexion = ConfigurationManager.ConnectionStrings["trazabilidad"].ConnectionString;
    }
    ///*******************************************************************************
    /// NOMBRE DE LA CLASE: Roles
    /// DESCRIPCIÓN: Clase que contiene los campos de la tabla APL_CAT_ROLES
    /// PARÁMETROS :
    /// CREO       : Susana Trigueros Armenta
    /// FECHA_CREO : 20/Agosto/2010 
    /// MODIFICO          :
    /// FECHA_MODIFICO    :
    /// CAUSA_MODIFICACIÓN:
    ///*******************************************************************************
    public class Apl_Cat_Roles
    {
        public const String Tabla_Apl_Cat_Roles = "Apl_Roles";
        public const String Campo_Rol_ID = "Rol_ID";
        public const String Campo_Nombre = "Nombre";
        public const String Campo_Empresa_ID = "Empresa_ID";
        public const String Campo_Sucursal_ID = "Sucursal_ID";
        public const String Campo_Estatus_ID = "Estatus_ID";
        public const String Campo_Nivel_ID = "Nivel_ID";
        public const String Campo_Descripcion = "Descripcion";
        public const String Campo_Usuario_Creo = "Usuario_Creo";
        public const String Campo_Fecha_Creo = "Fecha_Creo";
        public const String Campo_Usuario_Modifico = "Usuario_Modifico";
        public const String Campo_Fecha_Modifico = "Fecha_Modifico";
        public const String Campo_Tipo = "Tipo";
    }
    ///*******************************************************************************
    /// NOMBRE DE LA CLASE: Apl_Cat_Accesos
    /// DESCRIPCIÓN: Clase que contiene los campos de la tabla APL_CAT_ACCESOS
    /// PARÁMETROS :
    /// CREO       : Susana Trigueros Armenta
    /// FECHA_CREO : 20/Agosto/2010 
    /// MODIFICO          :
    /// FECHA_MODIFICO    :
    /// CAUSA_MODIFICACIÓN:
    ///*******************************************************************************
    public class Apl_Cat_Accesos
    {
        public const String Tabla_Apl_Cat_Accesos = "Apl_Accesos";
        public const String Campo_Menu_ID = "Menu_ID";
        public const String Campo_Rol_ID = "Rol_ID";
        public const String Campo_Habilitado = "Habilitado";
        public const String Campo_Alta = "Alta";
        public const String Campo_Cambio = "Cambio";
        public const String Campo_Eliminar = "Eliminar";
        public const String Campo_Consultar = "Consultar";
        public const String Campo_Usuario_Creo = "Usuario_Creo";
        public const String Campo_Fecha_Creo = "Fecha_Creo";
        public const String Campo_Estatus_ID = "Estatus_ID";
        //public const String Campo_Fecha_Modifico = "FECHA_MODIFICO";
    }
    ///*******************************************************************************
    /// NOMBRE DE LA CLASE: Apl_Cat_Menus
    /// DESCRIPCIÓN: Clase que contiene los campos de la tabla APL_CAT_MENUS
    /// PARÁMETROS :
    /// CREO       : Susana Trigueros Armenta
    /// FECHA_CREO : 20/Agosto/2010 
    /// MODIFICO          : Fernando Gonzalez
    /// FECHA_MODIFICO    : 4/Mayo/2012
    /// CAUSA_MODIFICACIÓN: Se agrego la constante del campo Moudlo_ID
    ///*******************************************************************************
    public class Apl_Cat_Menus
    {
        public const String Tabla_Apl_Cat_Menus = "Apl_Menus";
        public const String Campo_Menu_ID = "Menu_ID";
        public const String Campo_Parent_ID = "Parent_ID";
        public const String Campo_Menu_Descripcion = "Menu_Descripcion";
        public const String Campo_Nombre_Mostrar = "Nombre_Mostrar";
        public const String Campo_URL_Link = "URL_LINK";
        public const String Campo_Orden = "Orden";
        public const String Campo_Usuario_Creo = "Usuario_Creo";
        public const String Campo_Fecha_Creo = "Fecha_Creo";
        public const String Campo_Usuario_Modifico = "Usuario_Modifico";
        public const String Campo_Fecha_Modifico = "Fecha_Modifico";
        //public const String Campo_Clasificacion = "CLASIFICACION";
        //public const String Campo_Pagina = "PAGINA";
        public const String Campo_Modulo_ID = "Modulo_ID";
        public const String Campo_Empresa_ID = "Empresa_ID";
    }

    public class Apl_Menus_Empresas
    {
        public const String Tabla_Apl_Menus_Empresa = "Apl_Menus_Empresa";
        public const String Campo_Menu_ID = "Menu_ID";
        public const String Campo_Empresa_ID = "Empresa_ID";
        public const String Campo_Menu_Empresa_ID = "Menu_Empresa_ID";
    }
    ///*******************************************************************************
    /// NOMBRE DE LA CLASE: Apl_Cat_Modulos_Siag
    /// DESCRIPCIÓN: Clase que contiene los campos de la tabla APL_CAT_MODULOS_SIAG
    /// PARÁMETROS :
    /// CREO       : Fernando Gonzalez
    /// FECHA_CREO : 4/Mayo/2012 
    /// MODIFICO          :
    /// FECHA_MODIFICO    :
    /// CAUSA_MODIFICACIÓN:
    ///*******************************************************************************
    public class Apl_Cat_Modulos_Siag
    {
        public const String Tabla_Apl_Cat_Modulos_Siag = "Apl_Modulos";
        public const String Campo_Modulo_ID = "Modulo_ID";
        public const String Campo_Nombre = "Nombre";
        public const String Campo_Usuario_Creo = "Usuario_Creo";
        public const String Campo_Fecha_Creo = "Fecha_Creo";
        public const String Campo_Usuario_Modifico = "Usuario_Modifico";
        public const String Campo_Fecha_Modifico = "Fecha_Modifico";
    }
    ///*******************************************************************************
    /// NOMBRE DE LA CLASE: Apl_Grupos_Roles
    /// DESCRIPCIÓN: Clase que contiene los campos de la tabla APL_GRUPOS_ROLES
    /// PARÁMETROS :
    /// CREO       : Juan Alberto Hernandez Negrete
    /// FECHA_CREO : 06/Oct/2010 
    /// MODIFICO          :
    /// FECHA_MODIFICO    :
    /// CAUSA_MODIFICACIÓN:
    ///*******************************************************************************
    //public class Apl_Grupos_Roles
    //{
    //    public const String Tabla_Apl_Grupos_Roles = "APL_GRUPOS_ROLES";
    //    public const String Campo_Grupo_Roles_ID = "GRUPO_ROLES_ID";
    //    public const String Campo_Nombre = "NOMBRE";
    //    public const String Campo_Comentarios = "COMENTARIOS";
    //    public const String Campo_Usuario_Creo = "USUARIO_CREO";
    //    public const String Campo_Fecha_Creo = "FECHA_CREO";
    //    public const String Campo_Usuario_Modifico = "USUARIO_MODIFICO";
    //    public const String Campo_Fecha_Modifico = "FECHA_MODIFICO";
    //}
    public class Tabla_Tra_Cat_Empleados
    {
        public const string Campo_Empleado_ID = "Empleado_ID";
        public const string Campo_No_Empleado = "No_Empleado";
        public const string Campo_Apellido_Paterno = "Apellido_Paterno";
        public const string Campo_Apellido_Materno = "Apellido_Materno";
        public const string Campo_Nombre = "Nombre";
        public const string Campo_Empleado = "Empleado";
        public const string Campo_Puesto = "Puesto";
        public const string Campo_Codigo_Postal = "Codigo_Postal";
        public const string Campo_Fecha_Nacimiento = "Fecha_Nacimiento";
        public const string Campo_Estado_Lugar = "Estado_Lugar";
        public const string Campo_Tipo_Nomina_ID = "Tipo_Nomina_ID";
        public const string Campo_Registo_Patronal = "Registo_Patronal";
        public const string Campo_NSS = "NSS";
        public const string Campo_CURP = "CURP";
        public const string Campo_Sexo = "Sexo";
        public const string Campo_Fecha_Ingreso = "Fecha_Ingreso";
        public const string Campo_No_Infonavit = "No_Infonavit";
        public const string Campo_Unidad_Medica_Familiar = "Unidad_Medica_Familiar";
        public const string Campo_Fecha_Inicio_Desc_Info = "Fecha_Inicio_Desc_Info";
        public const string Campo_Tipo_Descuento_Infonavit = "Tipo_Descuento_Infonavit";
        public const string Campo_Valor_Descuento_Infonavit = "Valor_Descuento_Infonavit";
        public const string Campo_Tipo_Trabajador = "Tipo_Trabajador";
        public const string Campo_RFC = "RFC";
        public const string Campo_Tipo_Salario = "Tipo_Salario";
        public const string Campo_Semana_Jornada_Reducida = "Semana_Jornada_Reducida";
        public const string Campo_Horas_Labora = "Horas_Labora";

    }
    public class Tabla_Cat_Nom_Movimientos_IDSE
    {
        public const string Campo_No_Movimiento_IDSE_ID = "No_Movimiento_IDSE_ID";
        public const string Campo_Salario_Diario_Actual = "Salario_Diario_Actual";
        public const string Campo_SBC_Actual = "SBC_Actual";
        public const string Campo_Salario_Diario_Anterior = "Salario_Diario_Anterior";
        public const string Campo_SBC_Anterior = "SBC_Anterior";
        public const string Campo_Tipo_Movimiento = "Tipo_Movimiento";
        public const string Campo_Fecha_Movimiento = "Fecha_Movimiento";
        public const string Campo_Fecha_Inicio = "Fecha_Inicio";
        public const string Campo_Nombre_Puesto = "Nombre_Puesto";
        public const string Campo_Fecha_Baja_IMSS = "Fecha_Baja_IMSS";
        public const string Campo_Tipo_Baja = "Tipo_Baja";
        public const string Campo_Fecha_Inicio_Lic = "Fecha_Inicio_Lic";
        public const string Campo_Fecha_Termino_Lic = "Fecha_Termino_Lic";
        public const string Campo_Estado_Registro = "Estado_Registro";
        public const string Campo_Fecha_Registro = "Fecha_Registro";
        public const string Campo_Mes_Movimiento = "Mes_Movimiento";
        public const string Campo_Anio_Movimiento = "Anio_Movimiento";
        public const string Campo_Estado = "Estado";
        public const string Campo_Estatus = "Estatus";
    }

    public class Tabla_Cat_Nom_His_Mov_Infonavit
    {
        public const string Campo_His_Mov_ID = "His_Mov_ID";
        public const string Campo_No_IMSS = "No_IMSS";
        public const string Campo_Tipo_Movimiento = "Tipo_Movimiento";
        public const string Campo_Tipo_Descuento = "Tipo_Descuento";
        public const string Campo_Fecha_Movimiento = "Fecha_Movimiento";
        public const string Campo_Valor_Descuento = "Valor_Descuento";
        public const string Campo_No_Credito_Inf = "No_Credito_Inf";
        public const string Campo_Aplica_Tabla_Disminucion = "Aplica_Tabla_Disminucion";
    }

    public class Tabla_Ope_Nom_Incapacidades
    {
        public const string Campo_No_Incapacidad = "No_Incapacidad";
        public const string Campo_Empleado_ID = "Empleado_ID";
        public const string Campo_Folio_Incacidad = "Folio_Incacidad";
        public const string Campo_Departamento_ID = "Departamento_ID";
        public const string Campo_Tipo_Incapacidad_ID = "Tipo_Incapacidad_ID";
        public const string Campo_Rama_Incapacidad_ID = "Tipo_Incapacidad_ID";
        public const string Campo_Tipo_Riesgo_ID = "Tipo_Riesgo_ID";
        public const string Campo_Secuela_Incapacidad_ID = "Secuela_Incapacidad_ID";
        public const string Campo_Control_Incapacidad_ID = "Control_Incapacidad_ID";
        public const string Campo_Dias_Incapacidad = "Dias_Incapacidad";
        public const string Campo_Aplica_Pago_Cuarto_Dia = "Aplica_Pago_Cuarto_Dia";
        public const string Campo_Porcentaje_Incapacidad = "Porcentaje_Incapacidad";
        public const string Campo_Fecha_Inicio = "Fecha_Inicio";
        public const string Campo_Fecha_Termino = "Fecha_Termino";
        public const string Campo_Estatus = "Estatus";
    }
    public class Tabla_Ope_Reloj_Faltas
    {
        public const string Campo_No_Falta = "No_Falta";
        public const string Campo_Empleado_ID = "Empleado_ID";
        public const string Campo_Tipo_Falta_ID = "Tipo_Falta_ID";
        public const string Campo_Nomina_ID = "Nomina_ID";
        public const string Campo_Periodo_ID = "Periodo_ID";
        public const string Campo_Fecha = "Fecha";
        public const string Campo_Cantidad = "Cantidad_Falta";
        public const string Campo_Proceso = "Proceso";
        public const string Campo_Fecha_Generacion = "Fecha_Generacion";
        public const string Campo_Observaciones = "Observaciones";
        public const string Campo_Estatus = "Estatus";
    }

    public class XML_Contrato_Indeterminado
    {
        public const string Campo_Fecha = "Fecha";
        public const string Campo_Empleado = "Empleado";
        public const string Campo_Edad = "Edad";
        public const string Campo_Sexo = "Sexo";
        public const string Campo_Estado_Civil = "Estado_Civil";
        public const string Campo_CURP = "CURP";
        public const string Campo_RFC = "RFC";
        public const string Campo_Direccion = "Direccion";
        public const string Campo_Ciudad = "Ciudad";
        public const string Campo_Puesto = "Puesto";
        public const string Campo_Sueldo_Numerico = "Sueldo_Numerico";
        public const string Campo_Sueldo_Letras = "Sueldo_Letras";
        public const string Campo_Fecha_Inicio = "Fecha_Inicio";
        public const string Campo_Empleado_Firma = "Empleado_Firma";
    }

    public class XML_Contrato_Determinado
    {
        public const string Campo_Fecha = "Fecha";
        public const string Campo_Empleado = "Empleado";
        public const string Campo_Edad = "Edad";
        public const string Campo_Sexo = "Sexo";
        public const string Campo_Estado_Civil = "Estado_Civil";
        public const string Campo_CURP = "CURP";
        public const string Campo_RFC = "RFC";
        public const string Campo_Direccion = "Direccion";
        public const string Campo_Ciudad = "Ciudad";
        public const string Campo_Fecha_Termino = "Fecha_Termino";
        public const string Campo_Sueldo_Numerico = "Sueldo_Numerico";
        public const string Campo_Sueldo_Letras = "Sueldo_Letras";
        public const string Campo_Empleado_Firma = "Empleado_Firma";
        public const string Campo_Empleado_Firma_IMSS = "Empleado_Firma_IMSS";
    }
    public class XML_Carta_Renuncia
    {
        public const string Campo_Empleado = "Empleado";
        public const string Campo_Dia = "Dia";
        public const string Campo_Mes = "Mes";
        public const string Campo_Anio = "Anio";
        public const string Campo_Empleado_Firma = "Empleado_Firma";
        
    }
}