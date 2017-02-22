using System;
using System.Linq.Expressions;
using System.Reflection;
using Schwefel.Ruthenium.Reflection.Helpers;

namespace Schwefel.Ruthenium.Reflection.Attributes.Helpers
{
    public static class AttributeHelper
    {
        public static TResult GetAttributeValue<TAttributeType, TResult>(Type typeOfClassWithAttribute, Func<TAttributeType, TResult> getAttributeValue)
            where TAttributeType : Attribute
        {

            if(getAttributeValue == null)
                throw new ArgumentNullException(nameof(getAttributeValue));

            TAttributeType lAttribute = typeOfClassWithAttribute.GetTypeInfo().GetCustomAttribute<TAttributeType>();

            if(lAttribute == null)
                throw new InvalidOperationException(
                    $"No Attribute could be found for Type: {typeof(TAttributeType).Name}");

            TResult lResult = getAttributeValue(lAttribute);

            return lResult;
        }

        public static TResult GetAttributeValue<TAttributeType, TClassWithAttribute, TResult>(Func<TAttributeType, TResult> getAttributeValue)
            where TAttributeType : Attribute
            where TClassWithAttribute : class
        {
            TResult lResult = GetAttributeValue(typeof(TClassWithAttribute), getAttributeValue);

            return lResult;
        }

        public static TResult GetAttributeValue<TAttributeType, TClassWithAttribute, TResult>(Expression<Func<TClassWithAttribute, object>> getMember, Func<TAttributeType, TResult> getAttributeValue)
            where TAttributeType : Attribute
            where TClassWithAttribute : class
        {
            if(getMember == null)
                throw new ArgumentNullException(nameof(getMember));
            if(getAttributeValue == null)
                throw new ArgumentNullException(nameof(getAttributeValue));

            MemberInfo lMemberInfo = MemberHelper.GetMemberInfo(getMember);
            TAttributeType lAttribute = lMemberInfo.GetCustomAttribute<TAttributeType>();

            if(lAttribute == null)
                throw new InvalidOperationException(
                    $"No {typeof(TAttributeType).Name} Attribute could be found for Type: {typeof(TClassWithAttribute).Name}");

            TResult lResult = getAttributeValue(lAttribute);

            return lResult;
        }

    }
}
