using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml;
using web_trazabilidad.Models.Negocio.Facturacion;

namespace web_trazabilidad.Models.Ayudante
{
    public class Cls_Facturacion_Ayudante
    {

        public static String Convertir_Cantidad_Letras(Decimal Cantidad_Numero, String Moneda)
        {
            Cls_Numeros_Letras Letras = new Cls_Numeros_Letras();
            String Cantidad_Letra = String.Empty;

            try
            {
                if (Moneda.ToUpper().Equals("MXN"))
                {
                    Letras.MascaraSalidaDecimal = "00/100 M.N.";
                    Letras.SeparadorDecimalSalida = "PESOS CON";
                }
                else
                {
                    Letras.MascaraSalidaDecimal = "00/100 DLLS";
                    Letras.SeparadorDecimalSalida = "DOLARES CON";
                }
                Letras.ApocoparUnoParteEntera = true;
                Cantidad_Letra = Letras.ToCustomCardinal(Cantidad_Numero).Trim().ToUpper();
            }
            catch (Exception Ex)
            {
                throw new Exception("Convertir_Cantidad_Letras. Error:[" + Ex.Message + "]");
            }
            return Cantidad_Letra;
        }

        public static string Obtener_Pdf(Cls_Ope_Factura_Electronica_Negocio dt_fac, String P_Ruta_PDF, String ruta_carpeta_PDF, String P_Ruta_Carpeta_Qr, String P_Ruta_Imagen, String Tipo_Comprobante)
        {
            iTextSharp.text.Document Documento = null;
            string ruta = String.Empty;
            String Ruta_Pdf = P_Ruta_PDF;
            String Nombre_Pdf = String.Empty;
            Boolean crear = false;

            try
            {

                if (!Directory.Exists(HttpContext.Current.Server.MapPath(ruta_carpeta_PDF)))
                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath(ruta_carpeta_PDF));

                ruta = Ruta_Pdf;

                if (!File.Exists(HttpContext.Current.Server.MapPath(ruta)))
                    crear = true;

                if (dt_fac.Estatus.Trim().ToLower() == "cancelada")
                    crear = true;

                if (crear)
                {
                    Documento = new iTextSharp.text.Document(iTextSharp.text.PageSize.A4, 10, 10, 10, 10);
                    PdfWriter oWriter = iTextSharp.text.pdf.PdfWriter.GetInstance(Documento, new FileStream(HttpContext.Current.Server.MapPath(ruta), FileMode.Create));
                    Documento.Open();
                    Exportar_Datos_PDF(Documento, dt_fac, oWriter, P_Ruta_Carpeta_Qr, P_Ruta_Imagen, Tipo_Comprobante);
                    Documento.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return ruta;
        }

        //obtener pdf nota de credito
        public static string Obtener_Pdf_NC(Cls_Ope_Fac_Notas_Credito_Negocio dt_fac, String P_Ruta_PDF, String ruta_carpeta_PDF, String P_Ruta_Carpeta_Qr, String P_Ruta_Imagen, String Tipo_Comprobante)
        {
            iTextSharp.text.Document Documento = null;
            string ruta = String.Empty;
            String Ruta_Pdf = P_Ruta_PDF;
            String Nombre_Pdf = String.Empty;
            Boolean crear = false;

            try
            {

                if (!Directory.Exists(HttpContext.Current.Server.MapPath(ruta_carpeta_PDF)))
                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath(ruta_carpeta_PDF));

                ruta = Ruta_Pdf;

                if (!File.Exists(HttpContext.Current.Server.MapPath(ruta)))
                    crear = true;

                if (dt_fac.Estatus.Trim().ToLower() == "cancelada")
                    crear = true;

                if (crear)
                {
                    Documento = new iTextSharp.text.Document(iTextSharp.text.PageSize.A4, 10, 10, 10, 10);
                    PdfWriter oWriter = iTextSharp.text.pdf.PdfWriter.GetInstance(Documento, new FileStream(HttpContext.Current.Server.MapPath(ruta), FileMode.Create));
                    Documento.Open();
                    Exportar_Datos_PDF_NC(Documento, dt_fac, oWriter, P_Ruta_Carpeta_Qr, P_Ruta_Imagen, Tipo_Comprobante);
                    Documento.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return ruta;
        }

        public static string Obtener_Pdf_Cancelacion_Nota_Credito(Cls_Ope_Factura_Electronica_Negocio dt_fac, String P_Ruta_PDF, String ruta_carpeta_PDF, String P_Ruta_Carpeta_Qr, String P_Ruta_Imagen, String Tipo_Comprobante)
        {
            iTextSharp.text.Document Documento = null;
            string ruta = String.Empty;
            String Ruta_Pdf = P_Ruta_PDF;
            String Nombre_Pdf = String.Empty;
            Boolean crear = false;

            try
            {
                
                if (!Directory.Exists(HttpContext.Current.Server.MapPath(ruta_carpeta_PDF)))
                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath(ruta_carpeta_PDF));

                ruta = Ruta_Pdf;

                if (!File.Exists(HttpContext.Current.Server.MapPath(ruta)))
                    crear = true;

                if (dt_fac.Estatus.Trim().ToLower() == "cancelada")
                    crear = true;

                if (crear)
                {
                    Documento = new iTextSharp.text.Document(iTextSharp.text.PageSize.A4, 10, 10, 10, 10);
                    PdfWriter oWriter = iTextSharp.text.pdf.PdfWriter.GetInstance(Documento, new FileStream(HttpContext.Current.Server.MapPath(ruta), FileMode.Create));
                    Documento.Open();
                    Exportar_Datos_PDF(Documento, dt_fac, oWriter, P_Ruta_Carpeta_Qr, P_Ruta_Imagen, Tipo_Comprobante);
                    Documento.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return ruta;
        }

        public static void Exportar_Datos_PDF(iTextSharp.text.Document Documento, Cls_Ope_Factura_Electronica_Negocio dt_fac, PdfWriter writer, String P_Ruta_Carpeta_Qr, String P_Ruta_Imagen, String Tipo_Comprobante)
        {
            string domicilio = "";
            DataTable dt_fac_det = new DataTable();
            try
            {
                iTextSharp.text.FontFactory.RegisterDirectory(@"C:\Windows\Fonts");
                //Creamos el objeto de tipo tabla para almacenar el resultado de la búsqueda. 
                iTextSharp.text.pdf.PdfPTable Rpt_Tabla = new iTextSharp.text.pdf.PdfPTable(8);
                //Obtenemos y establecemos el formato de las columnas de la tabla.
                float[] Ancho_Cabeceras = new[] { 100f, 200f, 100, 140f, 120f, 120f, 120f, 120f };
                //Creamos y definimos algunas propiedades que tendrá la fuente que se aplicara a las celdas de la tabla de resultados.
                iTextSharp.text.Font Fuente_Tabla_Contenido = iTextSharp.text.FontFactory.GetFont("Century Gothic", 7, iTextSharp.text.Font.NORMAL, BaseColor.GRAY);
                iTextSharp.text.Font Fuente_Etiquetas = iTextSharp.text.FontFactory.GetFont("Century Gothic", 10, iTextSharp.text.Font.BOLD, new BaseColor(7, 87, 127));
                iTextSharp.text.Font Fuente_Etiquetas_ = iTextSharp.text.FontFactory.GetFont("Century Gothic", 9, iTextSharp.text.Font.BOLD, new BaseColor(51, 51, 51));
                iTextSharp.text.Font Fuente_Etiquetas1 = iTextSharp.text.FontFactory.GetFont("Century Gothic", 9, iTextSharp.text.Font.NORMAL, new BaseColor(51, 51, 51));
                iTextSharp.text.Font Fuente_Valor_Etq = iTextSharp.text.FontFactory.GetFont("Century Gothic", 9, iTextSharp.text.Font.NORMAL, new BaseColor(5, 34, 73));
                iTextSharp.text.Font Fuente_Etiquetas2 = iTextSharp.text.FontFactory.GetFont("Century Gothic", 8, iTextSharp.text.Font.NORMAL, new BaseColor(51, 51, 51));
                iTextSharp.text.Font Fuente_Etiquetas2_ = iTextSharp.text.FontFactory.GetFont("Century Gothic", 7, iTextSharp.text.Font.NORMAL, new BaseColor(51, 51, 51));

                //Establecemos el formato que tendrá la tabla que mostrara el resultado de la búsqueda según el movimiento consultado.
                Rpt_Tabla.DefaultCell.Padding = 3;
                Rpt_Tabla.SetWidths(Ancho_Cabeceras);
                Rpt_Tabla.TotalWidth = 1020;
                Rpt_Tabla.DefaultCell.BorderWidth = 2;
                Rpt_Tabla.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                Rpt_Tabla.HeaderRows = 1;

                #region (Datos Empresa)
                //creamos la imagen
                iTextSharp.text.Image Logo = null;
                if (File.Exists(HttpContext.Current.Server.MapPath(P_Ruta_Imagen)))
                {
                    Logo = iTextSharp.text.Image.GetInstance(HttpContext.Current.Server.MapPath(P_Ruta_Imagen));
                    Logo.ScalePercent(100);
                }
                iTextSharp.text.pdf.PdfPTable tbl_datos_emp = new iTextSharp.text.pdf.PdfPTable(2);
                tbl_datos_emp.SetWidthPercentage(new float[] { 635, 364 }, PageSize.A4);
                tbl_datos_emp.WidthPercentage = 98;
                tbl_datos_emp.DefaultCell.Border = iTextSharp.text.pdf.PdfPCell.NO_BORDER;
                tbl_datos_emp.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_TOP;
                tbl_datos_emp.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;

                domicilio = (string.IsNullOrEmpty(dt_fac.Calle_Emisor) ? "" : dt_fac.Calle_Emisor.Trim());
                domicilio += (string.IsNullOrEmpty(dt_fac.No_Exterior_Emisor) ? "" : " NO. " + dt_fac.No_Exterior_Emisor.Trim());
                domicilio += (string.IsNullOrEmpty(dt_fac.No_Interior_Emisor) ? "" : " INT. " + dt_fac.No_Interior_Emisor.Trim());
                domicilio += (string.IsNullOrEmpty(dt_fac.Codigo_Postal_Emisor) ? "" : ", C.P. " + dt_fac.Codigo_Postal_Emisor.Trim());

                iTextSharp.text.pdf.PdfPTable tbl_datos_emp1 = new iTextSharp.text.pdf.PdfPTable(1);
                tbl_datos_emp1.SetWidthPercentage(new float[] { 615 }, PageSize.A4);
                tbl_datos_emp1.WidthPercentage = 95;
                tbl_datos_emp1.DefaultCell.Border = iTextSharp.text.pdf.PdfPCell.NO_BORDER;
                tbl_datos_emp1.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                tbl_datos_emp1.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                tbl_datos_emp1.AddCell(new iTextSharp.text.Phrase(dt_fac.Razon_Social_Emisor.Trim(), Fuente_Etiquetas));
                tbl_datos_emp1.AddCell(new iTextSharp.text.Phrase("R.F.C.: " + dt_fac.RFC_Emisor.Trim(), Fuente_Etiquetas_));
                tbl_datos_emp1.AddCell(new iTextSharp.text.Phrase(domicilio, Fuente_Etiquetas_));
                if(!string.IsNullOrEmpty(dt_fac.Colonia_Emisor))
                tbl_datos_emp1.AddCell(new iTextSharp.text.Phrase(dt_fac.Colonia_Emisor.Trim(), Fuente_Etiquetas_));
                tbl_datos_emp1.AddCell(new iTextSharp.text.Phrase( (string.IsNullOrEmpty(dt_fac.Ciudad_Emisor)? "" : dt_fac.Ciudad_Emisor.Trim()+ ", ") 
                    + (string.IsNullOrEmpty(dt_fac.Estado_Emisor) ? "" : dt_fac.Estado_Emisor.Trim() + ", ")
                    + (string.IsNullOrEmpty(dt_fac.Pais_Emisor) ? "" : dt_fac.Pais_Emisor.Trim()), Fuente_Etiquetas_));
                tbl_datos_emp1.AddCell(new iTextSharp.text.Phrase(dt_fac.Regimen_Fiscal_Emisor.Trim(), Fuente_Etiquetas_));
                tbl_datos_emp.AddCell(tbl_datos_emp1);

                iTextSharp.text.pdf.PdfPTable tbl_datos_emp3 = new iTextSharp.text.pdf.PdfPTable(1);
                tbl_datos_emp3.SetWidthPercentage(new float[] { 300 }, PageSize.A4);
                tbl_datos_emp3.WidthPercentage = 95;
                tbl_datos_emp3.DefaultCell.Border = iTextSharp.text.pdf.PdfPCell.NO_BORDER;
                tbl_datos_emp3.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_TOP;
                tbl_datos_emp3.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                tbl_datos_emp3.AddCell(Logo);
                if (dt_fac.Tipo_Factura.ToLower() == "nota_credito")
                    tbl_datos_emp3.AddCell(new iTextSharp.text.Phrase("NOTA CRÉDITO", Fuente_Etiquetas_));
                else
                    tbl_datos_emp3.AddCell(new iTextSharp.text.Phrase("FACTURA", Fuente_Etiquetas_));
                tbl_datos_emp3.AddCell(new iTextSharp.text.Phrase(dt_fac.Serie.Trim() + " " + dt_fac.No_Factura.ToString().Trim(), Fuente_Etiquetas));
                //if (dt_fac.Rows[0][Ope_Facturas.Campo_Estatus].ToString().Trim() == "Cancelada")
                //{
                //    tbl_datos_emp3.AddCell(new iTextSharp.text.Phrase(dt_fac.Rows[0][Ope_Facturas.Campo_Estatus].ToString().Trim(), Fuente_Etiquetas));
                //}
                tbl_datos_emp.AddCell(tbl_datos_emp3);

                iTextSharp.text.pdf.PdfPTable tbl_datos_emp2 = new iTextSharp.text.pdf.PdfPTable(1);
                tbl_datos_emp2.SetWidthPercentage(new float[] { 999 }, PageSize.A4);
                tbl_datos_emp2.WidthPercentage = 98;
                tbl_datos_emp2.DefaultCell.Border = iTextSharp.text.pdf.PdfPCell.NO_BORDER;
                tbl_datos_emp2.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                tbl_datos_emp2.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                tbl_datos_emp2.AddCell(new iTextSharp.text.Phrase("ESTE DOCUMENTO ES UNA REPRESENTACIÓN IMPRESA DE UN CFDI", Fuente_Etiquetas2_));

                iTextSharp.text.pdf.PdfPTable tbl_datos_emp22 = new iTextSharp.text.pdf.PdfPTable(1);
                tbl_datos_emp22.SetWidthPercentage(new float[] { 999 }, PageSize.A4);
                tbl_datos_emp22.WidthPercentage = 98;
                tbl_datos_emp22.DefaultCell.Border = iTextSharp.text.pdf.PdfPCell.NO_BORDER;
                tbl_datos_emp22.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                tbl_datos_emp22.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                tbl_datos_emp22.AddCell(new iTextSharp.text.Phrase("USO CFDI: " + dt_fac.Uso_CFDI.ToString().Trim(), Fuente_Etiquetas2_));

                iTextSharp.text.pdf.PdfPTable tbl_datos_emp4 = new iTextSharp.text.pdf.PdfPTable(3);
                tbl_datos_emp4.SetWidthPercentage(new float[] { 333, 333, 333 }, PageSize.A4);
                tbl_datos_emp4.WidthPercentage = 98;
                tbl_datos_emp4.DefaultCell.Border = iTextSharp.text.pdf.PdfPCell.NO_BORDER;
                tbl_datos_emp4.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                tbl_datos_emp4.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                if (Tipo_Comprobante.Trim().ToUpper().Equals("E"))
                    tbl_datos_emp4.AddCell(new iTextSharp.text.Phrase("TIPO DE DOCUMENTO: EGRESO", Fuente_Etiquetas2_));
                else
                    tbl_datos_emp4.AddCell(new iTextSharp.text.Phrase("TIPO DE DOCUMENTO: INGRESO", Fuente_Etiquetas2_));
                tbl_datos_emp4.AddCell(new iTextSharp.text.Phrase("EFECTOS FISCALES AL PAGO", Fuente_Etiquetas2_));
                if (dt_fac.Timbre_Version.Trim() == "1.0")
                    tbl_datos_emp4.AddCell(new iTextSharp.text.Phrase("PAGO HECHO EN UNA SOLA EXHIBICIÓN", Fuente_Etiquetas2_));
                else
                    tbl_datos_emp4.AddCell(new iTextSharp.text.Phrase("Método de Pago: " + dt_fac.Metodo_Pago.ToString().Trim().ToUpper(), Fuente_Etiquetas2_));
                #endregion

                #region (Datos Fiscales)
                iTextSharp.text.pdf.PdfPTable tbl_datos_fiscales = new iTextSharp.text.pdf.PdfPTable(2);
                tbl_datos_fiscales.SetWidthPercentage(new float[] { 635, 364 }, PageSize.A4);
                tbl_datos_fiscales.WidthPercentage = 98;
                tbl_datos_fiscales.DefaultCell.BorderWidth = 0.5f;
                tbl_datos_fiscales.DefaultCell.BorderColor = new BaseColor(205, 203, 203);
                tbl_datos_fiscales.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                tbl_datos_fiscales.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;

                domicilio = (string.IsNullOrEmpty(dt_fac.Calle) ? "" : dt_fac.Calle.Trim());
                domicilio += (string.IsNullOrEmpty(dt_fac.Numero_Exterior) ? "" : " NO. " + dt_fac.Numero_Exterior.Trim());
                domicilio += (string.IsNullOrEmpty(dt_fac.Numero_Interior) ? "" : " INT. " + dt_fac.Numero_Interior.Trim());
                domicilio += (string.IsNullOrEmpty(dt_fac.Colonia) ? "" : ", COL. " + dt_fac.Colonia.Trim());
                domicilio += (string.IsNullOrEmpty(dt_fac.CP) ? "" : ", C.P. " + dt_fac.CP.Trim());

                iTextSharp.text.pdf.PdfPTable tbl_datos_fiscales1 = new iTextSharp.text.pdf.PdfPTable(2);
                tbl_datos_fiscales1.SetWidthPercentage(new float[] { 110, 515 }, PageSize.A4);
                tbl_datos_fiscales1.WidthPercentage = 98;
                tbl_datos_fiscales1.DefaultCell.Border = iTextSharp.text.pdf.PdfPCell.NO_BORDER;
                tbl_datos_fiscales1.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                tbl_datos_fiscales1.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                tbl_datos_fiscales1.AddCell(new iTextSharp.text.Phrase("Facturado a:", Fuente_Valor_Etq));
                tbl_datos_fiscales1.AddCell(new iTextSharp.text.Phrase(dt_fac.Razon_Social.Trim(), Fuente_Etiquetas1));
                if (!string.IsNullOrEmpty(domicilio))
                {
                    tbl_datos_fiscales1.AddCell(new iTextSharp.text.Phrase("Domicilio:", Fuente_Valor_Etq));
                    tbl_datos_fiscales1.AddCell(new iTextSharp.text.Phrase(domicilio, Fuente_Etiquetas1));
                    tbl_datos_fiscales1.AddCell(new iTextSharp.text.Phrase("Ciudad:", Fuente_Valor_Etq));
                    tbl_datos_fiscales1.AddCell(new iTextSharp.text.Phrase((string.IsNullOrEmpty(dt_fac.Localidad) ? "" : dt_fac.Localidad.Trim() + ", ")
                        + (string.IsNullOrEmpty(dt_fac.Estado) ? "" : dt_fac.Estado.Trim() + ", ")
                        + dt_fac.Pais.Trim(), Fuente_Etiquetas1));
                }
                tbl_datos_fiscales1.AddCell(new iTextSharp.text.Phrase("R.F.C.:", Fuente_Valor_Etq));
                tbl_datos_fiscales1.AddCell(new iTextSharp.text.Phrase(dt_fac.RFC.Trim(), Fuente_Etiquetas1));

                iTextSharp.text.pdf.PdfPTable tbl_datos_fiscales2 = new iTextSharp.text.pdf.PdfPTable(2);
                tbl_datos_fiscales2.SetWidthPercentage(new float[] { 150, 214 }, PageSize.A4);
                tbl_datos_fiscales2.WidthPercentage = 98;
                tbl_datos_fiscales2.DefaultCell.Border = iTextSharp.text.pdf.PdfPCell.NO_BORDER;
                tbl_datos_fiscales2.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                tbl_datos_fiscales2.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                tbl_datos_fiscales2.AddCell(new iTextSharp.text.Phrase("Fecha:", Fuente_Valor_Etq));
                tbl_datos_fiscales2.AddCell(new iTextSharp.text.Phrase(string.Format("{0:dd-MMM-yyyy HH:mm:ss}", dt_fac.Fecha_Emision), Fuente_Etiquetas1));
                tbl_datos_fiscales2.AddCell(new iTextSharp.text.Phrase("Expedida en:", Fuente_Valor_Etq));
                tbl_datos_fiscales2.AddCell(new iTextSharp.text.Phrase((string.IsNullOrEmpty(dt_fac.Ciudad) ? "" : dt_fac.Ciudad.Trim() + ", ")
                    + (string.IsNullOrEmpty(dt_fac.Estado) ? "" : dt_fac.Estado.Trim()), Fuente_Etiquetas1));

                if (dt_fac.Timbre_Version.Trim() == "1.0")
                {
                    tbl_datos_fiscales2.AddCell(new iTextSharp.text.Phrase("Metodo de pago:", Fuente_Valor_Etq));
                    tbl_datos_fiscales2.AddCell(new iTextSharp.text.Phrase(dt_fac.Metodo_Pago.Trim() + " "
                        + (string.IsNullOrEmpty(dt_fac.No_Cuenta_Pago.Trim()) ? "" : dt_fac.No_Cuenta_Pago.Trim()), Fuente_Etiquetas1));
                }
                else
                {
                    tbl_datos_fiscales2.AddCell(new iTextSharp.text.Phrase("Forma de pago:", Fuente_Valor_Etq));
                    tbl_datos_fiscales2.AddCell(new iTextSharp.text.Phrase(dt_fac.Forma_Pago.Trim() + " "
                        + (string.IsNullOrEmpty(dt_fac.No_Cuenta_Pago) ? "" : dt_fac.No_Cuenta_Pago.Trim()), Fuente_Etiquetas1));
                }

                tbl_datos_fiscales.AddCell(tbl_datos_fiscales1);
                tbl_datos_fiscales.AddCell(tbl_datos_fiscales2);
                #endregion

                #region (Datos Timbrado)
                iTextSharp.text.pdf.PdfPTable tbl_datos_timbrado = new iTextSharp.text.pdf.PdfPTable(4);
                tbl_datos_timbrado.SetWidthPercentage(new float[] { 215, 215, 205, 364 }, PageSize.A4);
                tbl_datos_timbrado.WidthPercentage = 98;
                tbl_datos_timbrado.DefaultCell.BorderWidth = 0.5f;
                tbl_datos_timbrado.DefaultCell.BorderColor = new BaseColor(205, 203, 203);
                tbl_datos_timbrado.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                tbl_datos_timbrado.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;

                iTextSharp.text.pdf.PdfPTable tbl_datos_timbrado1 = new iTextSharp.text.pdf.PdfPTable(1);
                tbl_datos_timbrado1.SetWidthPercentage(new float[] { 210 }, PageSize.A4);
                tbl_datos_timbrado1.WidthPercentage = 98;
                tbl_datos_timbrado1.DefaultCell.Border = iTextSharp.text.pdf.PdfPCell.NO_BORDER;
                tbl_datos_timbrado1.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                tbl_datos_timbrado1.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                tbl_datos_timbrado1.AddCell(new iTextSharp.text.Phrase("No. Certificado", Fuente_Valor_Etq));
                tbl_datos_timbrado1.AddCell(new iTextSharp.text.Phrase(dt_fac.No_Certificado.Trim(), Fuente_Etiquetas1));

                iTextSharp.text.pdf.PdfPTable tbl_datos_timbrado2 = new iTextSharp.text.pdf.PdfPTable(1);
                tbl_datos_timbrado2.SetWidthPercentage(new float[] { 210 }, PageSize.A4);
                tbl_datos_timbrado2.WidthPercentage = 98;
                tbl_datos_timbrado2.DefaultCell.Border = iTextSharp.text.pdf.PdfPCell.NO_BORDER;
                tbl_datos_timbrado2.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                tbl_datos_timbrado2.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                tbl_datos_timbrado2.AddCell(new iTextSharp.text.Phrase("No. Certificado SAT", Fuente_Valor_Etq));
                tbl_datos_timbrado2.AddCell(new iTextSharp.text.Phrase(dt_fac.Timbre_No_Certificado_SAT.Trim(), Fuente_Etiquetas1));

                iTextSharp.text.pdf.PdfPTable tbl_datos_timbrado3 = new iTextSharp.text.pdf.PdfPTable(1);
                tbl_datos_timbrado3.SetWidthPercentage(new float[] { 200 }, PageSize.A4);
                tbl_datos_timbrado3.WidthPercentage = 98;
                tbl_datos_timbrado3.DefaultCell.Border = iTextSharp.text.pdf.PdfPCell.NO_BORDER;
                tbl_datos_timbrado3.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                tbl_datos_timbrado3.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                tbl_datos_timbrado3.AddCell(new iTextSharp.text.Phrase("Fecha Certificación", Fuente_Valor_Etq));
                tbl_datos_timbrado3.AddCell(new iTextSharp.text.Phrase(string.Format("{0: dd-MMM-yyyy HH:mm:ss}", dt_fac.Timbre_Fecha_Timbrado), Fuente_Etiquetas1));

                iTextSharp.text.pdf.PdfPTable tbl_datos_timbrado4 = new iTextSharp.text.pdf.PdfPTable(1);
                tbl_datos_timbrado4.SetWidthPercentage(new float[] { 357 }, PageSize.A4);
                tbl_datos_timbrado4.WidthPercentage = 98;
                tbl_datos_timbrado4.DefaultCell.Border = iTextSharp.text.pdf.PdfPCell.NO_BORDER;
                tbl_datos_timbrado4.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                tbl_datos_timbrado4.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                tbl_datos_timbrado4.AddCell(new iTextSharp.text.Phrase("Folio Fiscal", Fuente_Valor_Etq));
                tbl_datos_timbrado4.AddCell(new iTextSharp.text.Phrase(dt_fac.Timbre_UUID.Trim(), Fuente_Etiquetas1));

                tbl_datos_timbrado.AddCell(tbl_datos_timbrado1);
                tbl_datos_timbrado.AddCell(tbl_datos_timbrado2);
                tbl_datos_timbrado.AddCell(tbl_datos_timbrado3);
                tbl_datos_timbrado.AddCell(tbl_datos_timbrado4);
                #endregion

                #region (Detalles)
                iTextSharp.text.pdf.PdfPTable tbl_det_header = new iTextSharp.text.pdf.PdfPTable(8);
                tbl_det_header.SetWidthPercentage(new float[] { 110, 110, 110, 120, 435, 105, 180, 170 }, PageSize.A4);
                tbl_det_header.WidthPercentage = 98;
                tbl_det_header.DefaultCell.BorderWidth = 0.5f;
                tbl_det_header.DefaultCell.BorderColor = new BaseColor(205, 203, 203);
                tbl_det_header.DefaultCell.BackgroundColor = new BaseColor(246, 245, 245);
                tbl_det_header.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                tbl_det_header.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                tbl_det_header.AddCell(new iTextSharp.text.Phrase("Cantidad", Fuente_Valor_Etq));
                tbl_det_header.AddCell(new iTextSharp.text.Phrase("Clave Unidad", Fuente_Valor_Etq));
                tbl_det_header.AddCell(new iTextSharp.text.Phrase("Unidad", Fuente_Valor_Etq));
                tbl_det_header.AddCell(new iTextSharp.text.Phrase("Clave Concepto", Fuente_Valor_Etq));
                tbl_det_header.AddCell(new iTextSharp.text.Phrase("Descripción", Fuente_Valor_Etq));
                tbl_det_header.AddCell(new iTextSharp.text.Phrase("Código", Fuente_Valor_Etq));
                tbl_det_header.AddCell(new iTextSharp.text.Phrase("P. Unitario", Fuente_Valor_Etq));
                tbl_det_header.AddCell(new iTextSharp.text.Phrase("Importe", Fuente_Valor_Etq));

                iTextSharp.text.pdf.PdfPTable tbl_det_header2 = new iTextSharp.text.pdf.PdfPTable(8);
                tbl_det_header2.SetWidthPercentage(new float[] { 110, 110, 110, 120, 435, 105, 180, 170 }, PageSize.A4);
                tbl_det_header2.WidthPercentage = 98;
                tbl_det_header2.DefaultCell.BorderWidth = 0.5f;
                tbl_det_header2.DefaultCell.BorderColor = new BaseColor(205, 203, 203);
                tbl_det_header2.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                tbl_det_header2.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;

                iTextSharp.text.Phrase _frase = null;
                iTextSharp.text.pdf.PdfPCell _celda = null;

                dt_fac_det = Obtener_Detalles_Xml(dt_fac.Timbre_XML.ToString());
                if (dt_fac_det != null)
                {
                    if (dt_fac_det.Rows.Count > 0)
                    {
                        foreach (DataRow dr_det in dt_fac_det.Rows)
                        {
                            tbl_det_header2.AddCell(new iTextSharp.text.Phrase(String.Format("{0:n}", dr_det["Cantidad"]), Fuente_Etiquetas1));
                            tbl_det_header2.AddCell(new iTextSharp.text.Phrase(dr_det["ClaveUnidad"].ToString().Trim(), Fuente_Etiquetas1));
                            tbl_det_header2.AddCell(new iTextSharp.text.Phrase(dr_det["Unidad"].ToString().Trim(), Fuente_Etiquetas1));
                            tbl_det_header2.AddCell(new iTextSharp.text.Phrase(dr_det["ClaveProdServ"].ToString().Trim(), Fuente_Etiquetas1));
                            tbl_det_header2.AddCell(new iTextSharp.text.Phrase(dr_det["Nombre"].ToString().Trim(), Fuente_Etiquetas1));
                            tbl_det_header2.AddCell(new iTextSharp.text.Phrase(dr_det["NoIdentificacion"].ToString().Trim(), Fuente_Etiquetas1));

                            _frase = new iTextSharp.text.Phrase(String.Format("{0:n}", dr_det["ValorUnitario"]), Fuente_Etiquetas1);
                            _celda = new iTextSharp.text.pdf.PdfPCell(_frase);
                            _celda.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
                            _celda.BorderWidth = 0.5f;
                            _celda.BorderColor = new BaseColor(205, 203, 203);
                            tbl_det_header2.AddCell(_celda);

                            _frase = new iTextSharp.text.Phrase(String.Format("{0:n}", dr_det["Subtotal"]), Fuente_Etiquetas1);
                            _celda = new iTextSharp.text.pdf.PdfPCell(_frase);
                            _celda.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
                            _celda.BorderWidth = 0.5f;
                            _celda.BorderColor = new BaseColor(205, 203, 203);
                            tbl_det_header2.AddCell(_celda);
                        }
                    }
                }

                #endregion

                #region (Datos Total)
                iTextSharp.text.pdf.PdfPTable tbl_datos_total = new iTextSharp.text.pdf.PdfPTable(2);
                tbl_datos_total.SetWidthPercentage(new float[] { 635, 364 }, PageSize.A4);
                tbl_datos_total.WidthPercentage = 98;
                tbl_datos_total.DefaultCell.BorderWidth = 0.5f;
                tbl_datos_total.DefaultCell.BorderColor = new BaseColor(205, 203, 203);
                tbl_datos_total.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_TOP;
                tbl_datos_total.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;

                iTextSharp.text.pdf.PdfPTable tbl_datos_total1 = new iTextSharp.text.pdf.PdfPTable(1);
                tbl_datos_total1.SetWidthPercentage(new float[] { 623 }, PageSize.A4);
                tbl_datos_total1.WidthPercentage = 98;
                tbl_datos_total1.DefaultCell.Border = iTextSharp.text.pdf.PdfPCell.NO_BORDER;
                tbl_datos_total1.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_TOP;
                tbl_datos_total1.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                tbl_datos_total1.AddCell(new iTextSharp.text.Phrase("Importe total con letra", Fuente_Valor_Etq));
                tbl_datos_total1.AddCell(new iTextSharp.text.Phrase(Convertir_Cantidad_Letras(Convert.ToDecimal(dt_fac.Total), dt_fac.Tipo_Moneda), Fuente_Etiquetas1));
                tbl_datos_total1.AddCell(new iTextSharp.text.Paragraph("\n"));

                iTextSharp.text.pdf.PdfPTable tbl_datos_total2 = new iTextSharp.text.pdf.PdfPTable(2);
                tbl_datos_total2.SetWidthPercentage(new float[] { 175, 181 }, PageSize.A4);
                tbl_datos_total2.WidthPercentage = 98;
                tbl_datos_total2.DefaultCell.Border = iTextSharp.text.pdf.PdfPCell.NO_BORDER;
                tbl_datos_total2.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                tbl_datos_total2.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
                tbl_datos_total2.AddCell(new iTextSharp.text.Phrase("Subtotal", Fuente_Valor_Etq));
                tbl_datos_total2.AddCell(new iTextSharp.text.Phrase(string.Format("{0:n}", dt_fac.Subtotal), Fuente_Etiquetas1));
                if (dt_fac.Descuento > 0)
                {
                tbl_datos_total2.AddCell(new iTextSharp.text.Phrase("Descuento", Fuente_Valor_Etq));
                tbl_datos_total2.AddCell(new iTextSharp.text.Phrase(string.Format("{0:n}", dt_fac.Descuento), Fuente_Etiquetas1));
                }
                tbl_datos_total2.AddCell(new iTextSharp.text.Phrase("Impuesto", Fuente_Valor_Etq));
                tbl_datos_total2.AddCell(new iTextSharp.text.Phrase(string.Format("{0:n}", dt_fac.Iva), Fuente_Etiquetas1));
                tbl_datos_total2.AddCell(new iTextSharp.text.Paragraph("\n"));
                tbl_datos_total2.AddCell(new iTextSharp.text.Paragraph("\n"));
                tbl_datos_total2.AddCell(new iTextSharp.text.Phrase("Total", Fuente_Valor_Etq));
                tbl_datos_total2.AddCell(new iTextSharp.text.Phrase(string.Format("{0:n}", dt_fac.Total), Fuente_Etiquetas1));

                tbl_datos_total.AddCell(tbl_datos_total1);
                tbl_datos_total.AddCell(tbl_datos_total2);
                #endregion

                #region (Datos CFDI)
                //generamos el codigo qr
                String Ruta_Carpeta = P_Ruta_Carpeta_Qr;
                String Ruta_qr = dt_fac.Ruta_Codigo_BD;

                if (!Directory.Exists(HttpContext.Current.Server.MapPath(Ruta_Carpeta)))
                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath(Ruta_Carpeta));

                if (!File.Exists(HttpContext.Current.Server.MapPath(Ruta_qr)))
                {
                    Generar_Codigo_Qr(dt_fac.RFC_Emisor.Trim(), dt_fac.RFC.Trim(),
                    Convert.ToDecimal(dt_fac.Total.ToString().Trim()), dt_fac.Timbre_UUID.ToString().Trim(), HttpContext.Current.Server.MapPath(Ruta_Carpeta), HttpContext.Current.Server.MapPath(Ruta_qr));
                }

                iTextSharp.text.Image qr = iTextSharp.text.Image.GetInstance(HttpContext.Current.Server.MapPath(Ruta_qr));
                qr.ScalePercent(90);

                iTextSharp.text.pdf.PdfPTable tbl_datos_cfdi = new iTextSharp.text.pdf.PdfPTable(2);
                tbl_datos_cfdi.SetWidthPercentage(new float[] { 200, 799 }, PageSize.A4);
                tbl_datos_cfdi.WidthPercentage = 98;
                tbl_datos_cfdi.DefaultCell.Border = iTextSharp.text.pdf.PdfPCell.NO_BORDER;
                tbl_datos_cfdi.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                tbl_datos_cfdi.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                tbl_datos_cfdi.AddCell(qr);

                iTextSharp.text.pdf.PdfPTable tbl_datos_cfdi1 = new iTextSharp.text.pdf.PdfPTable(1);
                tbl_datos_cfdi1.SetWidthPercentage(new float[] { 630 }, PageSize.A4);
                tbl_datos_cfdi1.WidthPercentage = 95;
                tbl_datos_cfdi1.DefaultCell.Border = iTextSharp.text.pdf.PdfPCell.NO_BORDER;
                tbl_datos_cfdi1.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                tbl_datos_cfdi1.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                tbl_datos_cfdi1.AddCell(new iTextSharp.text.Phrase("Versión CFDI", Fuente_Valor_Etq));
                tbl_datos_cfdi1.AddCell(new iTextSharp.text.Phrase("3.3", Fuente_Etiquetas2));
                tbl_datos_cfdi1.AddCell(new iTextSharp.text.Phrase("Cadena original de complemento de Certificado Digital SAT", Fuente_Valor_Etq));
                tbl_datos_cfdi1.AddCell(new iTextSharp.text.Phrase("||" + dt_fac.Timbre_Version.ToString().Trim() //version
                    + "|" + dt_fac.Timbre_UUID.ToString().Trim() //UUID
                    + "|" + string.Format("{0:yyyy-MM-dd}", dt_fac.Timbre_Fecha_Timbrado)
                    + "T" + string.Format("{0:HH:mm:ss}", dt_fac.Timbre_Fecha_Timbrado) //fecha timbrado
                    + "|" + dt_fac.RFC_Emisor.ToString().Trim()  // RfcProvCertif
                                                                                             //leyenda
                    + "|" + dt_fac.Timbre_Sello_CFD.Trim()  //SelloCFD
                    + "|" + dt_fac.Timbre_No_Certificado_SAT.Trim() + "||", Fuente_Etiquetas2)); //NoCertificadoSAT
                tbl_datos_cfdi1.AddCell(new iTextSharp.text.Paragraph("\n"));
                tbl_datos_cfdi1.AddCell(new iTextSharp.text.Phrase("Sello Digital del Emisor", Fuente_Valor_Etq));
                tbl_datos_cfdi1.AddCell(new iTextSharp.text.Phrase(dt_fac.Timbre_Sello_CFD.Trim(), Fuente_Etiquetas2));
                tbl_datos_cfdi1.AddCell(new iTextSharp.text.Paragraph("\n"));
                tbl_datos_cfdi1.AddCell(new iTextSharp.text.Phrase("Sello Digital del SAT", Fuente_Valor_Etq));
                tbl_datos_cfdi1.AddCell(new iTextSharp.text.Phrase(dt_fac.Timbre_Sello_SAT.Trim(), Fuente_Etiquetas2));
                tbl_datos_cfdi.AddCell(tbl_datos_cfdi1);
                #endregion

                // Se agrega el PDFTable al documento.
                Documento.Add(new iTextSharp.text.Paragraph("\n"));
                Documento.Add(tbl_datos_emp);

                Documento.Add(new iTextSharp.text.Paragraph("\n"));
                Documento.Add(tbl_datos_fiscales);
                Documento.Add(tbl_datos_timbrado);
                #region (Datos Timbrado Relacionado)
                if (!string.IsNullOrEmpty(dt_fac.UUID_Relacionado))
                {
                    iTextSharp.text.pdf.PdfPTable tbl_datos_timbradoR = new iTextSharp.text.pdf.PdfPTable(3);
                    tbl_datos_timbradoR.SetWidthPercentage(new float[] { 430, 205, 364 }, PageSize.A4);
                    tbl_datos_timbradoR.WidthPercentage = 98;
                    tbl_datos_timbradoR.DefaultCell.BorderWidth = 0.5f;
                    tbl_datos_timbradoR.DefaultCell.BorderColor = new BaseColor(205, 203, 203);
                    tbl_datos_timbradoR.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                    tbl_datos_timbradoR.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;

                    iTextSharp.text.pdf.PdfPTable tbl_datos_timbradoR2 = new iTextSharp.text.pdf.PdfPTable(1);
                    tbl_datos_timbradoR2.SetWidthPercentage(new float[] { 425 }, PageSize.A4);
                    tbl_datos_timbradoR2.WidthPercentage = 98;
                    tbl_datos_timbradoR2.DefaultCell.Border = iTextSharp.text.pdf.PdfPCell.NO_BORDER;
                    tbl_datos_timbradoR2.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                    tbl_datos_timbradoR2.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                    tbl_datos_timbradoR2.AddCell(new iTextSharp.text.Phrase("Tipo Relación", Fuente_Valor_Etq));
                    tbl_datos_timbradoR2.AddCell(new iTextSharp.text.Phrase(dt_fac.Tipo_Relacion.Trim(), Fuente_Etiquetas1));

                    iTextSharp.text.pdf.PdfPTable tbl_datos_timbradoR1 = new iTextSharp.text.pdf.PdfPTable(1);
                    tbl_datos_timbradoR1.SetWidthPercentage(new float[] { 200 }, PageSize.A4);
                    tbl_datos_timbradoR1.WidthPercentage = 98;
                    tbl_datos_timbradoR1.DefaultCell.Border = iTextSharp.text.pdf.PdfPCell.NO_BORDER;
                    tbl_datos_timbradoR1.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                    tbl_datos_timbradoR1.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                    tbl_datos_timbradoR1.AddCell(new iTextSharp.text.Phrase("Factura Relacionada", Fuente_Valor_Etq));
                    tbl_datos_timbradoR1.AddCell(new iTextSharp.text.Phrase(dt_fac.No_Factura_Relacionada.Trim() + " " + dt_fac.Serie_Relacionada.ToString().Trim(), Fuente_Etiquetas1));

                    iTextSharp.text.pdf.PdfPTable tbl_datos_timbradoR3 = new iTextSharp.text.pdf.PdfPTable(1);
                    tbl_datos_timbradoR3.SetWidthPercentage(new float[] { 357 }, PageSize.A4);
                    tbl_datos_timbradoR3.WidthPercentage = 98;
                    tbl_datos_timbradoR3.DefaultCell.Border = iTextSharp.text.pdf.PdfPCell.NO_BORDER;
                    tbl_datos_timbradoR3.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                    tbl_datos_timbradoR3.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                    tbl_datos_timbradoR3.AddCell(new iTextSharp.text.Phrase("Folio fiscal relacionado", Fuente_Valor_Etq));
                    tbl_datos_timbradoR3.AddCell(new iTextSharp.text.Phrase(dt_fac.UUID_Relacionado.Trim(), Fuente_Etiquetas1));

                    tbl_datos_timbradoR.AddCell(tbl_datos_timbradoR2);
                    tbl_datos_timbradoR.AddCell(tbl_datos_timbradoR1);
                    tbl_datos_timbradoR.AddCell(tbl_datos_timbradoR3);
                    Documento.Add(tbl_datos_timbradoR);
                }
                #endregion
                Documento.Add(new iTextSharp.text.Paragraph("\n"));
                Documento.Add(tbl_det_header);
                Documento.Add(tbl_det_header2);
                Documento.Add(new iTextSharp.text.Paragraph("\n"));
                Documento.Add(tbl_datos_total);
                Documento.Add(new iTextSharp.text.Paragraph("\n"));
                Documento.Add(tbl_datos_emp2);
                Documento.Add(tbl_datos_emp22);
                Documento.Add(tbl_datos_emp4);
                Documento.Add(new iTextSharp.text.Paragraph("\n"));
                Documento.Add(new iTextSharp.text.Paragraph("\n"));
                Documento.Add(tbl_datos_cfdi);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //pdf nota de credito
        public static void Exportar_Datos_PDF_NC(iTextSharp.text.Document Documento, Cls_Ope_Fac_Notas_Credito_Negocio dt_fac, PdfWriter writer, String P_Ruta_Carpeta_Qr, String P_Ruta_Imagen, String Tipo_Comprobante)
        {
            string domicilio = "";
            DataTable dt_fac_det = new DataTable();
            try
            {
                iTextSharp.text.FontFactory.RegisterDirectory(@"C:\Windows\Fonts");
                //Creamos el objeto de tipo tabla para almacenar el resultado de la búsqueda. 
                iTextSharp.text.pdf.PdfPTable Rpt_Tabla = new iTextSharp.text.pdf.PdfPTable(8);
                //Obtenemos y establecemos el formato de las columnas de la tabla.
                float[] Ancho_Cabeceras = new[] { 100f, 200f, 100, 140f, 120f, 120f, 120f, 120f };
                //Creamos y definimos algunas propiedades que tendrá la fuente que se aplicara a las celdas de la tabla de resultados.
                iTextSharp.text.Font Fuente_Tabla_Contenido = iTextSharp.text.FontFactory.GetFont("Century Gothic", 7, iTextSharp.text.Font.NORMAL, BaseColor.GRAY);
                iTextSharp.text.Font Fuente_Etiquetas = iTextSharp.text.FontFactory.GetFont("Century Gothic", 10, iTextSharp.text.Font.BOLD, new BaseColor(7, 87, 127));
                iTextSharp.text.Font Fuente_Etiquetas_ = iTextSharp.text.FontFactory.GetFont("Century Gothic", 9, iTextSharp.text.Font.BOLD, new BaseColor(51, 51, 51));
                iTextSharp.text.Font Fuente_Etiquetas1 = iTextSharp.text.FontFactory.GetFont("Century Gothic", 9, iTextSharp.text.Font.NORMAL, new BaseColor(51, 51, 51));
                iTextSharp.text.Font Fuente_Valor_Etq = iTextSharp.text.FontFactory.GetFont("Century Gothic", 9, iTextSharp.text.Font.NORMAL, new BaseColor(5, 34, 73));
                iTextSharp.text.Font Fuente_Etiquetas2 = iTextSharp.text.FontFactory.GetFont("Century Gothic", 8, iTextSharp.text.Font.NORMAL, new BaseColor(51, 51, 51));
                iTextSharp.text.Font Fuente_Etiquetas2_ = iTextSharp.text.FontFactory.GetFont("Century Gothic", 7, iTextSharp.text.Font.NORMAL, new BaseColor(51, 51, 51));

                //Establecemos el formato que tendrá la tabla que mostrara el resultado de la búsqueda según el movimiento consultado.
                Rpt_Tabla.DefaultCell.Padding = 3;
                Rpt_Tabla.SetWidths(Ancho_Cabeceras);
                Rpt_Tabla.TotalWidth = 1020;
                Rpt_Tabla.DefaultCell.BorderWidth = 2;
                Rpt_Tabla.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                Rpt_Tabla.HeaderRows = 1;

                #region (Datos Empresa)
                //creamos la imagen
                iTextSharp.text.Image Logo = null;
                if (File.Exists(HttpContext.Current.Server.MapPath(P_Ruta_Imagen)))
                {
                    Logo = iTextSharp.text.Image.GetInstance(HttpContext.Current.Server.MapPath(P_Ruta_Imagen));
                    Logo.ScalePercent(100);
                }
                iTextSharp.text.pdf.PdfPTable tbl_datos_emp = new iTextSharp.text.pdf.PdfPTable(2);
                tbl_datos_emp.SetWidthPercentage(new float[] { 635, 364 }, PageSize.A4);
                tbl_datos_emp.WidthPercentage = 98;
                tbl_datos_emp.DefaultCell.Border = iTextSharp.text.pdf.PdfPCell.NO_BORDER;
                tbl_datos_emp.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_TOP;
                tbl_datos_emp.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;

                domicilio = (string.IsNullOrEmpty(dt_fac.Calle_Emisor) ? "" : dt_fac.Calle_Emisor.Trim());
                domicilio += (string.IsNullOrEmpty(dt_fac.No_Exterior_Emisor) ? "" : " NO. " + dt_fac.No_Exterior_Emisor.Trim());
                domicilio += (string.IsNullOrEmpty(dt_fac.No_Interior_Emisor) ? "" : " INT. " + dt_fac.No_Interior_Emisor.Trim());
                domicilio += (string.IsNullOrEmpty(dt_fac.Codigo_Postal_Emisor) ? "" : ", C.P. " + dt_fac.Codigo_Postal_Emisor.Trim());

                iTextSharp.text.pdf.PdfPTable tbl_datos_emp1 = new iTextSharp.text.pdf.PdfPTable(1);
                tbl_datos_emp1.SetWidthPercentage(new float[] { 615 }, PageSize.A4);
                tbl_datos_emp1.WidthPercentage = 95;
                tbl_datos_emp1.DefaultCell.Border = iTextSharp.text.pdf.PdfPCell.NO_BORDER;
                tbl_datos_emp1.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                tbl_datos_emp1.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                tbl_datos_emp1.AddCell(new iTextSharp.text.Phrase(dt_fac.Razon_Social_Emisor.Trim(), Fuente_Etiquetas));
                tbl_datos_emp1.AddCell(new iTextSharp.text.Phrase("R.F.C.: " + dt_fac.RFC_Emisor.Trim(), Fuente_Etiquetas_));
                tbl_datos_emp1.AddCell(new iTextSharp.text.Phrase(domicilio, Fuente_Etiquetas_));
                if (!string.IsNullOrEmpty(dt_fac.Colonia_Emisor))
                    tbl_datos_emp1.AddCell(new iTextSharp.text.Phrase(dt_fac.Colonia_Emisor.Trim(), Fuente_Etiquetas_));
                tbl_datos_emp1.AddCell(new iTextSharp.text.Phrase((string.IsNullOrEmpty(dt_fac.Ciudad_Emisor) ? "" : dt_fac.Ciudad_Emisor.Trim() + ", ")
                    + (string.IsNullOrEmpty(dt_fac.Estado_Emisor) ? "" : dt_fac.Estado_Emisor.Trim() + ", ")
                    + (string.IsNullOrEmpty(dt_fac.Pais_Emisor) ? "" : dt_fac.Pais_Emisor.Trim()), Fuente_Etiquetas_));
                tbl_datos_emp1.AddCell(new iTextSharp.text.Phrase(dt_fac.Regimen_Fiscal_Emisor.Trim(), Fuente_Etiquetas_));
                tbl_datos_emp.AddCell(tbl_datos_emp1);

                iTextSharp.text.pdf.PdfPTable tbl_datos_emp3 = new iTextSharp.text.pdf.PdfPTable(1);
                tbl_datos_emp3.SetWidthPercentage(new float[] { 300 }, PageSize.A4);
                tbl_datos_emp3.WidthPercentage = 95;
                tbl_datos_emp3.DefaultCell.Border = iTextSharp.text.pdf.PdfPCell.NO_BORDER;
                tbl_datos_emp3.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_TOP;
                tbl_datos_emp3.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                tbl_datos_emp3.AddCell(Logo);
                if (dt_fac.Tipo_Factura.ToLower() == "nota_credito")
                    tbl_datos_emp3.AddCell(new iTextSharp.text.Phrase("NOTA CRÉDITO", Fuente_Etiquetas_));
                else
                    tbl_datos_emp3.AddCell(new iTextSharp.text.Phrase("FACTURA", Fuente_Etiquetas_));
                tbl_datos_emp3.AddCell(new iTextSharp.text.Phrase(dt_fac.Serie.Trim() + " " + dt_fac.No_Nota_Credito.ToString().Trim(), Fuente_Etiquetas));
                //if (dt_fac.Rows[0][Ope_Facturas.Campo_Estatus].ToString().Trim() == "Cancelada")
                //{
                //    tbl_datos_emp3.AddCell(new iTextSharp.text.Phrase(dt_fac.Rows[0][Ope_Facturas.Campo_Estatus].ToString().Trim(), Fuente_Etiquetas));
                //}
                tbl_datos_emp.AddCell(tbl_datos_emp3);

                iTextSharp.text.pdf.PdfPTable tbl_datos_emp2 = new iTextSharp.text.pdf.PdfPTable(1);
                tbl_datos_emp2.SetWidthPercentage(new float[] { 999 }, PageSize.A4);
                tbl_datos_emp2.WidthPercentage = 98;
                tbl_datos_emp2.DefaultCell.Border = iTextSharp.text.pdf.PdfPCell.NO_BORDER;
                tbl_datos_emp2.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                tbl_datos_emp2.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                tbl_datos_emp2.AddCell(new iTextSharp.text.Phrase("ESTE DOCUMENTO ES UNA REPRESENTACIÓN IMPRESA DE UN CFDI", Fuente_Etiquetas2_));

                iTextSharp.text.pdf.PdfPTable tbl_datos_emp22 = new iTextSharp.text.pdf.PdfPTable(1);
                tbl_datos_emp22.SetWidthPercentage(new float[] { 999 }, PageSize.A4);
                tbl_datos_emp22.WidthPercentage = 98;
                tbl_datos_emp22.DefaultCell.Border = iTextSharp.text.pdf.PdfPCell.NO_BORDER;
                tbl_datos_emp22.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                tbl_datos_emp22.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                tbl_datos_emp22.AddCell(new iTextSharp.text.Phrase("USO CFDI: " + dt_fac.Uso_CFDI.ToString().Trim(), Fuente_Etiquetas2_));

                iTextSharp.text.pdf.PdfPTable tbl_datos_emp4 = new iTextSharp.text.pdf.PdfPTable(3);
                tbl_datos_emp4.SetWidthPercentage(new float[] { 333, 333, 333 }, PageSize.A4);
                tbl_datos_emp4.WidthPercentage = 98;
                tbl_datos_emp4.DefaultCell.Border = iTextSharp.text.pdf.PdfPCell.NO_BORDER;
                tbl_datos_emp4.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                tbl_datos_emp4.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                if (Tipo_Comprobante.Trim().ToUpper().Equals("E"))
                    tbl_datos_emp4.AddCell(new iTextSharp.text.Phrase("TIPO DE DOCUMENTO: EGRESO", Fuente_Etiquetas2_));
                else
                    tbl_datos_emp4.AddCell(new iTextSharp.text.Phrase("TIPO DE DOCUMENTO: INGRESO", Fuente_Etiquetas2_));
                tbl_datos_emp4.AddCell(new iTextSharp.text.Phrase("EFECTOS FISCALES AL PAGO", Fuente_Etiquetas2_));
                if (dt_fac.Timbre_Version.Trim() == "1.0")
                    tbl_datos_emp4.AddCell(new iTextSharp.text.Phrase("PAGO HECHO EN UNA SOLA EXHIBICIÓN", Fuente_Etiquetas2_));
                else
                    tbl_datos_emp4.AddCell(new iTextSharp.text.Phrase("Método de Pago: " + dt_fac.Metodo_Pago.ToString().Trim().ToUpper(), Fuente_Etiquetas2_));
                #endregion

                #region (Datos Fiscales)
                iTextSharp.text.pdf.PdfPTable tbl_datos_fiscales = new iTextSharp.text.pdf.PdfPTable(2);
                tbl_datos_fiscales.SetWidthPercentage(new float[] { 635, 364 }, PageSize.A4);
                tbl_datos_fiscales.WidthPercentage = 98;
                tbl_datos_fiscales.DefaultCell.BorderWidth = 0.5f;
                tbl_datos_fiscales.DefaultCell.BorderColor = new BaseColor(205, 203, 203);
                tbl_datos_fiscales.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                tbl_datos_fiscales.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;

                domicilio = (string.IsNullOrEmpty(dt_fac.Calle) ? "" : dt_fac.Calle.Trim());
                domicilio += (string.IsNullOrEmpty(dt_fac.Numero_Exterior) ? "" : " NO. " + dt_fac.Numero_Exterior.Trim());
                domicilio += (string.IsNullOrEmpty(dt_fac.Numero_Interior) ? "" : " INT. " + dt_fac.Numero_Interior.Trim());
                domicilio += (string.IsNullOrEmpty(dt_fac.Colonia) ? "" : ", COL. " + dt_fac.Colonia.Trim());
                domicilio += (string.IsNullOrEmpty(dt_fac.CP) ? "" : ", C.P. " + dt_fac.CP.Trim());

                iTextSharp.text.pdf.PdfPTable tbl_datos_fiscales1 = new iTextSharp.text.pdf.PdfPTable(2);
                tbl_datos_fiscales1.SetWidthPercentage(new float[] { 110, 515 }, PageSize.A4);
                tbl_datos_fiscales1.WidthPercentage = 98;
                tbl_datos_fiscales1.DefaultCell.Border = iTextSharp.text.pdf.PdfPCell.NO_BORDER;
                tbl_datos_fiscales1.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                tbl_datos_fiscales1.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                tbl_datos_fiscales1.AddCell(new iTextSharp.text.Phrase("Facturado a:", Fuente_Valor_Etq));
                tbl_datos_fiscales1.AddCell(new iTextSharp.text.Phrase(dt_fac.Razon_Social.Trim(), Fuente_Etiquetas1));
                if (!string.IsNullOrEmpty(domicilio))
                {
                    tbl_datos_fiscales1.AddCell(new iTextSharp.text.Phrase("Domicilio:", Fuente_Valor_Etq));
                    tbl_datos_fiscales1.AddCell(new iTextSharp.text.Phrase(domicilio, Fuente_Etiquetas1));
                    tbl_datos_fiscales1.AddCell(new iTextSharp.text.Phrase("Ciudad:", Fuente_Valor_Etq));
                    tbl_datos_fiscales1.AddCell(new iTextSharp.text.Phrase((string.IsNullOrEmpty(dt_fac.Localidad) ? "" : dt_fac.Localidad.Trim() + ", ")
                        + (string.IsNullOrEmpty(dt_fac.Estado) ? "" : dt_fac.Estado.Trim() + ", ")
                        + dt_fac.Pais.Trim(), Fuente_Etiquetas1));
                }
                tbl_datos_fiscales1.AddCell(new iTextSharp.text.Phrase("R.F.C.:", Fuente_Valor_Etq));
                tbl_datos_fiscales1.AddCell(new iTextSharp.text.Phrase(dt_fac.RFC.Trim(), Fuente_Etiquetas1));

                iTextSharp.text.pdf.PdfPTable tbl_datos_fiscales2 = new iTextSharp.text.pdf.PdfPTable(2);
                tbl_datos_fiscales2.SetWidthPercentage(new float[] { 150, 214 }, PageSize.A4);
                tbl_datos_fiscales2.WidthPercentage = 98;
                tbl_datos_fiscales2.DefaultCell.Border = iTextSharp.text.pdf.PdfPCell.NO_BORDER;
                tbl_datos_fiscales2.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                tbl_datos_fiscales2.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                tbl_datos_fiscales2.AddCell(new iTextSharp.text.Phrase("Fecha:", Fuente_Valor_Etq));
                tbl_datos_fiscales2.AddCell(new iTextSharp.text.Phrase(string.Format("{0:dd-MMM-yyyy HH:mm:ss}", dt_fac.Fecha_Emision), Fuente_Etiquetas1));
                tbl_datos_fiscales2.AddCell(new iTextSharp.text.Phrase("Expedida en:", Fuente_Valor_Etq));
                tbl_datos_fiscales2.AddCell(new iTextSharp.text.Phrase((string.IsNullOrEmpty(dt_fac.Ciudad) ? "" : dt_fac.Ciudad.Trim() + ", ")
                    + (string.IsNullOrEmpty(dt_fac.Estado) ? "" : dt_fac.Estado.Trim()), Fuente_Etiquetas1));

                if (dt_fac.Timbre_Version.Trim() == "1.0")
                {
                    tbl_datos_fiscales2.AddCell(new iTextSharp.text.Phrase("Metodo de pago:", Fuente_Valor_Etq));
                    tbl_datos_fiscales2.AddCell(new iTextSharp.text.Phrase(dt_fac.Metodo_Pago.Trim() + " "
                        + (string.IsNullOrEmpty(dt_fac.No_Cuenta_Pago.Trim()) ? "" : dt_fac.No_Cuenta_Pago.Trim()), Fuente_Etiquetas1));
                }
                else
                {
                    tbl_datos_fiscales2.AddCell(new iTextSharp.text.Phrase("Forma de pago:", Fuente_Valor_Etq));
                    tbl_datos_fiscales2.AddCell(new iTextSharp.text.Phrase(dt_fac.Forma_Pago.Trim() + " "
                        + (string.IsNullOrEmpty(dt_fac.No_Cuenta_Pago) ? "" : dt_fac.No_Cuenta_Pago.Trim()), Fuente_Etiquetas1));
                }

                tbl_datos_fiscales.AddCell(tbl_datos_fiscales1);
                tbl_datos_fiscales.AddCell(tbl_datos_fiscales2);
                #endregion

                #region (Datos Timbrado)
                iTextSharp.text.pdf.PdfPTable tbl_datos_timbrado = new iTextSharp.text.pdf.PdfPTable(4);
                tbl_datos_timbrado.SetWidthPercentage(new float[] { 215, 215, 205, 364 }, PageSize.A4);
                tbl_datos_timbrado.WidthPercentage = 98;
                tbl_datos_timbrado.DefaultCell.BorderWidth = 0.5f;
                tbl_datos_timbrado.DefaultCell.BorderColor = new BaseColor(205, 203, 203);
                tbl_datos_timbrado.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                tbl_datos_timbrado.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;

                iTextSharp.text.pdf.PdfPTable tbl_datos_timbrado1 = new iTextSharp.text.pdf.PdfPTable(1);
                tbl_datos_timbrado1.SetWidthPercentage(new float[] { 210 }, PageSize.A4);
                tbl_datos_timbrado1.WidthPercentage = 98;
                tbl_datos_timbrado1.DefaultCell.Border = iTextSharp.text.pdf.PdfPCell.NO_BORDER;
                tbl_datos_timbrado1.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                tbl_datos_timbrado1.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                tbl_datos_timbrado1.AddCell(new iTextSharp.text.Phrase("No. Certificado", Fuente_Valor_Etq));
                tbl_datos_timbrado1.AddCell(new iTextSharp.text.Phrase(dt_fac.No_Certificado.Trim(), Fuente_Etiquetas1));

                iTextSharp.text.pdf.PdfPTable tbl_datos_timbrado2 = new iTextSharp.text.pdf.PdfPTable(1);
                tbl_datos_timbrado2.SetWidthPercentage(new float[] { 210 }, PageSize.A4);
                tbl_datos_timbrado2.WidthPercentage = 98;
                tbl_datos_timbrado2.DefaultCell.Border = iTextSharp.text.pdf.PdfPCell.NO_BORDER;
                tbl_datos_timbrado2.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                tbl_datos_timbrado2.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                tbl_datos_timbrado2.AddCell(new iTextSharp.text.Phrase("No. Certificado SAT", Fuente_Valor_Etq));
                tbl_datos_timbrado2.AddCell(new iTextSharp.text.Phrase(dt_fac.Timbre_No_Certificado_SAT.Trim(), Fuente_Etiquetas1));

                iTextSharp.text.pdf.PdfPTable tbl_datos_timbrado3 = new iTextSharp.text.pdf.PdfPTable(1);
                tbl_datos_timbrado3.SetWidthPercentage(new float[] { 200 }, PageSize.A4);
                tbl_datos_timbrado3.WidthPercentage = 98;
                tbl_datos_timbrado3.DefaultCell.Border = iTextSharp.text.pdf.PdfPCell.NO_BORDER;
                tbl_datos_timbrado3.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                tbl_datos_timbrado3.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                tbl_datos_timbrado3.AddCell(new iTextSharp.text.Phrase("Fecha Certificación", Fuente_Valor_Etq));
                tbl_datos_timbrado3.AddCell(new iTextSharp.text.Phrase(string.Format("{0: dd-MMM-yyyy HH:mm:ss}", dt_fac.Timbre_Fecha_Timbrado), Fuente_Etiquetas1));

                iTextSharp.text.pdf.PdfPTable tbl_datos_timbrado4 = new iTextSharp.text.pdf.PdfPTable(1);
                tbl_datos_timbrado4.SetWidthPercentage(new float[] { 357 }, PageSize.A4);
                tbl_datos_timbrado4.WidthPercentage = 98;
                tbl_datos_timbrado4.DefaultCell.Border = iTextSharp.text.pdf.PdfPCell.NO_BORDER;
                tbl_datos_timbrado4.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                tbl_datos_timbrado4.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                tbl_datos_timbrado4.AddCell(new iTextSharp.text.Phrase("Folio Fiscal", Fuente_Valor_Etq));
                tbl_datos_timbrado4.AddCell(new iTextSharp.text.Phrase(dt_fac.Timbre_UUID.Trim(), Fuente_Etiquetas1));

                tbl_datos_timbrado.AddCell(tbl_datos_timbrado1);
                tbl_datos_timbrado.AddCell(tbl_datos_timbrado2);
                tbl_datos_timbrado.AddCell(tbl_datos_timbrado3);
                tbl_datos_timbrado.AddCell(tbl_datos_timbrado4);
                #endregion

                #region (Detalles)
                iTextSharp.text.pdf.PdfPTable tbl_det_header = new iTextSharp.text.pdf.PdfPTable(8);
                tbl_det_header.SetWidthPercentage(new float[] { 110, 110, 110, 120, 435, 105, 180, 170 }, PageSize.A4);
                tbl_det_header.WidthPercentage = 98;
                tbl_det_header.DefaultCell.BorderWidth = 0.5f;
                tbl_det_header.DefaultCell.BorderColor = new BaseColor(205, 203, 203);
                tbl_det_header.DefaultCell.BackgroundColor = new BaseColor(246, 245, 245);
                tbl_det_header.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                tbl_det_header.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                tbl_det_header.AddCell(new iTextSharp.text.Phrase("Cantidad", Fuente_Valor_Etq));
                tbl_det_header.AddCell(new iTextSharp.text.Phrase("Clave Unidad", Fuente_Valor_Etq));
                tbl_det_header.AddCell(new iTextSharp.text.Phrase("Unidad", Fuente_Valor_Etq));
                tbl_det_header.AddCell(new iTextSharp.text.Phrase("Clave Concepto", Fuente_Valor_Etq));
                tbl_det_header.AddCell(new iTextSharp.text.Phrase("Descripción", Fuente_Valor_Etq));
                tbl_det_header.AddCell(new iTextSharp.text.Phrase("Código", Fuente_Valor_Etq));
                tbl_det_header.AddCell(new iTextSharp.text.Phrase("P. Unitario", Fuente_Valor_Etq));
                tbl_det_header.AddCell(new iTextSharp.text.Phrase("Importe", Fuente_Valor_Etq));

                iTextSharp.text.pdf.PdfPTable tbl_det_header2 = new iTextSharp.text.pdf.PdfPTable(8);
                tbl_det_header2.SetWidthPercentage(new float[] { 110, 110, 110, 120, 435, 105, 180, 170 }, PageSize.A4);
                tbl_det_header2.WidthPercentage = 98;
                tbl_det_header2.DefaultCell.BorderWidth = 0.5f;
                tbl_det_header2.DefaultCell.BorderColor = new BaseColor(205, 203, 203);
                tbl_det_header2.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                tbl_det_header2.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;

                iTextSharp.text.Phrase _frase = null;
                iTextSharp.text.pdf.PdfPCell _celda = null;

                dt_fac_det = Obtener_Detalles_Xml(dt_fac.Timbre_XML.ToString());
                if (dt_fac_det != null)
                {
                    if (dt_fac_det.Rows.Count > 0)
                    {
                        foreach (DataRow dr_det in dt_fac_det.Rows)
                        {
                            tbl_det_header2.AddCell(new iTextSharp.text.Phrase(String.Format("{0:n}", dr_det["Cantidad"]), Fuente_Etiquetas1));
                            tbl_det_header2.AddCell(new iTextSharp.text.Phrase(dr_det["ClaveUnidad"].ToString().Trim(), Fuente_Etiquetas1));
                            tbl_det_header2.AddCell(new iTextSharp.text.Phrase(dr_det["Unidad"].ToString().Trim(), Fuente_Etiquetas1));
                            tbl_det_header2.AddCell(new iTextSharp.text.Phrase(dr_det["ClaveProdServ"].ToString().Trim(), Fuente_Etiquetas1));
                            tbl_det_header2.AddCell(new iTextSharp.text.Phrase(dr_det["Nombre"].ToString().Trim(), Fuente_Etiquetas1));
                            tbl_det_header2.AddCell(new iTextSharp.text.Phrase(dr_det["NoIdentificacion"].ToString().Trim(), Fuente_Etiquetas1));

                            _frase = new iTextSharp.text.Phrase(String.Format("{0:n}", dr_det["ValorUnitario"]), Fuente_Etiquetas1);
                            _celda = new iTextSharp.text.pdf.PdfPCell(_frase);
                            _celda.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
                            _celda.BorderWidth = 0.5f;
                            _celda.BorderColor = new BaseColor(205, 203, 203);
                            tbl_det_header2.AddCell(_celda);

                            _frase = new iTextSharp.text.Phrase(String.Format("{0:n}", dr_det["Subtotal"]), Fuente_Etiquetas1);
                            _celda = new iTextSharp.text.pdf.PdfPCell(_frase);
                            _celda.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
                            _celda.BorderWidth = 0.5f;
                            _celda.BorderColor = new BaseColor(205, 203, 203);
                            tbl_det_header2.AddCell(_celda);
                        }
                    }
                }

                #endregion

                #region (Datos Total)
                iTextSharp.text.pdf.PdfPTable tbl_datos_total = new iTextSharp.text.pdf.PdfPTable(2);
                tbl_datos_total.SetWidthPercentage(new float[] { 635, 364 }, PageSize.A4);
                tbl_datos_total.WidthPercentage = 98;
                tbl_datos_total.DefaultCell.BorderWidth = 0.5f;
                tbl_datos_total.DefaultCell.BorderColor = new BaseColor(205, 203, 203);
                tbl_datos_total.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_TOP;
                tbl_datos_total.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;

                iTextSharp.text.pdf.PdfPTable tbl_datos_total1 = new iTextSharp.text.pdf.PdfPTable(1);
                tbl_datos_total1.SetWidthPercentage(new float[] { 623 }, PageSize.A4);
                tbl_datos_total1.WidthPercentage = 98;
                tbl_datos_total1.DefaultCell.Border = iTextSharp.text.pdf.PdfPCell.NO_BORDER;
                tbl_datos_total1.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_TOP;
                tbl_datos_total1.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                tbl_datos_total1.AddCell(new iTextSharp.text.Phrase("Importe total con letra", Fuente_Valor_Etq));
                tbl_datos_total1.AddCell(new iTextSharp.text.Phrase(Convertir_Cantidad_Letras(Convert.ToDecimal(dt_fac.Total), dt_fac.Tipo_Moneda), Fuente_Etiquetas1));
                tbl_datos_total1.AddCell(new iTextSharp.text.Paragraph("\n"));

                iTextSharp.text.pdf.PdfPTable tbl_datos_total2 = new iTextSharp.text.pdf.PdfPTable(2);
                tbl_datos_total2.SetWidthPercentage(new float[] { 175, 181 }, PageSize.A4);
                tbl_datos_total2.WidthPercentage = 98;
                tbl_datos_total2.DefaultCell.Border = iTextSharp.text.pdf.PdfPCell.NO_BORDER;
                tbl_datos_total2.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                tbl_datos_total2.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
                tbl_datos_total2.AddCell(new iTextSharp.text.Phrase("Subtotal", Fuente_Valor_Etq));
                tbl_datos_total2.AddCell(new iTextSharp.text.Phrase(string.Format("{0:n}", dt_fac.Subtotal), Fuente_Etiquetas1));
                if (dt_fac.Descuento > 0)
                {
                    tbl_datos_total2.AddCell(new iTextSharp.text.Phrase("Descuento", Fuente_Valor_Etq));
                    tbl_datos_total2.AddCell(new iTextSharp.text.Phrase(string.Format("{0:n}", dt_fac.Descuento), Fuente_Etiquetas1));
                }
                tbl_datos_total2.AddCell(new iTextSharp.text.Phrase("Impuesto", Fuente_Valor_Etq));
                tbl_datos_total2.AddCell(new iTextSharp.text.Phrase(string.Format("{0:n}", dt_fac.Iva), Fuente_Etiquetas1));
                tbl_datos_total2.AddCell(new iTextSharp.text.Paragraph("\n"));
                tbl_datos_total2.AddCell(new iTextSharp.text.Paragraph("\n"));
                tbl_datos_total2.AddCell(new iTextSharp.text.Phrase("Total", Fuente_Valor_Etq));
                tbl_datos_total2.AddCell(new iTextSharp.text.Phrase(string.Format("{0:n}", dt_fac.Total), Fuente_Etiquetas1));

                tbl_datos_total.AddCell(tbl_datos_total1);
                tbl_datos_total.AddCell(tbl_datos_total2);
                #endregion

                #region (Datos CFDI)
                //generamos el codigo qr
                String Ruta_Carpeta = P_Ruta_Carpeta_Qr;
                String Ruta_qr = dt_fac.Ruta_Codigo_BD;

                if (!Directory.Exists(HttpContext.Current.Server.MapPath(Ruta_Carpeta)))
                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath(Ruta_Carpeta));

                if (!File.Exists(HttpContext.Current.Server.MapPath(Ruta_qr)))
                {
                    Generar_Codigo_Qr(dt_fac.RFC_Emisor.Trim(), dt_fac.RFC.Trim(),
                    Convert.ToDecimal(dt_fac.Total.ToString().Trim()), dt_fac.Timbre_UUID.ToString().Trim(),
                    HttpContext.Current.Server.MapPath(Ruta_Carpeta), HttpContext.Current.Server.MapPath(Ruta_qr));
                }

                iTextSharp.text.Image qr = iTextSharp.text.Image.GetInstance(HttpContext.Current.Server.MapPath(Ruta_qr));
                qr.ScalePercent(90);

                iTextSharp.text.pdf.PdfPTable tbl_datos_cfdi = new iTextSharp.text.pdf.PdfPTable(2);
                tbl_datos_cfdi.SetWidthPercentage(new float[] { 200, 799 }, PageSize.A4);
                tbl_datos_cfdi.WidthPercentage = 98;
                tbl_datos_cfdi.DefaultCell.Border = iTextSharp.text.pdf.PdfPCell.NO_BORDER;
                tbl_datos_cfdi.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                tbl_datos_cfdi.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                tbl_datos_cfdi.AddCell(qr);

                iTextSharp.text.pdf.PdfPTable tbl_datos_cfdi1 = new iTextSharp.text.pdf.PdfPTable(1);
                tbl_datos_cfdi1.SetWidthPercentage(new float[] { 630 }, PageSize.A4);
                tbl_datos_cfdi1.WidthPercentage = 95;
                tbl_datos_cfdi1.DefaultCell.Border = iTextSharp.text.pdf.PdfPCell.NO_BORDER;
                tbl_datos_cfdi1.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                tbl_datos_cfdi1.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                tbl_datos_cfdi1.AddCell(new iTextSharp.text.Phrase("Versión CFDI", Fuente_Valor_Etq));
                tbl_datos_cfdi1.AddCell(new iTextSharp.text.Phrase("3.3", Fuente_Etiquetas2));
                tbl_datos_cfdi1.AddCell(new iTextSharp.text.Phrase("Cadena original de complemento de Certificado Digital SAT", Fuente_Valor_Etq));
                tbl_datos_cfdi1.AddCell(new iTextSharp.text.Phrase("||" + dt_fac.Timbre_Version.ToString().Trim() //version
                    + "|" + dt_fac.Timbre_UUID.ToString().Trim() //UUID
                    + "|" + string.Format("{0:yyyy-MM-dd}", dt_fac.Timbre_Fecha_Timbrado)
                    + "T" + string.Format("{0:HH:mm:ss}", dt_fac.Timbre_Fecha_Timbrado) //fecha timbrado
                    + "|" + dt_fac.RFC_Emisor.ToString().Trim()  // RfcProvCertif
                                                                 //leyenda
                    + "|" + dt_fac.Timbre_Sello_CFD.Trim()  //SelloCFD
                    + "|" + dt_fac.Timbre_No_Certificado_SAT.Trim() + "||", Fuente_Etiquetas2)); //NoCertificadoSAT
                tbl_datos_cfdi1.AddCell(new iTextSharp.text.Paragraph("\n"));
                tbl_datos_cfdi1.AddCell(new iTextSharp.text.Phrase("Sello Digital del Emisor", Fuente_Valor_Etq));
                tbl_datos_cfdi1.AddCell(new iTextSharp.text.Phrase(dt_fac.Timbre_Sello_CFD.Trim(), Fuente_Etiquetas2));
                tbl_datos_cfdi1.AddCell(new iTextSharp.text.Paragraph("\n"));
                tbl_datos_cfdi1.AddCell(new iTextSharp.text.Phrase("Sello Digital del SAT", Fuente_Valor_Etq));
                tbl_datos_cfdi1.AddCell(new iTextSharp.text.Phrase(dt_fac.Timbre_Sello_SAT.Trim(), Fuente_Etiquetas2));
                tbl_datos_cfdi.AddCell(tbl_datos_cfdi1);
                #endregion

                // Se agrega el PDFTable al documento.
                Documento.Add(new iTextSharp.text.Paragraph("\n"));
                Documento.Add(tbl_datos_emp);

                Documento.Add(new iTextSharp.text.Paragraph("\n"));
                Documento.Add(tbl_datos_fiscales);
                Documento.Add(tbl_datos_timbrado);
                #region (Datos Timbrado Relacionado)
                if (!string.IsNullOrEmpty(dt_fac.UUID_Relacionado))
                {
                    iTextSharp.text.pdf.PdfPTable tbl_datos_timbradoR = new iTextSharp.text.pdf.PdfPTable(3);
                    tbl_datos_timbradoR.SetWidthPercentage(new float[] { 430, 205, 364 }, PageSize.A4);
                    tbl_datos_timbradoR.WidthPercentage = 98;
                    tbl_datos_timbradoR.DefaultCell.BorderWidth = 0.5f;
                    tbl_datos_timbradoR.DefaultCell.BorderColor = new BaseColor(205, 203, 203);
                    tbl_datos_timbradoR.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                    tbl_datos_timbradoR.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;

                    iTextSharp.text.pdf.PdfPTable tbl_datos_timbradoR2 = new iTextSharp.text.pdf.PdfPTable(1);
                    tbl_datos_timbradoR2.SetWidthPercentage(new float[] { 425 }, PageSize.A4);
                    tbl_datos_timbradoR2.WidthPercentage = 98;
                    tbl_datos_timbradoR2.DefaultCell.Border = iTextSharp.text.pdf.PdfPCell.NO_BORDER;
                    tbl_datos_timbradoR2.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                    tbl_datos_timbradoR2.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                    tbl_datos_timbradoR2.AddCell(new iTextSharp.text.Phrase("Tipo Relación", Fuente_Valor_Etq));
                    tbl_datos_timbradoR2.AddCell(new iTextSharp.text.Phrase(dt_fac.Tipo_Relacion.Trim(), Fuente_Etiquetas1));

                    iTextSharp.text.pdf.PdfPTable tbl_datos_timbradoR1 = new iTextSharp.text.pdf.PdfPTable(1);
                    tbl_datos_timbradoR1.SetWidthPercentage(new float[] { 200 }, PageSize.A4);
                    tbl_datos_timbradoR1.WidthPercentage = 98;
                    tbl_datos_timbradoR1.DefaultCell.Border = iTextSharp.text.pdf.PdfPCell.NO_BORDER;
                    tbl_datos_timbradoR1.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                    tbl_datos_timbradoR1.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                    tbl_datos_timbradoR1.AddCell(new iTextSharp.text.Phrase("Factura Relacionada", Fuente_Valor_Etq));
                    tbl_datos_timbradoR1.AddCell(new iTextSharp.text.Phrase(dt_fac.No_Factura + " " + dt_fac.Serie.ToString().Trim(), Fuente_Etiquetas1));

                    iTextSharp.text.pdf.PdfPTable tbl_datos_timbradoR3 = new iTextSharp.text.pdf.PdfPTable(1);
                    tbl_datos_timbradoR3.SetWidthPercentage(new float[] { 357 }, PageSize.A4);
                    tbl_datos_timbradoR3.WidthPercentage = 98;
                    tbl_datos_timbradoR3.DefaultCell.Border = iTextSharp.text.pdf.PdfPCell.NO_BORDER;
                    tbl_datos_timbradoR3.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                    tbl_datos_timbradoR3.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                    tbl_datos_timbradoR3.AddCell(new iTextSharp.text.Phrase("Folio fiscal relacionado", Fuente_Valor_Etq));
                    tbl_datos_timbradoR3.AddCell(new iTextSharp.text.Phrase(dt_fac.UUID_Relacionado.Trim(), Fuente_Etiquetas1));

                    tbl_datos_timbradoR.AddCell(tbl_datos_timbradoR2);
                    tbl_datos_timbradoR.AddCell(tbl_datos_timbradoR1);
                    tbl_datos_timbradoR.AddCell(tbl_datos_timbradoR3);
                    Documento.Add(tbl_datos_timbradoR);
                }
                #endregion
                Documento.Add(new iTextSharp.text.Paragraph("\n"));
                Documento.Add(tbl_det_header);
                Documento.Add(tbl_det_header2);
                Documento.Add(new iTextSharp.text.Paragraph("\n"));
                Documento.Add(tbl_datos_total);
                Documento.Add(new iTextSharp.text.Paragraph("\n"));
                Documento.Add(tbl_datos_emp2);
                Documento.Add(tbl_datos_emp22);
                Documento.Add(tbl_datos_emp4);
                Documento.Add(new iTextSharp.text.Paragraph("\n"));
                Documento.Add(new iTextSharp.text.Paragraph("\n"));
                Documento.Add(tbl_datos_cfdi);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Metodo para crear el codigo qr
        /// </summary>
        /// <creo>Juan Alberto Hernández Negrete</creo>
        /// <fecha_creo>18-agosto-2016</fecha_creo>
        public static void Generar_Codigo_Qr(string Rfc_Empresa, string Rfc_Afiliado, Decimal Total_Factura, string Timbre_UUID, string Ruta_Carpeta, string Ruta_Qr)
        {
            string Codigo_Bidimensional = String.Empty;
            try
            {
                if (!Directory.Exists(Ruta_Carpeta))
                    Directory.CreateDirectory(Ruta_Carpeta);

                if (!File.Exists((Ruta_Qr)))
                {
                    //  Genera el codigo bidimensional
                    Codigo_Bidimensional = "?re=" + Rfc_Empresa + "&rr=" + Rfc_Afiliado + "&tt=" + string.Format("{0:0000000000.000000}", Total_Factura) + "&id=" + Timbre_UUID;
                    Cls_Timbrado.Generar_Codigo_QR(Codigo_Bidimensional, Ruta_Qr);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Metodo para obtener los detalles de la factura desde el xml
        /// </summary>
        /// <creo>Juan Alberto Hernández Negrete</creo>
        /// <fecha_creo>8-septiembre-2016</fecha_creo>
        public static DataTable Obtener_Detalles_Xml(String Xml)
        {
            DataTable Dt_Det_ = new DataTable();
            XmlDocument xml_ = new XmlDocument();
            XmlNode xml_nodo;
            XmlNode xml_nodo_temp;
            string descripcion_ = string.Empty;
            string subtotal = string.Empty;
            string cantidad_ = string.Empty;
            string valorunitario = string.Empty;
            string unidad_ = string.Empty;
            string claveunidad = string.Empty;
            string claveprodserv = string.Empty;
            string noidentificacion = string.Empty;
            string descuento = string.Empty;
            DataRow Fila = null;
            try
            {

                Dt_Det_.Columns.Add("Cantidad", typeof(Decimal));
                Dt_Det_.Columns.Add("Unidad", typeof(String));
                Dt_Det_.Columns.Add("Nombre", typeof(String));
                Dt_Det_.Columns.Add("Subtotal", typeof(Decimal));
                Dt_Det_.Columns.Add("ValorUnitario", typeof(Decimal));
                Dt_Det_.Columns.Add("ClaveUnidad", typeof(String));
                Dt_Det_.Columns.Add("ClaveProdServ", typeof(String));
                Dt_Det_.Columns.Add("NoIdentificacion", typeof(String));
                Dt_Det_.Columns.Add("Descuento", typeof(String));


                xml_.LoadXml(Xml);
                XmlNamespaceManager xmlnsManager = new XmlNamespaceManager(xml_.NameTable);
                xmlnsManager.AddNamespace("cfdi", "http://www.sat.gob.mx/cfd/3");
                xmlnsManager.AddNamespace("xsi", "http://www.w3.org/2001/XMLSchema-instance");
                xmlnsManager.AddNamespace("tfd", "http://www.sat.gob.mx/TimbreFiscalDigital");

                xml_nodo = xml_.SelectSingleNode("//cfdi:Conceptos", xmlnsManager);
                if (xml_nodo != null)
                {
                    for (int i = 0; i < xml_nodo.ChildNodes.Count; i++)
                    {
                        descripcion_ = string.Empty;
                        subtotal = string.Empty;
                        valorunitario = string.Empty;
                        descuento = string.Empty;
                        xml_nodo_temp = xml_nodo.ChildNodes[i];
                        for (int x = 0; x < xml_nodo_temp.Attributes.Count; x++)
                        {
                            if (xml_nodo_temp.Attributes[x].Name.ToLower() == "cantidad")
                                cantidad_ = xml_nodo_temp.Attributes[x].Value;
                            else if (xml_nodo_temp.Attributes[x].Name.ToLower() == "unidad")
                                unidad_ = xml_nodo_temp.Attributes[x].Value;
                            else if (xml_nodo_temp.Attributes[x].Name.ToLower() == "claveprodserv")
                                claveprodserv = xml_nodo_temp.Attributes[x].Value;
                            else if (xml_nodo_temp.Attributes[x].Name.ToLower() == "claveunidad")
                                claveunidad = xml_nodo_temp.Attributes[x].Value;
                            else if (xml_nodo_temp.Attributes[x].Name.ToLower() == "descripcion")
                                descripcion_ = xml_nodo_temp.Attributes[x].Value;
                            else if (xml_nodo_temp.Attributes[x].Name.ToLower() == "importe")
                                subtotal = xml_nodo_temp.Attributes[x].Value;
                            else if (xml_nodo_temp.Attributes[x].Name.ToLower() == "valorunitario")
                                valorunitario = xml_nodo_temp.Attributes[x].Value;
                            else if (xml_nodo_temp.Attributes[x].Name.ToLower() == "noidentificacion")
                                noidentificacion = xml_nodo_temp.Attributes[x].Value;
                            else if (xml_nodo_temp.Attributes[x].Name.ToLower() == "descuento")
                                descuento = xml_nodo_temp.Attributes[x].Value;
                        }

                        Fila = Dt_Det_.NewRow();
                        Fila["Cantidad"] = Convert.ToDecimal(string.IsNullOrEmpty(cantidad_) ? "0" : cantidad_);
                        Fila["Unidad"] = unidad_;
                        Fila["Nombre"] = descripcion_;
                        Fila["ClaveUnidad"] = claveunidad;
                        Fila["ClaveProdServ"] = claveprodserv;
                        Fila["NoIdentificacion"] = noidentificacion;
                        Fila["Subtotal"] = Convert.ToDecimal(string.IsNullOrEmpty(subtotal) ? "0" : subtotal);
                        Fila["ValorUnitario"] = Convert.ToDecimal(string.IsNullOrEmpty(valorunitario) ? "0" : valorunitario);
                        Fila["Descuento"]= Convert.ToDecimal(string.IsNullOrEmpty(descuento) ? "0" : descuento);
                        Dt_Det_.Rows.Add(Fila);
                    }
                }
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            return Dt_Det_;
        }


    }
}