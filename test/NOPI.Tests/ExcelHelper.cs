using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace NOPI.Tests
{
    public class ExcelHelper
    {
        public static string ToExcel(string sheetName, List<Header> headers, List<Hashtable> table)
        {
            var folderPath = "E:\\DEMO\\TEMP\\";
            if (!System.IO.Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            string path = string.Format("{0}/{1}.xls", folderPath, Guid.NewGuid().ToString("N"));
            NPOI.SS.UserModel.IWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();
            NPOI.SS.UserModel.ISheet sheet = book.CreateSheet(sheetName);
            // 添加表头
            NPOI.SS.UserModel.IRow row = sheet.CreateRow(0);
            int index = 0;
            foreach (var item in headers)
            {
                NPOI.SS.UserModel.ICell cell = row.CreateCell(index);
                cell.SetCellType(NPOI.SS.UserModel.CellType.String);
                cell.SetCellValue(item.Text);
                index++;
            }
            // 添加数据
            for (int i = 0; i < table.Count; i++)
            {
                index = 0;
                row = sheet.CreateRow(i + 1);
                foreach (var item in headers)
                {
                    NPOI.SS.UserModel.ICell cell = row.CreateCell(index);
                    cell.SetCellType(NPOI.SS.UserModel.CellType.String);
                    cell.SetCellValue((table[i][item.Value] ?? "").ToString());
                    index++;
                }
            }
            // 写入 
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            book.Write(ms);
            book = null;
            using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                byte[] data = ms.ToArray();
                fs.Write(data, 0, data.Length);
                fs.Flush();
            }
            ms.Close();
            ms.Dispose();
            return path;
        }

        /// <summary>
        /// 填充excel并生成文件
        /// </summary>
        /// <param name="fileName">目标excel文件名</param>
        /// <param name="sheetAt">目标sheet索引</param>
        /// <param name="startRow">起始行索引</param>
        /// <param name="rows">表格数据</param>
        /// <param name="templatePath">excel模板文件名</param>
        /// <param name="cellHandler">设置单元格样式的方法委托</param>
        /// <param name="complete">数据填充完成后触发</param>
        /// <returns></returns>
        public static string FillExcel(string fileName, int sheetAt, int startRow, List<Hashtable> rows, string templatePath = null,
            Action<IWorkbook, ISheet, IRow, ICell> cellHandler = null,
            Action<IWorkbook, ISheet> complete = null)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                var folderPath = "E:\\DEMO\\TEMP\\";
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                fileName = string.Format("{0}{1}.xls", folderPath, Guid.NewGuid().ToString("N"));
            }
            File.Copy(templatePath, fileName);
            IWorkbook workbook = null;
            //读改
            using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {

                if (fileName.IndexOf(".xlsx") > 0) // 2007版本
                    workbook = new XSSFWorkbook(fs);
                else if (fileName.IndexOf(".xls") > 0) // 2003版本
                    workbook = new HSSFWorkbook(fs);
            }
            ISheet sheet = workbook.GetSheetAt(sheetAt);
            // 填充数据
            for (int i = 0; i < rows.Count; i++)
            {
                int index = 0;
                IRow row = sheet.GetRow(startRow + i) ?? sheet.CreateRow(startRow + i);
                foreach (int key in rows[i].Keys)
                {
                    ICell cell = row.GetCell(key) ?? row.CreateCell(key);
                    cell.SetCellValueEx(workbook, rows[i][key]);
                    cellHandler?.Invoke(workbook, sheet, row, cell);
                    index++;
                }
            }
            complete?.Invoke(workbook, sheet);
            // 写入 
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            workbook.Write(ms);
            workbook = null;
            byte[] data = ms.ToArray();
            ms.Close();
            ms.Dispose();
            using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Write))
            {
                fs.Write(data, 0, data.Length);
                fs.Flush();
            }
            return fileName;
        }

        /// <summary>
        /// 将excel中的数据导入到DataTable中
        /// </summary>
        /// <param name="fileName">excel文件路径</param>
        /// <param name="sheetName">excel工作薄sheet的名称</param>
        /// <param name="firstRow">第一行索引(起始位0)</param>
        /// <param name="exportColumnName">是否将首行作为DataTable的ColumnName</param>
        /// <returns>返回的DataTable</returns>
        public static DataTable ToTable(string fileName, string sheetName, int firstRow, bool exportColumnName)
        {
            FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            IWorkbook workbook = null;
            if (fileName.IndexOf(".xlsx") > 0) // 2007版本
                workbook = new XSSFWorkbook(fs);
            else if (fileName.IndexOf(".xls") > 0) // 2003版本
                workbook = new HSSFWorkbook(fs);
            ISheet sheet = null;
            DataTable data = new DataTable();
            int startRow = 0;
            if (sheetName != null)
            {
                sheet = workbook.GetSheet(sheetName);
                if (sheet == null)
                {
                    sheet = workbook.GetSheetAt(0);
                }
            }
            else
            {
                sheet = workbook.GetSheetAt(0);
            }
            if (sheet != null)
            {
                IRow _firstRow = sheet.GetRow(firstRow);
                if (_firstRow != null)
                {
                    int cellCount = 0;
                    if (exportColumnName)
                    {
                        for (int i = _firstRow.FirstCellNum; i < _firstRow.LastCellNum; i++)
                        {
                            ICell cell = _firstRow.GetCell(i);
                            if (cell != null && cell.CellType != CellType.Blank)
                            {
                                string cellValue = cell.ToString();
                                if (cellValue != null)
                                {
                                    DataColumn column = new DataColumn(cellValue.Trim());
                                    data.Columns.Add(column);
                                }
                                cellCount++;
                            }
                        }
                        startRow = firstRow + 1;
                    }
                    else
                    {
                        startRow = firstRow;
                    }

                    int rowCount = sheet.LastRowNum;
                    for (int i = startRow; i <= rowCount; i++)
                    {
                        IRow row = sheet.GetRow(i);
                        if (row == null)
                        {
                            continue;
                        }
                        DataRow dataRow = data.NewRow();
                        if (row.FirstCellNum < 0)
                        {
                            break;
                        }
                        for (int j = row.FirstCellNum; j < cellCount; j++)
                        {
                            ICell cell = row.GetCell(j);
                            if (cell != null)
                            {
                                dataRow[j] = GetCellValue(cell, workbook);
                            }
                        }
                        data.Rows.Add(dataRow);
                    }
                }
            }
            return data;
        }

        public static Hashtable ToHashtable(object obj)
        {
            Type t = obj.GetType();
            Hashtable table = new Hashtable();
            foreach (PropertyInfo pi in t.GetProperties())
            {
                var value = pi.GetValue(obj, null);
                var name = pi.Name;
                table[name] = value;
            }
            return table;
        }
        public static List<Hashtable> ToHashtables(IEnumerable<dynamic> objs)
        {
            List<Hashtable> tables = new List<Hashtable>();
            foreach (var obj in objs)
            {
                Type t = obj.GetType();
                Hashtable table = new Hashtable();
                foreach (PropertyInfo pi in t.GetProperties())
                {
                    var value = pi.GetValue(obj, null);
                    var name = pi.Name;
                    table[name] = value;
                }
                tables.Add(table);
            }
            return tables;
        }

        public static void Fill(object obj, List<Header> headers, DataRow dr)
        {
            Type t = obj.GetType();
            foreach (PropertyInfo pi in t.GetProperties())
            {
                var header = headers.FirstOrDefault(x => x.Value == pi.Name);
                if (header != null)
                {
                    var val = dr[header.Text];
                    if (val == null)
                    {
                        pi.SetValue(obj, null);
                    }
                    else
                    {
                        var type = pi.PropertyType;
                        Type underlyingType = Nullable.GetUnderlyingType(type);
                        var nval = Convert.ChangeType(val, underlyingType ?? type);
                        pi.SetValue(obj, nval);
                    }
                }
            }
        }

        static object GetCellValue(ICell cell, IWorkbook workbook)
        {
            object result = null;
            switch (cell.CellType)
            {
                case CellType.Blank:
                    result = string.Empty;
                    break;
                case CellType.Boolean:
                    result = cell.BooleanCellValue;
                    break;
                case CellType.Numeric:
                    if (HSSFDateUtil.IsCellDateFormatted(cell))//日期类型
                    {
                        result = cell.DateCellValue;
                    }
                    else//其他数字类型
                    {
                        result = cell.NumericCellValue;
                    }
                    break;
                case CellType.Formula:
                    switch (cell.CachedFormulaResultType)
                    {
                        case CellType.String:
                            string strFORMULA = cell.StringCellValue;
                            if (strFORMULA != null && strFORMULA.Length > 0)
                            {
                                result = strFORMULA.ToString();
                            }
                            else
                            {
                                result = null;
                            }
                            break;
                        case CellType.Numeric:
                            result = cell.NumericCellValue;
                            break;
                        case CellType.Boolean:
                            result = cell.BooleanCellValue;
                            break;
                        case CellType.Error:
                            result = cell.ErrorCellValue;
                            break;
                        default:
                            result = string.Empty;
                            break;
                    }
                    break;
                case CellType.String:
                    result = cell.StringCellValue;
                    break;
                case CellType.Error:
                    result = cell.ErrorCellValue;
                    break;
                default:
                    result = cell.ToString();
                    break;
            }

            return result;
        }


        /// <summary>
        /// 填充excel并生成文件
        /// </summary>
        /// <param name="fileName">目标excel文件名</param>
        /// <param name="sheetAt">目标sheet索引</param>
        /// <param name="startRow">起始行索引</param>
        /// <param name="rows">表格数据</param>
        /// <param name="templatePath">excel模板文件名</param>
        /// <param name="cellHandler">设置单元格样式的方法委托</param>
        /// <param name="complete">数据填充完成后触发</param>
        /// <returns></returns>
        public static string FillExcelMultipleTables(string fileName, int sheetAt, int startRow, List<Hashtable> rows, string templatePath = null,
            Action<IWorkbook, ISheet, IRow, ICell> cellHandler = null,
            Action<IWorkbook, ISheet> complete = null)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                var folderPath = "E:\\DEMO\\TEMP\\";
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                fileName = string.Format("{0}{1}.xls", folderPath, Guid.NewGuid().ToString("N"));
                File.Copy(templatePath, fileName);
            }

            IWorkbook workbook = null;
            //读改
            using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {

                if (fileName.IndexOf(".xlsx") > 0) // 2007版本
                    workbook = new XSSFWorkbook(fs);
                else if (fileName.IndexOf(".xls") > 0) // 2003版本
                    workbook = new HSSFWorkbook(fs);
            }
            ISheet sheet = workbook.GetSheetAt(sheetAt);
            // 填充数据
            for (int i = 0; i < rows.Count; i++)
            {
                int index = 0;
                IRow row = sheet.GetRow(startRow + i) ?? sheet.CreateRow(startRow + i);
                foreach (int key in rows[i].Keys)
                {
                    ICell cell = row.GetCell(key) ?? row.CreateCell(key);
                    cell.SetCellValueEx(workbook, rows[i][key]);
                    cellHandler?.Invoke(workbook, sheet, row, cell);
                    index++;
                }
            }
            complete?.Invoke(workbook, sheet);
            // 写入 
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            workbook.Write(ms);
            workbook = null;
            byte[] data = ms.ToArray();
            ms.Close();
            ms.Dispose();
            using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Write))
            {
                fs.Write(data, 0, data.Length);
                fs.Flush();
            }
            return fileName;
        }


    }

    public class Header
    {
        public string Text { get; set; }
        public string Value { get; set; }
    }
}
