using Assignment3.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Assignment3.Controllers;

public class HomeController : Controller
{
    private readonly IBookRepository _bookRepo;
    private readonly IReaderRepository _readerRepo;
    private readonly IBorrowingRepository _borrowingRepo;

    public HomeController(IBookRepository bookRepo, IReaderRepository readerRepo, IBorrowingRepository borrowingRepo)
    {
        _bookRepo     = bookRepo;
        _readerRepo   = readerRepo;
        _borrowingRepo = borrowingRepo;
    }

    public IActionResult Index()
    {
        if (HttpContext.Session.GetString("Username") == null)
            return RedirectToAction("Login", "Account");

        var borrowings = _borrowingRepo.GetAll().ToList();

        ViewBag.TotalBooks      = _bookRepo.GetAll().Count();
        ViewBag.AvailableBooks  = _bookRepo.GetAll().Count(b => b.IsAvailable);
        ViewBag.TotalReaders    = _readerRepo.GetAll().Count();
        ViewBag.ActiveBorrows   = borrowings.Count(b => !b.IsReturned);
        ViewBag.OverdueCount    = borrowings.Count(b => !b.IsReturned && b.DueDate < DateTime.Today);

        return View();
    }

    public IActionResult Error() => View();
}
