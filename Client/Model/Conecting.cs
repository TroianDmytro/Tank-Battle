

using System.Net;
using System.Net.Sockets;

namespace Client.Model
{

    public class Conecting
    {
        const int SERVER_PORT = 4000;
        const string SERVER_IP = "127.0.0.1";
        public Socket ClientSocket;

        public bool ConnectingToServer()
        {
            try
            {
                IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Parse(SERVER_IP), SERVER_PORT);
                ClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                ClientSocket.Connect(iPEndPoint);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
            
        }

        public void Close()
        {
            if (ClientSocket != null)
            {
                ClientSocket.Shutdown(SocketShutdown.Both);
                ClientSocket.Close();
                ClientSocket.Dispose();
            }
        }

    }
}
