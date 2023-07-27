using log4net;
using log4net.Config;
using Notify;
using QuoteLineBot1;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace invade_system_daemon
{
    class Program
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        static void Main(string[] args)
        {
            InitialConfiguration();
            log.Info("start console");

            //var initSend = PurgeBot.SendTextMessageAsync(group, "娛樂城 USDT 代收付 init");
            //initSend.Wait();

            while (true)
            {
                try
                {
                    Utility.LogAndConsole("Thread_Payment");

                    // Hide
                    //ShowWindow(handle, SW_HIDE);
                    string dateTimeMMdd = DateTime.UtcNow.AddHours(8).ToString("MM/dd");

                    //定期刪除資料庫舊資料，只保留三天資料
                    InvadeLog.ClearData3DaysAgo();

                    //根據防區，一分鐘抓一次資料，然後如果有入侵或異常就通知
                    doCheckAndSend("1");
                    doCheckAndSend("2");
                    doCheckAndSend("3");
                    doCheckAndSend("4");
                }
                catch (Exception ex)
                {
                    string reTxt = "PaymentTask Error";
                    LineNotify.Send(reTxt);
                    log.Error(ex.ToString());
                    throw;
                }

                System.Threading.Thread.Sleep(30 * 1000);
            }
        }

        private static void doCheckAndSend(string zone)
        {
            var tObj = InvadeLog.GetLastValue(zone);

            if (tObj.value == null)
                return;            

            //檢查異常
            if (tObj.value[0] == '1')//無異常
            {

            }
            else //異常
            {
                //通知
                LineNotify.Send($"防區${zone} 系統異常");
                return;
            }

            //檢查入侵
            if (tObj.value[2] == '0')//無入侵
            {

            }
            else //有入侵
            {
                //通知
                LineNotify.Send($"防區${zone} 有入侵");
            }
        }


        private static void InitialConfiguration()
        {
            var directory = AppDomain.CurrentDomain.RelativeSearchPath ?? AppDomain.CurrentDomain.BaseDirectory;
            XmlConfigurator.ConfigureAndWatch(new FileInfo(Path.Combine(directory, "log4net.config")));
        }

    }
}
