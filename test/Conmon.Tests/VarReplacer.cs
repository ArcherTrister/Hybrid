using Aspose.Words;
using Aspose.Words.Drawing;
using Aspose.Words.Replacing;
using Hybrid.Application.Services.Dtos;
using Microsoft.AspNetCore.Http;
using SkiaSharp;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Conmon.Tests
{
    /// <summary>
    /// 模板变量替换处理器
    /// </summary>
    public class VarReplacer : IReplacingCallback
    {
        private readonly Document _doc;
        private readonly List<NameValue> _vars = new List<NameValue>();
        private readonly DefaultValueHandler _defaultValueHandler;
        private readonly object _state;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="vars">变量键值对集合</param>
        public VarReplacer(
            Document doc,
            List<NameValue> vars,
            DefaultValueHandler defaultValueHandler,
            object state)
        {
            _doc = doc;
            _vars = vars;
            _defaultValueHandler = defaultValueHandler;
            _state = state;
        }

        public ReplaceAction Replacing(ReplacingArgs args)
        {
            List<ImageVar> images = new List<ImageVar>();
            if (_state != null)
            {
                images = (List<ImageVar>)_state;
            }
            var varItem = _vars.FirstOrDefault(p => p.Name == args.Match.Value);
            if (varItem == null)
            {
                //检查该模板变量是否是图片字段，如果是，则将模板变量替换成图片
                var imageItem = images.FirstOrDefault(p => p.Name == args.Match.Value);
                if (imageItem == null)
                {
                    //非图片变量
                    args.Replacement = _defaultValueHandler?.Invoke(args.Match.Value) ?? string.Empty;
                }
                else
                {
                    //图片变量
                    if (!string.IsNullOrWhiteSpace(imageItem.Value))
                    {
                        var imagePath = imageItem.Value; // HttpContext.Current.Server.MapPath($"~/{imageItem.Value}");
                        if (!File.Exists(imagePath))
                        {
                            return ReplaceAction.Skip;
                        }
                        DocumentBuilder builder = new DocumentBuilder(_doc);
                        builder.MoveTo(args.MatchNode);
                        Shape image;
                        if (imageItem.ImageHandler != null)
                        {
                            //var newImg = imageItem.ImageHandler(imagePath, imageItem.Angle);
                            //MemoryStream mStream = new MemoryStream();
                            //newImg.Save(mStream, System.Drawing.Imaging.ImageFormat.Png);
                            //image = builder.InsertImage(mStream, imageItem.Width, imageItem.Height);
                            //mStream.Dispose();
                            var ms = FileSecurity.Instance.Descrypt(imagePath);
                            image = builder.InsertImage(ms, imageItem.Width, imageItem.Height);
                            ms.Dispose();
                        }
                        else
                        {
                            var ms = FileSecurity.Instance.Descrypt(imagePath);
                            image = builder.InsertImage(ms, imageItem.Width, imageItem.Height);
                            ms.Dispose();
                        }
                        image.AllowOverlap = false;
                        image.WrapType = imageItem.WrapType;
                        image.VerticalAlignment = imageItem.VerticalAlignment;
                        image.HorizontalAlignment = imageItem.HorizontalAlignment;
                        image.RelativeVerticalPosition = imageItem.RelativeVerticalPosition;
                        image.RelativeHorizontalPosition = imageItem.RelativeHorizontalPosition;
                        image.Top = imageItem.Top;
                        image.Left = imageItem.Left;
                        image.Rotation = imageItem.Angle;
                    }
                }
            }
            else
            {
                args.Replacement = varItem.Value.Replace("\n", "\r\n") ?? string.Empty;
            }

            return ReplaceAction.Replace;
        }
    }

    ///// <summary>
    ///// 模板变量替换处理器
    ///// </summary>
    //public class VarReplacer : IReplacingCallback
    //{
    //    private readonly Document _doc;
    //    private readonly List<NameValue> _vars = new List<NameValue>();
    //    private readonly DefaultValueHandler _defaultValueHandler;
    //    private readonly object _state;

    //    /// <summary>
    //    /// 构造方法
    //    /// </summary>
    //    /// <param name="vars">变量键值对集合</param>
    //    public VarReplacer(
    //        Document doc,
    //        List<NameValue> vars,
    //        DefaultValueHandler defaultValueHandler,
    //        object state)
    //    {
    //        _doc = doc;
    //        _vars = vars;
    //        _defaultValueHandler = defaultValueHandler;
    //        _state = state;
    //    }

    //    public ReplaceAction Replacing(ReplacingArgs args)
    //    {
    //        List<ImageVar> images = new List<ImageVar>();
    //        if (_state != null)
    //        {
    //            images = (List<ImageVar>)_state;
    //        }
    //        var varItem = _vars.FirstOrDefault(p => p.Name == args.Match.Value);
    //        if (varItem == null)
    //        {
    //            //检查该模板变量是否是图片字段，如果是，则将模板变量替换成图片
    //            var imageItem = images.FirstOrDefault(p => p.Name == args.Match.Value);
    //            if (imageItem == null)
    //            {
    //                //非图片变量
    //                args.Replacement = _defaultValueHandler?.Invoke(args.Match.Value) ?? string.Empty;
    //            }
    //            else
    //            {
    //                //图片变量
    //                if (!string.IsNullOrWhiteSpace(imageItem.Value))
    //                {
    //                    var imagePath = imageItem.Value; //HttpContext.Current.Server.MapPath($"~/{imageItem.Value}");
    //                    if (!File.Exists(imagePath))
    //                    {
    //                        return ReplaceAction.Skip;
    //                    }
    //                    DocumentBuilder builder = new DocumentBuilder(_doc);
    //                    builder.MoveTo(args.MatchNode);
    //                    Shape image;
    //                    if (imageItem.ImageHandler != null)
    //                    {
    //                        MemoryStream mStream = new MemoryStream();
    //                        this.pictureBox1.Image.Save(mStream, System.Drawing.Imaging.ImageFormat.Jpeg);
    //                        byte[] bytes = new byte[mStream.Length];
    //                        mStream.Write(bytes, 0, (int)mStream.Length);

    //                        image = builder.InsertImage(sKBitmap);
    //                    }
    //                    else
    //                    {
    //                        image = builder.InsertImage(imagePath, imageItem.Width, imageItem.Height);
    //                    }
    //                    image.AllowOverlap = false;
    //                    image.WrapType = imageItem.WrapType;
    //                    image.VerticalAlignment = imageItem.VerticalAlignment;
    //                    image.HorizontalAlignment = imageItem.HorizontalAlignment;
    //                    image.RelativeVerticalPosition = imageItem.RelativeVerticalPosition;
    //                    image.RelativeHorizontalPosition = imageItem.RelativeHorizontalPosition;
    //                    image.Top = imageItem.Top;
    //                    image.Left = imageItem.Left;
    //                }
    //            }
    //        }
    //        else
    //        {
    //            args.Replacement = varItem.Value ?? string.Empty;
    //        }

    //        return ReplaceAction.Replace;
    //    }
    //}
}
