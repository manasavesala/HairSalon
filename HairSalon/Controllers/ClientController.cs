using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;

namespace HairSalon.Controllers
{
    public class ClientController : Controller
    {
      [HttpGet("/clients")]
      public ActionResult Index()
      {
        return View();
      }
    }
}
