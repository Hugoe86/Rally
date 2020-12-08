using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_trazabilidad.Models.Negocio
{
    public class Cls_Column_Table
    {
        public string TableName { get; set; }
        public string ColumnName { get; set; }
        public string ColumnValue { get; set; }
        public string DataType { get; set; }
        public bool IsIdentity { get; set; }
        public bool IsNullable { get; set; }
        public int MaxLength { get; set; }
        public bool IsForeignKey
        {
            get
            {
                return string.IsNullOrEmpty(this.ReferenceColumnName) ? false : true;
            }
        }
        public string ReferenceTableName { get; set; }
        public string ReferenceColumnName { get; set; }
        public bool IsSelected { get; set; }
        public bool IsUnique { get; set; }
        public bool IsReferenceSelected { get; set; }
        public Cls_Column_Table ReferenceSelected { get; set; }
        public string Encabezado { get; set; }
        public bool IsValid { get; set; }
        public string Message { get; set; }
        public bool IsExist { get; set; }
    }
}