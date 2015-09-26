namespace MetaMind.Engine.Extensions
{
    using System;
    using System.Linq.Expressions;

    public static class FuncExtension
    {
        public static Expression<Func<T>> ToExpression<T>(this Func<T> func)
        {
            return Expression.Lambda<Func<T>>(Expression.Call(func.Method));
        }
    }
}