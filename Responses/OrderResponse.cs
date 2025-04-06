﻿namespace BookStoreAPI_Assessment.Responses;

public class OrderResponse
{
    public int Id { get; set; }
    public string BookTitle { get; set; }
    public int Quantity { get; set; }
    public decimal TotalPrice { get; set; }
    public DateTime OrderDate { get; set; }
    public string Status { get; set; }
}
