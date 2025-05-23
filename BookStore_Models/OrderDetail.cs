using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace BookStore_Models;

public class OrderDetail
{
    public int Id { get; set; }
    public int OrderHeaderId { get; set; }
    [ValidateNever]
    public OrderHeader OrderHeader { get; set; }
    
    public int ProductId { get; set; }
    [ValidateNever]
    public Product Product { get; set; }

    public int Count { get; set; }
    public double Price { get; set; }
}