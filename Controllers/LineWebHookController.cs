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
            var AdminUserId = "__________1657781599________________";

            try
            {
                //設定ChannelAccessToken
                this.ChannelAccessToken = "______________5hmaIoY2Qb5GjWMR11v4vYXeCa7O1mCmB9BRiQ+FUN3NTBip2hohTseD8lgtjvlVcT6lrtlz1YrUQaxLWCQ0ey6AQmhJu7NzxoV3CHChvHdpGP9jUOmMj6j6oMgK2xM/JnVvMI6vYs6F+8kSpBRBCQdB04t89/1O/w1cDnyilFU=
______________";
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
