using Aspose.Words;
using Aspose.Words.Replacing;
using Hybrid.Application.Services.Dtos;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Conmon.Tests
{
    /// <summary>
    /// 房屋合同模板word处理类
    /// </summary>
    public class HouseContractWordHandler : WordHandler, IWordHandler
    {
        private HouseContractWordHandler()
        {

        }

        /// <summary>
        /// 唯一实例
        /// </summary>
        public static HouseContractWordHandler Instance => new HouseContractWordHandler();

        /// <summary>
        /// 解析word模板，并使用用户变量集合生成新的word文档，
        /// 注意使用完释放内存流避免造成内存泄露
        /// </summary>
        /// <param name="src">原word模板文件路径</param>
        /// <param name="vars">模板变量集合</param>
        /// <param name="state">任意用户自定义对象</param>
        /// <param name="defaultSetter">设置未定义变量默认值的方法</param>
        /// <param name="extraHandler">额外操作</param>
        /// <returns>内存流</returns>
        public MemoryStream Process(string src, List<NameValue> vars, object state, DefaultValueHandler defaultSetter, ExtraHandler extraHandler = null)
        {
            Document doc = new Document(src);
            //处理各选项的勾叉
            Dictionary<string, ClauseMatchSectionHandler> clauseHandlers = GetClauseHandlers();
            FindReplaceOptions identityOptions = new FindReplaceOptions();
            identityOptions.ReplacingCallback = new IdentityReplacer(doc, vars, clauseHandlers, state);
            foreach (var keypair in clauseHandlers)
            {
                doc.Range.Replace(keypair.Key, string.Empty, identityOptions);
            }

            //查找并替换变量
            FindReplaceOptions varReplaceOptions = new FindReplaceOptions();
            varReplaceOptions.FindWholeWordsOnly = true;
            varReplaceOptions.MatchCase = true;
            varReplaceOptions.ReplacingCallback = new VarReplacer(doc, vars, defaultSetter, state);
            doc.Range.Replace(new Regex(@"\$[a-zA-Z0-9]*\$"), string.Empty, varReplaceOptions);

            //可选的额外操作
            extraHandler?.Invoke(doc, vars);

            //保存结果
            MemoryStream ms = new MemoryStream();
            doc.Save(ms, SaveFormat.Docx);
            return ms;
        }

        /// <summary>
        /// 解析word模板，并使用用户变量集合生成新的word文档，并保存到目标文件
        /// </summary>
        /// <param name="src">原word模板文件路径</param>
        /// <param name="dest">保存的目标文件</param>
        /// <param name="vars">模板变量集合</param>
        /// <param name="state">任意用户自定义对象</param>
        /// <param name="defaultSetter">设置未定义变量默认值的方法</param>
        /// <param name="extraHandler">额外操作</param>
        public void Process(string src, string dest, List<NameValue> vars, object state, DefaultValueHandler defaultSetter, ExtraHandler extraHandler = null)
        {
            MemoryStream ms = Process(src, vars, state, defaultSetter, extraHandler);

            using (FileStream stream = new FileStream(dest, FileMode.Create))
            {
                ms.WriteTo(stream);
            }
            ms.Dispose();
        }

        /// <summary>
        /// 获取各条款的匹配处理方法
        /// </summary>
        /// <returns></returns>
        private Dictionary<string, ClauseMatchSectionHandler> GetClauseHandlers()
        {
            Dictionary<string, ClauseMatchSectionHandler> result = new Dictionary<string, ClauseMatchSectionHandler>();
            //#region 第四条 计价方式与价款
            //result.Add("1、按套内建筑面积计价", (DocumentBuilder builder, List<NameValue> vars) => {
            //    //第四条 计价方式与价款 1
            //    var jjfs = vars.FirstOrDefault(p => p.Name == "$UserCustom100$")?.Value.ToInt32();
            //    builder.Write(jjfs == 1 ? "√ " : "x ");
            //});
            //result.Add("2、按建筑面积计价", (DocumentBuilder builder, List<NameValue> vars) => {
            //    //第四条 计价方式与价款 2
            //    var jjfs = vars.FirstOrDefault(p => p.Name == "$UserCustom100$")?.Value.ToInt32();
            //    builder.Write(jjfs == 2 ? "√ " : "x ");
            //});
            //result.Add("3、按套（单元、幢）计价", (DocumentBuilder builder, List<NameValue> vars) => {
            //    //第四条 计价方式与价款 3
            //    var jjfs = vars.FirstOrDefault(p => p.Name == "$UserCustom100$")?.Value.ToInt32();
            //    builder.Write(jjfs == 3 ? "√ " : "x ");
            //});
            //#endregion

            //#region 第五条 面积误差的处理
            //result.Add("一、采用按建筑面积或套内建筑面积计价销售的", (DocumentBuilder builder, List<NameValue> vars) => {
            //    //第五条 面积误差的处理 一
            //    var jjfs = vars.FirstOrDefault(p => p.Name == "$UserCustom100$")?.Value.ToInt32();
            //    builder.Write(jjfs == 1 || jjfs == 2 ? "√ " : "x ");
            //});
            //result.Add("二、按套（单元、幢）计价销售的", (DocumentBuilder builder, List<NameValue> vars) => {
            //    //第五条 面积误差的处理 二
            //    var jjfs = vars.FirstOrDefault(p => p.Name == "$UserCustom100$")?.Value.ToInt32();
            //    builder.Write(jjfs == 3 ? "√ " : "x ");
            //});
            //result.Add("1、乙方有权提出退房，", (DocumentBuilder builder, List<NameValue> vars) => {
            //    //第五条 面积误差的处理 一 -> 1
            //    var wccl = vars.FirstOrDefault(p => p.Name == "$UserCustom040$")?.Value.ToInt32();
            //    builder.Write(wccl == 1 ? "√ " : "x ");
            //});
            //result.Add("2、本合同条款第四条第", (DocumentBuilder builder, List<NameValue> vars) => {
            //    //第五条 面积误差的处理 一 -> 2
            //    var wccl = vars.FirstOrDefault(p => p.Name == "$UserCustom040$")?.Value.ToInt32();
            //    builder.Write(wccl == 2 ? "√ " : "x ");
            //});
            //result.Add("3、$UserCustom025$", (DocumentBuilder builder, List<NameValue> vars) => {
            //    //第五条 面积误差的处理 一 -> 3
            //    var wccl = vars.FirstOrDefault(p => p.Name == "$UserCustom040$")?.Value.ToInt32();
            //    builder.Write(wccl == 3 ? "√ " : "x ");
            //});
            //#endregion

            //#region 第六条 付款方式及期限
            //result.Add("1、一次性付款", (DocumentBuilder builder, List<NameValue> vars) => {
            //    var fkfs = vars.FirstOrDefault(p => p.Name == "$FKFS$")?.Value.ToInt32();
            //    builder.Write(fkfs == 1 ? "√ " : "x ");
            //});
            //result.Add("2、分期付款", (DocumentBuilder builder, List<NameValue> vars) => {
            //    var fkfs = vars.FirstOrDefault(p => p.Name == "$FKFS$")?.Value.ToInt32();
            //    builder.Write(fkfs == 2 ? "√ " : "x ");
            //});
            //result.Add("3、其他方式", (DocumentBuilder builder, List<NameValue> vars) => {
            //    var fkfs = vars.FirstOrDefault(p => p.Name == "$FKFS$")?.Value.ToInt32();
            //    builder.Write(fkfs == 3 ? "√ " : "x ");
            //});
            //#endregion

            //#region 第八条　乙方逾期付款的违约责任
            //result.Add("一、自本合同约定的应付款时限届满后的次日起", (DocumentBuilder builder, List<NameValue> vars) => {
            //    var yqfk = vars.FirstOrDefault(p => p.Name == "$UserCustom044$")?.Value;
            //    builder.Write(yqfk == "一" ? "√ " : "x ");
            //});
            //result.Add("二、$UserCustom030$", (DocumentBuilder builder, List<NameValue> vars) => {
            //    var yqfk = vars.FirstOrDefault(p => p.Name == "$UserCustom044$")?.Value;
            //    builder.Write(yqfk == "二" ? "√ " : "x ");
            //});
            //#endregion

            //#region 第八条　乙方逾期付款的违约责任 第一种方式
            //result.Add("1、解除合同，乙方按应付未付款的", (DocumentBuilder builder, List<NameValue> vars) => {
            //    var yqfk = vars.FirstOrDefault(p => p.Name == "$UserCustom044$")?.Value;
            //    var yqfk1 = vars.FirstOrDefault(p => p.Name == "$UserCustom064$")?.Value;
            //    builder.Write(yqfk == "一" && yqfk1 == "1" ? "√ " : "x ");
            //});
            //result.Add("2、乙方按应付未付款的", (DocumentBuilder builder, List<NameValue> vars) => {
            //    var yqfk = vars.FirstOrDefault(p => p.Name == "$UserCustom044$")?.Value;
            //    var yqfk1 = vars.FirstOrDefault(p => p.Name == "$UserCustom064$")?.Value;
            //    builder.Write(yqfk == "一" && yqfk1 == "2" ? "√ " : "x ");
            //});
            //result.Add("3、$UserCustom029$", (DocumentBuilder builder, List<NameValue> vars) => {
            //    var yqfk = vars.FirstOrDefault(p => p.Name == "$UserCustom044$")?.Value;
            //    var yqfk1 = vars.FirstOrDefault(p => p.Name == "$UserCustom064$")?.Value;
            //    builder.Write(yqfk == "一" && yqfk1 == "3" ? "√ " : "x ");
            //});
            //#endregion

            //#region 第九条　甲方逾期交房的违约责任
            //result.Add("一、自本合同约定的交房时间届满后的次日起", (DocumentBuilder builder, List<NameValue> vars) => {
            //    var yqjf = vars.FirstOrDefault(p => p.Name == "$UserCustom050$")?.Value;
            //    builder.Write(yqjf == "一" ? "√ " : "x ");
            //});
            //result.Add("二、$UserCustom032$", (DocumentBuilder builder, List<NameValue> vars) => {
            //    var yqjf = vars.FirstOrDefault(p => p.Name == "$UserCustom050$")?.Value;
            //    builder.Write(yqjf == "二" ? "√ " : "x ");
            //});
            //#endregion

            //#region 第九条　甲方逾期交房的违约责任 第一种方式
            //result.Add("1、解除合同，甲方按乙方已付款的$UserCustom054$%", (DocumentBuilder builder, List<NameValue> vars) => {
            //    var yqjf1 = vars.FirstOrDefault(p => p.Name == "$UserCustom065$")?.Value;
            //    builder.Write(yqjf1 == "1" ? "√ " : "x ");
            //});
            //result.Add("2、甲方按乙方已付款的$UserCustom055$%", (DocumentBuilder builder, List<NameValue> vars) => {
            //    var yqjf1 = vars.FirstOrDefault(p => p.Name == "$UserCustom065$")?.Value;
            //    builder.Write(yqjf1 == "2" ? "√ " : "x ");
            //});
            //result.Add("3、$UserCustom031$", (DocumentBuilder builder, List<NameValue> vars) => {
            //    var yqjf1 = vars.FirstOrDefault(p => p.Name == "$UserCustom065$")?.Value;
            //    builder.Write(yqjf1 == "3" ? "√ " : "x ");
            //});
            //#endregion

            //#region 第十四条 争议的处理方式
            //result.Add("【1】提交", (DocumentBuilder builder, List<NameValue> vars) => {
            //    var zycl = vars.FirstOrDefault(p => p.Name == "$UserCustom300$")?.Value;
            //    builder.MoveTo(builder.CurrentNode.NextSibling);
            //    builder.Font.Color = Color.Red;
            //    builder.Write(zycl == "1" ? "√ " : "x ");
            //});
            //result.Add("【2】依法向人民法院起诉", (DocumentBuilder builder, List<NameValue> vars) => {
            //    var zycl = vars.FirstOrDefault(p => p.Name == "$UserCustom300$")?.Value;
            //    builder.MoveTo(builder.CurrentNode.NextSibling);
            //    builder.Font.Color = Color.Red;
            //    builder.Write(zycl == "2" ? "√ " : "x ");
            //});
            //#endregion
            return result;
        }

        /// <summary>
        /// 解析新word模板，并使用用户变量集合生成新的word文档，并保存到目标文件
        /// </summary>
        /// <param name="src">原word模板文件路径</param>
        /// <param name="dest">保存的目标文件</param>
        /// <param name="vars">模板变量集合</param>
        /// <param name="state">任意用户自定义对象</param>
        /// <param name="defaultSetter">设置未定义变量默认值的方法</param>
        /// <param name="extraHandler">额外操作</param>
        public void NewProcess(string src, string dest, List<NameValue> vars, object state, DefaultValueHandler defaultSetter, ExtraHandler extraHandler = null)
        {
            MemoryStream ms = NewProcess(src, vars, state, defaultSetter, extraHandler);

            using (FileStream stream = new FileStream(dest, FileMode.Create))
            {
                ms.WriteTo(stream);
            }
            ms.Dispose();
        }

        /// <summary>
        /// 解析word模板，并使用用户变量集合生成新的word文档，
        /// 注意使用完释放内存流避免造成内存泄露
        /// </summary>
        /// <param name="src">原word模板文件路径</param>
        /// <param name="vars">模板变量集合</param>
        /// <param name="state">任意用户自定义对象</param>
        /// <param name="defaultSetter">设置未定义变量默认值的方法</param>
        /// <param name="extraHandler">额外操作</param>
        /// <returns>内存流</returns>
        public MemoryStream NewProcess(string src, List<NameValue> vars, object state, DefaultValueHandler defaultSetter, ExtraHandler extraHandler = null)
        {
            Document doc = new Document(src);
            //处理各选项的勾叉
            Dictionary<string, ClauseMatchSectionHandler> clauseHandlers = GetNewClauseHandlers();
            FindReplaceOptions identityOptions = new FindReplaceOptions();
            identityOptions.ReplacingCallback = new IdentityReplacer(doc, vars, clauseHandlers, state);
            foreach (var keypair in clauseHandlers)
            {
                doc.Range.Replace(keypair.Key, string.Empty, identityOptions);
            }

            //查找并替换变量
            FindReplaceOptions varReplaceOptions = new FindReplaceOptions();
            varReplaceOptions.FindWholeWordsOnly = true;
            varReplaceOptions.MatchCase = true;
            varReplaceOptions.ReplacingCallback = new VarReplacer(doc, vars, defaultSetter, state);
            doc.Range.Replace(new Regex(@"\$[a-zA-Z0-9]*\$"), string.Empty, varReplaceOptions);
            //可选的额外操作
            extraHandler?.Invoke(doc, vars);

            //保存结果
            MemoryStream ms = new MemoryStream();
            doc.Save(ms, SaveFormat.Docx);
            return ms;
        }

        /// <summary>
        /// 获取各条款的匹配处理方法
        /// </summary>
        /// <returns></returns>
        private Dictionary<string, ClauseMatchSectionHandler> GetNewClauseHandlers()
        {
            Dictionary<string, ClauseMatchSectionHandler> result = new Dictionary<string, ClauseMatchSectionHandler>();
            #region 第六条 计价方式与价款
            result.Add("1. 按照套内建筑面积计算", (DocumentBuilder builder, List<NameValue> vars) => {
                //第四条 计价方式与价款 1
                var jjfs = vars.FirstOrDefault(p => p.Name == "$ChoosePricingMethod$")?.Value;
                builder.Write("1".Equals(jjfs) ? "√ " : "x ");
            });
            result.Add("2. 按照建筑面积计算", (DocumentBuilder builder, List<NameValue> vars) => {
                //第四条 计价方式与价款 2
                var jjfs = vars.FirstOrDefault(p => p.Name == "$ChoosePricingMethod$")?.Value;
                builder.Write("2".Equals(jjfs) ? "√ " : "x ");
            });
            result.Add("3. 按照套计算", (DocumentBuilder builder, List<NameValue> vars) => {
                //第四条 计价方式与价款 3
                var jjfs = vars.FirstOrDefault(p => p.Name == "$ChoosePricingMethod$")?.Value;
                builder.Write("3".Equals(jjfs) ? "√ " : "x ");
            });
            result.Add("4. 按照$CustomCalcName$计算", (DocumentBuilder builder, List<NameValue> vars) => {
                //第四条 计价方式与价款 3
                var jjfs = vars.FirstOrDefault(p => p.Name == "$ChoosePricingMethod$")?.Value;
                builder.Write("4".Equals(jjfs) ? "√ " : "x ");
            });
            #endregion

            #region 第七条 付款方式及期限
            result.Add("1. 一次性付款", (DocumentBuilder builder, List<NameValue> vars) => {
                var fkfs = vars.FirstOrDefault(p => p.Name == "$FKFS$")?.Value;
                builder.Write("1".Equals(fkfs) ? "√ " : "x ");
            });
            result.Add("2. 分期付款", (DocumentBuilder builder, List<NameValue> vars) => {
                var fkfs = vars.FirstOrDefault(p => p.Name == "$FKFS$")?.Value;
                builder.Write("2".Equals(fkfs) ? "√ " : "x ");
            });
            result.Add("3. 贷款方式付款", (DocumentBuilder builder, List<NameValue> vars) => {
                var fkfs = vars.FirstOrDefault(p => p.Name == "$FKFS$")?.Value;
                builder.Write("3".Equals(fkfs) ? "√ " : "x ");
            });
            result.Add("4. 其他方式", (DocumentBuilder builder, List<NameValue> vars) => {
                var fkfs = vars.FirstOrDefault(p => p.Name == "$FKFS$")?.Value;
                builder.Write("4".Equals(fkfs) ? "√ " : "x ");
            });
            #endregion

            #region 第八条　乙方逾期付款的违约责任
            result.Add("1. $MSRYQCLFS1$", (DocumentBuilder builder, List<NameValue> vars) => {
                var yqfk = vars.FirstOrDefault(p => p.Name == "$OverduePaymentPMOptions$")?.Value;
                builder.Write("1".Equals(yqfk) ? "√ " : "x ");
            });
            result.Add("2. $ContentOfLatePaymentSecond$", (DocumentBuilder builder, List<NameValue> vars) => {
                var yqfk = vars.FirstOrDefault(p => p.Name == "$OverduePaymentPMOptions$")?.Value;
                builder.Write("2".Equals(yqfk) ? "√ " : "x ");
            });
            #endregion

            #region 第十二条	逾期交付责任

            result.Add("1. $CMRYQCLFS1$", (DocumentBuilder builder, List<NameValue> vars) =>
            {
                var yqfk = vars.FirstOrDefault(p => p.Name == "$LateDeliveryOptions$")?.Value;
                builder.Write("1".Equals(yqfk) ? "√ " : "x ");
            });
            result.Add("2. $ContentOfLateDeliveryOptionSecond$", (DocumentBuilder builder, List<NameValue> vars) =>
            {
                var yqfk = vars.FirstOrDefault(p => p.Name == "$LateDeliveryOptions$")?.Value;
                builder.Write("2".Equals(yqfk) ? "√ " : "x ");
            });

            #endregion

            #region 第十三条	面积差异处理

            result.Add("1. 根据第六条按照套内建筑面积计价的约定，双方同意按照下列原则处理", (DocumentBuilder builder, List<NameValue> vars) =>
            {
                var cycl = vars.FirstOrDefault(p => p.Name == "$AreaDiffSelectProcess$")?.Value;
                builder.Write("1".Equals(cycl) ? "√ " : "x ");
            });
            result.Add("2. 根据第六条按照建筑面积计价的约定，双方同意按照下列原则处理", (DocumentBuilder builder, List<NameValue> vars) =>
            {
                var cycl = vars.FirstOrDefault(p => p.Name == "$AreaDiffSelectProcess$")?.Value;
                builder.Write("2".Equals(cycl) ? "√ " : "x ");
            });
            result.Add("3. 根据第六条按照套计价的，出卖人承诺在房屋平面图中标明详细尺寸", (DocumentBuilder builder, List<NameValue> vars) =>
            {
                var cycl = vars.FirstOrDefault(p => p.Name == "$AreaDiffSelectProcess$")?.Value;
                builder.Write("3".Equals(cycl) ? "√ " : "x ");
            });
            result.Add("4. 双方自行约定：", (DocumentBuilder builder, List<NameValue> vars) =>
            {
                var cycl = vars.FirstOrDefault(p => p.Name == "$AreaDiffSelectProcess$")?.Value;
                builder.Write("4".Equals(cycl) ? "√ " : "x ");
            });
            #endregion

            #region 第二十七条 争议的处理方式
            result.Add("1. 依法向房屋所在地人民法院起诉", (DocumentBuilder builder, List<NameValue> vars) =>
            {
                var zycl = vars.FirstOrDefault(p => p.Name == "$DisputeResolutionOptions$")?.Value;
                //builder.MoveTo(builder.CurrentNode.NextSibling);
                //builder.Font.Color = Color.Red;
                builder.Write("1".Equals(zycl) ? "√ " : "x ");
            });
            result.Add("2. 提交$DisputeResolutionOptionsSecond$仲裁委员会仲裁", (DocumentBuilder builder, List<NameValue> vars) =>
            {
                var zycl = vars.FirstOrDefault(p => p.Name == "$DisputeResolutionOptions$")?.Value;
                //builder.MoveTo(builder.CurrentNode.NextSibling);
                //builder.Font.Color = Color.Red;
                builder.Write("2".Equals(zycl) ? "√ " : "x ");
            });
            #endregion

            //#region 第九条　甲方逾期交房的违约责任 第一种方式
            //result.Add("1、解除合同，甲方按乙方已付款的$UserCustom054$%", (DocumentBuilder builder, List<NameValue> vars) => {
            //    var yqjf1 = vars.FirstOrDefault(p => p.Name == "$UserCustom065$")?.Value;
            //    builder.Write(yqjf1 == "1" ? "√ " : "x ");
            //});
            //result.Add("2、甲方按乙方已付款的$UserCustom055$%", (DocumentBuilder builder, List<NameValue> vars) => {
            //    var yqjf1 = vars.FirstOrDefault(p => p.Name == "$UserCustom065$")?.Value;
            //    builder.Write(yqjf1 == "2" ? "√ " : "x ");
            //});
            //result.Add("3、$UserCustom031$", (DocumentBuilder builder, List<NameValue> vars) => {
            //    var yqjf1 = vars.FirstOrDefault(p => p.Name == "$UserCustom065$")?.Value;
            //    builder.Write(yqjf1 == "3" ? "√ " : "x ");
            //});
            //#endregion


            return result;
        }

    }
}
