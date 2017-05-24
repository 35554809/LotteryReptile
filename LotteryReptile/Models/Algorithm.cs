using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Xml;

namespace LotteryReptile.Models
{
    public class Algorithm
    {

        /// <summary>
        /// 采集香港六合彩通过网站www.lotto-8.com
        /// </summary>
        /// <param name="html">抓取到的html</param>
        /// <returns></returns>
        public static NewLotteryInfo XGLHC_lotto8(string html)
        {
            var now = DateTime.Now;
            NewLotteryInfo en = new NewLotteryInfo();
            var ss = tool.GetValue(html, "#CCCCCC\">", "</td>");
            var Day = now - Convert.ToDateTime(now.Year + "-01-01");
            var qh = Day.Days / 5 * 2 + 1;
            en.No = now.Year + tool.GetSupplement0(3 - qh.ToString().Length) + qh;
            en.OpenTime = Convert.ToDateTime(ss[0]);
            en.Numbers = string.Join(",", tool.GetStringNumbers(ss[1], 2)) + "," + ss[2];
            return en;
        }

        /// <summary>
        /// 采集PC蛋蛋通过网站www.dandan28.com
        /// </summary>
        /// <param name="html">抓取到的html</param>
        /// <returns></returns>
        public static NewLotteryInfo PCDD_dandan28(string html)
        {
            var now = DateTime.Now;
            NewLotteryInfo en = new NewLotteryInfo();
            var TrList = tool.GetValue(html, "<tr>", "</tr>");
            var tdList = tool.GetValue(TrList[2], "<td>", "</td>");
            en.No = tdList[0];
            en.OpenTime = Convert.ToDateTime(DateTime.Now.Year.ToString() + " " + tdList[1]);
            var Numbers = tdList[2].Trim().Replace(" ", "").Split('+');
            en.Numbers = Numbers[0] + "," + Numbers[1] + "," + Numbers[2].Substring(0, 1);
            var hz = tool.GetNumberAndValue(en.Numbers);
            en.Numbers += "," + hz;
            return en;
        }
        /// <summary>
        /// 采集时时彩通过网站baidu.lecai.com（百度彩票）
        /// </summary>
        /// <param name="html">抓取到的html</param>
        /// <returns></returns>
        public static NewLotteryInfo SSC_baidu(string html)
        {
            var now = DateTime.Now;
            NewLotteryInfo en = new NewLotteryInfo();
            var Json = (JObject)JsonConvert.DeserializeObject(html);
            var Data1 = (JToken)Json["data"].FirstOrDefault();
            en.No = Data1["phase"].ToString();
            en.OpenTime = Convert.ToDateTime(Data1["time_endsale"].ToString());
            var ss = (JArray)(Data1["result"]["result"].FirstOrDefault()["data"]);
            foreach (var item in ss)
                en.Numbers += "," + item.ToString();
            en.Numbers = en.Numbers.Remove(0, 1);
            return en;
        }

        /// <summary>
        /// 采集快三通过网站baidu.lecai.com（百度彩票）
        /// </summary>
        /// <param name="html">抓取到的html</param>
        /// <returns></returns>
        public static NewLotteryInfo K3_baidu(string html)
        {
            var now = DateTime.Now;
            NewLotteryInfo en = new NewLotteryInfo();
            var Json = (JObject)JsonConvert.DeserializeObject(html);
            var Data1 = (JToken)Json["data"]["latest_drawn"];
            en.No = Data1["phase"].ToString();
            en.OpenTime = Convert.ToDateTime(Data1["time_endsale"].ToString());
            var ss = (JArray)(Data1["result"]["result"].FirstOrDefault()["data"]);
            foreach (var item in ss)
                en.Numbers += "," + item.ToString();
            en.Numbers = en.Numbers.Remove(0, 1);
            return en;
        }


        /// <summary>
        /// 采集时时彩通过网站kaijiang.500.com(500彩票网)
        /// </summary>
        /// <param name="html">抓取到的html</param>
        /// <returns></returns>
        public static NewLotteryInfo ssc_500(string html)
        {

            NewLotteryInfo en = new NewLotteryInfo();
            var doc = new XmlDocument();
            doc.LoadXml(html);
            var NewNode = doc.DocumentElement.ChildNodes.Item(0);
            en.No = NewNode.Attributes["expect"].Value;
            en.Numbers = NewNode.Attributes["opencode"].Value;
            en.OpenTime = Convert.ToDateTime(NewNode.Attributes["opentime"].Value);
            return en;
        }

