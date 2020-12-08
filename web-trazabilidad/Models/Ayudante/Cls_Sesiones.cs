using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Data;
using datos_trazabilidad;
using web_trazabilidad.Models.Negocio;
using web_trazabilidad.Models.Negocio.Nomina;

namespace web_trazabilidad.Models.Ayudante
{
    public class Cls_Sesiones
    {
        private static String S_Datos_Usuario = "Datos_Usuario";
        private static String S_Usuario = "Usuario";
        private static String S_Imagen_Sistema = "Imagen_Sistema";
        private static String S_Rol_ID = "Rol_ID";
        private static String S_Empresa_ID = "Empresa_ID";
        private static String S_Sucursal_ID = "Sucursal_ID";
        private static String S_Usuario_ID = "Usuario_ID";
        private static String S_Mostrar_Menu = "Mostrar_Menu";
        private static String S_Menus_Control_Acceso = "MENUS_CONTROL_ACCESO";
        private static String S_Lista_Productos_Orden_Compra = "Productos_Orden_Compra";
        private static String S_Default_Admin_Empresa = "Default_Admin_Empresa";
        private static String S_Bloqueo_Pantalla = "Bloqueo_Pantalla";
        private static String S_Correo_Usuario = "Correo_Usuario";
        private static String S_Sucursal_Nombre = "Sucursal_Nombre";
        private static String S_Empresa_Nombre = "Empresa_Nombre";
        private static String S_Empresa_Direccion = "Empresa_Direccion";
        private static String S_Empresa_Email = "Empresa_Email";
        private static String S_Empresa_Telefono = "Empresa_Telefono";
        private static String S_Sucursal_Direccion = "Sucursal_Direccion";
        private static String S_Sucursal_Email = "Sucursal_Email";
        private static String S_Sucursal_Telefono = "Sucursal_Telefono";
        private static String S_Nombre_Carpeta = "Sucursal_Telefono";
        private static String S_Habilitar_Contenerdor_No_Piezas = "Habilitar_Contenerdor_No_Piezas";
        private static String S_Lista_Asistencia_Empleados = "Lista_Asistencia_Empleados";
        private static String S_Lista_Asistencia_Empleado = "Lista_Asistencia_Empleado";
        private static String S_Lista_Conceptos_Cargados = "Lista_Conceptos_Cargados";
        private static String S_Lista_Actualizacion_Salario_IDSE = "Lista_Actualizacion_Salario_IDSE";
        private static String S_Lista_Movimientos_IDSE = "Lista_Movimientos_IDSE";
        private static String S_Generar_Folio_Poliza = "Generar_Folio_Poliza";
        private static String S_Separador_Cuenta_Contable = "Separador_Cuenta_Contable";
        private static String S_Lista_Registros_Obtenidos_Reloj_Checador = "Lista_Registros_Obtenidos_Reloj_Checador";
        private static String S_Lista_Seguimiento_Contratos = "Lista_Seguimiento_Contratos";

        public static List<Cls_Cat_Productos_Negocio> Lista_Productos_Orden_Compra
        {
            get
            {
                // Verifica si es null
                if (HttpContext.Current.Session[Cls_Sesiones.S_Lista_Productos_Orden_Compra] == null)
                    return null;
                else
                    return (List<Cls_Cat_Productos_Negocio>)HttpContext.Current.Session[Cls_Sesiones.S_Lista_Productos_Orden_Compra];
            }
            set
            {
                HttpContext.Current.Session[Cls_Sesiones.S_Lista_Productos_Orden_Compra] = value;
            }
        }



     


     
        public static Apl_Usuarios Datos_Usuario
        {
            get
            {
                if (HttpContext.Current.Session[Cls_Sesiones.S_Datos_Usuario] == null)
                    return null;
                else
                    return (Apl_Usuarios)HttpContext.Current.Session[Cls_Sesiones.S_Datos_Usuario];
            }
            set
            {
                HttpContext.Current.Session[Cls_Sesiones.S_Datos_Usuario] = value;
            }
        }

        public static String Rol_ID
        {
            get
            {
                if (HttpContext.Current.Session[Cls_Sesiones.S_Rol_ID] == null)
                    return String.Empty;
                else
                    return HttpContext.Current.Session[Cls_Sesiones.S_Rol_ID].ToString();
            }
            set
            {
                HttpContext.Current.Session[Cls_Sesiones.S_Rol_ID] = value;
            }
        }

        public static String Empresa_ID
        {
            get
            {
                if (HttpContext.Current.Session[Cls_Sesiones.S_Empresa_ID] == null)
                    return String.Empty;
                else
                    return HttpContext.Current.Session[Cls_Sesiones.S_Empresa_ID].ToString();
            }
            set
            {
                HttpContext.Current.Session[Cls_Sesiones.S_Empresa_ID] = value;
            }
        }

