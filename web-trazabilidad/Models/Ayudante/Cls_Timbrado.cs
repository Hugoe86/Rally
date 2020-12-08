using CryptoSysPKI;
using System;
using System.Xml;
using web_trazabilidad.Models.Negocio.Facturacion;
using web_trazabilidad.Models.Negocio.Trazabilidad;
using System.Collections.Generic;
using datos_trazabilidad;
using System.Data;
using web_trazabilidad.Models.Negocio;
using System.Text.RegularExpressions;
using System.Text;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using Gma.QrCodeNet.Encoding;
using Gma.QrCodeNet.Encoding.Windows.Render;
using web_trazabilidad.traza;
using System.Linq;

namespace web_trazabilidad.Models.Ayudante
{
    public class Cls_Timbrado
    {

        public static String Generar_Timbrado(Boolean Timbre_Produccion, String Version, String Codigo_Usuario_Proveedor, String Codigo_Usuario, String Id_Sucursal, String Texto_Xml)
        {
            TimbreFiscalDigitalSoapClient Timbrado = new TimbreFiscalDigitalSoapClient();
            String Respuesta = String.Empty;
            String Parametros = String.Empty;
            XmlDocument Xml_Documento = new XmlDocument();
            XmlDocument Xml_Parametro = new XmlDocument();
            XmlElement Raiz;
            XmlDeclaration Declaracion;

            try
            {
                if (!String.IsNullOrEmpty(Version) && !String.IsNullOrEmpty(Codigo_Usuario_Proveedor) && !String.IsNullOrEmpty(Codigo_Usuario)
                    && !String.IsNullOrEmpty(Id_Sucursal) && !String.IsNullOrEmpty(Texto_Xml))
                {
                    if (Xml_Parametro.ChildNodes.Count == 0)
                    {
                        Declaracion = Xml_Parametro.CreateXmlDeclaration("1.0", "UTF-8", String.Empty);
                        Xml_Parametro.AppendChild(Declaracion);
                        Raiz = Xml_Parametro.CreateElement("Parametros");
                    }
                    else
                    {
                        Raiz = Xml_Parametro.DocumentElement;
                        Raiz.RemoveAll();
                    }

                    Raiz.SetAttribute("Version", Version);
                    Raiz.SetAttribute("CodigoUsuarioProveedor", Codigo_Usuario_Proveedor);
                    Raiz.SetAttribute("CodigoUsuario", Codigo_Usuario);
                    Raiz.SetAttribute("IdSucursal", Id_Sucursal);
                    Raiz.SetAttribute("TextoXml", Texto_Xml);

                    Xml_Parametro.AppendChild(Raiz);

                    Parametros = Xml_Parametro.InnerXml;

                    // Ejecuta el web service con los parametros
                    if (Timbre_Produccion) // Si es de produccion
                    {
                        Timbrado.GenerarTimbre(Parametros, out Respuesta);
                    }
                    else // Si es para prueba
                    {
                        Timbrado.GenerarTimbrePrueba(Parametros, out Respuesta);
                    }
                }
                else
                {
                    Respuesta = "Error: ";
                    if (String.IsNullOrEmpty(Version))
                    {
                        Respuesta += "Proporcione la versi&oacute;n";
                    }
                    if (String.IsNullOrEmpty(Codigo_Usuario_Proveedor))
                    {
                        Respuesta = (Respuesta.Length > 10 ? ", el codigo de usuario proveedor" : "Proporcione el codigo de usuario proveedor");
                    }
                    if (String.IsNullOrEmpty(Codigo_Usuario))
                    {
                        Respuesta = (Respuesta.Length > 10 ? ", el codigo de usuario" : "Proporcione el codigo de usuario");
                    }
                    if (String.IsNullOrEmpty(Id_Sucursal))
                    {
                        Respuesta = (Respuesta.Length > 10 ? ", el codigo del id de la sucursal" : "Proporcione el codigo del id de la sucursal");
                    }
                    if (String.IsNullOrEmpty(Texto_Xml))
                    {
                        Respuesta = (Respuesta.Length > 10 ? ", el XML en texto" : "Proporcione el XML en texto");
                    }
                    Respuesta += " del Timbrado.";
                }
            }
            catch (Exception Ex)
            {
                throw new Exception("Error Timbrado: " + Ex.Message);
            }

            return Respuesta;
        }

