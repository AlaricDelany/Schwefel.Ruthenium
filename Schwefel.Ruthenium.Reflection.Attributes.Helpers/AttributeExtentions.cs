using System;
using System.Linq.Expressions;

namespace Schwefel.Ruthenium.Reflection.Attributes.Helpers
{
    public static class AttributeExtentions
    {
        public static TResult GetAttributeValue<TAttributeType, TResult>(this Type typeOfClassWithAttribute,
            Func<TAttributeType, TResult> getAttributeValue)
            where TAttributeType : Attribute
        {
            return AttributeHelper.GetAttributeValue(typeOfClassWithAttribute, getAttributeValue);
        }

        public static TResult GetAttributeValue<TAttributeType, TClassWithAttribute, TResult>(this TClassWithAttribute self,
            Expression<Func<TClassWithAttribute, object>> getMember, Func<TAttributeType, TResult> getAttributeValue)
            where TAttributeType : Attribute
            where TClassWithAttribute : class
        {
            return AttributeHelper.GetAttributeValue(getMember, getAttributeValue);
        }
    }
}