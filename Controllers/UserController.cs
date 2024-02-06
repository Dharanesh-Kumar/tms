using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Task_Management.Models;

namespace Task_Management.Controllers;

public class UserController : Controller
{
[HttpGet]
    public IActionResult _Login()
    {
        return View();
    }
    [HttpPost]
   public IActionResult _Login(UserModel user)
    {
        
        int check = Repository.Login(user);
        if(check ==1){

            HttpContext.Session.SetString("ID",user.AceId);

            return RedirectToAction("Welcome","UserTask");
        }
        else{
            
            ViewBag.message = "*Invalid AceId or Password";
            return View("_Login");
        }
    }
    [HttpGet]
    public IActionResult SignUp()
    {
        return View();
    }
    [HttpPost]
    public IActionResult SignUp(UserModel user)
    {
        int check = Repository.SignUp(user);
        Console.WriteLine(check);
        if (check == 1)
        {
            ViewBag.Message = "*Invalid Username or Password";
            return View("SignUp");
        }
        else
        {
            return View("_Login");
        }
    }
    [HttpGet]
    public IActionResult ForgetPass()
    {
        return View();
    }
    [HttpPatch]
    public IActionResult ForgetPass(UserModel user)
    {
        int check = Repository.ForgetPass(user);
        if(check ==1){
            ViewBag.Message = "*Invalid AceId";
            return View("ForgetPass");
        }
        else{
            return View("_Login");
        }
        
    }
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
