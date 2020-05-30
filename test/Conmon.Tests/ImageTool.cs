using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Conmon.Tests
{
    /// <summary>
    /// 图片处理工具类
    /// </summary>
    public class ImageTool
    {
        /// <summary>  
        /// 合并图片  
        /// </summary>  
        /// <param name="imgBack"></param>  
        /// <param name="img"></param>  
        /// <returns></returns>  
        public static Bitmap CombinImage(Image imgBack, Image img, int xDeviation = 0, int yDeviation = 0)
        {
            Bitmap bmp = new Bitmap(400, 400);
            Graphics g = Graphics.FromImage(bmp);
            g.Clear(Color.FromArgb(240, 240, 240));
            g.DrawImage(imgBack, 50, 0, imgBack.Width, imgBack.Height); //g.DrawImage(imgBack, 0, 0, 相框宽, 相框高);     
            g.DrawImage(img, 400 / 2 - img.Width / 2 + xDeviation, 400 / 2 - img.Height / 2 + yDeviation, img.Width, img.Height);
            g.Dispose();
            return bmp;
        }

        /// <summary>  
        /// Resize图片  
        /// </summary>  
        /// <param name="bmp">原始Bitmap</param>  
        /// <param name="newW">新的宽度</param>  
        /// <param name="newH">新的高度</param>
        /// <returns>处理以后的图片</returns>  
        public static Image ResizeImage(Image bmp, int newW, int newH)
        {
            try
            {
                Image b = new Bitmap(newW, newH);
                Graphics g = Graphics.FromImage(b);

                // 插值算法的质量    
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.DrawImage(bmp, new Rectangle(0, 0, newW, newH), new Rectangle(0, 0, bmp.Width, bmp.Height),
                            GraphicsUnit.Pixel);
                g.Dispose();
                return b;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 将指定的图片横向切割为多个图片
        /// </summary>
        /// <param name="src">原图</param>
        /// <param name="percents">百分比</param>
        /// <returns>切割后的图片</returns>
        public static Image[] SplitImages(Image src, List<decimal> percents)
        {
            var originWidth = src.Width;
            var height = src.Height;
            List<Image> images = new List<Image>();
            int offsetX = 0;
            foreach (var percent in percents)
            {
                int width = (int)Math.Round(originWidth * percent);
                Image img = new Bitmap(width, height);
                Graphics g = Graphics.FromImage(img);
                g.Clear(Color.FromArgb(255, 255, 255));
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.DrawImage(src, new Rectangle(0, 0, width, height), new Rectangle(offsetX, 0, width, height), GraphicsUnit.Pixel);
                g.Dispose();

                offsetX += width;
                images.Add(img);
            }

            return images.ToArray();
        }

        /// <summary>
        /// 根据角度旋转图标
        /// </summary>
        /// <param name="imgPath"></param>
        /// <param name="angle"></param>
        /// <returns></returns>
        public static Image RotateImg(string imgPath, float angle)
        {
            using (Image img = Image.FromFile(imgPath))
            {
                //通过Png图片设置图片透明，修改旋转图片变黑问题。
                int width = img.Width;
                int height = img.Height;
                //角度
                Matrix mtrx = new Matrix();
                mtrx.RotateAt(angle, new PointF((width / 2), (height / 2)), MatrixOrder.Append);
                //得到旋转后的矩形
                GraphicsPath path = new GraphicsPath();
                path.AddRectangle(new RectangleF(0f, 0f, width, height));
                RectangleF rct = path.GetBounds(mtrx);
                //生成目标位图
                Bitmap devImage = new Bitmap((int)(rct.Width), (int)(rct.Height));
                Graphics g = Graphics.FromImage(devImage);
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Bilinear;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                //计算偏移量
                Point Offset = new Point((int)(rct.Width - width) / 2, (int)(rct.Height - height) / 2);
                //构造图像显示区域：让图像的中心与窗口的中心点一致
                Rectangle rect = new Rectangle(Offset.X, Offset.Y, (int)width, (int)height);
                Point center = new Point((int)(rect.X + rect.Width / 2), (int)(rect.Y + rect.Height / 2));
                g.TranslateTransform(center.X, center.Y);
                g.RotateTransform(angle);
                //恢复图像在水平和垂直方向的平移
                g.TranslateTransform(-center.X, -center.Y);
                g.DrawImage(img, rect);
                //重至绘图的所有变换
                g.ResetTransform();
                g.Save();
                g.Dispose();
                path.Dispose();
                return devImage;
            }
        }

        /// <summary>
        /// 第二种方法
        /// </summary>
        /// <param name="img"></param>
        /// <param name="angle"></param>
        /// <returns></returns>
        public static Image RotateImg(Image img, float angle)
        {
            angle = angle % 360;            //弧度转换
            double radian = angle * Math.PI / 180.0;
            double cos = Math.Cos(radian);
            double sin = Math.Sin(radian);
            //原图的宽和高
            int w = img.Width;
            int h = img.Height;
            int W = (int)(Math.Max(Math.Abs(w * cos - h * sin), Math.Abs(w * cos + h * sin)));
            int H = (int)(Math.Max(Math.Abs(w * sin - h * cos), Math.Abs(w * sin + h * cos)));
            //目标位图
            Image dsImage = new Bitmap(W, H);
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(dsImage);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Bilinear;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            //计算偏移量
            Point Offset = new Point((W - w) / 2, (H - h) / 2);
            //构造图像显示区域：让图像的中心与窗口的中心点一致
            Rectangle rect = new Rectangle(Offset.X, Offset.Y, w, h);
            Point center = new Point(rect.X + rect.Width / 2, rect.Y + rect.Height / 2);
            g.TranslateTransform(center.X, center.Y);
            g.RotateTransform(360 - angle);
            //恢复图像在水平和垂直方向的平移
            g.TranslateTransform(-center.X, -center.Y);
            g.DrawImage(img, rect);
            //重至绘图的所有变换
            g.ResetTransform();
            g.Save();
            g.Dispose();
            //dsImage.Save("yuancd.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
            return dsImage;
        }
    }
}
