using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iucs.lms.application.DTOs.Payment
{
    public record CreatePaymentDto(Guid UserId, Guid? CourseId, Guid? SubscriptionId, decimal Amount,
        string PaymentProvider);
    public record SubscribeDto(Guid UserId, string PlanName, decimal Price, int DurationMonths);
    public record CreateRefundDto(Guid PaymentTransactionId, Guid UserId, string Reason, decimal RefundAmount);
}
