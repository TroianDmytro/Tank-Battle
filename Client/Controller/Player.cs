
using System.Numerics;
using System.Windows.Forms;

namespace Client.Controller
{
    public class Player: ObjectOfThePlayingField
    {
        public string PlayerTag { get; set; }

        public Panel GamePanel { get; set; }

        public Projectile Projectile { get; set; }

        public List<Projectile> listProjectile;

        public Player()
        {
            Picture = new PictureBox();
            listProjectile = new();
        }

        public bool CreatePlayer(string playerTag)
        {
            if (playerTag == "Player1") 
            {
                Vector = MyVector.TOP;
                Image = new Bitmap(Properties.Resources.unnamed_2);
            }
            else if(playerTag == "Player2")
            {
                Vector = MyVector.BOTTOM;
                Image = new Bitmap(Properties.Resources.unnamed_150x150);
            }
            else
            {
                return false;
            }

            Picture.Size = new Size(60, 60);
            Picture.SizeMode = PictureBoxSizeMode.Zoom;
            Picture.Image = this.Image;

            PlayerTag = playerTag;
            Speed = 20;
            return true;
        }

        public void Fire(Projectile projectile)
        {
            if (this.Vector == MyVector.TOP)
            {
                if(this.Picture.Location.X > GamePanel.Location.X+30)
                {
                    projectile.Rotate(Keys.Up);
                    projectile.Picture.Location = new Point(this.Picture.Location.X + 16, this.Picture.Location.Y - 30);
                    //gamePanel.Controls.Add(projectile.Picture);
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
                    //gamePanel.Controls.Add(projectile.Picture);
                }
            }
            else if (this.Vector == MyVector.RIGHT)
            {
                if (this.Picture.Location.X < GamePanel.Size.Width - 30)
                {
                    projectile.Rotate(Keys.Right);
                    projectile.Picture.Location = new Point(this.Picture.Location.X + this.Picture.Width + 30 , this.Picture.Location.Y +16);
                    //gamePanel.Controls.Add(projectile.Picture);
                }
            }
            GamePanel.Controls.Add(projectile.Picture);
            listProjectile.Add(projectile);
        }

        
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
        public void SetVector( MyVector vector)
        {
            if (vector == MyVector.TOP) 
            {
                
            }

        }

        

    }
}
