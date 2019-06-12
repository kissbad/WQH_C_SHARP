using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WQH.system.IO
{
    public class Image
    {
        /// <summary>
        /// 收缩图片
        /// </summary>
        /// <param name="image"></param>
        /// <param name="height"></param>
        /// <param name="width"></param>
        /// <returns></returns>
        public static System.Drawing.Image Scaled(System.Drawing.Image image, int height, int width) {
            //float scale = Math.Min(image.Width / (float)width, image.Height / (float)height);
            //int x = (int)Math.Round(image.Width > width * scale ? (image.Width - width * scale) / 2 : 0, 0);
            //int y = (int)Math.Round(image.Height > height * scale ? (image.Height - height * scale) / 2 : 0, 0);

            var new_image = new Bitmap(height, width);
            foreach (var pItem in image.PropertyItems)
            {
                new_image.SetPropertyItem(pItem);
            }
            var graphics = Graphics.FromImage(new_image);
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            graphics.DrawImage(image, new Rectangle(0, 0, height, width));
            image.Dispose();
            return new_image;
        }
    }
}
