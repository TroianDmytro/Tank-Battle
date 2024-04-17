// See https://aka.ms/new-console-template for more information

using System.Net;
using System.Net.Sockets;


ConsoleColor colorServer = ConsoleColor.DarkYellow;
const int SERVER_PORT = 4000;
const string SERVER_IP = "127.0.0.1";
IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Parse(SERVER_IP),SERVER_PORT);
ReceivingAndSendingMessanges.Messange messange = new ReceivingAndSendingMessanges.Messange();

using var socketServer = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

socketServer.Bind(iPEndPoint);
socketServer.Listen();
ColorMessang("Server Start", colorServer);

Socket clientSocet = await socketServer.AcceptAsync();
messange.SendMessage(clientSocet, "Connecting Player1");
ColorMessang("Connecting Player1", colorServer);

Console.ReadLine();



void ColorMessang(string str, ConsoleColor color)
{
    Console.ForegroundColor = color;
    Console.WriteLine(str);
    Console.ResetColor();
}