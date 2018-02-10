using ImageSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace T_generator.Services.Amazon
{
    public class BackPicture
        {
        public Image m_backgroundImage;
        public Point m_topLeftPoint;
        public Point m_topRightPoint;
        public Point m_botLeftPoint;
        public Point m_botRightPoint;

        public BackPicture(Image image, Point topLeftPoint, Point topRightPoint, Point botLeftPoint, Point botRightPoint)
            {
            CheckAndAssingPoints(topLeftPoint, topRightPoint,botLeftPoint,botRightPoint);
            m_topRightPoint = topRightPoint;
            }

        public BackPicture(string imageLocation, Point topLeftPoint, Point topRightPoint, Point botLeftPoint, Point botRightPoint)
            {
            CheckAndAssingPoints(topLeftPoint, topRightPoint, botLeftPoint, botRightPoint);
            using (FileStream imageStream = File.OpenRead(imageLocation))
                m_backgroundImage = new Image(imageStream);
            }

        private void CheckAndAssingPoints(Point topLeft, Point topRight, Point botLeft, Point botRight)
            {
            if (topLeft.X >= topRight.X)
                throw new ArgumentException("Left point is more to right than top right point.");

            if (botLeft.Y <= topRight.Y)
                throw new ArgumentException("Bot point is higher than top point.");

            if (topLeft.Y != topRight.Y)
                throw new ArgumentException("Top points is not in the same level.");

            m_topLeftPoint = topLeft;
            m_topRightPoint = topRight;
            m_botLeftPoint = botLeft;
            m_botRightPoint = botRight;
        }
        }
    }
