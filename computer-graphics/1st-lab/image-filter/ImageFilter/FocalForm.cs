namespace ImageFilter
{
    public partial class FocalForm : System.Windows.Forms.Form
    {
        public FocalForm()
        {
            InitializeComponent();
        }

        private void buttonSource_Click(object sender, EventArgs e)
        {
            if (openSourceImageDialog.ShowDialog() == DialogResult.OK)
            {
                sourcePictureBox.Load(openSourceImageDialog.FileName);
            }
        }

        private void buttonTarget_Click(object sender, EventArgs e)
        {
            if (openTargetImageDialog.ShowDialog() == DialogResult.OK)
            {
                targetPictureBox.Load(openTargetImageDialog.FileName);
                ImageColorSpace sourceColorSpace = new(sourcePictureBox.Image);
                ImageColorSpace targetColorSpace = new(targetPictureBox.Image);
                Bitmap resultPicture = ImageColorMerger.Merge(sourceColorSpace, targetColorSpace);
                resultPictureBox.Image = resultPicture;
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
        }
    }
}