        ///******************************************************************************* 
        ///NOMBRE DE LA FUNCIÓN: Generar_Codigo_QR
        ///DESCRIPCIÓN: Crea y guarda el codigo bidimensional
        ///PARAMETROS:  
        ///             Codigo_Bidimensional, Cadena con la informacion general de la
        ///                         factura, que se reflejara en el codigo QR.
        ///             Ruta, Ruta donde se guardara la imagen
        ///CREO:        Luis Alberto Salas Garcia
        ///FECHA_CREO:  10/Junio/2013
        ///MODIFICO: 
        ///FECHA_MODIFICO:
        ///CAUSA_MODIFICACIÓN:
        ///*******************************************************************************
        public static void Generar_Codigo_QR(String Codigo_Bidimensional, String Ruta)
        {
            try
            {
                QrEncoder Qr_Encoder = new QrEncoder(ErrorCorrectionLevel.H);
                QrCode Qr_Code = new QrCode();
                MemoryStream Ms = new MemoryStream();
                Qr_Encoder.TryEncode(Codigo_Bidimensional, out Qr_Code);

                GraphicsRenderer G_Render = new GraphicsRenderer(new FixedModuleSize(2, QuietZoneModules.Two), Brushes.Black, System.Drawing.Brushes.White);
                G_Render.WriteToStream(Qr_Code.Matrix, ImageFormat.Png, Ms);

                Bitmap ImageTemp = new Bitmap(Ms);
                Bitmap Image = new Bitmap(ImageTemp, new Size(new Point(200, 200)));

                Image.Save(Ruta, ImageFormat.Png);

                //Limpia las variables
                Qr_Encoder = null;
                Qr_Code = null;
                Ms = null;
                G_Render = null;
                ImageTemp = null;
                Image = null;
            }
            catch (Exception Ex)
            {
                throw new Exception("Generar_Codigo_QR Error: " + Ex.Message);
            }
        }

        ///******************************************************************************* 
        ///NOMBRE DE LA FUNCIÓN: Valida_Caracteres_UTF
        ///DESCRIPCIÓN: Convierte la cadena original a UTF8
        ///PARAMETROS:  
        ///             Cadena_Original, Cadena con la informacion general de la factura
        ///CREO:        Luis Alberto Salas Garcia
        ///FECHA_CREO:  10/Junio/2013
        ///MODIFICO: 
        ///FECHA_MODIFICO:
        ///CAUSA_MODIFICACIÓN:
        ///*******************************************************************************
        public static String Valida_Caracteres_UTF(String Cadena_Original)
        {
            String Cadena_Copia = String.Empty;
            String Cadena_UTF = String.Empty;
            byte[] Bytes;
            long Tamanio;

            try
            {
                Cadena_Copia = Cadena_Original;

                Tamanio = Cnv.CheckUTF8(Cadena_Copia);
                if (Tamanio < 0)
                {
                    throw new Exception("Valida_Caracteres_UTF Error: No se puede convertir la cadena a UTF8.");
                }
                else
                {
                    Bytes = System.Text.Encoding.UTF8.GetBytes(Cadena_Copia);
                    Cadena_UTF = System.Text.Encoding.UTF8.GetString(Bytes);
                }
            }
            catch (Exception Ex)
            {
                throw new Exception("Valida_Caracteres_UTF Error: " + Ex.Message);
            }

            return Cadena_UTF;
        }

