using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Investimentos
{
  public class RouteConfig
  {
    public static void RegisterRoutes(RouteCollection routes)
    {
      routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

      routes.MapRoute(
          name: "Default",
          url: "{controller}/{action}/{id}",
          defaults: new { controller = "Home", action = "Cripto", id = UrlParameter.Optional }
      );

      routes.MapRoute(
        name: "Finance",
        url: "Finance/GetStockData/{symbol}",
        defaults: new { controller = "Finance", action = "Stock", symbol = UrlParameter.Optional }
    );
    }
  }
}
