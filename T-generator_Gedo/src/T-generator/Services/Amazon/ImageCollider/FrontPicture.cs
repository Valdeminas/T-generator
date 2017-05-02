using ImageSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace T_generator.Services.Amazon
{
    public class FrontPicture
        {
        public Image m_frontImage;
        public int m_opacity;

        public FrontPicture(Image frontImage, int opacity)
            {
            CheckAndAssignOpacity(opacity);
            m_frontImage = frontImage;
            }

        public FrontPicture(string imageLocation, int opacity)
            {
            CheckAndAssignOpacity(opacity);
            using (FileStream imageStream = File.OpenRead(imageLocation))
                m_frontImage = new Image(imageStream);
            }

        private void CheckAndAssignOpacity(int opacity)
            {
            if (opacity <= 0 ||
                opacity > 100)
                throw new ArgumentException("Opacity must be between 0 and 100");

            m_opacity = opacity;
            }
        }
    }
