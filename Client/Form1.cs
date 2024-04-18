using Client.Controller;
using Client.Model;
using ObjectMessange;
using ReceivingAndSendingMessanges;

namespace Client
{
    public partial class Form1 : Form
    {
        Players Player { get; set; }
        public Conecting connecting;
        bool premissionToMove = true;
        bool premissionToFire = true;
        Players Enemy {  get; set; }
        Messange messange;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Player = new Players();
            Enemy = new Players();

        }

        private async void Mstrip_connectItem_Click(object sender, EventArgs e)
        {
            Mstrip_connectItem.Enabled = false;
            connecting = new Conecting();
            messange = new Messange();

            bool b = await connecting.ConnectingToClientAsync();
            string str = string.Empty;
            if (!b)
            {
                MessageBox.Show("Not connecting");
                Mstrip_connectItem.Enabled = true;
                return;
            }
            Players.socket = connecting.ClientSocket;
            str = await ReceivingAndSendingMessanges.Messange.GetMessangeAsync(connecting.ClientSocket);

            if (str.Equals("Player1") || str.Equals("Player2"))
            {
                // створюемо гравц€
                Player.CreatePlayer(str);
                // виставл€емо координати на пол≥
                Player.StartPosition(Panel_gameField);
                // додаемо гравц€ на пол≥
                Panel_gameField.Controls.Add(Player.Picture);

            }

            //Enemy.socket = connecting.ClientSocket;///////////////////////////////

            if (Player.PlayerTag == "Player1")
            {
                var s = await ReceivingAndSendingMessanges.Messange.GetMessangeAsync(connecting.ClientSocket);
                if (s.Equals("Connecting Player2"))
                {
                    // додаемо противника на ≥грове поле
                    Enemy.CreateEnemi(str);
                    Enemy.StartPosition(Panel_gameField);
                    Panel_gameField.Controls.Add(Enemy.Picture);
                }
            }
            else
            {
                // додаемо противника на ≥грове поле
                Enemy.CreateEnemi(str);
                Enemy.StartPosition(Panel_gameField);
                Panel_gameField.Controls.Add(Enemy.Picture);
            }



            Task task = Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    string temp = ReceivingAndSendingMessanges.Messange.GetMessangeAsync(connecting.ClientSocket).Result;
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
                //Players.SetObjPlayer(new Projectile());
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
            if(connecting != null)
            {
                connecting.Close();
            }
        }
    }
}
