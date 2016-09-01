using EasyLOB.Library;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EasyLOB.Data
{
    /// <summary>
    /// Data Helper
    /// </summary>
    public static partial class DataHelper
    {
        #region Methods

        /// <summary>
        /// Get data dictionary attribute
        /// </summary>
        /// <param name="type">Entity type</param>
        /// <returns>Class Dictionary attribute</returns>
        public static ZDataDictionaryAttribute GetDataDictionaryAttribute(Type type)
        {
            foreach (Attribute attribute in type.GetCustomAttributes(true))
            {
                ZDataDictionaryAttribute dataDictionaryAttribute = attribute as ZDataDictionaryAttribute;
                if (dataDictionaryAttribute != null)
                {
                    return dataDictionaryAttribute;
                }
            }

            return new ZDataDictionaryAttribute(null, null, false, null, null, null, null);
        }

        public static bool TryValidate(object @object, ICollection<ValidationResult> results)
        {
            var context = new ValidationContext(@object, serviceProvider: null, items: null);

            return Validator.TryValidateObject(@object, context, results, validateAllProperties: true);
        }

        #endregion Methods

        #region Methods IdTo...

        public static byte IdToByte(object value)
        {
            if (value is int)
            {
                return (byte)value;
            }
            else if (value is Decimal)
            {
                return Decimal.ToByte((decimal)value);
            }
            else if (value is int)
            {
                return (byte)value;
            }
            else if (value is long)
            {
                return (byte)value;
            }
            else if (value is short)
            {
                return (byte)value;
            }
            else
            {
                return LibraryDefaults.Default_Byte;
            }
        }

        public static DateTime IdToDateTime(object value)
        {
            if (value is DateTime)
            {
                return (DateTime)value;
            }
            else
            {
                return LibraryDefaults.Default_DateTime;
            }
        }

        public static Guid IdToGuid(object value)
        {
            if (value is Guid)
            {
                return (Guid)value;
            }
            else
            {
                return LibraryDefaults.Default_Guid;
            }
        }

        public static short IdToInt16(object value)
        {
            if (value is Decimal)
            {
                return Decimal.ToInt16((decimal)value);
            }
            else if (value is int)
            {
                return (short)value;
            }
            else if (value is long)
            {
                return (short)value;
            }
            else if (value is short)
            {
                return (short)value;
            }
            else
            {
                return LibraryDefaults.Default_Int16;
            }
        }

        public static int IdToInt32(object value)
        {
            if (value is Decimal)
            {
                return Decimal.ToInt32((decimal)value);
            }
            else if (value is int)
            {
                return Convert.ToInt32(value);
            }
            else if (value is long)
            {
                return Convert.ToInt32(value);
            }
            else if (value is short)
            {
                return Convert.ToInt32(value);
            }
            else
            {
                return LibraryDefaults.Default_Int32;
            }
        }

        public static long IdToInt64(object value)
        {
            if (value is Decimal)
            {
                return Decimal.ToInt64((decimal)value);
            }
            else if (value is int)
            {
                return (long)value;
            }
            else if (value is long)
            {
                return (long)value;
            }
            else if (value is short)
            {
                return (long)value;
            }
            else
            {
                return LibraryDefaults.Default_Int64;
            }
        }

        public static string IdToString(object value)
        {
            if (value is string)
            {
                return (string)value;
            }
            else
            {
                return LibraryDefaults.Default_String;
            }
        }

        #endregion Methods IdTo...
    }
}