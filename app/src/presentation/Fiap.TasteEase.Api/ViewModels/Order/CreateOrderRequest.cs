namespace Fiap.TasteEase.Api.ViewModels.Order;

public record OrderRequest(
    string Description,
    Guid ClientId,
    IEnumerable<OrderFoodRequest>? Foods = null
);

public record OrderFoodRequest(
    Guid FoodId,
    int Quantity
);