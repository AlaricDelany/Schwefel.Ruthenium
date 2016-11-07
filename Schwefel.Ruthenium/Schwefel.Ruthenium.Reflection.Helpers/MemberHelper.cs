using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Schwefel.Ruthenium.Reflection.Helpers
{
    public static class MemberHelper
    {
        public static MemberInfo GetMemberInfo(Expression expression)
        {
            LambdaExpression lLambdaExpression = expression as LambdaExpression;

            if(lLambdaExpression == null)
                throw new ArgumentNullException(nameof(lLambdaExpression));

            MemberExpression lMemberExpression = null;

            if(lLambdaExpression.Body.NodeType == ExpressionType.Convert)
            {
                lMemberExpression = ((UnaryExpression)lLambdaExpression.Body).Operand as MemberExpression;
            }
            else if(lLambdaExpression.Body.NodeType == ExpressionType.MemberAccess)
            {
                lMemberExpression = lLambdaExpression.Body as MemberExpression;
            }
            MemberInfo lMemberInfo = lMemberExpression?.Member;

            return lMemberInfo;
        }

        public static IDictionary<string, object> ToDictionary<TTarget>(this TTarget self)
        {
            PropertyInfo[] lProperties = typeof(TTarget).GetTypeInfo().DeclaredProperties.ToArray();
            Dictionary<string, object> lCurrentEntryDic = new Dictionary<string, object>(lProperties.Count());

            foreach(PropertyInfo lProperty in lProperties)
            {
                lCurrentEntryDic.Add(lProperty.Name, lProperty.GetValue(self));
            }
            return lCurrentEntryDic;
        }

        public static TTarget ToObject<TTarget>(this IDictionary<string, object> input)
            where TTarget : new()
        {
            TTarget lResult = new TTarget();

            PropertyInfo[] lProperties = typeof(TTarget).GetTypeInfo().DeclaredProperties.ToArray();
            foreach(PropertyInfo lProperty in lProperties)
            {
                lProperty.SetValue(lResult, input[lProperty.Name]);
            }

            return lResult;
        }
    }
}