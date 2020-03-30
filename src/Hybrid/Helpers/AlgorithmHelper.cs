using System;
using System.Collections.Generic;
using System.Linq;

namespace Hybrid.Helpers
{
    /// <summary>
    /// 算法
    /// </summary>
    public sealed class AlgorithmHelper
    {
        #region 阶梯算法

        /// <summary>
        /// 阶梯算法
        /// </summary>
        /// <param name="ladderMinValues">每个阶梯的最小值集合</param>
        /// <param name="ladderPrices">每个阶梯的单价集合</param>
        /// <param name="actualUsage">实际使用量 eg:实际距离(单位：千米)</param>
        public static decimal GetLadderValue(IReadOnlyList<int> ladderMinValues, IReadOnlyList<int> ladderPrices, int actualUsage)
        {
            if (ladderMinValues.Count <= 1 || ladderPrices.Count <= 1)
            {
                throw new ArgumentException("未设置阶梯分组或阶梯单价");
            }
            var totalPrice = new List<int>();

            int m = ladderMinValues.Count - 1;

            for (int i = m; i >= 0; i--)
            {
                if (i == 0)
                {
                    totalPrice.Add(ladderPrices[i] * ladderMinValues[1]);
                }
                else
                {
                    int tempValue = actualUsage - ladderMinValues[i];
                    if (tempValue < 0)
                    {
                        totalPrice.Add(0);
                    }
                    else
                    {
                        totalPrice.Add(ladderPrices[i] * tempValue);
                        actualUsage = actualUsage - tempValue;
                    }
                }
            }

            return totalPrice.Sum();
        }

        /// <summary>
        /// 阶梯算法
        /// </summary>
        /// <param name="ladderMinValues">每个阶梯的最小值集合</param>
        /// <param name="ladderPrices">每个阶梯的单价集合</param>
        /// <param name="actualUsage">实际使用量 eg:实际距离(单位：千米)</param>
        public static decimal GetLadderValue(IReadOnlyList<decimal> ladderMinValues, IReadOnlyList<decimal> ladderPrices, decimal actualUsage)
        {
            if (ladderMinValues.Count <= 1 || ladderPrices.Count <= 1)
            {
                throw new ArgumentException("未设置阶梯分组或阶梯单价");
            }
            var totalPrice = new List<decimal>();

            int m = ladderMinValues.Count - 1;

            for (int i = m; i >= 0; i--)
            {
                if (i == 0)
                {
                    totalPrice.Add(ladderPrices[i] * ladderMinValues[1]);
                }
                else
                {
                    decimal tempValue = actualUsage - ladderMinValues[i];
                    if (tempValue < 0)
                    {
                        totalPrice.Add(0);
                    }
                    else
                    {
                        totalPrice.Add(ladderPrices[i] * tempValue);
                        actualUsage = actualUsage - tempValue;
                    }
                }
            }

            return totalPrice.Sum();
        }

        /// <summary>
        /// 阶梯算法
        /// </summary>
        /// <param name="ladderMinValues">每个阶梯的最小值集合</param>
        /// <param name="ladderPrices">每个阶梯的单价集合</param>
        /// <param name="actualUsage">实际使用量 eg:实际距离(单位：千米)</param>
        public static decimal GetLadderValue(IReadOnlyList<int> ladderMinValues, IReadOnlyList<decimal> ladderPrices, decimal actualUsage)
        {
            if (ladderMinValues.Count <= 1 || ladderPrices.Count <= 1)
            {
                throw new ArgumentException("未设置阶梯分组或阶梯单价");
            }
            var totalPrice = new List<decimal>();

            int m = ladderMinValues.Count - 1;

            for (int i = m; i >= 0; i--)
            {
                if (i == 0)
                {
                    totalPrice.Add(ladderPrices[i] * ladderMinValues[1]);
                }
                else
                {
                    decimal tempValue = actualUsage - ladderMinValues[i];
                    if (tempValue < 0)
                    {
                        totalPrice.Add(0);
                    }
                    else
                    {
                        totalPrice.Add(ladderPrices[i] * tempValue);
                        actualUsage = actualUsage - tempValue;
                    }
                }
            }

            return totalPrice.Sum();
        }

        /// <summary>
        /// 阶梯算法
        /// </summary>
        /// <param name="ladderMinValues">每个阶梯的最小值集合</param>
        /// <param name="ladderPrices">每个阶梯的单价集合</param>
        /// <param name="actualUsage">实际使用量 eg:实际距离(单位：千米)</param>
        public static decimal GetLadderValue(IReadOnlyList<int> ladderMinValues, IReadOnlyList<decimal> ladderPrices, int actualUsage)
        {
            if (ladderMinValues.Count <= 1 || ladderPrices.Count <= 1)
            {
                throw new ArgumentException("未设置阶梯分组或阶梯单价");
            }
            var totalPrice = new List<decimal>();

            int m = ladderMinValues.Count - 1;

            for (int i = m; i >= 0; i--)
            {
                if (i == 0)
                {
                    totalPrice.Add(ladderPrices[i] * ladderMinValues[1]);
                }
                else
                {
                    int tempValue = actualUsage - ladderMinValues[i];
                    if (tempValue < 0)
                    {
                        totalPrice.Add(0);
                    }
                    else
                    {
                        totalPrice.Add(ladderPrices[i] * tempValue);
                        actualUsage = actualUsage - tempValue;
                    }
                }
            }

            return totalPrice.Sum();
        }

        #endregion 阶梯算法
    }
}