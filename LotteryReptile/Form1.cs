using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LotteryReptile.Models;

namespace LotteryReptile
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1_Tick(timer1, new EventArgs());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Start();
            button1.Enabled = false;

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            List<tb_Lottery> LotteryList;
            List<string> RedisUpdateID = new List<string>(); //需要重新更新开奖结果的RedisID
            using (SqlEntities db = new SqlEntities())
            {
                LotteryList = db.tb_Lottery.AsNoTracking().Where(t => t.State == 0).ToList();



            }
            //Parallel.ForEach(LotteryList, item =>
            //    {


            //    });
            foreach (var item in LotteryList)
            {
                var url = string.Empty;
                if (item.IsUrl == 0)
                    url = item.Url1;
                else
                    url = item.Url2;

                if (url.IndexOf("kaijiang.500.com") != -1)
                    url += DateTime.Now.ToString("yyyyMMdd") + ".xml";

                IReptile LotteryReptile;
                if (url.IndexOf("dandan28") != -1)
                    LotteryReptile = new DanDanReptile();
                else
                    LotteryReptile = new Reptile();
                LotteryReptile.OnStart += (s, ea) =>
                {
                    //LogEx.l("开始抓取：" + url);
                };
                LotteryReptile.OnError += (s, ea) =>
                {
                    ea.Exception.l("");
                };
                LotteryReptile.OnComplete += (s, ea) =>
                {
                    try
                    {
                        NewLotteryInfo en;
                        if (ea.Uri.AbsoluteUri.IndexOf("www-cp77.com") != -1)
                        {
                            en = Algorithm.SSC_cp77(ea.PageSource);
                        }
                        else if (ea.Uri.AbsoluteUri.IndexOf("www.lotto-8.com") != -1)
                        {
                            en = Algorithm.XGLHC_lotto8(ea.PageSource);
                        }
                        else if (ea.Uri.AbsoluteUri.IndexOf("www.dandan28.com") != -1)
                        {
                            en = Algorithm.PCDD_dandan28(ea.PageSource);
                        }
                        else if (ea.Uri.AbsoluteUri.IndexOf("baidu.lecai.com/lottery/ajax_latestdrawn.php") != -1)
                        {
                            en = Algorithm.SSC_baidu(ea.PageSource);
                        }
                        else if (ea.Uri.AbsoluteUri.IndexOf("baidu.lecai.com/lottery/ajax_current_with_latest_drawn.php") != -1)
                        {
                            en = Algorithm.K3_baidu(ea.PageSource);
                        }
                        else if (ea.Uri.AbsoluteUri.IndexOf("kaijiang.500.com") != -1)
                        {
                            en = Algorithm.ssc_500(ea.PageSource);
                        }
                        else if (ea.Uri.AbsoluteUri.IndexOf("www.mingpaoracing.com") != -1)
                        {
                            en = Algorithm.XGLHC_mingpaoracing(ea.PageSource);
                        }
                        else if (ea.Uri.AbsoluteUri.IndexOf("www.xjflcp.com") != -1)
                        {
                            en = Algorithm.SSC_xjflcp(ea.PageSource);
                        }
                        else if (ea.Uri.AbsoluteUri.IndexOf("shishicai.cjcp.com.cn/tianjin") != -1)
                        {
                            en = Algorithm.SSC_shishicai(ea.PageSource);
                        }
                        //else if (ea.Uri.AbsoluteUri.IndexOf("www.pk10.com") != -1)
                        //{
                        //    en = Algorithm.SSC_pk10(ea.PageSource);
                        //}
                        else if (ea.Uri.AbsoluteUri.IndexOf("kuai3.cjcp.com.cn") != -1)
                        {
                            en = Algorithm.SSC_kuai3(ea.PageSource);
                        }
                        else if (ea.Uri.AbsoluteUri.IndexOf("zs.cailele.com") != -1)
                        {
                            en = Algorithm.SSC_zs(ea.PageSource);
                        }
                        else if (ea.Uri.AbsoluteUri.IndexOf("kaijiang.aicai.com") != -1)
                        {
                            en = Algorithm.SSC_kaijiang(ea.PageSource);
                        }
                        else if (ea.Uri.AbsoluteUri.IndexOf("kj.cjcp.com.cn") != -1)
                        {
                            en = Algorithm.SSC_kj(ea.PageSource);
                        }
                        else if (ea.Uri.AbsoluteUri.IndexOf("shishicai.cjcp.com.cn/heilongjiang") != -1)
                        {
                            en = Algorithm.SSC_shishicaihei(ea.PageSource);
                        }

                        else
                            en = new NewLotteryInfo();
                        bool isAdd = true;
                        using (SqlEntities db = new SqlEntities())
                        {
                            var LastNo = db.tb_LotteryNumber.AsNoTracking().Where(t => t.LotteryID == item.ID).OrderByDescending(t => t.AddTime).FirstOrDefault();
                            if (LastNo != null)
                            {
                                //if (item.ID == 1)
                                //    en.No = (Convert.ToInt64(LastNo) + 1).ToString();
                                if (LastNo.No == en.No)
                                {
                                    isAdd = false;
                                }
                                else if (LastNo.Numbers == en.Numbers)
                                {
                                    isAdd = false;
                                }
                            }
                            if (isAdd)
                            {
                                tb_LotteryNumber ln = new tb_LotteryNumber();
                                ln.AddTime = DateTime.Now;
                                ln.LotteryID = item.ID;
                                ln.No = en.No;
                                ln.Numbers = en.Numbers;
                                ln.OpenTime = en.OpenTime;
                                db.tb_LotteryNumber.Add(ln);
                                db.SaveChanges();
                                //sb.AppendFormat("INSERT INTO [tb_LotteryNumber] ([LotteryID] ,[No],[Numbers],[AddTime],[OpenTime])");
                                //sb.AppendFormat("VALUES ({0} ,'{1}','{2}','{3}','{4}');", item.ID, en.No, en.Numbers, DateTime.Now, en.OpenTime);
                                // RedisUpdateID.Add("ApiLotteryNumber:" + item.ID);
                                ReturnRedis.RemoveKey("ApiLotteryNumber:" + item.ID);
                                LogEx.l("抓取完成 url：" + url);
                            }
                        }

                    }
                    catch (Exception)
                    {

                    }

                };

                if (!string.IsNullOrWhiteSpace(url))
                    LotteryReptile.Start(new Uri(url));
            }


        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            button1.Enabled = true;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            using (SqlEntities db = new SqlEntities())
            {
                var LotteryList = db.tb_Lottery.AsNoTracking().Where(t => t.State == 0).ToList();

            }
        }
    }
}
