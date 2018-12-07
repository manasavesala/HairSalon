using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;

namespace HairSalon.Controllers
{
    public class StylistController : Controller
    {
      [HttpGet("/stylists")]
      public ActionResult Index()
      {
        List<Stylist> allStylists = Stylist.GetAll();
        return View(allStylists);
      }

      [HttpGet("/stylists/new")]
      public ActionResult New()
      { 
        return View();
      }

      [HttpPost("/stylists/new")]
      public ActionResult Create(string stylistname, string location, string rating)
      {
        Stylist newStylist = new Stylist( stylistname, location, rating);
        newStylist.Save();
        List<Stylist> allStylists = Stylist.GetAll();  
        return View("Index", allStylists);
      }   

      [HttpGet("/stylists/{id}")]
      public ActionResult Show(int id)
      {
        Stylist currentStylist = Stylist.Find(id);  
        return View(currentStylist);
      }

    }
}
