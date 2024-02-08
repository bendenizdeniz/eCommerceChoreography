using Shared.Messages;

namespace Shared.Events;

public class PaymentCompletedEvent
{
    public Guid BuyerId { get; set; }

    public Guid OrderId { get; set; }

    public List<OrderItemMessage> OrderItems { get; set; }

    // public string Address { get; set; }

    // public CreditInfo CreditInfo { get; set; }
}

// public class CreditInfo
// {
//     public string EmbossName { get; set; }
//
//     public string CVC { get; set; }
//
//     public string Month { get; set; }
//
//     public string Year { get; set; }
//
//     public string MaskedNumber { get; set; }
// }