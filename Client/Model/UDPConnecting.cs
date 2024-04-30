
using System.Net;
using System.Net.Sockets;

namespace Client.Model
{
    public class UDPConnecting
    {
        const int SERVER_PORT = 4000;
        const string SERVER_IP = "127.0.0.1";
        public Socket ClientSocket;
        public UdpClient udpClient;
        public IPEndPoint udpIPEndPoint;

        public UDPConnecting()
        {
            udpIPEndPoint = new IPEndPoint(IPAddress.Parse(SERVER_IP), SERVER_PORT);
        }

        // підключення до сервера
        public bool UDPConnectingToServer()
        {
            bool result = false;
            try
            {
                //using UDPClient udpServer = new UDPClient();
                udpClient = new UdpClient();
                udpClient.Connect(udpIPEndPoint);
                ClientSocket = udpClient.Client;

                result = true;
            }
            catch (Exception)
            {
                return result;
            }
            return result;

        }

        public async Task<bool> UDPConnectingToServerAsync()
        {
            try
            {
                Task<bool> task = Task<bool>.Factory.StartNew(() => UDPConnectingToServer());
                await task;
                return task.Result;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public void UDPClose()
        {
            if (ClientSocket.Connected)
            {
                ClientSocket.Shutdown(SocketShutdown.Both);
                ClientSocket.Close();
                ClientSocket.Dispose();
                
            }

            if (udpClient != null)
            {
                udpClient.Close();
                udpClient.Dispose();
            }
        }

    }
}
