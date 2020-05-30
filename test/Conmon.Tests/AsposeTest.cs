using Aspose.Words;
using Aspose.Words.Saving;
using Aspose.Words.Tables;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Drawing;

namespace Conmon.Tests
{
    [TestClass]
    public class AsposeTest
    {
        [TestMethod]
        public void parseWord2Html()
        {
            //HtmlSaveOptions saveOptions = new HtmlSaveOptions();
            //saveOptions.setExportHeadersFootersMode(ExportHeadersFootersMode.NONE); // HtmlSaveOptions的其他设置信息请参考相关API
            //ByteArrayOutputStream htmlStream = new ByteArrayOutputStream();
            //String htmlText = "";
            try
            {
                Document doc = new Document("e:\\demo\\20200513133519164.docx");
                //DocumentBuilder builder = new DocumentBuilder(doc);
                doc.Save("e:\\demo\\test.html",  SaveFormat.Html);
            }
            catch (Exception e)
            {

            }
        }


        [TestMethod]
        public void WriteTest()
        {
            Document doc = new Document();
            DocumentBuilder builder = new DocumentBuilder(doc);

            // Specify paragraph formatting
            
            ParagraphFormat paragraphFormat = builder.ParagraphFormat;
            //paragraphFormat.ClearFormatting();
            paragraphFormat.FirstLineIndent = 8;
            paragraphFormat.LineSpacingRule = LineSpacingRule.AtLeast;//单倍行距
            paragraphFormat.Alignment = ParagraphAlignment.Justify;
            paragraphFormat.KeepTogether = true;
            
            //builder.Writeln("A whole paragraph.");

            builder.Font.ClearFormatting();
            builder.Font.Name = "宋体";
            builder.Font.Size = 12;
            //builder.Font.
            builder.Write("买受人：");

            builder.Font.ClearFormatting();
            builder.Font.Name = "宋体";
            builder.Font.Size = 12;
            builder.Font.Underline = Underline.Single;
            builder.Writeln("张三");

            builder.Font.ClearFormatting();
            builder.Font.Name = "宋体";
            builder.Font.Size = 20;
            builder.Writeln("Text in merged 20.");


            builder.Font.Border.Color = Color.Green;
            builder.Font.Border.LineWidth = 2.5d;
            builder.Font.Border.LineStyle = LineStyle.DashDotStroker;

            builder.Writeln("Text surrounded by green border.");


            builder.InsertHtml("");

            //paragraphFormat.ClearFormatting();

            doc.Save("e:\\demo\\WriteTest.doc", SaveFormat.Doc);
        }

        public static void WordPageSetup(Document doc)
        {
            #region 段落格式设定
            Paragraph p = new Paragraph(doc);
            p.ParagraphFormat.LeftIndent = float.Parse("2.0");//左缩进
            p.ParagraphFormat.RightIndent = float.Parse("2.0");//右缩进
            p.ParagraphFormat.SpaceBefore = float.Parse("2.0");//段前间距
            p.ParagraphFormat.FirstLineIndent = float.Parse("2.0");//首行缩进
            p.ParagraphFormat.SpaceBeforeAuto = false;//
            p.ParagraphFormat.SpaceAfter = float.Parse("2.0");//段后间距
            p.ParagraphFormat.SpaceAfterAuto = false;//
            p.ParagraphFormat.LineSpacingRule = LineSpacingRule.AtLeast;//单倍行距
            p.ParagraphFormat.Alignment = ParagraphAlignment.Justify;//段落2端对齐
            p.ParagraphFormat.WidowControl = false;//孤行控制
            p.ParagraphFormat.KeepWithNext = false;//与下段同页
            p.ParagraphFormat.KeepTogether = false;//段中不分页
            p.ParagraphFormat.PageBreakBefore = false;//段前分页

            p.ParagraphFormat.OutlineLevel = OutlineLevel.BodyText;
            p.ParagraphFormat.FarEastLineBreakControl = false;
            p.ParagraphFormat.WordWrap = false;
            p.ParagraphFormat.HangingPunctuation = false;
            p.ParagraphFormat.AddSpaceBetweenFarEastAndAlpha = false;
            p.ParagraphFormat.AddSpaceBetweenFarEastAndDigit = false;
            #endregion 段落格式设定
            #region 文字样式
            p.ParagraphFormat.Style.Name = "样式名称";
            p.ParagraphFormat.Style.Font.NameAscii = "Times New Roman";
            p.ParagraphFormat.Style.Font.NameOther = "Times New Roman";
            p.ParagraphFormat.Style.Font.Name = "宋体";
            p.ParagraphFormat.Style.Font.Size = 0.3;
            p.ParagraphFormat.Style.Font.Bold = false;
            p.ParagraphFormat.Style.Font.Italic = false;
            p.ParagraphFormat.Style.Font.Underline = Underline.Dash;//下划线
            p.ParagraphFormat.Style.Font.UnderlineColor = Color.AliceBlue;//设置下划线颜色
            p.ParagraphFormat.Style.Font.StrikeThrough = false;//删除线
            p.ParagraphFormat.Style.Font.DoubleStrikeThrough = false;//双删除线
            p.ParagraphFormat.Style.Font.Outline = false;//空心
            p.ParagraphFormat.Style.Font.Emboss = false;//阳文
            p.ParagraphFormat.Style.Font.Shadow = false;//阴影
            p.ParagraphFormat.Style.Font.Hidden = false;//隐藏文字
            p.ParagraphFormat.Style.Font.SmallCaps = false;//小型大写字母
            p.ParagraphFormat.Style.Font.AllCaps = false;//全部大写字母
            p.ParagraphFormat.Style.Font.Color = Color.AliceBlue;
            p.ParagraphFormat.Style.Font.Engrave = false;//阴文
            p.ParagraphFormat.Style.Font.Superscript = false;//上标
            p.ParagraphFormat.Style.Font.Subscript = false;//下标
            p.ParagraphFormat.Style.Font.Spacing = 0.2;//字符间距
            p.ParagraphFormat.Style.Font.Scaling = 2;//字符缩放
            p.ParagraphFormat.Style.Font.Position = 1.2;//位置
            p.ParagraphFormat.Style.Font.Kerning = 0.2;//字体间距调整
            #endregion
        }
    


        [TestMethod]
        public void HorizontalMerge()
        {
            //ExStart
            //ExFor:CellMerge
            //ExFor:CellFormat.HorizontalMerge
            //ExId:HorizontalMerge
            //ExSummary:Creates a table with two rows with cells in the first row horizontally merged.
            Document doc = new Document();
            DocumentBuilder builder = new DocumentBuilder(doc);

            builder.InsertCell();
            builder.CellFormat.HorizontalMerge = CellMerge.First;
            builder.Write("Text in merged cells.");

            builder.InsertCell();
            // This cell is merged to the previous and should be empty.
            builder.CellFormat.HorizontalMerge = CellMerge.Previous;
            builder.EndRow();

            builder.InsertCell();
            builder.CellFormat.HorizontalMerge = CellMerge.None;
            builder.Write("Text in one cell.");

            builder.InsertCell();
            builder.Write("Text in another cell.");
            builder.EndRow();
            builder.EndTable();
            //ExEnd
        }
    }
}
