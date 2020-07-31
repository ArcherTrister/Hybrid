using NPOI.SS.UserModel;

using System;

namespace NOPI.Tests
{
    /// <summary>
    /// NPOI扩展方法
    /// </summary>
    public static class NPOIExtension
    {
        /// <summary>
        /// 设置未知类型的单元格值
        /// </summary>
        /// <param name="cell">单元格</param>
        /// <param name="value">数据</param>
        public static void SetCellValueEx(this ICell cell, IWorkbook workbook, object value)
        {
            if (value == null)
            {
                cell.SetCellType(CellType.Blank);
            }
            else
            {
                Type type = value.GetType();
                if (type == typeof(bool))
                {
                    bool? _value = (bool?)value;
                    if (_value.HasValue)
                    {
                        cell.SetCellType(CellType.Boolean);
                        cell.SetCellValue((bool)value);
                    }
                    else
                    {
                        cell.SetCellType(CellType.Blank);
                    }
                }
                else if (type == typeof(int))
                {
                    int? _value = (int?)value;
                    if (_value.HasValue)
                    {
                        cell.SetCellType(CellType.Numeric);
                        cell.SetCellValue((int)value);
                    }
                    else
                    {
                        cell.SetCellType(CellType.Blank);
                    }
                }
                else if (type == typeof(decimal))
                {
                    decimal? _value = (decimal?)value;
                    if (_value.HasValue)
                    {
                        cell.SetCellType(CellType.Numeric);
                        cell.SetCellValue(value.ToDouble());
                    }
                    else
                    {
                        cell.SetCellType(CellType.Blank);
                    }
                }
                else if (type == typeof(double))
                {
                    double? _value = (double?)value;
                    if (_value.HasValue)
                    {
                        cell.SetCellType(CellType.Numeric);
                        cell.SetCellValue((double)value);
                    }
                    else
                    {
                        cell.SetCellType(CellType.Blank);
                    }
                }
                else if (type == typeof(float))
                {
                    float? _value = (float?)value;
                    if (_value.HasValue)
                    {
                        cell.SetCellType(CellType.Numeric);
                        cell.SetCellValue(value.ToDouble());
                    }
                    else
                    {
                        cell.SetCellType(CellType.Blank);
                    }
                }
                else if (type == typeof(DateTime))
                {
                    DateTime? _value = (DateTime?)value;
                    if (_value.HasValue)
                    {
                        cell.SetCellType(CellType.Numeric);
                        cell.SetCellValue((DateTime)value);
                    }
                    else
                    {
                        cell.SetCellType(CellType.Blank);
                    }
                }
                else
                {
                    cell.SetCellType(CellType.String);
                    cell.SetCellValue(Fun.ToSafeString(value));
                }
            }
        }
    }
}