        public static String Genera_Cadena_Original(Cls_Ope_Factura_Electronica_Negocio Rs_Factura, String Expedida_En,List<Cls_Ope_Pos_Ventas_Detalles_Negocio> Dt_Factura_Detalles, DataTable Dt_Impuestos)
        {
            String Cadena_Original = String.Empty;
            //DateTime Fecha_Creo_Xml;

           
            try
            {
                //DateTime.TryParseExact(Rs_Factura.Fecha_Creo_Xml, "dd/MM/yyyy HH:mm:ss", null, System.Globalization.DateTimeStyles.None, out Fecha_Creo_Xml);
               
                // Datos Generales
                Cadena_Original += "||" + Rs_Factura.Timbre_Version;
                Cadena_Original += "|" + Cls_Utilidades.CFD_Elimina_Espacios(Rs_Factura.Serie); //serie opcional
                Cadena_Original += "|" + Cls_Utilidades.CFD_Elimina_Espacios(Convert.ToInt32(Rs_Factura.No_Factura).ToString()); //folio opcional
                Cadena_Original += "|" + String.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(Rs_Factura.Fecha_Creo_Xml));
                Cadena_Original += "T" + String.Format("{0:HH:mm:ss}", Convert.ToDateTime(Rs_Factura.Fecha_Creo_Xml));
                Cadena_Original += "|" + Cls_Utilidades.CFD_Elimina_Espacios(Rs_Factura.Clave_Forma_Pago);
                Cadena_Original += "|" + Cls_Utilidades.CFD_Elimina_Espacios(Rs_Factura.No_Certificado);
                if (!string.IsNullOrEmpty(Rs_Factura.Condiciones_Pago))
                    Cadena_Original += "|" + Cls_Utilidades.CFD_Elimina_Espacios(Rs_Factura.Condiciones_Pago);
                Cadena_Original += "|" + Rs_Factura.Subtotal;
                if (Rs_Factura.Descuento > 0 && Rs_Factura.Descuento!=null)
                    Cadena_Original += "|" + Rs_Factura.Descuento;
                Cadena_Original += "|" + Cls_Utilidades.CFD_Elimina_Espacios(Rs_Factura.Tipo_Moneda);
                if (Rs_Factura.Tipo_Moneda != "MXN" && Rs_Factura.Tipo_Moneda != "XXX")
                    Cadena_Original += "|" + Rs_Factura.Tipo_Cambio;
                Cadena_Original += "|" + String.Format("{0:#0.00}", Double.Parse(Rs_Factura.Total.ToString()));
                Cadena_Original += "|" + Cls_Utilidades.CFD_Elimina_Espacios(Rs_Factura.Tipo_Comprobante);
                Cadena_Original += "|" + Cls_Utilidades.CFD_Elimina_Espacios(Rs_Factura.Clave_Metodo_Pago);
                Cadena_Original += "|" + Expedida_En;//CP

                if (Rs_Factura.Tipo_Comprobante == "E" && !string.IsNullOrEmpty(Rs_Factura.UUID_Relacionado))
                {
                    Cadena_Original += "|" + Rs_Factura.Clave_Tipo_Relacion;//tipo relacion
                    Cadena_Original += "|" + Rs_Factura.UUID_Relacionado;//uuid
                }

                // Datos del Emisor
                Cadena_Original += "|" + Cls_Utilidades.CFD_Elimina_Espacios(Rs_Factura.RFC_Emisor);
                Cadena_Original += "|" + Cls_Utilidades.CFD_Elimina_Espacios(Rs_Factura.Razon_Social_Emisor);
                Cadena_Original += "|" + Cls_Utilidades.CFD_Elimina_Espacios(Rs_Factura.Clave_Regimen_Fiscal_Emisor);

                // Datos del receptor
                Cadena_Original += "|" + Cls_Utilidades.CFD_Elimina_Espacios(Rs_Factura.RFC);
                if (!string.IsNullOrEmpty(Rs_Factura.Razon_Social))
                    Cadena_Original += "|" + Cls_Utilidades.CFD_Elimina_Espacios(Rs_Factura.Razon_Social);

                if (!string.IsNullOrEmpty(Rs_Factura.Clave_Pais_Receptor) && Rs_Factura.Clave_Pais_Receptor != "MEX")
                {
                    Cadena_Original += "|" + Cls_Utilidades.CFD_Elimina_Espacios(Rs_Factura.Clave_Pais_Receptor);
                    Cadena_Original += "|" + "0000000000000"; //NumRegIdTrib
                }
                Cadena_Original += "|" + Cls_Utilidades.CFD_Elimina_Espacios(Rs_Factura.Clave_Uso_CFDI); //

                // Datos de los Conceptos
                foreach (var Fila_Tabla in Dt_Factura_Detalles)
                {

                    Cadena_Original += "|" + Cls_Utilidades.CFD_Elimina_Espacios(String.IsNullOrEmpty(Fila_Tabla.Clave_Sat_Producto) ? "01010101" : Fila_Tabla.Clave_Sat_Producto);//ClaveProdServ
                    Cadena_Original += "|" + Cls_Utilidades.CFD_Elimina_Espacios(Fila_Tabla.Codigo);//NoIdentificacion
                    Cadena_Original += "|" + String.Format("{0:#0.00}", Double.Parse(Fila_Tabla.Cantidad.ToString()));//Cantidad
                    Cadena_Original += "|" + Cls_Utilidades.CFD_Elimina_Espacios(String.IsNullOrEmpty(Fila_Tabla.Clave_Sat_Unidad) ? "ACT" : Fila_Tabla.Clave_Sat_Unidad);//ClaveUnidad
                    if (!String.IsNullOrEmpty(Fila_Tabla.UnidadAlm_Nombre))
                        Cadena_Original += "|" + Cls_Utilidades.CFD_Elimina_Espacios(Fila_Tabla.UnidadAlm_Nombre); //Unidad - opcional
                    Cadena_Original += "|" + Cls_Utilidades.CFD_Elimina_Espacios(Fila_Tabla.Nombre_Producto); //Descripcion producto
                    Cadena_Original += "|" + String.Format("{0:#0.00}", Double.Parse(Fila_Tabla.Precio.ToString()));//precio unitario
                    Cadena_Original += "|" + String.Format("{0:#0.00}", Double.Parse(Fila_Tabla.Subtotal.ToString()));//subtotal
                      if (Fila_Tabla.Descuento > 0)
                    Cadena_Original += "|" + String.Format("{0:#0.00}", Double.Parse(Fila_Tabla.Descuento.ToString()));//descuento

                    //incluimos los datos de los impuestos del concepto
                    if (Double.Parse(Fila_Tabla.Iva.ToString()) > 0)
                    {
                        Cadena_Original += "|" + String.Format("{0:#0.00}", Double.Parse(Fila_Tabla.Subtotal.ToString()));//base
                        Cadena_Original += "|" + Cls_Utilidades.CFD_Elimina_Espacios(String.IsNullOrEmpty(Fila_Tabla.Clave_Sat_Impuesto) ? "002" : Fila_Tabla.Clave_Sat_Impuesto); //impuesto
                        Cadena_Original += "|" + Cls_Utilidades.CFD_Elimina_Espacios(String.IsNullOrEmpty(Fila_Tabla.Tipo_Factor) ? "Tasa" : Fila_Tabla.Tipo_Factor); //impuesto; //tipo factor
                        Cadena_Original += "|" + String.Format("{0:#0.00}", Double.Parse(Fila_Tabla.Tasa.ToString()));//tasa o cuota
                        Cadena_Original += "|" + String.Format("{0:#0.00}", Double.Parse(Fila_Tabla.Iva.ToString()));//importe
                    }
                }

                // Datos Impuestos
                if (Dt_Impuestos.Rows.Count > 0)
                {
                    foreach (DataRow Fila_Tabla in Dt_Impuestos.Rows)
                    {
                        if (double.Parse(Fila_Tabla["IMPORTE"].ToString()) > 0)
                        {
                            Cadena_Original += "|" + Cls_Utilidades.CFD_Elimina_Espacios(string.IsNullOrEmpty(Fila_Tabla["CLAVE_IMPUESTO"].ToString()) ? "002" : Fila_Tabla["CLAVE_IMPUESTO"].ToString()); //impuesto
                            Cadena_Original += "|" + Cls_Utilidades.CFD_Elimina_Espacios(string.IsNullOrEmpty(Fila_Tabla["TIPOFACTOR"].ToString()) ? "Tasa" : Fila_Tabla["TIPOFACTOR"].ToString()); //tipo-factor
                            Cadena_Original += "|" + Cls_Utilidades.CFD_Elimina_Espacios(string.IsNullOrEmpty(Fila_Tabla["TASA"].ToString()) ? "0.160000" : Fila_Tabla["TASA"].ToString());//tasa o cuota
                            Cadena_Original += "|" + String.Format("{0:#0.00}", Double.Parse(Fila_Tabla["IMPORTE"].ToString())); //importe
                        }
                    }
                    Cadena_Original += "|" + String.Format("{0:#0.00}", Dt_Impuestos.AsEnumerable().Sum(x => x.Field<Double>("IMPORTE"))); //total impuestos trasladados
                }

                Cadena_Original += "||";

                // Elimina dobles espacios
                Cadena_Original = Regex.Replace(Cadena_Original, @"\s+", " ");
            }
            catch (Exception Ex)
            {
                throw new Exception("Genera_Cadena_Original Error: " + Ex.Message);
            }

