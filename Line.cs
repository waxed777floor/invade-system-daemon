using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using System.Net.Http;

namespace Notify
{
    public class Line
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);


        /*const string channelAccessToken = "+flJQwbBqV6Lb/9ji2rpkN25bUlkCROO0yuF3mVM9eOl8/XtZNKoqCrEE6WENHnT5HtyZP55Uc70qIsx8oS7KVhDKRWDRFgikQcD53colFicj42HtQ819A24Iscb3TTO/c+wfsGxXqTIFV+N4wVpAwdB04t89/1O/w1cDnyilFU=";
        const string AdminUserId = "U23aca0b0088af3916c5f1a91894485d4";//"U16b8cdf0402c90c2359b709eef1b7588";
        */

        public static void Send(string Msg)
        {
            /*try
            {
                string channelAccessToken = Contracts.channelAccessToken;
                List<string> AdminUsersId = Contracts.LineAdminDevelopersId.Concat(Contracts.LineAdminUsersId).ToList();
                foreach (var item in AdminUsersId)
                {
                    var bot = new Bot(channelAccessToken);
                    bot.PushMessage(item, Msg.Replace("\\", "/"));
                }
            }
            catch (Exception ex)
            {
                string exMsg = $"Line {Msg} On Error {ex.Message}";
                log.Error(exMsg);
                Controll.ProgramExitNoSendLine(exMsg);
            }*/
        }


        public static void SendToUser(string Msg)
        {
            /* string channelAccessToken = Contracts.channelAccessToken;
             List<string> AdminUsersId = Contracts.LineAdminUsersId;
             foreach (var item in AdminUsersId)
             {
                 var bot = new Bot(channelAccessToken);
                 bot.PushMessage(item, Msg.Replace("\\", "/"));
             }*/
        }

        public static void SendToDeveloper(string Msg)
        {
            /*  try
              {
                  string MsgAddWanIp = $"From: {Contracts.WanIp} _{Contracts.Function}" + Environment.NewLine + Msg;

                  string channelAccessToken = Contracts.channelAccessToken;
                  List<string> AdminUsersId = Contracts.LineAdminDevelopersId;
                  foreach (var item in AdminUsersId)
                  {
                      var bot = new Bot(channelAccessToken);
                      bot.PushMessage(item, MsgAddWanIp.Replace("\\", "/"));
                  }
              }
              catch (Exception ex)
              {
                  string exMsg = $"Line {Msg} On Error {ex.Message}";
                  log.Error(exMsg);
                  Controll.ProgramExitNoSendLine(exMsg);
              }*/


        }
    }

    public class LineNotify
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /*
        const string channelAccessToken = "+flJQwbBqV6Lb/9ji2rpkN25bUlkCROO0yuF3mVM9eOl8/XtZNKoqCrEE6WENHnT5HtyZP55Uc70qIsx8oS7KVhDKRWDRFgikQcD53colFicj42HtQ819A24Iscb3TTO/c+wfsGxXqTIFV+N4wVpAwdB04t89/1O/w1cDnyilFU=";
        const string AdminUserId = "U23aca0b0088af3916c5f1a91894485d4";//"U16b8cdf0402c90c2359b709eef1b7588";
        */

        public static void Send(string Msg)
        {
            SendToUser(Msg);
            //SendToDeveloper(Msg);
        }


        public static void SendToUser(string Msg)
        {
            string strSend = Msg;
            log.Info($"SendToUser {strSend}");
            SendLineNotify("kxDY1CH2CQT0C4itJo0BS3OytuMA9L5NXWCw6Au1OUY", strSend);
        }

        public static void SendToDeveloper(string Msg)
        {
            List<string> AdminUsersId_Tokens = new List<string>();
            AdminUsersId_Tokens.Add("kxDY1CH2CQT0C4itJo0BS3OytuMA9L5NXWCw6Au1OUY");
            AdminUsersId_Tokens.Add("TOirhGP6CiZNEJwoumAE9ddVxazBnitYf0A7Q4eP8hr");
            AdminUsersId_Tokens.Add("SIS2e0jIUj7YoR2PW5xrJmVYHhbqRRmiRW8u6zW0UfE");
            AdminUsersId_Tokens.Add("AbnIV5Q4HtWZc3rNPpdFqF7bXaZdlzw7Yyd2x6NKPus");
            AdminUsersId_Tokens.Add("Vwm8BzMO9kZsCC9mVxEyPbRdhE6ruyHqk4HPjtuHP8Z");

            if (AdminUsersId_Tokens.Count == 0)
                return;

            string strSend = Msg;
            string result = PickTokenAndSend(strSend, AdminUsersId_Tokens);
            string Ermsg = "Line Notify On Bug: " + result;

            if (result == "Success")
            {

            }
            else
            {
                log.Error(Ermsg);
            }
        }

        public static string PickTokenAndSend(string strSend, List<string> tokens)
        {
            //Pick One Token
            foreach (string token in tokens)
            {
                Console.WriteLine(strSend + " " + token);
                SendLineNotify(strSend, token);
            }

            //若迴圈跑完之後 還是沒有成功送出該訊息 則表示每個TOKEN都到達上限了
            //EMAIL通知並準備離開程式
            return "Token Limit";
        }

        static void SendLineNotify(string token, string message)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("message", message)
            });

            var response = client.PostAsync("https://notify-api.line.me/api/notify", content).Result;
            response.EnsureSuccessStatusCode();

            var responseString = response.Content.ReadAsStringAsync().Result;
            log.Info(responseString);
        }
    }


    public class Telegram
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);


        private static readonly string token = "5165944123:AAGgk-retaEQtBLwWI5nvQVDlVs38Y89JsM";
        private static readonly TelegramBotClient Bot = new TelegramBotClient(token);

        static string randy = "1009737887";//
        //group yuan 公司群組Ooo
        //static string coinShaOooGroup_steve = "-577725267";
        static string coinShaOooGroup_steve = "-538459278";// 錢包有變化就會自動通知Ooo的群組
        static string coinShaCkAutoGroup_steve = "-733389754"; //Bitopro Max漲跌超過0.1%就會自動通知一次CK的群組
        static string cockBroGroup = "-1001365185711";

        /*const string channelAccessToken = "+flJQwbBqV6Lb/9ji2rpkN25bUlkCROO0yuF3mVM9eOl8/XtZNKoqCrEE6WENHnT5HtyZP55Uc70qIsx8oS7KVhDKRWDRFgikQcD53colFicj42HtQ819A24Iscb3TTO/c+wfsGxXqTIFV+N4wVpAwdB04t89/1O/w1cDnyilFU=";
        const string AdminUserId = "U23aca0b0088af3916c5f1a91894485d4";//"U16b8cdf0402c90c2359b709eef1b7588";
        */

        public static void Send(string Msg)
        {
            SendToUser(Msg);
            SendToDeveloper(Msg);
        }

        public static void SendToOooAuto(string Msg)
        {
            Bot.SendTextMessageAsync(coinShaCkAutoGroup_steve, Msg);
            //Bot.SendTextMessageAsync(cockBroGroup, Msg);
            SendToDeveloper(Msg);
        }

        public static void SendToCkAuto(string Msg)
        {
            Bot.SendTextMessageAsync(coinShaCkAutoGroup_steve, Msg);
            //Bot.SendTextMessageAsync(cockBroGroup, Msg);
            SendToDeveloper(Msg);
        }

        public static void SendToCkAuto(string Msg, List<string> ckAutoGroupIds)
        {
            foreach (var item in ckAutoGroupIds)
            {
                Bot.SendTextMessageAsync(item, Msg);
                string devMsg = $"SendToCkAuto id:{item}" + Environment.NewLine + Msg;
                SendToDeveloper(devMsg);
            }
        }

        public static void SendToCk11(string Msg)
        {
            Bot.SendTextMessageAsync(coinShaCkAutoGroup_steve, Msg);
            Bot.SendTextMessageAsync(cockBroGroup, Msg);
            SendToDeveloper(Msg);
        }

        public static void SendToCk11(string Msg, List<string> ck11GroupIds)
        {
            foreach (var item in ck11GroupIds)
            {
                Bot.SendTextMessageAsync(item, Msg);
                string devMsg = $"SendToCk11 id:{item}" + Environment.NewLine + Msg;
                SendToDeveloper(devMsg);
            }
        }

        public static void SendToUser(string Msg)
        {
            Bot.SendTextMessageAsync(coinShaOooGroup_steve, Msg);
        }

        public static void SendToDeveloper(string Msg)
        {
            Bot.SendTextMessageAsync(randy, Msg);
        }

        public static void SendCustom(string Msg, string cusUser)
        {
            Bot.SendTextMessageAsync(cusUser, Msg);
            SendToDeveloper(Msg);
        }

    }

}
