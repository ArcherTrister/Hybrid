using Microsoft.VisualStudio.TestTools.UnitTesting;

using Newtonsoft.Json;

using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using NPOI.SS.UserModel;

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NOPI.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            string json = "{\"residenceTotalNum\": 44119,     \"residenceTotalArea\": 4369762.603,     \"garageTotalNum\": 1432,     \"garageTotalArea\": 46534.31,     \"stallTotalNum\": 9104,     \"stallTotalArea\": 130142.66,     \"businessTotalNum\": 7630,     \"businessTotalArea\": 394402.36,     \"salesOutputs\": [         {             \"status\": 4,             \"statisticsOutputs\": [                 {                     \"reserveAmount\": 50000,                     \"firstPayAmount\": 338211339951.55,                     \"finalPayAmount\": 0,                     \"uncollected\": -328152254401.85,                     \"contractTotalPrice\": 10059135549.7,                     \"housePurpose\": 10,                     \"totalNum\": 24446,                     \"totalArea\": 2942668.02,                     \"unitPrice\": 0                 },                 {                     \"reserveAmount\": 0,                     \"firstPayAmount\": 203585452,                     \"finalPayAmount\": 0,                     \"uncollected\": 8079300,                     \"contractTotalPrice\": 211664752,                     \"housePurpose\": 2,                     \"totalNum\": 2339,                     \"totalArea\": 32924.85,                     \"unitPrice\": 0                 },                 {                     \"reserveAmount\": 0,                     \"firstPayAmount\": 460578664.29,                     \"finalPayAmount\": 0,                     \"uncollected\": 108697750.22,                     \"contractTotalPrice\": 569276414.51,                     \"housePurpose\": 31,                     \"totalNum\": 1300,                     \"totalArea\": 91496.53,                     \"unitPrice\": 0                 },                 {                     \"reserveAmount\": 0,                     \"firstPayAmount\": 1013143138,                     \"finalPayAmount\": 0,                     \"uncollected\": -931592954,                     \"contractTotalPrice\": 81550184,                     \"housePurpose\": 1,                     \"totalNum\": 677,                     \"totalArea\": 17676.03,                     \"unitPrice\": 0                 }             ]         },         {             \"status\": 1,             \"statisticsOutputs\": [                 {                     \"reserveAmount\": 20000,                     \"firstPayAmount\": 2926008.7,                     \"finalPayAmount\": 0,                     \"uncollected\": 0,                     \"contractTotalPrice\": 26360683.7,                     \"housePurpose\": 10,                     \"totalNum\": 14492,                     \"totalArea\": 861736.513,                     \"unitPrice\": 0                 },                 {                     \"reserveAmount\": 100,                     \"firstPayAmount\": 1360000,                     \"finalPayAmount\": 0,                     \"uncollected\": 0,                     \"contractTotalPrice\": 3232915,                     \"housePurpose\": 31,                     \"totalNum\": 5421,                     \"totalArea\": 260863.64,                     \"unitPrice\": 0                 },                 {                     \"reserveAmount\": 10000,                     \"firstPayAmount\": 869540,                     \"finalPayAmount\": 0,                     \"uncollected\": 0,                     \"contractTotalPrice\": 5137780,                     \"housePurpose\": 2,                     \"totalNum\": 6423,                     \"totalArea\": 91652.32,                     \"unitPrice\": 0                 },                 {                     \"reserveAmount\": 0,                     \"firstPayAmount\": 1119066,                     \"finalPayAmount\": 0,                     \"uncollected\": 0,                     \"contractTotalPrice\": 1119066,                     \"housePurpose\": 1,                     \"totalNum\": 525,                     \"totalArea\": 23107.18,                     \"unitPrice\": 0                 }             ]         },         {             \"status\": 3,             \"statisticsOutputs\": [                 {                     \"reserveAmount\": 16598,                     \"firstPayAmount\": 928595,                     \"finalPayAmount\": 0,                     \"uncollected\": 1948568910.3,                     \"contractTotalPrice\": 1949514103.3,                     \"housePurpose\": 10,                     \"totalNum\": 5181,                     \"totalArea\": 565358.07,                     \"unitPrice\": 0                 },                 {                     \"reserveAmount\": 0,                     \"firstPayAmount\": 0,                     \"finalPayAmount\": 0,                     \"uncollected\": 231023107,                     \"contractTotalPrice\": 231023107,                     \"housePurpose\": 31,                     \"totalNum\": 909,                     \"totalArea\": 42042.19,                     \"unitPrice\": 0                 },                 {                     \"reserveAmount\": 225,                     \"firstPayAmount\": 12111,                     \"finalPayAmount\": 0,                     \"uncollected\": 26109471,                     \"contractTotalPrice\": 26121807,                     \"housePurpose\": 2,                     \"totalNum\": 342,                     \"totalArea\": 5565.49,                     \"unitPrice\": 0                 },                 {                     \"reserveAmount\": 0,                     \"firstPayAmount\": 0,                     \"finalPayAmount\": 0,                     \"uncollected\": 29294381,                     \"contractTotalPrice\": 29294381,                     \"housePurpose\": 1,                     \"totalNum\": 230,                     \"totalArea\": 5751.1,                     \"unitPrice\": 0                 }             ]         }     ] }";
            var datas = JsonConvert.DeserializeObject<SalesStatisticsOutput>(json);
            string template = "E:\\DEMO\\房屋销售统计.xls";
            //List<Hashtable> rows = new List<Hashtable>();
            //foreach (var item in datas.SalesOutputs)
            //{

            //}
            //foreach (var item in datas.SalesOutputs)
            //{
            //    foreach (var output in item.StatisticsOutputs)
            //    {
            //        Hashtable table = new Hashtable
            //        {
            //            { 4, output.TotalNum },
            //            { 5, output.TotalArea },
            //            { 6, output.UnitPrice },
            //            { 7, output.ContractTotalPrice },
            //            { 8, output.ReserveAmount},
            //            { 9, output.FirstPayAmount },
            //            { 10, output.FinalPayAmount },
            //            { 11, output.Uncollected },
            //        };
            //        rows.Add(table);
            //    }
            //}
            string path = ExcelHelper.FillExcel("E:\\DEMO\\TEMP\\" + Guid.NewGuid().ToString("N") + ".xls", 0, 50, new List<Hashtable>(), template, null,
                (workbook, sheet) =>
                {
                    //HSSFSheet _sheet = sheet as HSSFSheet;
                    //IRow row = HSSFCellUtil.GetRow(1, _sheet);
                    for (int i = 0; i < 22; i++)
                    {
                        IRow row = sheet.GetRow(i) ?? sheet.CreateRow(i);
                        switch (i)
                        {
                            case 0:
                                ICell cell0 = row.GetCell(4) ?? row.CreateCell(4);
                                cell0.SetCellValueEx(workbook, "xxxx");
                                //row.GetCell(4).SetCellValue($"xxxx");
                                break;
                            //日期
                            case 1:
                                ICell cell1 = row.GetCell(6) ?? row.CreateCell(6);
                                cell1.SetCellValueEx(workbook, DateTime.Now);
                                //row.GetCell(6).SetCellValue(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                                break;
                            ////住宅总套数 总面积
                            //case 2:
                            //    row.GetCell(1).SetCellValue(datas.ResidenceTotalNum);
                            //    row.GetCell(3).SetCellValue(datas.ResidenceTotalArea);
                            //    break;
                            ////商铺总套数 总面积
                            //case 3:
                            //    row.GetCell(1).SetCellValue(datas.BusinessTotalNum);
                            //    row.GetCell(3).SetCellValue(datas.BusinessTotalArea);
                            //    break;
                            ////车位总套数 总面积
                            //case 4:
                            //    row.GetCell(1).SetCellValue(datas.StallTotalNum);
                            //    row.GetCell(3).SetCellValue(datas.StallTotalArea);
                            //    break;
                            //已售 签约 住宅
                            case 7:
                                var signeds = datas.SalesOutputs.Where(p => p.Status == HouseStatusEnum.Signed).Select(p => p.StatisticsOutputs).FirstOrDefault();
                                if (signeds.Any())
                                {
                                    var residence = signeds.Where(p => p.HousePurpose == HousePurposeEnum.Residence).FirstOrDefault();
                                    if (residence != null)
                                    {
                                        Hashtable table = new Hashtable
                                                {
                                                    { 4, residence.TotalNum },
                                                    { 5, residence.TotalArea },
                                                    //{ 6, residence.UnitPrice },
                                                    { 6, residence.ContractTotalPrice / residence.TotalArea / residence.TotalNum },
                                                    { 7, residence.ContractTotalPrice },
                                                    { 8, residence.ReserveAmount},
                                                    { 9, residence.FirstPayAmount },
                                                    { 10, residence.FinalPayAmount },
                                                    { 11, residence.Uncollected },
                                                };
                                        for (int m = 4; m < 11; m++)
                                        {
                                            ICell cell7 = row.GetCell(m) ?? row.CreateCell(m);
                                            cell7.SetCellValueEx(workbook, table[m]);
                                        }
                                        //ICell cell74 = row.GetCell(4) ?? row.CreateCell(4);
                                        //cell74.SetCellValueEx(workbook, residence.TotalNum);
                                        //ICell cell75 = row.GetCell(5) ?? row.CreateCell(5);
                                        //cell75.SetCellValueEx(workbook, residence.TotalArea);
                                        //ICell cell76 = row.GetCell(6) ?? row.CreateCell(6);
                                        //cell76.SetCellValueEx(workbook, residence.UnitPrice);
                                        //ICell cell77 = row.GetCell(7) ?? row.CreateCell(7);
                                        //cell77.SetCellValueEx(workbook, residence.ContractTotalPrice);
                                        //ICell cell78 = row.GetCell(8) ?? row.CreateCell(8);
                                        //cell78.SetCellValueEx(workbook, residence.ReserveAmount);
                                        //ICell cell79 = row.GetCell(9) ?? row.CreateCell(9);
                                        //cell79.SetCellValueEx(workbook, residence.FirstPayAmount);
                                        //ICell cell710 = row.GetCell(10) ?? row.CreateCell(10);
                                        //cell710.SetCellValueEx(workbook, residence.FinalPayAmount);
                                    }
                                }

                                break;
                            //已售 签约 商铺
                            case 8:
                                var signeds2 = datas.SalesOutputs.Where(p => p.Status == HouseStatusEnum.Signed).Select(p => p.StatisticsOutputs).FirstOrDefault();
                                if (signeds2.Any())
                                {
                                    var residence = signeds2.Where(p => p.HousePurpose == HousePurposeEnum.Business).FirstOrDefault();
                                    if (residence != null)
                                    {
                                        Hashtable table = new Hashtable
                                                {
                                                    { 4, residence.TotalNum },
                                                    { 5, residence.TotalArea },
                                                                                                        //{ 6, residence.UnitPrice },
                                                    { 6, residence.ContractTotalPrice / residence.TotalArea / residence.TotalNum },
                                                    { 7, residence.ContractTotalPrice },
                                                    { 8, residence.ReserveAmount},
                                                    { 9, residence.FirstPayAmount },
                                                    { 10, residence.FinalPayAmount },
                                                    { 11, residence.Uncollected },
                                                };
                                        for (int m = 4; m < 11; m++)
                                        {
                                            ICell cell7 = row.GetCell(m) ?? row.CreateCell(m);
                                            cell7.SetCellValueEx(workbook, table[m]);
                                        }
                                    }
                                }
                                break;
                            //已售 备案 住宅
                            case 9:
                                var recorded = datas.SalesOutputs.Where(p => p.Status == HouseStatusEnum.Recorded).Select(p => p.StatisticsOutputs).FirstOrDefault();
                                if (recorded.Any())
                                {
                                    var residence = recorded.Where(p => p.HousePurpose == HousePurposeEnum.Residence).FirstOrDefault();
                                    if (residence != null)
                                    {
                                        Hashtable table = new Hashtable
                                                {
                                                    { 4, residence.TotalNum },
                                                    { 5, residence.TotalArea },
                                                                                                        //{ 6, residence.UnitPrice },
                                                    { 6, residence.ContractTotalPrice / residence.TotalArea / residence.TotalNum },
                                                    { 7, residence.ContractTotalPrice },
                                                    { 8, residence.ReserveAmount},
                                                    { 9, residence.FirstPayAmount },
                                                    { 10, residence.FinalPayAmount },
                                                    { 11, residence.Uncollected },
                                                };
                                        for (int m = 4; m < 11; m++)
                                        {
                                            ICell cell7 = row.GetCell(m) ?? row.CreateCell(m);
                                            cell7.SetCellValueEx(workbook, table[m]);
                                        }
                                    }
                                }
                                break;
                            //已售 备案 商铺
                            case 10:
                                var recorded2 = datas.SalesOutputs.Where(p => p.Status == HouseStatusEnum.Recorded).Select(p => p.StatisticsOutputs).FirstOrDefault();
                                if (recorded2.Any())
                                {
                                    var residence = recorded2.Where(p => p.HousePurpose == HousePurposeEnum.Business).FirstOrDefault();
                                    if (residence != null)
                                    {
                                        Hashtable table = new Hashtable
                                                {
                                                    { 4, residence.TotalNum },
                                                    { 5, residence.TotalArea },
                                                                                                       //{ 6, residence.UnitPrice },
                                                    { 6, residence.ContractTotalPrice / residence.TotalArea / residence.TotalNum },
                                                    { 7, residence.ContractTotalPrice },
                                                    { 8, residence.ReserveAmount},
                                                    { 9, residence.FirstPayAmount },
                                                    { 10, residence.FinalPayAmount },
                                                    { 11, residence.Uncollected },
                                                };
                                        for (int m = 4; m < 11; m++)
                                        {
                                            ICell cell7 = row.GetCell(m) ?? row.CreateCell(m);
                                            cell7.SetCellValueEx(workbook, table[m]);
                                        }
                                    }
                                }
                                break;
                            case 11:
                            case 12:
                                //foreach (var item in datas.SalesOutputs)
                                //{
                                //    foreach (var output in item.StatisticsOutputs)
                                //    {

                                //    }
                                //}
                                //for (int m = 0; m < datas.SalesOutputs.Count; m++)
                                //{
                                //    var item =
                                //    for (int n = 4; n < 11; n++)
                                //    {
                                //        row.GetCell(n).SetCellValue(datas.StallTotalNum);
                                //    }
                                //}
                                //for (int m = 0; m < rows.Count; m++)
                                //{
                                //    foreach (int key in rows[m].Keys)
                                //    {
                                //        ICell cell = row.GetCell(key) ?? row.CreateCell(key);
                                //        cell.SetCellValueEx(workbook, rows[m][key]);
                                //    }
                                //}
                                break;
                            //未预定 住宅
                            case 15:
                                var noReserve = datas.SalesOutputs.Where(p => p.Status == HouseStatusEnum.NoReserve).Select(p => p.StatisticsOutputs).FirstOrDefault();
                                if (noReserve.Any())
                                {
                                    var residence = noReserve.Where(p => p.HousePurpose == HousePurposeEnum.Residence).FirstOrDefault();
                                    if (residence != null)
                                    {
                                        Hashtable table = new Hashtable
                                                {
                                                    { 4, residence.TotalNum },
                                                    { 5, residence.TotalArea },
                                                    { 6, residence.UnitPrice },
                                                    //{ 7, residence.ContractTotalPrice },
                                                    { 7, residence.TotalNum * residence.TotalArea * residence.UnitPrice },
                                                    { 8, residence.ReserveAmount},
                                                    { 9, residence.FirstPayAmount },
                                                    { 10, residence.FinalPayAmount },
                                                    { 11, residence.Uncollected },
                                                };
                                        for (int m = 4; m < 11; m++)
                                        {
                                            ICell cell7 = row.GetCell(m) ?? row.CreateCell(m);
                                            cell7.SetCellValueEx(workbook, table[m]);
                                        }
                                    }
                                }
                                break;
                            //未预定 商铺
                            case 16:
                                var noReserve1 = datas.SalesOutputs.Where(p => p.Status == HouseStatusEnum.NoReserve).Select(p => p.StatisticsOutputs).FirstOrDefault();
                                if (noReserve1.Any())
                                {
                                    var residence = noReserve1.Where(p => p.HousePurpose == HousePurposeEnum.Business).FirstOrDefault();
                                    if (residence != null)
                                    {
                                        Hashtable table = new Hashtable
                                                {
                                                    { 4, residence.TotalNum },
                                                    { 5, residence.TotalArea },
                                                    { 6, residence.UnitPrice },
                                                         //{ 7, residence.ContractTotalPrice },
                                                    { 7, residence.TotalNum * residence.TotalArea * residence.UnitPrice },
                                                    { 8, residence.ReserveAmount},
                                                    { 9, residence.FirstPayAmount },
                                                    { 10, residence.FinalPayAmount },
                                                    { 11, residence.Uncollected },
                                                };
                                        for (int m = 4; m < 11; m++)
                                        {
                                            ICell cell7 = row.GetCell(m) ?? row.CreateCell(m);
                                            cell7.SetCellValueEx(workbook, table[m]);
                                        }
                                    }
                                }
                                break;
                            //未预定 车位
                            case 17:
                                var noReserve2 = datas.SalesOutputs.Where(p => p.Status == HouseStatusEnum.NoReserve).Select(p => p.StatisticsOutputs).FirstOrDefault();
                                if (noReserve2.Any())
                                {
                                    var residence = noReserve2.Where(p => p.HousePurpose == HousePurposeEnum.Stall).FirstOrDefault();
                                    if (residence != null)
                                    {
                                        Hashtable table = new Hashtable
                                                {
                                                    { 4, residence.TotalNum },
                                                    { 5, residence.TotalArea },
                                                    { 6, residence.UnitPrice },
                                                         //{ 7, residence.ContractTotalPrice },
                                                    { 7, residence.TotalNum * residence.TotalArea * residence.UnitPrice },
                                                    { 8, residence.ReserveAmount},
                                                    { 9, residence.FirstPayAmount },
                                                    { 10, residence.FinalPayAmount },
                                                    { 11, residence.Uncollected },
                                                };
                                        for (int m = 4; m < 11; m++)
                                        {
                                            ICell cell7 = row.GetCell(m) ?? row.CreateCell(m);
                                            cell7.SetCellValueEx(workbook, table[m]);
                                        }
                                    }
                                }
                                break;
                            //未预定 车库
                            case 18:
                                var noReserve3 = datas.SalesOutputs.Where(p => p.Status == HouseStatusEnum.NoReserve).Select(p => p.StatisticsOutputs).FirstOrDefault();
                                if (noReserve3.Any())
                                {
                                    var residence = noReserve3.Where(p => p.HousePurpose == HousePurposeEnum.Garage).FirstOrDefault();
                                    if (residence != null)
                                    {
                                        Hashtable table = new Hashtable
                                                {
                                                    { 4, residence.TotalNum },
                                                    { 5, residence.TotalArea },
                                                    { 6, residence.UnitPrice },
                                                         //{ 7, residence.ContractTotalPrice },
                                                    { 7, residence.TotalNum * residence.TotalArea * residence.UnitPrice },
                                                    { 8, residence.ReserveAmount},
                                                    { 9, residence.FirstPayAmount },
                                                    { 10, residence.FinalPayAmount },
                                                    { 11, residence.Uncollected },
                                                };
                                        for (int m = 4; m < 11; m++)
                                        {
                                            ICell cell7 = row.GetCell(m) ?? row.CreateCell(m);
                                            cell7.SetCellValueEx(workbook, table[m]);
                                        }
                                    }
                                }
                                break;
                            default:
                                break;
                        }

                    }
                    ////
                    //IRow row0 = sheet.GetRow(0);
                    //row0.GetCell(4).SetCellValue($"xxxx");
                    ////日期

                    ////住宅总套数
                    //IRow row2 = sheet.GetRow(2);
                    //row2.GetCell(4).SetCellValue($"xxxx");
                    ////住宅总面积

                });



            //Console.ReadKey();
            //return File(path, "application/vnd.ms-excel", "工程合同导出表.xls");
        }
    }
}
