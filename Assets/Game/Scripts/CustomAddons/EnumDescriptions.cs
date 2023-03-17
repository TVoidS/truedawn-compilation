using System;
using System.ComponentModel;

public static class EnumDescriptions
{
    public static string ToDiscriptionString(this Enum val) 
    {
        DescriptionAttribute[] attributes = (DescriptionAttribute[])val
           .GetType()
           .GetField(val.ToString())
           .GetCustomAttributes(typeof(DescriptionAttribute), false);
        return attributes.Length > 0 ? attributes[0].Description : string.Empty;
    }
}
