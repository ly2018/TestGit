using System;
using System.Collections.Generic;
using System.Text;

namespace Vic.Core.Utils.Models
{
    public class EchartModel
    {
    }
    public class Title
    {
        /// <summary>
        /// 年度用户注册统计
        /// </summary>
        public string text { get; set; }
        /// <summary>
        /// 月/人
        /// </summary>
        public string subtext { get; set; }
    }

    public class Tooltip
    {
        /// <summary>
        /// axis
        /// </summary>
        public string trigger { get; set; }
    }

    public class Legend
    {
        /// <summary>
        /// Data
        /// </summary>
        public List<string> data { get; set; }
        public string x { get { return "center"; } }
        public string y { get { return "bottom"; } }
    }

    public class Mark
    {
        /// <summary>
        /// Show
        /// </summary>
        public bool show { get; set; }
    }

    public class DataView
    {
        /// <summary>
        /// Show
        /// </summary>
        public bool show { get; set; }
        /// <summary>
        /// ReadOnly
        /// </summary>
        public bool readOnly { get; set; }
    }

    public class MagicType
    {
        /// <summary>
        /// Show
        /// </summary>
        public bool show { get; set; }
        /// <summary>
        /// Type
        /// </summary>
        public List<string> type { get; set; }
    }

    public class Restore
    {
        /// <summary>
        /// Show
        /// </summary>
        public bool show { get; set; }
    }

    public class SaveAsImage
    {
        /// <summary>
        /// Show
        /// </summary>
        public bool show { get; set; }
    }

    public class Feature
    {
        /// <summary>
        /// Mark
        /// </summary>
        public Mark mark { get; set; }
        /// <summary>
        /// DataView
        /// </summary>
        public DataView dataView { get; set; }
        /// <summary>
        /// MagicType
        /// </summary>
        public MagicType magicType { get; set; }
        /// <summary>
        /// Restore
        /// </summary>
        public Restore restore { get; set; }
        /// <summary>
        /// SaveAsImage
        /// </summary>
        public SaveAsImage saveAsImage { get; set; }
    }

    public class Toolbox
    {
        /// <summary>
        /// Show
        /// </summary>
        public bool show { get; set; }
        /// <summary>
        /// Feature
        /// </summary>
        public Feature feature { get; set; }
    }

    public class XAxis
    {
        /// <summary>
        /// category
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// Data
        /// </summary>
        public List<string> data { get; set; }
    }

    public class YAxis
    {
        /// <summary>
        /// value
        /// </summary>
        public string type { get; set; }
    }

    public class MaxMinData
    {
        /// <summary>
        /// max
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// 最大值
        /// </summary>
        public string name { get; set; }
    }

    public class MarkPoint
    {
        /// <summary>
        /// Data
        /// </summary>
        public List<MaxMinData> data { get; set; }
    }

    public class AverageData
    {
        /// <summary>
        /// average
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// 平均值
        /// </summary>
        public string name { get; set; }
    }

    public class MarkLine
    {
        /// <summary>
        /// Data
        /// </summary>
        public List<AverageData> data { get; set; }
    }

    public class Series
    {
        /// <summary>
        /// 注册人数
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// bar
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// Data
        /// </summary>
        public List<string> data { get; set; }
        /// <summary>
        /// MarkPoint
        /// </summary>
        public MarkPoint markPoint { get; set; }
        /// <summary>
        /// MarkLine
        /// </summary>
        public MarkLine markLine { get; set; }
    }

    /// <summary>
    /// Echarts标准柱状图模型
    /// </summary>
    public class Root
    {
        /// <summary>
        /// Title
        /// </summary>
        public Title title { get; set; }
        /// <summary>
        /// Tooltip
        /// </summary>
        public Tooltip tooltip { get; set; }
        /// <summary>
        /// Legend
        /// </summary>
        public Legend legend { get; set; }
        /// <summary>
        /// Toolbox
        /// </summary>
        public Toolbox toolbox { get; set; }
        /// <summary>
        /// Calculable
        /// </summary>
        public bool calculable { get; set; }
        /// <summary>
        /// XAxis
        /// </summary>
        public List<XAxis> xAxis { get; set; }
        /// <summary>
        /// YAxis
        /// </summary>
        public List<YAxis> yAxis { get; set; }
        /// <summary>
        /// Series
        /// </summary>
        public List<Series> series { get; set; }
    }
}