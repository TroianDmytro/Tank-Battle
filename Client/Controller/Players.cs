
using ObjectMessange;
using ReceivingAndSendingMessanges;
using System.Net;
using System.Net.Sockets;

namespace Client.Controller
{
    public class Players: ObjectOfThePlayingField
    {
        public string PlayerTag { get; set; }

        public Panel GamePanel;

        public Projectile Projectile { get; set; }

        public List<Projectile> listProjectile;

        public static Socket TCPsocket {  get; set; }

        public static UdpClient UDPClient { get; set; }

        public static IPEndPoint ServerIPEndPoint { get; set; }
        public Players()
        {
            Picture = new PictureBox();
            listProjectile = new();
        }
        

        // відправляем обьект ObjectMessangePlayer на сервер
        public static void SetObjPlayer(ObjectOfThePlayingField player)
        {
            ObjectMessangePlayer obj = new ObjectMessangePlayer();

            if (player.Vector == MyVector.TOP)
                obj.VectorProjectile = "Top";
            else if (player.Vector == MyVector.BOTTOM)
                obj.VectorProjectile = "Bottom";
            else if (player.Vector == MyVector.LEFT)
                obj.VectorProjectile = "Left";
            else if (player.Vector == MyVector.RIGHT)
                obj.VectorProjectile = "Right";

            if (player is Players)
            {
                Players objPlayer = player as Players;
                obj.Name = "Player";
                obj.LocationPlayerX = objPlayer.Picture.Location.X;
                obj.LocationPlayerY = objPlayer.Picture.Location.Y;
            }
            else if(player is Projectile)
            {
                Projectile objPlayer = player as Projectile;
                obj.Name = "Projectile";
                obj.LocationPlayerX = objPlayer.Picture.Location.X;
                obj.LocationPlayerY = objPlayer.Picture.Location.Y;
            }

            obj.ID = player.ID;
            string objJSON = ObjectMessangePlayer.SerializeToJSON(obj);
            UDPMessanges.UDPSendMessage(Players.UDPClient, objJSON);
        }

        // опримуемо об'ект ObjectMessangePlayer 
        public static string GetObjPlayer(Players enemy, ObjectMessangePlayer obj)
        {
            if (obj == null) return null;

            Keys keys = new();

            if (obj.Name.Equals("Player"))
            {
                enemy.Picture.Location = new Point(obj.LocationPlayerX, obj.LocationPlayerY);
                if (obj.VectorProjectile.Equals("Top"))
                {
                    keys = Keys.Up;
                }
                else if (obj.VectorProjectile.Equals("Left"))
                {
                    keys = Keys.Left;
                }
                else if (obj.VectorProjectile.Equals("Right"))
                {
                    keys = Keys.Right;
                }
                else if (obj.VectorProjectile.Equals("Bottom"))
                {
                    keys = Keys.Down;
                }
                enemy.Rotate(keys);
            }
            else if (obj.Name.Equals("Projectile"))
            {
                Projectile projectile = new Projectile();
                projectile.Picture.Location = new Point(obj.LocationPlayerX,obj.LocationPlayerY);
                if (obj.VectorProjectile.Equals("Top"))
                {
                    keys = Keys.Up;
                }
                else if (obj.VectorProjectile.Equals("Left"))
                {
                    keys = Keys.Left;
                }
                else if (obj.VectorProjectile.Equals("Right"))
                {
                    keys = Keys.Right;
                }
                else if (obj.VectorProjectile.Equals("Bottom"))
                {
                    keys = Keys.Down;
                }
                projectile.Rotate(keys);
                enemy.GamePanel.Controls.Add(projectile.Picture);
                enemy.listProjectile.Add(projectile);
            }

            return obj.Command;

        }

        // створення противника
        public void CreateEnemi(int idPlayer)
        {
            if (idPlayer == 1)
            {
                CreatePlayer(2);
            }
            else if (idPlayer == 2)
            {
                CreatePlayer(1);
            }
        }

        // створення гравця
        public bool CreatePlayer(int idPlayer)
        {
            if (idPlayer == 1) 
            {
                Vector = MyVector.TOP;
                Image = new Bitmap(Properties.Resources.unnamed_2);
                PlayerTag = "Player1";
            }
            else if(idPlayer == 2)
            {
                Vector = MyVector.TOP;
                Image = new Bitmap(Properties.Resources.unnamed_150x150);
                this.Rotate(Keys.Down);
                PlayerTag = "Player2";
            }
            else
            {
                return false;
            }

            Picture.Size = new Size(60, 60);
            Picture.SizeMode = PictureBoxSizeMode.Zoom;
            Picture.Image = this.Image;

            this.ID = idPlayer;
            Speed = 15;
            return true;
        }

        // постріл
        public void Fire(Projectile projectile)
        {
            if (this.Vector == MyVector.TOP)
            {
                if(this.Picture.Location.X > GamePanel.Location.X+30)
                {
                    projectile.Rotate(Keys.Up);
                    projectile.Picture.Location = new Point(this.Picture.Location.X + 16, this.Picture.Location.Y - 30);
                }
            }
            else if (this.Vector == MyVector.BOTTOM)
            {
                if (this.Picture.Location.Y <  GamePanel.Size.Height - 30)
                {
                    projectile.Rotate(Keys.Down);
                    projectile.Picture.Location = new Point(this.Picture.Location.X + 16, this.Picture.Location.Y + this.Picture.Height + 30); ;
                }
            }
            else if (this.Vector == MyVector.LEFT)
            {
                if (this.Picture.Location.X > GamePanel.Location.X + 30)
                {
                    projectile.Rotate(Keys.Left);
                    projectile.Picture.Location = new Point(this.Picture.Location.X - 30, this.Picture.Location.Y + 16);
                }
            }
            else if (this.Vector == MyVector.RIGHT)
            {
                if (this.Picture.Location.X < GamePanel.Size.Width - 30)
                {
                    projectile.Rotate(Keys.Right);
                    projectile.Picture.Location = new Point(this.Picture.Location.X + this.Picture.Width + 30 , this.Picture.Location.Y +16);
                }
            }
            projectile.ID = this.ID;
            Players.SetObjPlayer(projectile);
            GamePanel.Controls.Add(projectile.Picture);
            listProjectile.Add(projectile);

        }

        // початкова позиція гравців
        public void StartPosition(Panel panel)
        {
            GamePanel = panel;

            if (this.PlayerTag == "Player1")
            {
                Picture.Location = new Point(panel.Width / 2 - Picture.Width / 2 - 10, (panel.Height - Picture.Height-10));
            }
            else if(this.PlayerTag == "Player2")
            {
                Picture.Location = new Point(panel.Width / 2 - Picture.Width / 2 - 10, 10);
            }
        }
        

    }
}
