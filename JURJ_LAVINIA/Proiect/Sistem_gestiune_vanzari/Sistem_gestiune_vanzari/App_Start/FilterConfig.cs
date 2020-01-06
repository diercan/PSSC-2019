using System.Web;
using System.Web.Mvc;

namespace Sistem_gestiune_vanzari
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
