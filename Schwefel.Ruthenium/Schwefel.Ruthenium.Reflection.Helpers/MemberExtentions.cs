using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace Schwefel.Ruthenium.Reflection.Helpers
{
    public static class MemberExtentions
    {
        public static IDictionary<string, object> ToDictionary<TTarget>(this TTarget self)
        {
            return MemberHelper.ToDictionary(self);
        }

        public static TTarget ToObject<TTarget>(this IDictionary<string, object> input)
            where TTarget : new()
        {
            return MemberHelper.ToObject<TTarget>(input);
        }

        public static MemberInfo GetMemberInfo<TInstance>(this TInstance self, Expression<Func<TInstance, object>> getMember)
        {
            return MemberHelper.GetMemberInfo(getMember);
        }
    }
}
