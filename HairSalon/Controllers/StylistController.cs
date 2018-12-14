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
        Dictionary<string,object> model= new Dictionary<string,object>{};
        Stylist currentStylist = Stylist.Find(id);  
        List<Specialty> allSpecialties = Specialty.GetAll();
        List<Specialty> specialtiesOfStylist = currentStylist.FindSpecialtiesOfStylist(id);
        model.Add("specialties",specialtiesOfStylist);
        model.Add("stylist",currentStylist);
        return View(model);
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

      [HttpGet("/stylists/assign")]
      public ActionResult Assign()
      {
          Dictionary<string, object> model = new Dictionary<string, object> { };
          List<Stylist> allStylists = Stylist.GetAll();
          List<Specialty> allSpecialties = Specialty.GetAll();
          model.Add("stylists", allStylists);
          model.Add("Specialties", allSpecialties);
          return View(model);
      }

      [HttpPost("/stylists/assign")]
      public ActionResult Specialize(int specialty, int stylist)
      {
        Stylist selectedStylist = Stylist.Find(stylist);
        selectedStylist.AddSpecialty(specialty, stylist);
        return RedirectToAction("Index");
      }

      [HttpGet("stylist/{id}/specialties")]
      public ActionResult view(int id)
      {
        Stylist selectedStylist = Stylist.Find(id);
        List<Specialty> specialtiesOfStylist = selectedStylist.FindSpecialtiesOfStylist(id);
        return RedirectToAction("Details");
      }

  }
}
