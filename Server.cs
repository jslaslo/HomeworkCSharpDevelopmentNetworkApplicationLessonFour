using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Chat
{
    public class Server
    {
        private IPEndPoint ipEndPoint;
        private UdpClient udpClient;

        public Server()
        {
            ipEndPoint = new IPEndPoint(IPAddress.Any, 0);
            udpClient = new UdpClient(12345);
        }

        public void AcceptMessage()
        {
            Console.WriteLine("Сервер ожидает сообщения от клиента...");
            while (true)
            {
                try
                {
                    ReceiveMessage();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private void ReceiveMessage()
        {
            byte[] receiveBuffer = udpClient.Receive(ref ipEndPoint); ;
            string receivedString = Encoding.UTF8.GetString(receiveBuffer);

            Message? convertedMessage = Message.GetMessageForomJson(receivedString);
            if (convertedMessage != null)
            {
                Console.WriteLine(convertedMessage);
                SendMessage();
            }
            else
            {
                Console.WriteLine("Полученное сообщение некорректно...");
            }
        }

        private void SendMessage()
        {
            Message message = new Message("сервер", "Сообщение успешно отправлено!");
            string? convertingToJson = message.GetJsonFromMessage();
            byte[] responseBuffer = Encoding.UTF8.GetBytes(convertingToJson);

            udpClient.Send(responseBuffer, ipEndPoint);
        }
    }
}
