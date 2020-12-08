using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using datos_trazabilidad;
using web_trazabilidad.Models.Negocio;
using web_trazabilidad.Models.Ayudante;
//using Telerik.Reporting;

namespace web_trazabilidad.Models.Ayudante
{
    public class Cls_Utilidades
    {
       
        /// <summary>
        /// Metodo para generar el archivo del reporte
        /// </summary>
        /// <creo>Jose Maldonado Mendez</creo>
        /// <Parametros>report: Reporte del cual se va a generar el archivo
        ///             ruta: Ruta absoluta donde se guardara el archivo
        ///             Nombre_Reporte: Nombre del archivo
        ///             formato: La extension del archivo que se va a generar
        /// </Parametros>
        /// <fecha_creo>06/10/2016</fecha_creo>
        /// <modifico></modifico>
        /// <fecha_modifico></fecha_modifico>
        /// <causa_modificacion></causa_modificacion>
        //public static bool Generar_Archivo_Reporte(Report report, string ruta, string Nombre_Reporte, string formato)
        //{
        //    bool Resultado = false;
        //    try
        //    {
        //        Telerik.Reporting.Processing.ReportProcessor reportProcessor = new Telerik.Reporting.Processing.ReportProcessor();

        //        var reportSource = new Telerik.Reporting.InstanceReportSource();
        //        reportSource.ReportDocument = report;

        //        // set any deviceInfo settings if necessary
        //        System.Collections.Hashtable deviceInfo = new System.Collections.Hashtable();

        //        Telerik.Reporting.Processing.RenderingResult result = reportProcessor.RenderReport(formato, reportSource, deviceInfo);

        //        string fileName = Nombre_Reporte + "." + result.Extension;
        //        string filePath = System.IO.Path.Combine(ruta, fileName);

        //        using (System.IO.FileStream fs = new System.IO.FileStream(filePath, System.IO.FileMode.Create))
        //        {
        //            fs.Write(result.DocumentBytes, 0, result.DocumentBytes.Length);
        //        }
        //        Resultado = true;
        //    }catch(Exception Ex)
        //    {

        //    }
        //    return Resultado;
        //}

        

        ///'*******************************************************************************
        ///'NOMBRE DE LA FUNCIÓN: CFD_Elimina_Espacios
        ///'DESCRIPCIÓN: Eliminas los n espacios entre palabras
        ///'PARÁMETROS:
        ///'               1. Cadena, cadena a eliminar los espacios
        ///'CREO: Miguel Angel Alvarado Enriquez
        ///'FECHA_CREO: 07/Agosto/2013 10:18 pm
        ///'MODIFICO:
        ///'FECHA_MODIFICO
        ///'CAUSA_MODIFICACIÓN
        ///'*******************************************************************************
        public static String CFD_Elimina_Espacios(String Cadena)
        {
            String[] Grupo_Cadena;
            Int32 Cuenta_Cadena;
            String Cadena_Nueva;
            String CFD_Elimina_Espacios_Blanco;


            try
            {
                //Divide la cadena por los espacios
                Grupo_Cadena = Cadena.Split(new Char[] { ' ' });

                //Valida la longitud
                if (Grupo_Cadena.GetUpperBound(0) > 0)
                {
                    Cadena_Nueva = "";

                    for (Cuenta_Cadena = 0; Cuenta_Cadena <= Grupo_Cadena.GetUpperBound(0); Cuenta_Cadena++) //Grupo_Cadena.Length gives the same error
                    {
                        if (Grupo_Cadena[Cuenta_Cadena].Trim() != "")
                        {
                            Cadena_Nueva = Cadena_Nueva + " " + Grupo_Cadena[Cuenta_Cadena].Trim();
                        }
                    }
                    if (Cadena_Nueva.Trim() != "")
                    {
                        CFD_Elimina_Espacios_Blanco = Cls_Timbrado.Valida_Caracteres_UTF(Cadena_Nueva.Trim());
                    }
                    else
                    {
                        CFD_Elimina_Espacios_Blanco = Cadena_Nueva.Trim();
                    }

                }
                else
                {
                    if (Cadena.Trim() != "")
                    {
                        CFD_Elimina_Espacios_Blanco = Cls_Timbrado.Valida_Caracteres_UTF(Cadena.Trim());
                    }
                    else
                    {
                        CFD_Elimina_Espacios_Blanco = Cadena.Trim();
                    }
                }
            }

            catch (Exception Ex)
            {
                throw new Exception("CFD_Elimina_Espacios. Error:[" + Ex.Message + "]");
            }
            return CFD_Elimina_Espacios_Blanco;
        }

    }
}