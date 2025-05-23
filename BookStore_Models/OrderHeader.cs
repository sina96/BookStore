using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace BookStore_Models;

public class OrderHeader
{
    public int Id { get; set; }
    
    public string ApplicationUserId { get; set; }
    [ValidateNever]
    public ApplicationUser ApplicationUser { get; set; }
    
    public DateTime OrderDate { get; set; }
    public DateTime ShippingDate { get; set; }
    public DateTime PaymentDate { get; set; }
    public DateOnly PaymentDueDate { get; set; }
    
    public double OrderTotal { get; set; }
    
    public string? OrderStatus { get; set; }
    public string? PaymentStatus { get; set; }
    public string? TrackingNumber { get; set; }
    public string? Carrier { get; set; }
    
    public string? PaymentIntentId { get; set; }
    public string? SessionId { get; set; }
    
    public string Name { get; set; }
    public string PhoneNumber { get; set; }
    public string StreetAddress { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string PostalCode { get; set; }
    
    public OrderDetail? OrderDetail { get; set; }
    
}