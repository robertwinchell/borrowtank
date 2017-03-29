using System;
using System.Linq;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;

namespace ASOL.HireThings.Portal
{
    public static class ExtensionMethods
    {
        public static MvcHtmlString IncludeVersionedJs(this HtmlHelper helper, string filename)
        {
            string version = GetVersion(helper, filename);
            return MvcHtmlString.Create("<script type='text/javascript' src='" + filename + version + "'></script>");
        }
        private static string GetVersion(this HtmlHelper helper, string filename)
        {
            var context = helper.ViewContext.RequestContext.HttpContext;

            if (context.Cache[filename] == null)
            {
                var physicalPath = context.Server.MapPath(filename);
                var version = "?v=" +
                  new System.IO.FileInfo(physicalPath).LastWriteTime
                    .ToString("yyyyMMddHHmmss");
                context.Cache.Add(physicalPath, version, null,
                  DateTime.Now.AddMinutes(1), TimeSpan.Zero,
                  CacheItemPriority.Normal, null);
                context.Cache[filename] = version;
                return version;
            }
            else
            {
                return context.Cache[filename] as string;
            }
        }

        public static T GetAttributeFrom<T>(this object instance, string propertyName) where T : Attribute
        {
            var attrType = typeof(T);
            var property = instance.GetType().GetProperty(propertyName);
            // If there's no custom attribute of that type then return null
            if (property.GetCustomAttributes(attrType, false).Count() > 0)
                return (T)property.GetCustomAttributes(attrType, false).First();
            else
                return null;
        }
        public static IHtmlString SortAscMark(this HtmlHelper helper, string content)
        {
            string htmlString = String.Format(" <i class='fa  fa-chevron-up Pointer hidden  ascArrow {0}'></i>", content);
            return new HtmlString(htmlString);
        }
        public static IHtmlString SortDescMark(this HtmlHelper helper, string content)
        {
            string htmlString = String.Format(" <i class='fa  fa-chevron-down Pointer hidden descArrow {0}' ></i>", content);
            return new HtmlString(htmlString);
        }



        


        
    }
  
  
}