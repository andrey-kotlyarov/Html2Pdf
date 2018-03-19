using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;



namespace Html2Pdf.HParser
{
    public static partial class HUtil
    {
        public static class EnumUtil
        {
            public static string GetEnumDescription(Enum value)
            {
                FieldInfo fi = value.GetType().GetField(value.ToString());

                DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attributes != null && attributes.Length > 0)
                {
                    return attributes[0].Description;
                }
                else
                {
                    return value.ToString();
                }
            }

            public static IEnumerable<T> EnumToList<T>()
            {
                Type enumType = typeof(T);
                
                if (enumType.BaseType != typeof(Enum)) throw new ArgumentException("T must be of type System.Enum");

                Array enumValArray = Enum.GetValues(enumType);
                List<T> enumValList = new List<T>(enumValArray.Length);

                foreach (int val in enumValArray)
                {
                    enumValList.Add((T)Enum.Parse(enumType, val.ToString()));
                }

                return enumValList;
            }




            public static string GetTagNameByTagType(HTagType tagType)
            {
                return GetEnumDescription(tagType);
            }

            public static string GetStyleNameByStyleEnum(HStyleType styleType)
            {
                return GetEnumDescription(styleType);
            }

            public static HTagType GetTagTypeByTagName(string tagName)
            {
                HTagType? tagEnum = null;

                foreach (HTagType te in EnumToList<HTagType>())
                {
                    if (tagName.ToLower() == GetEnumDescription(te))
                    {
                        tagEnum = te;
                        break;
                    }
                }

                if (!tagEnum.HasValue)
                {
                    tagEnum = HTagType._unknown;
                }

                return tagEnum.Value;
            }

            public static HStyleType GetStyleTypeByStyleName(string styleName)
            {
                HStyleType? styleEnum = null;

                foreach (HStyleType se in EnumToList<HStyleType>())
                {
                    if (styleName.ToLower() == GetEnumDescription(se))
                    {
                        styleEnum = se;
                        break;
                    }
                }

                if (!styleEnum.HasValue)
                {
                    styleEnum = HStyleType._unknown;
                }

                return styleEnum.Value;
            }
        }
        
    }
}
