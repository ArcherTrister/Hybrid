using Aspose.Words;
using Aspose.Words.Drawing;
using Aspose.Words.Tables;

using Hybrid.Application.Services.Dtos;
using Hybrid.Entity;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using SignInfo;

using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Conmon.Tests
{
    [TestClass]
    public class Tests
    {
        [TestMethod]
        public async Task ImageTest()
        {
            //combine.png
            //dcfr.png
            //2s4jH7YNxoeiruDyvMhlLapQk8O0PfwRmTW+ZqF5Sb9/zBnCKgXGUtIAc3Ed6=V1J
            using (var image = Image.FromFile("E:\\DEMO\\jbr.jpg"))//E:\\DEMO\\combine.png
            {
                double imgWidth = image.Width / image.HorizontalResolution;
                double imgHeight = image.Height / image.VerticalResolution;

                var imgExt = GetImageExtExtension(image);

                var signName = Guid.NewGuid().ToString("N");

                SignInfoServiceClient client = new SignInfoServiceClient();

                var addSignInfoResponse = await client.addSignInfoAsync(ImageToBytes(image), imgExt, signName, "003", imgWidth.ToString("0.00"), imgHeight.ToString("0.00"));
                var signsn = addSignInfoResponse.signsn;

                //var getSigninfoBySignSNResponse = await client.getSigninfoBySignSNAsync("2s4jH7YNxoeiruDyvMhlLapQk8O0PfwRmTW+ZqF5Sb9/zBnCKgXGUtIAc3Ed6=V1J");
                //var signInfo = getSigninfoBySignSNResponse.signInfo;

                //var getSigninfoBySignSNResponse = await client.getSigninfoBySignSNAsync("da3c0e21e837459aba37f689dfd3ae23");
                //var signInfo = getSigninfoBySignSNResponse.signInfo;

                var getSignInfosByKeySNResponse = await client.getSignInfosByKeySNAsync("001");
                var signInfos = getSignInfosByKeySNResponse.signInfos;

                //var getSigninfosByUserCodeResponse = await client.getSigninfosByUserCodeAsync("002");
                // var signInfos = getSigninfosByUserCodeResponse.signInfos;
            }
        }

        /// <summary>
        /// Convert Image to Byte[]
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        private byte[] ImageToBytes(Image image)
        {
            ImageFormat format = image.RawFormat;
            using (MemoryStream ms = new MemoryStream())
            {
                if (format.Equals(ImageFormat.Jpeg))
                {
                    image.Save(ms, ImageFormat.Jpeg);
                }
                else if (format.Equals(ImageFormat.Png))
                {
                    image.Save(ms, ImageFormat.Png);
                }
                else if (format.Equals(ImageFormat.Bmp))
                {
                    image.Save(ms, ImageFormat.Bmp);
                }
                else if (format.Equals(ImageFormat.Gif))
                {
                    image.Save(ms, ImageFormat.Gif);
                }
                else if (format.Equals(ImageFormat.Icon))
                {
                    image.Save(ms, ImageFormat.Icon);
                }
                else
                {
                    throw new System.ArgumentException("Invalid File Type");
                }
                byte[] buffer = new byte[ms.Length];
                //Image.Save()��ı�MemoryStream��Position����Ҫ����Seek��Begin
                ms.Seek(0, SeekOrigin.Begin);
                ms.Read(buffer, 0, buffer.Length);
                return buffer;
            }
        }

        private string GetImageExtExtension(Image image)
        {
            try
            {
                ImageFormat format = image.RawFormat;
                if (format.Equals(ImageFormat.Bmp)) return ".bmp";
                else if (format.Equals(ImageFormat.Emf)) return ".wmf";
                else if (format.Equals(ImageFormat.Exif)) return ".exif";
                else if (format.Equals(ImageFormat.Gif)) return ".gif";
                else if (format.Equals(ImageFormat.Icon)) return ".icon";
                else if (format.Equals(ImageFormat.Jpeg)) return ".jpg";
                else if (format.Equals(ImageFormat.MemoryBmp)) return ".bmp";
                else if (format.Equals(ImageFormat.Png)) return ".png";
                else if (format.Equals(ImageFormat.Tiff)) return ".tiff";
                else if (format.Equals(ImageFormat.Wmf)) return ".wmf";
                else
                    throw new System.ArgumentException("Invalid File Type");

                //if (image.RawFormat.Guid == ImageFormat.Bmp.Guid) return ".bmp";
                //else if (image.RawFormat.Guid == ImageFormat.Emf.Guid) return ".wmf";
                //else if (image.RawFormat.Guid == ImageFormat.Exif.Guid) return ".exif";
                //else if (image.RawFormat.Guid == ImageFormat.Gif.Guid) return ".gif";
                //else if (image.RawFormat.Guid == ImageFormat.Icon.Guid) return ".icon";
                //else if (image.RawFormat.Guid == ImageFormat.Jpeg.Guid) return ".jpg";
                //else if (image.RawFormat.Guid == ImageFormat.MemoryBmp.Guid) return ".bmp";
                //else if (image.RawFormat.Guid == ImageFormat.Png.Guid) return ".png";
                //else if (image.RawFormat.Guid == ImageFormat.Tiff.Guid) return ".tiff";
                //else if (image.RawFormat.Guid == ImageFormat.Wmf.Guid) return ".wmf";
                //else
                //    throw new System.ArgumentException("Invalid File Type");
            }
            catch
            {
                throw new System.ArgumentException("Invalid File Type");
            }
        }

        /// <summary>
        /// ��ӡ��ͬ��Ϣ�е�������
        /// </summary>
        public class PrepareNewContractPerson
        {
            /// <summary>
            /// ����
            /// </summary>
            [System.ComponentModel.Description("����")]
            public string Name { get; set; }

            /// <summary>
            /// ����
            /// </summary>
            public string Nationality { get; set; }

            /// <summary>
            /// ֤������
            /// </summary>
            public string CertificateType { get; set; }

            /// <summary>
            /// ֤������
            /// </summary>
            public int CerType { get; set; }

            /// <summary>
            /// ֤������
            /// </summary>
            public string CertificateNumber { get; set; }

            /// <summary>
            /// ��ַ
            /// </summary>
            public string Address { get; set; }

            /// <summary>
            /// ��������
            /// </summary>
            public string PostCode { get; set; }

            /// <summary>
            /// ��ϵ�绰
            /// </summary>
            public string Mobile { get; set; }

            /// <summary>
            /// ռ�б���
            /// </summary>
            public decimal? Ratio { get; set; }

            /// <summary>
            /// ����
            /// </summary>
            public string Registered { get; set; }
        }

        [TestMethod]
        public void BuyersAutoCreateTest()
        {
            var dir = Directory.GetCurrentDirectory();
            string tmppath = dir + "\\BuyersTable.docx";
            Document doc = new Document(tmppath); //����ģ��
            DocumentBuilder builder = new DocumentBuilder(doc);

            List<PrepareNewContractPerson> list = new List<PrepareNewContractPerson>
            {
                new PrepareNewContractPerson { 
                    Name = "����", 
                    PostCode = "22", 
                    Mobile = "22"
                },
                new PrepareNewContractPerson { Name = "����", PostCode = "", Mobile="" }
            };

            builder.MoveToBookmark("BuyersTable");        //��ʼ���ֵ

            StringBuilder sb = new StringBuilder();

            foreach (var item in list)
            {
                sb.AppendFormat("<p style=\"margin-top:0.35pt; margin-right:9.4pt; margin-bottom:0pt; text-align:justify; line-height:24pt; widows:0; orphans:0\"><span style=\"font-family:����; font-size:12pt\">�����ˣ�</span><span style=\"font-family:����; font-size:12pt; text-decoration:underline\">{0}</span><span style=\"width:400pt; font-family:'Lucida Console'; font-size:10pt; text-decoration:underline; display:inline-block; -aw-font-family:'����'; -aw-tabstop-align:left; -aw-tabstop-pos:414pt\"> &#xa0;</span></p>", item.Name);
                sb.AppendFormat("<p style=\"margin-top:0.35pt; margin-right:9.4pt; margin-bottom:0pt; text-align:justify; line-height:24pt; widows:0; orphans:0\"><span style=\"font-family:����; font-size:12pt\">������������ {0}���������� {1}��</span><span style=\"font-family:����; font-size:12pt; text-decoration:underline\">{2}</span><span style=\"width:400pt; font-family:'Lucida Console'; font-size:10pt; text-decoration:underline; display:inline-block; -aw-font-family:'����'; -aw-tabstop-align:left; -aw-tabstop-pos:414pt\"> &#xa0;</span></p>", "��", "x", "��");
                sb.AppendFormat("<p style=\"margin-top:0.35pt; margin-right:9.4pt; margin-bottom:0pt; text-align:justify; line-height:24pt; widows:0; orphans:0\"><span style=\"font-family:����; font-size:12pt\">��������</span><span style=\"font-family:����; font-size:12pt; text-decoration:underline\">{0}</span><span style=\"width:400pt; font-family:'Lucida Console'; font-size:10pt; text-decoration:underline; display:inline-block; -aw-font-family:'����'; -aw-tabstop-align:left; -aw-tabstop-pos:414pt\"> &#xa0;</span></p>", item.Nationality);
                sb.AppendFormat("<p style=\"margin-top:0.35pt; margin-right:9.4pt; margin-bottom:0pt; text-align:justify; line-height:24pt; widows:0; orphans:0\"><span style=\"font-family:����; font-size:12pt\">���������ڵء�</span><span style=\"font-family:����; font-size:12pt; text-decoration:underline\">{0}</span><span style=\"width:400pt; font-family:'Lucida Console'; font-size:10pt; text-decoration:underline; display:inline-block; -aw-font-family:'����'; -aw-tabstop-align:left; -aw-tabstop-pos:414pt\"> &#xa0;</span></p>", item.Registered);
                sb.AppendFormat("<p style=\"margin-top:0.35pt; margin-right:9.4pt; margin-bottom:0pt; text-align:justify; line-height:24pt; widows:0; orphans:0\"><span style=\"font-family:����; font-size:12pt\">֤�����ͣ����������֤{0}��������{1}����Ӫҵִ��{2}����֤�ţ�</span><span style=\"font-family:����; font-size:12pt; text-decoration:underline\">{3}</span></p>", "��", "x", "x", item.CertificateNumber);
                sb.AppendFormat("<p style=\"margin-top:0.35pt; margin-right:9.4pt; margin-bottom:0pt; text-align:justify; line-height:24pt; widows:0; orphans:0\"><span style=\"font-family:����; font-size:12pt\">�������ڣ�</span><span style=\"font-family:����; font-size:12pt; text-decoration:underline\">{0}</span><span style=\"font-family:����; font-size:12pt; text-decoration:underline; -aw-import:spaces\"> &#xa0;</span><span style=\"font-family:����; font-size:12pt;\"> �Ա�</span><span style=\"font-family:����; font-size:12pt; text-decoration:underline\">{1}</span><span style=\"font-family:����; font-size:12pt; text-decoration:underline; -aw-import:spaces\"> &#xa0;</span></p>", "1992��12��02��", "��");
                sb.AppendFormat("<p style=\"margin-top:0.35pt; margin-right:9.4pt; margin-bottom:0pt; text-align:justify; line-height:24pt; widows:0; orphans:0\"><span style=\"font-family:����; font-size:12pt\">ͨѶ��ַ��</span><span style=\"font-family:����; font-size:12pt; text-decoration:underline\">{0}</span><span style=\"width:400pt; font-family:'Lucida Console'; font-size:10pt; text-decoration:underline; display:inline-block; -aw-font-family:'����'; -aw-tabstop-align:left; -aw-tabstop-pos:414pt\"> &#xa0;</span></p>", item.Address);
                //sb.AppendFormat("<p style=\"margin-top:0.35pt; margin-right:9.4pt; margin-bottom:0pt; text-align:justify; line-height:24pt; widows:0; orphans:0\"><span style=\"font-family:����; font-size:12pt\">�������룺</span><span style=\"font-family:����; font-size:12pt; text-decoration:underline\">{0}</span><span style=\"font-family:����; font-size:12pt; text-decoration:underline; -aw-import:spaces\"> &#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;</span><span style=\"font-family:����; font-size:12pt;\"> ��ϵ�绰��</span><span style=\"font-family:����; font-size:12pt; text-decoration:underline\">{1}</span><span style=\"font-family:����; font-size:12pt; text-decoration:underline; -aw-import:spaces\"> &#xa0;</span></p>", item.PostCode, item.Mobile);

                sb.AppendFormat("<p style=\"margin-top:0.35pt; margin-right:9.4pt; margin-bottom:0pt; text-align:justify; line-height:24pt; widows:0; orphans:0\"><span style=\"font-family:����; font-size:12pt\">�������룺</span><span style=\"font-family:����; font-size:12pt; text-decoration:underline\">{0}</span><span style=\"font-family:����; font-size:12pt; text-decoration:underline; -aw-import:spaces\">{1}</span><span style=\"font-family:����; font-size:12pt;\"> ��ϵ�绰��</span><span style=\"font-family:����; font-size:12pt; text-decoration:underline\">{2}</span><span style=\"font-family:����; font-size:12pt; text-decoration:underline; -aw-import:spaces\">{3}</span></p>", "", GetSpaces(""), item.Mobile, GetSpaces(null));
                sb.AppendFormat("<p style=\"margin-top:0.35pt; margin-right:9.4pt; margin-bottom:0pt; text-align:justify; line-height:24pt; widows:0; orphans:0\"><span style=\"font-family:����; font-size:12pt\">�������룺</span><span style=\"font-family:����; font-size:12pt; text-decoration:underline\">{0}</span><span style=\"font-family:����; font-size:12pt; text-decoration:underline; -aw-import:spaces\">{1}</span><span style=\"font-family:����; font-size:12pt;\"> ��ϵ�绰��</span><span style=\"font-family:����; font-size:12pt; text-decoration:underline\">{2}</span><span style=\"font-family:����; font-size:12pt; text-decoration:underline; -aw-import:spaces\">{3}</span></p>", "2", GetSpaces("2"), item.Mobile, GetSpaces(null));
                sb.AppendFormat("<p style=\"margin-top:0.35pt; margin-right:9.4pt; margin-bottom:0pt; text-align:justify; line-height:24pt; widows:0; orphans:0\"><span style=\"font-family:����; font-size:12pt\">�������룺</span><span style=\"font-family:����; font-size:12pt; text-decoration:underline\">{0}</span><span style=\"font-family:����; font-size:12pt; text-decoration:underline; -aw-import:spaces\">{1}</span><span style=\"font-family:����; font-size:12pt;\"> ��ϵ�绰��</span><span style=\"font-family:����; font-size:12pt; text-decoration:underline\">{2}</span><span style=\"font-family:����; font-size:12pt; text-decoration:underline; -aw-import:spaces\">{3}</span></p>", "22", GetSpaces("22"), item.Mobile, GetSpaces(null));
                sb.AppendFormat("<p style=\"margin-top:0.35pt; margin-right:9.4pt; margin-bottom:0pt; text-align:justify; line-height:24pt; widows:0; orphans:0\"><span style=\"font-family:����; font-size:12pt\">�������룺</span><span style=\"font-family:����; font-size:12pt; text-decoration:underline\">{0}</span><span style=\"font-family:����; font-size:12pt; text-decoration:underline; -aw-import:spaces\">{1}</span><span style=\"font-family:����; font-size:12pt;\"> ��ϵ�绰��</span><span style=\"font-family:����; font-size:12pt; text-decoration:underline\">{2}</span><span style=\"font-family:����; font-size:12pt; text-decoration:underline; -aw-import:spaces\">{3}</span></p>", "222", GetSpaces("222"), item.Mobile, GetSpaces(null));
                sb.AppendFormat("<p style=\"margin-top:0.35pt; margin-right:9.4pt; margin-bottom:0pt; text-align:justify; line-height:24pt; widows:0; orphans:0\"><span style=\"font-family:����; font-size:12pt\">�������룺</span><span style=\"font-family:����; font-size:12pt; text-decoration:underline\">{0}</span><span style=\"font-family:����; font-size:12pt; text-decoration:underline; -aw-import:spaces\">{1}</span><span style=\"font-family:����; font-size:12pt;\"> ��ϵ�绰��</span><span style=\"font-family:����; font-size:12pt; text-decoration:underline\">{2}</span><span style=\"font-family:����; font-size:12pt; text-decoration:underline; -aw-import:spaces\">{3}</span></p>", "2222", GetSpaces("2222"), item.Mobile, GetSpaces(null));
                sb.AppendFormat("<p style=\"margin-top:0.35pt; margin-right:9.4pt; margin-bottom:0pt; text-align:justify; line-height:24pt; widows:0; orphans:0\"><span style=\"font-family:����; font-size:12pt\">�������룺</span><span style=\"font-family:����; font-size:12pt; text-decoration:underline\">{0}</span><span style=\"font-family:����; font-size:12pt; text-decoration:underline; -aw-import:spaces\">{1}</span><span style=\"font-family:����; font-size:12pt;\"> ��ϵ�绰��</span><span style=\"font-family:����; font-size:12pt; text-decoration:underline\">{2}</span><span style=\"font-family:����; font-size:12pt; text-decoration:underline; -aw-import:spaces\">{3}</span></p>", "22222", GetSpaces("22222"), item.Mobile, GetSpaces(null));
                sb.AppendFormat("<p style=\"margin-top:0.35pt; margin-right:9.4pt; margin-bottom:0pt; text-align:justify; line-height:24pt; widows:0; orphans:0\"><span style=\"font-family:����; font-size:12pt\">�������룺</span><span style=\"font-family:����; font-size:12pt; text-decoration:underline\">{0}</span><span style=\"font-family:����; font-size:12pt; text-decoration:underline; -aw-import:spaces\">{1}</span><span style=\"font-family:����; font-size:12pt;\"> ��ϵ�绰��</span><span style=\"font-family:����; font-size:12pt; text-decoration:underline\">{2}</span><span style=\"font-family:����; font-size:12pt; text-decoration:underline; -aw-import:spaces\">{3}</span></p>", "222222", GetSpaces("222222"), item.Mobile, GetSpaces(null));
                sb.AppendFormat("<p style=\"margin-top:0.35pt; margin-right:9.4pt; margin-bottom:0pt; text-align:justify; line-height:24pt; widows:0; orphans:0\"><span style=\"font-family:����; font-size:12pt\">�������룺</span><span style=\"font-family:����; font-size:12pt; text-decoration:underline\">{0}</span><span style=\"font-family:����; font-size:12pt; text-decoration:underline; -aw-import:spaces\">{1}</span><span style=\"font-family:����; font-size:12pt;\"> ��ϵ�绰��</span><span style=\"font-family:����; font-size:12pt; text-decoration:underline\">{2}</span><span style=\"font-family:����; font-size:12pt; text-decoration:underline; -aw-import:spaces\">{3}</span></p>", "2222222", GetSpaces("2222222"), item.Mobile, GetSpaces(null));
                sb.AppendFormat("<p style=\"margin-top:0.35pt; margin-right:9.4pt; margin-bottom:0pt; text-align:justify; line-height:24pt; widows:0; orphans:0\"><span style=\"font-family:����; font-size:12pt\">�������룺</span><span style=\"font-family:����; font-size:12pt; text-decoration:underline\">{0}</span><span style=\"font-family:����; font-size:12pt; text-decoration:underline; -aw-import:spaces\">{1}</span><span style=\"font-family:����; font-size:12pt;\"> ��ϵ�绰��</span><span style=\"font-family:����; font-size:12pt; text-decoration:underline\">{2}</span><span style=\"font-family:����; font-size:12pt; text-decoration:underline; -aw-import:spaces\">{3}</span></p>", "22222222", GetSpaces("22222222"), item.Mobile, GetSpaces(null));
                sb.AppendFormat("<p style=\"margin-top:0.35pt; margin-right:9.4pt; margin-bottom:0pt; text-align:justify; line-height:24pt; widows:0; orphans:0\"><span style=\"font-family:����; font-size:12pt\">�������룺</span><span style=\"font-family:����; font-size:12pt; text-decoration:underline\">{0}</span><span style=\"font-family:����; font-size:12pt; text-decoration:underline; -aw-import:spaces\">{1}</span><span style=\"font-family:����; font-size:12pt;\"> ��ϵ�绰��</span><span style=\"font-family:����; font-size:12pt; text-decoration:underline\">{2}</span><span style=\"font-family:����; font-size:12pt; text-decoration:underline; -aw-import:spaces\">{3}</span></p>", "222222222", GetSpaces("222222222"), item.Mobile, GetSpaces(null));
                sb.AppendFormat("<p style=\"margin-top:0.35pt; margin-right:9.4pt; margin-bottom:0pt; text-align:justify; line-height:24pt; widows:0; orphans:0\"><span style=\"font-family:����; font-size:12pt\">�������룺</span><span style=\"font-family:����; font-size:12pt; text-decoration:underline\">{0}</span><span style=\"font-family:����; font-size:12pt; text-decoration:underline; -aw-import:spaces\">{1}</span><span style=\"font-family:����; font-size:12pt;\"> ��ϵ�绰��</span><span style=\"font-family:����; font-size:12pt; text-decoration:underline\">{2}</span><span style=\"font-family:����; font-size:12pt; text-decoration:underline; -aw-import:spaces\">{3}</span></p>", "2222222222", GetSpaces("2222222222"), item.Mobile, GetSpaces(null));
                sb.AppendFormat("<p style=\"margin-top:0.35pt; margin-right:9.4pt; margin-bottom:0pt; text-align:justify; line-height:24pt; widows:0; orphans:0\"><span style=\"font-family:����; font-size:12pt\">�������룺</span><span style=\"font-family:����; font-size:12pt; text-decoration:underline\">{0}</span><span style=\"font-family:����; font-size:12pt; text-decoration:underline; -aw-import:spaces\">{1}</span><span style=\"font-family:����; font-size:12pt;\"> ��ϵ�绰��</span><span style=\"font-family:����; font-size:12pt; text-decoration:underline\">{2}</span><span style=\"font-family:����; font-size:12pt; text-decoration:underline; -aw-import:spaces\">{3}</span></p>", "22222222222", GetSpaces("22222222222"), item.Mobile, GetSpaces(null));
                sb.AppendFormat("<p style=\"margin-top:0.35pt; margin-right:9.4pt; margin-bottom:0pt; text-align:justify; line-height:24pt; widows:0; orphans:0\"><span style=\"font-family:����; font-size:12pt\">�������룺</span><span style=\"font-family:����; font-size:12pt; text-decoration:underline\">{0}</span><span style=\"font-family:����; font-size:12pt; text-decoration:underline; -aw-import:spaces\">{1}</span><span style=\"font-family:����; font-size:12pt;\"> ��ϵ�绰��</span><span style=\"font-family:����; font-size:12pt; text-decoration:underline\">{2}</span><span style=\"font-family:����; font-size:12pt; text-decoration:underline; -aw-import:spaces\">{3}</span></p>", "222222222222", GetSpaces("222222222222"), item.Mobile, GetSpaces(null));
                sb.AppendFormat("<p style=\"margin-top:0.35pt; margin-right:9.4pt; margin-bottom:0pt; text-align:justify; line-height:24pt; widows:0; orphans:0\"><span style=\"font-family:����; font-size:12pt\">�������룺</span><span style=\"font-family:����; font-size:12pt; text-decoration:underline\">{0}</span><span style=\"font-family:����; font-size:12pt; text-decoration:underline; -aw-import:spaces\">{1}</span><span style=\"font-family:����; font-size:12pt;\"> ��ϵ�绰��</span><span style=\"font-family:����; font-size:12pt; text-decoration:underline\">{2}</span><span style=\"font-family:����; font-size:12pt; text-decoration:underline; -aw-import:spaces\">{3}</span></p>", "2222222222222", GetSpaces("2222222222222"), item.Mobile, GetSpaces(null));
                sb.AppendFormat("<p style=\"margin-top:0.35pt; margin-right:9.4pt; margin-bottom:0pt; text-align:justify; line-height:24pt; widows:0; orphans:0\"><span style=\"font-family:����; font-size:12pt\">�������룺</span><span style=\"font-family:����; font-size:12pt; text-decoration:underline\">{0}</span><span style=\"font-family:����; font-size:12pt; text-decoration:underline; -aw-import:spaces\">{1}</span><span style=\"font-family:����; font-size:12pt;\"> ��ϵ�绰��</span><span style=\"font-family:����; font-size:12pt; text-decoration:underline\">{2}</span><span style=\"font-family:����; font-size:12pt; text-decoration:underline; -aw-import:spaces\">{3}</span></p>", "22222222222222", GetSpaces("22222222222222"), item.Mobile, GetSpaces(null));
                sb.AppendFormat("<p style=\"margin-top:0.35pt; margin-right:9.4pt; margin-bottom:0pt; text-align:justify; line-height:24pt; widows:0; orphans:0\"><span style=\"font-family:����; font-size:12pt\">�������룺</span><span style=\"font-family:����; font-size:12pt; text-decoration:underline\">{0}</span><span style=\"font-family:����; font-size:12pt; text-decoration:underline; -aw-import:spaces\">{1}</span><span style=\"font-family:����; font-size:12pt;\"> ��ϵ�绰��</span><span style=\"font-family:����; font-size:12pt; text-decoration:underline\">{2}</span><span style=\"font-family:����; font-size:12pt; text-decoration:underline; -aw-import:spaces\">{3}</span></p>", "222222222222222", GetSpaces("222222222222222"), item.Mobile, GetSpaces(null));
                sb.AppendFormat("<p style=\"margin-top:0.35pt; margin-right:9.4pt; margin-bottom:0pt; text-align:justify; line-height:24pt; widows:0; orphans:0\"><span style=\"font-family:����; font-size:12pt\">�������룺</span><span style=\"font-family:����; font-size:12pt; text-decoration:underline\">{0}</span><span style=\"font-family:����; font-size:12pt; text-decoration:underline; -aw-import:spaces\">{1}</span><span style=\"font-family:����; font-size:12pt;\"> ��ϵ�绰��</span><span style=\"font-family:����; font-size:12pt; text-decoration:underline\">{2}</span><span style=\"font-family:����; font-size:12pt; text-decoration:underline; -aw-import:spaces\">{3}</span></p>", "2222222222222222", GetSpaces("2222222222222222"), item.Mobile, GetSpaces(null));
                sb.AppendFormat("<p style=\"margin-top:0.35pt; margin-right:9.4pt; margin-bottom:0pt; text-align:justify; line-height:24pt; widows:0; orphans:0\"><span style=\"font-family:����; font-size:12pt\">�������룺</span><span style=\"font-family:����; font-size:12pt; text-decoration:underline\">{0}</span><span style=\"font-family:����; font-size:12pt; text-decoration:underline; -aw-import:spaces\">{1}</span><span style=\"font-family:����; font-size:12pt;\"> ��ϵ�绰��</span><span style=\"font-family:����; font-size:12pt; text-decoration:underline\">{2}</span><span style=\"font-family:����; font-size:12pt; text-decoration:underline; -aw-import:spaces\">{3}</span></p>", "22222222222222222", GetSpaces("22222222222222222"), item.Mobile, GetSpaces(null));

                sb.AppendFormat("<p style=\"margin-top:0.35pt; margin-right:9.4pt; margin-bottom:0pt; text-align:justify; line-height:24pt; widows:0; orphans:0\"><span style=\"font-family:����; font-size:12pt\">�������ڣ�</span><span style=\"font-family:����; font-size:12pt; text-decoration:underline\">{0}</span><span style=\"font-family:����; font-size:12pt; text-decoration:underline; -aw-import:spaces\">{1}</span><span style=\"font-family:����; font-size:12pt;\"> ��    ��</span><span style=\"font-family:����; font-size:12pt; text-decoration:underline\">{2}</span><span style=\"font-family:����; font-size:12pt; text-decoration:underline; -aw-import:spaces\">{3}</span></p>", "1992��12��02��", GetSpaces("1992��12��02��"), "��", GetSpaces(null));

                sb.Append("<p style=\"margin-top:0.35pt; margin-right:9.4pt; margin-bottom:0pt; text-align:justify; line-height:24pt; widows:0; orphans:0\"><span style=\"font-family:����; font-size:12pt\"> </span><span style=\"width:400pt; font-family:'Lucida Console'; font-size:10pt; display:inline-block; -aw-font-family:'����'; -aw-tabstop-align:left; -aw-tabstop-pos:414pt\"> &#xa0;</span></p>");

            }
            builder.InsertHtml(sb.ToString(), true);

            //doc.Range.Bookmarks["BuyersTable"].Text = "";    // �����ʾ

            doc.Save("AUTOmsr.docx", SaveFormat.Docx);
        }


        public string GetSpaces(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return " &#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;";//11
            //int length = System.Text.Encoding.Default.GetBytes(value).Length;

            //������^[+-]?\d*[.]?\d*$
            //ֻ�������ֺ�Ӣ����ĸ ^[A-Za-z0-9]+$
            //��Ӣ��^[A-Za-z]+$
            if (Regex.IsMatch(value, @"^[A-Za-z0-9]+$"))
            {
                int length = value.Length;
                switch (length)
                {
                    case 0:
                        return " &#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;";//11
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                        return " &#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;";
                    case 6:
                        return " &#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;";
                    case 7:
                    case 8:
                        return " &#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;";
                    case 9:
                    case 10:
                        return " &#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;";
                    case 11:
                        return " &#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;";
                    default:
                        break;
                }
            }
            else {
                string other = Regex.Replace(value, @"[\u4e00-\u9fa5]", ""); //ȥ������

                string chineseCharacters = Regex.Replace(value, @"[^\u4e00-\u9fa5]", ""); //ֻ������

                int length = other.Length + chineseCharacters.Length * 2;

                switch (length)
                {
                    case 0:
                        return " &#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;";//11
                    case 1:
                        return " &#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;";
                    case 2:
                        return " &#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;";
                    case 3:
                        return " &#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;";
                    case 4:      
                        return " &#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;";
                    case 5:      
                        return " &#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;";
                    case 6:      
                        return " &#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;";
                    case 7:      
                        return " &#xa0;&#xa0;&#xa0;&#xa0;&#xa0;";
                    case 8:      
                        return " &#xa0;&#xa0;&#xa0;&#xa0;";
                    case 9:      
                        return " &#xa0;&#xa0;&#xa0;";
                    case 10:     
                        return " &#xa0;&#xa0;";
                    case 11:
                        return " &#xa0;&#xa0;&#xa0;&#xa0;&#xa0;&#xa0;";
                    case 12:
                        return " &#xa0;&#xa0;&#xa0;&#xa0;&#xa0;";
                    case 14:
                        return " &#xa0;&#xa0;&#xa0;";
                    default:
                        break;
                }
            }
            return "";
        }


        [TestMethod]
        public void CreateHtmlTest()
        {
            string tmppath = "E:\\demo\\20200513133519164.docx";
            Document doc = new Document(tmppath); //����ģ��
            DocumentBuilder builder = new DocumentBuilder(doc);
            doc.Save("E:\\demo\\index4.html", SaveFormat.Html);
        }

        /// <summary>
        /// ���ݽǶ���תͼ��
        /// </summary>
        /// <param name="img"></param>
        public static Image RotateImg(string imgPath, float angle)
        {
            using (Image img = Image.FromFile(imgPath))
            {
                //img.RotateFlip(RotateFlipType.RotateNoneFlipX)
                //ͨ��PngͼƬ����ͼƬ͸�����޸���תͼƬ������⡣
                int width = img.Width;
                int height = img.Height;
                //�Ƕ�
                Matrix mtrx = new Matrix();
                mtrx.RotateAt(angle, new PointF((width / 2), (height / 2)), MatrixOrder.Append);
                //�õ���ת��ľ���
                GraphicsPath path = new GraphicsPath();
                path.AddRectangle(new RectangleF(0f, 0f, width, height));
                RectangleF rct = path.GetBounds(mtrx);
                //����Ŀ��λͼ
                Bitmap devImage = new Bitmap((int)(rct.Width), (int)(rct.Height));
                Graphics g = Graphics.FromImage(devImage);
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Bilinear;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                //����ƫ����
                Point Offset = new Point((int)(rct.Width - width) / 2, (int)(rct.Height - height) / 2);
                //����ͼ����ʾ������ͼ��������봰�ڵ����ĵ�һ��
                Rectangle rect = new Rectangle(Offset.X, Offset.Y, (int)width, (int)height);
                Point center = new Point((int)(rect.X + rect.Width / 2), (int)(rect.Y + rect.Height / 2));
                g.TranslateTransform(center.X, center.Y);
                g.RotateTransform(angle);
                //�ָ�ͼ����ˮƽ�ʹ�ֱ�����ƽ��
                g.TranslateTransform(-center.X, -center.Y);
                g.DrawImage(img, rect);
                //������ͼ�����б任
                g.ResetTransform();
                g.Save();
                g.Dispose();
                path.Dispose();
                return devImage;
            }
        }

        private DataTable GetData()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("����", System.Type.GetType("System.String"));
            dt.Columns.Add("���", System.Type.GetType("System.String"));
            dt.Rows.Add(new object[] { "�ϻ���ֵ", "1" });
            dt.Rows.Add(new object[] { "��ˮ�ɷ�", "2" });
            return dt;
        }

        [TestMethod]
        public void Auto()
        {
            var dir = Directory.GetCurrentDirectory();
            string tmppath = dir + "\\auto.docx";
            Document doc = new Document(tmppath); //����ģ��
            DocumentBuilder builder = new DocumentBuilder(doc);

            //DataTable products = this.GetData(); //����Դ
            //int count = 0;
            ////��¼Ҫ��ʾ������
            //for (var i = 0; i < products.Columns.Count; i++)
            //{
            //    if (doc.Range.Bookmarks[products.Columns[i].ColumnName.Trim()] != null)
            //    {
            //        Bookmark mark = doc.Range.Bookmarks[products.Columns[i].ColumnName.Trim()];
            //        mark.Text = "";
            //        count++;
            //    }
            //}
            //System.Collections.Generic.List<string> listcolumn = new System.Collections.Generic.List<string>(count);
            //for (var i = 0; i < count; i++)
            //{
            //    builder.MoveToCell(0, 0, i, 0); //�ƶ���Ԫ��
            //    if (builder.CurrentNode.NodeType == NodeType.BookmarkStart)
            //    {
            //        listcolumn.Add((builder.CurrentNode as BookmarkStart).Name);
            //    }
            //}
            //double width = builder.CellFormat.Width;//��ȡ��Ԫ����
            //builder.MoveToBookmark("table");        //��ʼ���ֵ
            //for (var m = 0; m < products.Rows.Count; m++)
            //{
            //    for (var i = 0; i < listcolumn.Count; i++)
            //    {
            //        builder.InsertCell();            // ���һ����Ԫ��
            //        builder.CellFormat.Borders.LineStyle = LineStyle.Single;
            //        builder.CellFormat.Borders.Color = System.Drawing.Color.Black;
            //        builder.CellFormat.Width = width;
            //        builder.CellFormat.VerticalMerge = Aspose.Words.Tables.CellMerge.None;
            //        builder.Write(products.Rows[m][listcolumn[i]].ToString());
            //    }
            //    builder.EndRow();
            //}
            //doc.Range.Bookmarks["table"].Text = "";    // �����ʾ
            //doc.Save("baojiadan.doc", SaveFormat.Doc, SaveType.OpenInWord, page.Response);

            List<Student> list = new List<Student> { new Student { Id = 1, Name = "����" }, new Student { Id = 2, Name = "����" }, new Student { Id = 3, Name = "����111" } };
            //double width = builder.CellFormat.Width;//��ȡ��Ԫ����
            builder.CellFormat.PreferredWidth = PreferredWidth.Auto;
            builder.CellFormat.Borders.LineStyle = LineStyle.Single;
            builder.CellFormat.Borders.Color = System.Drawing.Color.Black;
            //builder.CellFormat.Width = width;
            //builder.CellFormat.VerticalMerge = Aspose.Words.Tables.CellMerge.None;
            builder.MoveToBookmark("table");        //��ʼ���ֵ
            var pis = typeof(Student).GetProperties();
            foreach (PropertyInfo property in pis)
            {
                object obj = property.GetCustomAttribute(typeof(System.ComponentModel.DescriptionAttribute), false);
                if (obj != null)
                {
                    System.ComponentModel.DescriptionAttribute temp = (System.ComponentModel.DescriptionAttribute)obj;
                    builder.InsertCell();// ���һ����Ԫ��
                    builder.Write(temp.Description);
                }
            }
            builder.EndRow();
            foreach (var item in list)
            {
                var properties = item.GetType().GetProperties();
                foreach (PropertyInfo property in properties)
                {
                    object desc = property.GetCustomAttribute(typeof(System.ComponentModel.DescriptionAttribute), false);
                    if (desc != null)
                    {
                        object obj = property.GetValue(item, null);
                        builder.InsertCell();// ���һ����Ԫ��
                        builder.Write(obj.ToString());
                    }
                }
                builder.EndRow();
            }
            doc.Range.Bookmarks["table"].Text = "";    // �����ʾ
            doc.Save("baojiadan.doc", SaveFormat.Doc);
        }

        //[TestMethod]
        //public void GenerateHouseContractFile()
        //{
        //    using (CurrentUnitOfWork.DisableFilter(AbpDataFilters.MayHaveTenant))
        //    {
        //        /*
        //         * ������Ʒ��������ͬģ��+�õز�����ģ��+�ô�ӡ��¼ʵ�ʺ�ͬ���ݣ��ϳ�pdf��ͬ�ļ�
        //         */
        //        List<NameValue> customzied = new List<NameValue>();

        //        string localFile = GeneratePrintFile();
        //        //����pdf�����ͼƬ����
        //        //��ȡ������˾��ͬ�ºͷ���ӡ��
        //        List<ImageVar> pdfImages = new List<ImageVar>();

        //        pdfImages.Add(new ImageVar("$HTBAZ$", string.IsNullOrWhiteSpace(company.ContractSeal) ? string.Empty : company.ContractSeal, 120, 120));
        //        pdfImages.Add(new ImageVar("$DCFRZ$", string.IsNullOrWhiteSpace(company.CorporateSeal) ? string.Empty : company.CorporateSeal, 60, 60) { VerticalAlignment = VerticalAlignment.None, Top = 30 });
        //        //����ͼ
        //        pdfImages.Add(new ImageVar("$HXTP$", string.IsNullOrWhiteSpace(planeFigureImage) ? string.Empty : planeFigureImage, 510, 620) { ImageHandler = null, WrapType = WrapType.Inline });
        //        //List<ContractSignDto> signs = JsonConvert.DeserializeObject<List<ContractSignDto>>(record.Signs);

        //        ////signs.ForEach(p =>
        //        ////{
        //        ////    pdfImages.Add(new ImageVar(p.Field, p.Combine, 60, 60) { VerticalAlignment = VerticalAlignment.None, Top = 20 });
        //        ////});

        //        ////ǩ�¸���
        //        //var seals = signs.Select(p => p.Seal).Count();
        //        //if (signs.Any())
        //        //{
        //        //    foreach (var p in signs)
        //        //    {
        //        //        if (p.Field.Equals("$FRS$"))
        //        //        {
        //        //            pdfImages.Add(new ImageVar(p.Field, p.Combine, 60, 60) { VerticalAlignment = VerticalAlignment.None, Top = 20 });
        //        //            pdfImages.Add(new ImageVar("$FRS4$", p.Seal, 120, 120) { VerticalAlignment = VerticalAlignment.None, Top = 20 });
        //        //        }
        //        //        else
        //        //        {
        //        //            string temp = p.Field;
        //        //            pdfImages.Add(new ImageVar(temp, p.Combine, 60, 60) { VerticalAlignment = VerticalAlignment.None, Top = 20 });
        //        //            string num = System.Text.RegularExpressions.Regex.Replace(temp, @"[^0-9]+", "");
        //        //            string newField = System.Text.RegularExpressions.Regex.Replace(temp, @"\d", (Convert.ToInt32(num) + 7).ToString());
        //        //            int left = seals * 2;
        //        //            pdfImages.Add(new ImageVar(newField, p.Seal, 120, 120) { Left = left, RelativeHorizontalPosition = RelativeHorizontalPosition.Page, RelativeVerticalPosition = RelativeVerticalPosition.Line, WrapType = WrapType.Square });
        //        //        }
        //        //    }
        //        //}

        //        HouseContractWordHandler.Instance.Process("E:\\demo\\template.docx", localFile, customzied.Where(p => !string.IsNullOrWhiteSpace(p.Value)).ToList(), pdfImages, SetDefaultValueForEmptyVar);

        //        //��localFileתΪpdf��ʽ
        //        //Document doc = new Document(localFile);
        //        //string pdfFolder = HttpContext.Current.Server.MapPath($"~{HouseManageConsts.ContractFileFolder}");
        //        //if (!Directory.Exists(pdfFolder))
        //        //{
        //        //    Directory.CreateDirectory(pdfFolder);
        //        //}
        //        //string pdfFile = Path.Combine(pdfFolder, $@"{input.Id}\{Guid.NewGuid().ToString().Replace("-", string.Empty)}.pdf");
        //        //doc.Save(pdfFile, SaveFormat.Pdf);
        //        //File.Delete(localFile);

        //    }
        //}

        [TestMethod]
        public void GenerateHouseContractFileTest()
        {
            using (FileStream fs = new FileStream("E:\\demo\\aspose.docx", FileMode.Create))
            {
                List<NameValue> vars = new List<NameValue>();
                vars.Add(new NameValue("$HTBH$", "00001"));
                vars.Add(new NameValue("$CMRMC$", "���������Ϣ�Ƽ����޹�˾"));
                vars.Add(new NameValue("$MSRXM1$", "����"));
                vars.Add(new NameValue("$MSRXM2$", "����"));
                vars.Add(new NameValue("$FKFS$", "1"));

                List<ImageVar> images = new List<ImageVar>();
                //images.Add(new ImageVar("$CONTRACTBUSSEAL$", "E:\\��ͬҵ����.png", 0, 0));
                //images.Add(new ImageVar("$DCFRZ$", "E:\\demo\\dcfr.png", 100, 100) { VerticalAlignment = VerticalAlignment.None, Top = 30 });
                //images.Add(new ImageVar("$FRS$", "E:\\demo\\combine.png", 40, 40) { Top = 30 });
                //images.Add(new ImageVar("$HTBAZ$", "E:\\demo\\htz.png", 100, 100) { VerticalAlignment = VerticalAlignment.None });
                //images.Add(new ImageVar("$OPERATORSEAL$", "E:\\demo\\jbr.png", 60, 60) { Left = 150, Top = 100 });
                //images.Add(new ImageVar("$RECORDSEAL$", "E:\\demo\\baz.png", 120, 120) { HorizontalAlignment = HorizontalAlignment.Right, Top = 100 });
                images.Add(new ImageVar("$BPS1$", "E:\\demo\\combine.png", 60, 60) { VerticalAlignment = VerticalAlignment.None, Top = 20 });
                images.Add(new ImageVar("$BPS2$", "E:\\demo\\combine.png", 60, 60) { VerticalAlignment = VerticalAlignment.None, Top = 20 });
                images.Add(new ImageVar("$BPS3$", "E:\\demo\\combine.png", 60, 60) { VerticalAlignment = VerticalAlignment.None, Top = 20 });
                images.Add(new ImageVar("$BPS4$", "E:\\demo\\combine.png", 60, 60) { VerticalAlignment = VerticalAlignment.None, Top = 20 });
                images.Add(new ImageVar("$BPS5$", "E:\\demo\\combine.png", 60, 60) { VerticalAlignment = VerticalAlignment.None, Top = 20 });
                images.Add(new ImageVar("$BPS6$", "E:\\demo\\combine.png", 60, 60) { VerticalAlignment = VerticalAlignment.None, Top = 20 });
                images.Add(new ImageVar("$BPS7$", "E:\\demo\\combine.png", 60, 60) { VerticalAlignment = VerticalAlignment.None, Top = 20 });

                images.Add(new ImageVar("$BPS8$", "E:\\demo\\qz.png", 120, 120) { VerticalAlignment = VerticalAlignment.None, Top = 20 });
                images.Add(new ImageVar("$BPS9$", "E:\\demo\\qz.png", 120, 120) { VerticalAlignment = VerticalAlignment.None, Top = 20 });
                images.Add(new ImageVar("$BPS10$", "E:\\demo\\qz.png", 120, 120) { VerticalAlignment = VerticalAlignment.None, Top = 20 });
                images.Add(new ImageVar("$BPS11$", "E:\\demo\\qz.png", 120, 120) { VerticalAlignment = VerticalAlignment.None, Top = 20 });
                images.Add(new ImageVar("$BPS12$", "E:\\demo\\qz.png", 120, 120) { VerticalAlignment = VerticalAlignment.None, Top = 20 });
                images.Add(new ImageVar("$BPS13$", "E:\\demo\\qz.png", 120, 120) { VerticalAlignment = VerticalAlignment.None, Top = 20 });
                images.Add(new ImageVar("$BPS14$", "E:\\demo\\qz.png", 120, 120) { VerticalAlignment = VerticalAlignment.None, Top = 20 });

                //����
                images.Add(new ImageVar("$BuildEngineerPlanGL0$", "E:\\demo\\BlankSeal.png", 120, 80) { ImageHandler = null, WrapType = WrapType.Inline });

                images.Add(new ImageVar("$AnnexesTwoImage0$", "E:\\demo\\BlankSeal.png", 120, 80) { ImageHandler = null, WrapType = WrapType.Inline, Angle = -45d });

                string templateFile = "E:\\demo\\2020��Ʒ����ͬģ��(סլ).docx";
                string localFile = "E:\\demo\\test.docx";
                HouseContractWordHandler.Instance.NewProcess(
                    templateFile,
                    localFile,
                    vars,
                    images,
                    SetDefaultValueForEmptyVar,
                    null);
                //��localFileתΪpdf��ʽ
                Document doc = new Document(localFile);
            }

            //Document document = new Document("E:\\��Ʒ����ͬģ��.docx");
            //var findOption = new FindReplaceOptions();
            //findOption.FindWholeWordsOnly = true;
            //findOption.MatchCase = true;
            //findOption.ReplacingCallback = new ReplaceHandler();
            //document.Range.Replace(new Regex(@"\$HTBH\$"), "00001", findOption);
            //document.Range.Replace(new Regex(@"\$CONTRACTBUSSEAL\$"), "00002", findOption);
            //DocumentBuilder builder = new DocumentBuilder(document);
            //builder.MoveToDocumentEnd();
            //builder.Writeln("hello world");
            //builder.Writeln("hello world");
            //var image = builder.InsertImage("E:\\demo\\2.bmp", RelativeHorizontalPosition.Page, 0, RelativeVerticalPosition.Page, 0, 100, 100, WrapType.None);
            //image.HorizontalAlignment = HorizontalAlignment.Right;
            //image.Top = 200;
            //var image2 = builder.InsertImage("E:\\demo\\1.png", RelativeHorizontalPosition.Page, 0, RelativeVerticalPosition.TopMargin, 0, 100, 100, WrapType.None);
            //image2.HorizontalAlignment = HorizontalAlignment.Right;
            //Console.WriteLine(document.PageCount);
            //document.Save("E:\\demo\\aspose.docx");
        }

        private string SetDefaultValueForEmptyVar(string varname)
        {
            List<string> skipVars = new List<string>
            {
                "$PC$","$PC$"
            };
            var skipVar = skipVars.FirstOrDefault(p => p == varname);
            if (skipVar != null)
            {
                return skipVar;
            }
            else
            {
                List<string> emptyVars = new List<string>
                {
                    "$HXTP$",
                    "$BuildEngineerPlanGL0$", "$BuildEngineerPlanGLRemark0$",
                    "$BuildEngineerPlanGL1$", "$BuildEngineerPlanGLRemark1$",
                    "$BuildEngineerPlanGL2$", "$BuildEngineerPlanGLRemark2$",
                    "$AnnexesTwoExplain$",
                    "$AnnexesTwoImage0$", "$AnnexesTwoImageRemark0$",
                    "$AnnexesTwoImage1$", "$AnnexesTwoImageRemark1$",
                    "$AnnexesTwoImage2$", "$AnnexesTwoImageRemark2$",
                    "$AnnexesThreeExplain$",
                    "$AnnexesThreeImage0$", "$AnnexesThreeImageRemark0$",
                    "$AnnexesThreeImage1$", "$AnnexesThreeImageRemark1$",
                    "$AnnexesThreeImage2$", "$AnnexesThreeImageRemark2$",
                    "$AnnexesFourExplain$",
                    "$AnnexesFourImage0$",
                    "$AnnexesFiveExplain$",
                    "$AnnexesFiveImage0$",
                    "$AnnexesEightExplain$",
                    "$AnnexesEightImage0$", "$AnnexesEightImageRemark0$",
                    "$AnnexesEightImage1$", "$AnnexesEightImageRemark1$",
                    "$AnnexesEightImage2$", "$AnnexesEightImageRemark2$",
                    "$AnnexesNineExplain$",
                    "$AnnexesNineImage0$",
                    "$AnnexesTenExplain$",
                    "$AnnexesTenImage0$",
                    "$AnnexesElevenExplain$",
                    "$AnnexesElevenImage0$",
                    "$CONTRACTBUSSEAL$", "$HTBAZ$",
                    "$BPS1$", "$BPS2$", "$BPS3$", "$BPS4$", "$BPS5$", "$BPS6$", "$BPS7$",
                    "$DCFRZ$", "$FRS$", "$FRS4$",
                    "$BRSS1$", "$BRSS2$", "$BRSS3$", "$BRSS4$", "$BRSS5$", "$BRSS6$", "$BRSS7$",
                    "$DLS1$", "$DLS2$", "$DLS3$", "$DLS4$", "$DLS5$", "$DLS6$", "$DLS7$",
                    "$HXTP$", "$HTBAZ$",
                    "$BCS1$", "$BCS2$", "$BCS3$", "$BCS4$", "$BCS5$", "$BCS6$", "$BCS7$",
                    "$BEIANHAO$", "$OPERATORSEAL$", "$RECORDSEAL$",
                    "$BEIANNIAN$", "$BEIANYUE$", "$BEIANRI$"
                };

                return emptyVars.Any(p => p == varname) ? string.Empty : "x";
            }
        }

        [TestMethod]
        public void TestMethod8()
        {
            //AppDomain.CurrentDomain.SetupInformation.ApplicationBase
            var dir = Directory.GetCurrentDirectory();
            var ImageDir = dir + "\\";
            var ArtifactsDir = ImageDir;
            Document doc = new Document();
            DocumentBuilder builder = new DocumentBuilder(doc);

            // By default, the image is inline
            //Shape shape = builder.InsertImage(ImageDir + "Logo.png");

            // Make the image float, put it behind text and center on the page
            //shape.WrapType = WrapType.None;
            //shape.BehindText = true;
            //shape.RelativeHorizontalPosition = RelativeHorizontalPosition.Page;
            //shape.RelativeVerticalPosition = RelativeVerticalPosition.Page;
            //shape.HorizontalAlignment = HorizontalAlignment.Center;
            //shape.VerticalAlignment = VerticalAlignment.Center;

            builder.InsertImage(ImageDir + "Logo.png", RelativeHorizontalPosition.Page, 0, RelativeVerticalPosition.Line, 0, 120, 120, WrapType.Square);
            builder.InsertImage(ImageDir + "Logo.png", RelativeHorizontalPosition.Page, 50, RelativeVerticalPosition.Line, 0, 120, 120, WrapType.Square);

            builder.InsertImage(ImageDir + "Logo.png", RelativeHorizontalPosition.Page, 0, RelativeVerticalPosition.Line, 200, 120, 120, WrapType.Square);
            builder.InsertImage(ImageDir + "Logo.png", RelativeHorizontalPosition.Page, 50, RelativeVerticalPosition.Line, 200, 120, 120, WrapType.Square);

            Shape shape = builder.InsertImage(ImageDir + "BlankSeal.png", RelativeHorizontalPosition.Page, 50, RelativeVerticalPosition.Line, 400, 120, 120, WrapType.Square);
            shape.Rotation = -45d;

            doc.Save(ArtifactsDir + "Image.CreateFloatingPageCenter.docx");
        }

        [TestMethod]
        public void TestMethod7()
        {
            var temp = DateTime.Now.ToString("yyyyMMdd") + (11 + 1).ToString("0000");

            var temp2 = DateTime.Now.ToString("yyyyMMdd") + (11 + 1).ToString("0000");
            var aaa = string.Empty.Equals("123");

            decimal a = 10.378m;
            decimal b = 80.3m;
            var c = Math.Round(a / b * 100, 2).ToString("0.00");
        }

        [TestMethod]
        public void TestMethod6()
        {
            string IDCardNo = "53262119860125293X";// "370986890623212";// "530423199212020638";
            var res1 = PackIden.GetBirthdayByIdentityCardId(IDCardNo, PackIden.BirthdayFormat.ChineseDate);
            var res2 = PackIden.GetCardIdInfo(IDCardNo);
        }

        public class PackIden
        {
            /// <summary>
            /// �������֤��ȡ����
            /// </summary>
            /// <param name="cardid">���֤</param>
            /// <param name="res">��ʽ����</param>
            /// <returns></returns>
            public static string GetBirthdayByIdentityCardId(string cardid, BirthdayFormat birthdayFormat)
            {
                string birthday = string.Empty;
                System.Text.RegularExpressions.Regex regex = null;

                if (cardid.Length == 18)
                {
                    regex = new Regex(@"^(^\d{18}$|^\d{17}(\d|X|x))$");
                    if (regex.IsMatch(cardid))
                    {
                        if (birthdayFormat == BirthdayFormat.ChineseDate)
                        {
                            birthday = cardid.Substring(6, 8).Insert(4, "��").Insert(7, "��").Insert(10, "��");
                        }
                        else if (birthdayFormat == BirthdayFormat.Dot)
                        {
                            birthday = cardid.Substring(6, 8).Insert(4, ".").Insert(7, ".");
                        }
                        else if (birthdayFormat == BirthdayFormat.HorizontalLine)
                        {
                            birthday = cardid.Substring(6, 8).Insert(4, "-").Insert(7, "-");
                        }
                        else if (birthdayFormat == BirthdayFormat.ObliqueLine)
                        {
                            birthday = cardid.Substring(6, 8).Insert(4, "/").Insert(7, "/");
                        }
                        else
                            birthday = cardid.Substring(6, 8);
                    }
                    else
                    {
                        birthday = "invalid cardid";
                    }
                }
                else if (cardid.Length == 15)
                {
                    regex = new Regex(@"^\d{15}");
                    if (regex.IsMatch(cardid))
                    {
                        if (birthdayFormat == BirthdayFormat.ChineseDate)
                        {
                            birthday = cardid.Substring(6, 6).Insert(2, "��").Insert(5, "��").Insert(8, "��");
                        }
                        else if (birthdayFormat == BirthdayFormat.Dot)
                        {
                            birthday = cardid.Substring(6, 6).Insert(2, ".").Insert(5, ".");
                        }
                        else if (birthdayFormat == BirthdayFormat.HorizontalLine)
                        {
                            birthday = cardid.Substring(6, 6).Insert(2, "-").Insert(5, "-");
                        }
                        else if (birthdayFormat == BirthdayFormat.ObliqueLine)
                        {
                            birthday = cardid.Substring(6, 6).Insert(2, "/").Insert(5, "/");
                        }
                        else
                            birthday = cardid.Substring(6, 6);
                    }
                    else
                    {
                        birthday = "invalid cardid";
                    }
                }
                else
                {
                    birthday = "invalid cardid";
                }

                return birthday;
            }

            public enum BirthdayFormat
            {
                /// <summary>
                /// �޷��� 19900101
                /// </summary>
                None,

                /// <summary>
                /// ���� 1990-01-01
                /// </summary>
                HorizontalLine,

                /// <summary>
                /// б�� 1990/01/01
                /// </summary>
                ObliqueLine,

                /// <summary>
                /// �� 1990.01.01
                /// </summary>
                Dot,

                /// <summary>
                /// �й�ʱ�� 1990��01��01��
                /// </summary>
                ChineseDate
            }

            /// <summary>
            /// �������֤��ȡ���֤��Ϣ
            /// 18λ���֤
            /// 0��������(1~6λ,����1��2λ��Ϊ��ʡ�������Ĵ��룬3��4λ��Ϊ�ء��м������Ĵ��룬5��6λ��Ϊ�ء�������������)
            /// 1����������(7~14λ)
            /// 2˳���(15~17λ����Ϊ���Է����룬˫��ΪŮ�Է�����)
            /// 3�Ա�
            ///
            /// 15λ���֤
            /// 0��������
            /// 1�������(7~8λ��,9~10λΪ�����·ݣ�11~12λΪ��������
            /// 2˳���(13~15λ)�����ܹ��ж��Ա�����Ϊ�У�ż��ΪŮ
            /// 3�Ա�
            /// </summary>
            /// <param name="cardId"></param>
            /// <returns></returns>
            public static string[] GetCardIdInfo(string cardId)
            {
                string[] info = new string[4];
                System.Text.RegularExpressions.Regex regex = null;
                if (cardId.Length == 18)
                {
                    regex = new Regex(@"^(^\d{18}$|^\d{17}(\d|X|x))$");
                    if (regex.IsMatch(cardId))
                    {
                        info.SetValue(cardId.Substring(0, 6), 0);
                        info.SetValue(cardId.Substring(6, 8), 1);
                        info.SetValue(cardId.Substring(14, 3), 2);
                        info.SetValue(Convert.ToInt32(info[2]) % 2 != 0 ? "��" : "Ů", 3);
                    }
                }
                else if (cardId.Length == 15)
                {
                    regex = new Regex(@"^\d{15}");
                    if (regex.IsMatch(cardId))
                    {
                        info.SetValue(cardId.Substring(0, 6), 0);
                        info.SetValue(cardId.Substring(6, 6), 1);
                        info.SetValue(cardId.Substring(12, 3), 2);
                        info.SetValue(Convert.ToInt32(info[2]) % 2 != 0 ? "��" : "Ů", 3);
                    }
                }
                return info;
            }
        }

        [TestMethod]
        public void TestMethod5()
        {
            var customInfo = new Product();
            var pis = customInfo.GetType().GetProperties();
            foreach (var item in pis)
            {
                var type = item.PropertyType.Name;
                var IsGenericType = item.PropertyType.IsGenericType;
                var list = item.PropertyType.GetInterface("IEnumerable", false);
                Console.WriteLine($"�������ƣ�{item.Name}�����ͣ�{type}��ֵ��{item.GetValue(customInfo)}");
                if (IsGenericType && list != null)
                {
                    var listVal = item.GetValue(customInfo) as IEnumerable<object>;
                    if (listVal == null) continue;
                    foreach (var aa in listVal)
                    {
                        var dtype = aa.GetType();
                        foreach (var bb in dtype.GetProperties())
                        {
                            var dtlName = bb.Name.ToLower();
                            var dtlType = bb.PropertyType.Name;
                            var oldValue = bb.GetValue(aa);
                            Console.WriteLine($"�Ӽ��������ƣ�{dtlName}�����ͣ�{dtlType}��ֵ��{oldValue}");
                        }
                    }
                }
            }
        }

        public class Product
        {
            public string Id { get; set; }
            public string Name { get; set; }

            public List<ProductDetail> Detail { get; set; } = new List<ProductDetail>
                {
                    new ProductDetail{Id="111" ,DtlId="1",Number=12.3568M,Price=5.689M,Amount=70.2978352M},
                    new ProductDetail{Id="222",DtlId="2",Number=12.35M,Price=5.689M,Amount=70.2978352M},
                    new ProductDetail{Id="333",DtlId="3",Number=12.358M,Price=5.689M,Amount=70.304662M},
                };

            public List<ProductComment> Comment { get; set; } = new List<ProductComment>();
        }

        public class ProductDetail
        {
            public string DtlId { get; set; }
            public string Id { get; set; }
            public decimal Number { get; set; }
            public decimal Price { get; set; }
            public decimal Amount { get; set; }
        }

        public class ProductComment
        {
            public string DtlId { get; set; }
            public string Id { get; set; }
            public string Comment { get; set; }
        }

        [TestMethod]
        public void TestMethod4()
        {
            int? a = 1;
            int? b = null;
            int aa = 1;
            int bb = 2;
            var result1 = a + b;
            var result2 = a + bb;
            var result3 = aa + b;
            var result4 = aa + bb;

            var allLayer = a ?? 0 + b ?? 0;

            Console.WriteLine(a + b);
            Console.WriteLine(aa + bb);
            Console.WriteLine(aa + b);
            Console.WriteLine(a + bb);
        }

        [TestMethod]
        public void TestMethod3()
        {
            string value = null;
            string temp = value.Equals("true", StringComparison.OrdinalIgnoreCase) ? "��" : value.Equals("true", StringComparison.OrdinalIgnoreCase) ? "��" : value;
            Console.WriteLine(temp);
        }

        //
        [TestMethod]
        public void TestReflex()
        {
            Reflex reflex = new Reflex();
            reflex.BZ = "haha";
            reflex.Students.Add(new Student { Id = 1, Name = "s1" });
            reflex.Students.Add(new Student { Id = 2, Name = "s2" });
            reflex.Students.Add(new Student { Id = 3, Name = "s3" });

            //��ȡ�����������ƺ���������
            PropertyInfo[] infos = reflex.GetType().GetProperties();// typeof(T).GetProperties();
            foreach (PropertyInfo item in infos)
            {
                Console.WriteLine(string.Format("PropertyName:{0},type:{1}", item.Name, item.PropertyType.Name));
                object obj = item.GetValue(item.Name, null);
            }

            //for (int i = 0; i < list.Count; i++)
            //{
            //    Console.WriteLine(list[i].GetType().GetProperty("Username").GetValue(list[i], null));
            //    Console.WriteLine(list[i].GetType().GetProperty("Password").GetValue(list[i], null));
            //    object obj = list[i].GetType().GetProperty("student").GetValue(list[i], null);
            //    IList ll = obj as IList;

            //    foreach (var item in ll)
            //    {
            //        Console.WriteLine(string.Format("Name:{0},Age:{1}", item.GetType().GetProperty("Name").GetValue(item, null), item.GetType().GetProperty("Age").GetValue(item, null)));
            //    }
            //}

            Console.WriteLine();
        }

        public class Reflex
        {
            public string BZ { get; set; }

            public List<Student> Students { get; set; } = new List<Student>();
        }

        public class Student
        {
            [System.ComponentModel.Description("���")]
            public int Id { get; set; }

            [System.ComponentModel.Description("����")]
            public string Name { get; set; }

            public int Age { get; set; }
        }

        [TestMethod]
        public void TestMethod1()
        {
            List<string> a = new List<string> { "1", "2", "3", "4" };
            List<string> b = new List<string> { "1", "2", "3" };
            var c = a.Except(b).ToList();

            var isTrue = (false && false);
            Console.WriteLine(isTrue);
            isTrue = (true && true);
            Console.WriteLine(isTrue);
        }

        [TestMethod]
        public void TestMethod2()
        {
            var A = typeof(ISoftDeletable);
            var B = A.GetProperties()[0];
            Console.WriteLine(nameof(ISoftDeletable));
        }

        [TestMethod]
        public void TestThreadPriority()
        {
            Console.WriteLine(System.Threading.ThreadPriority.Normal);
        }

        [TestMethod]
        public void TestGuidGen()
        {
            for (int i = 0; i < 20; i++)
            {
                Console.WriteLine(Guid.NewGuid());
            }
        }

        [TestMethod]
        public void TestNull()
        {
            decimal? test = null;
            test = 0.000m;
            Assert.IsTrue((test ?? 0) == 0);
        }

        [TestMethod]
        public void TestAny()
        {
            List<User> list = new List<User>();
            list.Add(new User { Id = 1 });
            Console.WriteLine(list.Any());
            Assert.IsTrue(list.Any());
        }

        public class User
        {
            public int Id { get; set; }
        }
    }
}