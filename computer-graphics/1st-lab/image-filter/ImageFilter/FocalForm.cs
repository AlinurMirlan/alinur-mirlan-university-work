using ImageFilter.Abstractions;
using ImageFilter.Converters;

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
                IColorSpace sourceColorSpace = new LabColorSpace(sourcePictureBox.Image);
                IColorSpace targetColorSpace = new LabColorSpace(targetPictureBox.Image);
                ImageTransformer transformer = new(sourceColorSpace, targetColorSpace);
                Bitmap bitmap = transformer.MergeTargetColorSpace(new LabConvertable());
                resultPictureBox.Image = bitmap;
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
        }
    }
}