            return Cadena_Original;
        }

        //Para nota de crédito, genera cadena original
        public static String Genera_Cadena_Original_NC(Cls_Ope_Fac_Notas_Credito_Negocio Rs_Factura, String Expedida_En, List<Cls_Ope_Fac_Notas_Credito_Detalles_Negocio> Dt_Factura_Detalles, DataTable Dt_Impuestos)
        {
            String Cadena_Original = String.Empty;
            //DateTime Fecha_Creo_Xml;


            try
            {
                //DateTime.TryParseExact(Rs_Factura.Fecha_Creo_Xml, "dd/MM/yyyy HH:mm:ss", null, System.Globalization.DateTimeStyles.None, out Fecha_Creo_Xml);

                // Datos Generales
                Cadena_Original += "||" + Rs_Factura.Timbre_Version;
                Cadena_Original += "|" + Cls_Utilidades.CFD_Elimina_Espacios(Rs_Factura.Serie); //serie opcional
                Cadena_Original += "|" + Cls_Utilidades.CFD_Elimina_Espacios(Convert.ToInt32(Rs_Factura.No_Nota_Credito).ToString()); //folio opcional
                Cadena_Original += "|" + String.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(Rs_Factura.Fecha_Creo_Xml));
                Cadena_Original += "T" + String.Format("{0:HH:mm:ss}", Convert.ToDateTime(Rs_Factura.Fecha_Creo_Xml));
                Cadena_Original += "|" + Cls_Utilidades.CFD_Elimina_Espacios(Rs_Factura.Clave_Forma_Pago);
                Cadena_Original += "|" + Cls_Utilidades.CFD_Elimina_Espacios(Rs_Factura.No_Certificado);
                if (!string.IsNullOrEmpty(Rs_Factura.Condiciones_Pago))
                    Cadena_Original += "|" + Cls_Utilidades.CFD_Elimina_Espacios(Rs_Factura.Condiciones_Pago);
                Cadena_Original += "|" + Rs_Factura.Subtotal;
                if (Rs_Factura.Descuento > 0 && Rs_Factura.Descuento != null)
                    Cadena_Original += "|" + Rs_Factura.Descuento;
                Cadena_Original += "|" + Cls_Utilidades.CFD_Elimina_Espacios(Rs_Factura.Tipo_Moneda);
                if (Rs_Factura.Tipo_Moneda != "MXN" && Rs_Factura.Tipo_Moneda != "XXX")
                    Cadena_Original += "|" + Rs_Factura.Tipo_Cambio;
                Cadena_Original += "|" + String.Format("{0:#0.00}", Double.Parse(Rs_Factura.Total.ToString()));
                Cadena_Original += "|" + Cls_Utilidades.CFD_Elimina_Espacios(Rs_Factura.Tipo_Comprobante);
                Cadena_Original += "|" + Cls_Utilidades.CFD_Elimina_Espacios(Rs_Factura.Clave_Metodo_Pago);
                Cadena_Original += "|" + Expedida_En;//CP

                if (Rs_Factura.Tipo_Comprobante == "E" && !string.IsNullOrEmpty(Rs_Factura.UUID_Relacionado))
                {
                    Cadena_Original += "|" + Rs_Factura.Clave_Tipo_Relacion;//tipo relacion
                    Cadena_Original += "|" + Rs_Factura.UUID_Relacionado;//uuid
                }

                // Datos del Emisor
                Cadena_Original += "|" + Cls_Utilidades.CFD_Elimina_Espacios(Rs_Factura.RFC_Emisor);
                Cadena_Original += "|" + Cls_Utilidades.CFD_Elimina_Espacios(Rs_Factura.Razon_Social_Emisor);
                Cadena_Original += "|" + Cls_Utilidades.CFD_Elimina_Espacios(Rs_Factura.Clave_Regimen_Fiscal_Emisor);

                // Datos del receptor
                Cadena_Original += "|" + Cls_Utilidades.CFD_Elimina_Espacios(Rs_Factura.RFC);
                if (!string.IsNullOrEmpty(Rs_Factura.Razon_Social))
                    Cadena_Original += "|" + Cls_Utilidades.CFD_Elimina_Espacios(Rs_Factura.Razon_Social);

                if (!string.IsNullOrEmpty(Rs_Factura.Clave_Pais_Receptor) && Rs_Factura.Clave_Pais_Receptor != "MEX")
                {
                    Cadena_Original += "|" + Cls_Utilidades.CFD_Elimina_Espacios(Rs_Factura.Clave_Pais_Receptor);
                    Cadena_Original += "|" + "0000000000000"; //NumRegIdTrib
                }
                Cadena_Original += "|" + Cls_Utilidades.CFD_Elimina_Espacios(Rs_Factura.Clave_Uso_CFDI); //

                // Datos de los Conceptos
                foreach (var Fila_Tabla in Dt_Factura_Detalles)
                {

                    Cadena_Original += "|" + Cls_Utilidades.CFD_Elimina_Espacios(String.IsNullOrEmpty(Fila_Tabla.Clave_Sat_Producto) ? "01010101" : Fila_Tabla.Clave_Sat_Producto);//ClaveProdServ
                    Cadena_Original += "|" + Cls_Utilidades.CFD_Elimina_Espacios(Fila_Tabla.Codigo);//NoIdentificacion
                    Cadena_Original += "|" + String.Format("{0:#0.00}", Double.Parse(Fila_Tabla.Cantidad.ToString()));//Cantidad
                    Cadena_Original += "|" + Cls_Utilidades.CFD_Elimina_Espacios(String.IsNullOrEmpty(Fila_Tabla.Clave_Sat_Unidad) ? "ACT" : Fila_Tabla.Clave_Sat_Unidad);//ClaveUnidad
                    if (!String.IsNullOrEmpty(Fila_Tabla.UnidadAlm_Nombre))
                        Cadena_Original += "|" + Cls_Utilidades.CFD_Elimina_Espacios(Fila_Tabla.UnidadAlm_Nombre); //Unidad - opcional
                    Cadena_Original += "|" + Cls_Utilidades.CFD_Elimina_Espacios(Fila_Tabla.Nombre_Producto); //Descripcion producto
                    Cadena_Original += "|" + String.Format("{0:#0.00}", Double.Parse(Fila_Tabla.Precio.ToString()));//precio unitario
                    Cadena_Original += "|" + String.Format("{0:#0.00}", Double.Parse(Fila_Tabla.Subtotal.ToString()));//subtotal

                    //incluimos los datos de los impuestos del concepto
                    if (Double.Parse(Fila_Tabla.Iva.ToString()) > 0)
                    {
                        Cadena_Original += "|" + String.Format("{0:#0.00}", Double.Parse(Fila_Tabla.Subtotal.ToString()));//base
                        Cadena_Original += "|" + Cls_Utilidades.CFD_Elimina_Espacios(String.IsNullOrEmpty(Fila_Tabla.Clave_Sat_Impuesto) ? "002" : Fila_Tabla.Clave_Sat_Impuesto); //impuesto
                        Cadena_Original += "|" + Cls_Utilidades.CFD_Elimina_Espacios(String.IsNullOrEmpty(Fila_Tabla.Tipo_Factor) ? "Tasa" : Fila_Tabla.Tipo_Factor); //impuesto; //tipo factor
                        Cadena_Original += "|" + String.Format("{0:#0.00}", Double.Parse(Fila_Tabla.Tasa.ToString()));//tasa o cuota
                        Cadena_Original += "|" + String.Format("{0:#0.00}", Double.Parse(Fila_Tabla.Iva.ToString()));//importe
                    }
                }

                // Datos Impuestos
                if (Dt_Impuestos.Rows.Count > 0)
                {
                    foreach (DataRow Fila_Tabla in Dt_Impuestos.Rows)
                    {
                        if (double.Parse(Fila_Tabla["IMPORTE"].ToString()) > 0)
                        {
                            Cadena_Original += "|" + Cls_Utilidades.CFD_Elimina_Espacios(string.IsNullOrEmpty(Fila_Tabla["CLAVE_IMPUESTO"].ToString()) ? "002" : Fila_Tabla["CLAVE_IMPUESTO"].ToString()); //impuesto
                            Cadena_Original += "|" + Cls_Utilidades.CFD_Elimina_Espacios(string.IsNullOrEmpty(Fila_Tabla["TIPOFACTOR"].ToString()) ? "Tasa" : Fila_Tabla["TIPOFACTOR"].ToString()); //tipo-factor
                            Cadena_Original += "|" + Cls_Utilidades.CFD_Elimina_Espacios(string.IsNullOrEmpty(Fila_Tabla["TASA"].ToString()) ? "0.160000" : Fila_Tabla["TASA"].ToString());//tasa o cuota
                            Cadena_Original += "|" + String.Format("{0:#0.00}", Double.Parse(Fila_Tabla["IMPORTE"].ToString())); //importe
                        }
                    }
                    Cadena_Original += "|" + String.Format("{0:#0.00}", Dt_Impuestos.AsEnumerable().Sum(x => x.Field<Double>("IMPORTE"))); //total impuestos trasladados
                }

                Cadena_Original += "||";

                // Elimina dobles espacios
                Cadena_Original = Regex.Replace(Cadena_Original, @"\s+", " ");
            }
            catch (Exception Ex)
            {
                throw new Exception("Genera_Cadena_Original Error: " + Ex.Message);
            }

