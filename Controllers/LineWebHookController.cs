using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace isRock.Template
{
    public class LineWebHookController : isRock.LineBot.LineWebHookControllerBase
    {
        [Route("api/LineBotWebHook")]
        [HttpPost]
        public IActionResult POST()
        {
            var AdminUserId = "d847ff896a03a236450d534b110316e5";

            try
            {
                //設定ChannelAccessToken
                this.ChannelAccessToken = "vzGyAfF31U+GOZ5xTgE5XVW6tWBxUYAKOt03c8zQmDpJ3Qtv6KjwHJe7ewMEMQYgrCZEANicyt1cHGMi3ggaR2YuDx4oMGmHb5MKko64uRLOC5XcPsUveUt/zcOv8Osaq5a6NtwAhI71TOiSmOnG/wdB04t89/1O/w1cDnyilFU=
";
                //配合Line Verify
                if (ReceivedMessage.events == null || ReceivedMessage.events.Count() <= 0 ||
                    ReceivedMessage.events.FirstOrDefault().replyToken == "00000000000000000000000000000000") return Ok();
                //取得Line Event
                var LineEvent = this.ReceivedMessage.events.FirstOrDefault();
                var responseMsg = "";
                //準備回覆訊息
                if (LineEvent.type.ToLower() == "message" && LineEvent.message.type == "text")
                    responseMsg = $"{  isRock.Template.ChatGPT.CallChatGPT(LineEvent.message.text).choices.FirstOrDefault().text}";
                else if (LineEvent.type.ToLower() == "message")
                    responseMsg = $"收到 event : {LineEvent.type} type: {LineEvent.message.type} ";
                else
                    responseMsg = $"收到 event : {LineEvent.type} ";
                //回覆訊息
                this.ReplyMessage(LineEvent.replyToken, responseMsg);
                //response OK
                return Ok();
            }
            catch (Exception ex)
            {
                //回覆訊息
                this.PushMessage(AdminUserId, "發生錯誤:\n" + ex.Message);
                //response OK
                return Ok();
            }
        }
    }
}
