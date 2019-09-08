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
        public static MvcHtmlString CreateInput(this HtmlHelper html, object htmlAttributes = null)
        {
            TagBuilder inp = new TagBuilder("input");
            inp.MergeAttribute("class", "form-control text-box single-line");
            inp.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
            return MvcHtmlString.Create(inp.ToString());
        }
        public static MvcHtmlString CreateLabel(this HtmlHelper html, object htmlAttributes = null)
        {
            TagBuilder lb = new TagBuilder("label");
            lb.MergeAttribute("class", "control-label col-md-2");
            lb.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
            return MvcHtmlString.Create(lb.ToString());
        }
    }
}