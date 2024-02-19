﻿using Entities.Entities;
using FastMember;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CommonUtilities
{
    public static class Extensions
    {
        public static DataTable GetDataTableFromUniversities(IEnumerable<University> universities)
        {
            DataTable table = new DataTable();
            using (var reader = ObjectReader.Create(universities))
            {
                table.Load(reader);
            }
            return table;
        }

        public static DataTable ToDataTable<T>(IEnumerable<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Defining type of data column gives proper data table 
                var type = (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType);
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name, type);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }



        public static List<T> ToList<T>(this DataTable table) where T : new()
        {
            IList<PropertyInfo> properties = typeof(T).GetProperties().ToList();
            List<T> result = new List<T>();

            foreach (var row in table.Rows)
            {
                var item = CreateItemFromRow<T>((DataRow)row, properties);
                result.Add(item);
            }

            return result;
        }

        private static T CreateItemFromRow<T>(DataRow row, IList<PropertyInfo> properties) where T : new()
        {
            T item = new T();
            foreach (var property in properties)
            {
                try
                {
                    if (property.PropertyType == typeof(System.DayOfWeek))
                    {
                        DayOfWeek day = (DayOfWeek)Enum.Parse(typeof(DayOfWeek), row[property.Name].ToString());
                        property.SetValue(item, day, null);
                    }
                    else if (property.PropertyType == typeof(System.Boolean))
                    {
                        bool isTrue = row[property.Name].ToString() == "1" || row[property.Name].ToString() == "true";
                        property.SetValue(item, isTrue, null);
                    }
                    else
                    {
                        if (row[property.Name] == DBNull.Value)
                            property.SetValue(item, null, null);
                        else
                        {
                            if (Nullable.GetUnderlyingType(property.PropertyType) != null)
                            {
                                //nullable
                                object convertedValue = null;
                                try
                                {
                                    convertedValue = System.Convert.ChangeType(row[property.Name], Nullable.GetUnderlyingType(property.PropertyType));
                                }
                                catch (Exception ex)
                                {
                                }
                                property.SetValue(item, convertedValue, null);
                            }
                            else
                                property.SetValue(item, row[property.Name], null);
                        }
                    }

                }
                catch (Exception ex)
                {


                }

            }
            return item;
        }
    }
}