        /// <summary>
        /// 采集快三通过网站caipiao.163.com（网易彩票）
        /// </summary>
        /// <param name="html">抓取到的html</param>
        /// <returns></returns>
        public static NewLotteryInfo K3_163(string html)
        {
            var now = DateTime.Now;
            NewLotteryInfo en = new NewLotteryInfo();
            var Json = (JObject)JsonConvert.DeserializeObject(html);
            var Data1 = (JToken)Json["awardNumberInfoList"].FirstOrDefault();
            en.No = Data1["period"].ToString();
            en.OpenTime = DateTime.Now;
            en.Numbers = Data1["winningNumber"].ToString().Replace(" ", ",");
            return en;
        }



        /// <summary>
        /// 采集香港六合彩通过网站www.mingpaoracing.com
        /// </summary>
        /// <param name="html">抓取到的html</param>
        /// <returns></returns>
        public static NewLotteryInfo XGLHC_mingpaoracing(string html)
        {
            var now = DateTime.Now;
            NewLotteryInfo en = new NewLotteryInfo();
            var ss = tool.GetValue(html, "../image/b", ".gif");
            var Day = now - Convert.ToDateTime(now.Year + "-01-01");
            var qh = Day.Days / 5 * 2 + 1;
            en.No = now.Year + tool.GetSupplement0(3 - qh.ToString().Length) + qh;
            en.OpenTime = DateTime.Now;
            ss[0] = ""; ss[1] = "";
            en.Numbers = string.Join(",", ss);
            en.Numbers = en.Numbers.Remove(0, 2);
            return en;
        }

        /// <summary>
        /// 采集时时彩通过网站buy.cqcp.net（重庆彩票）
        /// </summary>
        /// <param name="html">抓取到的html</param>
        /// <returns></returns>
        //public static NewLotteryInfo SSC_cqcp(string html)
        //{
        //    var now = DateTime.Now;
        //    NewLotteryInfo en = new NewLotteryInfo();
        //    var ss = tool.GetValue(html, "class=\"redball\">", "</td>");
        //    en.Numbers = string.Join(",", ss);
        //    en.OpenTime = DateTime.Now;
        //    var no = tool.GetValue(html, "<td>", "</td>");
        //    en.No = no[0];
        //    return en;
        //}

        /// <summary>
        /// 采集时时彩通过网站www.xjflcp.com（新疆彩票）
        /// </summary>
        /// <param name="html">抓取到的html</param>
        /// <returns></returns>
        public static NewLotteryInfo SSC_xjflcp(string html)
        {
            var now = DateTime.Now;
            NewLotteryInfo en = new NewLotteryInfo();
            var ss = tool.GetValue(html, "<i>", "</i>");
            en.Numbers = string.Join(",", ss);
            en.OpenTime = DateTime.Now;
            var no = tool.GetValue(html, "<span>", "</span>");
            en.No = no[0];
            return en;
        }

        /// <summary>
        /// 采集时时彩通过网站shishicai.cjcp.com.cn/tianjin（天津彩票）
        /// </summary>
        /// <param name="html">抓取到的html</param>
        /// <returns></returns>
        public static NewLotteryInfo SSC_shishicai(string html)
        {
            var now = DateTime.Now;
            NewLotteryInfo en = new NewLotteryInfo();
            var ss = tool.GetValue(html, "<div class=\"hm_bg\">", "</div>");
            en.Numbers = ss[0] + "," + ss[1] + "," + ss[2] + "," + ss[3] + "," + ss[4];
            en.OpenTime = DateTime.Now;
            var no = tool.GetValue(html, "<td>", "</td>");
            en.No = no[26];
            return en;
        }

        /// <summary>
        /// 采集北京PK10通过网站www.pk10.com
        /// </summary>
        /// <param name="html">抓取到的html</param>
        /// <returns></returns>
        //public static NewLotteryInfo SSC_pk10(string html)
        //{
        //    var now = DateTime.Now;
        //    NewLotteryInfo en = new NewLotteryInfo();
        //    var ss = tool.GetValue(html, "title=", ">");
        //    en.Numbers = string.Join(",", ss);
        //    en.OpenTime = DateTime.Now;
        //    var no = tool.GetValue(html, "<td>", "</td>");
        //    en.No = no[1];
        //    return en;
        //}

