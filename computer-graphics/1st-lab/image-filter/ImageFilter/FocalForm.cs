using ImageFilter.Abstractions;
using ImageFilter.Converters;
using System.Drawing.Imaging;

namespace ImageFilter
{
    public partial class FocalForm : System.Windows.Forms.Form
    {
        private bool isSourcePicureInLabSpace = true;
        private bool isTargetPicureInLabSpace = true;
        private bool areBothInLabSpace = true;
        private bool preserveContrast = true;
        private Bitmap resultingBitmap;
        private Bitmap resultingBitmapSecond;
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
            }
        }

        private void buttonConvert_Click(object sender, EventArgs e)
        {
            if (sourcePictureBox.Image is not null && targetPictureBox.Image is not null)
            {
                IColorSpace sourceColorSpace = isSourcePicureInLabSpace ?
                    new LabColorSpace(sourcePictureBox.Image) :
                    new HslColorSpace(sourcePictureBox.Image);
                IColorSpace targetColorSpace = isTargetPicureInLabSpace ?
                    new LabColorSpace(targetPictureBox.Image) :
                    new HslColorSpace(targetPictureBox.Image);
                ImageTransformer transformer = new(sourceColorSpace, targetColorSpace);
                resultingBitmap = transformer.MergeTargetColorSpace(
                    isSourcePicureInLabSpace ?
                    new LabConvertable() : new HslConvertable(),
                    preserveContrast);
                resultPictureBox.Image = resultingBitmap;
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (resultingBitmap != null && saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                switch (saveFileDialog.FilterIndex)
                {
                    case 1:
                        resultingBitmap.Save(saveFileDialog.FileName, ImageFormat.Bmp);
                        break;
                    case 2:
                        resultingBitmap.Save(saveFileDialog.FileName, ImageFormat.Jpeg);
                        break;
                    case 3:
                        resultingBitmap.Save(saveFileDialog.FileName, ImageFormat.Png);
                        break;
                    case 4:
                        resultingBitmap.Save(saveFileDialog.FileName, ImageFormat.Gif);
                        break;
                }
            }
        }

        private void buttonSaveSecond_Click(object sender, EventArgs e)
        {
            if (resultingBitmapSecond != null && saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                switch (saveFileDialog.FilterIndex)
                {
                    case 1:
                        resultingBitmapSecond.Save(saveFileDialog.FileName, ImageFormat.Bmp);
                        break;
                    case 2:
                        resultingBitmapSecond.Save(saveFileDialog.FileName, ImageFormat.Jpeg);
                        break;
                    case 3:
                        resultingBitmapSecond.Save(saveFileDialog.FileName, ImageFormat.Png);
                        break;
                    case 4:
                        resultingBitmapSecond.Save(saveFileDialog.FileName, ImageFormat.Gif);
                        break;
                }
            }
        }

        private void buttonSourceSwitcher_Click(object sender, EventArgs e)
        {
            if (isSourcePicureInLabSpace)
            {
                buttonSourceSwitcher.Text = "HSL";
                buttonLabelOne.Text = "HSL";
                buttonLabelTwo.Text = "LAB";
            }
            else
            {
                buttonSourceSwitcher.Text = "LAB";
                buttonLabelOne.Text = "LAB";
                buttonLabelTwo.Text = "HSL";
            }

            isSourcePicureInLabSpace = !isSourcePicureInLabSpace;
        }

        private void buttonTargetSwitcher_Click(object sender, EventArgs e)
        {
            if (isTargetPicureInLabSpace)
                buttonTargetSwitcher.Text = "HSL";
            else
                buttonTargetSwitcher.Text = "LAB";

            isTargetPicureInLabSpace = !isTargetPicureInLabSpace;
        }

        private void buttonColorSpaceSwitcher_Click(object sender, EventArgs e)
        {
            isSourcePicureInLabSpace = areBothInLabSpace;
            isTargetPicureInLabSpace = areBothInLabSpace;
            areBothInLabSpace = !areBothInLabSpace;
            buttonSourceSwitcher_Click(this, EventArgs.Empty);
            buttonTargetSwitcher_Click(this, EventArgs.Empty);
        }

        private void checkBoxPreserveContrast_CheckedChanged(object sender, EventArgs e)
        {
            preserveContrast = checkBoxPreserveContrast.Checked;
        }

        private void buttonConvertAll_Click(object sender, EventArgs e)
        {
            if (sourcePictureBox.Image is not null && targetPictureBox.Image is not null)
            {
                IColorSpace sourceColorSpace, targetColorSpace;
                IRgbConverter converter;
                if (isSourcePicureInLabSpace)
                {
                    sourceColorSpace = new LabColorSpace(sourcePictureBox.Image);
                    targetColorSpace = new LabColorSpace(targetPictureBox.Image);
                    converter = new LabConvertable();
                }
                else
                {
                    sourceColorSpace = new HslColorSpace(sourcePictureBox.Image);
                    targetColorSpace = new HslColorSpace(targetPictureBox.Image);
                    converter = new HslConvertable();
                }
                ImageTransformer transformer = new(sourceColorSpace, targetColorSpace);
                resultingBitmap = transformer.MergeTargetColorSpace(converter, preserveContrast);
                resultPictureBox.Image = resultingBitmap;

                if (isSourcePicureInLabSpace)
                {
                    sourceColorSpace = new HslColorSpace(sourcePictureBox.Image);
                    targetColorSpace = new HslColorSpace(targetPictureBox.Image);
                    converter = new HslConvertable();
                }
                else
                {
                    sourceColorSpace = new LabColorSpace(sourcePictureBox.Image);
                    targetColorSpace = new LabColorSpace(targetPictureBox.Image);
                    converter = new LabConvertable();
                }
                transformer = new(sourceColorSpace, targetColorSpace);
                resultingBitmapSecond = transformer.MergeTargetColorSpace(converter, preserveContrast);
                resultPictureBoxTwo.Image = resultingBitmapSecond;
            }
        }
    }
}