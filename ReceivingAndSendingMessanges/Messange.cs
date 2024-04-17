using System.Net.Sockets;
using System.Text;


namespace ReceivingAndSendingMessanges
{
    //Відправка і приом повідомлень
    public class Messange
    {
        public string GetMessange(Socket socket)
        {
            byte[] buffer = new byte[1024];
            int bytesRead = socket.Receive(buffer);

            string str = Encoding.Unicode.GetString(buffer, 0, bytesRead);
            return str;
        }

        public void SendMessage(Socket socket, string message) 
        {
            socket.Send(Encoding.Unicode.GetBytes(message));
        }
    }
}