        /// <summary>
        /// 采集快三通过网站kuai3.cjcp.com.cn(安徽)
        /// </summary>
        /// <param name="html">抓取到的html</param>
        /// <returns></returns>
        public static NewLotteryInfo SSC_kuai3(string html)
        {
            var now = DateTime.Now;
            NewLotteryInfo en = new NewLotteryInfo();
            var ss = tool.GetValue(html, "<div class=\"hm_bg\">", "</div>");
            en.Numbers = ss[0] + "," + ss[1] + "," + ss[2];
            en.OpenTime = DateTime.Now;
            var no = tool.GetValue(html, "<td>", "</td>");
            en.No = no[18];
            return en;
        }

        /// <summary>
        /// 采集十一选五通过网站zs.cailele.com(上海)
        /// </summary>
        /// <param name="html">抓取到的html</param>
        /// <returns></returns>
        public static NewLotteryInfo SSC_zs(string html)
        {
            var now = DateTime.Now;
            NewLotteryInfo en = new NewLotteryInfo();
            var ss = tool.GetValue(html, "class=\"yellow\">", "</td>");
            en.Numbers = ss[0] + "," + ss[1] + "," + ss[2] + "," + ss[3] + "," + ss[4];
            en.OpenTime = DateTime.Now;
            var no = tool.GetValue(html, "<td>", "</td>");
            en.No = no[5];
            return en;
        }

        /// <summary>
        /// 采集十一选五通过网站kaijiang.aicai.com(安徽)
        /// </summary>
        /// <param name="html">抓取到的html</param>
        /// <returns></returns>
        public static NewLotteryInfo SSC_kaijiang(string html)
        {
            var now = DateTime.Now;
            NewLotteryInfo en = new NewLotteryInfo();
            var ss = tool.GetValue(html, "style=\"color:red\">", "</td>");
            ss[0] = "";
            en.Numbers = ss[1];
            en.OpenTime = DateTime.Now;
            var no = tool.GetValue(html, "<td>", "</td>");
            en.No = no[0];
            return en;
        }

        /// <summary>
        /// 采集福彩3D通过网站kj.cjcp.com.cn
        /// </summary>
        /// <param name="html">抓取到的html</param>
        /// <returns></returns>
        public static NewLotteryInfo SSC_kj(string html)
        {
            var now = DateTime.Now;
            NewLotteryInfo en = new NewLotteryInfo();
            var ss = GetInput("value","input");
            en.Numbers = string.Join(",", ss);
            en.OpenTime = DateTime.Now;
            var no = tool.GetValue(html, "class=\"qihao\">", "</td>");
            en.No = no[0];
            return en;
        }

        private static string GetInput(string value,string input)
        {
            int start;
            start = value.IndexOf(string.Format("{0}=", input), StringComparison.OrdinalIgnoreCase);
            if (start == -1)
                return string.Empty;
            start += input.Length + 2;
            StringBuilder val = new StringBuilder();
            while(true)
            {
                if(value[start]=='\"')
                    break;
                val.Append(value[start]);
                start++;
            }
            return val.ToString();
        }

        /// <summary>
        /// 采集时时彩通过网站shishicai.cjcp.com.cn/heilongjiang（黑龙江彩票）
        /// </summary>
        /// <param name="html">抓取到的html</param>
        /// <returns></returns>
        public static NewLotteryInfo SSC_shishicaihei(string html)
        {
            var now = DateTime.Now;
            NewLotteryInfo en = new NewLotteryInfo();
            var ss = tool.GetValue(html, "<div class=\"hm_bg\">", "</div>");
            en.Numbers = ss[0] + "," + ss[1] + "," + ss[2] + "," + ss[3] + "," + ss[4];
            var time = tool.GetValue(html, "<td>", "</td>");
            en.OpenTime =Convert.ToDateTime(time[0]);
            var no = tool.GetValue(html, "<td>", "</td>");
            en.No = no[36];
            return en;
        }


        /// <summary>
        /// 采集时时彩通过网站www-cp77.com
        /// </summary>
        /// <param name="html">抓取到的html</param>
        /// <returns></returns>
        public static NewLotteryInfo SSC_cp77(string html)
        {
            var now = DateTime.Now;
            NewLotteryInfo en = new NewLotteryInfo();

            var Numbers = tool.GetValue(html, "<span>", "</span>");
            Numbers[0] = ""; Numbers[1]="";
            en.Numbers = string.Join(",", Numbers).Remove(0, 2);
            var Times = tool.GetValue(html, "<span class=\"red\">", "</span>");
            en.OpenTime = Convert.ToDateTime(Times[0]);
            var No = tool.GetValue(html, "selected = selected\">", "</option>");
            en.No = No[0];
            return en;
        }
    }
}
