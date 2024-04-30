
using ObjectMessange;

namespace Client.Controller
{
    // направлення гравця
    public enum MyVector
    {
        TOP,
        RIGHT,
        BOTTOM,
        LEFT
    }
    // обьек ігрового поля
    public class ObjectOfThePlayingField
    {
        public int ID {  get; set; }
        public int Speed { get; set; }
        public Image Image;
        public MyVector Vector { get; set; }

        public PictureBox Picture;

        // перевіряе вихід за ігрове поле
        public bool OutsideTheBorder(Panel gamePanel)
        {
            int i = 0;
            int j = 30;
            int q = 30;

            bool result = false;
            if (this == null)
                return result;
            Players player = this as Players;

            if (player != null)
            {
                i = 10;
                j = 70;
                q = 80;
            }

            if (this.Vector == MyVector.TOP)
            {
                if (this.Picture.Location.Y < i)
                {
                    result = true;
                }
            }
            else if (this.Vector == MyVector.BOTTOM)
            {
                if (this.Picture.Location.Y > gamePanel.Height - j)
                {
                    result = true;
                }
            }
            else if (this.Vector == MyVector.LEFT)
            {
                if (this.Picture.Location.X < i)
                {
                    result = true;
                }
            }
            else if (this.Vector == MyVector.RIGHT)
            {
                if (this.Picture.Location.X > gamePanel.Width - q)
                {
                    result = true;
                }
            }

            return result;
        }

        // рух по полю
        public void Move(Keys keys, int speed)
        {
            if (keys == null || this == null)
            {
                return;
            }
            Players player = this as Players;

            var location = this.Picture.Location;
            if (keys == Keys.Up && this.Vector == MyVector.TOP)
            {
                location = new Point(this.Picture.Location.X, this.Picture.Location.Y - speed);
                if (player != null && player.OutsideTheBorder(player.GamePanel))
                    location = new Point(this.Picture.Location.X, this.Picture.Location.Y + speed);
            }
            else if (keys == Keys.Left && this.Vector == MyVector.LEFT)
            {
                location = new Point(this.Picture.Location.X - speed, this.Picture.Location.Y);
                if (player != null && player.OutsideTheBorder(player.GamePanel))
                    location = new Point(this.Picture.Location.X + speed, this.Picture.Location.Y );
            }
            else if (keys == Keys.Right && this.Vector == MyVector.RIGHT)
            {
                location = new Point(this.Picture.Location.X + speed, this.Picture.Location.Y);
                if (player != null && player.OutsideTheBorder(player.GamePanel))
                    location = new Point(this.Picture.Location.X - speed, this.Picture.Location.Y);
            }
            else if (keys == Keys.Down && this.Vector == MyVector.BOTTOM)
            {
                location = new Point(this.Picture.Location.X, this.Picture.Location.Y + speed);
                if (player != null && player.OutsideTheBorder(player.GamePanel))
                    location = new Point(this.Picture.Location.X , this.Picture.Location.Y - speed);
            }

            Rotate(keys);
            this.Picture.Location = location;
        }


        //розвертае Image
        public void Rotate(Keys keys)
        {
            if (keys == Keys.Up)
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
            else if (keys == Keys.Right)// поворот в право
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
            else if (keys == Keys.Down)// поворот в низ
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
