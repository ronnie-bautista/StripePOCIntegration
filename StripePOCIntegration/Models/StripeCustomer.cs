﻿namespace StripePOCIntegration.Models
{
    public record StripeCustomer(
        string Name,
        string Email,
        string CustomerId);
}
