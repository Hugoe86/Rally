using datos_trazabilidad;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;

namespace web_trazabilidad.Models.Ayudante
{
    public class Cls_Jira
    {
        public enum IssuePriority{
            [Description("Highest")]
            Highest = 0,
            [Description("High")]
            High = 1,
            [Description("Medium")]
            Medium = 2,
            [Description("Low")]
            Low = 3,
            [Description("Lowest")]
            Lowest = 4
        }

        public enum IssueTypes
        {
            [Description("Task")]
            Task = 0,
            [Description("Bug")]
            Bug = 1,
            [Description("Epic")]
            Epic = 2,
            [Description("Story")]
            Story = 3
        }

        public static void Create_Issue(Exception e, string IssueType, string IssuePriority)
        {
            Apl_Cat_Parametros Parametro = null;
            Consultar_Parametros(ref Parametro);

            var Summary = "Genero bug: " + e.TargetSite + " Problema: " + e.Message + " Pila de errores: " + e.StackTrace;

            if (e.InnerException != null)
                Summary += " --> Bug interno: " + e.InnerException.TargetSite + " Problema: " + e.InnerException.Message + " Pila de errores: " + e.InnerException.StackTrace;

            var data = new web_trazabilidad.Models.Negocio.Issue();
            data.fields.project.key = Parametro.Name_Jira_Project;
            data.fields.summary = Summary;
            data.fields.description = Summary;
            data.fields.issuetype.name = IssueType;
            data.fields.priority.name = IssuePriority;

            string postUrl = Parametro.Url_Jira_Service;

            System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();
            client.BaseAddress = new System.Uri(postUrl);
            byte[] cred = UTF8Encoding.UTF8.GetBytes(Parametro.Usuario_Jira + ":" + Parametro.Password_Jira);
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(cred));
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            System.Net.Http.Formatting.MediaTypeFormatter jsonFormatter = new System.Net.Http.Formatting.JsonMediaTypeFormatter();
            System.Net.Http.HttpContent content = new System.Net.Http.ObjectContent<web_trazabilidad.Models.Negocio.Issue>(data, jsonFormatter);
            System.Net.Http.HttpResponseMessage response = client.PostAsync("issue", content).Result;

            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;
                Console.Write(result);
            }
            else
            {
                Console.Write(response.StatusCode.ToString());
                Console.ReadLine();
            }
        }

        private static void Consultar_Parametros(ref Apl_Cat_Parametros Parametro) {
            using (var dbContext = new Sistema_TrazabilidadEntities()) {
                Parametro = (from _p in dbContext.Apl_Cat_Parametros
                                  select _p).FirstOrDefault<Apl_Cat_Parametros>();
            }
        }

        public static String Descripcion_Referencia(Enum Referencia)
        {
            var Descripcion = new List<string>();
            var Tipo = Referencia.GetType();
            var Nombre = Enum.GetName(Tipo, Referencia);
            var Campo = Tipo.GetField(Nombre);
            var Campo_Descripcion = Campo.GetCustomAttributes(typeof(DescriptionAttribute), true);
            foreach (DescriptionAttribute Atributo in Campo_Descripcion)
            {
                Descripcion.Add(Atributo.Description);
            }
            return Descripcion.ElementAt(0);
        }
    }
}