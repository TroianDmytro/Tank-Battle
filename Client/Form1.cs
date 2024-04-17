using Client.Controller;
using Client.Model;

namespace Client
{
    public partial class Form1 : Form
    {
        Player Player { get; set; }
        Conecting conecting;
        bool premissionToMove = true;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void Mstrip_connectItem_Click(object sender, EventArgs e)
        {
            conecting = new Conecting();
            Player = new Player();
            Player.CreatePlayer("Player1");
            Player.Position(Panel_gameField);
            Panel_gameField.Controls.Add(Player.Picture);

            //Player.Fire(new Projectile());


            //Player player2 = new Player();
            //player2.CreatePlayer("Player2");
            //player2.Position(Panel_gameField);
            //if (!conecting.ConnectingToServer())
            //{
            //    MessageBox.Show("Not connecting");
            //    return;
            //}
            //Panel_gameField.Controls.Add(player2.Image);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Space)
            {
                Player.Move(e.KeyCode);
            }
            else if (e.KeyCode == Keys.Space)
            {
                Player.Fire(new Projectile());
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }
    }
}
