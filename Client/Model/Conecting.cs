

using System.Net;
using System.Net.Sockets;

namespace Client.Model
{

    public class Conecting
    {
        const int SERVER_PORT = 4000;
        const string SERVER_IP = "127.0.0.1";
        public Socket ClientSocket;

        // підключення до сервера
        public bool ConnectingToServer()
        {
            bool result = false;
            try
            {
                IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Parse(SERVER_IP), SERVER_PORT);
                ClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                ClientSocket.Connect(iPEndPoint);
                result = true;
            }
            catch (Exception)
            {
                return result;
            }
            return result;
            
        }

        public async Task<bool> ConnectingToClientAsync() 
        {
            try
            {
                Task < bool> task =  Task<bool>.Factory.StartNew(() => ConnectingToServer());
                await task;
                return task.Result;
            }
            catch (Exception)
            {
                return false;
            }
           
        }

        public void Close()
        {
            if (ClientSocket.Connected)
            {
                ClientSocket.Shutdown(SocketShutdown.Both);
                ClientSocket.Close();
                ClientSocket.Dispose();
            }
        }

    }
}
