using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using datos_trazabilidad;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Dynamic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;

namespace web_trazabilidad.Models.Ayudante
{
    public static class Cls_Metodos_Generales
    {

        #region (Variables)
        private static String[] Formatos = new String[]{
                    "dd/MM/yyyy",
                    "MM/dd/yyyy",
                    "MMM/dd/yyyy",
                    "dd/MMM/yyyy",
                    "dd/MM/yy",
                    "d/MM/yy",
                    "d/M/yy",
                    "dd-MM-yy",
                    "yyyy-MM-dd",
                    "dd-MMM-yyyy",
                    "d-M-yyyy",
                    "d-M-yy",
                    "yyyy/MMM/dd",
                    "yy/MM/dd",
                    "yy/M/dd",
                    "dd/MMMM/yyyy",
                    "MMMM/dd/yyyy",
                    "yyyy/MMMM/dd",
                    "ddMMyyyy",
                    "MMddyyyy",
                    "ddMMMyyyy",
                    "MMMddyyyy",
                    "dddd, dd MMMM yyyy",
                    "dddd, dd MMMM yyyy HH:mm",
                    "dddd, dd MMMM yyyy HH:mm:ss",
                    "MM/dd/yyyy HH:mm",
                    "MM/dd/yyyy HH:mm:ss",
                    "MMMM dd",
                    "ddd, dd MMM yyyy",
                    "dddd, dd MMMM yyyy HH:mm:ss",
                    "yyyy MMMM",
                    "yyyy-MM-dd HH:mm:ss",
                    "MMMMddyyyy",
                    "ddMMMMyyyy",
                    "yyyyddMMMM",
                    "MMddyy",
                    "ddMMyy",
                    "yyMMdd"
        };
        #endregion

        #region (Métodos)

        public static List<T> getItemsToDisplayFromTheTable<T>(string Pagina, string Tabla, string PK)
        {
            List<T> result = new List<T>();

            using (var dbContext = new Sistema_TrazabilidadEntities())
            {
                SqlParameter sqlEmpresaID = new SqlParameter("@Empresa_ID", Cls_Sesiones.Empresa_ID);
                SqlParameter sqlSucursalID = new SqlParameter("@Sucursal_ID", Cls_Sesiones.Sucursal_ID);
                SqlParameter sqlPagina = new SqlParameter("@Pagina", Pagina);
                SqlParameter sqlTabla = new SqlParameter("@Tabla", Tabla);
                SqlParameter sqlPK = new SqlParameter("@PK", PK);

                result = dbContext.Database.SqlQuery<T>("spGetItemsToDisplayFromTheTable @Empresa_ID, @Sucursal_ID, @Pagina, @Tabla, @PK",
                    sqlEmpresaID, sqlSucursalID, sqlPagina, sqlTabla, sqlPK).ToList();
            }
            return result;
        }

