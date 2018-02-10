using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using ImageSharp;
using System.Numerics;

namespace T_generator.Services.Amazon
{
    public class ImageCollider
        {
        //private static string testImgNo1 = @"C:\Users\Benas.Kikutis\Desktop\GediminasProject\oldFiles\download.jpg";
        //private static string testImgNo2 = @"C:\Users\Benas.Kikutis\Desktop\GediminasProject\oldFiles\images.jpg";
        //private static string testAddImgNo1 = @"C:\Users\Benas.Kikutis\Desktop\GediminasProject\oldFiles\BMCE.png";
        //private static string testAddImgNo1Updated = @"C:\Users\Benas.Kikutis\Desktop\GediminasProject\oldFiles\BMCEupdated.jpg";
        //private static string testAddImgNo2 = @"C:\Users\Benas.Kikutis\Desktop\GediminasProject\oldFiles\BMDE.png";

        //private static string testSaveLoc = @"C:\Users\Benas.Kikutis\Desktop\GediminasProject\oldFiles\saved2.jpg";

        public static Image MergeImages(BackPicture background, FrontPicture frontImage)
            {
            int resizedWidth_byPoints = background.m_topRightPoint.X - background.m_topLeftPoint.X;
            int resizedHeight_byPoints = background.m_botLeftPoint.Y - background.m_topLeftPoint.Y;
            float coeff = MathF.Min((float)resizedWidth_byPoints / (float)frontImage.m_frontImage.Width, (float)resizedHeight_byPoints / (float)frontImage.m_frontImage.Height);
            int resizedHeght = (int)(frontImage.m_frontImage.Height * coeff);
            int resizedWidth = (int)(frontImage.m_frontImage.Width * coeff);
            return background.m_backgroundImage.Blend(frontImage.m_frontImage.Resize(resizedWidth, resizedHeght).Pad(
                background.m_topLeftPoint.X * 2 +  resizedWidth_byPoints, background.m_topLeftPoint.Y * 2 + resizedHeight_byPoints), frontImage.m_opacity) as Image;
            }

        //public static void Test()
        //{
        //    Point topLeft = new Point(100, 100);
        //    Point topRight = new Point(200, 100);

        //    using (FileStream savePic = File.OpenWrite(testSaveLoc))
        //        MergeImages(new BackPicture(testImgNo2, topLeft, topRight),
        //                    new FrontPicture(testAddImgNo2, 40)).DrawLines(new Color(1, 1, 1), 1, new Vector2[] { new Vector2(topLeft.X, 0), new Vector2(topLeft.X, 1000) }).
        //                                                         DrawLines(new Color(1, 1, 1), 1, new Vector2[] { new Vector2(0, topLeft.Y), new Vector2(1000, topLeft.Y) }).Save(savePic);
        //}
    }
    }
