using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotteryReptile
{
    public class OnCompleteEventArgs
    {
        public Uri Uri { get; private set; }// 爬虫URL地址
        public int ThreadId { get; private set; }// 任务线程ID
        public string PageSource { get; private set; }// 页面源代码
        public long Milliseconds { get; private set; }// 爬虫请求执行事件
        public OnCompleteEventArgs(Uri uri, int threadId, long milliseconds, string pageSource)
        {
            this.Uri = uri;
            this.ThreadId = threadId;
            this.Milliseconds = milliseconds;
            this.PageSource = pageSource;
        }
        
    }
}
