using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PhoneSelling.Common
{
    public static class EnumHelper
    {
        public static string GetEnumMemberValue<T>(T enumValue) where T : Enum
        {
            var enumType = typeof(T);
            var memberInfo = enumType.GetMember(enumValue.ToString());

            if (memberInfo.Length > 0)
            {
                var attr = memberInfo[0].GetCustomAttribute<EnumMemberAttribute>();
                if (attr != null)
                {
                    return attr.Value; // ✅ Returns "asc" or "desc"
                }
            }

            return enumValue.ToString(); // Fallback to default name
        }
    }
}
