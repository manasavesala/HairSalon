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

      [HttpGet("/stylists/{id}/details")]
      public ActionResult Details(int id)
      {
        Stylist currentStylist = Stylist.Find(id);  
        return View(currentStylist);
      }

      [HttpGet("/stylists/{id}")]
      public ActionResult Show(int id)
      { 
        Dictionary<string, object> model = new Dictionary<string, object>();
        Stylist selectedStylist = Stylist.Find(id);
        List<Client> StylistClients = selectedStylist.GetClients();
        model.Add("stylist", selectedStylist);
        model.Add("clients", StylistClients);
        return View(model);
      } 

      [HttpGet("/stylists/{id}/delete")]
      public ActionResult Delete(int id)
      {
        Stylist.DeleteStylist(id);
        List<Stylist> allStylists = Stylist.GetAll();
        return View("Index",allStylists);
      }  

      [HttpGet("stylists/{id}/edit")]
      public ActionResult Edit(int id)
      {
        Stylist currentStylist = Stylist.Find(id);  
        return View(currentStylist);
      }

      [HttpPost("/stylists/{id}/edit")]
      public ActionResult Update(string name,string location, string rating,int id)
      {
        Stylist stylist = Stylist.Find(id);
        stylist.Edit(name, location,rating);
        List<Stylist> allStylists = Stylist.GetAll();
        return View("Index", allStylists);
      }               

  }
}
