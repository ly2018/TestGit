using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vic.Core.Utils.LocalizedHelper
{
    public class ConfigValue
    {
        /// <summary>
        /// 默认语言
        /// </summary>
        public static string DefaultCulture = "zh-CN";

        public static HashSet<string> CultureSet
        {
            get
            {
                return new HashSet<string>() { "zh-CN", "en", "zh-TW" };
            }
        }
    }
}
