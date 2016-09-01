using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace EasyLOB.Data
{
    public static class DataTableExtensions
    {
        /// <summary>
        /// Convert List to DataTable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        /// <remarks>
        /// Conversion Between DataTable and List in C#
        /// http://www.codeproject.com/Tips/784090/Conversion-Between-DataTable-and-List-in-Csharp
        /// References: System.Data.DataSetExtensions
        /// </remarks>
        public static DataTable ToDataTable<T>(this IList<T> list)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);
            PropertyInfo[] propertyInfos = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                dataTable.Columns.Add(propertyInfo.Name, Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType);
            }

            foreach (T t in list)
            {
                var values = new object[propertyInfos.Length];
                for (int i = 0; i < propertyInfos.Length; i++)
                {
                    values[i] = propertyInfos[i].GetValue(t, null);
                }
                dataTable.Rows.Add(values);
            }

            return dataTable;
        }

        /// <summary>
        /// Convert DataTable to List
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataTable"></param>
        /// <returns></returns>
        /// <remarks>
        /// Conversion Between DataTable and List in C#
        /// http://www.codeproject.com/Tips/784090/Conversion-Between-DataTable-and-List-in-Csharp
        /// References: System.Data.DataSetExtensions
        /// </remarks>
        public static List<T> ToList<T>(this DataTable dataTable)
            where T : new()
        {
            var list = new List<T>();

            const BindingFlags flags = BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic;

            var objectFieldNames = (
                from PropertyInfo propertyInfo in typeof(T).GetProperties(flags)
                select new
                {
                    Name = propertyInfo.Name,
                    Type = Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType
                }
            ).ToList();

            var dataTableFieldNames = (
                from DataColumn aHeader in dataTable.Columns
                select new
                {
                    Name = aHeader.ColumnName,
                    Type = aHeader.DataType
                }
            ).ToList();

            var commonFields = objectFieldNames.Intersect(dataTableFieldNames).ToList();

            foreach (DataRow dataRow in dataTable.AsEnumerable().ToList())
            {
                var t = new T();

                foreach (var aField in commonFields)
                {
                    PropertyInfo propertyInfos = t.GetType().GetProperty(aField.Name);
                    var value = (dataRow[aField.Name] == DBNull.Value) ? null : dataRow[aField.Name]; // database field is nullable
                    propertyInfos.SetValue(t, value, null);
                }

                list.Add(t);
            }

            return list;
        }
    }
}