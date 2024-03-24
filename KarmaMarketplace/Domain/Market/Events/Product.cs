﻿using KarmaMarketplace.Domain.Market.Entities;
using KarmaMarketplace.Infrastructure.EventDispatcher;

namespace KarmaMarketplace.Domain.Market.Events
{
    public class ProductCreated(ProductEntity product) : BaseEvent
    {
        public ProductEntity Product { get; set; } = product; 
    }
}
