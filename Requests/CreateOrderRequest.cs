namespace BookStoreAPI_Assessment.Requests;

public class CreateOrderRequest
{
    public int BookId { get; set; }
    public int Quantity { get; set; }
}
