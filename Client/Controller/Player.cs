
using System.Numerics;
using System.Windows.Forms;

namespace Client.Controller
{
    public enum MyVector
    {
        TOP,
        RIGHT,
        BOTTOM,
        LEFT
    }
    public class Player
    {
        public int Speed {  get; set; }
        public Image Image { get; set; }

        public string PlayerTag { get; set; }

        public MyVector Vector { get; set; }
        public PictureBox Picture { get; set; }

        public Panel GamePanel { get; set; }

        public Projectile Projectile { get; set; }

        public Player()
        {
            Picture = new PictureBox();
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

        // рух танку по полю
        public void Move(Keys keys, int speed)
        {
            if (keys == Keys.Up && this.Vector == MyVector.TOP)
            {
                this.Picture.Location = new Point(this.Picture.Location.X, this.Picture.Location.Y - speed);
                return;
            }
            else if (keys == Keys.Left && this.Vector == MyVector.LEFT)
            {
                this.Picture.Location = new Point(this.Picture.Location.X-speed, this.Picture.Location.Y);
                return;
            }
            else if (keys == Keys.Right && this.Vector == MyVector.RIGHT)
            {
                this.Picture.Location = new Point(this.Picture.Location.X+speed, this.Picture.Location.Y);
                return;
            }
            else if (keys == Keys.Down && this.Vector == MyVector.BOTTOM)
            {
                this.Picture.Location = new Point(this.Picture.Location.X, this.Picture.Location.Y + speed);
                return;
            }
            Rotate(keys);
        }



        public void Fire(Projectile projectile)
        {
            if (this.Vector == MyVector.TOP)
            {
                if(projectile.pictureBox.Location.X > GamePanel.Location.X+30)
                {
                    projectile.pictureBox.Location = new Point(this.Picture.Location.X + 16, this.Picture.Location.Y - 30);
                    GamePanel.Controls.Add(projectile.pictureBox);
                }
            }
            else if (this.Vector == MyVector.BOTTOM)
            {
                if (projectile.pictureBox.Location.Y > GamePanel.Location.Y - 30)
                {
                    projectile.pictureBox.Location = new Point(this.Picture.Location.X + 16, this.Picture.Location.Y - 30);
                    GamePanel.Controls.Add(projectile.pictureBox);
                }
            }
        }

        public void Position(Panel panel)
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

        //розвертае Image
        public void Rotate(Keys keys)
        {
            if (keys == Keys.Up)/////////////////////////////
            {
                if (this.Vector == MyVector.RIGHT)
                {
                    this.Image.RotateFlip(RotateFlipType.Rotate270FlipNone);
                    this.Picture.Image = this.Image;
                    this.Vector = MyVector.TOP;
                }
                else if (this.Vector == MyVector.LEFT)
                {
                    this.Image.RotateFlip(RotateFlipType.Rotate90FlipNone);
                    this.Picture.Image = this.Image;
                    this.Vector = MyVector.TOP;
                }
                else if (this.Vector == MyVector.BOTTOM)
                {
                    this.Image.RotateFlip(RotateFlipType.Rotate180FlipNone);
                    this.Picture.Image = this.Image;
                    this.Vector = MyVector.TOP;
                }
            }
            else if (keys == Keys.Left)// поворот в ліво
            {
                if (this.Vector == MyVector.TOP) 
                    this.Image.RotateFlip(RotateFlipType.Rotate270FlipNone);
                else if (this.Vector == MyVector.RIGHT)
                    this.Image.RotateFlip(RotateFlipType.Rotate180FlipNone);
                else if (this.Vector == MyVector.BOTTOM)
                    this.Image.RotateFlip(RotateFlipType.Rotate90FlipNone);

                this.Picture.Image = this.Image;
                this.Vector = MyVector.LEFT;
            }
            else if(keys == Keys.Right)// поворот в право
            {
                if (this.Vector == MyVector.TOP)
                    this.Image.RotateFlip(RotateFlipType.Rotate90FlipNone);
                else if (this.Vector == MyVector.LEFT)
                    this.Image.RotateFlip(RotateFlipType.Rotate180FlipNone);
                else if (this.Vector == MyVector.BOTTOM)
                    this.Image.RotateFlip(RotateFlipType.Rotate270FlipNone);

                this.Picture.Image = this.Image;
                this.Vector = MyVector.RIGHT;
            }
            else if(keys == Keys.Down)// поворот в низ
            {
                if (this.Vector == MyVector.TOP)
                    this.Image.RotateFlip(RotateFlipType.Rotate180FlipNone);
                else if (this.Vector == MyVector.LEFT)
                    this.Image.RotateFlip(RotateFlipType.Rotate270FlipNone);
                else if (this.Vector == MyVector.RIGHT)
                    this.Image.RotateFlip(RotateFlipType.Rotate90FlipNone);

                this.Picture.Image = this.Image;
                this.Vector = MyVector.BOTTOM;
            }

        }

    }
}
