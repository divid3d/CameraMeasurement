using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.UI;

namespace Kamerka
{
    public partial class Form1 : Form
    {
        const double constant = 0.05120967742;
        int objectCount = 0;
        const int INTERNAL_CAMERA = 0;
        const int EXTERNAL_CAMERA = 1;

        int threshold1Val =50;
        int blurVal=21;
        int threshold2Val= 70;

        VideoCapture capture;
        Image<Bgr, byte> imageInput;
        Image<Bgr, byte> imageProcessed;
        Image<Gray, byte> referanceFrame = null;
        GrayPreview grayPreview;
        blurPreview blurPreview;
        deltaPreview deltaPreview;
        thresholdPreview thresholdPreview;


        bool isCaptureRunning = false;
        bool isAnalyseRunning = false;
       

        public Form1()
        {
            InitializeComponent();
            capture = new VideoCapture("C:\\Users\\Divided\\Downloads\\wideo_projekt\\3.mp4");
            //capture.SetCaptureProperty(Emgu.CV.CvEnum.CapProp.Fps, 30);
            //capture.SetCaptureProperty(Emgu.CV.CvEnum.CapProp.FrameWidth, 1280);
            //capture.SetCaptureProperty(Emgu.CV.CvEnum.CapProp.FrameHeight, 1024);
            grayPreview = new GrayPreview();
            blurPreview = new blurPreview();
            deltaPreview = new deltaPreview();
            thresholdPreview = new thresholdPreview();
            grayPreview.Show();
            blurPreview.Show();
            deltaPreview.Show();
            thresholdPreview.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void TxtXYRadius_TextChanged(object sender, EventArgs e)
        {

        }

        private async void Btn_pauseOrResume_Click(object sender, EventArgs e)
        {
            isCaptureRunning = !isCaptureRunning;
            if (isCaptureRunning)
            {
                btn_pauseOrResume.Text = "Camera stop";
                textbox_log.AppendText(CreateLogCommunicate("Camera has been started"));
            }
            else
            {
                btn_pauseOrResume.Text = "Camera start";
                textbox_log.AppendText(CreateLogCommunicate("Camera has been stoped"));
                referanceFrame = null;
            }
            if (capture == null)
            {
                return;
            }

            try
            {
                while (isCaptureRunning)
                {
                    Mat m = new Mat();
                    capture.Read(m);
                    
                    if (!m.IsEmpty)
                    {
                        imageInput = m.ToImage<Bgr, byte>();

                        double fps = capture.GetCaptureProperty(Emgu.CV.CvEnum.CapProp.Fps);
                        if (isAnalyseRunning)
                        {
                            FindCountours(imageInput);

                            
                        }
                        CvInvoke.PutText(imageInput, fps.ToString("0.00")+" fps, "+imageInput.Width+"x"+imageInput.Height, new Point(0,25), Emgu.CV.CvEnum.FontFace.HersheySimplex, 1,new MCvScalar(255,255,255),1);
                        CvInvoke.PutText(imageInput, "Count: " + objectCount, new Point(0, imageInput.Height - 25), Emgu.CV.CvEnum.FontFace.HersheySimplex, 1, new MCvScalar(255, 255, 255), 2);
                        DrawPassLine(imageInput, imageInput.Height, imageInput.Width);


                        pb_orginal.Image = imageInput.Bitmap;
                        await Task.Delay(1000/Convert.ToInt32(fps));
                    }
                    else
                    {
                        break;
                    }
                }
            }
            catch(Exception ex)
            {
                textbox_log.AppendText(CreateLogCommunicate(ex.Message));
            }
        }

        private void FindCountours(Image<Bgr, byte> imageInput )
        {

            Image<Bgr, byte> imageToProcess = imageInput;
            /*Image<Gray, byte> imageProcessed = imageToProcess.Convert<Gray, byte>().ThresholdBinary(new Gray(100), new Gray(255));
            Emgu.CV.Util.VectorOfVectorOfPoint contours = new Emgu.CV.Util.VectorOfVectorOfPoint();
            Mat hier = new Mat();

            Image<Gray, byte> imageOutput = new Image<Gray, byte>(imageProcessed.Width, imageProcessed.Height);
            CvInvoke.GaussianBlur(imageProcessed,imageProcessed,CvInvoke.cvGetSize(imageProcessed),0);
            CvInvoke.Canny(imageProcessed, imageProcessed, 50, 100);
            CvInvoke.FindContours(imageProcessed, contours, hier, Emgu.CV.CvEnum.RetrType.External, Emgu.CV.CvEnum.ChainApproxMethod.ChainApproxNone);
            List<Rectangle> rectangles = new List<Rectangle>();
            contours.
            for(int i = 0; i < contours.Size; i++)
            {
                rectangles.Add(contours[i]);
            }
            contours = contours.OrderBy(r => r.Left).ThenBy(r => r.Top).ToList();
            CvInvoke.DrawContours(imageOutput, contours, -1, new MCvScalar(255, 255, 255));*/
            Image<Gray, byte> imageOutput = imageToProcess.Convert<Gray, byte>().ThresholdBinary(new Gray(threshold1Val), new Gray(255));
            grayPreview.SetImage(imageOutput);
            CvInvoke.GaussianBlur(imageOutput,imageOutput,new Size(blurVal,blurVal),0);
            blurPreview.SetImage(imageOutput);
            if(referanceFrame == null)
            {
                referanceFrame = imageOutput.Clone();
            }
            Image<Gray, byte> frameDelta = imageOutput.Clone();
            CvInvoke.AbsDiff(imageOutput,referanceFrame, frameDelta);
            var element = CvInvoke.GetStructuringElement(ElementShape.Cross, new Size(5, 5), new Point(-1, -1));
            CvInvoke.Erode(imageOutput, imageOutput, element, new Point(-1, -1), 5, BorderType.Default, default(MCvScalar));
            //CvInvoke.Dilate(imageOutput, temp, element, new Point(-1, -1), 1, BorderType.Reflect, default(MCvScalar));
            
            
            deltaPreview.SetImage(frameDelta);
            Image<Gray, byte> frameThresh = imageOutput.Clone();
            CvInvoke.Threshold(frameDelta, frameThresh, threshold2Val, 255, ThresholdType.Binary);
            thresholdPreview.SetImage(frameThresh);
            
            Emgu.CV.Util.VectorOfVectorOfPoint contours = new Emgu.CV.Util.VectorOfVectorOfPoint();
            Mat hier = new Mat();

            CvInvoke.FindContours(frameThresh, contours, hier, RetrType.External, ChainApproxMethod.ChainApproxSimple);
            Dictionary<int,double> dict = new Dictionary<int, double>();

            if (contours.Size > 0)
            {
                for(int i = 0; i < contours.Size; i++)
                {
                    double area = CvInvoke.ContourArea(contours[i]);
                    if (area > 2000 && area< (imageOutput.Width/4) * (imageOutput.Height/4))
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

                    CvInvoke.Line(imageInput, new Point((int)box[i].X,(int)box[i].Y), new Point((int)box[(i + 1) % 4].X,(int)box[(i + 1) % 4].Y), new MCvScalar(255, 0, 0), 4);
                }

                CvInvoke.Line(imageInput, new Point((int)box[0].X, (int)box[0].Y), new Point((int)box[2].X, (int)box[2].Y), new MCvScalar(255, 0, 0), 1);
                CvInvoke.Line(imageInput, new Point((int)box[1].X, (int)box[1].Y), new Point((int)box[3].X, (int)box[3].Y), new MCvScalar(255, 0, 0), 1);
                CvInvoke.PutText(imageInput, ((int)rotatedRect.Size.Width).ToString()+"x"+ ((int)rotatedRect.Size.Height).ToString(), new Point((int)box[0].X+5, (int)box[0].Y+5), Emgu.CV.CvEnum.FontFace.HersheySimplex, 1, new MCvScalar(255, 255, 255), 1);
                CvInvoke.Circle(imageToProcess, new Point(centerX, centerY), 1, new MCvScalar(0, 0, 255), 5);

                


                /*
                Rectangle rect = CvInvoke.BoundingRectangle(contours[key]);
                int x = rect.X;
                int y = rect.Y;
                int h = rect.Height;
                int w = rect.Width;
                int coordXCentroid = (x + x + w) / 2;
                int coordYCentroid = (y + y + h) / 2;
                CvInvoke.Circle(imageToProcess,new Point(coordXCentroid,coordYCentroid),1,new MCvScalar(255,0,0),5);
                //CvInvoke.DrawContours(imageOutput, contours, key, new MCvScalar(255,255, 255),4);*/

                if (CheckPassCountLine(centerX, imageInput.Width/2)) {
                    objectCount += 1;
                    textbox_log.AppendText(CreateLogCommunicate("New object measured, total count: "+objectCount));
                    //textbox_measurements.AppendText(CreateLogCommunicate("Id: " + objectCount + " " + "Width: " + w + "px" + " " + "Height" + h + "px"));

                }
                






                //CvInvoke.Rectangle(imageToProcess, rect, new MCvScalar(255,0,0),3);
                //textbox_log.AppendText(createLogCommunicate("height:\t"+rect.Height+"\twidth:\t"+rect.Width));

            }



            pb_processed.Image = frameThresh.Bitmap;
        }

        private String CreateLogCommunicate(String communicate)
        {
            return "<" + DateTime.Now.ToString("HH:mm:ss") + "> <"+communicate+">\n";
        }


        private void Btn_pauseOrResumeAnalyse_Click_1(object sender, EventArgs e)
        {
            isAnalyseRunning = !isAnalyseRunning;
            if (isAnalyseRunning)
            {
                btn_pauseOrResumeAnalyse.Text = "Analyse stop";
                textbox_log.AppendText(CreateLogCommunicate("Analyse has been started"));
            }
            else
            {
                btn_pauseOrResumeAnalyse.Text = "Analyse start";
                textbox_log.AppendText(CreateLogCommunicate("Analyse has been stoped"));
            }
        }

        private void DrawPassLine(IInputOutputArray image,int imageHeight,int imageWidth)
        {
            CvInvoke.Line(image,new Point(imageWidth/2,0),new Point(imageWidth/2,imageHeight),new MCvScalar(0,0,255),1);            // Enter line green
          
        }

        private bool CheckPassCountLine(int x,int cordXPassLine)
        {
            int absoluteDistance = Math.Abs(x - cordXPassLine);
            if((absoluteDistance <=2) && (x > cordXPassLine))
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        private void ClearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            objectCount = 0;
        }

        private void InternalCameraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            isCaptureRunning = !isCaptureRunning;
            capture = new VideoCapture(INTERNAL_CAMERA);
            isCaptureRunning = !isCaptureRunning;

        }

        private void ExternalCameraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            isCaptureRunning = !isCaptureRunning;
            capture = new VideoCapture(EXTERNAL_CAMERA);
            isCaptureRunning = !isCaptureRunning;

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void trackBar2_ValueChanged(object sender, EventArgs e)
        {
            threshold1Val = trackBar2.Value;
            label4.Text = trackBar2.Value.ToString();
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            blurVal = trackBar1.Value;
        }

        private void trackBar3_ValueChanged(object sender, EventArgs e)
        {
            threshold2Val = trackBar3.Value;
            label5.Text = trackBar3.Value.ToString();
        }

        private void pb_orginal_Click(object sender, EventArgs e)
        {

        }
    }
}
