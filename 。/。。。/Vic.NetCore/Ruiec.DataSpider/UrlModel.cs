using System;
using System.Collections.Generic;
using System.Text;

namespace Ruiec.DataSpider
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

    public class Root
    {
        public List<ParentModel> items { get; set; }
    }
}
