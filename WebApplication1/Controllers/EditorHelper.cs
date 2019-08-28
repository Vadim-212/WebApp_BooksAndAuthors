using System.Web;
using System.Web.Optimization;
using System.Web.Mvc;
using System.Linq.Expressions;
using System;

namespace WebApplication1
{
    public static class EditorHelper
    {
        public static MvcHtmlString Custom_Editor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, object additionalViewData)
        {
            //string customElement = "<input style=\"background-color:yellow;\" type=\"text\" value=\"" + expression + "\" name=\"\" id=\"\" />";
            //return new MvcHtmlString(customElement);

            var fieldName = ExpressionHelper.GetExpressionText(expression);
            var fullBindingName = html.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(fieldName);
            var fieldId = TagBuilder.CreateSanitizedId(fullBindingName);

            var metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            var value = metadata.Model;

            TagBuilder tag = new TagBuilder("input");
            tag.Attributes.Add("name", fullBindingName);
            tag.Attributes.Add("id", fieldId);
            tag.Attributes.Add("type", "datetime-local");
            tag.Attributes.Add("value", value == null ? "" : value.ToString());

            var validationAttributes = html.GetUnobtrusiveValidationAttributes(fullBindingName, metadata);
            foreach (var key in validationAttributes.Keys)
            {
                tag.Attributes.Add(key, validationAttributes[key].ToString());
            }
            return new MvcHtmlString(tag.ToString(TagRenderMode.SelfClosing));
        }
    }
}
