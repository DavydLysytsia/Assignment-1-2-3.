using Assignment3.Models;
using Assignment3.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Assignment3.Controllers;

public class BorrowingController : Controller
{
    private readonly IBorrowingRepository _repo;
    private readonly IBookRepository _bookRepo;
    private readonly IReaderRepository _readerRepo;

    public BorrowingController(IBorrowingRepository repo, IBookRepository bookRepo, IReaderRepository readerRepo)
    {
        _repo       = repo;
        _bookRepo   = bookRepo;
        _readerRepo = readerRepo;
    }

    private bool IsLoggedIn() => HttpContext.Session.GetString("Username") != null;

    private void EnrichBorrowing(Borrowing b)
    {
        b.BookTitle  = _bookRepo.GetById(b.BookId)?.Title   ?? "Unknown";
        b.ReaderName = _readerRepo.GetById(b.ReaderId)?.Name ?? "Unknown";
    }

    public IActionResult Index()
    {
        if (!IsLoggedIn()) return RedirectToAction("Login", "Account");
        var borrowings = _repo.GetAll().ToList();
        borrowings.ForEach(EnrichBorrowing);
        return View(borrowings);
    }

    public IActionResult Details(int id)
    {
        if (!IsLoggedIn()) return RedirectToAction("Login", "Account");
        var borrowing = _repo.GetById(id);
        if (borrowing == null) return NotFound();
        EnrichBorrowing(borrowing);
        return View(borrowing);
    }

    [HttpGet]
    public IActionResult Create()
    {
        if (!IsLoggedIn()) return RedirectToAction("Login", "Account");
        PopulateDropdowns();
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Borrowing borrowing)
    {
        if (!ModelState.IsValid)
        {
            PopulateDropdowns();
            return View(borrowing);
        }

        // Mark book as unavailable
        var book = _bookRepo.GetById(borrowing.BookId);
        if (book != null) { book.IsAvailable = false; _bookRepo.Update(book); }

        _repo.Create(borrowing);
        TempData["Success"] = "Borrowing created successfully.";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        if (!IsLoggedIn()) return RedirectToAction("Login", "Account");
        var borrowing = _repo.GetById(id);
        if (borrowing == null) return NotFound();
        PopulateDropdowns();
        return View(borrowing);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Borrowing borrowing)
    {
        if (!ModelState.IsValid)
        {
            PopulateDropdowns();
            return View(borrowing);
        }

        // If returned, mark book available
        if (borrowing.IsReturned)
        {
            var book = _bookRepo.GetById(borrowing.BookId);
            if (book != null) { book.IsAvailable = true; _bookRepo.Update(book); }
        }

        _repo.Update(borrowing);
        TempData["Success"] = "Borrowing updated successfully.";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult Delete(int id)
    {
        if (!IsLoggedIn()) return RedirectToAction("Login", "Account");
        var borrowing = _repo.GetById(id);
        if (borrowing == null) return NotFound();
        EnrichBorrowing(borrowing);
        return View(borrowing);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        _repo.Delete(id);
        TempData["Success"] = "Borrowing deleted successfully.";
        return RedirectToAction(nameof(Index));
    }

    private void PopulateDropdowns()
    {
        ViewBag.Books   = new SelectList(_bookRepo.GetAll().Where(b => b.IsAvailable), "Id", "Title");
        ViewBag.Readers = new SelectList(_readerRepo.GetAll(), "Id", "Name");
    }
}
