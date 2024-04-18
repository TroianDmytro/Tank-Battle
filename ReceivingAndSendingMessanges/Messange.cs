using System.Net.Sockets;
using System.Text;


namespace ReceivingAndSendingMessanges
{
    //Відправка і прийом повідомлень
    public class Messange
    {
        // прием повідомлення
        public static string GetMessange(Socket socket)
        {
            byte[] buffer = new byte[1024];
            int bytesRead = socket.Receive(buffer);

            string str = Encoding.Unicode.GetString(buffer, 0, bytesRead);
            return str;
        }

        public async static Task<string> GetMessangeAsync(Socket socket)
        {
            Task<string> result = Task<string>.Factory.StartNew(() => GetMessange(socket));
            await result;
            return result.Result;
        }

        // відправка
        public static void SendMessage(Socket socket, string message) 
        {
            socket?.SendAsync(Encoding.Unicode.GetBytes(message));
        }
    }
}
