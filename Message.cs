using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Chat
{
    public class Message
    {
        public string Nickname { get; private set; }
        public string TextMessage { get; private set; }
        public DateTime TimeSendingMessage { get; private set; }

        public Message(string nickname, string textMessage)
        {
            Nickname = nickname;
            TextMessage = textMessage;
            TimeSendingMessage = DateTime.Now;
        }

        public string GetJsonFromMessage()
        {
            return JsonSerializer.Serialize(this);
        }

        public static Message? GetMessageForomJson(string message)
        {
            return JsonSerializer.Deserialize<Message>(message);
        }

        public override string ToString()
        {
            return $"\n({TimeSendingMessage.ToShortTimeString()}) Получено сообщение.\nОтправитель {Nickname}: {TextMessage}\n";
        }
    }
}

