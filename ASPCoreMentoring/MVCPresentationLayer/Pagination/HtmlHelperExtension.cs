using System;
using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MVCPresentationLayer.Pagination
{
    public static class HtmlHelperExtension
    {
        public static IHtmlContent PagedLinks(this IHtmlHelper html, PagingInfo pagingInfo, Func<int, string> pageUrl)
        {
            var result = new StringBuilder();
            var anchorInnerHtml = String.Empty;

            for (int i = 1; i <= pagingInfo.TotalPages; i++)
            {
                var tag = new TagBuilder("a");
                anchorInnerHtml = AnchorInnerHtml(i, pagingInfo);
                tag.MergeAttribute("href", anchorInnerHtml == ".." ? "#" : pageUrl(i));
                tag.InnerHtml.AppendHtml(anchorInnerHtml);
                if (i == pagingInfo.CurrentPage)
                {
                    tag.AddCssClass("btn btn-light active");
                }

                tag.AddCssClass("btn btn-light");
                if (anchorInnerHtml != String.Empty)
                {
                    var writer = new System.IO.StringWriter();
                    tag.WriteTo(writer, HtmlEncoder.Default);
                    result.Append(writer);
                }
            }

            return new HtmlString(result.ToString());
        }

        public static string AnchorInnerHtml(int i, PagingInfo pagingInfo)
        {
            var anchorInnerHtml = String.Empty;

            if (pagingInfo.TotalPages <= 10)
                anchorInnerHtml = i.ToString();
            else
            {
                if (pagingInfo.CurrentPage <= 5)
                {
                    if ((i <= 8) || (i == pagingInfo.TotalPages))
                        anchorInnerHtml = i.ToString();
                    else if (i == pagingInfo.TotalPages - 1)
                        anchorInnerHtml = "..";
                }
                else if ((pagingInfo.CurrentPage > 5) && (pagingInfo.TotalPages - pagingInfo.CurrentPage >= 5))
                {
                    if ((i == 1) || (i == pagingInfo.TotalPages) || ((pagingInfo.CurrentPage - i >= -3) && (pagingInfo.CurrentPage - i <= 3)))
                        anchorInnerHtml = i.ToString();
                    else if ((i == pagingInfo.CurrentPage - 4) || (i == pagingInfo.CurrentPage + 4))
                        anchorInnerHtml = "..";
                }
                else if (pagingInfo.TotalPages - pagingInfo.CurrentPage < 5)
                {
                    if ((i == 1) || (pagingInfo.TotalPages - i <= 7))
                        anchorInnerHtml = i.ToString();
                    else if (pagingInfo.TotalPages - i == 8)
                        anchorInnerHtml = "..";
                }
            }
            return anchorInnerHtml;
        }
    }
}
