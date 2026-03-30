using Assignment3.Models;
using Assignment3.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Assignment3.Controllers;

public class ReaderController : Controller
{
    private readonly IReaderRepository _repo;

    public ReaderController(IReaderRepository repo) => _repo = repo;

    private bool IsLoggedIn() => HttpContext.Session.GetString("Username") != null;

    public IActionResult Index()
    {
        if (!IsLoggedIn()) return RedirectToAction("Login", "Account");
        return View(_repo.GetAll());
    }

    public IActionResult Details(int id)
    {
        if (!IsLoggedIn()) return RedirectToAction("Login", "Account");
        var reader = _repo.GetById(id);
        if (reader == null) return NotFound();
        return View(reader);
    }

    [HttpGet]
    public IActionResult Create()
    {
        if (!IsLoggedIn()) return RedirectToAction("Login", "Account");
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Reader reader)
    {
        if (!ModelState.IsValid) return View(reader);
        _repo.Create(reader);
        TempData["Success"] = $"Reader \"{reader.Name}\" added successfully.";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        if (!IsLoggedIn()) return RedirectToAction("Login", "Account");
        var reader = _repo.GetById(id);
        if (reader == null) return NotFound();
        return View(reader);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Reader reader)
    {
        if (!ModelState.IsValid) return View(reader);
        var updated = _repo.Update(reader);
        if (updated == null) return NotFound();
        TempData["Success"] = $"Reader \"{reader.Name}\" updated successfully.";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult Delete(int id)
    {
        if (!IsLoggedIn()) return RedirectToAction("Login", "Account");
        var reader = _repo.GetById(id);
        if (reader == null) return NotFound();
        return View(reader);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        _repo.Delete(id);
        TempData["Success"] = "Reader deleted successfully.";
        return RedirectToAction(nameof(Index));
    }
}
