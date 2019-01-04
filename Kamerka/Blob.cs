using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.Util;


namespace Kamerka
{
    public class Blob
    {

        public Emgu.CV.Util.VectorOfPointF currentContour = new Emgu.CV.Util.VectorOfPointF();
        public RotatedRect currentBoundingRect;
        public Emgu.CV.Util.VectorOfPointF centerPositions = new Emgu.CV.Util.VectorOfPointF();
      
        
       
        public double dblCurrentDiagonalSize;
        public double dblCurrentAspectRatio;
        public bool blnCurrentBlobFoundOrNewBlob;
        public bool blnStillBeingTracked;
        public int intNumOfConsecutiveFramesWithoutAMatch;

        public PointF predictedNextPosition = new PointF();


        public Blob(Emgu.CV.Util.VectorOfPointF contour)
        {

            this.currentContour = contour;

           // this.currentContour = contour;
            currentBoundingRect = CvInvoke.MinAreaRect(contour);

            PointF[] currentCenter = new PointF[1];
            currentCenter[0] = currentBoundingRect.Center;
            centerPositions.Push(currentCenter);

            this.dblCurrentDiagonalSize = Math.Sqrt(Math.Pow(currentBoundingRect.Size.Width, 2) + Math.Pow(currentBoundingRect.Size.Height, 2));
            this.dblCurrentAspectRatio = (float)currentBoundingRect.Size.Width / (float)currentBoundingRect.Size.Height;
            this.blnStillBeingTracked = true;
            this.blnCurrentBlobFoundOrNewBlob = true;

            this.intNumOfConsecutiveFramesWithoutAMatch = 0;

        }

        public void predictNextPosition()
        {
            int numPositions = centerPositions.Size;

            if(numPositions == 1)
            {
                predictedNextPosition.X = centerPositions[numPositions - 1].X;
                predictedNextPosition.Y = centerPositions[numPositions - 1].Y;
            }

            else if(numPositions == 2)
            {
                int deltaX =(int) (centerPositions[1].X - centerPositions[0].X);
                int deltaY = (int)(centerPositions[1].Y - centerPositions[0].Y);

                predictedNextPosition.X = centerPositions[numPositions - 1].X + deltaX;
                predictedNextPosition.Y = centerPositions[numPositions - 1].Y + deltaY;

             }
            else if( numPositions == 3)
            {
                int sumOfXChanges = (int)(((centerPositions[2].X - centerPositions[1].X) * 2) +
                    ((centerPositions[2].X - centerPositions[1].X) * 1));
                int deltaX = (int)Math.Round((float)sumOfXChanges / 3.0, MidpointRounding.ToEven);

                int sumOfYChanges = (int)(((centerPositions[2].Y - centerPositions[1].Y) * 2) +
                    ((centerPositions[2].Y - centerPositions[1].Y) * 1));
                int deltaY = (int)Math.Round((float)sumOfYChanges / 3.0, MidpointRounding.ToEven);

                predictedNextPosition.X = centerPositions[numPositions - 1].X + deltaX;
                predictedNextPosition.Y = centerPositions[numPositions - 1].Y + deltaY;
            }
            else if( numPositions == 4)
            {
                int sumOfXChanges = (int)(((centerPositions[3].X - centerPositions[2].X) * 3) +
            ((centerPositions[2].X - centerPositions[1].X) * 2) + ((centerPositions[1].X- centerPositions[0].X) * 1));

                int deltaX = (int)Math.Round((float)sumOfXChanges / 6.0, MidpointRounding.ToEven);


                int sumOfYChanges = (int)(((centerPositions[3].Y - centerPositions[2].Y) * 3) +
            ((centerPositions[2].Y - centerPositions[1].Y) * 2) + ((centerPositions[1].Y - centerPositions[0].Y) * 1));

                int deltaY = (int)Math.Round((float)sumOfXChanges / 6.0, MidpointRounding.ToEven);

                predictedNextPosition.X = centerPositions[numPositions - 1].X + deltaX;
                predictedNextPosition.Y = centerPositions[numPositions - 1].Y + deltaY;
            }
            else if (numPositions >= 5)
            {
                int sumOfXChanges = (int)(((centerPositions[numPositions - 1].X - centerPositions[numPositions - 2].X) * 4) +
            ((centerPositions[numPositions - 2].X - centerPositions[numPositions - 3].X) * 3) +
            ((centerPositions[numPositions - 3].X - centerPositions[numPositions - 4].X) * 2) +
            ((centerPositions[numPositions - 4].X - centerPositions[numPositions - 5].X) * 1));

                int deltaX = (int)Math.Round((float)sumOfXChanges / 10.0, MidpointRounding.ToEven);

                int sumOfYChanges = (int)(((centerPositions[numPositions - 1].Y - centerPositions[numPositions - 2].Y) * 4) +
           ((centerPositions[numPositions - 2].Y - centerPositions[numPositions - 3].Y) * 3) +
           ((centerPositions[numPositions - 3].Y - centerPositions[numPositions - 4].Y) * 2) +
           ((centerPositions[numPositions - 4].Y - centerPositions[numPositions - 5].Y) * 1));

                int deltaY = (int)Math.Round((float)sumOfXChanges / 10.0, MidpointRounding.ToEven);


                predictedNextPosition.X = centerPositions[numPositions - 1].X + deltaX;
                predictedNextPosition.Y = centerPositions[numPositions - 1].Y + deltaY;
            }
            else
            {
                //Should never go in here
            }
        }

    }
}
