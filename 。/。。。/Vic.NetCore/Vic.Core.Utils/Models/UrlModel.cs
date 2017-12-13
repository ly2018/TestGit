using System;
using System.Collections.Generic;
using System.Text;

namespace Vic.Core.Utils.Models
{
    public class baseModel
    {
        public string homepage { get; set; }
        public string tocHref { get; set; }
        public string toc_title { get; set; }
        public string href { get; set; }
    }

    public class ParentModel : baseModel
    {
        public List<baseModel> children { get; set; }
    }

    public class RootDoc
    {
        public List<ParentModel> items { get; set; }
    }
}
