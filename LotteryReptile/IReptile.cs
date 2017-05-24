using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotteryReptile
{
    public  interface IReptile
    {
        event EventHandler<OnStarEventArgs> OnStart;//爬虫启动事件

        event EventHandler<OnCompleteEventArgs> OnComplete;//爬虫完成事件

        event EventHandler<OnErrorEventArgs> OnError;//爬虫出错事件

        Task<string> Start(Uri uri, string proxy = null); //异步爬虫
    }
}
