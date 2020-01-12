// -----------------------------------------------------------------------
//  <copyright file="ExpressionCacheKeyGenerator.cs" company="com.esoftor">
//      Copyright © 2019-2020 ESoftor. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor></last-editor>
//  <last-date>2017-09-17 11:44</last-date>
// -----------------------------------------------------------------------

using ESoftor.Collections;

using System.Linq;
using System.Linq.Expressions;

namespace ESoftor.Caching
{
    /// <summary>
    /// 表达式缓存键生成器
    /// </summary>
    public class ExpressionCacheKeyGenerator : ICacheKeyGenerator
    {
        private readonly Expression _expression;

        /// <summary>
        /// 初始化一个<see cref="ExpressionCacheKeyGenerator"/>类型的新实例
        /// </summary>
        public ExpressionCacheKeyGenerator(Expression expression)
        {
            _expression = expression;
        }

        #region Implementation of ICacheKeyGenerator

        /// <summary>
        /// 生成缓存键
        /// </summary>
        /// <param name="args">参数</param>
        /// <returns></returns>
        public string GetKey(params object[] args)
        {
            Expression expression = _expression;
            expression = Evaluator.PartialEval(expression, CanBeEvaluatedLocally);
            expression = LocalCollectionExpressionVisitor.Rewrite(expression);
            string key = expression.ToString();
            return key + args.ExpandAndToString();
        }

        #endregion Implementation of ICacheKeyGenerator

        private static bool CanBeEvaluatedLocally(Expression expression)
        {
            if (expression.NodeType == ExpressionType.Parameter)
            {
                return false;
            }
            if (typeof(IQueryable).IsAssignableFrom(expression.Type))
            {
                return false;
            }
            return true;
        }
    }
}