        public static String Convertir_Cantidad_Letras(Decimal Cantidad_Numero, String Moneda)
        {
            String Cantidad_Letra = String.Empty;
            Cls_Numeros_Letras Letras = new Cls_Numeros_Letras();


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

        public static bool Generar_Reporte(ref DataSet Ds_Datos, String Ruta_Plantilla, String Nombre_Reporte_Generar, String Ruta_PDF)
        {
            ReportDocument Reporte = new ReportDocument();//Variable de tipo reporte.
            try
            {
                Reporte.Load(Ruta_Plantilla);

                if (Ds_Datos is DataSet)
                {
                    if (Ds_Datos.Tables.Count > 0)
                    {
                        Reporte.SetDataSource(Ds_Datos);
                        Exportar_Reporte_PDF(Reporte, Nombre_Reporte_Generar, Ruta_PDF);
                    }
                }

                return true;
            }
            catch (Exception Ex)
            {
                throw new Exception("Error al generar el reporte. Error: [" + Ex.Message + "]");
            }
        }

        public static void Exportar_Reporte_PDF(ReportDocument Reporte, String Nombre_Reporte, String Ruta_PDF)
        {
            ExportOptions Opciones_Exportacion = new ExportOptions();
            DiskFileDestinationOptions Direccion_Guardar_Disco = new DiskFileDestinationOptions();
            PdfRtfWordFormatOptions Opciones_Formato_PDF = new PdfRtfWordFormatOptions();

            try
            {
                if (Reporte is ReportDocument)
                {
                    Direccion_Guardar_Disco.DiskFileName = Ruta_PDF;
                    Opciones_Exportacion.ExportDestinationOptions = Direccion_Guardar_Disco;
                    Opciones_Exportacion.ExportDestinationType = ExportDestinationType.DiskFile;
                    Opciones_Exportacion.ExportFormatType = ExportFormatType.PortableDocFormat;

                    Reporte.Export(Opciones_Exportacion);
                }
            }
            catch (Exception Ex)
            {
                throw new Exception("Error al exportar el reporte. Error: [" + Ex.Message + "]");
            }
        }


        public static byte[] Convertir_Imagen_Bytes(String Ruta, int Nuevo_Ancho, int Resolucion)
        {
            try
            {
                System.Drawing.Image Imagen = System.Drawing.Image.FromFile(Ruta);
                MemoryStream Ms = new MemoryStream();
                int Old_Ancho = Imagen.Width;
                int Old_Alto = Imagen.Height;
                Double Aux_Alto = ((Convert.ToDouble(Nuevo_Ancho) / Convert.ToDouble(Old_Ancho)) * Convert.ToDouble(Old_Alto));
                int Nuevo_Alto = Convert.ToInt32(Aux_Alto);
                Bitmap Nueva_Imagen = new Bitmap(Imagen, Nuevo_Ancho, Nuevo_Alto);
                Nueva_Imagen.SetResolution(Resolucion, Resolucion);
                Nueva_Imagen.Save(Ms, Imagen.RawFormat);

                return Ms.ToArray();
            }
            catch
            {
                return new byte[0];
            }
        }

        /// <summary>
        /// Método que ayuda al proceso de utilizar un procedimiento sin necesidad de actualizar el Entity Framework
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sp"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static List<T> executeProc<T>(string sp, SqlParameter[] parameters)
        {
            List<T> result = new List<T>();

            using (var dbContext = new Sistema_TrazabilidadEntities())
            {
                result = dbContext.Database.SqlQuery<T>(sp, parameters).ToList();
            }
            return result;
        }
        /// <summary>
        /// Método que sirve para tratar la formula, quitar la constantes y agregar un valor "X" y asi realizar su evaluacion
        /// </summary>
        /// <param name="Dt_"></param>
        /// <param name="Formula_"></param>
        /// <returns></returns>
        public static string Tratar_Formula_Evaluacion(string Formula_)
        {
            string Formula_Temporal = Formula_;
            string Formula_Final = string.Empty;

            Formula_Final = Formula_Temporal.Replace("[", "emp.");
            Formula_Temporal = Formula_Final;
            Formula_Final = Formula_Temporal.Replace("]", "");

            return Formula_Final;
        }
        /// <summary>
        /// Metodo que sirve para tratar la formula, cambia los operadores && y || por los operadores PYTHON
        /// </summary>
        /// <param name="Formula_"></param>
        /// <returns></returns>
        public static string Tratar_Formula_Evaluacion_Caracteres_Logicos(string Formula_)
        {
            string Formula_Temporal = Formula_;
            string Formula_Final = string.Empty;

            Formula_Final = Formula_Temporal.Replace("&&", " and ");
            Formula_Temporal = Formula_Final;
            Formula_Final = Formula_Temporal.Replace("||", " or ");

            return Formula_Final;
        }
        /// <summary>
        /// Método que genera una lista del tipo deseado
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="table"></param>
        /// <returns></returns>
        public static List<T> DataTableToList<T>(this DataTable table) where T : class, new()
        {
            try
            {
                List<T> list = new List<T>();

                foreach (var row in table.AsEnumerable())
                {
                    T obj = new T();

                    foreach (var prop in obj.GetType().GetProperties())
                    {
                        try
                        {
                            PropertyInfo propertyInfo = obj.GetType().GetProperty(prop.Name);
                            propertyInfo.SetValue(obj, Convert.ChangeType(row[prop.Name], propertyInfo.PropertyType), null);
                        }
                        catch
                        {
                            continue;
                        }
                    }

                    list.Add(obj);
                }

                return list;
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// Método que genera un datatable de una lista
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public static DataTable ToDataTable<T>(this IList<T> data)
        {
            PropertyDescriptorCollection properties =
                TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;
        }
        public static DataTable ToDataTable_List_Dict(List<Dictionary<string, object>> list)
        {
            DataTable result = new DataTable();
            if (list.Count == 0)
                return result;

            var columnNames = list.SelectMany(dict => dict.Keys).Distinct();
            result.Columns.AddRange(columnNames.Select(c => new DataColumn(c)).ToArray());
            foreach (Dictionary<string, object> item in list)
            {
                var row = result.NewRow();
                foreach (var key in item.Keys)
                {
                    row[key] = item[key];
                }

                result.Rows.Add(row);
            }

            return result;
        }
        /// <summary>
        /// Método que sirve para obtener el bimestre y el mes
        /// </summary>
        /// <param name="_fecha"></param>
        /// <returns>2 posiciones, la primera contiene el mes y la segunda contiene el bimestre</returns>
        public static string[] Obtener_Mes_Bimestre(DateTime _fecha)
        {
            string[] response = new string[2];

            int mes = _fecha.Month;
            double bimestre = mes % 2 == 0 ? (mes / 2) : ((Convert.ToDouble(mes) / 2) + 0.5);

            response[0] = mes.ToString();
            response[1] = bimestre.ToString();

            return response;
        }
        /// <summary>
        /// Metodo que sirve para leer un archivo de excel
        /// </summary>
        /// <param name="Ruta"></param>
        /// <returns></returns>
        public static DataTable Leer_Excel_Cargar_Conceptos(string Ruta)
        {
            String Rta = Ruta;
            string sConnectionString = "";
            DataTable Dt = new DataTable();
            Dt.Columns.Add("No_Empleado", typeof(string));
            Dt.Columns.Add("Nombre", typeof(string));
            Dt.Columns.Add("Clave_Concepto", typeof(string));
            Dt.Columns.Add("Cantidad", typeof(string));
            Dt.Columns.Add("Importe", typeof(string));
            string no_empleado = string.Empty;
            string nombre = string.Empty;
            string clave = string.Empty;
            string cantidad = string.Empty;
            string importe = string.Empty;

            sConnectionString = Rta.Contains(".xlsx") ? @"Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=" + Rta + ";" + "Extended Properties=\"Excel 12.0 Xml;HDR=YES;IMEX=1\""//.xlsx
                                                      : @"Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=" + Rta + ";" + "Extended Properties=\"Excel 12.0 Xml;HDR=YES;IMEX=1\"";//.xls

            using (OleDbConnection connection = new OleDbConnection(sConnectionString))
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand("select * from [Conceptos$]", connection);
                using (OleDbDataReader dr = command.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        for (int i = 0; i < dr.FieldCount; i++)
                        {
                            if (dr.GetName(i).ToLower().Contains("empleado"))
                            {
                                no_empleado = dr[i].ToString();
                            }
                            else if (dr.GetName(i).ToLower().Contains("nombre"))
                            {
                                nombre = dr[i].ToString();
                            }
                            else if (dr.GetName(i).ToLower().Contains("clave"))
                            {
                                clave = dr[i].ToString();
                            }
                            else if (dr.GetName(i).ToLower().Contains("cantidad"))
                            {
                                cantidad = dr[i].ToString();
                            }
                            else if (dr.GetName(i).ToLower().Contains("importe"))
                            {
                                importe = dr[i].ToString();
                            }
                        }

                        DataRow Dr_ = Dt.NewRow();
                        Dr_["No_Empleado"] = no_empleado;
                        Dr_["Nombre"] = nombre;
                        Dr_["Clave_Concepto"] = clave;
                        Dr_["Cantidad"] = cantidad;
                        Dr_["Importe"] = importe;

                        Dt.Rows.Add(Dr_);
                    }
                }
            }

            return Dt;
        }
        /// <summary>
        /// Metodo que sirve para obtener la fecha
        /// </summary>
        /// <param name="Fecha"></param>
        /// <returns></returns>
        public static DateTime Obtener_Fecha(String Fecha)
        {
            try
            {
                return DateTime.ParseExact(Fecha, Formatos,
                    CultureInfo.CurrentCulture, DateTimeStyles.AllowWhiteSpaces);
            }
            catch (FormatException Ex)
            {
                try
                {
                    return DateTime.ParseExact(Fecha, Formatos,
                        new CultureInfo("es-Mx"), DateTimeStyles.AllowWhiteSpaces);
                }
                catch (FormatException Ex_Es_Mexico)
                {
                    try
                    {
                        return DateTime.ParseExact(Fecha, Formatos,
                            new CultureInfo("en-US"), DateTimeStyles.AllowWhiteSpaces);
                    }
                    catch (FormatException Ex_Ingles_EUA)
                    {
                        try
                        {
                            return DateTime.ParseExact(Fecha, Formatos,
                                new CultureInfo("es-ES"), DateTimeStyles.AllowWhiteSpaces);
                        }
                        catch (FormatException Ex_Es_Espana)
                        {
                            try
                            {
                                return DateTime.ParseExact(Fecha, Formatos,
                                    new CultureInfo("es"), DateTimeStyles.AllowWhiteSpaces);
                            }
                            catch (FormatException Ex_Es)
                            {
                                try
                                {
                                    return DateTime.ParseExact(Fecha, Formatos,
                                        new CultureInfo("en"), DateTimeStyles.AllowWhiteSpaces);
                                }
                                catch (FormatException Ex_Ingles)
                                {
                                    throw new Exception("Cultura de Fecha Incorrecto. Error: [" + Ex.Message + "]");
                                }
                            }
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Metodo que sirve para obtener el numero de dias bimestre
        /// </summary>
        /// <param name="Anio"></param>
        /// <param name="Bimestre"></param>
        /// <returns></returns>
        public static int Obtener_Numero_Dias_Bimestre(int Anio, int Bimestre)
        {
            int _reponse = 0;

            if (Bimestre == 1)
            {
                int Dias_Mes_1 = DateTime.DaysInMonth(Anio, 1);
                int Dias_Mes_2 = DateTime.DaysInMonth(Anio, 2);

                _reponse = Dias_Mes_1 + Dias_Mes_2;
            }
            else if (Bimestre == 2)
            {
                int Dias_Mes_3 = DateTime.DaysInMonth(Anio, 3);
                int Dias_Mes_4 = DateTime.DaysInMonth(Anio, 4);

                _reponse = Dias_Mes_3 + Dias_Mes_4;
            }
            else if (Bimestre == 3)
            {
                int Dias_Mes_5 = DateTime.DaysInMonth(Anio, 5);
                int Dias_Mes_6 = DateTime.DaysInMonth(Anio, 6);

                _reponse = Dias_Mes_5 + Dias_Mes_6;
            }
            else if (Bimestre == 4)
            {
                int Dias_Mes_7 = DateTime.DaysInMonth(Anio, 7);
                int Dias_Mes_8 = DateTime.DaysInMonth(Anio, 8);

                _reponse = Dias_Mes_7 + Dias_Mes_8;
            }
            else if (Bimestre == 5)
            {
                int Dias_Mes_9 = DateTime.DaysInMonth(Anio, 9);
                int Dias_Mes_10 = DateTime.DaysInMonth(Anio, 10);

                _reponse = Dias_Mes_9 + Dias_Mes_10;
            }
            else if (Bimestre == 6)
            {
                int Dias_Mes_11 = DateTime.DaysInMonth(Anio, 11);
                int Dias_Mes_12 = DateTime.DaysInMonth(Anio, 12);

                _reponse = Dias_Mes_11 + Dias_Mes_12;
            }

            return _reponse;
        }
        /// <summary>
        /// Metodo que sive para leer una lista de diccionario de datos
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static List<Dictionary<string, object>> read(DbDataReader reader)
        {
            List<Dictionary<string, object>> Lista = new List<Dictionary<string, object>>();

            foreach (var item in reader)
            {
                IDictionary<string, object> expando = new ExpandoObject();

                foreach (PropertyDescriptor propertyDescriptor in TypeDescriptor.GetProperties(item))
                {
                    var obj = propertyDescriptor.GetValue(item);
                    expando.Add(propertyDescriptor.Name, obj);
                }

                Lista.Add(new Dictionary<string, object>(expando));
            }

            return Lista;
        }
        /// <summary>
        /// Metodo que sirve para validar un datatable sea diferente a nulo y tenga registros
        /// </summary>
        /// <param name="Dt"></param>
        /// <returns></returns>
        public static bool Valida_DataTable(DataTable Dt)
        {
            bool _response = false;

            if (Dt != null)
            {
                if (Dt.Rows.Count > 0)
                {
                    _response = true;
                }
            }

            return _response;
        }

        #endregion (Métodos)
    }
}