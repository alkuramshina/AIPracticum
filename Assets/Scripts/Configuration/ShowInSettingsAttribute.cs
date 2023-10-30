using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;

namespace Configuration
{
    [AttributeUsage(AttributeTargets.Field)]
    public class ShowInSettingsAttribute : Attribute
    {
        public ShowInSettingsAttribute(string title)
        {
            Title = title;
        }
        
        public string Title { get; set; }
    }

    public static class ReflectionExtensions
    {
        public static IEnumerable<(string, string)> GetFieldsToShowInSettings(this Type type, object objectValue)
        {
            var resultingFields = new List<(string, string)>(); 
        
            var fieldsToShow = type.GetFields()
                .Where(f => f.HasAttribute<ShowInSettingsAttribute>());

            foreach (var fieldInfo in fieldsToShow)
            {
                if (fieldInfo.FieldType.IsClass || fieldInfo.FieldType.IsStruct())
                {
                    var subFields = fieldInfo.FieldType.GetFields()
                        .Where(ff => ff.HasAttribute<ShowInSettingsAttribute>());

                    resultingFields.AddRange(subFields.Select(subField =>
                        new ValueTuple<string, string>(
                            $"{fieldInfo.GetAttribute<ShowInSettingsAttribute>().Title}: {subField.GetAttribute<ShowInSettingsAttribute>().Title}",
                            $"{subField.GetValue(fieldInfo.GetValue(objectValue))}")));
                }
                else
                {
                    resultingFields.Add(new ValueTuple<string, string>(
                        fieldInfo.GetAttribute<ShowInSettingsAttribute>().Title,
                        $"{fieldInfo.GetValue(objectValue)}"));
                }
            }

            return resultingFields;
        }
    }
}