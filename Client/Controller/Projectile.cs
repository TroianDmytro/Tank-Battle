
using System.Windows.Forms;

namespace Client.Controller
{
    public class Projectile: ObjectOfThePlayingField, IDisposable
    {
        //public PictureBox pictureBox;
        //public Image Image { get; set; }
        //int Speed { get; set; }
        public Projectile()
        {
            Image = new Bitmap(Properties.Resources.klipartz_com);
            Picture = new PictureBox();
            Picture.Image = Image;
            Picture.Size = new Size(30, 30);
            Picture.SizeMode = PictureBoxSizeMode.Zoom;
            Vector = MyVector.RIGHT;
            Speed = 20;
            Picture.Tag = "Projectile";
        }

        public void Dispose()
        {
            if (this != null)
            {
                Image.Dispose();
                Picture.Dispose();
            }
        }



        //public Image SetVector(MyVector vector)
        //{
        //    Image temp = new Bitmap(Properties.Resources.klipartz_com);

        //    if (vector == MyVector.TOP)
        //    {
        //    }
        //}
    }
}
