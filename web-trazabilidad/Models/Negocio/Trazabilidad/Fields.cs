using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_trazabilidad.Models.Negocio
{
    public class Fields
    {
        public Project project { get; set; }
        public string summary { get; set; }
        public string description { get; set; }
        public IssueType issuetype { get; set; }
        public IssuePriority priority { get; set; }
        public Fields()
        {
            project = new Project();
            issuetype = new IssueType();
            priority = new IssuePriority();
        }
    }
}