            return Cadena_Original;
        }


        ///******************************************************************************* 
        ///NOMBRE DE LA FUNCIÓN: Genera_Sello
        ///DESCRIPCIÓN: Genera el sello de la factura.
        ///PARAMETROS:  
        ///             Cadena_UTF8, Cadena orginal en formato UTF8
        ///             Archivo_Llave, Archivo que contiene la llave
        ///             Password, Contraseña del archivo llave
        ///             Anio, Año de expedicion de la factura
        ///CREO:        Luis Alberto Salas Garcia
        ///FECHA_CREO:  10/Junio/2013
        ///MODIFICO: 
        ///FECHA_MODIFICO:
        ///CAUSA_MODIFICACIÓN:
        ///*******************************************************************************
        public static String Genera_Sello(String Cadena_UTF8, String Archivo_Llave, String Password)
        {
            String Sello = String.Empty;
            StringBuilder Llave_Privada;
            //Encoding Codificador_Base64= new 64
            byte[] Mensaje;
            byte[] Respuesta;
            int nBlock;

            try
            {
                Llave_Privada = Rsa.ReadEncPrivateKey(Archivo_Llave, Password);

                if (Llave_Privada.Length == 0)
                {
                    throw new Exception("Genera_Sello Error: No se puede leer la llave privada.");
                }
                else
                {
                    if (Cnv.CheckUTF8(Cadena_UTF8) >= 0)
                    {
                        Mensaje = System.Text.Encoding.UTF8.GetBytes(Cadena_UTF8);
                        nBlock = Rsa.KeyBytes(Llave_Privada.ToString().ToString());
                        //nBlock = 64;
                        Respuesta = Rsa.EncodeMsgForSignature(nBlock, Mensaje, HashAlgorithm.Sha256);

                        Respuesta = Rsa.RawPrivate(Respuesta, Llave_Privada.ToString().ToString());
                        Sello = Convert.ToBase64String(Respuesta);
                        Wipe.String(Llave_Privada);
                    }
                    else
                    {
                        throw new Exception("Genera_Sello Error: La Cadena UTF no es v&aacute;lida.");
                    }
                }
            }
            catch (Exception Ex)
            {
                throw new Exception("Genera_Sello Error: " + Ex.Message);
            }

            return Sello;
        }


        ///******************************************************************************* 
        ///NOMBRE DE LA FUNCIÓN: Consulta_Serie_Certificado
        ///DESCRIPCIÓN: Consulta la serie del certificado.
        ///PARAMETROS:  
        ///             Ruta_Certificado, Cadena con la ruta del certificado
        ///CREO:        Luis Alberto Salas Garcia
        ///FECHA_CREO:  10/Junio/2013
        ///MODIFICO: 
        ///FECHA_MODIFICO:
        ///CAUSA_MODIFICACIÓN:
        ///*******************************************************************************
        public static String Consulta_Serie_Certificado(String Ruta_Certificado)
        {
            String Serie = String.Empty;
            String Serie_Sat = String.Empty;

            try
            {
                // Obtiene el numero de serie
                Serie = X509.CertSerialNumber(Ruta_Certificado);

                // Convierte la serie original de hex a cadena
                Serie_Sat = System.Text.Encoding.Default.GetString(Cnv.FromHex(Serie));
            }
            catch (Exception Ex)
            {
                throw new Exception("Consulta_Serie_Certificado Error: " + Ex.Message);
            }

            return Serie_Sat;
        }

        ///******************************************************************************* 
        ///NOMBRE DE LA FUNCIÓN: Consulta_Certificado
        ///DESCRIPCIÓN: Consulta el certificado.
        ///PARAMETROS:  
        ///             Ruta_Certificado, Cadena con la ruta del certificado
        ///CREO:        Luis Alberto Salas Garcia
        ///FECHA_CREO:  10/Junio/2013
        ///MODIFICO: 
        ///FECHA_MODIFICO:
        ///CAUSA_MODIFICACIÓN:
        ///*******************************************************************************
        public static String Consulta_Certificado(String Ruta_Certificado)
        {
            String Certificado = String.Empty;
            try
            {
                Certificado = X509.ReadStringFromFile(Ruta_Certificado);
            }
            catch (Exception Ex)
            {
                throw new Exception("Consulta_Certificado Error: " + Ex.Message);
            }

            return Certificado;
        }

        ///******************************************************************************* 
        ///NOMBRE DE LA FUNCIÓN: Cancelar_Factura
        ///DESCRIPCIÓN: Cancela la factura ante el SAT.
        ///PARAMETROS:  
        ///             Timbre_Produccion, Determina si el timbre es para produccion o de prueba
        ///             Texto_Xml, Cadena que contiene la informacion de la factura
        ///CREO:        Luis Alberto Salas Garcia
        ///FECHA_CREO:  24/Junio/2013
        ///MODIFICO: 
        ///FECHA_MODIFICO:
        ///CAUSA_MODIFICACIÓN:
        ///*******************************************************************************
        public static String Cancelar_Factura(Boolean Timbre_Produccion, String RFC_Emisor, String Codigo_Usuario_Proveedor, String Codigo_Usuario, String Codigo_UUID)
        {
            TimbreFiscalDigitalSoapClient Timbrado = new TimbreFiscalDigitalSoapClient();
            String Respuesta = String.Empty;
            String Parametros = String.Empty;
            XmlDocument Xml_Parametro = new XmlDocument();
            XmlNode Raiz;
            XmlNode xmlFolios;
            XmlNode xmlUUID;
            XmlNode Declaracion;
            XmlAttribute Atributo;

            try
            {
                if (!String.IsNullOrEmpty(Codigo_Usuario_Proveedor) && !String.IsNullOrEmpty(Codigo_Usuario)
                    && !String.IsNullOrEmpty(RFC_Emisor) && !String.IsNullOrEmpty(Codigo_UUID))
                {
                    //Forma los parametros de entrada
                    Declaracion = Xml_Parametro.CreateXmlDeclaration("1.0", "UTF-8", String.Empty);
                    Xml_Parametro.AppendChild(Declaracion);
                    Raiz = Xml_Parametro.CreateNode(XmlNodeType.Element, "Parametros", "");
                    Xml_Parametro.AppendChild(Raiz);

                    Atributo = Raiz.OwnerDocument.CreateAttribute("CodigoUsuario");
                    Atributo.Value = Codigo_Usuario;
                    Raiz.Attributes.Append(Atributo);

                    Atributo = Raiz.OwnerDocument.CreateAttribute("CodigoUsuarioProveedor");
                    Atributo.Value = Codigo_Usuario_Proveedor;
                    Raiz.Attributes.Append(Atributo);

                    Atributo = Raiz.OwnerDocument.CreateAttribute("RFCEmisor");
                    Atributo.Value = RFC_Emisor;
                    Raiz.Attributes.Append(Atributo);

                    xmlFolios = Xml_Parametro.CreateNode(XmlNodeType.Element, "Folios", "");

                    xmlUUID = Xml_Parametro.CreateNode(XmlNodeType.Element, "UUID", "");
                    xmlUUID.InnerText = Codigo_UUID;

                    xmlFolios.AppendChild(xmlUUID);

                    Raiz.AppendChild(xmlFolios);

                    Parametros = Xml_Parametro.InnerXml;

                    // Ejecuta el web service con los parametros
                    if (Timbre_Produccion) // Si es de produccion
                    {
                        Timbrado.ReportarCancelacion(Parametros, out Respuesta);
                    }
                    else // Si es para prueba
                    {
                        Timbrado.ReportarCancelacionPrueba(Parametros, out Respuesta);
                    }
                }
                else
                {
                    Respuesta = "Error: ";
                    if (String.IsNullOrEmpty(RFC_Emisor))
                    {
                        Respuesta = (Respuesta.Length > 10 ? ", el RFC del emisor" : "Proporcione el RFC del emisor");
                    }
                    if (String.IsNullOrEmpty(Codigo_Usuario_Proveedor))
                    {
                        Respuesta = (Respuesta.Length > 10 ? ", el codigo de usuario proveedor" : "Proporcione el codigo de usuario proveedor");
                    }
                    if (String.IsNullOrEmpty(Codigo_Usuario))
                    {
                        Respuesta = (Respuesta.Length > 10 ? ", el codigo de usuario" : "Proporcione el codigo de usuario");
                    }
                    if (String.IsNullOrEmpty(Codigo_UUID))
                    {
                        Respuesta = (Respuesta.Length > 10 ? ", el codigo UUID" : "Proporcione el codigo UUID de timbrado");
                    }
                    Respuesta += " del Timbrado.";
                }
            }
            catch (Exception Ex)
            {
                throw new Exception("Error Timbrado: " + Ex.Message);
            }

            return Respuesta;
        }
    }
}