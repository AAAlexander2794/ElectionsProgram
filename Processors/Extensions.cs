using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Data;
using System.Windows;
using ElectionsProgram.Processors;
using ElectionsProgram.Entities;

namespace ElectionsProgram.Processors
{
    public static class ReflectionExtensions
    {

        #region Получение аргумента атрибута

        // Использовать так:
        // string displayName = ReflectionExtensions.GetPropertyDisplayName<SomeClass>(i => i.SomeProperty);

        public static T GetAttribute<T>(this MemberInfo member, bool isRequired)
        where T : Attribute
        {
            var attribute = member.GetCustomAttributes(typeof(T), false).SingleOrDefault();

            if (attribute == null && isRequired)
            {
                throw new ArgumentException(
                    string.Format(
                        CultureInfo.InvariantCulture,
                        "The {0} attribute must be defined on member {1}",
                        typeof(T).Name,
                        member.Name));
            }

            return (T)attribute;
        }

        public static string GetPropertyDisplayName<T>(Expression<Func<T, object>> propertyExpression)
        {
            var memberInfo = GetPropertyInformation(propertyExpression.Body);
            if (memberInfo == null)
            {
                throw new ArgumentException(
                    "No property reference expression was found.",
                    "propertyExpression");
            }

            var attr = memberInfo.GetAttribute<DisplayNameAttribute>(false);
            if (attr == null)
            {
                return memberInfo.Name;
            }

            return attr.DisplayName;
        }

        public static MemberInfo GetPropertyInformation(System.Linq.Expressions.Expression propertyExpression)
        {
            Debug.Assert(propertyExpression != null, "propertyExpression != null");
            MemberExpression memberExpr = propertyExpression as MemberExpression;
            if (memberExpr == null)
            {
                UnaryExpression unaryExpr = propertyExpression as UnaryExpression;
                if (unaryExpr != null && unaryExpr.NodeType == ExpressionType.Convert)
                {
                    memberExpr = unaryExpr.Operand as MemberExpression;
                }
            }

            if (memberExpr != null && memberExpr.Member.MemberType == MemberTypes.Property)
            {
                return memberExpr.Member;
            }

            return null;
        }

        #endregion Получение аргумента атрибута
    }

    public static class DataTableExtensions
    {
        public static IList<T> ToList<T>(this DataTable table) where T : new()
        {
            IList<PropertyInfo> properties = typeof(T).GetProperties().ToList();
            //
            IList<T> result = new List<T>();

            foreach (var row in table.Rows)
            {
                var item = CreateItemFromRow<T>((DataRow)row, properties);
                result.Add(item);
            }

            return result;
        }

        /// <summary>
        /// Разносит строку <see cref="DataRow"/> по свойствам, ориентируясь на 
        /// <see cref="DisplayNameAttribute"/> или на имя свойства.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="row"></param>
        /// <param name="properties"></param>
        /// <returns></returns>
        private static T CreateItemFromRow<T>(DataRow row, IList<PropertyInfo> properties) where T : new()
        {
            //
            T item = new T();
            //
            foreach (var property in properties)
            {
                var some = property.GetCustomAttribute<DisplayNameAttribute>();
                // Если атрибут присвоен
                if (some != null)
                {
                    var fieldName = some.DisplayName;
                    // Если есть такой столбец
                    if (row.Table.Columns.Contains(fieldName))
                    {
                        // Если значение отсутствует
                        if (row[fieldName] is DBNull)
                        {
                            property.SetValue(item, "", null);
                        }
                        else
                        {
                            property.SetValue(item, row[fieldName], null);
                        }
                    }
                }
                else
                {
                    if (row.Table.Columns.Contains(property.Name))
                    {
                        property.SetValue(item, row[property.Name], null);
                    }
                }
            }
            return item;
        }
    }
}
