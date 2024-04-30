using Client.Controller;
using Client.Model;
using ObjectMessange;
using ReceivingAndSendingMessanges;
using System.Net;

namespace Client
{
    public partial class Form1 : Form
    {
        Players Player { get; set; }// гравець
        public TCPConecting TCPConnecting {  get; set; }
        public UDPConnecting UDPConnecting { get; set; }

        bool premissionToMove = true; // зм≥нна €ка дозвол€е х≥д гравц€
        bool premissionToFire = true; // зм≥нна €ка дозвол€е постр≥л
        Players Enemy {  get; set; }// супротивник
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Player = new Players();//створенн€ екземпл€ру гравц€
            Enemy = new Players();// створенн€ екземпл€ру ворога
        }

        private async void Mstrip_connectItem_Click(object sender, EventArgs e)
        {

            Mstrip_connectItem.Enabled = false;// блокуе кнопку Connect

            //TCPConnecting = new TCPConecting();// TCP
            UDPConnecting = new UDPConnecting();// UDP

            //bool b = await TCPConnecting.TCPConnectingToClientAsync();// створенн€ пидключенн€ до сервера TCP
            bool b = await UDPConnecting.UDPConnectingToServerAsync(); // створенн€ пидключенн€ до сервера UDP

            string str = string.Empty;
            if (!b)
            {
                MessageBox.Show("Not TCPConnecting");
                Mstrip_connectItem.Enabled = true;
                return;
            }

            //Players.TCPsocket = TCPConnecting.ClientSocket;// TCP
            Players.UDPClient = UDPConnecting.udpClient;// UDP
            Players.ServerIPEndPoint = UDPConnecting.udpIPEndPoint;// UDP
            Players.SetObjPlayer(Player);//UDP

            //str = await ReceivingAndSendingMessanges.TCPMessanges.TCPGetMessangeAsync(TCPConnecting.ClientSocket);//отриманн€ пов≥домленн€ з PlayerTag гравц€ (Player1 or Player2)
            str = await ReceivingAndSendingMessanges.UDPMessanges.UDPGetMessangeAsync(UDPConnecting.udpClient,new IPEndPoint(IPAddress.Any,0)); ;//отриманн€ пов≥домленн€ з PlayerTag гравц€ (Player1 or Player2)
            var obj = ObjectMessangePlayer.DesiarilizeFromJSON(str);

            //if (str.Equals("Player1") || str.Equals("Player2"))
            //{
            //    // створюемо гравц€
            //    Player.CreatePlayer(str);
            //    // виставл€емо координати на пол≥
            //    Player.StartPosition(Panel_gameField);
            //    // додаемо гравц€ на пол≥
            //    Panel_gameField.Controls.Add(Player.Picture);

            //}
            if (obj.ID == 1 || obj.ID == 2)
            {
                //створюемо гравц€
                Player.CreatePlayer(obj.ID);
                // виставл€емо координати на пол≥
                Player.StartPosition(Panel_gameField);
                // додаемо гравц€ на пол≥
                Panel_gameField.Controls.Add(Player.Picture);
            }

            //Enemy.TCPsocket = TCPConnecting.ClientSocket;///////////////////////////////


            if (Player.ID == 1)
            {
                //var s = await ReceivingAndSendingMessanges.TCPMessanges.TCPGetMessangeAsync(TCPConnecting.ClientSocket);//€кщо гравцю було присвоенно PlayerTag Player1 чекаемо п≥дключенн€ другого гравц€
                var s = await UDPMessanges.UDPGetMessangeAsync(UDPConnecting.udpClient,new IPEndPoint(IPAddress.Any,0));//€кщо гравцю було присвоенно PlayerTag Player1 чекаемо п≥дключенн€ другого гравц€
                var tempObjectPlaer2 = ObjectMessangePlayer.DesiarilizeFromJSON(s);

                if (tempObjectPlaer2.Command.Equals("Connecting Player2"))
                {
                    // додаемо противника на ≥грове поле
                    Enemy.CreateEnemi(Player.ID);
                    Enemy.StartPosition(Panel_gameField);
                    Panel_gameField.Controls.Add(Enemy.Picture);
                }
            }
            else
            {
                // додаемо противника на ≥грове поле
                Enemy.CreateEnemi(Player.ID);
                Enemy.StartPosition(Panel_gameField);
                Panel_gameField.Controls.Add(Enemy.Picture);
            }



            Task task = Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    //string temp = ReceivingAndSendingMessanges.TCPMessanges.TCPGetMessangeAsync(TCPConnecting.ClientSocket).Result;// TCP
                    string temp = ReceivingAndSendingMessanges.UDPMessanges.UDPGetMessangeAsync(UDPConnecting.udpClient, new IPEndPoint(IPAddress.Any, 0)).Result;// UDP
                    string commandServer = string.Empty;
                    try
                    {
                        ObjectMessangePlayer objectMessangePlayer = ObjectMessangePlayer.DesiarilizeFromJSON(temp);
                        Action action = () =>
                        {
                            commandServer = Players.GetObjPlayer(Enemy, objectMessangePlayer);
                        };
                        Invoke(action);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error");
                    }
                }
            });
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Space && premissionToMove)
            {
                Player.Move(e.KeyCode, Player.Speed);
                Players.SetObjPlayer(Player);
                premissionToMove = false;

            }
            else if (e.KeyCode == Keys.Space && premissionToFire)
            {
                Player.Fire(new Projectile());
                premissionToFire = false;
            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Foo();
            premissionToMove = true;
        }

        void Foo()
        {

            if (Player.listProjectile != null)
            {
                var item = Player.listProjectile;
                for (int i = 0; i < item.Count; i++)
                {
                    Keys keys = new Keys();

                    if (item[i].Vector == MyVector.TOP)
                        keys = Keys.Up;
                    else if (item[i].Vector == MyVector.BOTTOM)
                        keys = Keys.Down;
                    else if ((item[i].Vector == MyVector.LEFT))
                        keys = Keys.Left;
                    else if ((item[i].Vector == MyVector.RIGHT))
                        keys = Keys.Right;

                    item[i].Move(keys, item[i].Speed);
                    if (item[i].OutsideTheBorder(Panel_gameField))
                    {
                        item[i].Dispose();
                        item[i] = null;
                        Player.listProjectile.Remove(item[i]);

                        return;
                    }
                }
            }
            if (Enemy.listProjectile != null)
            {
                var item = Enemy.listProjectile;
                for (int i = 0; i < item.Count; i++)
                {
                    Keys keys = new Keys();

                    if (item[i].Vector == MyVector.TOP)
                        keys = Keys.Up;
                    else if (item[i].Vector == MyVector.BOTTOM)
                        keys = Keys.Down;
                    else if ((item[i].Vector == MyVector.LEFT))
                        keys = Keys.Left;
                    else if ((item[i].Vector == MyVector.RIGHT))
                        keys = Keys.Right;

                    item[i].Move(keys, item[i].Speed);
                    if (item[i].OutsideTheBorder(Panel_gameField))
                    {
                        item[i].Dispose();
                        item[i] = null;
                        Enemy.listProjectile.Remove(item[i]);

                        return;
                    }
                }
            }
        }

        private void timerFire_Tick(object sender, EventArgs e)
        {
            premissionToFire = true;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if(TCPConnecting != null)// TCP
            //{
            //    TCPConnecting.TCPClose();
            //}

            if (UDPConnecting != null)
            {
                UDPConnecting.UDPClose();
            }
        }
    }
}
