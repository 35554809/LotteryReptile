using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LotteryReptile.Models
{
    public static class tool
    {
        /// <summary>
        /// 获得字符串中开始和结束字符串中间得值
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="s">开始</param>
        /// <param name="e">结束</param>
        /// <returns></returns> 
        public static string[] GetValue(string str, string s, string e)
        {
            Regex rg = new Regex("(?<=(" + s + "))[.\\s\\S]*?(?=(" + e + "))");
            MatchCollection mc = rg.Matches(str);
            string[] ss = new string[mc.Count];
            for (int i = 0; i < mc.Count; i++)
                ss[i] = mc[i].Value;
            return ss;
        }

        /// <summary>
        /// 获得字符串中的数字
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="num">匹配的数字为几位的</param>
        /// <returns></returns> 
        public static string[] GetStringNumbers(string str, int num = 1)
        {
            Regex rg = new Regex("\\d{" + num + "}");
            MatchCollection mc = rg.Matches(str);
            string[] ss = new string[mc.Count];
            for (int i = 0; i < mc.Count; i++)
                ss[i] = mc[i].Value;
            return ss;
        }

        /// <summary>
        /// 生成补位0
        /// </summary>
        /// <param name="num">生成个数</param>
        /// <returns></returns> 
        public static string GetSupplement0(int num)
        {
            string s = "";
            for (int i = 0; i < num; i++)
                s += "0";
            return s;
        }


        /// <summary>
        /// 字符串和值
        /// </summary>
        /// <param name="s">字符串</param>
        /// <returns></returns>
        public static int GetNumberAndValue(string s)
        {

            var ss = s.Split(',');
            int values = 0;
            foreach (var item in ss)
                values += Convert.ToInt32(item);
            return values;
        }


        
    }
}
