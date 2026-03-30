using Assignment3.Models;
using Assignment3.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Assignment3.Controllers;

public class AccountController : Controller
{
    private readonly IUserRepository _userRepo;

    public AccountController(IUserRepository userRepo)
    {
        _userRepo = userRepo;
    }

    [HttpGet]
    public IActionResult Login() => View();

    [HttpPost]
    public IActionResult Login(LoginViewModel model)
    {
        if (!ModelState.IsValid) return View(model);

        var user = _userRepo.Authenticate(model.Username, model.Password);
        if (user == null)
        {
            ModelState.AddModelError(string.Empty, "Invalid username or password.");
            return View(model);
        }

        HttpContext.Session.SetString("Username", user.Username);
        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public IActionResult Register() => View();

    [HttpPost]
    public IActionResult Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid) return View(model);

        if (_userRepo.UsernameExists(model.Username))
        {
            ModelState.AddModelError("Username", "Username already taken.");
            return View(model);
        }

        _userRepo.Register(model);
        TempData["Success"] = "Registration successful! Please log in.";
        return RedirectToAction(nameof(Login));
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction(nameof(Login));
    }
}
