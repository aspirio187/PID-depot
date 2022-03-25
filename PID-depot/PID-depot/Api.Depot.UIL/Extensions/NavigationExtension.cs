using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

namespace Api.Depot.UIL.Extensions
{
    public static class NavigationExtension
    {
        public static string ActiveClass(this IHtmlHelper htmlHelper, string page = null, string area = null, string cssClass = "active")
        {
            string currentPage = htmlHelper?.ViewContext.RouteData.Values["Page"] as string;
            string currentArea = htmlHelper?.ViewContext.RouteData.Values["Area"] as string;

            string[] acceptedPage = (page ?? currentPage ?? "").Split(',');
            string[] acceptedArea = (area ?? currentArea ?? "").Split(',');

            return (acceptedPage.Contains(currentPage) && (string.IsNullOrEmpty(area) ? true : acceptedArea.Contains(currentArea)))
                ? cssClass
                : "";
        }
    }
}
