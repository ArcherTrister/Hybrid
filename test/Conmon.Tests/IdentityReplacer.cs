using Aspose.Words;
using Aspose.Words.Replacing;

using Hybrid.Application.Services.Dtos;

using System.Collections.Generic;
using System.Drawing;

namespace Conmon.Tests
{
    /// <summary>
    /// 表示对合同条款勾叉状态处理的方法
    /// </summary>
    /// <param name="docBuilder">document builder</param>
    /// <param name="vars">模板键值对集合</param>
    public delegate void ClauseMatchSectionHandler(DocumentBuilder docBuilder, List<NameValue> vars);

    /// <summary>
    /// 处理合同条款内勾叉的替换回调
    /// </summary>
    public class IdentityReplacer : IReplacingCallback
    {
        private readonly Document _doc;
        private readonly List<NameValue> _vars = new List<NameValue>();
        private readonly Dictionary<string, ClauseMatchSectionHandler> _clauseHandlers = new Dictionary<string, ClauseMatchSectionHandler>();
        private readonly object _state;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="vars">变量键值对集合</param>
        public IdentityReplacer(
            Document doc,
            List<NameValue> vars,
            Dictionary<string, ClauseMatchSectionHandler> clauseHandlers,
            object state)
        {
            _doc = doc;
            _vars = vars;
            _clauseHandlers = clauseHandlers;
            _state = state;
        }

        public ReplaceAction Replacing(ReplacingArgs args)
        {
            DocumentBuilder builder = new DocumentBuilder(_doc);
            builder.MoveTo(args.MatchNode);
            builder.Font.Color = Color.Red;
            foreach (var keypair in _clauseHandlers)
            {
                if (keypair.Key == args.Match.Value)
                {
                    keypair.Value?.Invoke(builder, _vars);
                }
            }

            return ReplaceAction.Skip;
        }
    }
}
