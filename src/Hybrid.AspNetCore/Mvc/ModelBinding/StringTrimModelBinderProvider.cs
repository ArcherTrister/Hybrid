using Hybrid.Data;

using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace OSharp.AspNetCore.Mvc.ModelBinding
{
    /// <summary>
    /// <see cref="StringTrimModelBinder"/>提供者，提供对字符串前后空白进行Trim操作的模型绑定能力
    /// </summary>
    public class StringTrimModelBinderProvider : IModelBinderProvider
    {
        /// <summary>
        /// Creates a <see cref="T:Microsoft.AspNetCore.Mvc.ModelBinding.IModelBinder" /> based on <see cref="T:Microsoft.AspNetCore.Mvc.ModelBinding.ModelBinderProviderContext" />.
        /// </summary>
        /// <param name="context">The <see cref="T:Microsoft.AspNetCore.Mvc.ModelBinding.ModelBinderProviderContext" />.</param>
        /// <returns>An <see cref="T:Microsoft.AspNetCore.Mvc.ModelBinding.IModelBinder" />.</returns>
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            Check.NotNull(context, nameof(context));

            if (context.Metadata.UnderlyingOrModelType == typeof(string))
            {
                return new StringTrimModelBinder();
            }
            return null;
        }
    }
}