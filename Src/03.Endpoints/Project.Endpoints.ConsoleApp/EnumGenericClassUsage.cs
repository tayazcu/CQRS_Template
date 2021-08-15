using Project.Core.Infrastructures.Identity;
using Project.Framework.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Endpoints.ConsoleApp
{
    public static class EnumGenericClassUsage
    {
        static void Usage()
        {
            // int to enum
            int enumValue = 1;
            Claims intToEnum = enumValue.ToEnum<Claims>();

            // string to enum
            string enumKey = "News";
            Claims stringToEnum = enumKey.ToEnum<Claims>();

            // enum to string
            Claims claim4 = Claims.News;
            string enumToString = claim4.ToString<Claims>();

            // enum to int
            Claims claim3 = Claims.News;
            int enumToInt = claim3.ToInt<Claims>();

            // get description from Enum
            var enumToDescription1 = claim3.ToDescription<Claims>();

            // get description from string
            var stringToDescription2 = enumKey.ToDescription<Claims>();

            // get description from Enum
            var enumToDisplay = claim3.ToDisplay();
        }
    }
}
