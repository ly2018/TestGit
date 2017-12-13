using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Vic.Core.Utils
{
    public static class LambdaCommon
    {
        #region 表达式工具
        /// <summary>
        /// 相当于&&操作
        /// ——Author：hellthinker
        /// </summary>
        /// <param name="thisFilter">已生成的过滤条件</param>
        /// <param name="otherFilter">未生成的过滤条件</param>
        /// <returns>新的过滤</returns>
        public static Expression GotoAndAlso(this Expression thisFilter, Expression otherFilter)
        {
            return Expression.AndAlso(thisFilter, otherFilter);
        }
        /// <summary>
        /// 相当于||操作
        /// ——Author：hellthinker
        /// </summary>
        /// <param name="thisFilter">已生成的过滤条件</param>
        /// <param name="otherFilter">未生成的过滤条件</param>
        /// <returns>新的过滤</returns>
        public static Expression GotoOrElse(this Expression thisFilter, Expression otherFilter)
        {
            return Expression.OrElse(thisFilter, otherFilter);
        }
        /// <summary>
        /// 相当于==操作
        /// ——Author：hellthinker
        /// </summary>
        /// <param name="thisParameterExpression">查询对象</param>
        /// <param name="propertieName">属性名称</param>
        /// <param name="propertieValue">属性值</param>
        /// <returns>新的过滤</returns>
        public static Expression GotoEqual(this ParameterExpression thisParameterExpression, string propertieName, object propertieValue)
        {
            return Expression.Equal(Expression.Property(thisParameterExpression, propertieName), Expression.Constant(propertieValue));
        }
        /// <summary>
        /// 相当于!=操作
        /// ——Author：hellthinker
        /// </summary>
        /// <param name="thisParameterExpression">查询对象</param>
        /// <param name="propertieName">属性名称</param>
        /// <param name="propertieValue">属性值</param>
        /// <returns>新的过滤</returns>
        public static Expression GotoNotEqual(this ParameterExpression thisParameterExpression, string propertieName, object propertieValue)
        {
            return Expression.NotEqual(Expression.Property(thisParameterExpression, propertieName), Expression.Constant(propertieValue));
        }

        /// <summary>
        /// 相当于>=操作
        /// ——Author：hellthinker
        /// </summary>
        /// <param name="thisParameterExpression">查询对象</param>
        /// <param name="propertieName">属性名称</param>
        /// <param name="propertieValue">属性值</param>
        /// <returns>新的过滤</returns>
        public static Expression GotoGreaterThanOrEqual(this ParameterExpression thisParameterExpression, string propertieName, object propertieValue)
        {
            //大于或等于
            return Expression.GreaterThanOrEqual(Expression.Property(thisParameterExpression, propertieName), Expression.Constant(propertieValue));
        }
        /// <summary>
        /// 相当于小于等于操作
        /// ——Author：hellthinker
        /// </summary>
        /// <param name="thisParameterExpression">查询对象</param>
        /// <param name="propertieName">属性名称</param>
        /// <param name="propertieValue">属性值</param>
        /// <returns>新的过滤</returns>
        public static Expression GotoLessThanOrEqual<T>(this ParameterExpression thisParameterExpression, string propertieName, object propertieValue)
        {
            //小于或等于
            return Expression.LessThanOrEqual(Expression.Property(thisParameterExpression, propertieName), Expression.Constant(propertieValue, typeof(T)));
        }
        /// <summary>
        /// 相当于>操作
        /// ——Author：hellthinker
        /// </summary>
        /// <param name="thisParameterExpression">查询对象</param>
        /// <param name="propertieName">属性名称</param>
        /// <param name="propertieValue">属性值</param>
        /// <returns>新的过滤</returns>
        public static Expression GotoGreaterThan<T>(this ParameterExpression thisParameterExpression, string propertieName, object propertieValue)
        {
            //大于
            return Expression.GreaterThan(Expression.Property(thisParameterExpression, propertieName), Expression.Constant(propertieValue, typeof(T)));
        }
        /// <summary>
        /// 相当于小于操作
        /// ——Author：hellthinker
        /// </summary>
        /// <param name="thisParameterExpression">查询对象</param>
        /// <param name="propertieName">属性名称</param>
        /// <param name="propertieValue">属性值</param>
        /// <returns>新的过滤</returns>
        public static Expression GotoLessThan<T>(this ParameterExpression thisParameterExpression, string propertieName, object propertieValue)
        {
            //小于
            return Expression.LessThan(Expression.Property(thisParameterExpression, propertieName), Expression.Constant(propertieValue, typeof(T)));
        }
        /// <summary>
        /// 相当于>=操作
        /// ——Author：hellthinker
        /// </summary>
        /// <param name="thisParameterExpression">查询对象</param>
        /// <param name="propertieName">属性名称</param>
        /// <param name="propertieValue">属性值</param>
        /// <returns>新的过滤</returns>
        public static Expression GotoGreaterThanOrEqualByDateTime(this ParameterExpression thisParameterExpression, string propertieName, object propertieValue)
        {
            //大于或等于
            return Expression.GreaterThanOrEqual(Expression.Property(thisParameterExpression, propertieName), Expression.Constant(propertieValue, propertieValue.GetType()));
        }
        /// <summary>
        /// 字符串包含
        /// </summary>
        /// <param name="thisParameterExpression">查询对象</param>
        /// <param name="propertieName">属性名称</param>
        /// <param name="propertieValue">属性值</param>
        /// <returns>新的过滤</returns>
        public static Expression GotoContains(this ParameterExpression thisParameterExpression, string propertieName, object propertieValue)
        {
            return Expression.Call(Expression.Property(thisParameterExpression, propertieName), typeof(string).GetMethod("Contains"), Expression.Constant(propertieValue));
        }


        /// <summary>
        /// 相当于小于或等于操作
        /// ——Author：hellthinker
        /// </summary>
        /// <param name="thisParameterExpression">查询对象</param>
        /// <param name="propertieName">属性名称</param>
        /// <param name="propertieValue">属性值</param>
        /// <returns>新的过滤</returns>
        public static Expression GotoLessThanOrEqualByDateTime(this ParameterExpression thisParameterExpression, string propertieName, object propertieValue)
        {
            //小于或等于
            return Expression.LessThanOrEqual(Expression.Property(thisParameterExpression, propertieName), Expression.Constant(propertieValue, propertieValue.GetType()));
        }
        /// <summary>
        /// 相当于>操作
        /// ——Author：hellthinker
        /// </summary>
        /// <param name="thisParameterExpression">查询对象</param>
        /// <param name="propertieName">属性名称</param>
        /// <param name="propertieValue">属性值</param>
        /// <returns>新的过滤</returns>
        public static Expression GotoGreaterThanByDateTime(this ParameterExpression thisParameterExpression, string propertieName, object propertieValue)
        {
            //大于
            return Expression.GreaterThan(Expression.Property(thisParameterExpression, propertieName), Expression.Constant(propertieValue, propertieValue.GetType()));
        }
        /// <summary>
        /// 相当于小于操作
        /// ——Author：hellthinker
        /// </summary>
        /// <param name="thisParameterExpression">查询对象</param>
        /// <param name="propertieName">属性名称</param>
        /// <param name="propertieValue">属性值</param>
        /// <returns>新的过滤</returns>
        public static Expression GotoLessThanByDateTime(this ParameterExpression thisParameterExpression, string propertieName, object propertieValue)
        {
            //小于
            return Expression.LessThan(Expression.Property(thisParameterExpression, propertieName), Expression.Constant(propertieValue, propertieValue.GetType()));
        }
        /// <summary> 
        /// 包含操作 相当余 a=> arr.Contains(a.ID) 
        /// </summary> 
        /// <param name="thisParameterExpression"></param> 
        /// <param name="propertieName"></param> 
        /// <param name="propertieValue"></param> 
        /// <returns></returns> 
        public static Expression ContainsOperations(this ParameterExpression thisParameterExpression, string propertieName, object propertieValue)
        {
            MethodInfo method = null;
            MemberExpression member = Expression.Property(thisParameterExpression, propertieName);
            var containsMethods = typeof(Enumerable).GetMethods(BindingFlags.Static | BindingFlags.Public).Where(m => m.Name == "Contains");
            foreach (var m in containsMethods)
            {
                if (m.GetParameters().Count() == 2)
                {
                    method = m;
                    break;
                }
            }
            method = method.MakeGenericMethod(member.Type);
            var exprContains = Expression.Call(method, new Expression[] { Expression.Constant(propertieValue), member });
            return exprContains;
        }


        /// <summary>
        /// 包含操作 相当于  a=>a.ID.Contains(key)
        /// </summary>
        /// <param name="thisParameterExpression"></param>
        /// <param name="propertieName"></param>
        /// <param name="propertieValue"></param>
        /// <returns></returns>
        public static Expression Contains(this ParameterExpression thisParameterExpression, string propertieName, object propertieValue)
        {
            var propertyExp = Expression.Property(thisParameterExpression, propertieName);
            MethodInfo method = typeof(string).GetMethod("Contains", new[] { typeof(string) });
            var someValue = Expression.Constant(propertieValue, typeof(string));
            var containsMethodExp = Expression.Call(propertyExp, method, someValue);
            return containsMethodExp;
        }
        #endregion
    }
}