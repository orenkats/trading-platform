namespace PaymentService.Application.DTOs;
public class PaymentRequestDto
{
    public Guid UserId { get; set; }
    public decimal Amount { get; set; }
}