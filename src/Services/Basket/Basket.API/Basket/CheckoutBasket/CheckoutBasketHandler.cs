﻿
using BuildingBlocks.Messaging.Events;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Basket.API.Basket.CheckoutBasket
{
    public record CheckoutBasketCommand(BasketCheckoutDto BasketCheckoutDto)
        : ICommand<CheckoutBasketResult>;
    public record CheckoutBasketResult(bool IsSuccess);

    public class CheckoutBasketCommandValidator : AbstractValidator<CheckoutBasketCommand>
    {
        public CheckoutBasketCommandValidator()
        {
            RuleFor(x => x.BasketCheckoutDto).NotNull().WithMessage("Basket can't be null");
            RuleFor(x => x.BasketCheckoutDto.UserName).NotEmpty().WithMessage("Username is required");
        }
    }
    public class CheckoutBasketCommandHandler
        (IBasketRepository repository, IPublishEndpoint publishEndpoint, ILogger<CheckoutBasketCommandHandler> logger) 
        : ICommandHandler<CheckoutBasketCommand, CheckoutBasketResult>
    {
        public async Task<CheckoutBasketResult> Handle(CheckoutBasketCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var basket = await repository.GetBasketAsync(command.BasketCheckoutDto.UserName, cancellationToken);
                if (basket is null)
                    return new CheckoutBasketResult(false);

                var eventMessage = command.BasketCheckoutDto.Adapt<BasketCheckoutEvent>();
                eventMessage.TotalPrice = basket.TotalPrice;

                await publishEndpoint.Publish(eventMessage, cancellationToken);

                await repository.DeleteBasketAsync(command.BasketCheckoutDto.UserName, cancellationToken);

                return new CheckoutBasketResult(true);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occurred while handling CheckoutBasketCommand");
                return new CheckoutBasketResult(false);
            }
        }
    }
}
