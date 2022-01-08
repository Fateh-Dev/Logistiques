using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using System.Linq;
using System.Reflection;
using System.Resources;
using AbpDz.Models;

namespace AbpCompanyName.AbpProjectName
{
    static public class AbpEnumsHelper
    {
        static public AbpDzEnum Root<T>(string display)
        {
            var x = typeof(T);
            var lst = Enums<T>();
            var data = GetAbpEnum(x.Name, display, x.Name, x.GetCustomAttribute<DisplayAttribute>(), x.GetCustomAttribute<DescriptionAttribute>(), x.CustomAttributes);

            data.Childs = new HashSet<AbpDzEnum>(lst);
            return data;
        }


        static public List<AbpDzEnum> Enums<T>()
        {
            var type = typeof(T);

            var data = type.GetFields().
                Where(x => x.IsStatic)
                .Select(x =>
                {
                    try
                    {
                        return GetAbpEnum(x.Name, x.Name, x.GetRawConstantValue(), x.GetCustomAttribute<DisplayAttribute>(), x.GetCustomAttribute<DescriptionAttribute>(), x.CustomAttributes);
                    }
                    catch (System.Exception)
                    {

                        return null;
                    }
                }).Where(k => k != null)
                .OrderBy(x => x.Order)
                .ToList();
            return data;
        }
        public static AbpDzEnum GetAbpEnum(string Code, string Name, object Value, DisplayAttribute DisplayAttribute,
        DescriptionAttribute DescriptionAttribute, IEnumerable<CustomAttributeData> CustomAttributes)
        {
            return new AbpDzEnum()
            {
                Code = Code,
                Value = Value.ToString(),
                EntityType = GetTypeName(Value),
                Display = (DisplayAttribute?.ResourceType != null) ? new ResourceManager(DisplayAttribute?.ResourceType).GetString(DisplayAttribute.Name) : (DisplayAttribute.Name ?? Name),
                Description = DescriptionAttribute?.Description,
                Group = CustomAttributes.Where(c => c.NamedArguments.Where(n => n.MemberName == "GroupName").Any()).Any() ? DisplayAttribute.GroupName : "",
                Abbreviation = CustomAttributes.Where(c => c.NamedArguments.Where(n => n.MemberName == "ShortName").Any()).Any() ? DisplayAttribute.ShortName : "",
                Category = CustomAttributes.Where(c => c.NamedArguments.Where(n => n.MemberName == "Prompt").Any()).Any() ? DisplayAttribute.Prompt : "",
                Order = CustomAttributes.Where(c => c.NamedArguments.Where(n => n.MemberName == "Order").Any()).Any() ? DisplayAttribute.Order : 0
            };

        }
        static string GetTypeName(object o)
        {
            var t = o.GetType();
            if (t == typeof(int) || t == typeof(long) || t == typeof(short))
            {
                return "number";
            }
            if (typeof(Guid) == t)
            {
                return "guid";
            }
            return "text";
        }
        static public List<string> StringList<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<T>().Select(x => x.ToString()).ToList();
        }
        static public List<T> All<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<T>().ToList();
        }
        static public int Count<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<T>().Count();
        }
    }

}
