using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Web;

namespace LotteryReptile.Models
{
   public static class LogEx
    {
        private static Exception exception_0 = new Exception("日志常规记录");
        public static void l(this string message, string onlyName = "")
        {
            LogEx.exception_0.l(message, onlyName);
        }
        public static void l(this Exception exception_1, string message, string onlyName = "")
        {
          
                bool flag = true;
                StreamWriter streamWriter = LogEx.l(onlyName);
                streamWriter.WriteLine(string.Concat(new string[]
                {
                    "【",
                    DateTime.Now.ToString("HH:mm:ss"),
                    "】",
                    message,
                    "『",
                    (exception_1 == LogEx.exception_0) ? exception_1.Message.Trim() : ("异常错误：" + exception_1.Message.Trim()),
                    flag ? ((exception_1.StackTrace != null) ? ("_" + exception_1.StackTrace.Trim()) : "") : "",
                    "』"
                }));
                LogEx.smethod_2(streamWriter);
            
        }
        private static void smethod_2(StreamWriter streamWriter_0)
        {
            streamWriter_0.Close();
            streamWriter_0 = null;
        }
        private static StreamWriter l(string string_0)
        {
            string str = "";
            if (AppDomain.CurrentDomain != null)
            {
                try
                {
                    str = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
                   
                }
                catch
                {
                    str = Environment.CurrentDirectory;                   
                }
            }
         
          
            string text = str + ((string_0.Trim() != "") ? "\\Log\\Private\\" : "\\Log\\");
            if (!Directory.Exists(text))
            {
                Directory.CreateDirectory(text);
            }
            string str2 = (string_0 != "") ? string_0 : DateTime.Now.ToString("yyyyMMdd");
            string format = text + str2 + "_{0}.Log";
            int num = 0;
            while (!LogEx.smethod_4((num > 0) ? string.Format(format, num) : (text + str2 + ".Log")))
            {
                num++;
            }
            FileStream fileStream = new FileStream((num > 0) ? string.Format(format, num) : (text + str2 + ".Log"), FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
            fileStream.Position = fileStream.Length;
            return new StreamWriter(fileStream);
        }
        private static bool smethod_4(string string_0)
        {
            bool result = true;
            if (File.Exists(string_0))
            {
                FileInfo fileInfo = new FileInfo(string_0);
                if (fileInfo.Length > 1048576L)
                {
                    result = false;
                }
            }
            return result;
        }
    }
}
