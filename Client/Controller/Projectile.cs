
using System.Windows.Forms;

namespace Client.Controller
{
    public class Projectile
    {
        public PictureBox pictureBox;
        public Image Image { get; set; }
        int Speed { get; set; }
        public Projectile()
        {
            Image = new Bitmap(Properties.Resources.klipartz_com);
            pictureBox = new PictureBox();
            pictureBox.Image = Image;
            pictureBox.Size = new Size(30, 30);
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            Speed = 10;
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
