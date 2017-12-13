using System;
using System.Collections.Generic;
using System.Text;

namespace Vic.Core.Utils.Models
{

    public class Home_ViewModel
    {
        public Home_ViewModel() { }
        public Home_ViewModel(int _CurrYear)
        {
            if (_CurrYear <= 0)
            {
                _CurrYear = DateTime.Now.Year;
            }
            this.CurrYear = _CurrYear;
            List<int> _YearList = new List<int>();
            for (int i = CurrYear - 10; i < CurrYear + 10; i++)
            {
                _YearList.Add(i);
            }
            this.YearList = _YearList;
        }
        /// <summary>
        /// 年份列表
        /// </summary>
        public List<int> YearList { get; set; }
        /// <summary>
        /// 当前年份
        /// </summary>
        public int CurrYear { get; set; }
        /// <summary>
        /// 货币列表
        /// </summary>
        public List<DicList> CurrencyList { get; set; }
        /// <summary>
        /// 当前货币
        /// </summary>
        public string CurrCurrency { get; set; }
    }
    /// <summary>
    /// 字典表
    /// </summary>
    public class DicList
    {
        public string key { get; set; }
        public string value { get; set; }
    }
    /// <summary>
    /// 虚拟币报表
    /// </summary>
    public class CoinReport_ViewModel : SearchDate
    {
        /// <summary>
        /// 拒绝提货总量
        /// </summary>
        public decimal RefuseRollOutAm { get; set; }

        /// <summary>
        /// 买入均价
        /// </summary>
        public decimal BuyAvgPrice { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string GoodsName { get; set; }
        /// <summary>
        /// 认购总量
        /// </summary>
        public decimal SubscribeTotal { get; set; }
        /// <summary>
        /// 承销商分配总量
        /// </summary>
        public decimal UnderWriterSubscribeTotal { get; set; }
        /// <summary>
        /// 当日提货总量
        /// </summary>
        public decimal DayRollOutNum { get; set; }
        /// <summary>
        /// 累计提货总量
        /// </summary>
        public decimal SumRollOutNum { get; set; }
        /// <summary>
        /// 委托冻结总数量
        /// </summary>
        public decimal FreezeAmt { get; set; }
        /// <summary>
        /// 提货冻结总数量
        /// </summary>
        public decimal PickingFreezeAmt { get; set; }
        /// <summary>
        /// 会员持有币数
        /// </summary>
        public decimal UseNum { get; set; }
        /// <summary>
        /// 用户持仓总数量
        /// </summary>
        public decimal TotalNum { get; set; }
        /// <summary>
        /// 列表
        /// </summary>
        public List<CoinReport_ViewModel> CoinReportList { get; set; }
        /// <summary>
        /// 委托冻结总数量
        /// </summary>
        public int DelegateNum { get; set; }
    }
    /// <summary>
    /// 日期搜索
    /// </summary>
    public class SearchDate
    {
        public DateTime? BeginTime { get; set; }
        public DateTime? EndTime { get; set; }
    }
    /// <summary>
    /// 货币报表
    /// </summary>
    public class CurrencyReport_ViewModel : SearchDate
    {
        public decimal WithdrawFreezeAmt { get; set; }

        public decimal RollOutPoundage { get; set; }

        /// <summary>
        /// 货币名称
        /// </summary>
        public string CurrencyName { get; set; }
        /// <summary>
        /// 平台总资金
        /// </summary>
        public decimal PlateformAmt { get; set; }
        /// <summary>
        /// 充值总资金
        /// </summary>
        public decimal RechargeAmt { get; set; }
        /// <summary>
        ///提现总资金
        /// </summary>
        public decimal WithdrawAmt { get; set; }
        /// <summary>
        ///委托总资金
        /// </summary>
        public decimal DelegateAmt { get; set; }
        /// <summary>
        ///冻结总资金
        /// </summary>
        public decimal FreezeAmt { get; set; }
        /// <summary>
        /// 交易手续费总额
        /// </summary>
        public decimal DealPoundage { get; set; }
        /// <summary>
        /// 提现手续费总额
        /// </summary>
        public decimal WithdrawPoundage { get; set; }
        /// <summary>
        /// 微信充值总金额
        /// </summary>
        public decimal InGoldenAmt { get; set; }
        /// <summary>
        /// 手续费总额
        /// </summary>
        public decimal PoundageTotal
        {
            get
            {
                return this.WithdrawPoundage + this.DealPoundage;
            }
        }
        /// <summary>
        /// 虚拟币列表
        /// </summary>
        public List<CurrencyReport_ViewModel> CurrencyReportList { get; set; }
    }


    public class HomeDataReport_ViewModel
    {
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? StartTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }
        /// <summary>
        /// 货币
        /// </summary>
        public string CurrencyID { get; set; }
        /// <summary>
        /// 虚拟币
        /// </summary>
        public string CoinID { get; set; }
        /// <summary>
        /// 业务类型
        /// </summary>
        public string BusinessName { get; set; }
        /// <summary>
        /// 累计总金额
        /// </summary>
        public string TotalAmt { get; set; }
        /// <summary>
        /// 今日总金额
        /// </summary>
        public string TodayAmt { get; set; }
        /// <summary>
        /// 累计手续费
        /// </summary>
        public string TotalPoundage { get; set; }
        /// <summary>
        /// 今日手续费
        /// </summary>
        public string TodayPoundage { get; set; }
    }
}