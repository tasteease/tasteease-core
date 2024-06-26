﻿using System.Diagnostics.CodeAnalysis;
using Fiap.TasteEase.Application.UseCases.OrderUseCase.Queries.GetById;
using Fiap.TasteEase.Domain.Aggregates.OrderAggregate.ValueObjects;
using FluentResults;
using MediatR;

namespace Fiap.TasteEase.Application.UseCases.OrderUseCase.Queries.GetAll;

[ExcludeFromCodeCoverage]
public class GetOrderAllQuery : IRequest<Result<IEnumerable<OrderResponseQuery>>>
{
    public Guid? ClientId { get; init; }
    public List<OrderStatus> Status { get; init; }
}