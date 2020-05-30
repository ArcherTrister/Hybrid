using Aspose.Words.Drawing;

using Hybrid.Application.Services.Dtos;

using System;
using System.Drawing;

namespace Conmon.Tests
{
    /// <summary>
    /// 图片变量
    /// </summary>
    public class ImageVar : NameValue
    {
        public ImageVar(string name, string value)
        {
            Name = name;
            Value = value;
        }

        public ImageVar(string name, string value, int width, int height)
        {
            Name = name;
            Value = value;
            Width = width;
            Height = height;
        }

        /// <summary>
        /// 宽度
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// 高度
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// 顶部偏移位置(单位：磅)
        /// </summary>
        public int Top { get; set; }

        /// <summary>
        /// 左边偏移位置(单位：磅)
        /// </summary>
        public int Left { get; set; }

        /// <summary>
        /// 旋转角度
        /// </summary>
        public double Angle { get; set; }

        /// <summary>
        /// 水平对齐方式
        /// </summary>
        public HorizontalAlignment HorizontalAlignment { get; set; } = HorizontalAlignment.Left;

        /// <summary>
        /// 垂直对齐方式
        /// </summary>
        public VerticalAlignment VerticalAlignment { get; set; } = VerticalAlignment.Top;

        /// <summary>
        /// 水平定位方式
        /// </summary>
        public RelativeHorizontalPosition RelativeHorizontalPosition { get; set; } = RelativeHorizontalPosition.Column;

        /// <summary>
        /// 垂直定位方式
        /// </summary>
        public RelativeVerticalPosition RelativeVerticalPosition { get; set; } = RelativeVerticalPosition.Paragraph;

        /// <summary>
        /// 换行方式
        /// </summary>
        public WrapType WrapType { get; set; } = WrapType.None;

        /// <summary>
        /// 用于插入word之前的图片处理方法
        /// </summary>
        public Func<string, float, Image> ImageHandler { get; set; }
    }
}
