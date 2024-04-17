
namespace Client.Controller
{
    public enum MyVector
    {
        TOP,
        RIGHT,
        BOTTOM,
        LEFT
    }
    public class ObjectOfThePlayingField
    {
        public int Speed { get; set; }
        public Image Image { get; set; }
        public MyVector Vector { get; set; }
        public  PictureBox Picture { get; set; }

        // рух танку по полю
        public void Move(Keys keys, int speed)
        {
            if (keys == null)
            {
                return;
            }

            if (keys == Keys.Up && this.Vector == MyVector.TOP)
            {
                this.Picture.Location = new Point(this.Picture.Location.X, this.Picture.Location.Y - speed);
                return;
            }
            else if (keys == Keys.Left && this.Vector == MyVector.LEFT)
            {
                this.Picture.Location = new Point(this.Picture.Location.X - speed, this.Picture.Location.Y);
                return;
            }
            else if (keys == Keys.Right && this.Vector == MyVector.RIGHT)
            {
                this.Picture.Location = new Point(this.Picture.Location.X + speed, this.Picture.Location.Y);
                return;
            }
            else if (keys == Keys.Down && this.Vector == MyVector.BOTTOM)
            {
                this.Picture.Location = new Point(this.Picture.Location.X, this.Picture.Location.Y + speed);
                return;
            }
            Rotate(keys);
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
