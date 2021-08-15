using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Framework.Extensions
{
    public static class  CollectionExtensions
    {
        public static bool IsExist<T>(this T value)
        {
            bool result = value == null || value.Equals(default(T));
            return !result;
        }
    }
}
