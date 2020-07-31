using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Print.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            //try
            //{
            //    PrintDialog printDialog1 = new PrintDialog();
            //    //获取或设置一个值，该值指示是否显示“当前页”选项按钮。
            //    printDialog1.AllowCurrentPage = true;
            //    //获取或设置一个值，该值指示是否启用“打印到文件”复选框。
            //    printDialog1.AllowPrintToFile = false;
            //    //获取或设置一个值，该值指示是否启用“选择”选项按钮。
            //    printDialog1.AllowSelection = false;
            //    //获取或设置一个值，该值指示是否启用“页”选项按钮。
            //    printDialog1.AllowSomePages = true;
            //    //获取或设置一个值，该值指示是否选中“打印到文件”复选框。
            //    printDialog1.PrintToFile = false;
            //    //获取或设置一个值，该值指示是否显示“帮助”按钮。
            //    printDialog1.ShowHelp = true;
            //    //获取或设置一个值，该值指示是否显示“网络”按钮。
            //    printDialog1.ShowNetwork = true;
            //    //printDialog1.PrinterSettings.Copies = (short)1;

            //    //printDialog1.PrinterSettings.MinimumPage = 1;

            //    //printDialog1.PrinterSettings.PrinterName = this.PrinterToPrint;
            //    DialogResult result = printDialog1.ShowDialog();
            //    if (result == DialogResult.OK)
            //    {
            //        CheckPrinter(printDialog1);

            //        var aaaa = printDialog1.PrinterSettings;
            //        var bbbb = printDialog1.AllowSomePages;
            //        var cccc = printDialog1.AllowSelection;
            //        var dddd = printDialog1.AllowCurrentPage;
            //        var eeee = printDialog1.AllowPrintToFile;
            //        var ffff = printDialog1.PrintToFile;
            //        //只能打印一份
            //        printDialog1.PrinterSettings.Copies = (short)1;

            //        string app = Path.Combine(localPath, "SumatraPDF", "SumatraPDF.exe");
            //        string args = $" -silent -exit-on-print -print-to \"{printDialog1.PrinterSettings.PrinterName}\" \"{fileName}\"";

            //        Process process = new Process
            //        {
            //            StartInfo = new ProcessStartInfo()
            //            {
            //                UseShellExecute = true,
            //                //Verb = "Print",
            //                CreateNoWindow = true,
            //                WindowStyle = ProcessWindowStyle.Hidden,
            //                FileName = app,
            //                Arguments = args
            //            }
            //        };
            //        //process.Exited += new EventHandler(proc_Exited);

            //        process.Exited += delegate (Object o, EventArgs e) { proc_Exited(fileName); };
            //        //if (User.isNetworkService())
            //        //{
            //        //    p.StartInfo.UserName = Program.config.serviceLogin;
            //        //    p.StartInfo.Password = tools.secureString(tools.Decrypt(Program.config.servicePass));
            //        //    p.StartInfo.Domain = Program.config.serviceDomain;
            //        //    p.StartInfo.LoadUserProfile = true;
            //        //    p.StartInfo.UseShellExecute = false;
            //        //}
            //        try
            //        {
            //            process.Start();
            //        }
            //        catch (Exception ex)
            //        {
            //            MessageBox.Show(ex.Message);
            //        }
            //        finally
            //        {
            //            try
            //            {
            //                if (process != null)
            //                {
            //                    process.Close();
            //                    process = null;
            //                }
            //            }
            //            catch { }
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
        }
    }
}
