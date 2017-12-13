using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vic.Core.Utils.WebMethod
{
    public enum HttpDataFormartEnum
    {
        /// <summary>
        ///Json格式
        /// </summary>
        [Description("Json格式")]
        Formart_Json = 1,
        /// <summary>
        /// Form格式
        /// </summary>
        [Description("Form格式")]
        Formart_Form = 2
    }
}
