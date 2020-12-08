using DocumentFormat.OpenXml.Vml.Office;
using Microsoft.Scripting.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_trazabilidad.Models.Ayudante.Util
{
    public static class TypeUtil
    {
        /// <summary>
        ///     The numeric types
        /// </summary>
        private static readonly HashSet<Type> NumericTypes = new HashSet<Type>()
        {
            typeof(byte),
            typeof(sbyte),
            typeof(int),
            typeof(uint),
            typeof(UInt16),
            typeof(UInt32),
            typeof(UInt64),
            typeof(Int16),
            typeof(Int32),
            typeof(Int64),
            typeof(long),
            typeof(ulong),
            typeof(short),
            typeof(ushort),
            typeof(float),
            typeof(Single),
            typeof(double),
            typeof(Double),
            typeof(decimal),
            typeof(Decimal),
		    // Support for .Net types
		    typeof(BigInteger),
            typeof(Complex)
        };

        /// <summary>
        ///     Checks if the type is numeric.
        /// </summary>
        /// <param name="type">The type</param>
        /// <returns>True if the type is numeric</returns>
        public static bool IsNumeric(this Type type)
        {
            // Use the underlying type for nullable numeric types
            while (!NumericTypes.Contains(type) &&
                Type.GetTypeCode(type) == TypeCode.Object &&
                type.IsGenericType &&
                type.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                type = Nullable.GetUnderlyingType(type);
            }

            return NumericTypes.Contains(type);
        }

        /// <summary>
        ///     Determines if an object is of a numeric type.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns>True if the object's type is numeric</returns>
        public static bool IsNumericType(this object obj)
            => obj != null && obj.GetType().IsNumeric();
    }
}