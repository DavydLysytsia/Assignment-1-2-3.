using Assignment3.Models;
using Assignment3.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Assignment3.Controllers;

public class BookController : Controller
{
    private readonly IBookRepository _repo;

    public BookController(IBookRepository repo) => _repo = repo;

    private bool IsLoggedIn() => HttpContext.Session.GetString("Username") != null;

    public IActionResult Index()
    {
        if (!IsLoggedIn()) return RedirectToAction("Login", "Account");
        return View(_repo.GetAll());
    }

    public IActionResult Details(int id)
    {
        if (!IsLoggedIn()) return RedirectToAction("Login", "Account");
        var book = _repo.GetById(id);
        if (book == null) return NotFound();
        return View(book);
    }

    [HttpGet]
    public IActionResult Create()
    {
        if (!IsLoggedIn()) return RedirectToAction("Login", "Account");
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Book book)
    {
        if (!ModelState.IsValid) return View(book);
        _repo.Create(book);
        TempData["Success"] = $"Book \"{book.Title}\" added successfully.";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        if (!IsLoggedIn()) return RedirectToAction("Login", "Account");
        var book = _repo.GetById(id);
        if (book == null) return NotFound();
        return View(book);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Book book)
    {
        if (!ModelState.IsValid) return View(book);
        var updated = _repo.Update(book);
        if (updated == null) return NotFound();
        TempData["Success"] = $"Book \"{book.Title}\" updated successfully.";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult Delete(int id)
    {
        if (!IsLoggedIn()) return RedirectToAction("Login", "Account");
        var book = _repo.GetById(id);
        if (book == null) return NotFound();
        return View(book);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        _repo.Delete(id);
        TempData["Success"] = "Book deleted successfully.";
        return RedirectToAction(nameof(Index));
    }
}
