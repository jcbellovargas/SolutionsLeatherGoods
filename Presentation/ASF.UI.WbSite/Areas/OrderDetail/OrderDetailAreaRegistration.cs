using System.Web.Mvc;

namespace ASF.UI.WbSite.Areas.OrderDetail
{
    public class OrderDetailAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "OrderDetail";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "OrderDetail_default",
                "OrderDetail/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}