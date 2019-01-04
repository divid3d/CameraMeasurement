using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kamerka
{
    class measuredObject
    {
       private  int id;
       private  int height;
       private int width;
       private String measureTime;

        public measuredObject(int id, int height, int width, String measureTime)
        {
            this.id = id;
            this.height = height;
            this.width = width;
            this.measureTime = measureTime;
        }

        
        public int getId()
        {
            return this.id;
        }

       public int getWidth()
        {
            return this.width;
        }

        public int getHeight()
        {
            return this.height;
        }

        public String getMeasureTime()
        {
            return this.measureTime;
        }

    }
}
