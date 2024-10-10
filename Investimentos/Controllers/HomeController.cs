using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Investimentos.Controllers
{
  public class HomeController : Controller
  {
    public ActionResult Cripto()
    {
      return RedirectToAction("Cripto", "Finance");
    }

    public ActionResult Stock()
    {
      return RedirectToAction("Index", "Finance");
    }

    public ActionResult Favorites()
    {
      ViewBag.Message = "Your contact page.";

      return View();
    }
  }
}