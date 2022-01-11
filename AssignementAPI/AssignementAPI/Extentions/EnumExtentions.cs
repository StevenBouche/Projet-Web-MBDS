﻿using System.ComponentModel;

namespace AssignmentAPI.Extentions
{
    public static class EnumExtentions
    {
        public static string ToDescriptionString(this Enum val)
        {
            var field = val.GetType().GetField(val.ToString());

            if (field == null)
                return string.Empty;

            DescriptionAttribute[] attributes = (DescriptionAttribute[]) field.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : string.Empty;
        }
    }
}
