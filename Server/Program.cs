﻿// See https://aka.ms/new-console-template for more information
using ObjectMessange;
using ReceivingAndSendingMessanges;
using System.Net;
using System.Net.Sockets;
using Newtonsoft.Json;

ConsoleColor colorServer = ConsoleColor.DarkYellow;
ConsoleColor colorClient1 = ConsoleColor.DarkBlue;
ConsoleColor colorClient2 = ConsoleColor.DarkGreen;
IPEndPoint sender1 = new IPEndPoint(IPAddress.Any, 0);
IPEndPoint sender2 = new IPEndPoint(IPAddress.Any, 0);

int playerIndex = 1;
const int SERVER_PORT = 4000;
const string SERVER_IP = "127.0.0.1";
IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Parse(SERVER_IP),SERVER_PORT);

UdpClient udpServer = new UdpClient(iPEndPoint);
ColorMessang("Server Start", colorServer);

//connect player 1
string mess = ReceivingAndSendingMessanges.UDPMessanges.UDPGetMessange(udpServer,ref sender1);
ColorMessang("Connect Player1", colorServer);

var player1 = ObjectMessangePlayer.DesiarilizeFromJSON(mess);
player1.ID = playerIndex;
playerIndex++;
ColorMessang(mess, colorClient1);

mess = ObjectMessangePlayer.SerializeToJSON(player1);
UDPMessanges.UDPSendMessage(udpServer, sender1, mess);
ColorMessang($"->{mess}",colorServer);

//connect player 2
mess = string.Empty;
mess = UDPMessanges.UDPGetMessange(udpServer, ref sender2);
ColorMessang("Connect Player2", colorServer);

var player2 = ObjectMessangePlayer.DesiarilizeFromJSON(mess);
player2.ID = playerIndex;
playerIndex = 1;
ColorMessang(mess, colorClient2);

player2.Command = "Connecting Player2";
mess = ObjectMessangePlayer.SerializeToJSON(player2);
UDPMessanges.UDPSendMessage(udpServer, sender2, mess);
ColorMessang($"->{mess}", colorServer);

// send messange player 1 what player 2 is connect
UDPMessanges.UDPSendMessage(udpServer, sender1, mess);
ColorMessang($"->Player1 {mess}", colorServer);

Task task1 = Task.Factory.StartNew(() =>
{
    string MessangeClient = string.Empty;
    ObjectMessangePlayer obj = new ObjectMessangePlayer();
    IPEndPoint tempEndPoint = new IPEndPoint(IPAddress.Any, 0);//заглушка
    while (true)
    {
        MessangeClient = UDPMessanges.UDPGetMessange(udpServer, ref tempEndPoint);
        
        dynamic d = JsonConvert.DeserializeObject(MessangeClient);
        if (d != null) 
        {
            if (d.ID == 1)
            {
                ColorMessang(MessangeClient, colorClient1);
                UDPMessanges.UDPSendMessage(udpServer, sender2, MessangeClient);
                ColorMessang("->Player2" + MessangeClient, colorServer);
            }
            else if (d.ID == 2)
            {
                ColorMessang(MessangeClient, colorClient2);
                UDPMessanges.UDPSendMessage(udpServer, sender1, MessangeClient);
                ColorMessang("->Player1" + MessangeClient, colorServer);
            }
        }
        
    }
});








Console.ReadLine();
#region TCP
////ReceivingAndSendingMessanges.TCPMessanges messange = new ReceivingAndSendingMessanges.TCPMessanges();

//// підключення сервера
//using var socketServer = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

//socketServer.Bind(iPEndPoint);
//socketServer.Listen();
//ColorMessang("Server Start", colorServer);


//// підключення першого гравця
//Socket clientSocet_1 = await socketServer.AcceptAsync();
//ReceivingAndSendingMessanges.TCPMessanges.TCPSendMessage(clientSocet_1, "Player1");
//ColorMessang("Connecting Player1", colorServer);

//// підключення другого гравця
//Socket clientSocet_2 = await socketServer.AcceptAsync();
//ReceivingAndSendingMessanges.TCPMessanges.TCPSendMessage(clientSocet_2, "Player2");
//ColorMessang("Connecting Player2", colorServer);

//// відправка першому гравцю повідомлення про підключення дрyгого гравця
//ReceivingAndSendingMessanges.TCPMessanges.TCPSendMessage(clientSocet_1, "Connecting Player2");


//Task task1 = Task.Factory.StartNew(() =>
//{
//    string MessangeClient1 = string.Empty;
//    ObjectMessangePlayer obj = new ObjectMessangePlayer();
//    while (true)
//    {
//        MessangeClient1 = ReceivingAndSendingMessanges.TCPMessanges.TCPGetMessange(clientSocet_1);
//        ReceivingAndSendingMessanges.TCPMessanges.TCPSendMessage(clientSocet_2, MessangeClient1);
//        ColorMessang(MessangeClient1, colorClient1);
//    }
//});

//Task task2 = Task.Factory.StartNew(() => 
//{
//    string MessangeClient2 = string.Empty;
//    while (true)
//    {
//        MessangeClient2 = ReceivingAndSendingMessanges.TCPMessanges.TCPGetMessange(clientSocet_2);
//        ReceivingAndSendingMessanges.TCPMessanges.TCPSendMessage(clientSocet_1, MessangeClient2);
//        ColorMessang(MessangeClient2, colorClient2);
//    }
//});
#endregion

udpServer.Close();
udpServer.Dispose();


Console.ReadLine();





void ColorMessang(string str, ConsoleColor color)
{
    Console.ForegroundColor = color;
    Console.WriteLine(str);
    Console.ResetColor();
}