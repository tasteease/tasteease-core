using System.Diagnostics.CodeAnalysis;

namespace Fiap.TasteEase.Api.ViewModels;

[ExcludeFromCodeCoverage]
public class ResponseViewModel<T>
{
    public bool Error { get; set; } = false;
    public IEnumerable<string> ErrorMessages { get; set; } = null!;
    public T Data { get; set; }
}