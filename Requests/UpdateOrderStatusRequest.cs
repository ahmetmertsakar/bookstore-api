using BookStoreAPI_Assessment.Models;

namespace BookStoreAPI_Assessment.Requests;

public class UpdateOrderStatusRequest
{
    public OrderStatus Status { get; set; }
}
