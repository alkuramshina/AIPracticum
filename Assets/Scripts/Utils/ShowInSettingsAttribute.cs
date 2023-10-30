using System;

namespace Utils
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
}