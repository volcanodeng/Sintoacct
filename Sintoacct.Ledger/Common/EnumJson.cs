using System;
using System.Collections.Generic;

namespace Sintoacct.Ledger.Common
{
    public class EnumJson
    {
        public string Name { get; set; }

        public int Value { get; set; }

        public static EnumJson[] Convert(Type enumType)
        {
            List<EnumJson> enums = new List<EnumJson>();
            string[] enumNames = Enum.GetNames(enumType);

            foreach (string en in enumNames)
            {
                EnumJson ej = new EnumJson();
                ej.Name = en;
                ej.Value = (int)Enum.Parse(enumType, en);
                enums.Add(ej);
            }

            return enums.ToArray();
        }
    }
}