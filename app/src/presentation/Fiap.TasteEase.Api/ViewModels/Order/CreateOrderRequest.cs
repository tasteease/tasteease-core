using System.Diagnostics.CodeAnalysis;

namespace Fiap.TasteEase.Api.ViewModels.Order;

[ExcludeFromCodeCoverage]
public record OrderRequest(
    string Description,
    Guid ClientId,
    IEnumerable<OrderFoodRequest>? Foods = null
);

[ExcludeFromCodeCoverage]
public record OrderFoodRequest(
    Guid FoodId,
    int Quantity
);