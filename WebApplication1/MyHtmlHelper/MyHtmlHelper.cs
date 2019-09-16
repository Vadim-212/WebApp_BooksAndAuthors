using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;
using System.Web.Mvc;

namespace WebApplication1.MyHtmlHelper
{
    public static class MyHtmlHelper
    {
        public static MvcHtmlString CreateSubmitBtn(this HtmlHelper html)
        {
            TagBuilder inp = new TagBuilder("input");
            inp.MergeAttribute("class", "btn btn-default btn-light");
            inp.MergeAttribute("type", "submit");
            inp.MergeAttribute("value", "Save");
            return MvcHtmlString.Create(inp.ToString());
        }
        public static MvcHtmlString CreateSubmitBtn(this HtmlHelper html, object htmlAttributes)
        {
            TagBuilder inp = new TagBuilder("input");
            inp.MergeAttribute("class", "form-control text-box single-line");
            inp.MergeAttribute("value", "Save");
            if (htmlAttributes != null)
                inp.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
            inp.MergeAttribute("type", "submit");
            return MvcHtmlString.Create(inp.ToString());
        }
        public static MvcHtmlString CreateSubmitBtn(this HtmlHelper html, object htmlAttributes, string templateText)
        {
            TagBuilder inp = new TagBuilder("input");
            inp.MergeAttribute("class", "form-control text-box single-line");
            if (htmlAttributes != null)
                inp.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
            if (templateText != null)
                inp.MergeAttribute("value", templateText);
            else
                inp.MergeAttribute("value", "Save");
            inp.MergeAttribute("type", "submit");
            return MvcHtmlString.Create(inp.ToString());
        }


        public static MvcHtmlString CreateLabel(this HtmlHelper html)
        {
            TagBuilder lb = new TagBuilder("label");
            lb.MergeAttribute("class", "control-label col-md-2");
            return MvcHtmlString.Create(lb.ToString());
        }
        public static MvcHtmlString CreateLabel(this HtmlHelper html, object htmlAttributes)
        {
            TagBuilder lb = new TagBuilder("label");
            lb.MergeAttribute("class", "control-label col-md-2");
            if (htmlAttributes != null)
                lb.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
            return MvcHtmlString.Create(lb.ToString());
        }
        public static MvcHtmlString CreateLabel(this HtmlHelper html, object htmlAttributes, string labelText)
        {
            TagBuilder lb = new TagBuilder("label");
            lb.MergeAttribute("class", "control-label col-md-2");
            if (htmlAttributes != null)
                lb.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
            lb.SetInnerText(labelText);
            return MvcHtmlString.Create(lb.ToString());
        }
    }
}