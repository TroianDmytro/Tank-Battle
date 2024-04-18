// See https://aka.ms/new-console-template for more information

using ObjectMessange;
using ReceivingAndSendingMessanges;
using System.Net;
using System.Net.Sockets;


ConsoleColor colorServer = ConsoleColor.DarkYellow;
ConsoleColor colorClient1 = ConsoleColor.DarkBlue;
ConsoleColor colorClient2 = ConsoleColor.DarkGreen;

const int SERVER_PORT = 4000;
const string SERVER_IP = "127.0.0.1";
IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Parse(SERVER_IP),SERVER_PORT);
ReceivingAndSendingMessanges.Messange messange = new ReceivingAndSendingMessanges.Messange();

// підключення сервера
using var socketServer = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

socketServer.Bind(iPEndPoint);
socketServer.Listen();
ColorMessang("Server Start", colorServer);


// підключення першого гравця
Socket clientSocet_1 = await socketServer.AcceptAsync();
ReceivingAndSendingMessanges.Messange.SendMessage(clientSocet_1, "Player1");
ColorMessang("Connecting Player1", colorServer);

// підключення другого гравця
Socket clientSocet_2 = await socketServer.AcceptAsync();
ReceivingAndSendingMessanges.Messange.SendMessage(clientSocet_2, "Player2");
ColorMessang("Connecting Player2", colorServer);

// відправка першому гравцю повідомлення про підключення дрyгого гравця
ReceivingAndSendingMessanges.Messange.SendMessage(clientSocet_1, "Connecting Player2");

Task task1 = Task.Factory.StartNew(() =>
{
    string MessangeClient1 = string.Empty;
    ObjectMessangePlayer obj = new ObjectMessangePlayer();
    while (true)
    {
        MessangeClient1 = ReceivingAndSendingMessanges.Messange.GetMessange(clientSocet_1);
        ReceivingAndSendingMessanges.Messange.SendMessage(clientSocet_2, MessangeClient1);
        ColorMessang(MessangeClient1, colorClient1);

    }
});

Task task2 = Task.Factory.StartNew(() => 
{
    string MessangeClient2 = string.Empty;
    while (true)
    {
        MessangeClient2 = ReceivingAndSendingMessanges.Messange.GetMessange(clientSocet_2);
        ReceivingAndSendingMessanges.Messange.SendMessage(clientSocet_1, MessangeClient2);
        ColorMessang(MessangeClient2, colorClient2);

    }
});




Console.ReadLine();





void ColorMessang(string str, ConsoleColor color)
{
    Console.ForegroundColor = color;
    Console.WriteLine(str);
    Console.ResetColor();
}