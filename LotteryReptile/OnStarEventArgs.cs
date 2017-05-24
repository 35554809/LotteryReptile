using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotteryReptile
{
    public class OnStarEventArgs
    {
        public Uri Uri { get; set; }
        public OnStarEventArgs(Uri Uri)
        {
            this.Uri = Uri;
        }
    }
}
