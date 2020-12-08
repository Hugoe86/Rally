using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Data;
using datos_trazabilidad;
using admin_trazabilidad.Models.Negocio;

namespace admin_trazabilidad.Models.Ayudante
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