using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kamerka
{
    public partial class thresholdPreview : Form
    {
        public thresholdPreview()
        {
            InitializeComponent();
            this.Text = "threshold preview - step 4";
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        public void SetImage(Image<Gray, byte> imageGray)
        {
            pictureBox1.Image = imageGray.Bitmap;
        }

        private void thresholdPreview_Load(object sender, EventArgs e)
        {

        }
    }
}
