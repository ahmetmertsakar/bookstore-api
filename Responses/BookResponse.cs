namespace BookStoreAPI_Assessment.Responses;

public class BookResponse
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public string ISBN { get; set; }
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
    public int PublicationYear { get; set; }
    public string CategoryName { get; set; }
}
