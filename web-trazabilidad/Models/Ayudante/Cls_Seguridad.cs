using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Text;
using System.Security.Cryptography;
using web_trazabilidad.Models.Negocio.Trazabilidad;
using datos_trazabilidad;

namespace web_trazabilidad.Models.Ayudante
{
    public class Cls_Seguridad
    {
        private static string key = "ABCDEFGHIJKLMÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyz0123456789_-";
        //constructorpublic 
        Cls_Seguridad()
        {
            /* Establecer una clave. La misma clave    
             * debe ser utilizada para descifrar    
             * los datos que son cifrados con esta clave.    
             * pueden ser los caracteres que uno desee*/
            //key = "ABCDEFGHIJKLMÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyz0123456789_-";
        }


        /// ************************************************************************************
        /// Nombre          : Encriptar
        /// Descripción     : Metodo que encriptara texto.
        /// Parámetros      : Texto, texto que encriptara
        /// Usuario Creo    :
        /// Fecha Creó      : 
        /// Usuario Modifico:
        /// Fecha Modifico  :
        /// ***********************************************************************************
        public static string Encriptar(String Texto)
        {
            TripleDESCryptoServiceProvider Encriptador = new TripleDESCryptoServiceProvider();
            ICryptoTransform Criptografia;
            MD5CryptoServiceProvider Hash_Md5 = new MD5CryptoServiceProvider();//   se utilizan las clases de encriptación provistas por el Framework  Algoritmo MD5
            byte[] Arreglo_Llave; //    arreglo de bytes donde guardaremos la llave 
            byte[] Arreglo_A_Cifrar;//  arreglo de bytes donde guardaremos el texto que vamos a encriptar
            byte[] Arreglo_Resultante;//  arreglo de bytes donde se guarda la cadena cifrada 

            try
            {
                //   
                Arreglo_Llave = Hash_Md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                Hash_Md5.Clear();

                //  se guarda la llave para que se le realice hashing 
                //  Algoritmo 3DAS 
                Encriptador.Key = Arreglo_Llave;
                Encriptador.Mode = CipherMode.ECB;
                Encriptador.Padding = PaddingMode.PKCS7;

                //  se empieza con la transformación de la cadena 
                Criptografia = Encriptador.CreateEncryptor();
                Arreglo_A_Cifrar = UTF8Encoding.UTF8.GetBytes(Texto);

                Arreglo_Resultante = Criptografia.TransformFinalBlock(Arreglo_A_Cifrar, 0, Arreglo_A_Cifrar.Length);
                Encriptador.Clear();

                //  se regresa el resultado en forma de una cadena 
                return Convert.ToBase64String(Arreglo_Resultante, 0, Arreglo_Resultante.Length);
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.ToString());
            }


        }

        /// ************************************************************************************
        /// Nombre          : Desencriptar
        /// Descripción     : Metodo que encriptara texto.
        /// Parámetros      : Texto_Encriptado, texto que se desencriptara
        /// Usuario Creo    :
        /// Fecha Creó      : 
        /// Usuario Modifico:
        /// Fecha Modifico  :
        /// ***********************************************************************************
        public static string Desencriptar(string Texto_Encriptado)
        {
            TripleDESCryptoServiceProvider Desencriptador = new TripleDESCryptoServiceProvider();
            MD5CryptoServiceProvider Hash_Md5 = new MD5CryptoServiceProvider(); //se llama a las clases que tienen los algoritmos de encriptación se le aplica hashing 
            ICryptoTransform Criptografia;
            String Texto = "";
            byte[] Arreglo_Llave;//  convierte el texto en una secuencia de bytes
            byte[] Arragle_A_Descifrar;
            byte[] Arreglo_Resultante;

            try
            {
                Arragle_A_Descifrar = Convert.FromBase64String(Texto_Encriptado);

                //algoritmo MD5 
                Arreglo_Llave = Hash_Md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                Hash_Md5.Clear();

                Desencriptador.Key = Arreglo_Llave;
                Desencriptador.Mode = CipherMode.ECB;
                Desencriptador.Padding = PaddingMode.PKCS7;

                Criptografia = Desencriptador.CreateDecryptor();
                Arreglo_Resultante = Criptografia.TransformFinalBlock(Arragle_A_Descifrar, 0, Arragle_A_Descifrar.Length);
                Desencriptador.Clear();

                //se regresa en forma de cadena
                Texto = UTF8Encoding.UTF8.GetString(Arreglo_Resultante);
            }
            catch (Exception Ex)
            {
                Texto = Texto_Encriptado;
                throw new Exception(Ex.ToString());
            }

            return Texto;
        }// fin de la funcion Desencriptar()

        public static Cls_Permisos_Usuario_Mensaje Validar_Permisos_Usuario(string Nombre_Proceso)
        {
            Cls_Permisos_Usuario_Mensaje Obj_Mensaje = new Cls_Permisos_Usuario_Mensaje();
            try
            {
                using (var dbContext = new Sistema_TrazabilidadEntities())
                {

                    var _responsable_autorizacion = (from _responsables in dbContext.Apl_Usuarios
                                                     join _usuarios_pro in dbContext.Cat_Rel_Usuarios_Procesos_Sistema
                                                        on _responsables.Usuario_ID equals _usuarios_pro.Usuario_ID
                                                     join _procesos_sistema in dbContext.Cat_Procesos_Sistema
                                                        on _usuarios_pro.Proceso_ID equals _procesos_sistema.Proceso_ID

                                                     where _responsables.Usuario_ID.ToString() == Cls_Sesiones.Usuario_ID
                                                     && _procesos_sistema.Nombre == Nombre_Proceso

                                                     select new
                                                     {
                                                         Proceso_ID = _procesos_sistema.Proceso_ID,
                                                         Nombre = _procesos_sistema.Nombre,
                                                         Usuario_ID = _usuarios_pro.Usuario_ID,
                                                         Usuario = _responsables.Nombre ?? ""

                                                     });

                    if (_responsable_autorizacion.Any())
                    {
                        var datos = _responsable_autorizacion.First();
                        Obj_Mensaje.Estatus = true;
                        Obj_Mensaje.Mensaje = "El Usuario " + datos.Usuario + " tiene permiso";
                    }
                    else
                    {
                        Obj_Mensaje.Estatus = false;
                        Obj_Mensaje.Mensaje = "El Usuario logueado no tiene permiso para realizar esta actividad.";
                    }

                }
            }
            catch (Exception ex)
            {
                Obj_Mensaje.Estatus = false;
                Obj_Mensaje.Mensaje = "Erro al hacer la validacion de permiso del usuario.";
            }
            return Obj_Mensaje;
        }
    }
}
