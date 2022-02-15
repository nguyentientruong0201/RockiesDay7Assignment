using System.Net.Http;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MVCDay7.Models;

namespace MVCDay7.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        Set("TruongCookies", "This is my cookies",10);
        HttpContext.Session.SetString("TruongSession", "this is first mvc session");
        return View();
    }

    public IActionResult Privacy()
    {
        var result = Get("ThanksCookies");
        var sessionValue = HttpContext.Session.GetString("Truong session");
        ViewBag.ThanhTest =  result;
        ViewBag.Truongtest1 = "this is truong test 1";
        ViewBag.Truongtest3 = sessionValue;
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    private void Set(string key, string value, int? expireTime)
    {
        CookieOptions options = new CookieOptions();
        if(expireTime.HasValue)
            options.Expires = DateTime.Now.AddMinutes(expireTime.Value);
        else 
            options.Expires = DateTime.Now.AddSeconds(30);
        Response.Cookies.Append(key,value,options);        
    }

    private string Get(string key)
    {
        return Request.Cookies[key];
    }


    private void Remove(string key)
    {
        Response.Cookies.Delete(key);
    }
}
