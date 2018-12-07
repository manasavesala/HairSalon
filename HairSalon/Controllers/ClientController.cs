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
      List<Client> allClients = Client.GetAll();
      return View(allClients);
    }

    [HttpGet("/clients/new")]
    public ActionResult New()
    { 
      return View();
    }

    [HttpPost("/clients/new")]
    public ActionResult Create(string name, string stylistName)
    {
      Client newClient = new Client( name, stylistName);
      newClient.Save();
      List<Client> allClients = Client.GetAll();  
      return View("Index", allClients);
    }   

    [HttpGet("/clients/{id}")]
    public ActionResult Show(int id)
    {
      Client currentClient = Client.Find(id);  
      return View(currentClient);
    }

    [HttpGet("/clients/{id}/delete")]
    public ActionResult Delete(int id)
    {
      Client.DeleteClient(id);
      List<Client> allClients = Client.GetAll();
      return View("Index",allClients);
  
    }

  }
}
