
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
            byte[] buffer /*= new byte[1024]*/;
            //IPEndPoint tempEndPoint = new IPEndPoint(IPAddress.Any, 0);
            //tempEndPoint = new IPEndPoint(IPAddress.Any, 0);
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
        public static void UDPSendMessage(UdpClient udpClient, /*IPEndPoint iPEnd,*/ string message)
        {
            udpClient?.Send(Encoding.Unicode.GetBytes(message));
            //udpClient?.SendAsync(Encoding.Unicode.GetBytes(message),iPEnd);
        }

        public static void UDPSendMessage(UdpClient udpClient, IPEndPoint iPEnd, string message)
        {
            udpClient?.Send(Encoding.Unicode.GetBytes(message), iPEnd);
        }




    }
}
