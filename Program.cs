namespace Chat
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Если вы клиент, введите имя: ");
            string? nickname = Console.ReadLine();

            if (String.IsNullOrEmpty(nickname))
            {
                Server server = new Server();
                server.AcceptMessage();
            }
            else
            {
                Client client = new Client();
                client.SendMessage(nickname);
            }
        }
    }
}