        public static String Empresa_Nombre
        {
            get
            {
                if (HttpContext.Current.Session[Cls_Sesiones.S_Empresa_Nombre] == null)
                    return String.Empty;
                else
                    return HttpContext.Current.Session[Cls_Sesiones.S_Empresa_Nombre].ToString();
            }
            set
            {
                HttpContext.Current.Session[Cls_Sesiones.S_Empresa_Nombre] = value;
            }
        }

        public static String Empresa_Direccion
        {
            get
            {
                if (HttpContext.Current.Session[Cls_Sesiones.S_Empresa_Direccion] == null)
                    return String.Empty;
                else
                    return HttpContext.Current.Session[Cls_Sesiones.S_Empresa_Direccion].ToString();
            }
            set
            {
                HttpContext.Current.Session[Cls_Sesiones.S_Empresa_Direccion] = value;
            }
        }
        public static String Empresa_Email
        {
            get
            {
                if (HttpContext.Current.Session[Cls_Sesiones.S_Empresa_Email] == null)
                    return String.Empty;
                else
                    return HttpContext.Current.Session[Cls_Sesiones.S_Empresa_Email].ToString();
            }
            set
            {
                HttpContext.Current.Session[Cls_Sesiones.S_Empresa_Email] = value;
            }
        }
        public static String Empresa_Telefono
        {
            get
            {
                if (HttpContext.Current.Session[Cls_Sesiones.S_Empresa_Telefono] == null)
                    return String.Empty;
                else
                    return HttpContext.Current.Session[Cls_Sesiones.S_Empresa_Telefono].ToString();
            }
            set
            {
                HttpContext.Current.Session[Cls_Sesiones.S_Empresa_Telefono] = value;
            }
        }
        public static String Sucursal_ID
        {
            get
            {
                if (HttpContext.Current.Session[Cls_Sesiones.S_Sucursal_ID] == null)
                    return String.Empty;
                else
                    return HttpContext.Current.Session[Cls_Sesiones.S_Sucursal_ID].ToString();
            }
            set
            {
                HttpContext.Current.Session[Cls_Sesiones.S_Sucursal_ID] = value;
            }
        }

        public static String Sucursal_Nombre
        {
            get
            {
                if (HttpContext.Current.Session[Cls_Sesiones.S_Sucursal_Nombre] == null)
                    return String.Empty;
                else
                    return HttpContext.Current.Session[Cls_Sesiones.S_Sucursal_Nombre].ToString();
            }
            set
            {
                HttpContext.Current.Session[Cls_Sesiones.S_Sucursal_Nombre] = value;
            }
        }

        public static String Sucursal_Email
        {
            get
            {
                if (HttpContext.Current.Session[Cls_Sesiones.S_Sucursal_Email] == null)
                    return String.Empty;
                else
                    return HttpContext.Current.Session[Cls_Sesiones.S_Sucursal_Email].ToString();
            }
            set
            {
                HttpContext.Current.Session[Cls_Sesiones.S_Sucursal_Email] = value;
            }
        }

        public static String Sucursal_Telefono
        {
            get
            {
                if (HttpContext.Current.Session[Cls_Sesiones.S_Sucursal_Telefono] == null)
                    return String.Empty;
                else
                    return HttpContext.Current.Session[Cls_Sesiones.S_Sucursal_Telefono].ToString();
            }
            set
            {
                HttpContext.Current.Session[Cls_Sesiones.S_Sucursal_Telefono] = value;
            }
        }

        public static String Sucursal_Direccion
        {
            get
            {
                if (HttpContext.Current.Session[Cls_Sesiones.S_Sucursal_Direccion] == null)
                    return String.Empty;
                else
                    return HttpContext.Current.Session[Cls_Sesiones.S_Sucursal_Direccion].ToString();
            }
            set
            {
                HttpContext.Current.Session[Cls_Sesiones.S_Sucursal_Direccion] = value;
            }
        }
        public static String Default_Admin_Empresa
        {
            get
            {
                if (HttpContext.Current.Session[Cls_Sesiones.S_Default_Admin_Empresa] == null)
                    return String.Empty;
                else
                    return HttpContext.Current.Session[Cls_Sesiones.S_Default_Admin_Empresa].ToString();
            }
            set
            {
                HttpContext.Current.Session[Cls_Sesiones.S_Default_Admin_Empresa] = value;
            }
        }

        public static String Usuario_ID
        {
            get
            {
                if (HttpContext.Current.Session[Cls_Sesiones.S_Usuario_ID] == null)
                    return String.Empty;
                else
                    return HttpContext.Current.Session[Cls_Sesiones.S_Usuario_ID].ToString();
            }
            set
            {
                HttpContext.Current.Session[Cls_Sesiones.S_Usuario_ID] = value;
            }
        }

