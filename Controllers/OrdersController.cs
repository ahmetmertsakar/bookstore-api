using BookStoreAPI_Assessment.Data;
using BookStoreAPI_Assessment.Models;
using BookStoreAPI_Assessment.Requests;
using BookStoreAPI_Assessment.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStoreAPI_Assessment.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController(BookStoreContext context) : ControllerBase
{
    private readonly BookStoreContext _context = context;

    // POST /api/orders - Create order
    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest request)
    {
        var book = await _context.Books.FindAsync(request.BookId);
        if (book == null) return NotFound("Book not found.");

        if (request.Quantity <= 0)
            return BadRequest("Invalid order data. Quantity must be greater than zero.");

        if (book.StockQuantity < request.Quantity)
            return Conflict("Insufficient stock.");

        book.StockQuantity -= request.Quantity;

        var order = new Order
        {
            BookId = request.BookId,
            Quantity = request.Quantity,
            TotalPrice = book.Price * request.Quantity,
            OrderDate = DateTime.UtcNow,
            Status = OrderStatus.PENDING
        };

        _context.Orders.Add(order);
        await _context.SaveChangesAsync();

        var response = new OrderResponse
        {
            Id = order.Id,
            BookTitle = book.Title,
            Quantity = order.Quantity,
            TotalPrice = order.TotalPrice,
            OrderDate = order.OrderDate,
            Status = order.Status.ToString()
        };

        return CreatedAtAction(nameof(GetOrderById), new { id = order.Id }, response);
    }

    // GET /api/orders/{id} - Get order details
    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrderById(int id)
    {
        var order = await _context.Orders
            .Include(o => o.Book)
            .FirstOrDefaultAsync(o => o.Id == id);

        if (order == null) return NotFound("Order not found.");

        var response = new OrderResponse
        {
            Id = order.Id,
            BookTitle = order.Book.Title,
            Quantity = order.Quantity,
            TotalPrice = order.TotalPrice,
            OrderDate = order.OrderDate,
            Status = order.Status.ToString()
        };

        return Ok(response);
    }

    // PUT /api/orders/{id}/status - Update order status
    [HttpPut("{id}/status")]
    public async Task<IActionResult> UpdateOrderStatus(int id, [FromBody] UpdateOrderStatusRequest request)
    {
        var order = await _context.Orders
            .Include(o => o.Book)
            .FirstOrDefaultAsync(o => o.Id == id);
        if (order == null) return NotFound("Order not found.");

        order.Status = request.Status;
        await _context.SaveChangesAsync();

        var response = new OrderResponse
        {
            Id = order.Id,
            BookTitle = order.Book.Title,
            Quantity = order.Quantity,
            TotalPrice = order.TotalPrice,
            OrderDate = order.OrderDate,
            Status = order.Status.ToString()
        };

        return Ok(response);
    }
}
