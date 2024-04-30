
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ReceivingAndSendingMessanges
{
    public class UDPMessanges //Відправка і прийом повідомлень
    {
        // прием повідомлення
        public static string UDPGetMessange(UdpClient udp,ref IPEndPoint senderEndPoint)
        {
            byte[] buffer ;
            buffer = udp.Receive(ref senderEndPoint);
            string str = Encoding.Unicode.GetString(buffer);
            return str;
        }

        public async static Task<string> UDPGetMessangeAsync(UdpClient udp, IPEndPoint senderEndPoint)
        {
            Task<string> result = Task<string>.Factory.StartNew(() => UDPGetMessange(udp,ref senderEndPoint));
            await result;
            return result.Result;
        }

        // відправка
        public static void UDPSendMessage(UdpClient udpClient, string message)
        {
            udpClient?.Send(Encoding.Unicode.GetBytes(message));
        }

        public static void UDPSendMessage(UdpClient udpClient, IPEndPoint iPEnd, string message)
        {
            udpClient?.Send(Encoding.Unicode.GetBytes(message), iPEnd);
        }




    }
}
