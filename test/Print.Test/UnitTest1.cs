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
            //    //��ȡ������һ��ֵ����ֵָʾ�Ƿ���ʾ����ǰҳ��ѡ�ť��
            //    printDialog1.AllowCurrentPage = true;
            //    //��ȡ������һ��ֵ����ֵָʾ�Ƿ����á���ӡ���ļ�����ѡ��
            //    printDialog1.AllowPrintToFile = false;
            //    //��ȡ������һ��ֵ����ֵָʾ�Ƿ����á�ѡ��ѡ�ť��
            //    printDialog1.AllowSelection = false;
            //    //��ȡ������һ��ֵ����ֵָʾ�Ƿ����á�ҳ��ѡ�ť��
            //    printDialog1.AllowSomePages = true;
            //    //��ȡ������һ��ֵ����ֵָʾ�Ƿ�ѡ�С���ӡ���ļ�����ѡ��
            //    printDialog1.PrintToFile = false;
            //    //��ȡ������һ��ֵ����ֵָʾ�Ƿ���ʾ����������ť��
            //    printDialog1.ShowHelp = true;
            //    //��ȡ������һ��ֵ����ֵָʾ�Ƿ���ʾ�����硱��ť��
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
            //        //ֻ�ܴ�ӡһ��
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
