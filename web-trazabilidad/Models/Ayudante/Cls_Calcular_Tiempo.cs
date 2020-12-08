using datos_trazabilidad;
using Elmah;
using LitJson;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.Script.Services;
using System.Web.Services;
using web_trazabilidad.Models.Ayudante;
using web_trazabilidad.Models.Negocio;
using web_trazabilidad.Models.Negocio.Catalogos;
using web_trazabilidad.Models.Negocio.Operaciones;

namespace web_trazabilidad.Models.Ayudante
{
    public class Cls_Calcular_Tiempo
    {

        public static void Calcular_Tiempos(List<Cls_Ope_Evento_Registro_Tiempo_Negocio> Registros)
        {
            TimeSpan Tiempo_Idela;
            TimeSpan Tiempo_Real;
            TimeSpan Tiempo_Puntuacion;
            Double Db_Tiempo = 0;

            try
            {
                using (var dbContext = new Sistema_TrazabilidadEntities())
                {
                    Ope_Eventos_Registro_Tiempo registro_tiempo = new Ope_Eventos_Registro_Tiempo();


                    foreach (var datos in Registros.ToList())
                    {
                        //  se inicializan los valores
                        Db_Tiempo = 0;

                        //  se consulta el registro del tiempo
                        registro_tiempo = dbContext.Ope_Eventos_Registro_Tiempo.Where(w => w.Registro_Id == datos.Registro_Id).FirstOrDefault();
                        Tiempo_Idela = TimeSpan.Parse(datos.Tiempo_Ideal.ToString());
                        Tiempo_Real = TimeSpan.Parse(datos.Tiempo_Real.ToString());

                        Tiempo_Puntuacion = Tiempo_Idela - Tiempo_Real;
                        Db_Tiempo = Tiempo_Puntuacion.TotalSeconds;

                        //  se genere el valor a positivo
                        if (Db_Tiempo < 0) { Db_Tiempo = Db_Tiempo * -1; }

                        //  si es mayor el valor a 600 se le asignara como maximo el valor de 600
                        if (Db_Tiempo > 600) { Db_Tiempo = 600; }


                        registro_tiempo.Puntuacion = Convert.ToInt32(Db_Tiempo);
                        registro_tiempo.Usuario_Modifico = Cls_Sesiones.Usuario;
                        registro_tiempo.Fecha_Modifico = DateTime.Now;

                    }

                    dbContext.SaveChanges();

                }
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.ToString());
            }

        }

    }
}