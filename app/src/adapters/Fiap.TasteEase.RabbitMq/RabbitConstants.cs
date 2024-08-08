namespace Fiap.TasteEase.RabbitMq;

public static class RabbitConstants
{
    public const string PaymentWebhookExchange = "tasteease-payment-webhook";
    public const string StartOrderExchange = "tasteease-start-order";
    public const string NotifyClientExchange = "tasteease-notify-client";
    public const string NotifyClientQueue = "tasteease-notify-client-queue";
    public const string PaymentWebhookQueue = "tasteease-payment-webhook-queue";
    public const string StartOrderQueue = "tasteease-start-order-queue";
}

public enum RabbitQueue
{
    StartOrder = 1,
    PaymentWebhook = 2,
    NotifyClient = 3
}