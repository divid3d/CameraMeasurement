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
    public partial class GrayPreview : Form
    {
        public GrayPreview()
        {
            InitializeComponent();
            
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            this.Text = "Gray preview - 1 step";
        }

        public void SetImage(Image<Gray, byte> imageGray)
        {
            pictureBox1.Image = imageGray.Bitmap;
        }
    }
}
