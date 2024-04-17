using Client.Controller;
using Client.Model;

namespace Client
{
    public partial class Form1 : Form
    {
        Player Player { get; set; }
        Conecting conecting;
        bool premissionToMove = true;
        bool premissionToFire = true;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Player = new Player();

        }

        private void Mstrip_connectItem_Click(object sender, EventArgs e)
        {
            conecting = new Conecting();
            Player.CreatePlayer("Player1");
            Player.StartPosition(Panel_gameField);
            Panel_gameField.Controls.Add(Player.Picture);

            //Player.Fire(new Projectile());


            //Player player2 = new Player();
            //player2.CreatePlayer("Player2");
            //player2.StartPosition(Panel_gameField);
            //if (!conecting.ConnectingToServer())
            //{
            //    MessageBox.Show("Not connecting");
            //    return;
            //}
            //Panel_gameField.Controls.Add(player2.Image);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Space && premissionToMove)
            {
                Player.Move(e.KeyCode, Player.Speed);
            }
            else if (e.KeyCode == Keys.Space && premissionToFire)
            {
                Player.Fire(new Projectile());
            }

            premissionToMove = premissionToFire = false;
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
                    if (Player.OutsideTheBorder(item[i]))
                    {
                        item[i].Dispose();
                        item[i] = null;
                        Player.listProjectile.Remove(item[i]);

                        return;
                    }
                }
            }

        }

        private void timerFire_Tick(object sender, EventArgs e)
        {
            premissionToFire = true;
        }
    }
}
