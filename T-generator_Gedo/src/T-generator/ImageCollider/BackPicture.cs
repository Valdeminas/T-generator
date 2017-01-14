using ImageSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace T_generator.ImageCollider
    {
    public class BackPicture
        {
        public Image m_backgroundImage;
        public Point m_topLeftPoint;
        public Point m_topRightPoint;

        public BackPicture(Image image, Point topLeftPoint, Point topRightPoint)
            {
            CheckAndAssingPoints(topLeftPoint, topRightPoint);
            m_topRightPoint = topRightPoint;
            }

        public BackPicture(string imageLocation, Point topLeftPoint, Point topRightPoint)
            {
            CheckAndAssingPoints(topLeftPoint, topRightPoint);
            using (FileStream imageStream = File.OpenRead(imageLocation))
                m_backgroundImage = new Image(imageStream);
            }

        private void CheckAndAssingPoints(Point topLeft, Point topRight)
            {
            if (topLeft.X >= topRight.X)
                throw new ArgumentException("Top left point is more to right than top right point.");

            if (topLeft.Y != topRight.Y)
                throw new ArgumentException("Top points is not in the same level.");

            m_topLeftPoint = topLeft;
            m_topRightPoint = topRight;
            }
        }
    }
