using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Chat
{
    public class Client
    {
        private IPEndPoint ipEndPoint;
        private UdpClient udpClient;

        public Client()
        {
            ipEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 12345);
            udpClient = new UdpClient();
        }
        public void SendMessage(string? nickname)
        {
            while (true)
            {
                Console.Write("Введите сообщение: ");
                string? textMessage = Console.ReadLine();

                if (String.IsNullOrEmpty(textMessage))
                {
                    break;
                }

                Message message = new Message(nickname, textMessage);
                string? convertingToJson = message.GetJsonFromMessage();
                byte[] buffer = Encoding.UTF8.GetBytes(convertingToJson);

                udpClient.Send(buffer, ipEndPoint);

                ReceiveMessage();
            }
        }

        private void ReceiveMessage()
        {
            byte[] receiveBuffer = udpClient.Receive(ref ipEndPoint);
            string receivedString = Encoding.UTF8.GetString(receiveBuffer);
            Message? convertedMessage = Message.GetMessageForomJson(receivedString);
            Console.WriteLine(convertedMessage);
        }
    }
}
