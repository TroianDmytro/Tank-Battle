
using ObjectMessange;
using System.Net.Sockets;
using System.Numerics;
using System.Windows.Forms;

namespace Client.Controller
{
    public class Players: ObjectOfThePlayingField
    {
        public string PlayerTag { get; set; }

        public Panel GamePanel;

        public Projectile Projectile { get; set; }

        public List<Projectile> listProjectile;

        public Socket socket {  get; set; }

        public Players()
        {
            Picture = new PictureBox();
            listProjectile = new();

        }
        

        // відправляем обьект ObjectMessangePlayer на сервер
        public static void SetObjPlayer(Players player)
        {
            ObjectMessangePlayer obj = new ObjectMessangePlayer();
            obj.LocationPlayerX = player.Picture.Location.X;
            obj.LocationPlayerY = player.Picture.Location.Y;
            obj.Name = "Player";

            if (player.Vector == MyVector.TOP)
                obj.VectorProjectile = "Top";
            else if (player.Vector == MyVector.BOTTOM)
                obj.VectorProjectile = "Bottom";
            else if (player.Vector == MyVector.LEFT)
                obj.VectorProjectile = "Left";
            else if (player.Vector == MyVector.RIGHT)
                obj.VectorProjectile = "Right";

            string objJSON = ObjectMessangePlayer.SerializeToJSON(obj);
            ReceivingAndSendingMessanges.Messange.SendMessage(player.socket, objJSON);
        }

        // опримуемо об'ект ObjectMessangePlayer 
        public static string GetObjPlayer(Players enemy, ObjectMessangePlayer obj)
        {
            if (obj == null) return null;

            if (obj.Name.Equals("Player") || obj.Name.Equals("Player"))
            {
                Keys keys = new Keys();
                enemy.Picture.Location = new Point(obj.LocationPlayerX, obj.LocationPlayerY);
                if (obj.VectorProjectile.Equals("Top"))
                {
                    //enemy.Vector = MyVector.TOP;
                    keys = Keys.Up;
                }
                else if (obj.VectorProjectile.Equals("Left"))
                {
                    //enemy.Vector = MyVector.LEFT;
                    keys = Keys.Left;
                }
                else if (obj.VectorProjectile.Equals("Right"))
                {
                    //enemy.Vector = MyVector.RIGHT;
                    keys = Keys.Right;
                }
                else if (obj.VectorProjectile.Equals("Bottom"))
                {
                    //enemy.Vector = MyVector.BOTTOM;
                    keys = Keys.Down;
                }
                enemy.Rotate(keys);
            }
            return obj.Command;

        }

        // створення противника
        public void CreateEnemi(string playerTag)
        {
            if (playerTag.Equals("Player1"))
            {
                CreatePlayer("Player2");
            }
            else if (playerTag == "Player2")
            {
                CreatePlayer("Player1");
            }
        }

        // створення гравця
        public bool CreatePlayer(string playerTag)
        {
            if (playerTag == "Player1") 
            {
                Vector = MyVector.TOP;
                Image = new Bitmap(Properties.Resources.unnamed_2);
            }
            else if(playerTag == "Player2")
            {
                Vector = MyVector.TOP;
                Image = new Bitmap(Properties.Resources.unnamed_150x150);
                this.Rotate(Keys.Down);
            }
            else
            {
                return false;
            }

            Picture.Size = new Size(60, 60);
            Picture.SizeMode = PictureBoxSizeMode.Zoom;
            Picture.Image = this.Image;

            PlayerTag = playerTag;
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
                    GamePanel.Controls.Add(projectile.Picture);
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
            GamePanel.Controls.Add(projectile.Picture);
            listProjectile.Add(projectile);
        }

        // початкова погиція гравців
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
