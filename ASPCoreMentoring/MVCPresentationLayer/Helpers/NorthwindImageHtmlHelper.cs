using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.Encodings.Web;

namespace MVCPresentationLayer.Helpers
{
    public static class NorthwindImageHtmlHelper
    {
        public static HtmlString NorthwindImageLink(this IHtmlHelper html, string imageId, string linkText)
        {
            var tag = new TagBuilder("a");

            tag.Attributes.Add("href", $"images/{imageId}");
            tag.InnerHtml.Append(linkText);

            var writer = new System.IO.StringWriter();
            tag.WriteTo(writer, HtmlEncoder.Default);

            return new HtmlString(writer.ToString());
        }
    }
}
