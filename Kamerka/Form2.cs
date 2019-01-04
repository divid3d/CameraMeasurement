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
using Emgu.CV.CvEnum;

namespace Kamerka
{
    public partial class Form2 : Form
    {
        VideoCapture capVideo;
        double fps;
        double hueMin=0;
        double hueMax =180;
        double saturationMin=0;
        double saturationMax=255;
        double valueMin=0;
        double valueMax=255;
        
        public Form2()
        {
            InitializeComponent();
            

            //capVideo = new VideoCapture("C:\\Users\\Divided\\wideo_projekt\\3.mp4");
            //fps = capVideo.GetCaptureProperty(Emgu.CV.CvEnum.CapProp.Fps);

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            start();
        }

        private async void start()
        {
            Mat inputImage = new Mat();
            inputImage = CvInvoke.Imread("C:\\Users\\Divided\\Pictures\\drewno\\2_2.bmp", Emgu.CV.CvEnum.ImreadModes.AnyColor);
            pictureBox1.Image = inputImage.Bitmap;

            while (true)
            {
                Mat outputImage = new Mat();
                CvInvoke.CvtColor(inputImage, outputImage, ColorConversion.Rgb2Hsv);
                CvInvoke.InRange(outputImage, new ScalarArray(new MCvScalar(hueMin, saturationMin, valueMin)), new ScalarArray(new MCvScalar(hueMax, saturationMax, valueMax)), outputImage);
                var element = CvInvoke.GetStructuringElement(ElementShape.Cross, new Size(3, 3), new Point(-1, -1));
                CvInvoke.Erode(outputImage, outputImage, element, new Point(-1, -1), 1, BorderType.Reflect, default(MCvScalar));

                Emgu.CV.Util.VectorOfVectorOfPoint contours = new Emgu.CV.Util.VectorOfVectorOfPoint();
                Mat hier = new Mat();

                CvInvoke.FindContours(outputImage, contours, hier, RetrType.External, ChainApproxMethod.ChainApproxSimple);
                Dictionary<int, double> dict = new Dictionary<int, double>();

                if (contours.Size > 0)
                {
                    for (int i = 0; i < contours.Size; i++)
                    {
                        double area = CvInvoke.ContourArea(contours[i]);
                        if (area > 2000 && area < (outputImage.Width / 4) * (outputImage.Height / 4))
                        {
                            dict.Add(i, area);
                        }
                    }

                }

           
            
                var item = dict.OrderByDescending(v => v.Value).Take(5);

                foreach (var it in item)
                {
                    int key = int.Parse(it.Key.ToString());
                    RotatedRect rotatedRect = CvInvoke.MinAreaRect(contours[key]);
                    Rectangle rect = CvInvoke.BoundingRectangle(contours[key]);
                    /* Emgu.CV.Tracking.TrackerMOSSE tracker = new Emgu.CV.Tracking.TrackerMOSSE();
                     tracker.Init(m,rect);
                     tracker.Update(m);*/
                    PointF rotatedRectCenter = rotatedRect.Center;
                    int centerX = (int)rotatedRectCenter.X;
                    int centerY = (int)rotatedRectCenter.Y;

                    PointF[] box = CvInvoke.BoxPoints(rotatedRect);
                    for (int i = 0; i < 4; i++)
                    {

                        CvInvoke.Line(outputImage, new Point((int)box[i].X, (int)box[i].Y), new Point((int)box[(i + 1) % 4].X, (int)box[(i + 1) % 4].Y), new MCvScalar(255, 0, 0), 4);
                    }

                    CvInvoke.Line(outputImage, new Point((int)box[0].X, (int)box[0].Y), new Point((int)box[2].X, (int)box[2].Y), new MCvScalar(255, 0, 0), 1);
                    CvInvoke.Line(outputImage, new Point((int)box[1].X, (int)box[1].Y), new Point((int)box[3].X, (int)box[3].Y), new MCvScalar(255, 0, 0), 1);
                    CvInvoke.PutText(outputImage, ((int)rotatedRect.Size.Width).ToString() + "x" + ((int)rotatedRect.Size.Height).ToString(), new Point((int)box[0].X + 5, (int)box[0].Y + 5), Emgu.CV.CvEnum.FontFace.HersheySimplex, 1, new MCvScalar(255, 255, 255), 1);
                    CvInvoke.Circle(outputImage, new Point(centerX, centerY), 1, new MCvScalar(0, 0, 255), 5);

                }
                    pictureBox2.Image = outputImage.Bitmap;


                await Task.Delay(10);
            }


            //while (true) {
            //    try
            //    {
            //        Mat mat = CvInvoke.Imread("C:\\Users\\Divided\\Pictures\\loool.bmp", Emgu.CV.CvEnum.ImreadModes.AnyColor);
            //        Mat outputImage = new Mat();
            //        capVideo.Read(mat);
            //        CvInvoke.CvtColor(mat, outputImage, ColorConversion.Rgb2Hsv);
            //        CvInvoke.InRange(outputImage, new ScalarArray(new MCvScalar(10, 40, 190)), new ScalarArray(new MCvScalar(21, 76, 242)), outputImage);

            //        //CvInvoke.Canny(outputImage, outputImage, 0, 255);
            //        //CvInvoke.Threshold(outputImage,outputImage,40.0,125,ThresholdType.Binary);
            //        //CvInvoke.CvtColor(outputImage, outputImage, ColorConversion.Hsv2Rgb);
            //        //CvInvoke.CvtColor(outputImage, outputImage, ColorConversion.Rgb2Gray);
            //        //CvInvoke.InRange(outputImage, new ScalarArray(new MCvScalar(30, 0, 0)), new ScalarArray(new MCvScalar(50, 100, 100)), outputImage);

            //        //Mat matImage = new Mat();
            //        //capVideo.Read(matImage);

            //        //pictureBox1.Image = matImage.Bitmap;
            //        //Mat after = new Mat();
            //        //Mat output = new Mat();
            //        //Mat bgrAfter = new Mat();
            //        //CvInvoke.CvtColor(matImage, after, ColorConversion.Bgr2Hsv);
            //        //CvInvoke.InRange(after, new ScalarArray(new MCvScalar(0, 0, 88)), new ScalarArray(new MCvScalar(90, 100, 100)), after);
            //        //CvInvoke.Threshold(after, after, 10.0, 100, ThresholdType.BinaryInv);
            //        //var element = CvInvoke.GetStructuringElement(ElementShape.Rectangle, new Size(1, 1), new Point(-1, -1));
            //        //CvInvoke.Erode(after, after, element, new Point(-1, -1), 1, BorderType.Default, default(MCvScalar));
            //        //CvInvoke.Dilate(after, after, element, new Point(-1, -1), 1, BorderType.Default, default(MCvScalar));
            //        //CvInvoke.Canny(after, after, 0, 255);
            //        //CvInvoke.Threshold(after, after, 0, 100, ThresholdType.Binary);

            //        //CvInvoke.GaussianBlur(after, after, new Size(3, 3), 0, 0, BorderType.Default);
            //        //pictureBox2.Image = after.Bitmap;
            //        pictureBox1.Image = mat.Bitmap;
            //        pictureBox2.Image = outputImage.Bitmap;
            //    }
            //    catch(Exception ex)
            //    {
            //        //capVideo = new VideoCapture("C:\\Users\\Divided\\Downloads\\wideo_projekt\\3.mp4");
            //        //fps = capVideo.GetCaptureProperty(Emgu.CV.CvEnum.CapProp.Fps);
            //    }


            //    //await Task.Delay((1000 / Convert.ToInt32(fps)));
            //     }
        }

        private void trackBar3_ValueChanged(object sender, EventArgs e)
        {
            this.hueMin = trackBar3.Value;
            textBox1.Text = trackBar3.Value.ToString();
        }

        private void trackBar4_ValueChanged(object sender, EventArgs e)
        {
            this.hueMax = trackBar4.Value;
            textBox4.Text = trackBar4.Value.ToString();
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            this.saturationMin = trackBar1.Value;
            textBox2.Text = trackBar1.Value.ToString();
        }

        private void trackBar5_ValueChanged(object sender, EventArgs e)
        {
            this.saturationMax = trackBar5.Value;
            textBox5.Text = trackBar5.Value.ToString();
        }

        private void trackBar2_ValueChanged(object sender, EventArgs e)
        {
            this.valueMin = trackBar2.Value;
            textBox3.Text = trackBar2.Value.ToString();
        }

        private void trackBar6_ValueChanged(object sender, EventArgs e)
        {
            this.valueMax = trackBar6.Value;
            textBox6.Text = trackBar6.Value.ToString();
        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {

        }
    }
}
