using Client.Controller;
using Client.Model;
using ObjectMessange;
using ReceivingAndSendingMessanges;
using System.Net;

namespace Client
{
    public partial class Form1 : Form
    {
        Players Player { get; set; }// �������
        public TCPConecting TCPConnecting {  get; set; }
        public UDPConnecting UDPConnecting { get; set; }

        bool premissionToMove = true; // ����� ��� �������� ��� ������
        bool premissionToFire = true; // ����� ��� �������� ������
        Players Enemy {  get; set; }// �����������
        CancellationTokenSource cancelTokSSrc;
        public Form1()
        {
            InitializeComponent();
            cancelTokSSrc = new CancellationTokenSource();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Player = new Players();//��������� ���������� ������
            Enemy = new Players();// ��������� ���������� ������
        }

        private async void Mstrip_connectItem_Click(object sender, EventArgs e)
        {

            Mstrip_connectItem.Enabled = false;// ������ ������ Connect

            UDPConnecting = new UDPConnecting();// UDP

            /*bool b =*/ await UDPConnecting.UDPConnectingToServerAsync(); // ��������� ����������� �� ������� UDP

            string str = string.Empty;
            //if (!b)
            //{
                
            //}

            Players.UDPClient = UDPConnecting.udpClient;// UDP
            Players.ServerIPEndPoint = UDPConnecting.udpIPEndPoint;// UDP
            Players.SetObjPlayer(Player);//UDP

            // �������� ���������� �� �������
            try
            {
                str = await UDPMessanges.UDPGetMessangeAsync(UDPConnecting.udpClient, new IPEndPoint(IPAddress.Any, 0)); ;//��������� ����������� � PlayerTag ������ (Player1 or Player2)
            }
            catch (Exception)
            {
                MessageBox.Show("Not TCPConnecting");
                Mstrip_connectItem.Enabled = true;
                return;
            }

            var obj = ObjectMessangePlayer.DesiarilizeFromJSON(str);

            
            if (obj.ID == 1 || obj.ID == 2)
            {
                //��������� ������
                Player.CreatePlayer(obj.ID);
                // ����������� ���������� �� ���
                Player.StartPosition(Panel_gameField);
                // ������� ������ �� ���
                Panel_gameField.Controls.Add(Player.Picture);
            }


            if (Player.ID == 1)
            {
                string s = string.Empty;
                try
                {
                    s = await UDPMessanges.UDPGetMessangeAsync(UDPConnecting.udpClient, new IPEndPoint(IPAddress.Any, 0));//���� ������ ���� ���������� PlayerTag Player1 ������� ���������� ������� ������
                }
                catch (Exception)
                {
                    Mstrip_connectItem.Enabled = true;
                    return;
                }

                var tempObjectPlaer2 = ObjectMessangePlayer.DesiarilizeFromJSON(s);

                if (tempObjectPlaer2.Command.Equals("Connecting Player2"))
                {
                    // ������� ���������� �� ������ ����
                    Enemy.CreateEnemi(Player.ID);
                    Enemy.StartPosition(Panel_gameField);
                    Panel_gameField.Controls.Add(Enemy.Picture);
                }
            }
            else
            {
                // ������� ���������� �� ������ ����
                Enemy.CreateEnemi(Player.ID);
                Enemy.StartPosition(Panel_gameField);
                Panel_gameField.Controls.Add(Enemy.Picture);
            }


            Task task = Task.Factory.StartNew((ct) =>
            {
                CancellationToken cancelTok = (CancellationToken)ct;

                while (!cancelTok.IsCancellationRequested)
                {
                    try
                    {
                        string temp = UDPMessanges.UDPGetMessangeAsync(UDPConnecting.udpClient, new IPEndPoint(IPAddress.Any, 0)).Result;// UDP
                        string commandServer = string.Empty;

                        ObjectMessangePlayer objectMessangePlayer = ObjectMessangePlayer.DesiarilizeFromJSON(temp);
                        Action action = () =>
                        {
                            commandServer = Players.GetObjPlayer(Enemy, objectMessangePlayer);
                        };
                        Invoke(action);
                    }
                    catch(AggregateException agEx)
                    {
                        DialogResult result = MessageBox.Show("Disconection",$"{Player.ID}");
                        if (result == DialogResult.OK) 
                        {
                            Action action = (() =>
                            {
                                Mstrip_connectItem.Enabled = true;
                            });
                            Invoke(action);
                            break;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error");
                    }
                }
            },cancelTokSSrc.Token,cancelTokSSrc.Token);

        }
        // ������� ������ ����� Up, Down, Left, Right, Space
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
        // ��������� ����
        private void timer1_Tick(object sender, EventArgs e)
        {
            Foo();
            premissionToMove = true;
        }

        //�����i��� �������� ������� �� ����
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
        // ��������� ��������
        private void timerFire_Tick(object sender, EventArgs e)
        {
            //����� �� ������
            premissionToFire = true;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            

            if (UDPConnecting != null)
            {
                ObjectMessangePlayer command = new ObjectMessangePlayer();
                command.Command = "Close";
                string objJSON = ObjectMessangePlayer.SerializeToJSON(command);
                UDPMessanges.UDPSendMessage(Players.UDPClient, objJSON);
                
                cancelTokSSrc.Cancel();

                UDPConnecting.UDPClose();
            }
        }
    }
}
