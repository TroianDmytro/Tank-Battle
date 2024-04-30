using System.Net.Sockets;
using System.Text;


namespace ReceivingAndSendingMessanges
{
    //Відправка і прийом повідомлень
    public class TCPMessanges
    {
        // прием повідомлення
        public static string TCPGetMessange(Socket socket)
        {
            byte[] buffer = new byte[1024];
            int bytesRead = socket.Receive(buffer);

            string str = Encoding.Unicode.GetString(buffer, 0, bytesRead);
            return str;
        }

        public async static Task<string> TCPGetMessangeAsync(Socket socket)
        {
            Task<string> result = Task<string>.Factory.StartNew(() => TCPGetMessange(socket));
            await result;
            return result.Result;
        }

        // відправка
        public static void TCPSendMessage(Socket socket, string message) 
        {
            socket?.SendAsync(Encoding.Unicode.GetBytes(message));
        }
    }
}
