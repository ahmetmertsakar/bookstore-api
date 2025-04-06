using BookStoreAPI_Assessment.Data;
using BookStoreAPI_Assessment.Models;
using BookStoreAPI_Assessment.Requests;
using BookStoreAPI_Assessment.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStoreAPI_Assessment.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController(BookStoreContext context) : ControllerBase
{
    private readonly BookStoreContext _context = context;

    // GET /api/books - List all books
    [HttpGet]
    public async Task<IActionResult> GetAllBooks()
    {
        var books = await _context.Books
        .Include(b => b.Category)
        .Select(b => new BookResponse
        {
            Id = b.Id,
            Title = b.Title,
            Author = b.Author,
            ISBN = b.ISBN,
            Price = b.Price,
            StockQuantity = b.StockQuantity,
            PublicationYear = b.PublicationYear,
            CategoryName = b.Category.Name
        })
        .ToListAsync();

        return Ok(books);
    }

    // GET /api/books/{id} - Get book details
    [HttpGet("{id}")]
    public async Task<IActionResult> GetBookById(int id)
    {
        var book = await _context.Books
        .Include(b => b.Category)
        .Where(b => b.Id == id)
        .Select(b => new BookResponse
        {
            Id = b.Id,
            Title = b.Title,
            Author = b.Author,
            ISBN = b.ISBN,
            Price = b.Price,
            StockQuantity = b.StockQuantity,
            PublicationYear = b.PublicationYear,
            CategoryName = b.Category.Name
        })
        .FirstOrDefaultAsync();

        if (book == null)
            return NotFound("Book not found.");

        return Ok(book);
    }

    // POST /api/books - Add new book
    [HttpPost]
    public async Task<IActionResult> CreateBook([FromBody] CreateBookRequest request)
    {
        var category = await _context.Categories.FindAsync(request.CategoryId);
        if (category == null) return BadRequest("Invalid input data. Invalid Category ID.");

        var book = new Book
        {
            Title = request.Title,
            Author = request.Author,
            ISBN = request.ISBN,
            Price = request.Price,
            StockQuantity = request.StockQuantity,
            CategoryId = request.CategoryId,
            PublicationYear = request.PublicationYear
        };

        _context.Books.Add(book);
        await _context.SaveChangesAsync();

        var response = new BookResponse
        {
            Id = book.Id,
            Title = book.Title,
            Author = book.Author,
            ISBN = book.ISBN,
            Price = book.Price,
            StockQuantity = book.StockQuantity,
            PublicationYear = book.PublicationYear,
            CategoryName = category.Name
        };

        return CreatedAtAction(nameof(GetBookById), new { id = book.Id }, response);
    }

    // GET /api/books/search?title={title} - Search books
    [HttpGet("search")]
    public async Task<IActionResult> SearchBooksByTitle([FromQuery] string title)
    {
        var books = await _context.Books
            .Include(b => b.Category)
            .Where(b => b.Title.ToLower().Contains(title.ToLower()))
            .Select(b => new BookResponse
            {
                Id = b.Id,
                Title = b.Title,
                Author = b.Author,
                ISBN = b.ISBN,
                Price = b.Price,
                StockQuantity = b.StockQuantity,
                PublicationYear = b.PublicationYear,
                CategoryName = b.Category.Name
            })
            .ToListAsync();

        return Ok(books);
    }
}