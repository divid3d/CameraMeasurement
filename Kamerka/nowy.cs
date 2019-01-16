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

        bool databaseConnected = false;

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

            if (databaseConnected)
            {
                try
                {
                    Npgsql.NpgsqlConnection npgsqlConnection = new Npgsql.NpgsqlConnection(connString);
                    String sql = "insert into pomiary values(@id,@date,@time,@width,@height)";
                    npgsqlConnection.Open();
                    Npgsql.NpgsqlCommand cmd = new Npgsql.NpgsqlCommand(sql, npgsqlConnection);
                    cmd.Parameters.AddWithValue("@id", NpgsqlTypes.NpgsqlDbType.Integer, Convert.ToInt32(measures.Count));
                    cmd.Parameters.AddWithValue("@date", NpgsqlTypes.NpgsqlDbType.Date, DateTime.Now.Date);
                    cmd.Parameters.AddWithValue("@time", NpgsqlTypes.NpgsqlDbType.Text, DateTime.Now.ToString("h:mm:ss tt"));
                    cmd.Parameters.AddWithValue("@width", NpgsqlTypes.NpgsqlDbType.Integer, Convert.ToInt32(measuredObject.getWidth()));
                    cmd.Parameters.AddWithValue("@height", NpgsqlTypes.NpgsqlDbType.Integer, Convert.ToInt32(measuredObject.getHeight()));
                    cmd.Prepare();
                    cmd.ExecuteNonQuery();
                }
                catch (Npgsql.NpgsqlException ex)
                {
                    textbox_log.AppendText(CreateLogCommunicate(ex.Message));
                }
                finally
                {
                    if (npgsqlConnection != null) npgsqlConnection.Close();
                }
            }
            
        }

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

        bool checkIfBloobsCrossedTheLine(List<Blob> blobs, int verticalLinePosition, Mat imgCopy)
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

            capVideo.Read(imgFrame);

            int intVericalLinePosition = imgFrame.Width / 2;
            crossingLine[0].X = intVericalLinePosition;
            crossingLine[0].Y = 0;

            crossingLine[1].X = intVericalLinePosition;
            crossingLine[1].Y = imgFrame.Height;
            bool blnFirstFrame = true;

            try
            {
                while (isCaptureRunning)
                {
                    stopwatch.Reset();
                    stopwatch.Start();


                    if (!imgFrame.IsEmpty)
                    {
                    
                        fps = capVideo.GetCaptureProperty(Emgu.CV.CvEnum.CapProp.Fps);


                        if (isAnalyseRunning)
                        {
                            List<Blob> currentFrameBlobs = new List<Blob>();

                            Mat colorFiltered = new Mat();
                            CvInvoke.CvtColor(imgFrame, colorFiltered, ColorConversion.Rgb2Hsv);
                            CvInvoke.InRange(colorFiltered, lowerHsvLimit, upperHsvLimit, colorFiltered);
                            CvInvoke.GaussianBlur(colorFiltered, colorFiltered, new Size(5, 5), 0);
                            Mat structuringElement5x5 = CvInvoke.GetStructuringElement(Emgu.CV.CvEnum.ElementShape.Rectangle, new Size(5, 5), new Point(-1, -1));
                            CvInvoke.Erode(colorFiltered, colorFiltered, structuringElement5x5, new Point(-1, -1), 1, BorderType.Default, new MCvScalar(1.0));
                            
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


                            bool blnAtLeastOneBlobCrossedTheLine = checkIfBloobsCrossedTheLine(blobs, intVericalLinePosition, imgFrame);

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
                            blnFirstFrame = false;
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
                                
                            }                   
                        }
                    }
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
                databaseConnected = true;
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

        private void nowy_Load(object sender, EventArgs e)
        {

        }
    }
}
