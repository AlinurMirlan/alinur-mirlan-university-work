using Flash.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Flash.Infrastructure.TagHelpers
{
    [HtmlTargetElement(Attributes = "pagination")]
    public class PaginationTagHelper : TagHelper
    {
        private readonly IUrlHelperFactory _urlHelperFactory;

        public PaginationTagHelper(IUrlHelperFactory urlHelperFactory)
        {
            _urlHelperFactory = urlHelperFactory;
        }

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; } = default!;

        public Pagination? Pagination { get; set; }

        public string? SearchTerm { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (Pagination is null)
                return;

            IUrlHelper urlHelper = _urlHelperFactory.GetUrlHelper(ViewContext);
            const string pageNumber = "pageNumber";
            Dictionary<string, object> routeValues = 
                string.IsNullOrEmpty(SearchTerm) ?
                new() { { pageNumber, 0 } } :
                new() { { pageNumber, 0 }, { "searchTerm", SearchTerm } };
            for (int i = 1; i <= Pagination.PageCount; i++)
            {
                TagBuilder anchor = new("a");
                anchor.AddCssClass("page-number");
                anchor.InnerHtml.Append(i.ToString());
                routeValues[pageNumber] = i;
                anchor.Attributes["href"] = urlHelper.Page(Pagination.ReturnPage, routeValues);
                if (Pagination.CurrentPage == i)
                {
                    anchor.AddCssClass("page-chosen");
                }
                output.Content.AppendHtml(anchor);
            }
        }
    }
}
