using System.Drawing.Imaging;
using System.Net.Mime;
using System.Windows.Forms;

namespace TreasureSeeker
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void ButtonOpen_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                pictureBox.Load(openFileDialog.FileName);
            }
        }

        private void ButtonSeekTreasure_Click(object sender, EventArgs e)
        {
            if (pictureBox.Image is not null)
            {
                TreasureSeeker seeker = new(pictureBox.Image);
                Bitmap bitmap = seeker.GetTreasureMap();
                pictureBoxResult.Image = bitmap;
            }
        }

        private void ButtonMedianFilter_Click(object sender, EventArgs e)
        {
            if (pictureBox.Image is not null && int.TryParse(textBox.Text, out int medianLength))
            {
                if (medianLength <= 0 || medianLength > 30)
                    return;

                ImageFilter filter = new(new ColorSpace(pictureBox.Image));
                pictureBoxResult.Image = filter.MedianFilter(medianLength);
            }
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            if (pictureBoxResult.Image != null && saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                switch (saveFileDialog.FilterIndex)
                {
                    case 1:
                        pictureBoxResult.Image.Save(saveFileDialog.FileName, ImageFormat.Bmp);
                        break;
                    case 2:
                        pictureBoxResult.Image.Save(saveFileDialog.FileName, ImageFormat.Jpeg);
                        break;
                    case 3:
                        pictureBoxResult.Image.Save(saveFileDialog.FileName, ImageFormat.Png);
                        break;
                    case 4:
                        pictureBoxResult.Image.Save(saveFileDialog.FileName, ImageFormat.Gif);
                        break;
                }
            }
        }

        private void ButtonGreyWorldFilter_Click(object sender, EventArgs e)
        {
            if (pictureBox.Image is not null)
            {
                ImageFilter filter = new(new ColorSpace(pictureBox.Image));
                pictureBoxResult.Image = filter.GreyWorldFilter();
            }
        }
    }
}