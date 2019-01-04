using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;

namespace Kamerka
{
    public partial class nowy : Form
    {
        String connString;
        Npgsql.NpgsqlConnection npgsqlConnection;

        string host;
        string username;
        string password;
        string database;


        VideoCapture capVideo;
        String currentPath = "";
        double fps;
        //Mat imgFrame1 = new Mat();
        //Mat imgFrame2 = new Mat();
        Mat imgFrame = new Mat();
        List<measuredObject> measures = new List<measuredObject>();
        ScalarArray lowerHsvLimit = new ScalarArray(new MCvScalar(93, 0, 187));
        ScalarArray upperHsvLimit = new ScalarArray(new MCvScalar(111, 76, 255));


        List<Blob> blobs = new List<Blob>();
        Point[] crossingLine = new Point[2];
        int objectCount = 0;

        bool isCaptureRunning = false;
        bool isAnalyseRunning = false;
        Stopwatch stopwatch = new Stopwatch();

        DataTable dt = new DataTable();

        public void initVideoSource(VideoSource videoSource, String path)
        {
            Mat initFrame = new Mat();
            if (isCaptureRunning)
            {
                button1.PerformClick();
            }

            if (isAnalyseRunning)
            {
                button2.PerformClick();
            }

            if (capVideo != null)
            {
                capVideo.Stop();
                capVideo.Dispose();
            }
            switch (videoSource)
            {
                case VideoSource.FILE:
                    try
                    {
                        capVideo = new VideoCapture(path);
                        fps = capVideo.GetCaptureProperty(Emgu.CV.CvEnum.CapProp.Fps);
                        textbox_log.AppendText(CreateLogCommunicate("Selected video stream: " + currentPath));
                        capVideo.Read(initFrame);
                        pictureBox1.Image = initFrame.ToImage<Bgr, Byte>().ToBitmap();
                    }
                    catch (Exception e)
                    {
                        textbox_log.AppendText(CreateLogCommunicate("Error while loading selected video stream"));
                    }


                    break;

                case VideoSource.INTERNAL_CAMERA:
                    try
                    {
                        capVideo = new VideoCapture(0);
                        fps = capVideo.GetCaptureProperty(Emgu.CV.CvEnum.CapProp.Fps);
                        textbox_log.AppendText(CreateLogCommunicate("Selected video stream: Internal camera"));
                        capVideo.Read(initFrame);
                        pictureBox1.Image = initFrame.ToImage<Bgr, Byte>().ToBitmap();
                    }
                    catch (Exception e)
                    {
                        textbox_log.AppendText(CreateLogCommunicate("Error while loading selected video stream"));
                    }

                    break;

                case VideoSource.EXTERNAL_CAMERA:
                    try
                    {
                        capVideo = new VideoCapture(1);
                        fps = capVideo.GetCaptureProperty(Emgu.CV.CvEnum.CapProp.Fps);
                        textbox_log.AppendText(CreateLogCommunicate("Selected video stream: External camera"));
                        capVideo.Read(initFrame);
                        pictureBox1.Image = initFrame.ToImage<Bgr, Byte>().ToBitmap();
                    }
                    catch (Exception e)
                    {
                        textbox_log.AppendText(CreateLogCommunicate("Error while loading selected video stream"));
                    }

                    break;
            }

        }

