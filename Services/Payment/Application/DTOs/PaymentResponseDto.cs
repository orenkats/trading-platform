namespace PaymentService.Application.DTOs;
public class PaymentResponseDto
{
    public Guid PaymentId { get; set; }
    public string Status { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
}