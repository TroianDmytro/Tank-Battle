// See https://aka.ms/new-console-template for more information

using System.Net;
using System.Net.Sockets;

const int SERVER_PORT = 4000;
const string SERVER_IP = "127.0.0.1";
IPEndPoint iPEndPoint = new IPEndPoint(SERVER_PORT,SERVER_PORT);
ReceivingAndSendingMessanges.Messange messange = new ReceivingAndSendingMessanges.Messange();

using var socketServer = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

socketServer.Bind(iPEndPoint);
socketServer.Listen();

Socket clientSocet = await socketServer.AcceptAsync();
messange.SendMessage(clientSocet, "Connecting Player1");