        public static List<Cls_Apl_Menus_Negocio> Menu_Control_Acceso
        {
            get
            {
                if (HttpContext.Current.Session[Cls_Sesiones.S_Menus_Control_Acceso] == null)
                    return null;
                else
                    return (List<Cls_Apl_Menus_Negocio>)HttpContext.Current.Session[Cls_Sesiones.S_Menus_Control_Acceso];
            }
            set
            {
                HttpContext.Current.Session[Cls_Sesiones.S_Menus_Control_Acceso] = value;
            }
        }

        public static String Usuario
        {
            get
            {
                // Verifica si es null
                if (HttpContext.Current.Session[Cls_Sesiones.S_Usuario] == null)
                    return String.Empty;
                else
                    return HttpContext.Current.Session[Cls_Sesiones.S_Usuario].ToString();
            }
            set
            {
                HttpContext.Current.Session[Cls_Sesiones.S_Usuario] = value;
            }
        }

        public static String Correo_Usuario
        {
            get
            {
                // Verifica si es null
                if (HttpContext.Current.Session[Cls_Sesiones.S_Correo_Usuario] == null)
                    return String.Empty;
                else
                    return HttpContext.Current.Session[Cls_Sesiones.S_Correo_Usuario].ToString();
            }
            set
            {
                HttpContext.Current.Session[Cls_Sesiones.S_Correo_Usuario] = value;
            }
        }
        public static bool Mostrar_Menu
        {
            get
            {
                bool dato = Convert.ToBoolean(HttpContext.Current.Session[Cls_Sesiones.S_Mostrar_Menu]);
                return dato;
            }
            set
            {
                HttpContext.Current.Session[Cls_Sesiones.S_Mostrar_Menu] = value;
            }
        }

        public static String Imagen_Sistema
        {
            get
            {
                if (HttpContext.Current.Session[Cls_Sesiones.S_Imagen_Sistema] == null)
                    return String.Empty;
                else
                    return HttpContext.Current.Session[Cls_Sesiones.S_Imagen_Sistema].ToString();
            }
            set
            {
                HttpContext.Current.Session[Cls_Sesiones.S_Imagen_Sistema] = value;
            }
        }

        public static String Nombre_Carpeta
        {
            get
            {
                if (HttpContext.Current.Session[Cls_Sesiones.S_Nombre_Carpeta] == null)
                    return String.Empty;
                else
                    return HttpContext.Current.Session[Cls_Sesiones.S_Nombre_Carpeta].ToString();
            }
            set
            {
                HttpContext.Current.Session[Cls_Sesiones.S_Nombre_Carpeta] = value;
            }
        }

        public static String Habilitar_Contenerdor_No_Piezas
        {
            get
            {
                if (HttpContext.Current.Session[Cls_Sesiones.S_Habilitar_Contenerdor_No_Piezas] == null)
                    return String.Empty;
                else
                    return HttpContext.Current.Session[Cls_Sesiones.S_Habilitar_Contenerdor_No_Piezas].ToString();
            }
            set
            {
                HttpContext.Current.Session[Cls_Sesiones.S_Habilitar_Contenerdor_No_Piezas] = value;
            }
        }

        public static String Generar_Folio_Poliza
        {
            get
            {
                if (HttpContext.Current.Session[Cls_Sesiones.S_Generar_Folio_Poliza] == null)
                    return String.Empty;
                else
                    return HttpContext.Current.Session[Cls_Sesiones.S_Generar_Folio_Poliza].ToString();
            }
            set
            {
                HttpContext.Current.Session[Cls_Sesiones.S_Generar_Folio_Poliza] = value;
            }
        }

        public static String Separador_Cuenta_Contable
        {
            get
            {
                if (HttpContext.Current.Session[Cls_Sesiones.S_Separador_Cuenta_Contable] == null)
                    return String.Empty;
                else
                    return HttpContext.Current.Session[Cls_Sesiones.S_Separador_Cuenta_Contable].ToString();
            }
            set
            {
                HttpContext.Current.Session[Cls_Sesiones.S_Separador_Cuenta_Contable] = value;
            }
        }
        public static String Bloqueo_Pantalla
        {
            get
            {
                // Verifica si es null
                if (HttpContext.Current.Session[Cls_Sesiones.S_Bloqueo_Pantalla] == null)
                    return String.Empty;
                else
                    return HttpContext.Current.Session[Cls_Sesiones.S_Bloqueo_Pantalla].ToString();
            }
            set
            {
                HttpContext.Current.Session[Cls_Sesiones.S_Bloqueo_Pantalla] = value;
            }
        }
    }
}
