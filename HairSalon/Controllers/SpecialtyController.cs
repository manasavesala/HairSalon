using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;

namespace HairSalon.Controllers
{
  public class SpecialtyController : Controller
  {
  
    [HttpGet("/specialties")]
    public ActionResult Index()
    {
      List<Specialty> allspecialties = Specialty.GetAll();
      return View(allspecialties);
    }

    [HttpGet("/specialties/new")]
    public ActionResult New()
    { 
      return View();
    }

    [HttpPost("/specialties/new")]
    public ActionResult Create(string name)
    {
      Specialty newSpecialty = new Specialty( name);
      newSpecialty.Save();
      List<Specialty> allspecialties = Specialty.GetAll();  
      return View("Index", allspecialties);
    }   

    [HttpGet("/specialties/{id}")]
    public ActionResult Show(int id)
    {
      Specialty currentSpecialty = Specialty.Find(id);  
      return View(currentSpecialty);
    }

    [HttpGet("/specialties/{id}/delete")]
    public ActionResult Delete(int id)
    {
      Specialty.DeleteSpecialty(id);
      List<Specialty> allspecialties = Specialty.GetAll();
      return View("Index",allspecialties);
  
    }

    [HttpGet("specialty/{id}/stylists")]
    public ActionResult stylists(int id )
    {
      List<Stylist> selectedStylists = Stylist.FindStylistsOfStylist(id);
      return View("Details",selectedStylists); 
    }

  }
}