        public nowy()
        {
            InitializeComponent();
            dt.Columns.Add("Time");
            dt.Columns.Add("Id");
            dt.Columns.Add("Width");
            dt.Columns.Add("Height");
            dataGridView1.DataSource = dt;
            textbox_log.AppendText(CreateLogCommunicate("Program start"));

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        public void objectMeasure(Blob measuredBlob, Mat referanceBackgroundFrame, Mat image)
        {
            measuredObject measuredObject = new measuredObject(measures.Count + 1, (int)measuredBlob.currentBoundingRect.Size.Height, (int)measuredBlob.currentBoundingRect.Size.Width, DateTime.Now.ToString("HH:mm:ss"));
            measures.Add(measuredObject);
            addNewMeasureRow(measuredObject.getMeasureTime(), measuredObject.getId(), measuredObject.getWidth(), measuredObject.getHeight());
            textbox_log.AppendText(CreateLogCommunicate("New object measured: Id: " + measuredObject.getId().ToString() + ", Width: " + measuredObject.getWidth().ToString() + ", Height: " + measuredObject.getHeight().ToString()));


            //// matrices we'll use
            //Mat M = new Mat();
            //Mat cropped = new Mat();
            //Mat rotated = new Mat();
            //Emgu.CV.Util.VectorOfVectorOfPoint contours = new Emgu.CV.Util.VectorOfVectorOfPoint();
            //// get angle and size from the bounding box
            //Mat imageCpy = image.Clone();
            //float angle = measuredBlob.currentBoundingRect.Angle;
            //Size rect_size = new Size((int)measuredBlob.currentBoundingRect.Size.Width + 10, (int)measuredBlob.currentBoundingRect.Size.Height + 10);
            //// thanks to http://felix.abecassis.me/2011/10/opencv-rotation-deskewing/
            //if (measuredBlob.currentBoundingRect.Angle < -45)
            //{
            //    angle += 90;
            //    float height = measuredBlob.currentBoundingRect.Size.Height;
            //    float width = measuredBlob.currentBoundingRect.Size.Width;

            //    measuredBlob.currentBoundingRect.Size.Height = width;

            //    measuredBlob.currentBoundingRect.Size.Width = height;

            //}

            //// get the rotation matrix
            //CvInvoke.GetRotationMatrix2D(measuredBlob.currentBoundingRect.Center, angle, 1.0, M);
            //// perform the affine transformation
            //CvInvoke.WarpAffine(imageCpy, rotated, M, imageCpy.Size, Inter.Linear);
            ////warpAffine(src, rotated, M, src.size(), INTER_CUBIC);
            //// crop the resulting image
            //// getRectSubPix(rotated, rect_size, rect.center, cropped);

            //CvInvoke.GetRectSubPix(rotated, rect_size, measuredBlob.currentBoundingRect.Center, cropped);

            //Mat croppedCopy = cropped.Clone();
            //CvInvoke.CvtColor(croppedCopy, croppedCopy, Emgu.CV.CvEnum.ColorConversion.Bgr2Gray);
            //Mat strElement = new Mat();
            //strElement = CvInvoke.GetStructuringElement(ElementShape.Rectangle, new Size(3, 3), new Point(-1, -1));
            ////CvInvoke.Erode(cropped, cropped, strElement, new Point(-1, -1), 2, BorderType.Default, new MCvScalar(1.0));

            //// CvInvoke.GaussianBlur(croppedCopy, croppedCopy, new Size(3, 3), 0);

            //CvInvoke.Threshold(croppedCopy, croppedCopy, 100, 255.0, ThresholdType.Binary);
            //CvInvoke.Dilate(croppedCopy, croppedCopy, strElement, new Point(-1, -1), 4, Emgu.CV.CvEnum.BorderType.Default, new MCvScalar(1.0));
            ////CvInvoke.Erode(croppedCopy, croppedCopy, strElement, new Point(-1, -1), 2, Emgu.CV.CvEnum.BorderType.Default, new MCvScalar(1.0));            //CvInvoke.MorphologyEx(croppedCopy, croppedCopy, MorphOp.Close, strElement, new Point(-1, -1), 1, BorderType.Default, new MCvScalar(1.0));
            //// CvInvoke.MorphologyEx(croppedCopy, croppedCopy, MorphOp.Open, strElement, new Point(-1, -1), 1, BorderType.Default, new MCvScalar(1.0));
            ////CvInvoke.Canny(croppedCopy, croppedCopy, 15, 150, 3);

            ////CvInvoke.Erode(croppedCopy, croppedCopy, strElement, new Point(-1, -1), 2, BorderType.Default, new MCvScalar(1.0));
            //CvInvoke.FindContours(croppedCopy, contours, new Mat(), RetrType.External, ChainApproxMethod.ChainApproxSimple);
            //if (contours.Size > 0)
            //{
            //    RotatedRect rotatedRect = CvInvoke.MinAreaRect(contours[0]);
            //    int centerX = (int)rotatedRect.Center.X;
            //    int centerY = (int)rotatedRect.Center.Y;

            //    PointF[] box = CvInvoke.BoxPoints(rotatedRect);

            //    for (int i = 0; i < 4; i++)
            //    {

            //        CvInvoke.Line(cropped, new Point((int)box[i].X, (int)box[i].Y), new Point((int)box[(i + 1) % 4].X, (int)box[(i + 1) % 4].Y), new MCvScalar(255, 0, 0), 2);
            //    }

            //    CvInvoke.Line(cropped, new Point((int)box[0].X, (int)box[0].Y), new Point((int)box[2].X, (int)box[2].Y), new MCvScalar(255, 0, 0), 1);
            //    CvInvoke.Line(cropped, new Point((int)box[1].X, (int)box[1].Y), new Point((int)box[3].X, (int)box[3].Y), new MCvScalar(255, 0, 0), 1);
            //    CvInvoke.PutText(cropped, ((int)rotatedRect.Size.Width).ToString() + "x" + ((int)rotatedRect.Size.Height).ToString(), new Point((int)box[0].X + 5, (int)box[0].Y + 5), Emgu.CV.CvEnum.FontFace.HersheySimplex, 1, new MCvScalar(255, 255, 255), 1);
            //    CvInvoke.Circle(cropped, new Point(centerX, centerY), 1, new MCvScalar(0, 0, 255), 2);
            //    DataRow dr = dt.NewRow();

            //    dr[0] = DateTime.Now.ToString("HH:mm:ss");
            //    dr[1] = objectCount;
            //    dr[2] = (int)rotatedRect.Size.Width;
            //    dr[3] = (int)rotatedRect.Size.Height;
            //    textbox_log.AppendText(CreateLogCommunicate("Object measured:\t" + "Width: " + dr[2] + " " + "Height: " + dr[3]));
            //    dt.Rows.Add(dr);
            //    /*
            //    //w oddzielnym wątku async bo inaczej jak bedzie 100 pool albo 15s timeout to wisi program 
            //    try
            //    {
            //        Npgsql.NpgsqlConnection npgsqlConnection = new Npgsql.NpgsqlConnection(connString);
            //        String sql = "insert into pomiary values(@id,@date,@time,@width,@height)";
            //        npgsqlConnection.Open();
            //        Npgsql.NpgsqlCommand cmd = new Npgsql.NpgsqlCommand(sql, npgsqlConnection);
            //        cmd.Parameters.AddWithValue("@id", NpgsqlTypes.NpgsqlDbType.Integer, Convert.ToInt32(objectCount));
            //        cmd.Parameters.AddWithValue("@date",NpgsqlTypes.NpgsqlDbType.Date, DateTime.Now.Date);
            //        cmd.Parameters.AddWithValue("@time", NpgsqlTypes.NpgsqlDbType.Text, dr[0]);
            //        cmd.Parameters.AddWithValue("@width", NpgsqlTypes.NpgsqlDbType.Integer,Convert.ToInt32(rotatedRect.Size.Width));
            //        cmd.Parameters.AddWithValue("@height",NpgsqlTypes.NpgsqlDbType.Integer,Convert.ToInt32(rotatedRect.Size.Height));
            //        cmd.Prepare();
            //        cmd.ExecuteNonQuery();
            //    }
            //    catch (Npgsql.NpgsqlException ex)
            //    {
            //        textbox_log.AppendText(CreateLogCommunicate(ex.Message));
            //    }
            //    finally
            //    {
            //        if (npgsqlConnection != null) npgsqlConnection.Close();
            //    }*/
            //}


            //pictureBox3.Image = cropped.Bitmap;
            //pictureBox4.Image = croppedCopy.Bitmap;
        }

        // CvInvoke.FindContours(cropped,objectContour,null,RetrType.External,ChainApproxMethod.ChainApproxTc89L1);

        //pictureBox4.Image = objectContour.Bitmap;


        /* void drawBlobContours(List<Blob> blobs,Mat image)
         {

             Emgu.CV.Util.VectorOfVectorOfPointF contours = new Emgu.CV.Util.VectorOfVectorOfPointF();
             foreach(Blob blob in blobs)
             {
                 contours.Push(blob.currentContour);
             }
             CvInvoke.DrawContours(image,contours ,0,new MCvScalar(255,255,255),1);
         }*/
        void drawBlobInfoOnImage(List<Blob> blobs, Mat image)
        {
            for (int i = 0; i < blobs.Count; i++)
            {
                if (blobs[i].blnStillBeingTracked == true)
                {

                    double dblFontScale = (image.Height * image.Width) / 300000.0;
                    int intFontThickness = (int)Math.Round((dblFontScale * 1.0));

                    PointF centerPointF = blobs[i].centerPositions[blobs[i].centerPositions.Size - 1];
                    Point centerPoint = new Point((int)centerPointF.X, (int)centerPointF.Y);

                    CvInvoke.PutText(image, (i + 1).ToString(), centerPoint, FontFace.HersheyComplex, dblFontScale, new MCvScalar(0, 255, 0), intFontThickness);
                }
            }
        }

        void matchCurrentFrameBlobsToExistingBlobs(List<Blob> existingBlobs, List<Blob> currentFrameBlobs)
        {
            foreach (Blob existingBlob in existingBlobs)
            {
                existingBlob.blnCurrentBlobFoundOrNewBlob = false;
                existingBlob.predictNextPosition();
            }

            foreach (Blob currentFrameBlob in currentFrameBlobs)
            {
                int intIndexOfLeastDistance = 0;
                double dblLeastDistance = 100000.0;

                for (int i = 0; i < existingBlobs.Count; i++)
                {
                    if (existingBlobs[i].blnStillBeingTracked == true)
                    {
                        double dblDistance = distanceBetweenPoints(currentFrameBlob.centerPositions[currentFrameBlob.centerPositions.Size - 1], existingBlobs[i].predictedNextPosition);

                        if (dblDistance < dblLeastDistance)
                        {
                            dblLeastDistance = dblDistance;
                            intIndexOfLeastDistance = i;
                        }
                    }
                }
                if (dblLeastDistance < currentFrameBlob.dblCurrentDiagonalSize * 0.5)
                {
                    addBlobToExistingBlobs(currentFrameBlob, existingBlobs, intIndexOfLeastDistance);
                }
                else
                {
                    addNewBlob(currentFrameBlob, existingBlobs);
                }
            }
            foreach (Blob existingBlob in existingBlobs)
            {
                if (existingBlob.blnCurrentBlobFoundOrNewBlob == false)
                {
                    existingBlob.intNumOfConsecutiveFramesWithoutAMatch++;
                }

                if (existingBlob.intNumOfConsecutiveFramesWithoutAMatch >= 5)
                {
                    existingBlob.blnStillBeingTracked = false;
                }
            }

        }

        double distanceBetweenPoints(PointF point1, PointF point2)
        {

            int intX = (int)Math.Abs(point1.X - point2.X);
            int intY = (int)Math.Abs(point1.Y - point2.Y);

            return (Math.Sqrt(Math.Pow(intX, 2) + Math.Pow(intY, 2)));
        }

        void addBlobToExistingBlobs(Blob currentFrameBlob, List<Blob> existingBlobs, int intIndex)
        {
            existingBlobs[intIndex].currentContour = currentFrameBlob.currentContour;
            existingBlobs[intIndex].currentBoundingRect = currentFrameBlob.currentBoundingRect;


            PointF centerPosition = currentFrameBlob.centerPositions.ToArray()[currentFrameBlob.centerPositions.Size - 1];
            PointF[] temp = new PointF[1];
            temp[0] = centerPosition;

            existingBlobs[intIndex].centerPositions.Push(temp);

            existingBlobs[intIndex].dblCurrentDiagonalSize = currentFrameBlob.dblCurrentDiagonalSize;
            existingBlobs[intIndex].dblCurrentAspectRatio = currentFrameBlob.dblCurrentAspectRatio;

            existingBlobs[intIndex].blnStillBeingTracked = true;
            existingBlobs[intIndex].blnCurrentBlobFoundOrNewBlob = true;
        }

        void addNewBlob(Blob currentFrameBlob, List<Blob> existingBlobs)
        {
            currentFrameBlob.blnCurrentBlobFoundOrNewBlob = true;
            existingBlobs.Add(currentFrameBlob);
        }

        bool checkIfBloobsCrossedTheLine(List<Blob> blobs, int verticalLinePosition, ref int objectCount, Mat imgCopy)
        {
            bool blnAtLeastOneBlobCrossedTheLine = false;
            foreach (Blob blob in blobs)
            {
                if (blob.blnStillBeingTracked == true && blob.centerPositions.Size >= 2)
                {
                    int prevFrameIndex = (int)blob.centerPositions.Size - 2;
                    int currentFrameIndex = (int)blob.centerPositions.Size - 1;

                    if (blob.centerPositions[prevFrameIndex].X <= verticalLinePosition && blob.centerPositions[currentFrameIndex].X > verticalLinePosition)
                    {
                        objectCount++;
                        blnAtLeastOneBlobCrossedTheLine = true;
                        objectMeasure(blob, imgCopy, imgCopy);

                    }
                }
            }
            return blnAtLeastOneBlobCrossedTheLine;
        }
        void drawAndShowContours(Mat image, List<Blob> blobs)
        {

            foreach (Blob blob in blobs)
            {
                PointF rotatedRectCenter = blob.currentBoundingRect.Center;
                int centerX = (int)rotatedRectCenter.X;
                int centerY = (int)rotatedRectCenter.Y;

                PointF[] box = CvInvoke.BoxPoints(blob.currentBoundingRect);
                for (int i = 0; i < 4; i++)
                {

                    CvInvoke.Line(image, new Point((int)box[i].X, (int)box[i].Y), new Point((int)box[(i + 1) % 4].X, (int)box[(i + 1) % 4].Y), new MCvScalar(255, 0, 0), 4);
                }

                CvInvoke.Line(image, new Point((int)box[0].X, (int)box[0].Y), new Point((int)box[2].X, (int)box[2].Y), new MCvScalar(255, 0, 0), 1);
                CvInvoke.Line(image, new Point((int)box[1].X, (int)box[1].Y), new Point((int)box[3].X, (int)box[3].Y), new MCvScalar(255, 0, 0), 1);
                CvInvoke.PutText(image, ((int)blob.currentBoundingRect.Size.Width).ToString() + "x" + ((int)blob.currentBoundingRect.Size.Height).ToString(), new Point((int)box[0].X + 5, (int)box[0].Y + 5), Emgu.CV.CvEnum.FontFace.HersheySimplex, 1, new MCvScalar(0, 0, 255), 2);
                CvInvoke.Circle(image, new Point(centerX, centerY), 1, new MCvScalar(0, 0, 255), 5);

            }
            drawBlobInfoOnImage(blobs, image);
            pictureBox1.Image = image.ToImage<Bgr, Byte>().ToBitmap();

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (capVideo == null)
            {
                textbox_log.AppendText(CreateLogCommunicate("Select video source"));
                return;
            }

            if (isAnalyseRunning)
            {
                button2.PerformClick();
            }

            isCaptureRunning = !isCaptureRunning;
            if (isCaptureRunning)
            {
                button1.Text = "Camera stop";
                textbox_log.AppendText(CreateLogCommunicate("Camera has been started"));
            }
            else
            {
                button1.Text = "Camera start";
                textbox_log.AppendText(CreateLogCommunicate("Camera has been stoped"));
            }

            //capVideo.Read(imgFrame1);
            //capVideo.Read(imgFrame2);

            capVideo.Read(imgFrame);

            int intVericalLinePosition = imgFrame.Width / 2;
            crossingLine[0].X = intVericalLinePosition;
            crossingLine[0].Y = 0;

            crossingLine[1].X = intVericalLinePosition;
            crossingLine[1].Y = imgFrame.Height;
            bool blnFirstFrame = true;

            //int frameCount = 2;

            try
            {
                while (isCaptureRunning)
                {
                    stopwatch.Reset();
                    stopwatch.Start();


                    if (!imgFrame.IsEmpty)
                    {
                        //imageInput = m.ToImage<Bgr, byte>();

                        fps = capVideo.GetCaptureProperty(Emgu.CV.CvEnum.CapProp.Fps);


                        if (isAnalyseRunning)
                        {
                            // FindCountours(imageInput);
                            List<Blob> currentFrameBlobs = new List<Blob>();

                            Mat colorFiltered = new Mat();
                            CvInvoke.CvtColor(imgFrame, colorFiltered, ColorConversion.Rgb2Hsv);
                            CvInvoke.InRange(colorFiltered, lowerHsvLimit, upperHsvLimit, colorFiltered);
                            CvInvoke.GaussianBlur(colorFiltered, colorFiltered, new Size(5, 5), 0);
                            Mat structuringElement5x5 = CvInvoke.GetStructuringElement(Emgu.CV.CvEnum.ElementShape.Rectangle, new Size(5, 5), new Point(-1, -1));
                            CvInvoke.Erode(colorFiltered, colorFiltered, structuringElement5x5, new Point(-1, -1), 1, BorderType.Default, new MCvScalar(1.0));
                            //CvInvoke.Dilate(colorFiltered, colorFiltered, structuringElement5x5, new Point(-1, -1), 3, BorderType.Default, new MCvScalar(1.0));
                            //Mat imgFrame2Copy = imgFrame2.Clone();


                            //Mat imgDifferance = new Mat();
                            //Mat imgThresh = new Mat();

                            //    Image<Bgr, byte> img1 = new Image<Bgr, byte>(imgFrame1Copy.Bitmap);

                            //    Image<Hsv, byte> img = img1.Convert<Hsv, byte>();
                            //    //38
                            //    img = img.ThresholdToZero(new Hsv(180, 0.1, 84));
                            //    //upper
                            //    img = img.ThresholdToZeroInv(new Hsv(180, 22, 96));

                            //    Image<Bgr, byte> color = img.Convert<Bgr,byte>();

                            //    pictureBox3.Image = color.Bitmap;


                            //CvInvoke.CvtColor(imgFrame1Copy, imgFrame1Copy, Emgu.CV.CvEnum.ColorConversion.Bgr2Gray);
                            //CvInvoke.CvtColor(imgFrame2Copy, imgFrame2Copy, Emgu.CV.CvEnum.ColorConversion.Bgr2Gray);



                            //CvInvoke.GaussianBlur(imgFrame1Copy, imgFrame1Copy, new Size(5, 5), 0);
                            //CvInvoke.GaussianBlur(imgFrame2Copy, imgFrame2Copy, new Size(5, 5), 0);

                            //CvInvoke.AbsDiff(imgFrame1Copy, imgFrame2Copy, imgDifferance);

                            //CvInvoke.Threshold(imgDifferance, imgThresh, 20.0, 255.0, Emgu.CV.CvEnum.ThresholdType.Binary);


                            //Mat structuringElement3x3 = CvInvoke.GetStructuringElement(Emgu.CV.CvEnum.ElementShape.Rectangle, new Size(3, 3), new Point(-1, -1));
                            //Mat structuringElement5x5 = CvInvoke.GetStructuringElement(Emgu.CV.CvEnum.ElementShape.Rectangle, new Size(5, 5), new Point(-1, -1));
                            //Mat structuringElement7x7 = CvInvoke.GetStructuringElement(Emgu.CV.CvEnum.ElementShape.Rectangle, new Size(7, 7), new Point(-1, -1));
                            //Mat structuringElement15x15 = CvInvoke.GetStructuringElement(Emgu.CV.CvEnum.ElementShape.Rectangle, new Size(15, 15), new Point(-1, -1));


                            ////ciekawe nie ogarniam
                            //CvInvoke.Dilate(imgThresh, imgThresh, structuringElement3x3, new Point(-1, -1), 4, Emgu.CV.CvEnum.BorderType.Default, new MCvScalar(1.0));
                            //CvInvoke.Erode(imgThresh, imgThresh, structuringElement3x3, new Point(-1, -1), 2, Emgu.CV.CvEnum.BorderType.Default, new MCvScalar(1.0));

                            // CvInvoke.Canny(imgThresh, imgThresh, 15, 150, 3);

                            //CvInvoke.MorphologyEx(imgThresh, imgThresh, MorphOp.Close, structuringElement5x5, new Point(-1, -1), 1, BorderType.Default, new MCvScalar(1.0));

                            //CvInvoke.MorphologyEx(imgThresh,imgThresh,MorphOp.Open,structuringElement5x5,new Point(-1,-1),1,BorderType.Default,new MCvScalar(1.0));
                            //CvInvoke.MorphologyEx(imgThresh, imgThresh, MorphOp.Close, structuringElement5x5, new Point(-1, -1), 1, BorderType.Default, new MCvScalar(1.0));

                            /* Mat imgPreview = new Mat();
                            imgPreview = imgThresh.Clone();
                            drawBlobContours(blobs,imgPreview);*/
                            pictureBox2.Image = colorFiltered.ToImage<Gray, Byte>().ToBitmap();

                            Emgu.CV.Util.VectorOfVectorOfPoint contours = new Emgu.CV.Util.VectorOfVectorOfPoint();

                            CvInvoke.FindContours(colorFiltered, contours, new Mat(), RetrType.External, ChainApproxMethod.ChainApproxSimple);

                            Emgu.CV.Util.VectorOfVectorOfPointF convexHulls = new Emgu.CV.Util.VectorOfVectorOfPointF();

                            for (int i = 0; i < contours.Size; i++)
                            {
                                Emgu.CV.Util.VectorOfPoint contour = new Emgu.CV.Util.VectorOfPoint();
                                contour = contours[i];
                                Point[] contourArray = contour.ToArray();
                                PointF[] contourArrayF = new PointF[contourArray.Count()];
                                for (int j = 0; j < contourArray.Count(); j++)
                                {
                                    contourArrayF[j] = new PointF(contourArray[j].X, contourArray[j].Y);
                                }
                                PointF[] convexHull = CvInvoke.ConvexHull(contourArrayF);
                                convexHulls.Push(new Emgu.CV.Util.VectorOfPointF(convexHull));

                            }

                            for (int i = 0; i < convexHulls.Size; i++)
                            {
                                Emgu.CV.Util.VectorOfPointF convexHull = new Emgu.CV.Util.VectorOfPointF(convexHulls[i].ToArray());
                                Blob possibleBlob = new Blob(convexHull);


                                if ((possibleBlob.currentBoundingRect.Size.Height * possibleBlob.currentBoundingRect.Size.Width) > 5000 &&
                                possibleBlob.currentBoundingRect.Size.Height / possibleBlob.currentBoundingRect.Size.Width > 4 &&
                                possibleBlob.currentBoundingRect.Size.Width > 50 &&
                                possibleBlob.currentBoundingRect.Size.Height > 50 &&
                                possibleBlob.dblCurrentDiagonalSize > 60.0 &&
                                ((CvInvoke.ContourArea(possibleBlob.currentContour) / ((double)possibleBlob.currentBoundingRect.Size.Width * (double)possibleBlob.currentBoundingRect.Size.Height))) > 0.5)
                                {
                                    currentFrameBlobs.Add(possibleBlob);
                                }
                            }

                            if (blnFirstFrame == true)
                            {
                                foreach (Blob currentFrameBlob in currentFrameBlobs)
                                {
                                    blobs.Add(currentFrameBlob);
                                }
                            }
                            else
                            {
                                matchCurrentFrameBlobsToExistingBlobs(blobs, currentFrameBlobs);
                            }

                            //imgFrame2Copy = imgFrame2.Clone();

                            bool blnAtLeastOneBlobCrossedTheLine = checkIfBloobsCrossedTheLine(blobs, intVericalLinePosition, ref objectCount, imgFrame);

                            if (blnAtLeastOneBlobCrossedTheLine == true)
                            {
                                CvInvoke.Line(imgFrame, crossingLine[0], crossingLine[1], new MCvScalar(0, 255, 0), 2);
                            }
                            else
                            {
                                CvInvoke.Line(imgFrame, crossingLine[0], crossingLine[1], new MCvScalar(0, 0, 255), 2);
                            }

                            CvInvoke.PutText(imgFrame, fps.ToString("0.00") + " fps, " + imgFrame.Width + "x" + imgFrame.Height, new Point(0, 25), Emgu.CV.CvEnum.FontFace.HersheyComplex, 1, new MCvScalar(255, 255, 255), 3);
                            CvInvoke.PutText(imgFrame, "Count: " + measures.Count.ToString(), new Point(0, imgFrame.Height - 25), Emgu.CV.CvEnum.FontFace.HersheySimplex, 1, new MCvScalar(255, 255, 255), 2);

                            drawAndShowContours(imgFrame, currentFrameBlobs);


                            currentFrameBlobs.Clear();

                            //imgFrame1 = imgFrame2.Clone();
                            /*
                            if ((capVideo.GetCaptureProperty(Emgu.CV.CvEnum.CapProp.PosFrames) + 1) < capVideo.GetCaptureProperty(Emgu.CV.CvEnum.CapProp.FrameCount))
                            {
                                capVideo.Read(imgFrame2);
                            }
                            else
                            {
                                break;
                            }*/
                            blnFirstFrame = false;
                            //frameCount++;
                            stopwatch.Stop();
                            double computeTime = stopwatch.ElapsedMilliseconds;
                            int millsToDelay = (int)((1000.0 / fps) - computeTime);
                            if(millsToDelay > 0)
                            {
                                await Task.Delay(millsToDelay);
                            }
                            else
                            {
                                await Task.Delay(0);
                            }
                            capVideo.Read(imgFrame);
                        }
                        else
                        {
                            stopwatch.Stop();

                            CvInvoke.PutText(imgFrame, fps.ToString("0.00") + " fps, " + imgFrame.Width + "x" + imgFrame.Height, new Point(0, 25), Emgu.CV.CvEnum.FontFace.HersheyComplex, 1, new MCvScalar(255, 255, 255), 3);
                            pictureBox1.Image = imgFrame.ToImage<Bgr, Byte>().ToBitmap(); ;
                            stopwatch.Stop();
                            double computeTime = stopwatch.ElapsedMilliseconds;
                            int millsToDelay = (int)((1000.0 / fps) - computeTime);
                            if (millsToDelay > 0)
                            {
                                await Task.Delay(millsToDelay);
                            }
                            else
                            {
                                await Task.Delay(0);
                            }


                            capVideo.Read(imgFrame);

                            if (imgFrame == null)
                            {

                                textbox_log.AppendText(CreateLogCommunicate("Video has ended"));
                                break;
                                //if (isCaptureRunning)
                                //{
                                //    button1.PerformClick();
                                //}
                                //if (isAnalyseRunning)
                                //{
                                //    button2.PerformClick();
                                //}

                                //initVideoSource(VideoSource.FILE, currentPath);
                            }


                        }



                        //CvInvoke.PutText(im2, fps.ToString("0.00") + " fps, " + imageInput.Width + "x" + imageInput.Height, new Point(0, 25), Emgu.CV.CvEnum.FontFace.HersheySimplex, 1, new MCvScalar(255, 255, 255), 1);
                        // CvInvoke.PutText(imageInput, "Count: " + objectCount, new Point(0, imageInput.Height - 25), Emgu.CV.CvEnum.FontFace.HersheySimplex, 1, new MCvScalar(255, 255, 255), 2);
                        //DrawPassLine(imageInput, imageInput.Height, imageInput.Width);


                        //await Task.Delay(1000 / Convert.ToInt32(fps));
                    }/*
                    else
                    {
                        break;
                    }*/
                }
            }
            catch (Exception ex)
            {
                textbox_log.AppendText(CreateLogCommunicate(ex.Message));
            }

        }
        private String CreateLogCommunicate(String communicate)
        {
            return "<" + DateTime.Now.ToString("HH:mm:ss") + "> <" + communicate + ">\n";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (capVideo == null)
            {
                textbox_log.AppendText(CreateLogCommunicate("Select video source"));
                return;
            }
            else if (!isCaptureRunning)
            {
                textbox_log.AppendText(CreateLogCommunicate("Start video playback first"));
                return;
            }

            isAnalyseRunning = !isAnalyseRunning;
            if (isAnalyseRunning)
            {
                button2.Text = "Analyse stop";
                textbox_log.AppendText(CreateLogCommunicate("Analyse has been started"));
            }
            else
            {
                button2.Text = "Analyse start";
                textbox_log.AppendText(CreateLogCommunicate("Analyse has been stoped"));
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {



        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DatabaseSettings databaseSettings = new DatabaseSettings();
            DialogResult dialogResult = databaseSettings.ShowDialog(this);
            if (dialogResult == DialogResult.OK)
            {
                this.host = databaseSettings.getHost();
                this.username = databaseSettings.getUsername();
                this.password = databaseSettings.getPassword();
                this.database = databaseSettings.getDatabase();
                connString = "Host=localhost;Username=postgres;Password=;Database=test";
                connString = "Host=" + host + ";Username=" + username + ";Password=" + password + ";Database=" + database;
                textbox_log.AppendText(CreateLogCommunicate(databaseSettings.getLog()));
            }
            else if (dialogResult == DialogResult.Abort)
            {
                textbox_log.AppendText(CreateLogCommunicate(databaseSettings.getLog()));
            }

        }

        private void addNewMeasureRow(String time, int id, int width, int height)
        {
            DataRow dr = dt.NewRow();
            dr[0] = time;
            dr[1] = id;
            dr[2] = width;
            dr[3] = height;
            dt.Rows.Add(dr);
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            if (dt.Rows.Count > 0)
            {
                measures.Clear();
                dt.Rows.Clear();
                dataGridView1.Refresh();
                textbox_log.AppendText(CreateLogCommunicate("Measurements has been cleared"));
            }
            else
            {
                textbox_log.AppendText(CreateLogCommunicate("There are no measurements to clear"));
            }
        }

        private void selectVideoFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog choofdlog = new OpenFileDialog();
            choofdlog.Filter = "All Files (*.mp4*)|*.mp4*";
            choofdlog.FilterIndex = 1;
            choofdlog.Multiselect = false;

            if (choofdlog.ShowDialog() == DialogResult.OK)
            {
                currentPath = choofdlog.FileName;
                initVideoSource(VideoSource.FILE, currentPath);
            }

        }

        public enum VideoSource { EXTERNAL_CAMERA, INTERNAL_CAMERA, FILE }

        private void internalCameraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            initVideoSource(VideoSource.INTERNAL_CAMERA, null);
        }

        private void externalCameraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            initVideoSource(VideoSource.EXTERNAL_CAMERA, null);
        }
    }
}
