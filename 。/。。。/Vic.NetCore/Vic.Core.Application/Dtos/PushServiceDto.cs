using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Vic.Core.Application.Dtos
{
    public class PushServiceDto : Vic.Core.Application.Dtos.BaseDto
    {
        		/// <summary>
		///IP地址
		/// </summary>
		public string IP { get; set; }
		/// <summary>
		///端口
		/// </summary>
		public int Port { get; set; }

    }
}
