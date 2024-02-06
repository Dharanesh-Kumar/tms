using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Task_Management.Models;

namespace Task_Management.Controllers;

public class UserTaskController : Controller
{
    public IActionResult Welcome()
    {
        return View();
    }
    [HttpGet]
    public IActionResult TaskSubmit()
    {       
        return View();
    }
    [HttpPost]
    public IActionResult TaskSubmit(UserTask submit)
    {  
        String aceId = HttpContext.Session.GetString("ID");
        Repository1.TaskSubmit(submit, aceId);
        return  View("Welcome");
    }
    public IActionResult MyTask()
    {
        return View();
    }
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index", "Home");
    }
      [HttpGet]
    public IActionResult ViewSubmit()
    {
        string username = HttpContext.Session.GetString("ID");
        var Tickets = Repository1.ViewSubmit(username);
        return View(Tickets);
    }
    
}